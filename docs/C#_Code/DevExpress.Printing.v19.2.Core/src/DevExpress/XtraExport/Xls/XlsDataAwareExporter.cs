namespace DevExpress.XtraExport.Xls
{
    using DevExpress.Export.Binary;
    using DevExpress.Export.Xl;
    using DevExpress.Office.Crypto;
    using DevExpress.Office.Utils;
    using DevExpress.Utils;
    using DevExpress.Utils.Crypt;
    using DevExpress.Utils.StructuredStorage.Internal.Writer;
    using DevExpress.Utils.StructuredStorage.Writer;
    using DevExpress.XtraExport;
    using DevExpress.XtraExport.Implementation;
    using DevExpress.XtraExport.OfficeArt;
    using DevExpress.XtraExport.Xlsx;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.Globalization;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Security.Cryptography;
    using System.Text;

    public class XlsDataAwareExporter : IXlExportEx, IXlExport, IXlFormulaEngine, IXlExporter, IXlCellFormatter
    {
        private int cfCount;
        private readonly XlCondFmtExpressionFactory expressionFactory;
        private readonly Dictionary<XlCellRange, int> condFmtExtRanges;
        private int shapeId;
        private readonly List<XlsPictureData> drawingGroup;
        private XlPicture currentPicture;
        private const string defaultTableStyleName = "TableStyleMedium2";
        private const string defaultPivotTableStyleName = "PivotStyleLight16";
        private const string defaultTotalRowLabel = "Total";
        private int tableId;
        private readonly List<XlCell> cellsToExport;
        private XlCellFormatting inheritedFormatting;
        private XlCell currentCell;
        private XlFormulaConverter formulaConverter;
        private XlPtgDataType paramType;
        private XlsTableColumn currentColumn;
        private readonly Dictionary<XlCellRange, int> dataValidationListRanges;
        private const string addInUDFDefinition = "*AddInUDF";
        private const int rowsInBlock = 0x20;
        private readonly Stack<XlGroup> groups;
        private XlGroup currentGroup;
        private int rowCount;
        private XlsTableRow currentRow;
        private readonly List<XlsTableRow> rowsToExport;
        private readonly XlsDbCellCalculator dbCellCalculator;
        private XlsPalette palette;
        private readonly Dictionary<XlFont, int> fontTable;
        private readonly List<XlFont> fontList;
        private readonly List<XlsXf> xfList;
        private readonly Dictionary<XlsXf, int> xfTable;
        private readonly List<XlsContentXFExt> xfExtensions;
        private readonly List<ExcelNumberFormat> predefinedNumberFormats;
        private readonly Dictionary<string, ExcelNumberFormat> numberFormatTable;
        private readonly Dictionary<XlNetNumberFormat, ExcelNumberFormat> netNumberFormatTable;
        private readonly XlExportNumberFormatConverter numberFormatConverter;
        private int customNumberFormatId;
        private XlFont defaultFont;
        private XlCellAlignment defaultAlignment;
        private XlBorder defaultBorder;
        private XlFill defaultFill;
        private int xfCount;
        private int xfCrc;
        private readonly Queue<long> boundPositions;
        private readonly SimpleSharedStringTable sharedStrings;
        private readonly List<XlsSSTInfo> extendedSSTItems;
        private int sharedStringsRefCount;
        private int stringsInBucket;
        private readonly Dictionary<string, int> sheetDefinitions;
        private readonly List<XlsContentDefinedName> definedNames;
        private XlDocument currentDocument;
        private XlDocumentProperties documentProperties;
        private XlCalculationOptions calculationOptions;
        private readonly Dictionary<string, int> externalNames;
        private readonly Dictionary<string, int> futureFunctions;
        private readonly List<XlsTableBasedDocumentSheet> sheets;
        private XlsTableBasedDocumentSheet currentSheet;
        private int currentSheetIndex;
        private int maxRowOutlineLevel;
        private long indexRecordPosition;
        private long defColWidthRecordPosition;
        private long cellTablePosition;
        private bool rowContentStarted;
        private int currentRowIndex;
        private int currentColumnIndex;
        private BinaryWriter writer;
        private Stream outputStream;
        private CompositeMemoryStream workbookStream;
        private BinaryWriter workbookWriter;
        private BinaryWriter summaryInformationWriter;
        private BinaryWriter docSummaryInformationWriter;
        private readonly XlsContentEmpty contentEmpty;
        private readonly XlsContentBoolValue contentBoolValue;
        private readonly XlsContentShortValue contentShortValue;
        private readonly XlsContentDoubleValue contentDoubleValue;
        private readonly XlsContentStringValue contentStringValue;
        private readonly XlsContentBeginOfSubstream contentBOF;
        private readonly XlsDataAwareExporterOptions options;
        private readonly IXlFormulaParser formulaParser;
        private readonly XlExpressionContext expressionContext;
        private readonly HashSet<XlCellPosition> sharedFormulaHostCells;
        private XlsRC4EncryptionHeader encryptionHeader;
        private ARC4KeyGen keygen;
        private ARC4Cipher cipher;
        private XlCellFormatter cellFormatter;

        public XlsDataAwareExporter() : this(null)
        {
        }

        public XlsDataAwareExporter(IXlFormulaParser formulaParser)
        {
            this.expressionFactory = new XlCondFmtExpressionFactory();
            this.condFmtExtRanges = new Dictionary<XlCellRange, int>();
            this.drawingGroup = new List<XlsPictureData>();
            this.cellsToExport = new List<XlCell>();
            this.paramType = XlPtgDataType.Value;
            this.dataValidationListRanges = new Dictionary<XlCellRange, int>();
            this.groups = new Stack<XlGroup>();
            this.rowsToExport = new List<XlsTableRow>();
            this.dbCellCalculator = new XlsDbCellCalculator();
            this.fontTable = new Dictionary<XlFont, int>();
            this.fontList = new List<XlFont>();
            this.xfList = new List<XlsXf>();
            this.xfTable = new Dictionary<XlsXf, int>();
            this.xfExtensions = new List<XlsContentXFExt>();
            this.predefinedNumberFormats = new List<ExcelNumberFormat>();
            this.numberFormatTable = new Dictionary<string, ExcelNumberFormat>();
            this.netNumberFormatTable = new Dictionary<XlNetNumberFormat, ExcelNumberFormat>();
            this.numberFormatConverter = new XlExportNumberFormatConverter();
            this.boundPositions = new Queue<long>();
            this.sharedStrings = new SimpleSharedStringTable();
            this.extendedSSTItems = new List<XlsSSTInfo>();
            this.sheetDefinitions = new Dictionary<string, int>();
            this.definedNames = new List<XlsContentDefinedName>();
            this.externalNames = new Dictionary<string, int>();
            this.futureFunctions = new Dictionary<string, int>();
            this.sheets = new List<XlsTableBasedDocumentSheet>();
            this.maxRowOutlineLevel = 1;
            this.contentEmpty = new XlsContentEmpty();
            this.contentBoolValue = new XlsContentBoolValue();
            this.contentShortValue = new XlsContentShortValue();
            this.contentDoubleValue = new XlsContentDoubleValue();
            this.contentStringValue = new XlsContentStringValue();
            this.contentBOF = new XlsContentBeginOfSubstream();
            this.options = new XlsDataAwareExporterOptions();
            this.expressionContext = new XlExpressionContext();
            this.sharedFormulaHostCells = new HashSet<XlCellPosition>();
            this.formulaParser = formulaParser;
            this.expressionContext.MaxColumnCount = 0x100;
            this.expressionContext.MaxRowCount = 0x10000;
        }

        private void AddPredefined(int id, string formatCode)
        {
            this.predefinedNumberFormats.Add(new ExcelNumberFormat(id, formatCode));
        }

        private void AddRangeReference(XlExpression formula, IXlSheet sheet, XlCellRange range)
        {
            if (range != null)
            {
                XlPtgArea3d item = new XlPtgArea3d(range, sheet.Name);
                formula.Add(item);
            }
        }

        private int AddToDrawingGroup(XlPicture picture)
        {
            byte[] imageBytes;
            XlsPictureData data;
            if ((picture == null) || (picture.Image == null))
            {
                return -1;
            }
            ImageFormat png = this.ReplaceImageFormat(picture.Format);
            try
            {
                imageBytes = picture.GetImageBytes(png);
            }
            catch
            {
                png = ImageFormat.Png;
                imageBytes = picture.GetImageBytes(png);
            }
            MD4HashCalculator calculator = new MD4HashCalculator();
            uint[] initialCheckSumValue = calculator.InitialCheckSumValue;
            calculator.UpdateCheckSum(initialCheckSumValue, imageBytes, 0, imageBytes.Length);
            byte[] digest = MD4HashConverter.ToArray(calculator.GetFinalCheckSum(initialCheckSumValue));
            for (int i = 0; i < this.drawingGroup.Count; i++)
            {
                data = this.drawingGroup[i];
                if (data.EqualsDigest(digest))
                {
                    data.NumberOfReferences++;
                    return (i + 1);
                }
            }
            data = new XlsPictureData(this.GetImageFormat(png), picture.Image.Width, picture.Image.Height, imageBytes, digest);
            this.drawingGroup.Add(data);
            return this.drawingGroup.Count;
        }

        private void AssignTableIds()
        {
            IList<XlTable> innerTables = this.currentSheet.InnerTables;
            for (int i = 0; i < innerTables.Count; i++)
            {
                XlTable table = innerTables[i];
                this.tableId++;
                table.Id = this.tableId;
            }
        }

        public IXlCell BeginCell()
        {
            IXlColumn column;
            this.currentCell = new XlCell();
            this.currentCell.RowIndex = this.CurrentRowIndex;
            this.currentCell.ColumnIndex = this.currentColumnIndex;
            this.inheritedFormatting = !this.currentSheet.ColumnsTable.TryGetValue(this.currentColumnIndex, out column) ? XlFormatting.CopyObject<XlCellFormatting>(this.currentRow.Formatting) : XlCellFormatting.Merge(XlFormatting.CopyObject<XlCellFormatting>(column.Formatting), this.currentRow.Formatting);
            this.currentCell.Formatting = XlFormatting.CopyObject<XlCellFormatting>(this.inheritedFormatting);
            XlExpression calculatedColumnExpression = this.currentSheet.GetCalculatedColumnExpression(this.currentCell.ColumnIndex, this.currentCell.RowIndex);
            if (calculatedColumnExpression != null)
            {
                this.currentCell.SetFormula(calculatedColumnExpression);
            }
            return this.currentCell;
        }

        public IXlColumn BeginColumn()
        {
            if (this.rowContentStarted)
            {
                throw new InvalidOperationException("Columns have to be created before rows and cells.");
            }
            this.currentColumn = this.currentSheet.CreateXlsColumn();
            return this.currentColumn;
        }

        protected IXlDocument BeginDocument()
        {
            this.sheetDefinitions.Clear();
            this.externalNames.Clear();
            this.futureFunctions.Clear();
            this.definedNames.Clear();
            this.dataValidationListRanges.Clear();
            this.condFmtExtRanges.Clear();
            this.drawingGroup.Clear();
            this.documentProperties = new XlDocumentProperties();
            this.documentProperties.Created = DateTimeHelper.Now;
            this.currentDocument = new XlDocument(this);
            this.calculationOptions = new XlCalculationOptions();
            this.tableId = 0;
            return this.currentDocument;
        }

        public IXlDocument BeginExport(Stream stream)
        {
            this.ClearEncryptionHeader();
            return this.BeginExportCore(stream);
        }

        public IXlDocument BeginExport(Stream stream, EncryptionOptions encryptionOptions)
        {
            this.CreateEncryptionHeader(encryptionOptions);
            return this.BeginExportCore(stream);
        }

        private IXlDocument BeginExportCore(Stream stream)
        {
            this.InitializeStyles();
            this.InitializeSharedStrings();
            this.outputStream = stream;
            this.workbookStream = new CompositeMemoryStream();
            this.workbookWriter = new BinaryWriter(this.workbookStream);
            ChunkedMemoryStream output = new ChunkedMemoryStream();
            this.summaryInformationWriter = new BinaryWriter(output);
            ChunkedMemoryStream stream3 = new ChunkedMemoryStream();
            this.docSummaryInformationWriter = new BinaryWriter(stream3);
            this.cellFormatter = new XlCellFormatter(this);
            return this.BeginDocument();
        }

        public IXlGroup BeginGroup()
        {
            XlGroup item = new XlGroup();
            if (this.currentGroup == null)
            {
                item.OutlineLevel = 1;
            }
            else
            {
                item.OutlineLevel = this.currentGroup.OutlineLevel;
                item.IsCollapsed = this.currentGroup.IsCollapsed;
            }
            this.groups.Push(item);
            this.currentGroup = item;
            return item;
        }

        public IXlPicture BeginPicture()
        {
            this.currentPicture = new XlPicture(this);
            return this.currentPicture;
        }

        public IXlRow BeginRow()
        {
            this.rowContentStarted = true;
            this.currentColumnIndex = 0;
            this.currentRow = new XlsTableRow(this);
            this.currentRow.RowIndex = this.currentRowIndex;
            this.currentSheet.ExtendTableRanges(this.currentRowIndex);
            return this.currentRow;
        }

        public IXlSheet BeginSheet()
        {
            this.rowCount = 0;
            this.currentRowIndex = 0;
            this.currentColumnIndex = 0;
            this.dbCellCalculator.Reset();
            this.sharedFormulaHostCells.Clear();
            this.shapeId = 0;
            this.currentSheet = new XlsTableBasedDocumentSheet(this);
            this.currentSheet.Name = $"Sheet{this.sheets.Count + 1}";
            this.rowContentStarted = false;
            return this.currentSheet;
        }

        private void CalcStringsInBucket(int count)
        {
            this.stringsInBucket = (count / 0x80) + 1;
            if (this.stringsInBucket < 8)
            {
                this.stringsInBucket = 8;
            }
        }

        private XlAnchorPoint CalculateAnchorPoint(XlAnchorPoint source)
        {
            int column = source.Column;
            int row = source.Row;
            int columnOffsetInPixels = source.ColumnOffsetInPixels;
            int rowOffsetInPixels = source.RowOffsetInPixels;
            int cellWidth = this.GetCellWidth(column);
            int cellHeight = this.GetCellHeight(row);
            int num7 = this.options.MaxColumnCount - 1;
            int num8 = this.options.MaxRowCount - 1;
            while ((columnOffsetInPixels < 0) && (column > 0))
            {
                column--;
                cellWidth = this.GetCellWidth(column);
                columnOffsetInPixels += cellWidth;
            }
            while ((columnOffsetInPixels >= cellWidth) && (column < num7))
            {
                columnOffsetInPixels -= cellWidth;
                column++;
                cellWidth = this.GetCellWidth(column);
            }
            while ((rowOffsetInPixels < 0) && (row > 0))
            {
                row--;
                cellHeight = this.GetCellHeight(row);
                rowOffsetInPixels += cellHeight;
            }
            while ((rowOffsetInPixels >= cellHeight) && (row < num8))
            {
                rowOffsetInPixels -= cellHeight;
                row++;
                cellHeight = this.GetCellHeight(row);
            }
            return new XlAnchorPoint(column, row, columnOffsetInPixels, rowOffsetInPixels, cellWidth, cellHeight);
        }

        private void CalculateAutomaticHeight(XlCellFormatting formatting)
        {
            if (!this.currentRow.IsHidden && ((this.currentRow.HeightInPixels < 0) && ((formatting != null) && (formatting.Font != null))))
            {
                XlFont font = formatting.Font;
                FontStyle regular = FontStyle.Regular;
                if (font.Bold)
                {
                    regular |= FontStyle.Bold;
                }
                if (font.Italic)
                {
                    regular |= FontStyle.Italic;
                }
                using (Font font2 = new Font(font.Name, (float) font.Size, regular, GraphicsUnit.Point))
                {
                    int num = ((int) (((double) (FontMetrics.CreateInstance(font2, GraphicsUnit.Point).CalculateHeight(1) * this.DpiY)) / 72.0)) + 3;
                    this.currentRow.AutomaticHeightInPixels = Math.Max(this.currentRow.AutomaticHeightInPixels, num);
                }
            }
        }

        private void CalculateDrawingAnchorPoints()
        {
            foreach (XlDrawingObjectBase base2 in this.currentSheet.DrawingObjects)
            {
                base2.TopLeft = this.CalculateAnchorPoint(base2.TopLeft);
                base2.BottomRight = this.CalculateAnchorPoint(base2.BottomRight);
            }
        }

        private string CheckLength(string value, int maxLength) => 
            (string.IsNullOrEmpty(value) || (value.Length <= maxLength)) ? value : value.Substring(0, maxLength);

        private void ClearEncryptionHeader()
        {
            this.encryptionHeader = null;
            this.keygen = null;
            this.cipher = null;
        }

        private void ClearSheets()
        {
            foreach (XlsTableBasedDocumentSheet sheet in this.sheets)
            {
                sheet.Dispose();
            }
            this.sheets.Clear();
        }

        private void CloseTables()
        {
            foreach (XlTable table in this.currentSheet.InnerTables)
            {
                table.IsClosed = true;
            }
        }

        private int CompareColumns(XlsTableColumn x, XlsTableColumn y) => 
            (x != null) ? ((y != null) ? ((x.ColumnIndex >= y.ColumnIndex) ? ((x.ColumnIndex <= y.ColumnIndex) ? 0 : 1) : -1) : 1) : ((y == null) ? 0 : -1);

        private void ConvertCondFmtArea3D(XlExpression expression)
        {
            int count = expression.Count;
            for (int i = 0; i < count; i++)
            {
                if (expression[i].TypeCode == 0x1b)
                {
                    XlPtgArea3d aread = (XlPtgArea3d) expression[i];
                    XlCellRange cellRange = aread.CellRange;
                    cellRange.SheetName = aread.SheetName;
                    expression[i] = new XlPtgName(this.RegisterCondFmtExtRange(cellRange), XlPtgDataType.Reference);
                }
            }
        }

        private ExcelNumberFormat ConvertNetFormatStringToXlFormatCode(string netFormatString, bool isDateTimeFormatString)
        {
            CultureInfo culture = this.options.Culture;
            culture ??= CultureInfo.InvariantCulture;
            return this.numberFormatConverter.Convert(netFormatString, isDateTimeFormatString, culture);
        }

        private OfficeArtBlipStoreContainer CreateBlipStore()
        {
            OfficeArtBlipStoreContainer container = new OfficeArtBlipStoreContainer();
            foreach (XlsPictureData data in this.drawingGroup)
            {
                OfficeArtBlipImage blip = new OfficeArtBlipImage(data);
                container.Items.Add(new OfficeArtBlipStoreFileBlock(blip));
            }
            return container;
        }

        private XlsContentCondFmt CreateCFContentRoot(bool isCondFmt12) => 
            !isCondFmt12 ? new XlsContentCondFmt() : new XlsContentCondFmt12();

        public IXlDocument CreateDocument(Stream stream) => 
            this.BeginExport(stream);

        public IXlDocument CreateDocument(Stream stream, EncryptionOptions encryptionOptions) => 
            this.BeginExport(stream, encryptionOptions);

        private XlsPictureObject CreateDrawingObject(int blipId, XlPicture picture)
        {
            XlsPictureObject obj2 = new XlsPictureObject();
            int num = this.shapeId + 1;
            this.shapeId = num;
            obj2.PictureId = num;
            obj2.BlipId = blipId;
            obj2.Name = string.IsNullOrEmpty(picture.Name) ? $"Picture{obj2.PictureId}" : picture.Name;
            if (picture.AnchorType == XlAnchorType.Absolute)
            {
                obj2.AnchorType = XlAnchorType.TwoCell;
                obj2.AnchorBehavior = XlAnchorType.Absolute;
            }
            else if (picture.AnchorType == XlAnchorType.OneCell)
            {
                obj2.AnchorType = XlAnchorType.TwoCell;
                obj2.AnchorBehavior = XlAnchorType.OneCell;
            }
            else
            {
                obj2.AnchorType = picture.AnchorType;
                obj2.AnchorBehavior = picture.AnchorBehavior;
            }
            obj2.TopLeft = picture.TopLeft;
            obj2.BottomRight = picture.BottomRight;
            obj2.HyperlinkClick = picture.HyperlinkClick.Clone();
            obj2.SourceRectangle = picture.SourceRectangle;
            return obj2;
        }

        private void CreateEncryptionHeader(EncryptionOptions encryptionOptions)
        {
            this.ClearEncryptionHeader();
            if (encryptionOptions != null)
            {
                string str;
                this.encryptionHeader = new XlsRC4EncryptionHeader();
                if (string.IsNullOrEmpty(encryptionOptions.Password))
                {
                    str = "VelvetSweatshop";
                }
                byte[] data = new byte[0x10];
                byte[] buffer2 = new byte[0x10];
                using (RandomNumberGenerator generator = RandomNumberGenerator.Create())
                {
                    generator.GetBytes(data);
                    generator.GetBytes(buffer2);
                }
                byte[] input = MD5Hash.ComputeHash(buffer2);
                this.keygen = new ARC4KeyGen(str, data);
                this.cipher = new ARC4Cipher(this.keygen.DeriveKey(0));
                this.encryptionHeader.Salt = data;
                this.encryptionHeader.EncryptedVerifier = this.cipher.Encrypt(buffer2);
                this.encryptionHeader.EncryptedVerifierHash = this.cipher.Encrypt(input);
            }
        }

        private OfficeArtFileDrawingGroupRecord CreateFileDrawingBlock()
        {
            OfficeArtFileDrawingGroupRecord record = new OfficeArtFileDrawingGroupRecord();
            int num = 0;
            int num2 = 0;
            int num3 = 0;
            for (int i = 0; i < this.sheets.Count; i++)
            {
                XlsTableBasedDocumentSheet sheet = this.sheets[i];
                int count = sheet.DrawingObjects.Count;
                if (count > 0)
                {
                    num2 += count + 1;
                    num3++;
                }
                int num6 = count + 1;
                num = Math.Max(num, this.GetTopmostShapeId(sheet.SheetId) + num6);
                OfficeArtFileIdCluster item = new OfficeArtFileIdCluster {
                    ClusterId = sheet.SheetId,
                    LargestShapeId = num6
                };
                record.Clusters.Add(item);
            }
            record.MaxShapeId = num;
            record.TotalShapes = num2;
            record.TotalDrawings = num3;
            return record;
        }

        private OfficeArtFileDrawingRecord CreateFileDrawingRecord()
        {
            int count = this.currentSheet.DrawingObjects.Count;
            return new OfficeArtFileDrawingRecord(this.currentSheet.SheetId, count + 1, this.GetTopmostShapeId(this.currentSheet.SheetId) + count);
        }

        private OfficeArtDrawingContainer CreateMsoDrawing() => 
            new OfficeArtDrawingContainer { Items = { 
                this.CreateFileDrawingRecord(),
                this.CreateShapeGroup()
            } };

        private OfficeArtDrawingGroupContainer CreateMsoDrawingGroup()
        {
            OfficeArtDrawingGroupContainer container = new OfficeArtDrawingGroupContainer {
                Items = { this.CreateFileDrawingBlock() }
            };
            if (this.drawingGroup.Count > 0)
            {
                container.Items.Add(this.CreateBlipStore());
            }
            return container;
        }

        private XlPtgBase CreatePtg(XlCellRange range) => 
            this.CreatePtg(range, XlPtgDataType.Reference);

        private XlPtgBase CreatePtg(XlVariantValue value) => 
            !value.IsBoolean ? (!value.IsNumeric ? (!value.IsText ? (!value.IsError ? ((XlPtgBase) new XlPtgMissArg()) : ((value.ErrorValue.Type != XlCellErrorType.Reference) ? ((XlPtgBase) new XlPtgErr(value.ErrorValue.Type)) : ((XlPtgBase) new XlPtgRefErr(this.paramType)))) : ((XlPtgBase) new XlPtgStr(value.TextValue))) : ((XlPtgBase) new XlPtgNum(value.NumericValue))) : ((XlPtgBase) new XlPtgBool(value.BooleanValue));

        private XlPtgBase CreatePtg(XlCellRange range, XlPtgDataType dataType)
        {
            if (string.IsNullOrEmpty(range.SheetName))
            {
                if (range.TopLeft.Equals(range.BottomRight))
                {
                    XlPtgRef ref1 = new XlPtgRef(range.TopLeft);
                    ref1.DataType = dataType;
                    return ref1;
                }
                XlPtgArea area1 = new XlPtgArea(range);
                area1.DataType = dataType;
                return area1;
            }
            if (range.TopLeft.Equals(range.BottomRight))
            {
                XlPtgRef3d refd1 = new XlPtgRef3d(range.TopLeft, range.SheetName);
                refd1.DataType = dataType;
                return refd1;
            }
            XlPtgArea3d aread1 = new XlPtgArea3d(range, range.SheetName);
            aread1.DataType = dataType;
            return aread1;
        }

        private OfficeArtShapeContainer CreateShape(XlShape shape)
        {
            OfficeArtShapeContainer container = new OfficeArtShapeContainer();
            int flags = 0xb00;
            if (shape.Frame.FlipVertical)
            {
                flags |= 0x80;
            }
            if (shape.Frame.FlipHorizontal)
            {
                flags |= 0x40;
            }
            container.Items.Add(new OfficeArtShapeRecord(this.GetShapeTypeCode(shape), this.GetTopmostShapeId(this.currentSheet.SheetId) + shape.Id, flags));
            container.Items.Add(this.CreateShapeProperties(shape));
            OfficeArtTertiaryProperties item = this.CreateTertiaryProperties(shape);
            if (item != null)
            {
                container.Items.Add(item);
            }
            container.Items.Add(new OfficeArtClientAnchorSheet(shape));
            container.Items.Add(new OfficeArtShapeClientData(shape));
            return container;
        }

        private OfficeArtShapeContainer CreateShape(XlsPictureObject pictureObject)
        {
            OfficeArtShapeContainer container = new OfficeArtShapeContainer {
                Items = { 
                    new OfficeArtShapeRecord(0x4b, this.GetTopmostShapeId(this.currentSheet.SheetId) + pictureObject.PictureId, 0xa00),
                    this.CreateShapeProperties(pictureObject)
                }
            };
            OfficeArtTertiaryProperties item = this.CreateTertiaryProperties(pictureObject);
            if (item != null)
            {
                container.Items.Add(item);
            }
            container.Items.Add(new OfficeArtClientAnchorSheet(pictureObject));
            container.Items.Add(new OfficeArtPictureClientData(pictureObject));
            return container;
        }

        private OfficeArtShapeGroupContainer CreateShapeGroup()
        {
            OfficeArtShapeGroupContainer container = new OfficeArtShapeGroupContainer {
                Items = { this.CreateTopmostShape() }
            };
            foreach (XlDrawingObjectBase base2 in this.currentSheet.DrawingObjects)
            {
                XlsPictureObject pictureObject = base2 as XlsPictureObject;
                if (pictureObject != null)
                {
                    container.Items.Add(this.CreateShape(pictureObject));
                    continue;
                }
                XlShape shape = base2 as XlShape;
                if (shape != null)
                {
                    int num;
                    if (shape.GeometryPreset == XlGeometryPreset.Line)
                    {
                        num = this.shapeId + 1;
                        this.shapeId = num;
                        shape.Id = num;
                        if (string.IsNullOrEmpty(shape.Name))
                        {
                            shape.Name = $"Straight Connector {this.shapeId}";
                        }
                        container.Items.Add(this.CreateShape(shape));
                        continue;
                    }
                    if (shape.GeometryPreset == XlGeometryPreset.Rect)
                    {
                        num = this.shapeId + 1;
                        this.shapeId = num;
                        shape.Id = num;
                        if (string.IsNullOrEmpty(shape.Name))
                        {
                            shape.Name = $"Rectangle {this.shapeId}";
                        }
                        container.Items.Add(this.CreateShape(shape));
                    }
                }
            }
            return container;
        }

        private OfficeArtProperties CreateShapeProperties(XlShape shape)
        {
            Color color;
            int num;
            OfficeArtProperties properties = new OfficeArtProperties();
            if (!shape.IsConnector)
            {
                properties.Properties.Add(new OfficeArtIntProperty(0xbf, 0x1f0018));
            }
            if (shape.IsConnector)
            {
                properties.Properties.Add(new OfficeArtIntProperty(0x17f, 0x10000));
            }
            if (shape.IsConnector || (shape.Fill.FillType == XlDrawingFillType.None))
            {
                properties.Properties.Add(new OfficeArtIntProperty(0x1bf, 0x100000));
            }
            else
            {
                color = shape.Fill.Color.ConvertToRgb(this.currentDocument.Theme);
                num = (color.R | (color.G << 8)) | (color.B << 0x10);
                properties.Properties.Add(new OfficeArtIntProperty(0x181, num));
                if (shape.Fill.Opacity < 1.0)
                {
                    properties.Properties.Add(new OfficeArtIntProperty(0x182, (int) (shape.Fill.Opacity * 65536.0)));
                }
                properties.Properties.Add(new OfficeArtIntProperty(0x1bf, 0x100010));
            }
            if (!shape.Outline.Color.IsEmpty)
            {
                color = shape.Outline.Color.ConvertToRgb(this.currentDocument.Theme);
                num = (color.R | (color.G << 8)) | (color.B << 0x10);
                properties.Properties.Add(new OfficeArtIntProperty(0x1c0, num));
                if (shape.Outline.Opacity < 1.0)
                {
                    properties.Properties.Add(new OfficeArtIntProperty(0x1c1, (int) (shape.Outline.Opacity * 65536.0)));
                }
            }
            properties.Properties.Add(new OfficeArtIntProperty(0x1cb, (int) (shape.Outline.Width * 12700.0)));
            if (shape.Outline.JoinType == XlLineJoinType.Miter)
            {
                properties.Properties.Add(new OfficeArtIntProperty(460, this.GetFixedPoint(shape.Outline.MiterLimit)));
            }
            if (shape.Outline.CompoundType != XlOutlineCompoundType.Single)
            {
                properties.Properties.Add(new OfficeArtIntProperty(0x1cd, (int) shape.Outline.CompoundType));
            }
            properties.Properties.Add(new OfficeArtIntProperty(0x1ce, (int) shape.Outline.Dashing));
            properties.Properties.Add(new OfficeArtIntProperty(470, (int) shape.Outline.JoinType));
            if (shape.Outline.CapType != XlLineCapType.Flat)
            {
                properties.Properties.Add(new OfficeArtIntProperty(0x1d7, (int) shape.Outline.CapType));
            }
            properties.Properties.Add(new OfficeArtIntProperty(0x1ff, this.GetLineStyleBooleanProperties(shape)));
            if (shape.IsConnector)
            {
                properties.Properties.Add(new OfficeArtIntProperty(0x303, 0));
            }
            properties.Properties.Add(new OfficeArtStringProperty(0xc380, shape.Name));
            byte[] hyperlinkData = this.GetHyperlinkData(shape);
            if (hyperlinkData != null)
            {
                properties.Properties.Add(new OfficeArtDataProperty(0xc382, hyperlinkData));
            }
            properties.Properties.Add(new OfficeArtIntProperty(0x3bf, 0x200000));
            return properties;
        }

        private OfficeArtProperties CreateShapeProperties(XlsPictureObject pictureObject)
        {
            OfficeArtProperties properties = new OfficeArtProperties {
                Properties = { new OfficeArtIntProperty(0x7f, 0x1fb0080) }
            };
            if (pictureObject.SourceRectangle.Top != 0.0)
            {
                properties.Properties.Add(new OfficeArtFixedPointProperty(0x100, pictureObject.SourceRectangle.Top));
            }
            if (pictureObject.SourceRectangle.Bottom != 0.0)
            {
                properties.Properties.Add(new OfficeArtFixedPointProperty(0x101, pictureObject.SourceRectangle.Bottom));
            }
            if (pictureObject.SourceRectangle.Left != 0.0)
            {
                properties.Properties.Add(new OfficeArtFixedPointProperty(0x102, pictureObject.SourceRectangle.Left));
            }
            if (pictureObject.SourceRectangle.Right != 0.0)
            {
                properties.Properties.Add(new OfficeArtFixedPointProperty(0x103, pictureObject.SourceRectangle.Right));
            }
            properties.Properties.Add(new OfficeArtIntProperty(0x4104, pictureObject.BlipId));
            properties.Properties.Add(new OfficeArtIntProperty(0x13f, 0x60000));
            properties.Properties.Add(new OfficeArtIntProperty(0x1bf, 0x100000));
            properties.Properties.Add(new OfficeArtIntProperty(0x1ff, 0x180000));
            properties.Properties.Add(new OfficeArtIntProperty(0x33f, 0x180010));
            properties.Properties.Add(new OfficeArtStringProperty(0xc380, pictureObject.Name));
            byte[] hyperlinkData = this.GetHyperlinkData(pictureObject);
            if (hyperlinkData != null)
            {
                properties.Properties.Add(new OfficeArtDataProperty(0xc382, hyperlinkData));
            }
            properties.Properties.Add(new OfficeArtIntProperty(0x3bf, 0x200000));
            return properties;
        }

        private void CreateTableCells(XlCellRange range)
        {
            IEnumerable<int> cellsToCreate = this.currentSheet.GetCellsToCreate(range);
            if (cellsToCreate != null)
            {
                foreach (int num in cellsToCreate)
                {
                    this.currentColumnIndex = num;
                    this.BeginCell();
                    this.EndCell();
                }
            }
        }

        private OfficeArtTertiaryProperties CreateTertiaryProperties(IXlHyperlinkOwner drawingObject)
        {
            if ((drawingObject.HyperlinkClick == null) || (string.IsNullOrEmpty(drawingObject.HyperlinkClick.TargetUri) || string.IsNullOrEmpty(drawingObject.HyperlinkClick.Tooltip)))
            {
                return null;
            }
            return new OfficeArtTertiaryProperties { Properties = { new OfficeArtStringProperty(0xc38d, drawingObject.HyperlinkClick.Tooltip) } };
        }

        private OfficeArtShapeContainer CreateTopmostShape() => 
            new OfficeArtShapeContainer { Items = { 
                new OfficeArtShapeGroupCoordinateSystem(),
                new OfficeArtShapeRecord(0, this.GetTopmostShapeId(this.currentSheet.SheetId), 5)
            } };

        string IXlCellFormatter.GetFormattedValue(IXlCell cell) => 
            this.cellFormatter.GetFormattedValue(cell);

        IXlFormulaParameter IXlFormulaEngine.Concatenate(params IXlFormulaParameter[] parameters) => 
            new XlConcatenateFunction(parameters);

        IXlFormulaParameter IXlFormulaEngine.Param(XlVariantValue value) => 
            new XlFormulaParameter(value);

        IXlFormulaParameter IXlFormulaEngine.Subtotal(XlCellRange range, XlSummary summary, bool ignoreHidden) => 
            XlSubtotalFunction.Create(range, summary, ignoreHidden);

        IXlFormulaParameter IXlFormulaEngine.Subtotal(IList<XlCellRange> ranges, XlSummary summary, bool ignoreHidden) => 
            XlSubtotalFunction.Create(ranges, summary, ignoreHidden);

        IXlFormulaParameter IXlFormulaEngine.Text(IXlFormulaParameter formula, string netFormatString, bool isDateTimeFormatString) => 
            XlTextFunction.Create(formula, netFormatString, isDateTimeFormatString);

        IXlFormulaParameter IXlFormulaEngine.Text(XlVariantValue value, string netFormatString, bool isDateTimeFormatString) => 
            XlTextFunction.Create(value, netFormatString, isDateTimeFormatString);

        IXlFormulaParameter IXlFormulaEngine.VLookup(IXlFormulaParameter lookupValue, XlCellRange table, int columnIndex, bool rangeLookup) => 
            XlVLookupFunction.Create(lookupValue, table, columnIndex, rangeLookup);

        IXlFormulaParameter IXlFormulaEngine.VLookup(XlVariantValue lookupValue, XlCellRange table, int columnIndex, bool rangeLookup) => 
            XlVLookupFunction.Create(lookupValue, table, columnIndex, rangeLookup);

        private void Encrypt()
        {
            if (this.cipher != null)
            {
                new XlsRC4Encryptor(this.keygen, this.cipher, this.workbookWriter.BaseStream).Execute();
            }
        }

        public void EndCell()
        {
            if (this.currentCell == null)
            {
                throw new InvalidOperationException("BeginCell/EndCell calls consistency.");
            }
            if (this.currentCell.ColumnIndex >= this.options.MaxColumnCount)
            {
                throw new ArgumentOutOfRangeException($"Cell column index out of range 0..{this.options.MaxColumnCount - 1}.");
            }
            if (this.currentCell.ColumnIndex < this.currentColumnIndex)
            {
                throw new InvalidOperationException("Cell column index consistency.");
            }
            XlDifferentialFormatting differentialFormat = this.currentSheet.GetDifferentialFormat(this.currentCell.ColumnIndex, this.currentCell.RowIndex);
            this.currentCell.ApplyDifferentialFormatting(differentialFormat);
            if (this.options.SuppressEmptyStrings && (this.currentCell.Value.IsText && string.IsNullOrEmpty(this.currentCell.Value.TextValue)))
            {
                this.currentCell.Value = XlVariantValue.Empty;
            }
            if (!this.currentCell.Value.IsEmpty || (this.currentCell.HasFormula || !XlCellFormatting.Equals(this.inheritedFormatting, this.currentCell.Formatting)))
            {
                if (this.currentCell.Value.IsNumeric && XNumChecker.IsNegativeZero(this.currentCell.Value.NumericValue))
                {
                    this.currentCell.Value = 0.0;
                }
                this.currentSheet.RegisterCellPosition(this.currentCell);
                if (this.currentRow.Cells.Count == 0)
                {
                    this.currentRow.FirstColumnIndex = this.currentCell.ColumnIndex;
                }
                this.currentRow.LastColumnIndex = this.currentCell.ColumnIndex;
                this.currentRow.Cells.Add(this.currentCell);
                if (!XlCellFormatting.EqualFonts(this.inheritedFormatting, this.currentCell.Formatting))
                {
                    this.CalculateAutomaticHeight(this.currentCell.Formatting);
                }
            }
            this.currentColumnIndex = this.currentCell.ColumnIndex + 1;
            this.currentCell = null;
        }

        public void EndColumn()
        {
            if (this.currentColumn == null)
            {
                throw new InvalidOperationException("BeginColumn/EndColumn calls consistency.");
            }
            if (this.rowContentStarted)
            {
                throw new InvalidOperationException("Columns have to be created before rows and cells.");
            }
            if (this.currentColumn.ColumnIndex >= this.options.MaxColumnCount)
            {
                throw new ArgumentOutOfRangeException($"Column index out of range 0...{this.options.MaxColumnCount - 1}");
            }
            this.currentSheet.RegisterColumn(this.currentColumn);
            this.currentColumnIndex = this.currentSheet.ColumnIndex;
            if ((this.currentGroup != null) && this.currentGroup.IsCollapsed)
            {
                this.currentColumn.IsHidden = true;
            }
            if ((this.currentGroup != null) && (this.currentGroup.OutlineLevel > 0))
            {
                this.currentColumn.OutlineLevel = Math.Min(7, this.currentGroup.OutlineLevel);
            }
            int num = this.RegisterFormatting(this.currentColumn.Formatting);
            if (num < 0)
            {
                num = 15;
            }
            this.currentColumn.FormatIndex = num;
            this.currentColumn = null;
        }

        protected void EndDocument()
        {
            if (this.sheets.Count == 0)
            {
                this.BeginSheet();
                this.EndSheet();
            }
            this.palette.UseCustomPalette = this.options.UseCustomPalette;
            this.RegisterDefinedNames();
            this.RegisterCondFmtExpressions();
            this.boundPositions.Clear();
            this.writer = this.workbookWriter;
            this.ValidateWorkbookView();
            this.WriteWorkbookGlobals();
            this.WriteWorksheets();
            this.writer = null;
            this.ExportSummaryInformation();
            this.ExportDocumentSummaryInformation();
            this.currentDocument = null;
            this.documentProperties = null;
        }

        public void EndExport()
        {
            this.EndDocument();
            this.cellFormatter = null;
            if (this.outputStream == null)
            {
                throw new InvalidOperationException("BeginExport/EndExport calls consistency.");
            }
            this.Encrypt();
            StructuredStorageWriter writer = new StructuredStorageWriter();
            StorageDirectoryEntry rootDirectoryEntry = writer.RootDirectoryEntry;
            rootDirectoryEntry.AddStreamDirectoryEntry("Workbook", this.workbookWriter.BaseStream);
            this.workbookWriter.Flush();
            if (this.summaryInformationWriter.BaseStream.Length > 0L)
            {
                rootDirectoryEntry.AddStreamDirectoryEntry("\x0005SummaryInformation", this.summaryInformationWriter.BaseStream);
            }
            this.summaryInformationWriter.Flush();
            if (this.docSummaryInformationWriter.BaseStream.Length > 0L)
            {
                rootDirectoryEntry.AddStreamDirectoryEntry("\x0005DocumentSummaryInformation", this.docSummaryInformationWriter.BaseStream);
            }
            this.docSummaryInformationWriter.Flush();
            writer.Write(this.outputStream);
            this.outputStream = null;
            if (this.workbookWriter != null)
            {
                this.workbookWriter.Dispose();
                this.workbookWriter = null;
            }
            if (this.workbookStream != null)
            {
                this.workbookStream.Dispose();
                this.workbookStream = null;
            }
            if (this.summaryInformationWriter != null)
            {
                this.summaryInformationWriter.Dispose();
                this.summaryInformationWriter = null;
            }
            if (this.docSummaryInformationWriter != null)
            {
                this.docSummaryInformationWriter.Dispose();
                this.docSummaryInformationWriter = null;
            }
            this.ClearSheets();
            this.sharedStrings.Clear();
            this.extendedSSTItems.Clear();
            this.drawingGroup.Clear();
            this.ClearEncryptionHeader();
        }

        public void EndGroup()
        {
            this.groups.Pop();
            if (this.groups.Count <= 0)
            {
                this.currentGroup = null;
            }
            else
            {
                this.currentGroup = this.groups.Peek();
            }
        }

        public void EndPicture()
        {
            if (this.currentSheet == null)
            {
                throw new InvalidOperationException("No current sheet. Please use BeginPicture/EndPicture inside BeginSheet/EndSheet scope.");
            }
            int blipId = this.AddToDrawingGroup(this.currentPicture);
            if (blipId > 0)
            {
                XlsPictureObject item = this.CreateDrawingObject(blipId, this.currentPicture);
                this.currentSheet.DrawingObjects.Add(item);
            }
            this.currentPicture = null;
        }

        public void EndRow()
        {
            if (this.currentRow == null)
            {
                throw new InvalidOperationException("BeginRow/EndRow calls consistency.");
            }
            if (this.currentRow.RowIndex >= this.options.MaxRowCount)
            {
                throw new ArgumentOutOfRangeException("Maximum number of rows exceeded (XLS format). Reduce the number of rows or use XLSX format.");
            }
            if (this.currentRow.RowIndex < this.currentRowIndex)
            {
                throw new InvalidOperationException("Row index consistency.");
            }
            XlCellRange range = XlCellRange.FromLTRB(this.currentColumnIndex, this.currentRowIndex, this.options.MaxColumnCount - 1, this.currentRowIndex);
            this.CreateTableCells(range);
            this.currentSheet.FilterRow(this.currentRow, this.currentRow.Cells);
            this.writer = this.currentSheet.GetCellTableWriter();
            this.currentRowIndex = this.currentRow.RowIndex + 1;
            this.currentRow.IsHidden |= (this.currentGroup != null) && this.currentGroup.IsCollapsed;
            if (!this.currentRow.IsDefault() || ((this.currentGroup != null) && (this.currentGroup.OutlineLevel > 0)))
            {
                this.rowsToExport.Add(this.currentRow);
                this.CalculateAutomaticHeight(this.currentRow.Formatting);
                this.currentSheet.RegisterRow(this.currentRow);
                this.RegisterDimensions();
                this.WriteRow();
            }
            this.rowCount++;
            if (this.rowCount >= 0x20)
            {
                this.WriteRowsContent();
                this.rowCount = 0;
            }
            this.currentRow = null;
        }

        public void EndSheet()
        {
            if (this.currentSheet == null)
            {
                throw new InvalidOperationException("BeginSheet/EndSheet calls consistency.");
            }
            if (!this.IsUniqueSheetName())
            {
                throw new InvalidOperationException($"Worksheet name '{this.currentSheet.Name}' is not unique.");
            }
            this.WritePendingRowContent();
            this.sheets.Add(this.currentSheet);
            this.currentSheet.SheetId = this.sheets.Count;
            this.CloseTables();
            this.RegisterSheetDefinitions();
            this.RegisterMergeCells();
            this.currentSheet = null;
            this.writer = null;
        }

        private void ExportDocumentSummaryInformation()
        {
            OlePropertyStreamContent content = new OlePropertyStreamContent();
            this.GenerateDocSummary(content);
            this.GenerateUserDefined(content);
            content.Write(this.docSummaryInformationWriter);
        }

        private void ExportSummaryInformation()
        {
            OlePropertyStreamContent content = new OlePropertyStreamContent();
            this.GenerateSummary(content);
            content.Write(this.summaryInformationWriter);
        }

        private bool FirstStringInBucket(int index) => 
            (index % this.stringsInBucket) == 0;

        private void FlushCells()
        {
            XlCell cell = this.cellsToExport[0];
            if (cell.HasFormula)
            {
                this.WriteFormulaCell();
            }
            else
            {
                switch (cell.Value.Type)
                {
                    case XlVariantValueType.None:
                        this.WriteBlankCells();
                        break;

                    case XlVariantValueType.Boolean:
                        this.WriteBooleanCell();
                        break;

                    case XlVariantValueType.Text:
                        this.WriteSharedStringCell();
                        break;

                    case XlVariantValueType.Numeric:
                    case XlVariantValueType.DateTime:
                        this.WriteNumericCells();
                        break;

                    case XlVariantValueType.Error:
                        this.WriteErrorCell();
                        break;

                    default:
                        break;
                }
            }
            this.cellsToExport.Clear();
        }

        private void GenerateDocSummary(OlePropertyStreamContent content)
        {
            OlePropertySetDocSummary item = new OlePropertySetDocSummary();
            OlePropertyCodePage page1 = new OlePropertyCodePage();
            page1.Value = 0x4b0;
            item.Properties.Add(page1);
            if (!string.IsNullOrEmpty(this.documentProperties.Category))
            {
                OlePropertyCategory category1 = new OlePropertyCategory(0x1f);
                category1.Value = this.documentProperties.Category;
                item.Properties.Add(category1);
            }
            if (!string.IsNullOrEmpty(this.documentProperties.Manager))
            {
                OlePropertyManager manager1 = new OlePropertyManager(0x1f);
                manager1.Value = this.documentProperties.Manager;
                item.Properties.Add(manager1);
            }
            if (!string.IsNullOrEmpty(this.documentProperties.Company))
            {
                OlePropertyCompany company1 = new OlePropertyCompany(0x1f);
                company1.Value = this.documentProperties.Company;
                item.Properties.Add(company1);
            }
            if (!string.IsNullOrEmpty(this.documentProperties.Version))
            {
                char[] separator = new char[] { '.' };
                string[] strArray = this.documentProperties.Version.Split(separator, StringSplitOptions.RemoveEmptyEntries);
                try
                {
                    int num = Convert.ToInt32(strArray[0]) << 0x10;
                    OlePropertyVersion version1 = new OlePropertyVersion();
                    version1.Value = num;
                    item.Properties.Add(version1);
                }
                catch
                {
                }
            }
            content.PropertySets.Add(item);
        }

        private void GenerateSummary(OlePropertyStreamContent content)
        {
            OlePropertySetSummary item = new OlePropertySetSummary();
            OlePropertyCodePage page1 = new OlePropertyCodePage();
            page1.Value = 0x4b0;
            item.Properties.Add(page1);
            if (!string.IsNullOrEmpty(this.documentProperties.Title))
            {
                OlePropertyTitle title1 = new OlePropertyTitle(0x1f);
                title1.Value = this.documentProperties.Title;
                item.Properties.Add(title1);
            }
            if (!string.IsNullOrEmpty(this.documentProperties.Subject))
            {
                OlePropertySubject subject1 = new OlePropertySubject(0x1f);
                subject1.Value = this.documentProperties.Subject;
                item.Properties.Add(subject1);
            }
            if (!string.IsNullOrEmpty(this.documentProperties.Author))
            {
                OlePropertyAuthor author1 = new OlePropertyAuthor(0x1f);
                author1.Value = this.documentProperties.Author;
                item.Properties.Add(author1);
            }
            if (!string.IsNullOrEmpty(this.documentProperties.Keywords))
            {
                OlePropertyKeywords keywords1 = new OlePropertyKeywords(0x1f);
                keywords1.Value = this.documentProperties.Keywords;
                item.Properties.Add(keywords1);
            }
            if (!string.IsNullOrEmpty(this.documentProperties.Description))
            {
                OlePropertyComments comments1 = new OlePropertyComments(0x1f);
                comments1.Value = this.documentProperties.Description;
                item.Properties.Add(comments1);
            }
            if (!string.IsNullOrEmpty(this.documentProperties.Author))
            {
                OlePropertyLastAuthor author2 = new OlePropertyLastAuthor(0x1f);
                author2.Value = this.documentProperties.Author;
                item.Properties.Add(author2);
            }
            if (this.documentProperties.Created != DateTime.MinValue)
            {
                OlePropertyCreated created1 = new OlePropertyCreated();
                created1.Value = this.documentProperties.Created;
                item.Properties.Add(created1);
                OlePropertyModified modified1 = new OlePropertyModified();
                modified1.Value = this.documentProperties.Created;
                item.Properties.Add(modified1);
            }
            if (!string.IsNullOrEmpty(this.documentProperties.Application))
            {
                OlePropertyApplication application1 = new OlePropertyApplication(0x1f);
                application1.Value = this.documentProperties.Application;
                item.Properties.Add(application1);
            }
            OlePropertyDocSecurity security1 = new OlePropertyDocSecurity();
            security1.Value = (int) this.documentProperties.Security;
            item.Properties.Add(security1);
            content.PropertySets.Add(item);
        }

        private void GenerateUserDefined(OlePropertyStreamContent content)
        {
            XlDocumentCustomProperties custom = this.documentProperties.Custom;
            if (custom.Count != 0)
            {
                OlePropertySetUserDefined item = new OlePropertySetUserDefined();
                OlePropertyDictionary dictionary = new OlePropertyDictionary();
                item.Properties.Add(dictionary);
                OlePropertyCodePage page1 = new OlePropertyCodePage();
                page1.Value = 0x4b0;
                item.Properties.Add(page1);
                int propertyId = 2;
                foreach (string str in custom.Names)
                {
                    XlCustomPropertyValue value2 = custom[str];
                    if (value2.Type == XlVariantValueType.Text)
                    {
                        OlePropertyString text1 = new OlePropertyString(propertyId, 0x1f);
                        text1.Value = value2.TextValue;
                        item.Properties.Add(text1);
                    }
                    else if (value2.Type == XlVariantValueType.DateTime)
                    {
                        OlePropertyFileTime time1 = new OlePropertyFileTime(propertyId);
                        time1.Value = value2.DateTimeValue;
                        item.Properties.Add(time1);
                    }
                    else if (value2.Type == XlVariantValueType.Boolean)
                    {
                        OlePropertyBool bool1 = new OlePropertyBool(propertyId);
                        bool1.Value = value2.BooleanValue;
                        item.Properties.Add(bool1);
                    }
                    else
                    {
                        if (value2.Type != XlVariantValueType.Numeric)
                        {
                            continue;
                        }
                        double numericValue = value2.NumericValue;
                        int num3 = (int) numericValue;
                        if (num3 == numericValue)
                        {
                            OlePropertyInt32 num1 = new OlePropertyInt32(propertyId);
                            num1.Value = num3;
                            item.Properties.Add(num1);
                        }
                        else
                        {
                            OlePropertyDouble num4 = new OlePropertyDouble(propertyId);
                            num4.Value = numericValue;
                            item.Properties.Add(num4);
                        }
                    }
                    dictionary.Entries.Add(propertyId, str);
                    propertyId++;
                }
                if (dictionary.Entries.Count > 0)
                {
                    content.PropertySets.Add(item);
                }
            }
        }

        private XlViewPaneType GetActivePane(bool hasRightPane, bool hasBottomPane) => 
            !(hasRightPane & hasBottomPane) ? (!hasBottomPane ? (!hasRightPane ? XlViewPaneType.TopLeft : XlViewPaneType.TopRight) : XlViewPaneType.BottomLeft) : XlViewPaneType.BottomRight;

        private int GetBorderColorIndex(XlBorderLineStyle lineStyle, XlColor color) => 
            (lineStyle != XlBorderLineStyle.None) ? this.palette.GetForegroundColorIndex(color, this.currentDocument.Theme) : 0;

        private XlsRef8 GetBoundRange(IList<XlCellRange> ranges)
        {
            XlsRef8 ref2 = null;
            for (int i = 0; i < ranges.Count; i++)
            {
                XlsRef8 other = XlsRef8.FromRange(ranges[i]);
                if (ref2 == null)
                {
                    ref2 = other;
                }
                else
                {
                    ref2.Union(other);
                }
            }
            return ref2;
        }

        private int GetCellHeight(int row)
        {
            int defaultRowHeightInPixels = -1;
            if (this.currentSheet.RowHeights.ContainsKey(row))
            {
                defaultRowHeightInPixels = this.currentSheet.RowHeights[row];
            }
            if (defaultRowHeightInPixels < 0)
            {
                defaultRowHeightInPixels = (int) this.currentSheet.DefaultRowHeightInPixels;
            }
            return defaultRowHeightInPixels;
        }

        private int GetCellWidth(int column)
        {
            int widthInPixels = -1;
            if (this.currentSheet.ColumnsTable.ContainsKey(column))
            {
                IXlColumn column2 = this.currentSheet.ColumnsTable[column];
                widthInPixels = column2.WidthInPixels;
                if (column2.IsHidden)
                {
                    widthInPixels = 0;
                }
            }
            if (widthInPixels < 0)
            {
                widthInPixels = (int) this.currentSheet.DefaultColumnWidthInPixels;
            }
            return widthInPixels;
        }

        private int GetDataValidationsRecordCount(IList<XlDataValidation> collection)
        {
            int num = 0;
            int count = collection.Count;
            for (int i = 0; i < count; i++)
            {
                XlDataValidation validation = collection[i];
                if (this.IsXlsCompatibleRange(validation.Ranges))
                {
                    num++;
                    if (num == 0xfffe)
                    {
                        break;
                    }
                }
            }
            return num;
        }

        private int GetFixedPoint(double value)
        {
            short num = (short) value;
            if (((value - num) != 0.0) && (value < 0.0))
            {
                num = (short) (num - 1);
            }
            return ((((ushort) ((value - num) * 65536.0)) + num) << 0x10);
        }

        private int GetFormatIndex(XlCellFormatting formatting)
        {
            int num = this.RegisterFormatting(formatting);
            if (num < 0)
            {
                num = 15;
            }
            return num;
        }

        private byte[] GetFormulaBytes(XlCellRange range)
        {
            XlExpression expression = new XlExpression();
            expression.Add(this.CreatePtg(range));
            return expression.GetBytes(this);
        }

        private byte[] GetFormulaBytes(XlValueObject value)
        {
            if (value.IsEmpty)
            {
                return null;
            }
            if (value.IsRange)
            {
                return this.GetFormulaBytes(value.RangeValue);
            }
            if (value.IsExpression)
            {
                return value.Expression.ToXlsExpression(this).GetBytes(this);
            }
            if (!value.IsFormula)
            {
                return this.GetFormulaBytes(value.VariantValue);
            }
            if (this.formulaParser == null)
            {
                throw new InvalidOperationException("Formula parser required for this operation.");
            }
            this.expressionContext.ReferenceMode = XlCellReferenceMode.Offset;
            this.expressionContext.ExpressionStyle = XlExpressionStyle.Normal;
            XlExpression expression = this.formulaParser.Parse(value.Formula, this.expressionContext);
            if (expression == null)
            {
                throw new InvalidOperationException($"Can't parse formula '{value.Formula}'.");
            }
            return expression.ToXlsExpression(this).GetBytes(this);
        }

        private byte[] GetFormulaBytes(XlVariantValue value)
        {
            XlExpression expression = new XlExpression();
            expression.Add(this.CreatePtg(value));
            return expression.GetBytes(this);
        }

        private byte[] GetFormulaBytes(XlCell cell)
        {
            XlExpression expression;
            if ((cell == null) || !cell.HasFormula)
            {
                return null;
            }
            this.expressionContext.CurrentCell = new XlCellPosition(cell.ColumnIndex, cell.RowIndex);
            if (cell.Formula != null)
            {
                expression = this.FormulaConverter.Convert(cell.Formula).ToXlsExpression(this);
            }
            else if (cell.Expression != null)
            {
                expression = cell.Expression.ToXlsExpression(this);
            }
            else
            {
                if (this.formulaParser == null)
                {
                    throw new InvalidOperationException("Formula parser required for this operation.");
                }
                this.expressionContext.ReferenceMode = XlCellReferenceMode.Reference;
                this.expressionContext.ExpressionStyle = XlExpressionStyle.Normal;
                expression = this.formulaParser.Parse(cell.FormulaString, this.expressionContext);
                if ((expression == null) || (expression.Count == 0))
                {
                    throw new InvalidOperationException($"Can't parse formula '{cell.FormulaString}'.");
                }
                expression = expression.ToXlsExpression(this);
            }
            byte[] bytes = expression.GetBytes(this);
            if (bytes.Length <= 0x708)
            {
                return bytes;
            }
            expression.Clear();
            expression.Add(new XlPtgErr(XlCellErrorType.Value));
            return expression.GetBytes(this);
        }

        private byte[] GetFormulaBytes(IList<XlVariantValue> values)
        {
            XlExpression expression = new XlExpression();
            StringBuilder builder = new StringBuilder();
            foreach (XlVariantValue value2 in values)
            {
                if (builder.Length > 0)
                {
                    builder.Append("\0");
                }
                builder.Append(value2.ToText().TextValue);
            }
            expression.Add(new XlPtgStr(builder.ToString()));
            return expression.GetBytes(this);
        }

        private byte[] GetHyperlinkData(IXlHyperlinkOwner drawingObject)
        {
            if ((drawingObject.HyperlinkClick == null) || string.IsNullOrEmpty(drawingObject.HyperlinkClick.TargetUri))
            {
                return null;
            }
            BinaryHyperlinkObject obj2 = new BinaryHyperlinkObject();
            string targetUri = drawingObject.HyperlinkClick.TargetUri;
            if (!drawingObject.HyperlinkClick.IsExternal)
            {
                char[] trimChars = new char[] { '#' };
                obj2.Location = targetUri.TrimStart(trimChars);
                obj2.HasLocationString = true;
            }
            else
            {
                Uri uri;
                string str2 = string.Empty;
                int index = targetUri.IndexOf('#');
                if (index != -1)
                {
                    str2 = targetUri.Substring(index + 1);
                    targetUri = targetUri.Substring(0, index);
                }
                if (!Uri.TryCreate(targetUri, UriKind.RelativeOrAbsolute, out uri))
                {
                    return null;
                }
                obj2.HasMoniker = true;
                obj2.IsAbsolute = uri.IsAbsoluteUri;
                if (!uri.IsAbsoluteUri || (uri.Scheme == "file"))
                {
                    BinaryHyperlinkFileMoniker moniker2 = new BinaryHyperlinkFileMoniker {
                        Path = targetUri
                    };
                    obj2.OleMoniker = moniker2;
                }
                else
                {
                    BinaryHyperlinkURLMoniker moniker = new BinaryHyperlinkURLMoniker {
                        Url = targetUri,
                        HasOptionalData = true,
                        AllowImplicitFileScheme = true,
                        AllowRelative = true,
                        Canonicalize = true,
                        CrackUnknownSchemes = true,
                        DecodeExtraInfo = true,
                        IESettings = true,
                        NoEncodeForbiddenChars = true,
                        PreProcessHtmlUri = true
                    };
                    obj2.OleMoniker = moniker;
                }
                if (!string.IsNullOrEmpty(str2))
                {
                    obj2.Location = str2;
                    obj2.HasLocationString = true;
                }
            }
            if (!string.IsNullOrEmpty(drawingObject.HyperlinkClick.TargetFrame))
            {
                obj2.FrameName = drawingObject.HyperlinkClick.TargetFrame;
                obj2.HasFrameName = true;
            }
            return obj2.GetHyperlinkData();
        }

        private XlsImageFormat GetImageFormat(ImageFormat format) => 
            !ReferenceEquals(format, ImageFormat.Jpeg) ? (!ReferenceEquals(format, ImageFormat.Tiff) ? ((ReferenceEquals(format, ImageFormat.Emf) || ReferenceEquals(format, ImageFormat.Wmf)) ? XlsImageFormat.Emf : XlsImageFormat.Png) : XlsImageFormat.Tiff) : XlsImageFormat.Jpeg;

        private int GetLineStyleBooleanProperties(XlShape shape)
        {
            int num = 0x80000;
            if (!shape.Outline.Color.IsEmpty)
            {
                num |= 8;
            }
            if (shape.IsConnector)
            {
                num |= 0x100010;
            }
            if (shape.Outline.StrokeAlignment == XlOutlineStrokeAlignment.Inset)
            {
                num |= 0x400040;
            }
            return num;
        }

        private byte[] GetListRangeFormulaBytes(XlCellRange range)
        {
            if (string.IsNullOrEmpty(range.SheetName))
            {
                return this.GetFormulaBytes(range);
            }
            XlExpression expression = new XlExpression();
            expression.Add(new XlPtgName(this.GetListSourceNameIndex(range), XlPtgDataType.Reference));
            return expression.GetBytes(this);
        }

        private int GetListSourceNameIndex(XlCellRange range) => 
            !this.dataValidationListRanges.ContainsKey(range) ? 0 : this.dataValidationListRanges[range];

        private int GetRuleCount(XlConditionalFormatting cf)
        {
            int num = 0;
            foreach (XlCondFmtRule rule in cf.Rules)
            {
                XlCondFmtRuleIconSet set = rule as XlCondFmtRuleIconSet;
                if (set == null)
                {
                    num++;
                    continue;
                }
                if (!set.IsCustom && XlsCondFmtHelper.IsSupportedIconSet(set.IconSetType))
                {
                    num++;
                }
            }
            return num;
        }

        private XlsCFRuleTemplate GetRuleTemplate(bool above, bool equal) => 
            !above ? (equal ? XlsCFRuleTemplate.BelowOrEqualToAverage : XlsCFRuleTemplate.BelowAverage) : (equal ? XlsCFRuleTemplate.AboveOrEqualToAverage : XlsCFRuleTemplate.AboveAverage);

        private int GetShapeTypeCode(XlShape shape) => 
            (shape.GeometryPreset != XlGeometryPreset.Rect) ? 20 : 1;

        private byte[] GetSharedFormulaBytes(XlCell cell)
        {
            XlExpression expression;
            this.expressionContext.CurrentCell = new XlCellPosition(cell.ColumnIndex, cell.RowIndex);
            if (cell.Expression != null)
            {
                expression = cell.Expression.ToXlsExpression(this);
            }
            else
            {
                if (this.formulaParser == null)
                {
                    throw new InvalidOperationException("Formula parser required for this operation.");
                }
                this.expressionContext.ReferenceMode = XlCellReferenceMode.Offset;
                this.expressionContext.ExpressionStyle = XlExpressionStyle.Shared;
                expression = this.formulaParser.Parse(cell.FormulaString, this.expressionContext);
                if ((expression == null) || (expression.Count == 0))
                {
                    throw new InvalidOperationException($"Can't parse shared formula '{cell.FormulaString}'.");
                }
                expression = expression.ToXlsExpression(this);
            }
            byte[] bytes = expression.GetBytes(this);
            if (bytes.Length <= 0x708)
            {
                return bytes;
            }
            expression.Clear();
            expression.Add(new XlPtgErr(XlCellErrorType.Value));
            return expression.GetBytes(this);
        }

        private byte[] GetSharedFormulaRefBytes(XlCellPosition hostCell)
        {
            XlExpression expression = new XlExpression();
            expression.Add(new XlPtgExp(hostCell));
            return expression.GetBytes(this);
        }

        internal int GetSheetDefinitionIndex(string sheetName)
        {
            int num = -1;
            this.sheetDefinitions.TryGetValue(sheetName, out num);
            return num;
        }

        private int GetTablesCount()
        {
            int num = 0;
            foreach (XlsTableBasedDocumentSheet sheet in this.sheets)
            {
                num += sheet.InnerTables.Count;
            }
            return num;
        }

        private XlsCFRuleTemplate GetTimePeriodTemplate(XlCondFmtRuleTimePeriod rule)
        {
            switch (rule.TimePeriod)
            {
                case XlCondFmtTimePeriod.Last7Days:
                    return XlsCFRuleTemplate.Last7Days;

                case XlCondFmtTimePeriod.LastMonth:
                    return XlsCFRuleTemplate.LastMonth;

                case XlCondFmtTimePeriod.LastWeek:
                    return XlsCFRuleTemplate.LastWeek;

                case XlCondFmtTimePeriod.NextMonth:
                    return XlsCFRuleTemplate.NextMonth;

                case XlCondFmtTimePeriod.NextWeek:
                    return XlsCFRuleTemplate.NextWeek;

                case XlCondFmtTimePeriod.ThisMonth:
                    return XlsCFRuleTemplate.ThisMonth;

                case XlCondFmtTimePeriod.ThisWeek:
                    return XlsCFRuleTemplate.ThisWeek;

                case XlCondFmtTimePeriod.Today:
                    return XlsCFRuleTemplate.Today;

                case XlCondFmtTimePeriod.Tomorrow:
                    return XlsCFRuleTemplate.Tomorrow;

                case XlCondFmtTimePeriod.Yesterday:
                    return XlsCFRuleTemplate.Yesterday;
            }
            return XlsCFRuleTemplate.Formula;
        }

        private XlCellPosition GetTopLeftCell(IList<XlCellRange> ranges)
        {
            if (ranges.Count == 0)
            {
                return XlCellPosition.InvalidValue;
            }
            XlCellRange range = ranges[0];
            int column = range.TopLeft.Column;
            int row = range.TopLeft.Row;
            for (int i = 1; i < ranges.Count; i++)
            {
                range = ranges[i];
                column = Math.Min(column, range.TopLeft.Column);
                XlCellPosition topLeft = range.TopLeft;
                row = Math.Min(row, topLeft.Row);
            }
            return new XlCellPosition(column, row);
        }

        private int GetTopmostShapeId(int sheetId) => 
            (((sheetId - 1) % 0x10) + 1) * 0x400;

        private bool HasAddInUDFDefinition() => 
            this.sheetDefinitions.ContainsKey("*AddInUDF");

        private bool HasAutoFilterCriteria()
        {
            bool flag;
            using (List<XlsTableBasedDocumentSheet>.Enumerator enumerator = this.sheets.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlsTableBasedDocumentSheet current = enumerator.Current;
                        if (!current.HasAutoFilterCriteria())
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private bool HasFormulaInCF12(XlCondFmtRuleWithFormatting rule) => 
            (rule != null) ? (rule.RuleType != XlCondFmtType.Top10) : false;

        private bool HasSelfRererence() => 
            this.sheetDefinitions.Count > (this.HasAddInUDFDefinition() ? 1 : 0);

        private int IndexOfSheet(string sheetName)
        {
            for (int i = 0; i < this.sheets.Count; i++)
            {
                if (this.sheets[i].Name == sheetName)
                {
                    return i;
                }
            }
            return -1;
        }

        private void InitFonts()
        {
            this.fontTable.Clear();
            this.fontList.Clear();
            XlFont key = XlFont.BodyFont();
            this.fontTable.Add(key, 0);
            this.fontList.Add(key);
            this.defaultFont = key;
            key = new XlFont();
            key.CopyFrom(this.defaultFont);
            key.Bold = true;
            this.fontTable.Add(key, 1);
            this.fontList.Add(key);
            key = new XlFont();
            key.CopyFrom(this.defaultFont);
            key.Italic = true;
            this.fontTable.Add(key, 2);
            this.fontList.Add(key);
            key = new XlFont();
            key.CopyFrom(this.defaultFont);
            key.Bold = true;
            key.Italic = true;
            this.fontTable.Add(key, 3);
            this.fontList.Add(key);
        }

        private void InitializeContent(XlsContentCellBase content, IXlCell cell)
        {
            content.RowIndex = cell.RowIndex;
            content.ColumnIndex = cell.ColumnIndex;
            content.FormatIndex = this.GetFormatIndex(cell.Formatting);
        }

        private void InitializeSharedStrings()
        {
            this.sharedStringsRefCount = 0;
            this.sharedStrings.Clear();
            this.extendedSSTItems.Clear();
        }

        private void InitializeStyles()
        {
            this.palette = new XlsPalette();
            this.InitFonts();
            this.InitNumberFormats();
            this.defaultBorder = new XlBorder();
            this.defaultAlignment = new XlCellAlignment();
            this.defaultFill = XlFill.NoFill();
            this.InitXfs();
        }

        private void InitNumberFormats()
        {
            this.predefinedNumberFormats.Clear();
            this.AddPredefined(5, "_($#,##0_);($#,##0)");
            this.AddPredefined(6, "_($#,##0_);[Red]($#,##0)");
            this.AddPredefined(7, "_($#,##0.00_);($#,##0.00)");
            this.AddPredefined(8, "_($#,##0.00_);[Red]($#,##0.00)");
            this.AddPredefined(0x29, "_(* #,##0_);_(* \\(#,##0\\);_(* \"-\"_);_(@_)");
            this.AddPredefined(0x2a, "_(\"$\"* #,##0_);_(\"$\"* \\(#,##0\\);_(\"$\"* \"-\"_);_(@_)");
            this.AddPredefined(0x2b, "_(* #,##0.00_);_(* \\(#,##0.00\\);_(* \"-\"??_);_(@_)");
            this.AddPredefined(0x2c, "_(\"$\"* #,##0.00_);_(\"$\"* \\(#,##0.00\\);_(\"$\"* \"-\"??_);_(@_)");
            this.numberFormatTable.Clear();
            this.netNumberFormatTable.Clear();
            this.customNumberFormatId = 0xa4;
        }

        private void InitXfs()
        {
            this.xfList.Clear();
            this.xfTable.Clear();
            XlsXf key = new XlsXf {
                IsStyleFormat = true,
                Fill = this.defaultFill,
                Border = this.defaultBorder,
                Alignment = this.defaultAlignment
            };
            this.xfTable.Add(key, this.xfList.Count);
            this.xfList.Add(key);
            for (int i = 0; i < 2; i++)
            {
                key = new XlsXf {
                    IsStyleFormat = true,
                    Fill = this.defaultFill,
                    Border = this.defaultBorder,
                    Alignment = this.defaultAlignment,
                    FontId = 1,
                    ApplyAlignment = false,
                    ApplyBorder = false,
                    ApplyFill = false,
                    ApplyNumberFormat = false,
                    ApplyProtection = false
                };
                if (!this.xfTable.ContainsKey(key))
                {
                    this.xfTable.Add(key, this.xfList.Count);
                }
                this.xfList.Add(key);
            }
            for (int j = 0; j < 2; j++)
            {
                key = new XlsXf {
                    IsStyleFormat = true,
                    Fill = this.defaultFill,
                    Border = this.defaultBorder,
                    Alignment = this.defaultAlignment,
                    FontId = 2,
                    ApplyAlignment = false,
                    ApplyBorder = false,
                    ApplyFill = false,
                    ApplyNumberFormat = false,
                    ApplyProtection = false
                };
                if (!this.xfTable.ContainsKey(key))
                {
                    this.xfTable.Add(key, this.xfList.Count);
                }
                this.xfList.Add(key);
            }
            for (int k = 0; k < 10; k++)
            {
                key = new XlsXf {
                    IsStyleFormat = true,
                    Fill = this.defaultFill,
                    Border = this.defaultBorder,
                    Alignment = this.defaultAlignment,
                    ApplyAlignment = false,
                    ApplyBorder = false,
                    ApplyFill = false,
                    ApplyNumberFormat = false,
                    ApplyProtection = false
                };
                if (!this.xfTable.ContainsKey(key))
                {
                    this.xfTable.Add(key, this.xfList.Count);
                }
                this.xfList.Add(key);
            }
            key = new XlsXf {
                IsStyleFormat = false,
                Fill = this.defaultFill,
                Border = this.defaultBorder,
                Alignment = this.defaultAlignment,
                ApplyAlignment = false,
                ApplyBorder = false,
                ApplyFill = false,
                ApplyNumberFormat = false,
                ApplyProtection = false,
                ApplyFont = false
            };
            this.xfTable.Add(key, this.xfList.Count);
            this.xfList.Add(key);
            key = new XlsXf {
                IsStyleFormat = true,
                Fill = this.defaultFill,
                Border = this.defaultBorder,
                Alignment = this.defaultAlignment,
                NumberFormatId = 0x2b,
                ApplyAlignment = false,
                ApplyBorder = false,
                ApplyFill = false,
                ApplyFont = false,
                ApplyProtection = false
            };
            if (!this.xfTable.ContainsKey(key))
            {
                this.xfTable.Add(key, this.xfList.Count);
            }
            this.xfList.Add(key);
            key = new XlsXf {
                IsStyleFormat = true,
                Fill = this.defaultFill,
                Border = this.defaultBorder,
                Alignment = this.defaultAlignment,
                NumberFormatId = 0x29,
                ApplyAlignment = false,
                ApplyBorder = false,
                ApplyFill = false,
                ApplyFont = false,
                ApplyProtection = false
            };
            if (!this.xfTable.ContainsKey(key))
            {
                this.xfTable.Add(key, this.xfList.Count);
            }
            this.xfList.Add(key);
            key = new XlsXf {
                IsStyleFormat = true,
                Fill = this.defaultFill,
                Border = this.defaultBorder,
                Alignment = this.defaultAlignment,
                NumberFormatId = 0x2c,
                ApplyAlignment = false,
                ApplyBorder = false,
                ApplyFill = false,
                ApplyFont = false,
                ApplyProtection = false
            };
            if (!this.xfTable.ContainsKey(key))
            {
                this.xfTable.Add(key, this.xfList.Count);
            }
            this.xfList.Add(key);
            key = new XlsXf {
                IsStyleFormat = true,
                Fill = this.defaultFill,
                Border = this.defaultBorder,
                Alignment = this.defaultAlignment,
                NumberFormatId = 0x2a,
                ApplyAlignment = false,
                ApplyBorder = false,
                ApplyFill = false,
                ApplyFont = false,
                ApplyProtection = false
            };
            if (!this.xfTable.ContainsKey(key))
            {
                this.xfTable.Add(key, this.xfList.Count);
            }
            this.xfList.Add(key);
            key = new XlsXf {
                IsStyleFormat = true,
                Fill = this.defaultFill,
                Border = this.defaultBorder,
                Alignment = this.defaultAlignment,
                NumberFormatId = 9,
                ApplyAlignment = false,
                ApplyBorder = false,
                ApplyFill = false,
                ApplyFont = false,
                ApplyProtection = false
            };
            if (!this.xfTable.ContainsKey(key))
            {
                this.xfTable.Add(key, this.xfList.Count);
            }
            this.xfList.Add(key);
        }

        private bool IsCondFmt12(XlConditionalFormatting cf)
        {
            bool flag;
            using (IEnumerator<XlCondFmtRule> enumerator = cf.Rules.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlCondFmtRule current = enumerator.Current;
                        if (this.IsCondFmt12(cf, current as XlCondFmtRuleWithFormatting))
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        private bool IsCondFmt12(XlConditionalFormatting cf, XlCondFmtRuleWithFormatting rule) => 
            (rule != null) ? (((rule.Formatting == null) || string.IsNullOrEmpty(rule.Formatting.NetFormatString)) ? (((rule.RuleType != XlCondFmtType.Top10) || (cf.Ranges.Count <= 1)) ? ((rule.RuleType == XlCondFmtType.AboveOrBelowAverage) && ((cf.Ranges.Count > 1) && (((XlCondFmtRuleAboveAverage) rule).StdDev > 0))) : true) : true) : true;

        private bool IsExtColor(XlColor color)
        {
            XlColorType colorType = color.ColorType;
            return ((colorType == XlColorType.Auto) || ((colorType == XlColorType.Rgb) || (colorType == XlColorType.Theme)));
        }

        private bool IsUniqueSheetName()
        {
            bool flag;
            using (List<XlsTableBasedDocumentSheet>.Enumerator enumerator = this.sheets.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        IXlSheet current = enumerator.Current;
                        if (!string.Equals(this.currentSheet.Name, current.Name, StringComparison.OrdinalIgnoreCase))
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        private bool IsXlsCompatibleRange(XlCellRange cellRange) => 
            (cellRange.TopLeft.Column < 0x100) && (cellRange.TopLeft.Row < 0x10000);

        private bool IsXlsCompatibleRange(IList<XlCellRange> ranges)
        {
            int count = ranges.Count;
            if (count == 0)
            {
                return false;
            }
            for (int i = 0; i < count; i++)
            {
                if (!this.IsXlsCompatibleRange(ranges[i]))
                {
                    return false;
                }
            }
            return true;
        }

        private bool LoadTotalString(XlTable table, XlTableColumn column) => 
            (column.TotalRowFunction == XlTotalRowFunction.None) && (!string.IsNullOrEmpty(column.TotalRowLabel) && (column.TotalRowLabel != "Total"));

        private bool NeedToFlushCells(XlCell cell)
        {
            int count = this.cellsToExport.Count;
            if (count == 0)
            {
                return false;
            }
            if (cell.HasFormula)
            {
                return true;
            }
            XlCell cell2 = this.cellsToExport[count - 1];
            if (cell2.HasFormula)
            {
                return true;
            }
            if ((cell.ColumnIndex - cell2.ColumnIndex) > 1)
            {
                return true;
            }
            XlVariantValueType type = cell.Value.Type;
            return (((type == XlVariantValueType.None) || (type == XlVariantValueType.Numeric)) ? ((cell2.Value.Type == type) ? (((cell2.Value.Type != XlVariantValueType.Numeric) || XlsRkNumber.IsRkValue(cell2.Value.NumericValue)) ? (((type != XlVariantValueType.Numeric) || XlsRkNumber.IsRkValue(cell.Value.NumericValue)) ? ((type == XlVariantValueType.None) && ReferenceEquals(cell.Formatting, null)) : true) : true) : true) : true);
        }

        private void PrepareAutoFilter12Content(XlsContentAutoFilter12 content, IXlFilterCriteria filterCriteria)
        {
            XlFilterType filterType = filterCriteria.FilterType;
            if (filterType == XlFilterType.Dynamic)
            {
                XlDynamicFilter filter = (XlDynamicFilter) filterCriteria;
                content.FilterType = XlsAutoFilter12Type.None;
                content.CustomFilterType = (int) filter.DynamicFilterType;
                content.CriteriaCount = filter.GetCriteriaCount();
            }
            else if (filterType == XlFilterType.Icon)
            {
                XlIconFilter filter2 = (XlIconFilter) filterCriteria;
                content.FilterType = XlsAutoFilter12Type.CellIcon;
                content.IconSet = filter2.IconSet;
                content.IconId = filter2.IconId;
            }
            else if (filterType == XlFilterType.Color)
            {
                XlColorFilter filter3 = (XlColorFilter) filterCriteria;
                content.FilterType = filter3.FilterByCellColor ? XlsAutoFilter12Type.CellColor : XlsAutoFilter12Type.CellFont;
                XlColor rgb = filter3.Color.Rgb;
                XlFill fill = rgb.IsEmpty ? (!filter3.FilterByCellColor ? XlFill.PatternFill(XlPatternType.Solid, XlColor.DefaultBackground, XlColor.Auto) : XlFill.PatternFill(XlPatternType.None, XlColor.DefaultBackground, XlColor.DefaultForeground)) : ((filter3.PatternType != XlPatternType.Solid) ? XlFill.PatternFill(filter3.PatternType, rgb, filter3.PatternColor.Rgb) : XlFill.PatternFill(XlPatternType.Solid, XlColor.FromArgb(0xff, 0xff, 0xff), rgb));
                this.PrepareDxfFill(content.ColorInfo, fill);
            }
            else if (filterType == XlFilterType.Values)
            {
                XlValuesFilter filter4 = (XlValuesFilter) filterCriteria;
                content.FilterType = XlsAutoFilter12Type.None;
                content.CriteriaCount = filter4.FilterByBlank ? (filter4.Values.Count + 1) : filter4.Values.Count;
                content.DateGroupingsCount = filter4.DateGroups.Count;
            }
        }

        private void PrepareAutoFilterContent(XlsContentAutoFilter content, IXlFilterCriteria filterCriteria)
        {
            if (filterCriteria.FilterType == XlFilterType.Top10)
            {
                XlTop10Filter filter = (XlTop10Filter) filterCriteria;
                content.Join = true;
                content.IsTopFilter = true;
                content.IsTop = filter.Top;
                content.IsPercent = filter.Percent;
                content.TopValue = (int) filter.Value;
                content.First.Value = filter.FilterValue;
                content.Second.IsBlank = true;
            }
            else if (filterCriteria.FilterType == XlFilterType.Custom)
            {
                XlCustomFilters filters = (XlCustomFilters) filterCriteria;
                content.Join = !filters.And;
                content.First.FilterOperator = filters.First.FilterOperator;
                content.First.Value = filters.First.Value;
                if (filters.Second != null)
                {
                    content.Second.FilterOperator = filters.Second.FilterOperator;
                    content.Second.Value = filters.Second.Value;
                }
                else
                {
                    content.Second.IsBlank = true;
                    content.Join = true;
                }
            }
            else
            {
                XlValuesFilter filter2 = (XlValuesFilter) filterCriteria;
                content.Join = true;
                if (filter2.FilterByBlank)
                {
                    content.First.AllBlanksMatched = true;
                    content.First.FilterOperator = XlFilterOperator.Equal;
                    if (filter2.Values.Count <= 0)
                    {
                        content.Second.IsBlank = true;
                    }
                    else
                    {
                        content.Second.FilterOperator = XlFilterOperator.Equal;
                        content.Second.Value = filter2.Values[0];
                    }
                }
                else
                {
                    if (filter2.Values.Count > 0)
                    {
                        content.First.FilterOperator = XlFilterOperator.Equal;
                        content.First.Value = filter2.Values[0];
                    }
                    if (filter2.Values.Count <= 1)
                    {
                        content.Second.IsBlank = true;
                    }
                    else
                    {
                        content.Second.FilterOperator = XlFilterOperator.Equal;
                        content.Second.Value = filter2.Values[1];
                    }
                }
            }
        }

        private void PrepareCondFmtDxfFont(XlsDxfN dxf, XlFont font)
        {
            if (font != null)
            {
                dxf.SetIsEmpty(false);
                dxf.FlagsInfo.IncludeFont = true;
                XlsDxfFont fontInfo = dxf.FontInfo;
                if (font.Bold != this.defaultFont.Bold)
                {
                    fontInfo.FontBold = new bool?(font.Bold);
                    fontInfo.FontBoldNinch = false;
                }
                if (font.Italic != this.defaultFont.Italic)
                {
                    fontInfo.FontItalic = font.Italic;
                    fontInfo.FontItalicNinch = false;
                }
                if (font.StrikeThrough != this.defaultFont.StrikeThrough)
                {
                    fontInfo.FontStrikeThrough = font.StrikeThrough;
                    fontInfo.FontStrikeThroughNinch = false;
                }
                if (font.Underline != this.defaultFont.Underline)
                {
                    fontInfo.FontUnderline = new XlUnderlineType?(font.Underline);
                    fontInfo.FontUnderlineNinch = false;
                }
                if (font.Script != this.defaultFont.Script)
                {
                    fontInfo.FontScript = new XlScriptType?(font.Script);
                    fontInfo.FontScriptNinch = false;
                }
                fontInfo.FontColorIndex = this.palette.GetFontColorIndex(font.Color, this.currentDocument.Theme);
                if (!font.Color.IsEmpty)
                {
                    dxf.AddExtProperty(new XfPropFullColor(13, font.Color));
                }
                fontInfo.IsDefaultFont = false;
            }
        }

        private void PrepareContent(IXlsCFContentAdapter content, XlCondFmtRule rule, XlConditionalFormatting cf)
        {
            switch (rule.RuleType)
            {
                case XlCondFmtType.AboveOrBelowAverage:
                    this.PrepareRuleAboveAverage(content, cf.Ranges, rule as XlCondFmtRuleAboveAverage);
                    return;

                case XlCondFmtType.BeginsWith:
                case XlCondFmtType.ContainsText:
                case XlCondFmtType.EndsWith:
                case XlCondFmtType.NotContainsText:
                    this.PrepareRuleSpecificText(content, rule as XlCondFmtRuleSpecificText);
                    return;

                case XlCondFmtType.CellIs:
                    this.PrepareRuleCellIs(content, rule as XlCondFmtRuleCellIs, cf);
                    return;

                case XlCondFmtType.ColorScale:
                case XlCondFmtType.ContainsErrors:
                case XlCondFmtType.DataBar:
                case XlCondFmtType.IconSet:
                case XlCondFmtType.NotContainsErrors:
                    break;

                case XlCondFmtType.ContainsBlanks:
                    this.PrepareRuleBlanks(content, rule as XlCondFmtRuleWithFormatting);
                    return;

                case XlCondFmtType.DuplicateValues:
                    this.PrepareRuleUniqueDuplicates(content, cf.Ranges, rule as XlCondFmtRuleWithFormatting, false);
                    return;

                case XlCondFmtType.Expression:
                    this.PrepareRuleExpression(content, rule as XlCondFmtRuleExpression, cf);
                    return;

                case XlCondFmtType.NotContainsBlanks:
                    this.PrepareRuleNoBlanks(content, rule as XlCondFmtRuleWithFormatting);
                    return;

                case XlCondFmtType.TimePeriod:
                    this.PrepareRuleTimePeriod(content, rule as XlCondFmtRuleTimePeriod);
                    break;

                case XlCondFmtType.Top10:
                    this.PrepareRuleTop10(content, cf.Ranges, rule as XlCondFmtRuleTop10);
                    return;

                case XlCondFmtType.UniqueValues:
                    this.PrepareRuleUniqueDuplicates(content, cf.Ranges, rule as XlCondFmtRuleWithFormatting, true);
                    return;

                default:
                    return;
            }
        }

        private void PrepareDxf(XlsDxfN dxf, XlCondFmtRuleWithFormatting rule)
        {
            if (rule != null)
            {
                XlDifferentialFormatting formatting = rule.Formatting;
                if (formatting != null)
                {
                    this.PrepareDxfNumberFormat(dxf, formatting);
                    this.PrepareCondFmtDxfFont(dxf, formatting.Font);
                    this.PrepareDxfAlign(dxf, formatting.Alignment);
                    this.PrepareDxfBorder(dxf, formatting.Border);
                    this.PrepareDxfFill(dxf, formatting.Fill);
                }
            }
        }

        protected void PrepareDxf(XlsDxfN dxf, XlDifferentialFormatting formatting)
        {
            if (formatting != null)
            {
                this.PrepareDxfNumberFormat(dxf, formatting);
                this.PrepareDxfFont(dxf, formatting.Font);
                this.PrepareDxfAlign(dxf, formatting.Alignment);
                this.PrepareDxfBorder(dxf, formatting.Border);
                this.PrepareDxfFill(dxf, formatting.Fill);
            }
        }

        private void PrepareDxfAlign(XlsDxfN dxf, XlCellAlignment align)
        {
            if (align != null)
            {
                dxf.SetIsEmpty(false);
                dxf.FlagsInfo.IncludeAlignment = true;
                if (this.defaultAlignment.HorizontalAlignment != align.HorizontalAlignment)
                {
                    dxf.AlignmentInfo.HorizontalAlignment = align.HorizontalAlignment;
                    dxf.FlagsInfo.AlignmentHorizontalNinch = false;
                }
                if (this.defaultAlignment.VerticalAlignment != align.VerticalAlignment)
                {
                    dxf.AlignmentInfo.VerticalAlignment = align.VerticalAlignment;
                    dxf.FlagsInfo.AlignmentVerticalNinch = false;
                }
                if (this.defaultAlignment.WrapText != align.WrapText)
                {
                    dxf.AlignmentInfo.WrapText = align.WrapText;
                    dxf.FlagsInfo.AlignmentWrapTextNinch = false;
                }
                if (this.defaultAlignment.Indent != align.Indent)
                {
                    dxf.AlignmentInfo.Indent = align.Indent;
                    dxf.FlagsInfo.AlignmentIndentNinch = false;
                }
                dxf.AlignmentInfo.RelativeIndent = align.RelativeIndent;
                if (this.defaultAlignment.JustifyLastLine != align.JustifyLastLine)
                {
                    dxf.AlignmentInfo.JustifyLastLine = align.JustifyLastLine;
                    dxf.FlagsInfo.AlignmentJustifyLastLineNinch = false;
                }
                if (this.defaultAlignment.ShrinkToFit != align.ShrinkToFit)
                {
                    dxf.AlignmentInfo.ShrinkToFit = align.ShrinkToFit;
                    dxf.FlagsInfo.AlignmentShrinkToFitNinch = false;
                }
                if (this.defaultAlignment.ReadingOrder != align.ReadingOrder)
                {
                    dxf.AlignmentInfo.ReadingOrder = align.ReadingOrder;
                    dxf.FlagsInfo.AlignmentReadingOrderNinch = false;
                    dxf.FlagsInfo.AlignmentReadingOrderZeroInited = true;
                }
            }
        }

        private void PrepareDxfBorder(XlsDxfN dxf, XlBorder border)
        {
            if (border != null)
            {
                dxf.SetIsEmpty(false);
                dxf.FlagsInfo.IncludeBorder = true;
                if (border.LeftLineStyle != XlBorderLineStyle.None)
                {
                    dxf.BorderInfo.LeftLineStyle = border.LeftLineStyle;
                    dxf.BorderInfo.LeftColorIndex = this.palette.GetColorIndex(border.LeftColor.ConvertToRgb(this.currentDocument.Theme));
                    dxf.FlagsInfo.BorderLeftNinch = false;
                    if (!border.LeftColor.IsEmpty)
                    {
                        dxf.AddExtProperty(new XfPropFullColor(9, border.LeftColor));
                    }
                }
                if (border.RightLineStyle != XlBorderLineStyle.None)
                {
                    dxf.BorderInfo.RightLineStyle = border.RightLineStyle;
                    dxf.BorderInfo.RightColorIndex = this.palette.GetColorIndex(border.RightColor.ConvertToRgb(this.currentDocument.Theme));
                    dxf.FlagsInfo.BorderRightNinch = false;
                    if (!border.RightColor.IsEmpty)
                    {
                        dxf.AddExtProperty(new XfPropFullColor(10, border.RightColor));
                    }
                }
                if (border.TopLineStyle != XlBorderLineStyle.None)
                {
                    dxf.BorderInfo.TopLineStyle = border.TopLineStyle;
                    dxf.BorderInfo.TopColorIndex = this.palette.GetColorIndex(border.TopColor.ConvertToRgb(this.currentDocument.Theme));
                    dxf.FlagsInfo.BorderTopNinch = false;
                    if (!border.TopColor.IsEmpty)
                    {
                        dxf.AddExtProperty(new XfPropFullColor(7, border.TopColor));
                    }
                }
                if (border.BottomLineStyle != XlBorderLineStyle.None)
                {
                    dxf.BorderInfo.BottomLineStyle = border.BottomLineStyle;
                    dxf.BorderInfo.BottomColorIndex = this.palette.GetColorIndex(border.BottomColor.ConvertToRgb(this.currentDocument.Theme));
                    dxf.FlagsInfo.BorderBottomNinch = false;
                    if (!border.BottomColor.IsEmpty)
                    {
                        dxf.AddExtProperty(new XfPropFullColor(8, border.BottomColor));
                    }
                }
                if (border.DiagonalUpLineStyle != XlBorderLineStyle.None)
                {
                    dxf.BorderInfo.DiagonalUp = true;
                    dxf.BorderInfo.DiagonalLineStyle = border.DiagonalUpLineStyle;
                    dxf.BorderInfo.DiagonalColorIndex = this.palette.GetColorIndex(border.DiagonalColor.ConvertToRgb(this.currentDocument.Theme));
                    dxf.FlagsInfo.BorderDiagonalUpNinch = false;
                }
                if (border.DiagonalDownLineStyle != XlBorderLineStyle.None)
                {
                    dxf.BorderInfo.DiagonalDown = true;
                    dxf.BorderInfo.DiagonalLineStyle = border.DiagonalDownLineStyle;
                    dxf.BorderInfo.DiagonalColorIndex = this.palette.GetColorIndex(border.DiagonalColor.ConvertToRgb(this.currentDocument.Theme));
                    dxf.FlagsInfo.BorderDiagonalDownNinch = false;
                }
                if (!border.DiagonalColor.IsEmpty && (border.DiagonalLineStyle != XlBorderLineStyle.None))
                {
                    dxf.AddExtProperty(new XfPropFullColor(11, border.DiagonalColor));
                }
                dxf.FlagsInfo.NewBorder = border.Outline;
            }
        }

        private void PrepareDxfFill(XlsDxfN dxf, XlFill fill)
        {
            if (fill != null)
            {
                dxf.SetIsEmpty(false);
                dxf.FlagsInfo.IncludeFill = true;
                dxf.FillInfo.PatternType = fill.PatternType;
                dxf.FlagsInfo.FillPatternTypeNinch = false;
                dxf.FillInfo.ForeColorIndex = this.palette.GetForegroundColorIndex(fill.ForeColor, this.currentDocument.Theme);
                dxf.FlagsInfo.FillForegroundColorNinch = false;
                if (!fill.ForeColor.IsEmpty)
                {
                    dxf.AddExtProperty(new XfPropFullColor(4, fill.ForeColor));
                }
                dxf.FillInfo.BackColorIndex = this.palette.GetBackgroundColorIndex(fill.BackColor, this.currentDocument.Theme);
                dxf.FlagsInfo.FillBackgroundColorNinch = false;
                if (!fill.BackColor.IsEmpty)
                {
                    dxf.AddExtProperty(new XfPropFullColor(5, fill.BackColor));
                }
            }
        }

        protected void PrepareDxfFont(XlsDxfN dxf, XlFont font)
        {
            if (font != null)
            {
                dxf.SetIsEmpty(false);
                dxf.FlagsInfo.IncludeFont = true;
                XlsDxfFont fontInfo = dxf.FontInfo;
                if (font.Bold != this.defaultFont.Bold)
                {
                    fontInfo.FontBold = new bool?(font.Bold);
                    fontInfo.FontBoldNinch = false;
                }
                if (font.Italic != this.defaultFont.Italic)
                {
                    fontInfo.FontItalic = font.Italic;
                    fontInfo.FontItalicNinch = false;
                }
                if (font.StrikeThrough != this.defaultFont.StrikeThrough)
                {
                    fontInfo.FontStrikeThrough = font.StrikeThrough;
                    fontInfo.FontStrikeThroughNinch = false;
                }
                if (font.Underline != this.defaultFont.Underline)
                {
                    fontInfo.FontUnderline = new XlUnderlineType?(font.Underline);
                    fontInfo.FontUnderlineNinch = false;
                }
                if (font.Script != this.defaultFont.Script)
                {
                    fontInfo.FontScript = new XlScriptType?(font.Script);
                    fontInfo.FontScriptNinch = false;
                }
                if (font.Size > 0.0)
                {
                    fontInfo.FontSize = Math.Min(0x1fff, (int) (font.Size * 20.0));
                }
                fontInfo.FontColorIndex = this.palette.GetFontColorIndex(font.Color, this.currentDocument.Theme);
                if (!font.Color.IsEmpty)
                {
                    dxf.AddExtProperty(new XfPropFullColor(13, font.Color));
                }
                fontInfo.FontName = font.Name;
                fontInfo.FontFamily = (int) font.FontFamily;
                fontInfo.FontCharset = font.Charset;
                fontInfo.FontScript = new XlScriptType?(font.Script);
                if (font.SchemeStyle != XlFontSchemeStyles.None)
                {
                    dxf.AddExtProperty(new XfPropByte(14, (font.SchemeStyle == XlFontSchemeStyles.Major) ? ((byte) 1) : ((byte) 2)));
                }
                fontInfo.IsDefaultFont = false;
            }
        }

        private void PrepareDxfNumberFormat(XlsDxfN dxf, XlDifferentialFormatting formatting)
        {
            if (!string.IsNullOrEmpty(formatting.NetFormatString))
            {
                ExcelNumberFormat format2 = this.ConvertNetFormatStringToXlFormatCode(formatting.NetFormatString, formatting.IsDateTimeFormatString);
                if (format2 != null)
                {
                    this.SetDxfNumberFormat(dxf, format2.Id, format2.FormatString);
                }
            }
            else
            {
                XlNumberFormat numberFormat = formatting.NumberFormat;
                if (numberFormat != null)
                {
                    this.SetDxfNumberFormat(dxf, numberFormat.FormatId, numberFormat.FormatCode);
                }
            }
        }

        private void PrepareRuleAboveAverage(IXlsCFContentAdapter content, IList<XlCellRange> ranges, XlCondFmtRuleAboveAverage rule)
        {
            content.SetRuleType(XlCondFmtType.Expression);
            content.SetOperator(XlCondFmtOperator.BeginsWith);
            content.SetRuleTemplate(this.GetRuleTemplate(rule.AboveAverage, rule.EqualAverage));
            XlExpression expression = this.expressionFactory.CreateAboveAverageExpression(rule, ranges);
            content.SetFirstFormula(expression.GetBytes(this));
            content.SetStdDev(rule.StdDev);
        }

        private void PrepareRuleBlanks(IXlsCFContentAdapter content, XlCondFmtRuleWithFormatting rule)
        {
            content.SetRuleType(XlCondFmtType.Expression);
            content.SetOperator(XlCondFmtOperator.BeginsWith);
            content.SetRuleTemplate(XlsCFRuleTemplate.ContainsBlanks);
            XlExpression expression = this.expressionFactory.CreateBlanksExpression();
            content.SetFirstFormula(expression.GetBytes(this));
        }

        private void PrepareRuleCellIs(IXlsCFContentAdapter content, XlCondFmtRuleCellIs rule, XlConditionalFormatting cf)
        {
            content.SetRuleType(rule.RuleType);
            content.SetOperator(rule.Operator);
            content.SetRuleTemplate(XlsCFRuleTemplate.CellValue);
            this.expressionContext.CurrentCell = cf.GetTopLeftCell();
            content.SetFirstFormula(this.GetFormulaBytes(rule.Value));
            if ((rule.Operator == XlCondFmtOperator.Between) || (rule.Operator == XlCondFmtOperator.NotBetween))
            {
                content.SetSecondFormula(this.GetFormulaBytes(rule.SecondValue));
            }
        }

        private void PrepareRuleColorScale(XlsContentCF12 content, XlCondFmtRuleColorScale rule, XlConditionalFormatting cf)
        {
            content.RuleType = XlCondFmtType.ColorScale;
            content.Operator = XlCondFmtOperator.BeginsWith;
            content.RuleTemplate = XlsCFRuleTemplate.ColorScale;
            content.Format.IsEmpty = true;
            content.ColorScaleParams.ColorScaleType = rule.ColorScaleType;
            this.expressionContext.CurrentCell = cf.GetTopLeftCell();
            this.PrepareValueObject(content.ColorScaleParams.MinValue, rule.MinValue);
            this.PrepareValueObject(content.ColorScaleParams.MidValue, rule.MidpointValue);
            this.PrepareValueObject(content.ColorScaleParams.MaxValue, rule.MaxValue);
            content.ColorScaleParams.MinColor = rule.MinColor;
            content.ColorScaleParams.MidColor = rule.MidpointColor;
            content.ColorScaleParams.MaxColor = rule.MaxColor;
        }

        private void PrepareRuleDataBar(XlsContentCF12 content, XlCondFmtRuleDataBar rule, XlConditionalFormatting cf)
        {
            content.RuleType = XlCondFmtType.DataBar;
            content.Operator = XlCondFmtOperator.BeginsWith;
            content.RuleTemplate = XlsCFRuleTemplate.DataBar;
            content.Format.IsEmpty = true;
            content.DataBarParams.BarColor = rule.FillColor;
            this.expressionContext.CurrentCell = cf.GetTopLeftCell();
            this.PrepareValueObject(content.DataBarParams.MinValue, rule.MinValue);
            this.PrepareValueObject(content.DataBarParams.MaxValue, rule.MaxValue);
            content.DataBarParams.PercentMin = rule.MinLength;
            content.DataBarParams.PercentMax = rule.MaxLength;
            content.DataBarParams.ShowBarOnly = !rule.ShowValues;
        }

        private void PrepareRuleExpression(IXlsCFContentAdapter content, XlCondFmtRuleExpression rule, XlConditionalFormatting cf)
        {
            content.SetRuleType(rule.RuleType);
            content.SetOperator(XlCondFmtOperator.BeginsWith);
            content.SetRuleTemplate(XlsCFRuleTemplate.Formula);
            XlExpression expression = null;
            if (rule.Expression != null)
            {
                expression = rule.Expression;
                this.expressionContext.CurrentCell = cf.GetTopLeftCell();
            }
            else if (!string.IsNullOrEmpty(rule.Formula))
            {
                if (this.formulaParser == null)
                {
                    throw new InvalidOperationException("Formula parser required for this operation.");
                }
                this.expressionContext.CurrentCell = cf.GetTopLeftCell();
                this.expressionContext.ReferenceMode = XlCellReferenceMode.Offset;
                this.expressionContext.ExpressionStyle = XlExpressionStyle.Normal;
                expression = this.formulaParser.Parse(rule.Formula, this.expressionContext);
                if (expression == null)
                {
                    throw new InvalidOperationException($"Can't parse rule formula '{rule.Formula}'.");
                }
            }
            if (expression != null)
            {
                content.SetFirstFormula(expression.ToXlsExpression(this).GetBytes(this));
            }
        }

        private bool PrepareRuleIconSet(XlsContentCF12 content, XlCondFmtRuleIconSet rule, XlConditionalFormatting cf)
        {
            if (!XlsCondFmtHelper.IsSupportedIconSet(rule.IconSetType))
            {
                return false;
            }
            content.RuleType = XlCondFmtType.IconSet;
            content.Operator = XlCondFmtOperator.BeginsWith;
            content.RuleTemplate = XlsCFRuleTemplate.IconSet;
            content.Format.IsEmpty = true;
            content.IconSetParams.IconSet = rule.IconSetType;
            content.IconSetParams.IconsOnly = !rule.ShowValues;
            content.IconSetParams.Reverse = rule.Reverse;
            this.expressionContext.CurrentCell = cf.GetTopLeftCell();
            foreach (XlCondFmtValueObject obj2 in rule.Values)
            {
                XlsCondFmtIconThreshold threshold = new XlsCondFmtIconThreshold();
                this.PrepareValueObject(threshold, obj2);
                threshold.EqualPass = obj2.GreaterThanOrEqual;
                content.IconSetParams.Thresholds.Add(threshold);
            }
            return true;
        }

        private void PrepareRuleNoBlanks(IXlsCFContentAdapter content, XlCondFmtRuleWithFormatting rule)
        {
            content.SetRuleType(XlCondFmtType.Expression);
            content.SetOperator(XlCondFmtOperator.BeginsWith);
            content.SetRuleTemplate(XlsCFRuleTemplate.ContainsNoBlanks);
            XlExpression expression = this.expressionFactory.CreateNoBlanksExpression();
            content.SetFirstFormula(expression.GetBytes(this));
        }

        private void PrepareRuleSpecificText(IXlsCFContentAdapter content, XlCondFmtRuleSpecificText rule)
        {
            content.SetRuleType(XlCondFmtType.Expression);
            content.SetOperator(XlCondFmtOperator.BeginsWith);
            content.SetRuleTemplate(XlsCFRuleTemplate.ContainsText);
            XlExpression expression = this.expressionFactory.CreateSpecificTextExpression(rule);
            content.SetFirstFormula(expression.GetBytes(this));
            content.SetTextRule((XlCondFmtSpecificTextType) rule.RuleType);
        }

        private void PrepareRuleTimePeriod(IXlsCFContentAdapter content, XlCondFmtRuleTimePeriod rule)
        {
            content.SetRuleType(XlCondFmtType.Expression);
            content.SetOperator(XlCondFmtOperator.BeginsWith);
            content.SetRuleTemplate(this.GetTimePeriodTemplate(rule));
            XlExpression expression = this.expressionFactory.CreateTimePeriodExpression(rule);
            content.SetFirstFormula(expression.GetBytes(this));
        }

        private void PrepareRuleTop10(IXlsCFContentAdapter content, IList<XlCellRange> ranges, XlCondFmtRuleTop10 rule)
        {
            content.SetRuleType(XlCondFmtType.Top10);
            content.SetOperator(XlCondFmtOperator.BeginsWith);
            content.SetRuleTemplate(XlsCFRuleTemplate.Filter);
            content.SetFilterTop(!rule.Bottom);
            content.SetFilterPercent(rule.Percent);
            content.SetFilterValue(rule.Rank);
            XlExpression expression = this.expressionFactory.CreateTop10Expression(rule, ranges);
            content.SetFirstFormula(expression.GetBytes(this));
        }

        private void PrepareRuleUniqueDuplicates(IXlsCFContentAdapter content, IList<XlCellRange> ranges, XlCondFmtRuleWithFormatting rule, bool unique)
        {
            content.SetRuleType(XlCondFmtType.Expression);
            content.SetOperator(XlCondFmtOperator.BeginsWith);
            content.SetRuleTemplate(unique ? XlsCFRuleTemplate.UniqueValues : XlsCFRuleTemplate.DuplicateValues);
            XlExpression expression = this.expressionFactory.CreateUniqueDuplicatesExpression(unique, ranges);
            content.SetFirstFormula(expression.GetBytes(this));
        }

        private void PrepareValueObject(XlsCondFmtValueObject obj, XlCondFmtValueObject value)
        {
            switch (value.ObjectType)
            {
                case XlCondFmtValueObjectType.Max:
                case XlCondFmtValueObjectType.AutoMax:
                    obj.ObjectType = XlCondFmtValueObjectType.Max;
                    return;

                case XlCondFmtValueObjectType.Min:
                case XlCondFmtValueObjectType.AutoMin:
                    obj.ObjectType = XlCondFmtValueObjectType.Min;
                    return;
            }
            obj.ObjectType = value.ObjectType;
            if ((value.ObjectType != XlCondFmtValueObjectType.Formula) && (!value.Value.IsRange && value.Value.IsNumeric))
            {
                obj.Value = value.Value.NumericValue;
            }
            else
            {
                obj.FormulaBytes = this.GetFormulaBytes(value.Value);
            }
        }

        private bool PrepareXFExtension(XlsXf format, int xfIndex)
        {
            XlsContentXFExt item = new XlsContentXFExt {
                XFIndex = xfIndex
            };
            this.PrepareXFExtProperties(format, item.Properties);
            bool flag = item.Properties.Count > 0;
            if (flag)
            {
                this.xfExtensions.Add(item);
            }
            return flag;
        }

        private void PrepareXFExtProperties(XlsXf format, XfProperties properties)
        {
            XlFill fill = format.Fill;
            if (this.IsExtColor(fill.ForeColor))
            {
                properties.Add(new XfPropFullColor(4, fill.ForeColor));
            }
            if (this.IsExtColor(fill.BackColor))
            {
                properties.Add(new XfPropFullColor(5, fill.BackColor));
            }
            XlBorder border = format.Border;
            if ((border.TopLineStyle != XlBorderLineStyle.None) && this.IsExtColor(border.TopColor))
            {
                properties.Add(new XfPropFullColor(7, border.TopColor));
            }
            if ((border.BottomLineStyle != XlBorderLineStyle.None) && this.IsExtColor(border.BottomColor))
            {
                properties.Add(new XfPropFullColor(8, border.BottomColor));
            }
            if ((border.LeftLineStyle != XlBorderLineStyle.None) && this.IsExtColor(border.LeftColor))
            {
                properties.Add(new XfPropFullColor(9, border.LeftColor));
            }
            if ((border.RightLineStyle != XlBorderLineStyle.None) && this.IsExtColor(border.RightColor))
            {
                properties.Add(new XfPropFullColor(10, border.RightColor));
            }
            if (((border.DiagonalDownLineStyle != XlBorderLineStyle.None) || (border.DiagonalUpLineStyle != XlBorderLineStyle.None)) && this.IsExtColor(border.DiagonalColor))
            {
                properties.Add(new XfPropFullColor(11, border.DiagonalColor));
            }
            XlFont font = this.fontList[format.FontId];
            if (this.IsExtColor(font.Color))
            {
                properties.Add(new XfPropFullColor(13, font.Color));
            }
            if (font.SchemeStyle != XlFontSchemeStyles.None)
            {
                properties.Add(new XfPropByte(14, (font.SchemeStyle == XlFontSchemeStyles.Minor) ? ((byte) 2) : ((byte) 1)));
            }
            XlCellAlignment alignment = format.Alignment;
            if (alignment.Indent > 15)
            {
                properties.Add(new XfPropUInt16(15, Math.Min(alignment.Indent, 250)));
            }
        }

        private void RegisterAutoFilter(IXlSheet sheet, int sheetIndex)
        {
            if (sheet.AutoFilterRange != null)
            {
                XlsContentDefinedName item = new XlsContentDefinedName {
                    Name = "_xlnm._FilterDatabase",
                    SheetIndex = sheetIndex + 1,
                    IsHidden = true
                };
                XlExpression expression = new XlExpression();
                expression.Add(new XlPtgArea3d(sheet.AutoFilterRange.AsAbsolute(), sheet.Name));
                item.FormulaBytes = expression.ToXlsExpression(this).GetBytes(this);
                this.definedNames.Add(item);
            }
        }

        private void RegisterCondFmtExpression(XlCondFmtRuleExpression rule, XlConditionalFormatting cf)
        {
            XlExpression expression = null;
            if (rule.Expression != null)
            {
                this.expressionContext.CurrentCell = cf.GetTopLeftCell();
                expression = rule.Expression;
            }
            else if (!string.IsNullOrEmpty(rule.Formula) && (this.formulaParser != null))
            {
                this.expressionContext.CurrentCell = cf.GetTopLeftCell();
                this.expressionContext.ReferenceMode = XlCellReferenceMode.Offset;
                this.expressionContext.ExpressionStyle = XlExpressionStyle.Normal;
                expression = this.formulaParser.Parse(rule.Formula, this.expressionContext);
            }
            if (expression != null)
            {
                this.ConvertCondFmtArea3D(expression);
                expression.ToXlsExpression(this);
            }
        }

        private void RegisterCondFmtExpressions()
        {
            for (int i = 0; i < this.sheets.Count; i++)
            {
                IXlSheet sheet = this.sheets[i];
                this.RegisterCondFmtExpressions(sheet);
            }
        }

        private void RegisterCondFmtExpressions(IXlSheet sheet)
        {
            IList<XlConditionalFormatting> conditionalFormattings = sheet.ConditionalFormattings;
            int count = conditionalFormattings.Count;
            for (int i = 0; i < count; i++)
            {
                XlConditionalFormatting cf = conditionalFormattings[i];
                if (!this.ShouldRemove(cf))
                {
                    foreach (XlCondFmtRule rule in cf.Rules)
                    {
                        if (rule.RuleType == XlCondFmtType.Expression)
                        {
                            this.RegisterCondFmtExpression(rule as XlCondFmtRuleExpression, cf);
                        }
                    }
                }
            }
        }

        private int RegisterCondFmtExtRange(XlCellRange range)
        {
            if (this.condFmtExtRanges.ContainsKey(range))
            {
                return this.condFmtExtRanges[range];
            }
            XlsContentDefinedName item = new XlsContentDefinedName {
                Name = $"CfDataRange{this.condFmtExtRanges.Count + 1}",
                SheetIndex = 0,
                IsHidden = false,
                FormulaBytes = this.GetFormulaBytes(range)
            };
            this.definedNames.Add(item);
            this.condFmtExtRanges.Add(range, this.definedNames.Count);
            return this.definedNames.Count;
        }

        private bool RegisterDataValidation(XlDataValidation validation)
        {
            if (!this.IsXlsCompatibleRange(validation.Ranges))
            {
                return false;
            }
            if ((validation.Type == XlDataValidationType.List) && ((validation.ListRange != null) && !string.IsNullOrEmpty(validation.ListRange.SheetName)))
            {
                this.RegisterDataValidationListRange(validation.ListRange);
            }
            return true;
        }

        private void RegisterDataValidationListRange(XlCellRange range)
        {
            if (!this.dataValidationListRanges.ContainsKey(range))
            {
                XlsContentDefinedName item = new XlsContentDefinedName {
                    Name = $"DvListSource{this.dataValidationListRanges.Count + 1}",
                    SheetIndex = 0,
                    IsHidden = false,
                    FormulaBytes = this.GetFormulaBytes(range)
                };
                this.definedNames.Add(item);
                this.dataValidationListRanges.Add(range, this.definedNames.Count);
            }
        }

        private void RegisterDefinedNames()
        {
            for (int i = 0; i < this.sheets.Count; i++)
            {
                IXlSheet sheet = this.sheets[i];
                this.RegisterAutoFilter(sheet, i);
                this.RegisterPrintArea(sheet, i);
                this.RegisterPrintTitles(sheet, i);
            }
        }

        private void RegisterDimensions()
        {
            if (this.currentSheet != null)
            {
                XlDimensions dimensions = this.currentSheet.Dimensions;
                if (dimensions != null)
                {
                    dimensions.FirstRowIndex = Math.Min(dimensions.FirstRowIndex, this.currentRow.RowIndex);
                    dimensions.LastRowIndex = Math.Max(dimensions.LastRowIndex, this.currentRow.RowIndex);
                    if (this.currentRow.Cells.Count > 0)
                    {
                        dimensions.FirstColumnIndex = Math.Min(dimensions.FirstColumnIndex, this.currentRow.FirstColumnIndex);
                        dimensions.LastColumnIndex = Math.Max(dimensions.LastColumnIndex, this.currentRow.LastColumnIndex);
                    }
                }
                else
                {
                    dimensions = new XlDimensions {
                        FirstRowIndex = this.currentRow.RowIndex,
                        LastRowIndex = this.currentRow.RowIndex
                    };
                    if (this.currentRow.Cells.Count <= 0)
                    {
                        dimensions.LastColumnIndex = -1;
                    }
                    else
                    {
                        dimensions.FirstColumnIndex = this.currentRow.FirstColumnIndex;
                        dimensions.LastColumnIndex = this.currentRow.LastColumnIndex;
                    }
                    this.currentSheet.Dimensions = dimensions;
                }
            }
        }

        private void RegisterDimensions(XlsRef8 mergeCells)
        {
            if (this.currentSheet != null)
            {
                XlDimensions dimensions = this.currentSheet.Dimensions;
                if (dimensions != null)
                {
                    dimensions.FirstRowIndex = Math.Min(dimensions.FirstRowIndex, mergeCells.FirstRowIndex);
                    dimensions.LastRowIndex = Math.Max(dimensions.LastRowIndex, mergeCells.LastRowIndex);
                    dimensions.FirstColumnIndex = Math.Min(dimensions.FirstColumnIndex, mergeCells.FirstColumnIndex);
                    dimensions.LastColumnIndex = Math.Max(dimensions.LastColumnIndex, mergeCells.LastColumnIndex);
                }
                else
                {
                    dimensions = new XlDimensions {
                        FirstRowIndex = mergeCells.FirstRowIndex,
                        LastRowIndex = mergeCells.LastRowIndex,
                        FirstColumnIndex = mergeCells.FirstColumnIndex,
                        LastColumnIndex = mergeCells.LastColumnIndex
                    };
                    this.currentSheet.Dimensions = dimensions;
                }
            }
        }

        internal void RegisterExternalName(XlPtgNameX ptg)
        {
            int count = -1;
            if (!this.sheetDefinitions.TryGetValue("*AddInUDF", out count))
            {
                count = this.sheetDefinitions.Count;
                this.sheetDefinitions.Add("*AddInUDF", count);
            }
            int num2 = -1;
            if (!this.externalNames.TryGetValue(ptg.Name, out num2))
            {
                num2 = this.externalNames.Count + 1;
                this.externalNames.Add(ptg.Name, num2);
            }
            ptg.XtiIndex = count;
            ptg.NameIndex = num2;
        }

        private int RegisterFont(XlFont font)
        {
            int count;
            if (font == null)
            {
                return -1;
            }
            if (!this.fontTable.TryGetValue(font, out count))
            {
                count = this.fontTable.Count;
                this.fontTable.Add(font, count);
                this.fontList.Add(font);
            }
            return count;
        }

        protected int RegisterFormatting(XlCellFormatting formatting)
        {
            int count;
            if (formatting == null)
            {
                return -1;
            }
            int num = this.RegisterFont(formatting.Font);
            int num2 = this.RegisterNumberFormat(formatting.NetFormatString, formatting.IsDateTimeFormatString, formatting.NumberFormat);
            XlsXf key = new XlsXf {
                FontId = Math.Max(0, num),
                NumberFormatId = Math.Max(0, num2),
                ApplyFont = num >= 0,
                ApplyFill = formatting.Fill != null,
                ApplyNumberFormat = num2 >= 0,
                ApplyBorder = formatting.Border != null,
                ApplyAlignment = formatting.Alignment != null,
                Fill = (formatting.Fill != null) ? formatting.Fill : this.defaultFill,
                Border = (formatting.Border != null) ? formatting.Border : this.defaultBorder,
                Alignment = (formatting.Alignment != null) ? formatting.Alignment : this.defaultAlignment
            };
            if (!this.xfTable.TryGetValue(key, out count))
            {
                count = this.xfList.Count;
                this.xfTable.Add(key, count);
                this.xfList.Add(key);
            }
            return count;
        }

        internal int RegisterFutureFunction(string name)
        {
            int num = -1;
            if (!this.futureFunctions.TryGetValue(name, out num))
            {
                num = this.definedNames.Count + 1;
                this.futureFunctions.Add(name, num);
                XlsContentDefinedName item = new XlsContentDefinedName {
                    Name = $"_xlfn.{name}",
                    SheetIndex = 0,
                    IsHidden = true,
                    IsXlmMacro = true,
                    IsMacro = true
                };
                XlExpression expression = new XlExpression();
                expression.Add(new XlPtgErr(XlCellErrorType.Name));
                item.FormulaBytes = expression.GetBytes(this);
                this.definedNames.Add(item);
            }
            return num;
        }

        private void RegisterMergeCells()
        {
            IXlMergedCells mergedCells = this.currentSheet.MergedCells;
            if (mergedCells.Count != 0)
            {
                foreach (XlCellRange range in mergedCells)
                {
                    XlsRef8 mergeCells = XlsRef8.FromRange(range);
                    if (mergeCells != null)
                    {
                        this.RegisterDimensions(mergeCells);
                    }
                }
            }
        }

        private int RegisterNumberFormat(string netFormatString, bool isDateTimeFormatString, XlNumberFormat xlNumberFormat)
        {
            ExcelNumberFormat format;
            if (string.IsNullOrEmpty(netFormatString))
            {
                if (xlNumberFormat == null)
                {
                    return -1;
                }
                if (xlNumberFormat.FormatId >= 0)
                {
                    return xlNumberFormat.FormatId;
                }
                string formatCode = xlNumberFormat.FormatCode;
                if (this.numberFormatTable.ContainsKey(formatCode))
                {
                    format = this.numberFormatTable[formatCode];
                }
                else
                {
                    format = new ExcelNumberFormat(this.customNumberFormatId, formatCode);
                    this.numberFormatTable.Add(formatCode, format);
                    this.customNumberFormatId++;
                }
                return format.Id;
            }
            XlNetNumberFormat format1 = new XlNetNumberFormat();
            format1.FormatString = netFormatString;
            format1.IsDateTimeFormat = isDateTimeFormatString;
            XlNetNumberFormat key = format1;
            if (!this.netNumberFormatTable.TryGetValue(key, out format))
            {
                format = this.ConvertNetFormatStringToXlFormatCode(netFormatString, isDateTimeFormatString);
                if (format == null)
                {
                    return -1;
                }
                if (string.IsNullOrEmpty(format.FormatString))
                {
                    return 0;
                }
                if (format.Id >= 0)
                {
                    return format.Id;
                }
                if (this.numberFormatTable.ContainsKey(format.FormatString))
                {
                    format = this.numberFormatTable[format.FormatString];
                }
                else
                {
                    format.Id = this.customNumberFormatId;
                    this.numberFormatTable.Add(format.FormatString, format);
                    this.customNumberFormatId++;
                }
                this.netNumberFormatTable.Add(key, format);
            }
            return format.Id;
        }

        private void RegisterPrintArea(IXlSheet sheet, int sheetIndex)
        {
            if (sheet.PrintArea != null)
            {
                XlsContentDefinedName item = new XlsContentDefinedName {
                    Name = "_xlnm.Print_Area",
                    SheetIndex = sheetIndex + 1
                };
                XlExpression expression = new XlExpression();
                expression.Add(new XlPtgArea3d(sheet.PrintArea.AsAbsolute(), sheet.Name));
                item.FormulaBytes = expression.ToXlsExpression(this).GetBytes(this);
                this.definedNames.Add(item);
            }
        }

        private void RegisterPrintTitles(IXlSheet sheet, int sheetIndex)
        {
            if (sheet.PrintTitles.IsValid())
            {
                XlsContentDefinedName item = new XlsContentDefinedName {
                    Name = "_xlnm.Print_Titles",
                    SheetIndex = sheetIndex + 1
                };
                XlExpression formula = new XlExpression();
                this.AddRangeReference(formula, sheet, sheet.PrintTitles.Columns);
                this.AddRangeReference(formula, sheet, sheet.PrintTitles.Rows);
                if (formula.Count > 1)
                {
                    formula.Add(new XlPtgBinaryOperator(0x10));
                }
                item.FormulaBytes = formula.GetBytes(this);
                this.definedNames.Add(item);
            }
        }

        internal int RegisterSheetDefinition(string sheetName)
        {
            int count = -1;
            if (!this.sheetDefinitions.TryGetValue(sheetName, out count))
            {
                count = this.sheetDefinitions.Count;
                this.sheetDefinitions.Add(sheetName, count);
            }
            return count;
        }

        private void RegisterSheetDefinitions()
        {
            int num = 0;
            foreach (XlDataValidation validation in this.currentSheet.DataValidations)
            {
                if (this.RegisterDataValidation(validation) && ((num + 1) > 0xfffe))
                {
                    break;
                }
            }
        }

        protected int RegisterString(XlRichTextString text)
        {
            this.sharedStringsRefCount++;
            int num2 = this.sharedStrings.RegisterString(text);
            if (num2 == this.sharedStrings.Count)
            {
                foreach (XlRichTextRun run in text.Runs)
                {
                    int num3 = Math.Max(0, this.RegisterFont(run.Font));
                    if (num3 >= 4)
                    {
                        num3++;
                    }
                    run.FontIndex = num3;
                }
            }
            return num2;
        }

        protected int RegisterString(string text)
        {
            this.sharedStringsRefCount++;
            return this.sharedStrings.RegisterString(text);
        }

        private void RemoveIncompatibleFormattings()
        {
            IList<XlConditionalFormatting> conditionalFormattings = this.currentSheet.ConditionalFormattings;
            for (int i = conditionalFormattings.Count - 1; i >= 0; i--)
            {
                if (this.ShouldRemove(conditionalFormattings[i]))
                {
                    conditionalFormattings.RemoveAt(i);
                }
            }
        }

        private ImageFormat ReplaceImageFormat(ImageFormat format) => 
            !ReferenceEquals(format, ImageFormat.Jpeg) ? (!ReferenceEquals(format, ImageFormat.Tiff) ? ((ReferenceEquals(format, ImageFormat.Emf) || ReferenceEquals(format, ImageFormat.Wmf)) ? ImageFormat.Emf : ImageFormat.Png) : format) : format;

        private void RewriteIndex()
        {
            long position = this.writer.BaseStream.Position;
            this.writer.BaseStream.Position = this.indexRecordPosition;
            XlsContentIndex content = new XlsContentIndex();
            XlDimensions dimensions = this.currentSheet.Dimensions;
            if (dimensions != null)
            {
                content.FirstRowIndex = dimensions.FirstRowIndex;
                content.LastRowIndex = dimensions.LastRowIndex + 1;
            }
            content.DefaultColumnWidthOffset = this.defColWidthRecordPosition;
            foreach (long num2 in this.currentSheet.DbCellsPositions)
            {
                content.DbCellsPositions.Add(num2 + this.cellTablePosition);
            }
            this.WriteContent(0x20b, content);
            this.writer.BaseStream.Position = position;
        }

        private void SetDxfNumberFormat(XlsDxfN dxf, int formatId, string formatCode)
        {
            dxf.SetIsEmpty(false);
            dxf.FlagsInfo.IncludeNumberFormat = true;
            if (formatId >= 0)
            {
                XlsDxfNumIFmt fmt1 = new XlsDxfNumIFmt();
                fmt1.NumberFormatId = formatId;
                dxf.NumberFormatInfo = fmt1;
            }
            else
            {
                dxf.FlagsInfo.UserDefinedNumberFormat = true;
                XlsDxfNumUser user1 = new XlsDxfNumUser();
                user1.NumberFormatCode = formatCode;
                dxf.NumberFormatInfo = user1;
            }
        }

        private void SetFormulaValue(XlsContentFormula content, XlCell cell)
        {
            switch (cell.Value.Type)
            {
                case XlVariantValueType.None:
                    content.Value.IsBlankString = true;
                    content.AlwaysCalc = true;
                    return;

                case XlVariantValueType.Boolean:
                    content.Value.BooleanValue = cell.Value.BooleanValue;
                    return;

                case XlVariantValueType.Text:
                    if (string.IsNullOrEmpty(cell.Value.TextValue))
                    {
                        content.Value.IsBlankString = true;
                        return;
                    }
                    content.Value.IsString = true;
                    return;

                case XlVariantValueType.Numeric:
                case XlVariantValueType.DateTime:
                    content.Value.NumericValue = cell.Value.NumericValue;
                    return;
            }
        }

        private void SetupFormatRuns(XLUnicodeRichExtendedString item, XlRichTextString text)
        {
            int num = 0;
            foreach (XlRichTextRun run in text.Runs)
            {
                XlsFormatRun run2 = new XlsFormatRun {
                    CharIndex = num,
                    FontIndex = run.FontIndex
                };
                if (!run2.IsDefault())
                {
                    item.FormatRuns.Add(run2);
                }
                num += run.Text.Length;
            }
        }

        public void SetWorksheetPosition(string name, int position)
        {
            Guard.ArgumentIsInRange<XlsTableBasedDocumentSheet>(this.sheets, position, "position");
            int index = this.sheets.FindIndex(x => string.Equals(x.Name, name, StringComparison.OrdinalIgnoreCase));
            if (index == -1)
            {
                throw new ArgumentException("Can't find worksheet with specified name!");
            }
            XlsTableBasedDocumentSheet item = this.sheets[index];
            this.sheets.RemoveAt(index);
            this.sheets.Insert(position, item);
        }

        private bool ShouldExportShapes()
        {
            bool flag;
            using (List<XlsTableBasedDocumentSheet>.Enumerator enumerator = this.sheets.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlsTableBasedDocumentSheet current = enumerator.Current;
                        if (!this.ShouldExportShapes(current))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private bool ShouldExportShapes(XlsTableBasedDocumentSheet sheet)
        {
            bool flag;
            using (List<XlDrawingObjectBase>.Enumerator enumerator = sheet.DrawingObjects.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlDrawingObjectBase current = enumerator.Current;
                        if (current.DrawingObjectType != XlDrawingObjectType.Shape)
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private bool ShouldRemove(XlConditionalFormatting cf) => 
            (this.GetRuleCount(cf) != 0) ? (this.GetBoundRange(cf.Ranges) == null) : true;

        private bool ShouldWriteAutoFilter12(XlFilterColumn column)
        {
            if (column.FilterCriteria == null)
            {
                return false;
            }
            XlFilterType filterType = column.FilterCriteria.FilterType;
            if ((filterType == XlFilterType.Color) || ((filterType == XlFilterType.Icon) || (filterType == XlFilterType.Dynamic)))
            {
                return true;
            }
            if (filterType != XlFilterType.Values)
            {
                return false;
            }
            XlValuesFilter filterCriteria = (XlValuesFilter) column.FilterCriteria;
            return ((!filterCriteria.FilterByBlank || (filterCriteria.Values.Count <= 1)) ? ((filterCriteria.Values.Count > 2) || ((filterCriteria.DateGroups.Count > 0) || column.HiddenButton)) : true);
        }

        private bool ShouldWriteTableAutoFilter12(XlTableColumn column)
        {
            if (column.FilterCriteria == null)
            {
                return false;
            }
            XlFilterType filterType = column.FilterCriteria.FilterType;
            if ((filterType == XlFilterType.Color) || ((filterType == XlFilterType.Icon) || (filterType == XlFilterType.Dynamic)))
            {
                return true;
            }
            if (filterType != XlFilterType.Values)
            {
                return false;
            }
            XlValuesFilter filterCriteria = (XlValuesFilter) column.FilterCriteria;
            return ((!filterCriteria.FilterByBlank || (filterCriteria.Values.Count <= 1)) ? ((filterCriteria.Values.Count > 2) || (filterCriteria.DateGroups.Count > 0)) : true);
        }

        public void SkipCells(int count)
        {
            Guard.ArgumentPositive(count, "count");
            if (this.currentCell != null)
            {
                throw new InvalidOperationException("Operation cannot be executed inside BeginCell/EndCell scope.");
            }
            if ((this.currentColumnIndex + count) >= this.options.MaxColumnCount)
            {
                throw new ArgumentOutOfRangeException($"Cell column index goes beyond range 0..{this.options.MaxColumnCount - 1}.");
            }
            int num = this.currentColumnIndex + count;
            XlCellRange range = XlCellRange.FromLTRB(this.currentColumnIndex, this.CurrentRowIndex, num - 1, this.CurrentRowIndex);
            this.CreateTableCells(range);
            this.currentColumnIndex = num;
        }

        public void SkipColumns(int count)
        {
            Guard.ArgumentPositive(count, "count");
            if (this.currentColumn != null)
            {
                throw new InvalidOperationException("Operation cannot be executed inside BeginColumn/EndColumn scope.");
            }
            int num = this.currentSheet.ColumnIndex + count;
            if (num >= this.options.MaxColumnCount)
            {
                throw new ArgumentOutOfRangeException($"Column index goes beyond range 0..{this.options.MaxColumnCount - 1}.");
            }
            if (this.CurrentOutlineLevel <= 0)
            {
                this.currentSheet.ColumnIndex = num;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    this.BeginColumn();
                    this.EndColumn();
                }
            }
        }

        public void SkipRows(int count)
        {
            Guard.ArgumentPositive(count, "count");
            if (this.currentRow != null)
            {
                throw new InvalidOperationException("Operation cannot be executed inside BeginRow/EndRow scope.");
            }
            int num = this.currentRowIndex + count;
            if (num >= this.options.MaxRowCount)
            {
                throw new ArgumentOutOfRangeException($"Row index goes beyond range 0..{this.options.MaxRowCount - 1}.");
            }
            if ((this.CurrentOutlineLevel <= 0) && !this.currentSheet.HasActiveTables)
            {
                this.currentRowIndex = num;
            }
            else
            {
                for (int i = 0; i < count; i++)
                {
                    this.BeginRow();
                    this.EndRow();
                }
            }
        }

        protected bool UseFeature12(XlTable table)
        {
            bool flag;
            if (!table.HasHeaderRow)
            {
                return true;
            }
            using (IEnumerator<XlTableColumn> enumerator = table.InnerColumns.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        XlTableColumn current = enumerator.Current;
                        if ((current.TotalRowFunction != XlTotalRowFunction.None) || (string.IsNullOrEmpty(current.TotalRowLabel) || (current.TotalRowLabel == "Total")))
                        {
                            continue;
                        }
                        flag = true;
                    }
                    else
                    {
                        return false;
                    }
                    break;
                }
            }
            return flag;
        }

        private void ValidateWorkbookView()
        {
            this.currentDocument.View.Validate(this.sheets.Cast<IXlSheet>().ToList<IXlSheet>());
        }

        private void WriteAutoFilter()
        {
            XlCellRange autoFilterRange = this.currentSheet.AutoFilterRange;
            if (autoFilterRange != null)
            {
                int num = autoFilterRange.TopLeft.Column;
                if (num < 0x100)
                {
                    int columnCount = (Math.Min(autoFilterRange.BottomRight.Column, 0xff) - num) + 1;
                    XlFilterColumnsChecker.Check(this.currentSheet.AutoFilterColumns, columnCount);
                    if (this.currentSheet.AutoFilterColumns.Count > 0)
                    {
                        this.WriteContent(0x9b, this.contentEmpty);
                    }
                    this.WriteContent(0x9d, (short) columnCount);
                    foreach (XlFilterColumn column in this.currentSheet.AutoFilterColumns)
                    {
                        if (this.ShouldWriteAutoFilter12(column))
                        {
                            this.WriteAutoFilterColumn12(column, autoFilterRange);
                            continue;
                        }
                        this.WriteAutoFilterColumn(column);
                    }
                }
            }
        }

        private void WriteAutoFilter12Criteria(IXlFilterCriteria filterCriteria, XlCellRange range)
        {
            XlFilterType filterType = filterCriteria.FilterType;
            if (filterType == XlFilterType.Dynamic)
            {
                XlDynamicFilter filter = (XlDynamicFilter) filterCriteria;
                if (filter.DynamicFilterValueRequired() && filter.Value.IsNumeric)
                {
                    XlsContentAutoFilter12Criteria content = new XlsContentAutoFilter12Criteria {
                        BoundRange = XlsRef8.FromRange(range),
                        Criteria = { 
                            FilterOperator = filter.GetFilterOperator(false),
                            Value = filter.Value
                        }
                    };
                    this.WriteContent(0x87f, content);
                }
                if (filter.DynamicFilterMaxValueRequired() && filter.MaxValue.IsNumeric)
                {
                    XlsContentAutoFilter12Criteria content = new XlsContentAutoFilter12Criteria {
                        BoundRange = XlsRef8.FromRange(range),
                        Criteria = { 
                            FilterOperator = filter.GetFilterOperator(true),
                            Value = filter.MaxValue
                        }
                    };
                    this.WriteContent(0x87f, content);
                }
            }
            else if (filterType == XlFilterType.Values)
            {
                XlValuesFilter filter2 = (XlValuesFilter) filterCriteria;
                foreach (XlVariantValue value3 in filter2.Values)
                {
                    XlsContentAutoFilter12Criteria content = new XlsContentAutoFilter12Criteria {
                        BoundRange = XlsRef8.FromRange(range),
                        Criteria = { 
                            FilterOperator = XlFilterOperator.Equal,
                            Value = value3
                        }
                    };
                    this.WriteContent(0x87f, content);
                }
                if (filter2.FilterByBlank)
                {
                    XlsContentAutoFilter12Criteria content = new XlsContentAutoFilter12Criteria {
                        BoundRange = XlsRef8.FromRange(range),
                        Criteria = { AllBlanksMatched = true }
                    };
                    this.WriteContent(0x87f, content);
                }
            }
        }

        private void WriteAutoFilter12DateGroup(IXlFilterCriteria filterCriteria, XlCellRange range)
        {
            XlValuesFilter filter = filterCriteria as XlValuesFilter;
            if (filter != null)
            {
                foreach (XlDateGroupItem item in filter.DateGroups)
                {
                    XlsContentAutoFilter12DateGroup content = new XlsContentAutoFilter12DateGroup {
                        BoundRange = XlsRef8.FromRange(range),
                        DateGroup = item
                    };
                    this.WriteContent(0x87f, content);
                }
            }
        }

        private void WriteAutoFilterColumn(XlFilterColumn column)
        {
            if (column.FilterCriteria != null)
            {
                XlsContentAutoFilter content = new XlsContentAutoFilter {
                    ColumnId = column.ColumnId
                };
                this.PrepareAutoFilterContent(content, column.FilterCriteria);
                this.WriteContent(0x9e, content);
            }
        }

        private void WriteAutoFilterColumn12(XlFilterColumn column, XlCellRange range)
        {
            this.WriteBlankAutoFilterColumn(column);
            XlsContentAutoFilter12 content = new XlsContentAutoFilter12 {
                ColumnId = column.ColumnId,
                BoundRange = XlsRef8.FromRange(range),
                HiddenButton = column.HiddenButton
            };
            this.PrepareAutoFilter12Content(content, column.FilterCriteria);
            this.WriteContent(0x87e, content);
            this.WriteAutoFilter12Criteria(column.FilterCriteria, range);
            this.WriteAutoFilter12DateGroup(column.FilterCriteria, range);
        }

        private void WriteBlankAutoFilterColumn(XlFilterColumn column)
        {
            XlsContentAutoFilter content = new XlsContentAutoFilter {
                ColumnId = column.ColumnId,
                Join = true,
                Second = { IsBlank = true }
            };
            this.WriteContent(0x9e, content);
        }

        private void WriteBlankCells()
        {
            IXlCell cell = this.cellsToExport[0];
            int count = this.cellsToExport.Count;
            if (count <= 1)
            {
                XlsContentBlank content = new XlsContentBlank();
                this.InitializeContent(content, cell);
                this.WriteContent(0x201, content);
            }
            else
            {
                XlsContentMulBlank content = new XlsContentMulBlank {
                    RowIndex = cell.RowIndex,
                    FirstColumnIndex = cell.ColumnIndex
                };
                for (int i = 0; i < count; i++)
                {
                    content.FormatIndices.Add(this.GetFormatIndex(this.cellsToExport[i].Formatting));
                }
                this.WriteContent(190, content);
            }
        }

        protected void WriteBOF(XlsSubstreamType substreamType)
        {
            this.contentBOF.SubstreamType = substreamType;
            this.contentBOF.FileHistoryFlags = XlsFileHistory.Default;
            this.WriteContent(0x809, this.contentBOF);
        }

        private void WriteBooleanCell()
        {
            IXlCell cell = this.cellsToExport[0];
            XlsContentBoolErr content = new XlsContentBoolErr();
            this.InitializeContent(content, cell);
            content.Value = cell.Value.BooleanValue ? ((byte) 1) : ((byte) 0);
            content.IsError = false;
            this.WriteContent(0x205, content);
        }

        private void WriteBoundSheetStartPosition()
        {
            long offset = this.boundPositions.Dequeue();
            long position = this.writer.BaseStream.Position;
            this.writer.BaseStream.Seek(offset, SeekOrigin.Begin);
            this.writer.Write((uint) position);
            this.writer.BaseStream.Seek(position, SeekOrigin.Begin);
        }

        private void WriteBuiltInStyle(int builtInId, string styleName, int styleFormatId)
        {
            XlsContentStyle content = new XlsContentStyle {
                BuiltInId = builtInId,
                IsBuiltIn = true,
                IsHidden = false,
                OutlineLevel = 0,
                StyleFormatId = styleFormatId,
                StyleName = styleName
            };
            this.WriteContent(0x293, content);
        }

        protected void WriteBundleSheet()
        {
            XlsContentBoundSheet8 content = new XlsContentBoundSheet8();
            foreach (XlsTableBasedDocumentSheet sheet2 in this.sheets)
            {
                this.boundPositions.Enqueue(this.writer.BaseStream.Position + 4L);
                content.Name = sheet2.Name;
                content.VisibleState = sheet2.VisibleState;
                this.WriteContent(0x85, content);
            }
        }

        private void WriteCellTable()
        {
            if (this.currentSheet.Dimensions != null)
            {
                this.writer.Flush();
                if (this.currentSheet.CellTableWriter != null)
                {
                    this.currentSheet.CellTableWriter.Flush();
                    Stream baseStream = this.writer.BaseStream;
                    Stream stream = this.currentSheet.CellTableWriter.BaseStream;
                    this.cellTablePosition = baseStream.Position;
                    this.workbookStream.Attach(stream);
                    baseStream.Seek(0L, SeekOrigin.End);
                }
            }
        }

        private void WriteCFEx(int id, XlConditionalFormatting cf, XlCondFmtRule rule, bool isCF12, int cfIndex)
        {
            XlsContentCFEx content = new XlsContentCFEx {
                Id = id,
                IsCF12 = isCF12,
                Range = this.GetBoundRange(cf.Ranges)
            };
            if (!isCF12)
            {
                content.CFIndex = cfIndex;
                this.PrepareDxf(content.Format, rule as XlCondFmtRuleWithFormatting);
                content.HasFormat = content.Format.ExtProperties.Count > 0;
                content.IsActive = true;
                content.Priority = rule.Priority;
                content.StopIfTrue = rule.StopIfTrue;
                IXlsCFContentAdapter adapter = new XlsContentCFExAdapter(content);
                this.PrepareContent(adapter, rule, cf);
            }
            this.WriteContent(0x87b, content);
        }

        protected void WriteCodePage()
        {
            XlsContentEncoding content = new XlsContentEncoding {
                Value = Encoding.Unicode
            };
            this.WriteContent(0x42, content);
        }

        private void WriteColumns()
        {
            List<XlsTableColumn> columns = this.currentSheet.Columns;
            if (columns.Count != 0)
            {
                columns.Sort(new Comparison<XlsTableColumn>(this.CompareColumns));
                XlsContentColumnInfo content = new XlsContentColumnInfo();
                foreach (XlsTableColumn column in columns)
                {
                    content.FirstColumn = column.ColumnIndex;
                    content.LastColumn = column.ColumnIndex;
                    content.Collapsed = column.IsCollapsed;
                    content.Hidden = column.IsHidden;
                    content.OutlineLevel = column.OutlineLevel;
                    if (column.WidthInPixels >= 0)
                    {
                        content.ColumnWidth = (int) (ColumnWidthConverter.PixelsToCharactersWidth((float) column.WidthInPixels, this.currentSheet.DefaultMaxDigitWidthInPixels) * 256f);
                        content.CustomWidth = true;
                    }
                    else
                    {
                        content.ColumnWidth = (int) (ColumnWidthConverter.PixelsToCharactersWidth(this.currentSheet.DefaultColumnWidthInPixels, this.currentSheet.DefaultMaxDigitWidthInPixels) * 256f);
                        content.CustomWidth = false;
                    }
                    content.FormatIndex = column.FormatIndex;
                    this.WriteContent(0x7d, content);
                }
            }
        }

        private void WriteCondFmtRule(XlCondFmtRule rule, XlConditionalFormatting cf)
        {
            if (this.cfCount < 3)
            {
                XlCondFmtRuleWithFormatting formatting = rule as XlCondFmtRuleWithFormatting;
                if (!this.IsCondFmt12(cf, formatting))
                {
                    XlsContentCF content = new XlsContentCF();
                    this.PrepareDxf(content.Format, formatting);
                    IXlsCFContentAdapter adapter = new XlsContentCFAdapter(content);
                    this.PrepareContent(adapter, rule, cf);
                    this.WriteContent(0x1b1, content);
                    this.cfCount++;
                }
            }
        }

        private void WriteCondFmtRule12(XlCondFmtRule rule, XlConditionalFormatting cf)
        {
            XlsContentCF12 content = new XlsContentCF12 {
                Priority = rule.Priority,
                StopIfTrue = rule.StopIfTrue
            };
            XlCondFmtRuleWithFormatting formatting = rule as XlCondFmtRuleWithFormatting;
            this.PrepareDxf(content.Format, formatting);
            XlCondFmtType ruleType = rule.RuleType;
            if (ruleType == XlCondFmtType.ColorScale)
            {
                this.PrepareRuleColorScale(content, rule as XlCondFmtRuleColorScale, cf);
            }
            else if (ruleType == XlCondFmtType.DataBar)
            {
                this.PrepareRuleDataBar(content, rule as XlCondFmtRuleDataBar, cf);
            }
            else if (ruleType != XlCondFmtType.IconSet)
            {
                IXlsCFContentAdapter adapter = new XlsContentCF12Adapter(content, this.HasFormulaInCF12(formatting));
                this.PrepareContent(adapter, rule, cf);
            }
            else if (!this.PrepareRuleIconSet(content, rule as XlCondFmtRuleIconSet, cf))
            {
                return;
            }
            this.WriteContent(0x87a, content);
        }

        private void WriteConditionalFormatting(int condFmtId, XlConditionalFormatting cf)
        {
            bool flag = this.IsCondFmt12(cf);
            XlsContentCondFmt content = this.CreateCFContentRoot(flag);
            content.Id = condFmtId;
            content.BoundRange = this.GetBoundRange(cf.Ranges);
            int ruleCount = this.GetRuleCount(cf);
            if (!flag)
            {
                ruleCount = Math.Min(3, ruleCount);
            }
            content.RecordCount = ruleCount;
            content.ToughRecalc = true;
            int num2 = 0;
            foreach (XlCellRange range in cf.Ranges)
            {
                XlsRef8 item = XlsRef8.FromRange(range);
                if (item != null)
                {
                    if (num2 >= 0x201)
                    {
                        break;
                    }
                    content.Ranges.Add(item);
                    num2++;
                }
            }
            if (flag)
            {
                this.WriteContent(0x879, content);
                foreach (XlCondFmtRule rule in cf.Rules)
                {
                    this.WriteCondFmtRule12(rule, cf);
                }
            }
            else
            {
                this.WriteContent(0x1b0, content);
                this.cfCount = 0;
                foreach (XlCondFmtRule rule2 in cf.Rules)
                {
                    this.WriteCondFmtRule(rule2, cf);
                }
            }
        }

        private void WriteConditionalFormattingExt(int condFmtId, XlConditionalFormatting cf)
        {
            if (!this.IsCondFmt12(cf))
            {
                this.cfCount = 0;
                int count = cf.Rules.Count;
                for (int i = 0; i < count; i++)
                {
                    XlCondFmtRule rule = cf.Rules[i];
                    if (!this.IsCondFmt12(cf, rule as XlCondFmtRuleWithFormatting) && (this.cfCount < 3))
                    {
                        this.WriteCFEx(condFmtId, cf, rule, false, this.cfCount);
                        this.cfCount++;
                    }
                    else
                    {
                        this.WriteCFEx(condFmtId, cf, rule, true, 0);
                        this.WriteCondFmtRule12(rule, cf);
                    }
                }
            }
        }

        protected void WriteConditionalFormattings()
        {
            this.RemoveIncompatibleFormattings();
            IList<XlConditionalFormatting> conditionalFormattings = this.currentSheet.ConditionalFormattings;
            int count = conditionalFormattings.Count;
            for (int i = 0; i < count; i++)
            {
                this.WriteConditionalFormatting(i + 1, conditionalFormattings[i]);
            }
            for (int j = 0; j < count; j++)
            {
                this.WriteConditionalFormattingExt(j + 1, conditionalFormattings[j]);
            }
        }

        protected void WriteContent(short recordType, IXlsContent content)
        {
            if (this.writer != null)
            {
                if (content.RecordHeader != null)
                {
                    content.RecordHeader.RecordTypeId = recordType;
                }
                this.writer.Write(recordType);
                this.writer.Write((short) content.GetSize());
                content.Write(this.writer);
            }
        }

        protected void WriteContent(short recordType, bool content)
        {
            this.contentBoolValue.Value = content;
            this.WriteContent(recordType, this.contentBoolValue);
        }

        protected void WriteContent(short recordType, double content)
        {
            this.contentDoubleValue.Value = content;
            this.WriteContent(recordType, this.contentDoubleValue);
        }

        protected void WriteContent(short recordType, short content)
        {
            this.contentShortValue.Value = content;
            this.WriteContent(recordType, this.contentShortValue);
        }

        protected void WriteContent(short recordType, string content)
        {
            this.contentStringValue.Value = content;
            this.WriteContent(recordType, this.contentStringValue);
        }

        protected void WriteCountry()
        {
            XlsContentCountry content = new XlsContentCountry {
                DefaultCountryIndex = XlsCountryCodes.GetCountryCode(CultureInfo.CurrentUICulture),
                CountryIndex = XlsCountryCodes.GetCountryCode(CultureInfo.CurrentCulture)
            };
            this.WriteContent(140, content);
        }

        private void WriteCustomPalette()
        {
            if (!this.palette.IsDefault)
            {
                XlsContentPalette content = new XlsContentPalette(this.palette);
                this.WriteContent(0x92, content);
            }
        }

        private bool WriteDataValidation(XlDataValidation validation)
        {
            XlsContentDv content = new XlsContentDv();
            foreach (XlCellRange range in validation.Ranges)
            {
                XlsRef8 item = XlsRef8.FromRange(range);
                if (item != null)
                {
                    content.Ranges.Add(item);
                }
            }
            if (content.Ranges.Count == 0)
            {
                return false;
            }
            content.ValidationType = validation.Type;
            content.ErrorStyle = validation.ErrorStyle;
            content.AllowBlank = validation.AllowBlank;
            content.SuppressCombo = (validation.Type != XlDataValidationType.List) || !validation.ShowDropDown;
            content.ImeMode = validation.ImeMode;
            content.ShowInputMessage = validation.ShowInputMessage;
            content.ShowErrorMessage = validation.ShowErrorMessage;
            content.ValidationOperator = validation.Operator;
            content.PromptTitle = this.CheckLength(validation.PromptTitle, 0x20);
            content.ErrorTitle = this.CheckLength(validation.ErrorTitle, 0x20);
            content.Prompt = this.CheckLength(validation.InputPrompt, 0xff);
            content.Error = this.CheckLength(validation.ErrorMessage, 0xe1);
            if (validation.Type == XlDataValidationType.List)
            {
                if (validation.ListRange != null)
                {
                    content.Formula1Bytes = this.GetListRangeFormulaBytes(validation.ListRange);
                }
                else
                {
                    content.Formula1Bytes = this.GetFormulaBytes(validation.ListValues);
                    content.StringLookup = true;
                }
            }
            else
            {
                this.expressionContext.CurrentCell = this.GetTopLeftCell(validation.Ranges);
                if (!validation.Criteria1.IsEmpty)
                {
                    content.Formula1Bytes = this.GetFormulaBytes(validation.Criteria1);
                }
                if (!validation.Criteria2.IsEmpty)
                {
                    content.Formula2Bytes = this.GetFormulaBytes(validation.Criteria2);
                }
            }
            this.WriteContent(0x1be, content);
            return true;
        }

        protected void WriteDataValidations()
        {
            IList<XlDataValidation> dataValidations = this.currentSheet.DataValidations;
            int dataValidationsRecordCount = this.GetDataValidationsRecordCount(dataValidations);
            if (dataValidationsRecordCount != 0)
            {
                XlsContentDVal content = new XlsContentDVal {
                    RecordCount = dataValidationsRecordCount
                };
                this.WriteContent(0x1b2, content);
                int num2 = 0;
                foreach (XlDataValidation validation in dataValidations)
                {
                    if (this.WriteDataValidation(validation) && ((num2 + 1) > 0xfffe))
                    {
                        break;
                    }
                }
            }
        }

        private void WriteDbCell()
        {
            long position = this.writer.BaseStream.Position;
            this.currentSheet.DbCellsPositions.Add(position);
            this.dbCellCalculator.RegisterDbCellPosition(position);
            XlsContentDbCell content = new XlsContentDbCell {
                FirstRowOffset = this.dbCellCalculator.CalculateFirstRowOffset()
            };
            content.StreamOffsets.AddRange(this.dbCellCalculator.CalculateStreamOffsets());
            this.WriteContent(0xd7, content);
        }

        private void WriteDefaultRowHeight()
        {
            XlsContentDefaultRowHeight content = new XlsContentDefaultRowHeight {
                DefaultRowHeightInTwips = 300
            };
            this.WriteContent(0x225, content);
        }

        protected void WriteDefinedNames()
        {
            foreach (XlsContentDefinedName name in this.definedNames)
            {
                this.WriteContent(0x18, name);
            }
        }

        private void WriteDimensions()
        {
            XlsContentDimensions content = new XlsContentDimensions();
            XlDimensions dimensions = this.currentSheet.Dimensions;
            if (dimensions != null)
            {
                content.FirstRowIndex = dimensions.FirstRowIndex + 1;
                content.LastRowIndex = dimensions.LastRowIndex + 1;
                content.FirstColumnIndex = dimensions.FirstColumnIndex + 1;
                content.LastColumnIndex = dimensions.LastColumnIndex + 1;
            }
            else
            {
                content.FirstRowIndex = 1;
                content.LastRowIndex = 0;
                content.FirstColumnIndex = 1;
                content.LastColumnIndex = 0;
            }
            this.WriteContent(0x200, content);
        }

        private void WriteDrawingGroup()
        {
            if ((this.drawingGroup.Count != 0) || this.ShouldExportShapes())
            {
                XlsChunk firstChunk = new XlsChunk(0xeb);
                using (XlsChunkWriter writer = new XlsChunkWriter(this.writer, firstChunk, new XlsChunk(60)))
                {
                    this.CreateMsoDrawingGroup().Write(writer);
                }
            }
        }

        private void WriteDrawingObjects()
        {
            if (this.currentSheet.DrawingObjects.Count != 0)
            {
                this.CalculateDrawingAnchorPoints();
                XlsChunk firstChunk = new XlsChunk(0xec);
                using (XlsChunkWriter writer = new XlsChunkWriter(this.writer, firstChunk, new XlsChunk(60)))
                {
                    this.CreateMsoDrawing().Write(writer);
                }
            }
        }

        protected void WriteDSF()
        {
            this.WriteContent(0x161, (short) 0);
        }

        protected void WriteEOF()
        {
            this.WriteContent(10, this.contentEmpty);
        }

        private void WriteErrorCell()
        {
            IXlCell cell = this.cellsToExport[0];
            XlsContentBoolErr content = new XlsContentBoolErr();
            this.InitializeContent(content, cell);
            content.Value = (byte) cell.Value.ErrorValue.Type;
            content.IsError = true;
            this.WriteContent(0x205, content);
        }

        private void WriteExternalNames()
        {
            if (this.externalNames.Count != 0)
            {
                XlsContentAddInExternName content = new XlsContentAddInExternName();
                foreach (string str in this.externalNames.Keys)
                {
                    content.Name = str;
                    this.WriteContent(0x23, content);
                }
            }
        }

        private void WriteExternSheet()
        {
            XlsChunk firstChunk = new XlsChunk(0x17);
            using (XlsChunkWriter writer = new XlsChunkWriter(this.writer, firstChunk, new XlsChunk(60)))
            {
                writer.Write((ushort) this.sheetDefinitions.Count);
                XlsXTI sxti = new XlsXTI();
                int num2 = this.HasAddInUDFDefinition() ? 1 : 0;
                foreach (string str in this.sheetDefinitions.Keys)
                {
                    if (str == "*AddInUDF")
                    {
                        sxti.SupBookIndex = 0;
                        sxti.FirstSheetIndex = -2;
                        sxti.LastSheetIndex = -2;
                        sxti.Write(writer);
                        continue;
                    }
                    int num3 = this.IndexOfSheet(str);
                    sxti.SupBookIndex = num2;
                    sxti.FirstSheetIndex = num3;
                    sxti.LastSheetIndex = num3;
                    sxti.Write(writer);
                }
            }
        }

        protected void WriteExtSST()
        {
            XlsContentExtSST content = new XlsContentExtSST {
                StringsInBucket = this.stringsInBucket
            };
            content.Items.AddRange(this.extendedSSTItems);
            this.WriteContent(0xff, content);
        }

        private void WriteFeat11(XlTable table)
        {
            XlsChunk firstChunk = new XlsChunk(this.UseFeature12(table) ? ((short) 0x878) : ((short) 0x872));
            using (XlsChunkWriter writer = new XlsChunkWriter(this.writer, firstChunk, new XlsChunkFrtHeader(0x875)))
            {
                this.WriteTableHeader(writer, table);
                this.WriteTableFeatureType(writer, table);
                for (int i = 0; i < table.Columns.Count; i++)
                {
                    this.WriteTableFieldDataItem(writer, table, i);
                }
            }
        }

        private void WriteFeatHdr11()
        {
            XlsContentFeatHdr11 content = new XlsContentFeatHdr11 {
                NextId = this.GetTablesCount() + 1
            };
            this.WriteContent(0x871, content);
        }

        private void WriteFeatures()
        {
            if ((this.currentSheet.IgnoreErrors != DevExpress.Export.Xl.XlIgnoreErrors.None) && (this.currentSheet.DataRange != null))
            {
                this.WriteIgnoredErrorsHeader();
                this.WriteIngoredErrors();
            }
        }

        protected void WriteFilePass()
        {
            if ((this.encryptionHeader != null) && (this.writer != null))
            {
                this.writer.Write((short) 0x2f);
                this.writer.Write((short) (this.encryptionHeader.GetSize() + 2));
                this.writer.Write((short) 1);
                this.encryptionHeader.Write(this.writer);
            }
        }

        protected void WriteFonts()
        {
            XlsContentFont content = new XlsContentFont();
            foreach (XlFont font2 in this.fontTable.Keys)
            {
                content.Size = font2.Size;
                content.Bold = font2.Bold;
                content.Italic = font2.Italic;
                content.StrikeThrough = font2.StrikeThrough;
                content.Outline = font2.Outline;
                content.Shadow = font2.Shadow;
                content.Condense = font2.Condense;
                content.Extend = font2.Extend;
                content.Script = font2.Script;
                content.Underline = font2.Underline;
                content.FontFamily = (int) font2.FontFamily;
                content.Charset = font2.Charset;
                content.FontName = font2.Name;
                content.ColorIndex = this.palette.GetFontColorIndex(font2.Color, this.currentDocument.Theme);
                this.WriteContent(0x31, content);
            }
        }

        private void WriteFormulaCell()
        {
            XlCell cell = this.cellsToExport[0];
            XlsContentFormula content = new XlsContentFormula();
            this.InitializeContent(content, cell);
            this.SetFormulaValue(content, cell);
            if (cell.SharedFormulaPosition.IsValid)
            {
                if (!this.sharedFormulaHostCells.Contains(cell.SharedFormulaPosition))
                {
                    throw new AggregateException($"Position {cell.SharedFormulaPosition.ToString()} refers to non existing shared formula");
                }
                content.PartOfSharedFormula = true;
                content.FormulaBytes = this.GetSharedFormulaRefBytes(cell.SharedFormulaPosition);
            }
            else if (cell.SharedFormulaRange == null)
            {
                content.FormulaBytes = this.GetFormulaBytes(cell);
            }
            else
            {
                XlCellPosition item = new XlCellPosition(cell.ColumnIndex, cell.RowIndex);
                this.sharedFormulaHostCells.Add(item);
                content.PartOfSharedFormula = true;
                content.FormulaBytes = this.GetSharedFormulaRefBytes(item);
            }
            this.WriteContent(6, content);
            this.WriteSharedFormula(cell);
            if (content.Value.IsString)
            {
                this.WriteStringFormulaValue(cell);
            }
        }

        private void WriteGuts()
        {
            int num = 0;
            foreach (XlsTableColumn column in this.currentSheet.Columns)
            {
                num = Math.Max(num, column.OutlineLevel);
            }
            XlsContentGuts content = new XlsContentGuts {
                RowGutterMaxOutlineLevel = Math.Max(0, Math.Min(7, this.maxRowOutlineLevel - 1)),
                ColumnGutterMaxOutlineLevel = num
            };
            this.WriteContent(0x80, content);
        }

        private void WriteHeaderFooter()
        {
            XlHeaderFooter headerFooter = this.currentSheet.HeaderFooter;
            XlsContentHeaderFooter content = new XlsContentHeaderFooter {
                AlignWithMargins = headerFooter.AlignWithMargins,
                DifferentFirst = headerFooter.DifferentFirst,
                DifferentOddEven = headerFooter.DifferentOddEven,
                ScaleWithDoc = headerFooter.ScaleWithDoc,
                EvenFooter = headerFooter.EvenFooter,
                EvenHeader = headerFooter.EvenHeader,
                FirstFooter = headerFooter.FirstFooter,
                FirstHeader = headerFooter.FirstHeader
            };
            this.WriteContent(0x89c, content);
        }

        private void WriteHyperlink(XlHyperlink hyperlink)
        {
            if ((hyperlink.Reference != null) && !string.IsNullOrEmpty(hyperlink.TargetUri))
            {
                XlsRef8 range = XlsRef8.FromRange(hyperlink.Reference);
                if (range != null)
                {
                    XlsContentHyperlink content = new XlsContentHyperlink {
                        Range = range
                    };
                    string targetUri = hyperlink.TargetUri;
                    string str2 = string.Empty;
                    int index = targetUri.IndexOf('#');
                    if (index != -1)
                    {
                        str2 = targetUri.Substring(index + 1);
                        targetUri = targetUri.Substring(0, index);
                    }
                    if (!string.IsNullOrEmpty(targetUri))
                    {
                        Uri uri;
                        if (!Uri.TryCreate(targetUri, UriKind.RelativeOrAbsolute, out uri))
                        {
                            return;
                        }
                        content.HasMoniker = true;
                        content.IsAbsolute = uri.IsAbsoluteUri;
                        if (!uri.IsAbsoluteUri || (uri.Scheme == Uri.UriSchemeFile))
                        {
                            BinaryHyperlinkFileMoniker moniker2 = new BinaryHyperlinkFileMoniker {
                                Path = targetUri
                            };
                            content.OleMoniker = moniker2;
                        }
                        else
                        {
                            BinaryHyperlinkURLMoniker moniker = new BinaryHyperlinkURLMoniker {
                                Url = targetUri,
                                HasOptionalData = true,
                                AllowImplicitFileScheme = true,
                                AllowRelative = true,
                                Canonicalize = true,
                                CrackUnknownSchemes = true,
                                DecodeExtraInfo = true,
                                IESettings = true,
                                NoEncodeForbiddenChars = true,
                                PreProcessHtmlUri = true
                            };
                            content.OleMoniker = moniker;
                        }
                    }
                    if (!string.IsNullOrEmpty(str2))
                    {
                        content.Location = str2;
                        content.HasLocationString = true;
                    }
                    if (!string.IsNullOrEmpty(hyperlink.DisplayText))
                    {
                        content.DisplayName = hyperlink.DisplayText;
                        content.HasDisplayName = true;
                        content.SiteGaveDisplayName = true;
                    }
                    this.WriteContent(440, content);
                    if (!string.IsNullOrEmpty(hyperlink.Tooltip))
                    {
                        this.WriteHyperlinkTooltip(hyperlink, range);
                    }
                }
            }
        }

        private void WriteHyperlinks()
        {
            int count = this.currentSheet.Hyperlinks.Count;
            for (int i = 0; i < count; i++)
            {
                this.WriteHyperlink(this.currentSheet.Hyperlinks[i]);
            }
        }

        private void WriteHyperlinkTooltip(XlHyperlink hyperlink, XlsRef8 range)
        {
            XlsContentHyperlinkTooltip content = new XlsContentHyperlinkTooltip {
                Range = range,
                Tooltip = hyperlink.Tooltip
            };
            this.WriteContent(0x800, content);
        }

        private void WriteIgnoredErrorsHeader()
        {
            XlsContentFeatHdr content = new XlsContentFeatHdr {
                FeatureType = XlsFeatureType.IgnoredErrors
            };
            this.WriteContent(0x867, content);
        }

        private void WriteIndex()
        {
            this.indexRecordPosition = this.writer.BaseStream.Position;
            XlsContentIndex content = new XlsContentIndex();
            content.DbCellsPositions.AddRange(this.currentSheet.DbCellsPositions);
            this.WriteContent(0x20b, content);
        }

        private void WriteIngoredErrors()
        {
            XlsContentFeatIgnoredErrors content = new XlsContentFeatIgnoredErrors {
                Refs = { XlsRef8.FromRange(this.currentSheet.DataRange) },
                CalculationErrors = (this.currentSheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.EvaluationError) != DevExpress.Export.Xl.XlIgnoreErrors.None,
                DataValidation = (this.currentSheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.ListDataValidation) != DevExpress.Export.Xl.XlIgnoreErrors.None,
                EmptyCellRef = (this.currentSheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.EmptyCellReference) != DevExpress.Export.Xl.XlIgnoreErrors.None,
                InconsistFormula = (this.currentSheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.Formula) != DevExpress.Export.Xl.XlIgnoreErrors.None,
                InconsistRange = (this.currentSheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.FormulaRange) != DevExpress.Export.Xl.XlIgnoreErrors.None,
                NumberStoredAsText = (this.currentSheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.NumberStoredAsText) != DevExpress.Export.Xl.XlIgnoreErrors.None,
                TextDateInsuff = (this.currentSheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.TwoDigitTextYear) != DevExpress.Export.Xl.XlIgnoreErrors.None,
                UnprotectedFormula = (this.currentSheet.IgnoreErrors & DevExpress.Export.Xl.XlIgnoreErrors.UnlockedFormula) != DevExpress.Export.Xl.XlIgnoreErrors.None
            };
            this.WriteContent(0x868, content);
        }

        protected void WriteInterface()
        {
            XlsContentEncoding content = new XlsContentEncoding {
                Value = Encoding.Unicode
            };
            this.WriteContent(0xe1, content);
            this.WriteContent(0xc1, (short) 0);
            this.WriteContent(0xe2, this.contentEmpty);
        }

        private void WriteList12BlockLevel(XlTable table)
        {
            XlsContentList12BlockLevel content = new XlsContentList12BlockLevel {
                TableId = table.Id
            };
            this.PrepareDxf(content.Header, XlDifferentialFormatting.ExcludeBorderFormatting(table.HeaderRowFormatting));
            this.PrepareDxf(content.Data, table.DataFormatting);
            this.PrepareDxf(content.Total, XlDifferentialFormatting.ExcludeBorderFormatting(table.TotalRowFormatting));
            this.PrepareDxf(content.HeaderBorder, XlDifferentialFormatting.ExtractBorderFormatting(table.HeaderRowFormatting));
            this.PrepareDxf(content.Border, XlDifferentialFormatting.ExtractBorderFormatting(table.TableBorderFormatting));
            this.PrepareDxf(content.TotalBorder, XlDifferentialFormatting.ExtractBorderFormatting(table.TotalRowFormatting));
            this.WriteContent(0x877, content);
        }

        private void WriteList12DisplayName(XlTable table)
        {
            XlsContentList12DisplayName content = new XlsContentList12DisplayName {
                TableId = table.Id,
                DisplayName = table.Name,
                Comment = table.Comment
            };
            this.WriteContent(0x877, content);
        }

        private void WriteList12TableStyleInfo(XlTable table)
        {
            XlsContentList12TableStyleInfo content = new XlsContentList12TableStyleInfo {
                TableId = table.Id,
                StyleName = table.Style.Name,
                ColumnStripes = table.Style.ShowColumnStripes,
                FirstColumn = table.Style.ShowFirstColumn,
                LastColumn = table.Style.ShowLastColumn,
                RowStripes = table.Style.ShowRowStripes
            };
            this.WriteContent(0x877, content);
        }

        protected void WriteMergeCells()
        {
            IXlMergedCells mergedCells = this.currentSheet.MergedCells;
            if (mergedCells.Count != 0)
            {
                XlsContentMergeCells content = new XlsContentMergeCells();
                foreach (XlCellRange range in mergedCells)
                {
                    XlsRef8 item = XlsRef8.FromRange(range);
                    if (item != null)
                    {
                        content.MergeCells.Add(item);
                        if (content.MergeCells.Count >= 0x402)
                        {
                            this.WriteContent(0xe5, content);
                            content.MergeCells.Clear();
                        }
                    }
                }
                if (content.MergeCells.Count > 0)
                {
                    this.WriteContent(0xe5, content);
                }
            }
        }

        protected void WriteNumberFormats()
        {
            XlsContentNumberFormat content = new XlsContentNumberFormat();
            this.WriteNumberFormats(content, this.predefinedNumberFormats);
            this.WriteNumberFormats(content, this.numberFormatTable.Values);
        }

        private void WriteNumberFormats(XlsContentNumberFormat content, IEnumerable<ExcelNumberFormat> formats)
        {
            foreach (ExcelNumberFormat format in formats)
            {
                content.FormatId = format.Id;
                content.FormatCode = format.FormatString;
                this.WriteContent(0x41e, content);
            }
        }

        private void WriteNumericCells()
        {
            IXlCell cell = this.cellsToExport[0];
            int count = this.cellsToExport.Count;
            if (count <= 1)
            {
                if (XlsRkNumber.IsRkValue(cell.Value.NumericValue))
                {
                    XlsContentRk content = new XlsContentRk();
                    this.InitializeContent(content, cell);
                    content.Value = cell.Value.NumericValue;
                    this.WriteContent(0x27e, content);
                }
                else
                {
                    XlsContentNumber content = new XlsContentNumber();
                    this.InitializeContent(content, cell);
                    content.Value = cell.Value.NumericValue;
                    this.WriteContent(0x203, content);
                }
            }
            else
            {
                XlsContentMulRk content = new XlsContentMulRk {
                    RowIndex = cell.RowIndex,
                    FirstColumnIndex = cell.ColumnIndex
                };
                for (int i = 0; i < count; i++)
                {
                    cell = this.cellsToExport[i];
                    XlsRkRec item = new XlsRkRec {
                        FormatIndex = this.GetFormatIndex(cell.Formatting)
                    };
                    XlVariantValue value2 = cell.Value;
                    item.Rk.Value = value2.NumericValue;
                    content.RkRecords.Add(item);
                }
                this.WriteContent(0xbd, content);
            }
        }

        private void WritePageBreaks()
        {
            if (this.currentSheet.RowPageBreaks.Count > 0)
            {
                XlsContentRowPageBreaks content = new XlsContentRowPageBreaks(this.currentSheet.RowPageBreaks);
                this.WriteContent(0x1b, content);
            }
            if (this.currentSheet.ColumnPageBreaks.Count > 0)
            {
                XlsContentColumnPageBreaks content = new XlsContentColumnPageBreaks(this.currentSheet.ColumnPageBreaks);
                this.WriteContent(0x1a, content);
            }
        }

        private void WritePageSetup()
        {
            XlsContentPageSetup content = new XlsContentPageSetup();
            XlPageSetup pageSetup = this.currentSheet.PageSetup;
            if (pageSetup == null)
            {
                content.PaperKind = PaperKind.Letter;
                content.Scale = 100;
                content.HorizontalDpi = 600;
                content.VerticalDpi = 600;
                content.Copies = 1;
                content.CommentsPrintMode = XlCommentsPrintMode.None;
                content.ErrorsPrintMode = XlErrorsPrintMode.Displayed;
                content.PageOrientation = XlPageOrientation.Default;
                content.FirstPageNumber = 1;
                content.FitToWidth = 0;
                content.FitToHeight = 0;
                content.PagePrintOrder = XlPagePrintOrder.DownThenOver;
            }
            else
            {
                content.PaperKind = pageSetup.PaperKind;
                content.Scale = pageSetup.Scale;
                content.HorizontalDpi = pageSetup.HorizontalDpi;
                content.VerticalDpi = pageSetup.VerticalDpi;
                content.Copies = pageSetup.Copies;
                content.CommentsPrintMode = pageSetup.CommentsPrintMode;
                content.ErrorsPrintMode = pageSetup.ErrorsPrintMode;
                content.PageOrientation = pageSetup.PageOrientation;
                content.FirstPageNumber = pageSetup.FirstPageNumber;
                content.FitToWidth = pageSetup.FitToWidth;
                content.FitToHeight = pageSetup.FitToHeight;
                content.PagePrintOrder = pageSetup.PagePrintOrder;
                content.BlackAndWhite = pageSetup.BlackAndWhite;
                content.Draft = pageSetup.Draft;
                content.UseFirstPageNumber = !pageSetup.AutomaticFirstPageNumber;
            }
            XlPageMargins pageMargins = this.currentSheet.PageMargins;
            if (pageMargins == null)
            {
                content.HeaderMargin = 0.3;
                content.FooterMargin = 0.3;
            }
            else
            {
                content.HeaderMargin = pageMargins.HeaderInches;
                content.FooterMargin = pageMargins.FooterInches;
            }
            this.WriteContent(0xa1, content);
        }

        private void WritePane()
        {
            XlsContentPane content = new XlsContentPane {
                XPos = this.currentSheet.SplitPosition.Column,
                YPos = this.currentSheet.SplitPosition.Row
            };
            content.LeftColumn = content.XPos;
            content.TopRow = content.YPos;
            byte num = 0;
            if (content.XPos == 0)
            {
                num = 2;
            }
            if (content.YPos == 0)
            {
                num = 1;
            }
            content.ActivePane = num;
            this.WriteContent(0x41, content);
        }

        private void WritePendingRowContent()
        {
            if (this.rowsToExport.Count > 0)
            {
                this.WriteRowsContent();
            }
        }

        protected void WriteProtection()
        {
            this.WriteContent(0x19, false);
            this.WriteContent(0x12, false);
            this.WriteContent(0x13, (short) 0);
            this.WriteContent(0x1af, false);
            this.WriteContent(0x1bc, (short) 0);
        }

        private void WriteRow()
        {
            this.dbCellCalculator.RegisterRowPosition(this.writer.BaseStream.Position);
            XlsContentRow content = new XlsContentRow();
            if (this.currentRow.Cells.Count == 0)
            {
                content.FirstColumnIndex = 0;
                content.LastColumnIndex = 0;
            }
            else
            {
                content.FirstColumnIndex = this.currentRow.FirstColumnIndex;
                content.LastColumnIndex = this.currentRow.LastColumnIndex + 1;
            }
            int num = this.RegisterFormatting(this.currentRow.Formatting);
            if (num < 0)
            {
                num = 15;
            }
            content.HasFormatting = num != 15;
            content.FormatIndex = num;
            if (this.currentRow.HeightInPixels >= 0)
            {
                content.HeightInTwips = Math.Min(0x2000, XlGraphicUnitConverter.PixelsToTwips(this.currentRow.HeightInPixels, this.DpiY));
                content.IsCustomHeight = true;
            }
            else if (this.currentRow.AutomaticHeightInPixels >= 0)
            {
                content.HeightInTwips = Math.Min(0x2000, XlGraphicUnitConverter.PixelsToTwips(this.currentRow.AutomaticHeightInPixels, this.DpiY));
                content.IsCustomHeight = false;
            }
            else
            {
                content.HeightInTwips = 0xff;
                content.IsCustomHeight = false;
            }
            content.Index = this.currentRow.RowIndex;
            content.IsCollapsed = this.currentRow.IsCollapsed;
            content.IsHidden = this.currentRow.IsHidden || ((this.currentGroup != null) && this.currentGroup.IsCollapsed);
            if ((this.currentGroup != null) && (this.currentGroup.OutlineLevel > 0))
            {
                int num2 = Math.Min(7, this.currentGroup.OutlineLevel);
                content.OutlineLevel = num2;
                this.maxRowOutlineLevel = Math.Max(this.maxRowOutlineLevel, num2 + 1);
            }
            this.WriteContent(520, content);
        }

        private void WriteRowCells(XlsTableRow row)
        {
            this.cellsToExport.Clear();
            foreach (XlCell cell in row.Cells)
            {
                if (this.NeedToFlushCells(cell))
                {
                    this.FlushCells();
                }
                this.cellsToExport.Add(cell);
            }
            if (this.cellsToExport.Count > 0)
            {
                this.FlushCells();
            }
        }

        private void WriteRowsContent()
        {
            int count = this.rowsToExport.Count;
            for (int i = 0; i < count; i++)
            {
                this.dbCellCalculator.RegisterFirstCellPosition(this.writer.BaseStream.Position);
                this.WriteRowCells(this.rowsToExport[i]);
            }
            this.WriteDbCell();
            this.rowsToExport.Clear();
            this.dbCellCalculator.Reset();
        }

        private void WriteSelection(XlViewPaneType pane, XlCellPosition splitPosition, XlViewPaneType activePane)
        {
            XlCellPosition topLeft;
            int count = -1;
            List<XlCellRange> list = new List<XlCellRange>();
            if (pane != activePane)
            {
                switch (pane)
                {
                    case XlViewPaneType.BottomLeft:
                        topLeft = new XlCellPosition(0, splitPosition.Row);
                        break;

                    case XlViewPaneType.BottomRight:
                        topLeft = splitPosition;
                        break;

                    case XlViewPaneType.TopRight:
                        topLeft = new XlCellPosition(splitPosition.Column, 0);
                        break;

                    default:
                        topLeft = XlCellPosition.TopLeft;
                        break;
                }
                count = 0;
                list.Add(new XlCellRange(topLeft));
            }
            else
            {
                XlCellPosition activeCell = this.currentSheet.Selection.ActiveCell;
                topLeft = activeCell.AsRelative();
                foreach (XlCellRange range in this.currentSheet.Selection.SelectedRanges)
                {
                    XlCellRange item = range.AsRelative();
                    item.SheetName = string.Empty;
                    activeCell = item.TopLeft;
                    if ((activeCell.Column < 0x100) && (item.TopLeft.Row < 0x10000))
                    {
                        if (item.Contains(topLeft))
                        {
                            count = list.Count;
                        }
                        list.Add(item);
                    }
                }
                if (count == -1)
                {
                    count = list.Count;
                    list.Add(new XlCellRange(topLeft));
                }
                if (((activePane == XlViewPaneType.TopLeft) && (topLeft.EqualsPosition(XlCellPosition.TopLeft) && (splitPosition.EqualsPosition(XlCellPosition.TopLeft) && (list.Count == 1)))) && (list[0].TopLeft.EqualsPosition(topLeft) && list[0].BottomRight.EqualsPosition(topLeft)))
                {
                    return;
                }
            }
            XlsContentSelection content = new XlsContentSelection {
                ViewPane = pane,
                ActiveCell = topLeft,
                ActiveCellIndex = count
            };
            foreach (XlCellRange range3 in list)
            {
                XlsRefU item = XlsRefU.FromRange(range3);
                if (item != null)
                {
                    content.SelectedCells.Add(item);
                }
                if (content.SelectedCells.Count >= 0x200)
                {
                    this.WriteContent(0x1d, content);
                    content.SelectedCells.Clear();
                }
            }
            if (content.SelectedCells.Count > 0)
            {
                this.WriteContent(0x1d, content);
            }
        }

        private void WriteSelections()
        {
            if ((this.currentSheet.Selection.ActiveCell.Column < 0x100) && (this.currentSheet.Selection.ActiveCell.Row < 0x10000))
            {
                XlCellPosition splitPosition = this.currentSheet.SplitPosition;
                splitPosition = new XlCellPosition((splitPosition.Column >= 0x100) ? 0 : splitPosition.Column, (splitPosition.Row >= 0x10000) ? 0 : splitPosition.Row);
                bool hasRightPane = splitPosition.Column > 0;
                bool hasBottomPane = splitPosition.Row > 0;
                if (!(hasRightPane | hasBottomPane))
                {
                    this.WriteSelection(XlViewPaneType.TopLeft, splitPosition, XlViewPaneType.TopLeft);
                }
                else
                {
                    XlViewPaneType activePane = this.GetActivePane(hasRightPane, hasBottomPane);
                    this.WriteSelection(XlViewPaneType.TopLeft, splitPosition, activePane);
                    if (hasRightPane)
                    {
                        this.WriteSelection(XlViewPaneType.TopRight, splitPosition, activePane);
                    }
                    if (hasBottomPane)
                    {
                        this.WriteSelection(XlViewPaneType.BottomLeft, splitPosition, activePane);
                    }
                    if (hasRightPane & hasBottomPane)
                    {
                        this.WriteSelection(XlViewPaneType.BottomRight, splitPosition, activePane);
                    }
                }
            }
        }

        private void WriteSharedFormula(XlCell cell)
        {
            if (cell.SharedFormulaRange != null)
            {
                XlsContentSharedFormula content = new XlsContentSharedFormula();
                XlsRefU fu = XlsRefU.FromRange(cell.SharedFormulaRange);
                if (fu == null)
                {
                    throw new ArgumentException($"Shared formula range {cell.SharedFormulaRange.ToString()} out of XLS worksheet limits");
                }
                content.Range = fu;
                content.UseCount = (byte) Math.Max(0xff, content.Range.CellCount);
                content.FormulaBytes = this.GetSharedFormulaBytes(cell);
                this.WriteContent(0x4bc, content);
            }
        }

        private void WriteSharedStringCell()
        {
            XlCell cell = this.cellsToExport[0];
            XlsContentLabelSst content = new XlsContentLabelSst();
            this.InitializeContent(content, cell);
            XlRichTextString richTextValue = cell.RichTextValue;
            content.StringIndex = (richTextValue == null) ? this.RegisterString(XlStringHelper.CheckLength(cell.Value.TextValue, this.options)) : this.RegisterString(XlStringHelper.CheckLength(richTextValue, this.options));
            this.WriteContent(0xfd, content);
        }

        protected void WriteSharedStrings()
        {
            int count = this.sharedStrings.Count;
            this.CalcStringsInBucket(count);
            XlsChunk firstChunk = new XlsChunk(0xfc);
            using (XlsChunkWriter writer = new XlsChunkWriter(this.writer, firstChunk, new XlsChunk(60)))
            {
                writer.Write(this.sharedStringsRefCount);
                writer.Write(count);
                XLUnicodeRichExtendedString item = new XLUnicodeRichExtendedString();
                IList<IXlString> stringList = this.sharedStrings.StringList;
                for (int i = 0; i < count; i++)
                {
                    IXlString str2 = stringList[i];
                    item.Value = str2.Text;
                    item.FormatRuns.Clear();
                    if (!str2.IsPlainText)
                    {
                        this.SetupFormatRuns(item, str2 as XlRichTextString);
                    }
                    if (this.FirstStringInBucket(i))
                    {
                        XlsSSTInfo info = new XlsSSTInfo {
                            Offset = (int) (writer.BaseStream.Position + 4L)
                        };
                        info.StreamPosition = ((int) this.writer.BaseStream.Position) + info.Offset;
                        this.extendedSSTItems.Add(info);
                    }
                    item.Write(writer);
                }
            }
        }

        protected void WriteSheetIdTable()
        {
            XlsContentSheetIdTable content = new XlsContentSheetIdTable();
            for (int i = 0; i < this.sheets.Count; i++)
            {
                content.SheetIdTable.Add(i + 1);
            }
            this.WriteContent(0x13d, content);
        }

        private void WriteStringFormulaValue(IXlCell cell)
        {
            string textValue = cell.Value.TextValue;
            if (!string.IsNullOrEmpty(textValue))
            {
                XlsChunk firstChunk = new XlsChunk(0x207);
                using (XlsChunkWriter writer = new XlsChunkWriter(this.writer, firstChunk, new XlsChunk(60)))
                {
                    new XLUnicodeString { Value = XlStringHelper.CheckLength(textValue, this.options) }.Write(writer);
                }
            }
        }

        private void WriteStyleExt(int builtInId, string styleName, XlStyleCategory category, bool writeFontScheme)
        {
            XlsContentStyleExt content = new XlsContentStyleExt {
                BuiltInId = builtInId,
                Category = category,
                CustomBuiltIn = false,
                IsBuiltIn = true,
                IsHidden = false,
                OutlineLevel = 0,
                StyleName = styleName
            };
            if (writeFontScheme)
            {
                content.Properties.Add(new XfPropByte(0x25, 2));
            }
            this.WriteContent(0x892, content);
        }

        protected void WriteStyles()
        {
            this.WriteBuiltInStyle(0, "Normal", 0);
            this.WriteStyleExt(0, "Normal", XlStyleCategory.GoodBadNeutralStyle, true);
            this.WriteBuiltInStyle(3, "Comma", 0x10);
            this.WriteStyleExt(3, "Comma", XlStyleCategory.NumberFormatStyle, false);
            this.WriteBuiltInStyle(6, "Comma [0]", 0x11);
            this.WriteStyleExt(6, "Comma [0]", XlStyleCategory.NumberFormatStyle, false);
            this.WriteBuiltInStyle(4, "Currency", 0x12);
            this.WriteStyleExt(4, "Currency", XlStyleCategory.NumberFormatStyle, false);
            this.WriteBuiltInStyle(7, "Currency [0]", 0x13);
            this.WriteStyleExt(7, "Currency [0]", XlStyleCategory.NumberFormatStyle, false);
            this.WriteBuiltInStyle(5, "Percent", 20);
            this.WriteStyleExt(5, "Percent", XlStyleCategory.NumberFormatStyle, false);
        }

        private void WriteSupBookAddInUDF()
        {
            if (this.HasAddInUDFDefinition())
            {
                XlsContentSupBookAddInUDF content = new XlsContentSupBookAddInUDF();
                this.WriteContent(430, content);
                this.WriteExternalNames();
            }
        }

        private void WriteSupBookSelfReference()
        {
            if (this.HasSelfRererence())
            {
                XlsContentSupBookSelf content = new XlsContentSupBookSelf {
                    SheetCount = this.sheets.Count
                };
                this.WriteContent(430, content);
            }
        }

        protected void WriteSupportingLinks()
        {
            if (this.sheetDefinitions.Count != 0)
            {
                this.WriteSupBookAddInUDF();
                this.WriteSupBookSelfReference();
                this.WriteExternSheet();
            }
        }

        private void WriteTable(XlTable table)
        {
            this.WriteFeat11(table);
            this.WriteList12BlockLevel(table);
            this.WriteTableAutoFilter12(table);
            this.WriteList12TableStyleInfo(table);
            this.WriteList12DisplayName(table);
        }

        private void WriteTableAutoFilter12(XlTable table)
        {
            if (table.HasHeaderRow && table.HasAutoFilter)
            {
                XlCellPosition bottomRight;
                if (table.HasTotalRow)
                {
                    bottomRight = new XlCellPosition(table.Range.BottomRight.Column, table.Range.BottomRight.Row - 1);
                }
                else
                {
                    bottomRight = table.Range.BottomRight;
                }
                XlCellRange range = new XlCellRange(table.Range.TopLeft, bottomRight);
                for (int i = 0; i < table.InnerColumns.Count; i++)
                {
                    XlTableColumn column = table.InnerColumns[i];
                    if (this.ShouldWriteTableAutoFilter12(column))
                    {
                        XlsContentAutoFilter12 content = new XlsContentAutoFilter12 {
                            ColumnId = column.ColumnIndex,
                            BoundRange = XlsRef8.FromRange(range),
                            HiddenButton = column.HiddenButton,
                            IdList = table.Id
                        };
                        this.PrepareAutoFilter12Content(content, column.FilterCriteria);
                        this.WriteContent(0x87e, content);
                        this.WriteAutoFilter12Criteria(column.FilterCriteria, range);
                        this.WriteAutoFilter12DateGroup(column.FilterCriteria, range);
                    }
                }
            }
        }

        private void WriteTableColumnFlags(BinaryWriter writer, XlTable table, XlTableColumn column)
        {
            uint num = 0;
            bool flag = table.HasHeaderRow && table.HasAutoFilter;
            if (flag)
            {
                num |= 1;
            }
            if (flag && column.HiddenButton)
            {
                num |= 2;
            }
            if (this.LoadTotalString(table, column))
            {
                num |= 0x400;
            }
            if (column.HasColumnFormula)
            {
                num |= 0x800;
            }
            writer.Write(num);
        }

        private void WriteTableFeatureFlags(BinaryWriter writer, XlTable table)
        {
            uint num = 0xde0800;
            if (table.HasHeaderRow && table.HasAutoFilter)
            {
                num |= 6;
            }
            if (table.InsertRowShowing)
            {
                num |= 8;
            }
            if (table.InsertRowShift)
            {
                num |= (uint) 0x10;
            }
            if (table.HasTotalRow)
            {
                num |= (uint) 0x40;
            }
            if (table.Published)
            {
                num |= 0x1000000;
            }
            writer.Write(num);
        }

        protected void WriteTableFeatureType(BinaryWriter writer, XlTable table)
        {
            writer.Write(0);
            writer.Write(table.Id);
            writer.Write(table.HasHeaderRow ? 1 : 0);
            writer.Write(table.HasTotalRow ? 1 : 0);
            writer.Write((int) (table.Columns.Count + 1));
            writer.Write(0x40);
            writer.Write((short) 0x3267);
            writer.Write((short) 0);
            this.WriteTableFeatureFlags(writer, table);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(0);
            writer.Write(new byte[0x10]);
            XLUnicodeString str = new XLUnicodeString {
                Value = table.Name
            };
            str.Write(writer);
            writer.Write((short) table.Columns.Count);
            str.Value = table.Id.ToString();
            str.Write(writer);
        }

        protected void WriteTableFieldDataItem(BinaryWriter writer, XlTable table, int index)
        {
            XlTableColumn column = table.InnerColumns[index];
            int num = index + 1;
            writer.Write(num);
            writer.Write(0);
            writer.Write(0);
            writer.Write((int) column.TotalRowFunction);
            XlsDxfN12List list1 = new XlsDxfN12List();
            list1.IsEmpty = true;
            XlsDxfN12List dxf = list1;
            XlsDxfN12List list4 = new XlsDxfN12List();
            list4.IsEmpty = true;
            XlsDxfN12List list2 = list4;
            this.PrepareDxf(dxf, table.GetColumnTotalRowFormat(column));
            this.PrepareDxf(list2, column.DataFormatting);
            writer.Write((int) dxf.GetSize());
            writer.Write(-1);
            this.WriteTableColumnFlags(writer, table, column);
            writer.Write((int) list2.GetSize());
            writer.Write(-1);
            XLUnicodeString str = new XLUnicodeString {
                Value = "0"
            };
            str.Write(writer);
            str.Value = column.Name;
            str.Write(writer);
            dxf.Write(writer);
            list2.Write(writer);
            if (table.HasHeaderRow && table.HasAutoFilter)
            {
                if (column.FilterCriteria == null)
                {
                    writer.Write(0);
                    writer.Write((ushort) 0xffff);
                }
                else
                {
                    XlsContentAutoFilter content = new XlsContentAutoFilter();
                    if (this.ShouldWriteTableAutoFilter12(column))
                    {
                        content.Second.IsBlank = true;
                    }
                    else
                    {
                        this.PrepareAutoFilterContent(content, column.FilterCriteria);
                    }
                    writer.Write(content.GetSize());
                    writer.Write((ushort) 0xffff);
                    content.Write(writer);
                }
            }
            if (this.LoadTotalString(table, column))
            {
                str.Value = column.TotalRowLabel;
                str.Write(writer);
            }
            if (!table.HasHeaderRow)
            {
                XlsDxfN12List list5 = new XlsDxfN12List();
                list5.IsEmpty = true;
                XlsDxfN12List list3 = list5;
                this.PrepareDxf(list3, table.GetColumnHeaderRowFormat(column));
                writer.Write((int) list3.GetSize());
                list3.Write(writer);
            }
        }

        protected void WriteTableHeader(BinaryWriter writer, XlTable table)
        {
            XlsRef8 ref2 = XlsRef8.FromRange((XlCellRange) table.Range);
            new FutureRecordHeaderRefU { 
                RecordTypeId = this.UseFeature12(table) ? ((short) 0x878) : ((short) 0x872),
                RangeOfCells = true,
                Range = ref2
            }.Write(writer);
            writer.Write((ushort) 5);
            writer.Write((byte) 0);
            writer.Write(0);
            writer.Write((short) 1);
            writer.Write(0);
            writer.Write((short) 0);
            ref2.Write(writer);
        }

        private void WriteTables()
        {
            if (this.currentSheet.InnerTables.Count != 0)
            {
                this.AssignTableIds();
                this.WriteFeatHdr11();
                foreach (XlTable table in this.currentSheet.InnerTables)
                {
                    this.WriteTable(table);
                }
            }
        }

        private void WriteTableStyles()
        {
            if (this.GetTablesCount() != 0)
            {
                XlsContentTableStyles content = new XlsContentTableStyles {
                    DefaultTableStyleName = "TableStyleMedium2",
                    DefaultPivotTableStyleName = "PivotStyleLight16"
                };
                this.WriteContent(0x88e, content);
            }
        }

        private void WriteTheme()
        {
            if ((this.currentDocument.Theme != XlDocumentTheme.None) && !this.ClipboardMode)
            {
                XlsContentTheme content = new XlsContentTheme();
                Stream manifestResourceStream = base.GetType().Assembly.GetManifestResourceStream((this.currentDocument.Theme == XlDocumentTheme.Office2010) ? "DevExpress.Printing.Export.Xls.Theme.theme2010.zip" : "DevExpress.Printing.Export.Xls.Theme.theme2013.zip");
                int length = (int) manifestResourceStream.Length;
                byte[] buffer = new byte[length];
                manifestResourceStream.Read(buffer, 0, length);
                content.ThemeContent = buffer;
                this.WriteContent(0x896, content);
            }
        }

        protected void WriteUseELFs()
        {
            XlsContentWorkbookBool content = new XlsContentWorkbookBool();
            this.WriteContent(0x160, content);
        }

        protected void WriteWindow1()
        {
            XlsContentWorkbookWindow content = new XlsContentWorkbookWindow();
            XlDocumentView view = this.currentDocument.View;
            content.HorizontalScrollDisplayed = view.ShowHorizontalScrollBar;
            content.VerticalScrollDisplayed = view.ShowVerticalScrollBar;
            content.SheetTabsDisplayed = view.ShowSheetTabs;
            content.NoAutoFilterDateGrouping = !view.GroupDatesInAutoFilterMenu;
            content.TabRatio = view.TabRatio * 10;
            content.HorizontalPosition = view.WindowX;
            content.VerticalPosition = view.WindowY;
            content.WidthInTwips = view.WindowWidth;
            content.HeightInTwips = view.WindowHeight;
            content.FirstDisplayedTabIndex = view.FirstVisibleTabIndex;
            content.SelectedTabIndex = view.SelectedTabIndex;
            content.SelectedTabsCount = 1;
            this.WriteContent(0x3d, content);
        }

        protected void WriteWorkbookBool()
        {
            XlsContentWorkbookBool content = new XlsContentWorkbookBool();
            this.WriteContent(0xda, content);
        }

        protected void WriteWorkbookGlobals()
        {
            this.WriteBOF(XlsSubstreamType.WorkbookGlobals);
            this.WriteFilePass();
            this.WriteInterface();
            this.WriteWriteAccess();
            this.WriteCodePage();
            this.WriteDSF();
            this.WriteSheetIdTable();
            this.WriteProtection();
            this.WriteWindow1();
            this.WriteWorkbookOptions();
            this.WriteWorkbookBool();
            this.WriteFonts();
            this.WriteNumberFormats();
            this.WriteXFs();
            this.WriteXFExtensions();
            this.WriteStyles();
            this.WriteTableStyles();
            this.WriteCustomPalette();
            this.WriteUseELFs();
            this.WriteBundleSheet();
            this.WriteCountry();
            this.WriteSupportingLinks();
            this.WriteDefinedNames();
            this.WriteDrawingGroup();
            this.WriteSharedStrings();
            this.WriteExtSST();
            this.WriteTheme();
            this.WriteEOF();
        }

        protected void WriteWorkbookOptions()
        {
            this.WriteContent(0x40, false);
            this.WriteContent(0x8d, false);
            this.WriteContent(0x22, false);
            this.WriteContent(14, true);
            this.WriteContent(0x1b7, false);
        }

        protected void WriteWorksheet()
        {
            this.WriteBOF(XlsSubstreamType.Sheet);
            this.WriteIndex();
            this.WriteWorksheetGlobals();
            this.WriteWorksheetPageSetup();
            this.WriteWorksheetColumns();
            this.WriteAutoFilter();
            this.WriteDimensions();
            this.WriteCellTable();
            this.WriteDrawingObjects();
            this.WriteWorksheetWindow();
            this.WriteMergeCells();
            this.WriteConditionalFormattings();
            this.WriteHyperlinks();
            this.WriteDataValidations();
            this.WriteFeatures();
            this.WriteTables();
            this.RewriteIndex();
            this.WriteEOF();
        }

        private void WriteWorksheetColumns()
        {
            this.defColWidthRecordPosition = this.writer.BaseStream.Position;
            this.WriteContent(0x55, (short) 8);
            this.WriteColumns();
        }

        private void WriteWorksheetGlobals()
        {
            this.WriteContent(13, (short) 1);
            this.WriteContent(12, (short) 100);
            this.WriteContent(15, (short) 1);
            this.WriteContent(0x11, (short) 0);
            this.WriteContent(0x10, (double) 0.001);
            this.WriteContent(0x5f, true);
            this.WriteContent(0x2a, (this.currentSheet.PrintOptions != null) ? this.currentSheet.PrintOptions.Headings : false);
            this.WriteContent(0x2b, (this.currentSheet.PrintOptions != null) ? this.currentSheet.PrintOptions.GridLines : false);
            this.WriteContent(130, (short) 1);
            this.WriteGuts();
            this.WriteDefaultRowHeight();
            this.WriteWsBool();
            this.WritePageBreaks();
        }

        private void WriteWorksheetPageSetup()
        {
            this.WriteContent(20, this.currentSheet.HeaderFooter.OddHeader);
            this.WriteContent(0x15, this.currentSheet.HeaderFooter.OddFooter);
            this.WriteContent(0x83, (this.currentSheet.PrintOptions != null) ? this.currentSheet.PrintOptions.HorizontalCentered : false);
            this.WriteContent(0x84, (this.currentSheet.PrintOptions != null) ? this.currentSheet.PrintOptions.VerticalCentered : false);
            XlPageMargins pageMargins = this.currentSheet.PageMargins;
            if (pageMargins == null)
            {
                this.WriteContent(0x26, (double) 0.7);
                this.WriteContent(0x27, (double) 0.7);
                this.WriteContent(40, (double) 0.75);
                this.WriteContent(0x29, (double) 0.75);
            }
            else
            {
                this.WriteContent(0x26, pageMargins.LeftInches);
                this.WriteContent(0x27, pageMargins.RightInches);
                this.WriteContent(40, pageMargins.TopInches);
                this.WriteContent(0x29, pageMargins.BottomInches);
            }
            this.WritePageSetup();
            this.WriteHeaderFooter();
        }

        protected void WriteWorksheets()
        {
            for (int i = 0; i < this.sheets.Count; i++)
            {
                this.currentSheet = this.sheets[i];
                this.currentSheetIndex = i;
                this.WriteBoundSheetStartPosition();
                this.WriteWorksheet();
            }
            this.currentSheet = null;
        }

        protected void WriteWorksheetWindow()
        {
            XlsContentWorksheetWindow content = new XlsContentWorksheetWindow();
            IXlSheetViewOptions viewOptions = this.currentSheet.ViewOptions;
            content.ShowFormulas = viewOptions.ShowFormulas;
            content.ShowGridlines = viewOptions.ShowGridLines;
            content.ShowRowColumnHeadings = viewOptions.ShowRowColumnHeaders;
            content.ShowOutlineSymbols = viewOptions.ShowOutlineSymbols;
            content.ShowZeroValues = viewOptions.ShowZeroValues;
            content.RightToLeft = viewOptions.RightToLeft;
            content.Frozen = !this.currentSheet.SplitPosition.EqualsPosition(XlCellPosition.TopLeft);
            content.FrozenWithoutPaneSplit = content.Frozen;
            content.SheetTabIsSelected = this.currentSheetIndex == this.currentDocument.View.SelectedTabIndex;
            content.GridlinesColorIndex = 0x40;
            this.WriteContent(0x23e, content);
            if (content.Frozen)
            {
                this.WritePane();
            }
            this.WriteSelections();
        }

        protected void WriteWriteAccess()
        {
            XlsContentWriteAccess content = new XlsContentWriteAccess();
            this.WriteContent(0x5c, content);
        }

        private void WriteWsBool()
        {
            XlsContentWsBool content = new XlsContentWsBool {
                ShowPageBreaks = true,
                ShowRowSumsBelow = this.currentSheet.OutlineProperties.SummaryBelow,
                ShowColumnSumsRight = this.currentSheet.OutlineProperties.SummaryRight,
                FitToPage = (this.currentSheet.PageSetup != null) ? this.currentSheet.PageSetup.FitToPage : false
            };
            this.WriteContent(0x81, content);
        }

        private void WriteXFCrc()
        {
            XlsContentXFCrc content = new XlsContentXFCrc {
                XFCount = this.xfCount,
                XFCRC = this.xfCrc
            };
            this.WriteContent(0x87c, content);
        }

        protected void WriteXFExtensions()
        {
            if ((this.xfExtensions.Count > 0) && (this.xfCount <= 0xfd2))
            {
                this.WriteXFCrc();
                foreach (XlsContentXFExt ext in this.xfExtensions)
                {
                    this.WriteContent(0x87d, ext);
                }
                this.xfExtensions.Clear();
            }
        }

        protected void WriteXFs()
        {
            this.xfExtensions.Clear();
            this.xfCount = 0;
            this.xfCrc = 0;
            XlsContentXF content = new XlsContentXF();
            foreach (XlsXf xf in this.xfList)
            {
                content.IsStyleFormat = xf.IsStyleFormat;
                content.IsHidden = xf.IsHidden;
                content.IsLocked = xf.IsLocked;
                content.FontId = xf.FontId;
                content.NumberFormatId = xf.NumberFormatId;
                content.StyleId = xf.IsStyleFormat ? 0xfff : xf.StyleId;
                content.ApplyAlignment = xf.ApplyAlignment;
                content.ApplyBorder = xf.ApplyBorder;
                content.ApplyFill = xf.ApplyFill;
                content.ApplyFont = xf.ApplyFont;
                content.ApplyNumberFormat = xf.ApplyNumberFormat;
                content.ApplyProtection = xf.ApplyProtection;
                XlBorder border = xf.Border;
                content.BorderTopColorIndex = this.GetBorderColorIndex(border.TopLineStyle, border.TopColor);
                content.BorderBottomColorIndex = this.GetBorderColorIndex(border.BottomLineStyle, border.BottomColor);
                content.BorderLeftColorIndex = this.GetBorderColorIndex(border.LeftLineStyle, border.LeftColor);
                content.BorderRightColorIndex = this.GetBorderColorIndex(border.RightLineStyle, border.RightColor);
                XlBorderLineStyle lineStyle = (border.DiagonalUpLineStyle != XlBorderLineStyle.None) ? border.DiagonalUpLineStyle : border.DiagonalDownLineStyle;
                content.BorderDiagonalColorIndex = this.GetBorderColorIndex(lineStyle, border.DiagonalColor);
                content.BorderTopLineStyle = border.TopLineStyle;
                content.BorderBottomLineStyle = border.BottomLineStyle;
                content.BorderLeftLineStyle = border.LeftLineStyle;
                content.BorderRightLineStyle = border.RightLineStyle;
                content.BorderDiagonalLineStyle = lineStyle;
                content.BorderDiagonalDown = border.DiagonalDownLineStyle != XlBorderLineStyle.None;
                content.BorderDiagonalUp = border.DiagonalUpLineStyle != XlBorderLineStyle.None;
                XlFill fill = xf.Fill;
                content.FillBackColorIndex = this.palette.GetBackgroundColorIndex(fill.BackColor, this.currentDocument.Theme);
                content.FillForeColorIndex = this.palette.GetForegroundColorIndex(fill.ForeColor, this.currentDocument.Theme);
                content.FillPatternType = fill.PatternType;
                XlCellAlignment alignment = xf.Alignment;
                content.HorizontalAlignment = alignment.HorizontalAlignment;
                content.VerticalAlignment = alignment.VerticalAlignment;
                content.Indent = Math.Min(alignment.Indent, 15);
                content.PivotButton = false;
                content.QuotePrefix = xf.QuotePrefix;
                content.ReadingOrder = alignment.ReadingOrder;
                content.ShrinkToFit = alignment.ShrinkToFit;
                content.TextRotation = alignment.TextRotation;
                content.WrapText = alignment.WrapText;
                content.HasExtension = this.PrepareXFExtension(xf, this.xfCount);
                content.CrcValue = this.xfCrc;
                this.WriteContent(0xe0, content);
                this.xfCrc = content.CrcValue;
                this.xfCount++;
            }
        }

        private XlFormulaConverter FormulaConverter
        {
            get
            {
                this.formulaConverter ??= new XlFormulaConverter(this.options);
                return this.formulaConverter;
            }
        }

        public int CurrentOutlineLevel =>
            (this.currentGroup != null) ? this.currentGroup.OutlineLevel : 0;

        private float DpiY =>
            this.options.UseDeviceIndependentPixels ? 96f : GraphicsDpi.Pixel;

        public IXlDocument CurrentDocument =>
            this.currentDocument;

        public IXlSheet CurrentSheet =>
            this.currentSheet;

        internal XlExpressionContext ExpressionContext =>
            this.expressionContext;

        public int CurrentRowIndex =>
            (this.currentRow == null) ? this.currentRowIndex : this.currentRow.RowIndex;

        public int CurrentColumnIndex =>
            this.currentColumnIndex;

        public IXlDocumentOptions DocumentOptions =>
            this.options;

        public XlDocumentProperties DocumentProperties =>
            this.documentProperties;

        internal XlCalculationOptions CalculationOptions =>
            this.calculationOptions;

        public bool ClipboardMode { get; set; }

        public IXlFormulaEngine FormulaEngine =>
            this;

        public IXlFormulaParser FormulaParser =>
            this.formulaParser;

        public CultureInfo CurrentCulture =>
            (this.options.Culture == null) ? CultureInfo.InvariantCulture : this.options.Culture;
    }
}

