namespace DevExpress.XtraPrinting.Export.Xl
{
    using DevExpress.Export.Xl;
    using DevExpress.Office.Crypto;
    using DevExpress.Printing.Native;
    using DevExpress.Utils;
    using DevExpress.XtraExport;
    using DevExpress.XtraExport.Implementation;
    using DevExpress.XtraExport.Xls;
    using DevExpress.XtraExport.Xlsx;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Localization;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Shape;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Forms;

    public class ExcelExportProvider : IXlExportProvider, ITableExportProvider
    {
        private const int maxXlsRowCount = 0x10000;
        private const short maxXlsColumnCount = 0x100;
        private const int maxXlsxRowCount = 0x100000;
        private const short maxXlsxColumnCount = 0x4000;
        private const int MaxRowHeight = 0x222;
        private IXlExport export;
        private IXlSheet sheet;
        private IXlShapeContainer container;
        private IXlCell cell;
        private List<int> colWidths;
        private List<int> rowHeights;
        private DevExpress.XtraPrinting.Native.XlExportContext excelExportContext;
        private ProgressMaster progressMaster;
        private ObjectInfo currentObjectInfo;
        private Dictionary<MultiKey, XlCellFormatting> styleCache = new Dictionary<MultiKey, XlCellFormatting>();
        private Dictionary<int, XlCellRange> mergeCache = new Dictionary<int, XlCellRange>();
        private bool exportCrossReferences;
        private List<XlHyperlink> crossReferencesCache = new List<XlHyperlink>();
        private Dictionary<string, int[]> anchorCache = new Dictionary<string, int[]>();
        private Dictionary<int, int> pageIndexes = new Dictionary<int, int>();
        private bool hasIntersectedBricks;

        public ExcelExportProvider(Stream stream, DevExpress.XtraPrinting.Native.XlExportContext exportContext, ProgressMaster progressMaster)
        {
            this.excelExportContext = exportContext;
            if (progressMaster != null)
            {
                this.progressMaster = progressMaster;
            }
            if (exportContext.XlExportOptions is XlsExportOptions)
            {
                this.export = new XlsDataAwareExporter();
                XlsExportOptions xlExportOptions = (XlsExportOptions) exportContext.XlExportOptions;
                ((XlsDataAwareExporterOptions) ((XlsDataAwareExporter) this.export).DocumentOptions).UseCustomPalette = xlExportOptions.WorkbookColorPaletteCompliance == WorkbookColorPaletteCompliance.ReducePaletteForExactColors;
            }
            else
            {
                this.export = new XlsxDataAwareExporter();
            }
            ((IXlDocumentOptionsEx) this.export.DocumentOptions).UseDeviceIndependentPixels = true;
            ((IXlDocumentOptionsEx) this.export.DocumentOptions).TruncateStringsToMaxLength = true;
            this.export.BeginExport(stream, this.GetEncryptionOptions(exportContext));
        }

        private static XlAnchorPoint CalculateAnchorPoint(XlAnchorPoint source, List<int> colWidths, List<int> rowHeights)
        {
            int column = source.Column;
            int row = source.Row;
            int columnOffsetInPixels = source.ColumnOffsetInPixels;
            int rowOffsetInPixels = source.RowOffsetInPixels;
            int cellWidthInPixels = colWidths[column];
            int cellHeightInPixels = rowHeights[row];
            while ((columnOffsetInPixels < 0) && (column > 0))
            {
                column--;
                cellWidthInPixels = colWidths[column];
                columnOffsetInPixels += cellWidthInPixels;
            }
            while (columnOffsetInPixels > cellWidthInPixels)
            {
                columnOffsetInPixels -= cellWidthInPixels;
                column++;
                cellWidthInPixels = colWidths[column];
            }
            while ((rowOffsetInPixels < 0) && (row > 0))
            {
                row--;
                cellHeightInPixels = rowHeights[row];
                rowOffsetInPixels += cellHeightInPixels;
            }
            while (rowOffsetInPixels > cellHeightInPixels)
            {
                rowOffsetInPixels -= cellHeightInPixels;
                row++;
                cellHeightInPixels = rowHeights[row];
            }
            return new XlAnchorPoint(column, row, columnOffsetInPixels, rowOffsetInPixels, cellWidthInPixels, cellHeightInPixels);
        }

        private void ChangeMerge(int objectIndex, int addedRowCount)
        {
            if (this.mergeCache.ContainsKey(objectIndex))
            {
                XlCellRange range = this.mergeCache[objectIndex];
                range.BottomRight = new XlCellPosition(range.BottomRight.Column, range.BottomRight.Row + addedRowCount);
            }
        }

        private bool ColorExists(Color color) => 
            (color != Color.Transparent) && (color != Color.Empty);

        public void CreateDocument(Document document)
        {
            this.CreateDocumentCore(document);
            string data = "The source document contains intersecting bricks. The layout of the resulting document may differ from the source document layout.";
            if (this.hasIntersectedBricks)
            {
                Tracer.TraceWarning("DXperience.Reporting", data);
            }
        }

        internal void CreateDocumentCore(Document document)
        {
            this.SetDocumentProperties();
            if (document.PageCount > 0)
            {
                if (!this.excelExportContext.IsPageExport)
                {
                    this.exportCrossReferences = true;
                    this.CreateSheet(document, this.CreateLayoutBuilder(document), this.excelExportContext.XlExportOptions.SheetName);
                }
                else
                {
                    int[] indices = PageRangeParser.GetIndices(this.XlExportContext.XlExportOptions.PageRange, document.PageCount);
                    if (indices.Length <= 1)
                    {
                        int pageIndex = string.IsNullOrEmpty(this.XlExportContext.XlExportOptions.PageRange) ? 0 : (int.Parse(this.XlExportContext.XlExportOptions.PageRange) - 1);
                        this.CreateSheet(document, pageIndex);
                    }
                    else
                    {
                        this.progressMaster.InitializeRangeByPages(indices.Length);
                        int[] numArray2 = indices;
                        int index = 0;
                        while (true)
                        {
                            if (index >= numArray2.Length)
                            {
                                this.progressMaster.AllPagesExported();
                                break;
                            }
                            int pageIndex = numArray2[index];
                            this.CreateSheet(document, pageIndex);
                            this.progressMaster.PageExported();
                            index++;
                        }
                    }
                }
            }
            this.export.EndExport();
        }

        private ILayoutBuilder CreateLayoutBuilder(Document document) => 
            new XlLayoutBuilder(document, this.XlExportContext);

        private ILayoutBuilder CreateLayoutBuilder(Document document, int index) => 
            new XlPageLayoutBuilder(document.Pages[index], this.XlExportContext);

        private void CreateSheet(Document document, int pageIndex)
        {
            using (XlPageLayoutBuilder builder = (XlPageLayoutBuilder) this.CreateLayoutBuilder(document, pageIndex))
            {
                this.CreateSheet(document, builder, this.GetFinalSheetName(pageIndex));
            }
        }

        private void CreateSheet(Document document, ILayoutBuilder layoutBuilder, string sheetName)
        {
            this.sheet = this.export.BeginSheet();
            this.container = this.sheet as IXlShapeContainer;
            bool correctImportBrickBounds = document.CorrectImportBrickBounds;
            ReadonlyPageData data = this.excelExportContext.IsPageExport ? this.excelExportContext.DrawingPage.PageData : this.excelExportContext.PrintingSystem.PageSettings.Data;
            MarginsF margins = GraphicsUnitConverter.Convert(data.MarginsF, (float) 300f, (float) 1f);
            this.SetPageSettings(margins, data.PaperKind, data.Landscape, sheetName, this.excelExportContext.RightToLeftLayout);
            MegaTable megaTable = new MegaTable(layoutBuilder.BuildLayoutControls(), !this.XlExportContext.RawDataMode, correctImportBrickBounds, 0x222);
            this.ValidateMegaTableCore(this.XlExportContext.XlExportOptions, megaTable);
            if (megaTable.Objects.Length != 0)
            {
                try
                {
                    if (this.progressMaster != null)
                    {
                        this.progressMaster.InitializeRangeByObjects(megaTable.Objects.Length);
                    }
                    this.SetCurrentSheetTable(megaTable);
                    this.SetMergedCells();
                    if (this.exportCrossReferences)
                    {
                        this.SetCrossReferences();
                    }
                }
                finally
                {
                    if (this.progressMaster != null)
                    {
                        this.progressMaster.AllObjectsExported();
                    }
                    this.export.EndSheet();
                }
            }
        }

        void ITableExportProvider.SetCellImage(System.Drawing.Image image, TableCellImageInfo imageInfo, Rectangle bounds, PaddingInfo padding)
        {
            int columnOffsetInPixels = 0;
            int rowOffsetInPixels = 0;
            int column = (this.CurrentColIndex + this.CurrentColSpan) - 1;
            int num4 = this.colWidths[column];
            int row = (this.CurrentRowIndex + this.CurrentRowSpan) - 1;
            int num6 = this.rowHeights[row];
            IXlPicture picture = this.export.BeginPicture();
            picture.Image = image;
            picture.Format = (image is Metafile) ? ImageFormat.Emf : image.RawFormat;
            if (this.excelExportContext.XlExportOptions.ExportHyperlinks && !this.CurrentData.TableCell.HasCrossReference)
            {
                picture.HyperlinkClick.TargetUri = this.CurrentData.TableCell.Url;
            }
            picture.AnchorType = XlAnchorType.TwoCell;
            picture.AnchorBehavior = XlAnchorType.OneCell;
            if ((this.CurrentData.Style != null) && ((this.CurrentData.Style.Sides != BorderSide.None) && (this.CurrentData.Style.BorderWidth > 0f)))
            {
                int num11 = Convert.ToInt32(this.CurrentData.Style.BorderWidth);
                if ((BorderSide.Left & this.CurrentData.Style.Sides) != BorderSide.None)
                {
                    columnOffsetInPixels += num11;
                }
                if ((BorderSide.Right & this.CurrentData.Style.Sides) != BorderSide.None)
                {
                    num4 -= num11;
                }
                if ((BorderSide.Top & this.CurrentData.Style.Sides) != BorderSide.None)
                {
                    rowOffsetInPixels += num11;
                }
                if ((BorderSide.Bottom & this.CurrentData.Style.Sides) != BorderSide.None)
                {
                    num6 -= num11;
                }
            }
            picture.TopLeft = new XlAnchorPoint(this.CurrentColIndex, this.CurrentRowIndex, columnOffsetInPixels, rowOffsetInPixels, this.colWidths[this.CurrentColIndex], this.rowHeights[this.CurrentRowIndex]);
            picture.BottomRight = new XlAnchorPoint(column, row, num4, num6, this.colWidths[column], this.rowHeights[row]);
            Size size = GraphicsUnitConverter.Convert(image.Size, image.HorizontalResolution, padding.Dpi);
            picture.SourceRectangle = new XlSourceRectangle(((double) padding.Left) / ((double) size.Width), ((double) padding.Top) / ((double) size.Height), ((double) padding.Right) / ((double) size.Width), ((double) padding.Bottom) / ((double) size.Height));
            this.export.EndPicture();
        }

        void ITableExportProvider.SetCellShape(ShapeBase shape, TableCellLineInfo lineInfo, Color fillColor, int angle, PaddingInfo padding)
        {
            if ((shape is ShapeLine) || (shape is ShapeRectangle))
            {
                int column = (this.CurrentColIndex + this.CurrentColSpan) - 1;
                int columnOffsetInPixels = this.colWidths[column];
                int row = (this.CurrentRowIndex + this.CurrentRowSpan) - 1;
                int rowOffsetInPixels = this.rowHeights[row];
                XlShape shape2 = null;
                XlColor xlColor = this.GetXlColor(fillColor);
                XlColor color = lineInfo.LineVisible ? this.GetXlColor(lineInfo.LineColor) : XlColor.Empty;
                XlOutlineDashing xlOutlineDashing = GetXlOutlineDashing(lineInfo.LineStyle);
                int left = padding.Left;
                int top = padding.Top;
                columnOffsetInPixels -= padding.Right;
                rowOffsetInPixels -= padding.Bottom;
                if (!(shape is ShapeLine) || !lineInfo.LineVisible)
                {
                    if (!(shape is ShapeRectangle) || (!this.ColorExists((Color) xlColor) && !this.ColorExists((Color) color)))
                    {
                        return;
                    }
                    shape2 = XlShape.Rectangle(fillColor, lineInfo.LineColor, xlOutlineDashing);
                }
                else
                {
                    shape2 = XlShape.Line(color, xlOutlineDashing);
                    int convertedAngle = this.GetConvertedAngle(angle, shape2.Frame);
                    if (convertedAngle != 0x2d)
                    {
                        int num8 = -padding.Top - padding.Bottom;
                        int currentRowIndex = this.CurrentRowIndex;
                        while (true)
                        {
                            if (currentRowIndex > row)
                            {
                                int num9 = -padding.Left - padding.Right;
                                int currentColIndex = this.CurrentColIndex;
                                while (true)
                                {
                                    if (currentColIndex > column)
                                    {
                                        if (convertedAngle < 0x2d)
                                        {
                                            int num12 = Convert.ToInt32((double) ((((double) num9) / 2.0) - ((((double) num8) / 2.0) * Math.Tan((convertedAngle * 3.1415926535897931) / 180.0))));
                                            left += num12;
                                            columnOffsetInPixels -= num12;
                                        }
                                        else
                                        {
                                            int num13 = Convert.ToInt32((double) ((((double) num8) / 2.0) - ((((double) num9) / 2.0) * Math.Tan(((90 - convertedAngle) * 3.1415926535897931) / 180.0))));
                                            top += num13;
                                            rowOffsetInPixels -= num13;
                                        }
                                        break;
                                    }
                                    num9 += this.colWidths[currentColIndex];
                                    currentColIndex++;
                                }
                                break;
                            }
                            num8 += this.rowHeights[currentRowIndex];
                            currentRowIndex++;
                        }
                    }
                }
                if (lineInfo.LineVisible)
                {
                    shape2.Outline.Width = GraphicsUnitConverter.Convert(lineInfo.LineWidth, (float) 96f, (float) 72f);
                    shape2.Outline.StrokeAlignment = XlOutlineStrokeAlignment.Inset;
                }
                XlAnchorPoint topLeft = CalculateAnchorPoint(new XlAnchorPoint(this.CurrentColIndex, this.CurrentRowIndex, left, top, this.colWidths[this.CurrentColIndex], this.rowHeights[this.CurrentRowIndex]), this.colWidths, this.rowHeights);
                XlAnchorPoint bottomRight = CalculateAnchorPoint(new XlAnchorPoint(column, row, columnOffsetInPixels, rowOffsetInPixels, this.colWidths[column], this.rowHeights[row]), this.colWidths, this.rowHeights);
                if (this.excelExportContext.XlExportOptions.ExportHyperlinks && !this.CurrentData.TableCell.HasCrossReference)
                {
                    shape2.HyperlinkClick.TargetUri = this.CurrentData.TableCell.Url;
                }
                shape2.SetTwoCellAnchor(topLeft, bottomRight, XlAnchorType.TwoCell);
                this.container.AddShape(shape2);
            }
        }

        void ITableExportProvider.SetCellText(object textValue)
        {
            if (textValue is string)
            {
                textValue = HotkeyPrefixHelper.PreprocessHotkeyPrefixesInString((string) textValue, this.CurrentData.Style);
            }
            ((IXlExportProvider) this).SetCellData(textValue);
        }

        void IXlExportProvider.SetCellData(object data)
        {
            XlVariantValue empty = XlVariantValue.FromObject(data, true);
            if (!empty.IsEmpty)
            {
                if (this.XlExportContext.RawDataMode && empty.IsBoolean)
                {
                    empty = empty.ToText();
                }
                if (empty.IsText)
                {
                    if (string.IsNullOrEmpty(empty.TextValue))
                    {
                        empty = XlVariantValue.Empty;
                    }
                    else
                    {
                        empty.TextValue = empty.TextValue.Replace("\r\n", "\n");
                    }
                }
                this.cell.Value = empty;
            }
        }

        void IXlExportProvider.SetRichTextRuns(IList<XlRichTextRun> richTextRuns)
        {
            XlRichTextString str = new XlRichTextString();
            foreach (XlRichTextRun run in richTextRuns)
            {
                str.Runs.Add(run);
            }
            this.cell.SetRichText(str);
        }

        private void FoundIntersectingBricks()
        {
            this.hasIntersectedBricks = true;
        }

        private int GetAngle(ITableCell tableCell)
        {
            LabelBrick brick = tableCell as LabelBrick;
            if ((brick == null) || ((brick.Angle % 360f) == 0f))
            {
                return 0;
            }
            float num = brick.Angle % 360f;
            if (num < 0f)
            {
                num += 360f;
            }
            int num2 = Convert.ToInt16(num);
            return (((num2 == 90) || (num2 == 270)) ? num2 : 0);
        }

        private int GetConvertedAngle(int angle, XlGraphicFrame frame)
        {
            int num = angle;
            if (num >= 180)
            {
                num -= 180;
            }
            if (num > 90)
            {
                num = 180 - num;
                frame.FlipVertical = true;
            }
            return num;
        }

        private EncryptionOptions GetEncryptionOptions(DevExpress.XtraPrinting.Native.XlExportContext exportContext)
        {
            if (string.IsNullOrEmpty(exportContext.XlExportOptions.EncryptionOptions.Password))
            {
                return null;
            }
            EncryptionOptions options1 = new EncryptionOptions();
            options1.Password = exportContext.XlExportOptions.EncryptionOptions.Password;
            options1.Type = (ModelEncryptionType) exportContext.XlExportOptions.EncryptionOptions.Type;
            return options1;
        }

        private string GetFinalSheetName(int pageIndex)
        {
            string str = $"{this.excelExportContext.XlExportOptions.SheetName}{pageIndex + 1}";
            if (!this.pageIndexes.ContainsKey(pageIndex))
            {
                this.pageIndexes.Add(pageIndex, 1);
            }
            else
            {
                int num = pageIndex;
                int num2 = this.pageIndexes[num] + 1;
                this.pageIndexes[num] = num2;
                str = $"{str}_{num2}";
            }
            XlSheetCreatedEventArgs e = new XlSheetCreatedEventArgs(pageIndex, str);
            this.excelExportContext.PrintingSystem.OnXlSheetCreated(this, e);
            return e.SheetName;
        }

        private XlCellFormatting GetFormatting(ObjectInfo objectInfo)
        {
            XlCellFormatting formatting;
            BrickViewData data = (BrickViewData) objectInfo.Object;
            System.Type type = data.TableCell.TextValue?.GetType();
            int angle = this.GetAngle(data.TableCell);
            object[] keyParts = new object[] { data.Style, data.TableCell.FormatString, data.TableCell.XlsxFormatString, type, angle };
            MultiKey key = new MultiKey(keyParts);
            if (!this.styleCache.TryGetValue(key, out formatting))
            {
                TextExportMode textExportMode = ToTextExportMode(data.TableCell.XlsExportNativeFormat, this.excelExportContext.XlExportOptions.TextExportMode);
                formatting = XlCellFormattingCreator.CreateFormatting(data.Style, data.TableCell, this.excelExportContext.RawDataMode, textExportMode, (float) angle);
                this.styleCache.Add(key, formatting);
            }
            return formatting;
        }

        private XlColor GetXlColor(Color color) => 
            this.ColorExists(color) ? XlColor.FromArgb(color.ToArgb()) : XlColor.Empty;

        private static XlOutlineDashing GetXlOutlineDashing(DashStyle lineStyle)
        {
            switch (lineStyle)
            {
                case DashStyle.Solid:
                    return XlOutlineDashing.Solid;

                case DashStyle.Dash:
                    return XlOutlineDashing.SystemDash;

                case DashStyle.Dot:
                    return XlOutlineDashing.SystemDot;

                case DashStyle.DashDot:
                    return XlOutlineDashing.SystemDashDot;

                case DashStyle.DashDotDot:
                    return XlOutlineDashing.SystemDashDotDot;
            }
            return XlOutlineDashing.Solid;
        }

        private static string IntToLetter(int intCol)
        {
            int num2;
            int num = (intCol / 0x2a4) + 0x40;
            int num3 = (intCol % 0x1a) + 0x41;
            if (((((intCol % 0x2a4) / 0x1a) + 0x40) == 0x40) && (num > 0x40))
            {
                num2 = 90;
                num--;
            }
            char ch = (num > 0x40) ? ((char) num) : ' ';
            char ch2 = (num2 > 0x40) ? ((char) num2) : ' ';
            char ch3 = (char) num3;
            return (ch + ch2 + ch3).Trim();
        }

        private bool IsDrawingObject(ObjectInfo objectInfo)
        {
            BrickViewData data = (BrickViewData) objectInfo.Object;
            return (!this.XlExportContext.RawDataMode && ((data.TableCell is ImageBrick) || ((data.TableCell is LineBrick) || ((data.TableCell is ShapeBrick) || (data.TableCell is BarCodeBrick)))));
        }

        private void RemoveMerge(int objectIndex)
        {
            this.mergeCache.Remove(objectIndex);
        }

        private void SetAnchor(int col, int row, ITableCell tableCell)
        {
            AnchorCell cell = tableCell as AnchorCell;
            if ((cell != null) && (cell.InnerCell != null))
            {
                this.SetAnchor(col, row, cell.InnerCell);
            }
            else
            {
                string str = (tableCell is VisualBrick) ? ((VisualBrick) tableCell).AnchorName : ((cell != null) ? cell.AnchorName : string.Empty);
                if (!string.IsNullOrEmpty(str) && !this.anchorCache.ContainsKey(str))
                {
                    int[] numArray1 = new int[] { col, row };
                    this.anchorCache.Add(str, numArray1);
                }
            }
        }

        private void SetCellMerge(int objectIndex, XlCellRange cellRange)
        {
            this.mergeCache.Add(objectIndex, cellRange);
        }

        private void SetCellUrl(ITableCell tableCell, XlCellPosition cellPosition)
        {
            if (!string.IsNullOrEmpty(tableCell.Url))
            {
                XlHyperlink hyperlink1 = new XlHyperlink();
                hyperlink1.TargetUri = tableCell.Url;
                hyperlink1.Reference = new XlCellRange(cellPosition);
                XlHyperlink item = hyperlink1;
                if (!tableCell.HasCrossReference)
                {
                    this.sheet.Hyperlinks.Add(item);
                }
                else if (this.exportCrossReferences)
                {
                    this.crossReferencesCache.Add(item);
                }
            }
        }

        private void SetCrossReferences()
        {
            foreach (XlHyperlink hyperlink in this.crossReferencesCache)
            {
                foreach (KeyValuePair<string, int[]> pair in this.anchorCache)
                {
                    if (hyperlink.TargetUri == pair.Key)
                    {
                        string str = IntToLetter(pair.Value[0]) + (((int) pair.Value[1]) + 1).ToString();
                        hyperlink.TargetUri = "#" + str;
                        this.sheet.Hyperlinks.Add(hyperlink);
                        break;
                    }
                }
            }
        }

        private void SetCurrentSheetTable(MegaTable megaTable)
        {
            ObjectInfo[] objects = megaTable.Objects;
            this.colWidths = megaTable.ColWidths;
            this.rowHeights = megaTable.RowHeights;
            Comparison<ObjectInfo> comparison = <>c.<>9__61_0;
            if (<>c.<>9__61_0 == null)
            {
                Comparison<ObjectInfo> local1 = <>c.<>9__61_0;
                comparison = <>c.<>9__61_0 = delegate (ObjectInfo x, ObjectInfo y) {
                    int num = x.RowIndex.CompareTo(y.RowIndex);
                    return (num != 0) ? num : x.ColIndex.CompareTo(y.ColIndex);
                };
            }
            Array.Sort<ObjectInfo>(objects, comparison);
            if (megaTable.ColumnCount > this.export.DocumentOptions.MaxColumnCount)
            {
                string data = "The maximum number of columns supported for the current document format is exceeded. The exceeding columns will not be exported.";
                Tracer.TraceWarning("DXperience.Reporting", data);
                this.colWidths = this.colWidths.Take<int>(this.export.DocumentOptions.MaxColumnCount).ToList<int>();
            }
            for (int i = 0; i < this.colWidths.Count; i++)
            {
                IXlColumn column = this.export.BeginColumn();
                column.WidthInPixels = this.colWidths[i];
                this.export.EndColumn();
            }
            new CellFiller(this, this.export, megaTable.RowHeights).FillCells(objects);
        }

        private void SetDocumentProperties()
        {
            XlDocumentOptions documentOptions = this.excelExportContext.XlExportOptions.DocumentOptions;
            this.export.DocumentProperties.Application = documentOptions.Application;
            this.export.DocumentProperties.Author = documentOptions.Author;
            this.export.DocumentProperties.Category = documentOptions.Category;
            this.export.DocumentProperties.Company = documentOptions.Company;
            this.export.DocumentProperties.Description = documentOptions.Comments;
            this.export.DocumentProperties.Keywords = documentOptions.Tags;
            this.export.DocumentProperties.Subject = documentOptions.Subject;
            this.export.DocumentProperties.Title = documentOptions.Title;
            this.export.DocumentProperties.Custom["DXVersion"] = "19.2.9.0";
        }

        private void SetDrawingObject(ObjectInfo objectInfo)
        {
            this.currentObjectInfo = objectInfo;
            BrickViewData data = (BrickViewData) objectInfo.Object;
            ((BrickExporter) this.excelExportContext.PrintingSystem.ExportersFactory.GetExporter(data.TableCell)).FillXlTableCell(this);
            if (this.excelExportContext.XlExportOptions.ExportHyperlinks && this.exportCrossReferences)
            {
                this.SetAnchor(objectInfo.ColIndex, objectInfo.RowIndex, data.TableCell);
            }
            if (this.progressMaster != null)
            {
                this.progressMaster.ObjectExported();
            }
        }

        private void SetMergedCells()
        {
            foreach (KeyValuePair<int, XlCellRange> pair in this.mergeCache)
            {
                this.sheet.MergedCells.Add(pair.Value, false);
            }
            this.mergeCache.Clear();
        }

        private void SetObjectInfo(ObjectInfo objectInfo, IXlCell cell)
        {
            this.cell = cell;
            this.currentObjectInfo = objectInfo;
            BrickViewData data = (BrickViewData) objectInfo.Object;
            ((BrickExporter) this.excelExportContext.PrintingSystem.ExportersFactory.GetExporter(data.TableCell)).FillXlTableCell(this);
            if (this.excelExportContext.XlExportOptions.ExportHyperlinks)
            {
                if (cell != null)
                {
                    this.SetCellUrl(data.TableCell, cell.Position);
                }
                if (this.exportCrossReferences)
                {
                    this.SetAnchor(objectInfo.ColIndex, objectInfo.RowIndex, data.TableCell);
                }
            }
            if (this.progressMaster != null)
            {
                this.progressMaster.ObjectExported();
            }
        }

        private void SetPageSettings(MarginsF margins, PaperKind paperKind, bool landscape, string sheetName, bool pageRightToLeftLayout)
        {
            XlPageMargins margins2 = new XlPageMargins {
                Bottom = margins.Bottom,
                Left = margins.Left,
                Right = margins.Right,
                Top = margins.Top
            };
            this.sheet.PageMargins = margins2;
            this.sheet.PageSetup = new XlPageSetup();
            this.sheet.PageSetup.PaperKind = paperKind;
            this.sheet.PageSetup.PageOrientation = landscape ? XlPageOrientation.Landscape : XlPageOrientation.Portrait;
            this.sheet.PageSetup.FitToPage = this.excelExportContext.XlExportOptions.FitToPrintedPageWidth || this.excelExportContext.XlExportOptions.FitToPrintedPageHeight;
            if (this.sheet.PageSetup.FitToPage)
            {
                this.sheet.PageSetup.FitToWidth = this.excelExportContext.XlExportOptions.FitToPrintedPageWidth ? 1 : 0;
                this.sheet.PageSetup.FitToHeight = this.excelExportContext.XlExportOptions.FitToPrintedPageHeight ? 1 : 0;
            }
            this.sheet.ViewOptions.ShowGridLines = this.excelExportContext.XlExportOptions.ShowGridLines;
            this.sheet.ViewOptions.RightToLeft = this.excelExportContext.XlExportOptions.RightToLeftDocument.ToBoolean(pageRightToLeftLayout);
            this.sheet.PrintOptions = new XlPrintOptions();
            this.sheet.IgnoreErrors = (DevExpress.Export.Xl.XlIgnoreErrors) this.excelExportContext.XlExportOptions.IgnoreErrors;
            this.sheet.Name = sheetName;
        }

        private static TextExportMode ToTextExportMode(DefaultBoolean value, TextExportMode defaultTextExportMode) => 
            (value == DefaultBoolean.True) ? TextExportMode.Value : ((value == DefaultBoolean.False) ? TextExportMode.Text : defaultTextExportMode);

        internal static void ValidateMegaTable(XlExportOptionsBase exportOptions, MegaTable megaTable)
        {
            if (exportOptions is XlsExportOptions)
            {
                if ((megaTable.RowCount > 0x10000) && !((XlsExportOptions) exportOptions).Suppress65536RowsWarning)
                {
                    throw new Exception(PreviewLocalizer.GetString(PreviewStringId.Msg_XlsMoreThanMaxRows));
                }
                if ((megaTable.ColumnCount > 0x100) && !((XlsExportOptions) exportOptions).Suppress256ColumnsWarning)
                {
                    throw new Exception(PreviewLocalizer.GetString(PreviewStringId.Msg_XlsMoreThanMaxColumns));
                }
            }
            else
            {
                if (megaTable.RowCount > 0x100000)
                {
                    throw new Exception(PreviewLocalizer.GetString(PreviewStringId.Msg_XlsxMoreThanMaxRows));
                }
                if (megaTable.ColumnCount > 0x4000)
                {
                    throw new Exception(PreviewLocalizer.GetString(PreviewStringId.Msg_XlsxMoreThanMaxColumns));
                }
            }
        }

        protected virtual void ValidateMegaTableCore(XlExportOptionsBase exportOptions, MegaTable megaTable)
        {
            ValidateMegaTable(exportOptions, megaTable);
        }

        private int CurrentRowIndex =>
            this.currentObjectInfo.RowIndex;

        private int CurrentColIndex =>
            this.currentObjectInfo.ColIndex;

        private int CurrentRowSpan =>
            this.currentObjectInfo.RowSpan;

        private int CurrentColSpan =>
            this.currentObjectInfo.ColSpan;

        public BrickViewData CurrentData =>
            (BrickViewData) this.currentObjectInfo.Object;

        public DevExpress.XtraPrinting.Native.XlExportContext XlExportContext =>
            this.excelExportContext;

        ExportContext ITableExportProvider.ExportContext =>
            this.excelExportContext;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelExportProvider.<>c <>9 = new ExcelExportProvider.<>c();
            public static Comparison<ObjectInfo> <>9__61_0;

            internal int <SetCurrentSheetTable>b__61_0(ObjectInfo x, ObjectInfo y)
            {
                int num = x.RowIndex.CompareTo(y.RowIndex);
                return ((num != 0) ? num : x.ColIndex.CompareTo(y.ColIndex));
            }
        }

        public class CellFiller
        {
            private ExcelExportProvider provider;
            private IXlExport export;
            private IList<int> rowHeights;
            private ObjectInfo nextObject;
            private ObjectInfo[] mtObjects;
            private int objectIndex;
            private KeyValuePair<Coords, FormattingSpan> nextFormatting;
            private static Coords maxCoord = new Coords(0x7fffffff, 0x7fffffff);
            private Coords nextObjectCoord;
            private Coords nextFormattingCoord;
            private Coords nextCellCoord;
            private IXlRow currentRow;
            private SortedDictionary<Coords, FormattingSpan> styleRange = new SortedDictionary<Coords, FormattingSpan>();

            public CellFiller(ExcelExportProvider provider, IXlExport export, IList<int> rowHeights)
            {
                this.provider = provider;
                this.export = export;
                this.rowHeights = rowHeights;
            }

            private void AddStyleRange(Coords coords, FormattingSpan formattingSpan)
            {
                if (this.styleRange.ContainsKey(coords))
                {
                    this.FoundIntersectingBricks();
                    this.RemoveMerge(this.styleRange[coords].objectIndex);
                    if ((this.mtObjects[formattingSpan.objectIndex].ColIndex <= this.mtObjects[this.styleRange[coords].objectIndex].ColIndex) && (this.styleRange[coords].colSpan > formattingSpan.colSpan))
                    {
                        Coords coords2 = new Coords(coords.Col + formattingSpan.colSpan, coords.Row);
                        this.AddStyleRange(coords2, new FormattingSpan(this.styleRange[coords].formatting, this.styleRange[coords].colSpan - formattingSpan.colSpan, this.styleRange[coords].objectIndex));
                    }
                    this.styleRange.Remove(coords);
                }
                this.styleRange.Add(coords, formattingSpan);
            }

            internal void ChangeMerge(int objectIndex, int addedRowCount)
            {
                this.provider.ChangeMerge(objectIndex, addedRowCount);
            }

            private void CheckIntersection(int columnIndex, FormattingSpan formattingSpan, ref int count)
            {
                if (columnIndex < this.export.CurrentColumnIndex)
                {
                    this.FoundIntersectingBricks();
                    this.RemoveMerge(formattingSpan.objectIndex);
                    count -= this.export.CurrentColumnIndex - columnIndex;
                }
                int columnIndexForIntersectionWithNextObject = this.GetColumnIndexForIntersectionWithNextObject(count);
                int columnIndexForIntersectionWithNextFormattingSpan = this.GetColumnIndexForIntersectionWithNextFormattingSpan(count);
                if ((columnIndexForIntersectionWithNextObject >= 0) || (columnIndexForIntersectionWithNextFormattingSpan >= 0))
                {
                    int num4;
                    this.FoundIntersectingBricks();
                    this.RemoveMerge(formattingSpan.objectIndex);
                    int num3 = (this.export.CurrentColumnIndex + count) - 1;
                    if ((columnIndexForIntersectionWithNextFormattingSpan >= 0) && ((columnIndexForIntersectionWithNextObject < 0) || (columnIndexForIntersectionWithNextObject > columnIndexForIntersectionWithNextFormattingSpan)))
                    {
                        num4 = (this.styleRange.ElementAt<KeyValuePair<Coords, FormattingSpan>>(1).Key.Col + this.styleRange.ElementAt<KeyValuePair<Coords, FormattingSpan>>(1).Value.colSpan) - 1;
                        count = this.styleRange.ElementAt<KeyValuePair<Coords, FormattingSpan>>(1).Key.Col - this.export.CurrentColumnIndex;
                    }
                    else
                    {
                        num4 = (this.mtObjects[this.objectIndex].ColIndex + this.mtObjects[this.objectIndex].ColSpan) - 1;
                        count = this.mtObjects[this.objectIndex].ColIndex - this.export.CurrentColumnIndex;
                    }
                    if (num3 > num4)
                    {
                        Coords coords = new Coords(num4 + 1, this.export.CurrentRowIndex);
                        this.AddStyleRange(coords, new FormattingSpan(formattingSpan.formatting, num3 - num4, formattingSpan.objectIndex));
                    }
                }
            }

            public void FillCells(ObjectInfo[] objects)
            {
                this.mtObjects = objects;
                this.nextObject = this.mtObjects[this.objectIndex];
                this.nextFormatting = new KeyValuePair<Coords, FormattingSpan>();
                this.nextObjectCoord = new Coords(this.nextObject.ColIndex, this.nextObject.RowIndex);
                this.nextFormattingCoord = maxCoord;
                this.nextCellCoord = this.nextObjectCoord;
                this.currentRow = this.export.BeginRow();
                this.currentRow.HeightInPixels = this.rowHeights[this.currentRow.RowIndex];
                while (this.TryMoveToFillingCell())
                {
                    if (this.nextCellCoord.Equals(this.nextFormattingCoord))
                    {
                        FormattingSpan formattingSpan = this.nextFormatting.Value;
                        this.FillCellsWithStyle(this.nextFormattingCoord.Col, formattingSpan);
                        this.styleRange.Remove(this.nextFormatting.Key);
                    }
                    else
                    {
                        this.FillCellWithData(this.nextObject);
                        do
                        {
                            this.objectIndex++;
                            this.nextObject = (this.objectIndex < this.mtObjects.Length) ? this.mtObjects[this.objectIndex] : null;
                            this.nextObjectCoord = (this.nextObject != null) ? new Coords(this.nextObject.ColIndex, this.nextObject.RowIndex) : maxCoord;
                        }
                        while ((this.nextObjectCoord.Col >= this.export.DocumentOptions.MaxColumnCount) && !this.nextObjectCoord.Equals(maxCoord));
                    }
                    if (this.styleRange.Count <= 0)
                    {
                        this.nextFormattingCoord = maxCoord;
                    }
                    else
                    {
                        this.nextFormatting = this.styleRange.First<KeyValuePair<Coords, FormattingSpan>>();
                        this.nextFormattingCoord = this.nextFormatting.Key;
                    }
                    this.nextCellCoord = (this.nextObjectCoord < this.nextFormattingCoord) ? this.nextObjectCoord : this.nextFormattingCoord;
                    if (this.nextCellCoord.Equals(maxCoord))
                    {
                        if ((this.currentRow.RowIndex == (this.rowHeights.Count - 1)) || this.TryAdvanceRow((this.rowHeights.Count - 1) - this.currentRow.RowIndex))
                        {
                            this.export.EndRow();
                        }
                        return;
                    }
                }
            }

            private void FillCellsWithStyle(int columnIndex, FormattingSpan formattingSpan)
            {
                int colSpan = formattingSpan.colSpan;
                this.CheckIntersection(columnIndex, formattingSpan, ref colSpan);
                for (int i = 0; i < colSpan; i++)
                {
                    if (this.export.CurrentColumnIndex < this.export.DocumentOptions.MaxColumnCount)
                    {
                        this.export.BeginCell().Formatting = formattingSpan.formatting;
                        this.export.EndCell();
                    }
                }
            }

            private void FillCellWithData(ObjectInfo obj)
            {
                XlCellFormatting formatting = this.GetFormatting(obj);
                if (this.IsDrawingObject(obj) && ((formatting.Border == null) && ((formatting.Fill == null) || (formatting.Fill.PatternType == XlPatternType.None))))
                {
                    this.SetDrawingObject(obj);
                }
                else if (obj.ColIndex < this.export.CurrentColumnIndex)
                {
                    this.FoundIntersectingBricks();
                }
                else
                {
                    IXlCell cell = this.export.BeginCell();
                    this.SetCurrentObjectInfo(obj, cell);
                    cell.Formatting = formatting;
                    this.export.EndCell();
                    if ((obj.ColSpan > 1) || (obj.RowSpan > 1))
                    {
                        XlCellRange objectRange = new XlCellRange(new XlCellPosition(obj.ColIndex, obj.RowIndex), new XlCellPosition((obj.ColIndex + obj.ColSpan) - 1, (obj.RowIndex + obj.RowSpan) - 1));
                        this.SetCellMerge(this.objectIndex, objectRange);
                        if (obj.ColSpan > 1)
                        {
                            this.AddStyleRange(new Coords(obj.ColIndex + 1, obj.RowIndex), new FormattingSpan(formatting, obj.ColSpan - 1, this.objectIndex));
                        }
                        for (int i = 1; i < obj.RowSpan; i++)
                        {
                            this.AddStyleRange(new Coords(obj.ColIndex, obj.RowIndex + i), new FormattingSpan(formatting, obj.ColSpan, this.objectIndex));
                        }
                    }
                }
            }

            internal void FoundIntersectingBricks()
            {
                this.provider.FoundIntersectingBricks();
            }

            private int GetColumnIndexForIntersectionWithNextFormattingSpan(int cellCount)
            {
                if (this.styleRange.Count <= 1)
                {
                    return -1;
                }
                Coords key = this.styleRange.ElementAt<KeyValuePair<Coords, FormattingSpan>>(1).Key;
                return ((((key.Row != this.export.CurrentRowIndex) || (key.Col >= (this.export.CurrentColumnIndex + cellCount))) || (this.styleRange.ElementAt<KeyValuePair<Coords, FormattingSpan>>(0).Value.objectIndex >= this.styleRange.ElementAt<KeyValuePair<Coords, FormattingSpan>>(1).Value.objectIndex)) ? -1 : key.Col);
            }

            private int GetColumnIndexForIntersectionWithNextObject(int cellCount) => 
                ((this.objectIndex >= this.mtObjects.Length) || ((this.mtObjects[this.objectIndex].RowIndex != this.export.CurrentRowIndex) || (this.mtObjects[this.objectIndex].ColIndex >= (this.export.CurrentColumnIndex + cellCount)))) ? -1 : this.mtObjects[this.objectIndex].ColIndex;

            internal XlCellFormatting GetFormatting(ObjectInfo obj) => 
                this.provider.GetFormatting(obj);

            internal bool IsDrawingObject(ObjectInfo objectInfo) => 
                this.provider.IsDrawingObject(objectInfo);

            internal void RemoveMerge(int objectIndex)
            {
                this.provider.RemoveMerge(objectIndex);
            }

            internal void SetCellMerge(int objectIndex, XlCellRange objectRange)
            {
                this.provider.SetCellMerge(objectIndex, objectRange);
            }

            internal void SetCurrentObjectInfo(ObjectInfo obj, IXlCell cell)
            {
                this.provider.SetObjectInfo(obj, cell);
            }

            internal void SetDrawingObject(ObjectInfo obj)
            {
                this.provider.SetDrawingObject(obj);
            }

            private bool TryAdvanceRow(int rowCount)
            {
                int num = 0;
                int num2 = rowCount;
                while (num++ < num2)
                {
                    this.export.EndRow();
                    if (this.export.CurrentRowIndex == this.export.DocumentOptions.MaxRowCount)
                    {
                        string data = "The maximum number of rows supported for the current document format is exceeded. The exceeding rows will not be exported.";
                        Tracer.TraceWarning("DXperience.Reporting", data);
                        return false;
                    }
                    this.currentRow = this.export.BeginRow();
                    this.currentRow.HeightInPixels = this.rowHeights[this.currentRow.RowIndex];
                }
                return true;
            }

            private bool TryMoveToFillingCell()
            {
                int rowCount = this.nextCellCoord.Row - this.currentRow.RowIndex;
                if ((rowCount > 0) && !this.TryAdvanceRow(rowCount))
                {
                    return false;
                }
                int count = this.nextCellCoord.Col - this.export.CurrentColumnIndex;
                if (count > 0)
                {
                    this.currentRow.SkipCells(count);
                }
                return true;
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct Coords : IComparable<ExcelExportProvider.CellFiller.Coords>
            {
                public readonly int Col;
                public readonly int Row;
                public Coords(int col, int row)
                {
                    this.Col = col;
                    this.Row = row;
                }

                public int CompareTo(ExcelExportProvider.CellFiller.Coords y) => 
                    (this.Row != y.Row) ? (this.Row - y.Row) : (this.Col - y.Col);

                public static bool operator <(ExcelExportProvider.CellFiller.Coords x, ExcelExportProvider.CellFiller.Coords y) => 
                    (x.Row == y.Row) ? (x.Col < y.Col) : (x.Row < y.Row);

                public static bool operator >(ExcelExportProvider.CellFiller.Coords x, ExcelExportProvider.CellFiller.Coords y) => 
                    (x.Row == y.Row) ? (x.Col > y.Col) : (x.Row > y.Row);

                public static bool Equals(ExcelExportProvider.CellFiller.Coords x, ExcelExportProvider.CellFiller.Coords y) => 
                    (x.Row == y.Row) && (x.Col == y.Col);
            }

            [StructLayout(LayoutKind.Sequential)]
            private struct FormattingSpan
            {
                public XlCellFormatting formatting;
                public int colSpan;
                public int objectIndex;
                public FormattingSpan(XlCellFormatting formatting, int colSpan, int objectIndex)
                {
                    this.formatting = formatting;
                    this.colSpan = colSpan;
                    this.objectIndex = objectIndex;
                }
            }
        }

        private static class XlCellFormattingCreator
        {
            private static XlBorder CreateBorder(BrickStyle style)
            {
                XlBorder border = new XlBorder();
                Color borderColor = FormattedTextExportHelper.GetBorderColor(style.BorderColor, style.BackColor);
                XlBorderLineStyle style2 = ExcelHelper.ConvertBorderStyle(style.BorderWidth, style.BorderDashStyle);
                if ((BorderSide.Left & style.Sides) != BorderSide.None)
                {
                    border.LeftColor = borderColor;
                    border.LeftLineStyle = style2;
                }
                if ((BorderSide.Right & style.Sides) != BorderSide.None)
                {
                    border.RightColor = borderColor;
                    border.RightLineStyle = style2;
                }
                if ((BorderSide.Top & style.Sides) != BorderSide.None)
                {
                    border.TopColor = borderColor;
                    border.TopLineStyle = style2;
                }
                if ((BorderSide.Bottom & style.Sides) != BorderSide.None)
                {
                    border.BottomColor = borderColor;
                    border.BottomLineStyle = style2;
                }
                return border;
            }

            private static XlFont CreateFont(Font styleFont)
            {
                XlFont font = XlFont.CustomFont(styleFont.Name, Math.Truncate((double) Math.Max(styleFont.SizeInPoints, 1f)));
                font.Bold = styleFont.Bold;
                font.Italic = styleFont.Italic;
                font.StrikeThrough = styleFont.Strikeout;
                font.Underline = styleFont.Underline ? XlUnderlineType.Single : XlUnderlineType.None;
                return font;
            }

            public static XlCellFormatting CreateFormatting(BrickStyle style, ITableCell tableCell, bool rawDataMode, TextExportMode textExportMode, float angle)
            {
                XlCellFormatting formatting = new XlCellFormatting();
                if (style != null)
                {
                    formatting.Font = CreateFont(style.Font);
                    XlCellAlignment alignment1 = new XlCellAlignment();
                    XlCellAlignment alignment2 = new XlCellAlignment();
                    alignment2.HorizontalAlignment = ToHorisontalAlignment(style.StringFormat.RightToLeft ? GraphicsConvertHelper.RTLStringAlignment(style.StringFormat.Alignment) : style.StringFormat.Alignment);
                    XlCellAlignment local1 = alignment2;
                    local1.VerticalAlignment = ToVerticalAlignment(style.StringFormat.LineAlignment);
                    local1.ReadingOrder = style.StringFormat.RightToLeft ? XlReadingOrder.RightToLeft : XlReadingOrder.LeftToRight;
                    XlCellAlignment local2 = local1;
                    local2.WrapText = style.StringFormat.WordWrap;
                    local2.ShrinkToFit = style.StringFormat.WordWrap;
                    formatting.Alignment = local2;
                    formatting.Alignment.Indent = GetIndent(formatting.Alignment.HorizontalAlignment, style.Padding);
                    if (!rawDataMode)
                    {
                        SetColorDetails(style, ref formatting);
                    }
                    if (angle != 0f)
                    {
                        formatting.Alignment.TextRotation = (angle == 90f) ? 90 : 180;
                    }
                }
                if (textExportMode != TextExportMode.Value)
                {
                    formatting.NumberFormat = XlNumberFormat.Text;
                }
                else
                {
                    formatting.IsDateTimeFormatString = (tableCell.TextValue is DateTime) || (tableCell.TextValue is TimeSpan);
                    formatting.NumberFormat = GetNumberFormat(tableCell.XlsxFormatString, tableCell.FormatString, formatting.IsDateTimeFormatString, tableCell.TextValue);
                }
                return formatting;
            }

            private static float GetBrickPaddingInPixels(XlHorizontalAlignment horizontalAlignment, PaddingInfo padding) => 
                (horizontalAlignment == XlHorizontalAlignment.Left) ? GraphicsUnitConverter.Convert((float) padding.Left, padding.Dpi, (float) 96f) : ((horizontalAlignment == XlHorizontalAlignment.Right) ? GraphicsUnitConverter.Convert((float) padding.Right, padding.Dpi, (float) 96f) : 0f);

            internal static byte GetIndent(XlHorizontalAlignment horizontalAlignment, PaddingInfo padding) => 
                Convert.ToByte(Math.Truncate((double) (((GetBrickPaddingInPixels(horizontalAlignment, padding) - 2f) + 2f) / 9f)));

            private static XlNumberFormat GetNumberFormat(string xlsxFormatString, string formatString, bool isDateTimeFormat, object value)
            {
                XlNumberFormat general;
                try
                {
                    if (string.IsNullOrEmpty(xlsxFormatString) || !XlNumberFormatHelper.IsValidXlsxFormatString(xlsxFormatString))
                    {
                        if (!string.IsNullOrEmpty(formatString))
                        {
                            CultureInfo currentCulture = Application.CurrentCulture;
                            ExcelNumberFormat format2 = new XlExportNumberFormatConverter().Convert(formatString, isDateTimeFormat, currentCulture);
                            if (format2 != null)
                            {
                                return format2.FormatString;
                            }
                        }
                        general = !(value is DateTime) ? (!(value is TimeSpan) ? (!(value is string) ? XlNumberFormat.General : ((((string) value).Length <= 0x100) ? XlNumberFormat.Text : XlNumberFormat.General)) : XlNumberFormat.Span) : XlNumberFormat.ShortDateTime;
                    }
                    else
                    {
                        general = xlsxFormatString;
                    }
                }
                catch
                {
                    general = XlNumberFormat.General;
                }
                return general;
            }

            private static void SetColorDetails(BrickStyle style, ref XlCellFormatting formatting)
            {
                formatting.Font.Color = XlColor.FromArgb(DXColor.Blend(style.ForeColor, Color.White).ToArgb());
                if (!style.IsTransparent || (style.BackColor.A > 0))
                {
                    XlFill fill1 = new XlFill();
                    fill1.ForeColor = DXColor.Blend(style.BackColor, Color.White);
                    fill1.PatternType = (style.BackColor == Color.Transparent) ? XlPatternType.None : XlPatternType.Solid;
                    formatting.Fill = fill1;
                }
                if ((style.Sides != BorderSide.None) && (style.BorderWidth > 0f))
                {
                    formatting.Border = CreateBorder(style);
                }
            }

            private static XlHorizontalAlignment ToHorisontalAlignment(StringAlignment stringAlignment) => 
                (stringAlignment == StringAlignment.Center) ? XlHorizontalAlignment.Center : ((stringAlignment != StringAlignment.Far) ? XlHorizontalAlignment.Left : XlHorizontalAlignment.Right);

            private static XlVerticalAlignment ToVerticalAlignment(StringAlignment stringAlignment) => 
                (stringAlignment == StringAlignment.Center) ? XlVerticalAlignment.Center : ((stringAlignment != StringAlignment.Far) ? XlVerticalAlignment.Top : XlVerticalAlignment.Bottom);
        }
    }
}

