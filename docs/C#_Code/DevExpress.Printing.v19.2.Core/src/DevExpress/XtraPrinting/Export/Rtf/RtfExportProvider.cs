namespace DevExpress.XtraPrinting.Export.Rtf
{
    using DevExpress.Printing.Exports.RtfExport;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class RtfExportProvider : RtfDocumentProviderBase, IRtfExportProvider, ITableExportProvider
    {
        private int footerY;
        private int topMarginOffset;
        private RtfChunkData currentChunkData;
        private RtfChunkData headerChunkData;
        private RtfChunkData footerChunkData;
        private List<RtfChunkData> mainChunkDataList;
        private Margins margins;
        private Rectangle pageBounds;
        private ObjectInfo currentInfo;

        public RtfExportProvider(Stream stream, Document document, RtfExportOptions options) : base(stream, new RtfExportContext(document.PrintingSystem, options, new RtfExportHelper()))
        {
        }

        private bool CanExportAsPlain(ObjectInfo[] objectInfos)
        {
            if (objectInfos.Length != 1)
            {
                return false;
            }
            BrickViewData data = (BrickViewData) objectInfos[0].Object;
            if ((data.TableCell is ImageBrick) && ((data.TableCell as VisualBrick).BackColor == Color.Transparent))
            {
                return true;
            }
            TextBrick tableCell = data.TableCell as TextBrick;
            return ((tableCell != null) && ((GraphicsConvertHelper.ToVertStringAlignment(tableCell.Style.TextAlignment) == StringAlignment.Near) && ((tableCell.Sides == BorderSide.None) && (tableCell.BackColor == Color.Transparent))));
        }

        private static int DocToDip(float value) => 
            Convert.ToInt32(GraphicsUnitConverter.DocToDip(value));

        private int GetBottomMarginContent(LayoutControlCollection layoutControls, float bottomMarginOffset)
        {
            LayoutControlCollection source = new LayoutControlCollection();
            int num = layoutControls.Count - 1;
            while (true)
            {
                if (num >= 0)
                {
                    BrickViewData data = (BrickViewData) layoutControls[num];
                    if (data.TableCell.Modifier == BrickModifier.MarginalFooter)
                    {
                        source.Add(data);
                        num--;
                        continue;
                    }
                }
                if (source.Count > 0)
                {
                    Func<ILayoutControl, int> selector = <>c.<>9__30_0;
                    if (<>c.<>9__30_0 == null)
                    {
                        Func<ILayoutControl, int> local1 = <>c.<>9__30_0;
                        selector = <>c.<>9__30_0 = x => x.Top;
                    }
                    float num2 = source.Min<ILayoutControl>(selector);
                    Func<ILayoutControl, float> func2 = <>c.<>9__30_1;
                    if (<>c.<>9__30_1 == null)
                    {
                        Func<ILayoutControl, float> local2 = <>c.<>9__30_1;
                        func2 = <>c.<>9__30_1 = x => x.BoundsF.Bottom;
                    }
                    float num4 = source.Max<ILayoutControl>(func2) - num2;
                    this.footerY = Math.Max(0, ConvertToTwips(this.margins.Bottom - num4, 100f) - ConvertToTwips(bottomMarginOffset, 300f));
                    MegaTable megaTable = new MegaTable(source, true, this.PrintingSystem.Document.CorrectImportBrickBounds, -1);
                    List<int> rowHeights = megaTable.RowHeights;
                    rowHeights[0] = (int) GraphicsUnitConverter.Convert(bottomMarginOffset, (float) 300f, (float) 96f);
                    TableRtfChunkData data1 = new TableRtfChunkData(megaTable.ColWidths, rowHeights);
                    data1.IsHeaderFooterContent = true;
                    data1.RtlLayout = base.RightToLeftLayout;
                    this.footerChunkData = data1;
                    this.currentChunkData = this.footerChunkData;
                    this.GetHeaderFooterContent(this.footerChunkData, megaTable);
                }
                return source.Count;
            }
        }

        protected override void GetContent()
        {
            float[] singleArray3;
            this.mainChunkDataList = new List<RtfChunkData>();
            if (this.PrintingSystem.DocumentIsDeserialized)
            {
                singleArray3 = new float[] { 1f };
            }
            else
            {
                singleArray3 = new float[] { 1f, 4f };
            }
            float[] ranges = singleArray3;
            base.RtfExportContext.ProgressReflector.SetProgressRanges(ranges);
            RtfLayoutBuilder layoutBuilder = new RtfLayoutBuilder(this.PrintingSystem.Document, base.RtfExportContext);
            LayoutControlCollection layoutControls = layoutBuilder.BuildLayoutControls();
            this.margins = layoutBuilder.PageMargins;
            this.pageBounds = layoutBuilder.PageBounds;
            if (layoutControls.Count != 0)
            {
                try
                {
                    base.RtfExportContext.ProgressReflector.InitializeRange(layoutControls.Count);
                    layoutControls.Sort(new RtfLayoutControlComparer());
                    if (!base.RtfExportOptions.ExportToClipboard)
                    {
                        int topMarginContent = this.GetTopMarginContent(layoutControls);
                        int bottomMarginContent = this.GetBottomMarginContent(layoutControls, layoutBuilder.BottomMarginOffset);
                        layoutControls = new LayoutControlCollection(layoutControls.GetRange(topMarginContent, (layoutControls.Count - topMarginContent) - bottomMarginContent));
                    }
                    if (layoutControls.Count != 0)
                    {
                        IEnumerator mcEnumerator = layoutBuilder.MultiColumnInfo.GetEnumerator();
                        MultiColumnInfo nextMultiColumnValue = this.GetNextMultiColumnValue(mcEnumerator);
                        bool flag = false;
                        Queue<int> pageBreaks = this.GetPageBreaks(layoutBuilder, this.topMarginOffset);
                        int topMarginOffset = this.topMarginOffset;
                        foreach (LayoutControlCollection controls2 in new LayoutControlDivider(layoutControls).Parts)
                        {
                            RtfChunkData data;
                            MegaTable table = new MegaTable(controls2, true, this.PrintingSystem.Document.CorrectImportBrickBounds, -1);
                            List<int> rowHeights = table.RowHeights;
                            List<int> list2 = rowHeights;
                            list2[0] -= Math.Min(topMarginOffset, rowHeights[0]);
                            RtfMultiColumn none = RtfMultiColumn.None;
                            if (nextMultiColumnValue != null)
                            {
                                if (((nextMultiColumnValue.Start + this.topMarginOffset) >= topMarginOffset) && ((nextMultiColumnValue.Start + this.topMarginOffset) < table.Height))
                                {
                                    none = RtfMultiColumn.None | RtfMultiColumn.Start;
                                    flag = true;
                                }
                                else if ((nextMultiColumnValue.End != 0f) && (((nextMultiColumnValue.End + this.topMarginOffset) >= topMarginOffset) && ((nextMultiColumnValue.End + this.topMarginOffset) < table.Height)))
                                {
                                    none = RtfMultiColumn.End;
                                    flag = false;
                                    nextMultiColumnValue = this.GetNextMultiColumnValue(mcEnumerator);
                                    if ((nextMultiColumnValue != null) && (((nextMultiColumnValue.Start + this.topMarginOffset) >= topMarginOffset) && ((nextMultiColumnValue.Start + this.topMarginOffset) < table.Height)))
                                    {
                                        none |= RtfMultiColumn.None | RtfMultiColumn.Start;
                                        flag = true;
                                    }
                                }
                            }
                            if (flag || !this.CanExportAsPlain(table.Objects))
                            {
                                TableRtfChunkData data2 = new TableRtfChunkData(table.ColWidths, rowHeights);
                                data2.RtlLayout = base.RightToLeftLayout;
                                data = data2;
                            }
                            else
                            {
                                int num5 = ConvertToTwips((float) ((this.pageBounds.Width - this.margins.Left) - this.margins.Right), 100f);
                                PlainRtfChunkData data1 = new PlainRtfChunkData(table.ColWidths, rowHeights);
                                data1.UsefulPageWidth = num5;
                                data1.RtlLayout = base.RightToLeftLayout;
                                data = data1;
                            }
                            this.currentChunkData = data;
                            int num4 = this.GetPageBreaksBeforeCount(pageBreaks, topMarginOffset, table.Height);
                            if (num4 > 0)
                            {
                                RtfPageBreakInfo info1 = new RtfPageBreakInfo();
                                info1.InsideMultiColumn = flag;
                                info1.Count = num4;
                                this.currentChunkData.PageBreakInfo = info1;
                            }
                            if ((none & (RtfMultiColumn.None | RtfMultiColumn.Start)) > RtfMultiColumn.None)
                            {
                                this.currentChunkData.MultiColumn = RtfMultiColumn.None | RtfMultiColumn.Start;
                                this.currentChunkData.ColumnCount = nextMultiColumnValue.ColumnCount;
                            }
                            if ((none & RtfMultiColumn.End) > RtfMultiColumn.None)
                            {
                                this.currentChunkData.MultiColumn |= RtfMultiColumn.End;
                            }
                            this.currentChunkData.FillTemplate();
                            this.mainChunkDataList.Add(this.currentChunkData);
                            topMarginOffset = table.Height;
                            for (int i = 0; i < table.Objects.Length; i++)
                            {
                                this.currentInfo = table.Objects[i];
                                this.UpdateCurrentChunkInfo();
                                base.SetCurrentCell();
                                ProgressReflector progressReflector = base.RtfExportContext.ProgressReflector;
                                float rangeValue = progressReflector.RangeValue;
                                progressReflector.RangeValue = rangeValue + 1f;
                            }
                        }
                    }
                }
                finally
                {
                    base.RtfExportContext.ProgressReflector.MaximizeRange();
                }
            }
        }

        private void GetHeaderFooterContent(RtfChunkData chunkData, MegaTable megaTable)
        {
            chunkData.FillTemplate();
            for (int i = 0; i < megaTable.Objects.Length; i++)
            {
                this.currentInfo = megaTable.Objects[i];
                this.UpdateCurrentChunkInfo();
                base.SetCurrentCell();
            }
        }

        private MultiColumnInfo GetNextMultiColumnValue(IEnumerator mcEnumerator)
        {
            if (!mcEnumerator.MoveNext())
            {
                return null;
            }
            MultiColumnInfo current = (MultiColumnInfo) mcEnumerator.Current;
            MultiColumnInfo info1 = new MultiColumnInfo();
            info1.ColumnCount = current.ColumnCount;
            info1.Start = DocToDip(current.Start);
            info1.End = DocToDip(current.End);
            return info1;
        }

        private Queue<int> GetPageBreaks(LayoutBuilder layoutBuilder, int topMarginOffset)
        {
            if (!base.RtfExportOptions.ExportPageBreaks)
            {
                return new Queue<int>();
            }
            Queue<int> queue = new Queue<int>();
            foreach (PageBreakData data in layoutBuilder.PageBreaks)
            {
                queue.Enqueue(DocToDip(data.Value) + topMarginOffset);
            }
            return queue;
        }

        private int GetPageBreaksBeforeCount(Queue<int> pageBreaks, int offsetY, int height)
        {
            int num = 0;
            while ((pageBreaks.Count != 0) && ((pageBreaks.Peek() >= offsetY) && (pageBreaks.Peek() < height)))
            {
                pageBreaks.Dequeue();
                num++;
            }
            return num;
        }

        private int GetTopMarginContent(LayoutControlCollection layoutControls)
        {
            LayoutControlCollection controls = new LayoutControlCollection();
            int num = 0;
            while (true)
            {
                if (num < layoutControls.Count)
                {
                    BrickViewData data = (BrickViewData) layoutControls[num];
                    if (data.TableCell.Modifier == BrickModifier.MarginalHeader)
                    {
                        controls.Add(data);
                        num++;
                        continue;
                    }
                }
                if (controls.Count > 0)
                {
                    this.topMarginOffset = GraphicsUnitConverter.Convert(this.margins.Top, 100f, 96f);
                    MegaTable megaTable = new MegaTable(controls, true, this.PrintingSystem.Document.CorrectImportBrickBounds, -1);
                    TableRtfChunkData data1 = new TableRtfChunkData(megaTable.ColWidths, megaTable.RowHeights);
                    data1.IsHeaderFooterContent = true;
                    data1.RtlLayout = base.RightToLeftLayout;
                    this.headerChunkData = data1;
                    this.currentChunkData = this.headerChunkData;
                    this.GetHeaderFooterContent(this.headerChunkData, megaTable);
                }
                return controls.Count;
            }
        }

        protected override void OverrideFontSizeToPreventDisappearBottomBorder()
        {
        }

        protected override void SetAngle(float angle)
        {
            this.currentChunkData.SetAngle(angle);
        }

        public override void SetCellStyle()
        {
            if (base.CurrentStyle.BackColor != Color.Transparent)
            {
                this.currentChunkData.SetBackColor();
            }
            this.currentChunkData.SetBorders(this.CurrentColIndex, this.CurrentRowIndex);
            this.currentChunkData.SetForeColor();
            this.currentChunkData.SetFontString(base.GetFontString());
            if (!(this.CurrentData.TableCell is ImageBrick) || !(this.currentChunkData is PlainRtfChunkData))
            {
                this.currentChunkData.SetHAlign();
                this.currentChunkData.SetVAlign();
            }
            if ((this.CurrentData.TableCell is ImageBrick) || (this.CurrentData.TableCell is LineBrick))
            {
                this.currentChunkData.SetIndent();
            }
            else
            {
                this.currentChunkData.SetPadding();
            }
            this.currentChunkData.SetDirection();
        }

        protected override void SetCellUnion()
        {
            this.currentChunkData.SetCellUnion();
        }

        protected override void SetContent(string content)
        {
            this.currentChunkData.SetContent(content);
            if (this.CurrentCanShrinkAndGrow)
            {
                this.currentChunkData.SetSpecifyRowHeightType(SpecifyRowHeightType.None, this.CurrentRowIndex, this.CurrentRowSpan);
            }
            else if (this.IsAtLeastHeightAllowed)
            {
                this.currentChunkData.SetSpecifyRowHeightType(SpecifyRowHeightType.AtLeast, this.CurrentRowIndex, this.CurrentRowSpan);
            }
        }

        protected override void SetFrameText(string text)
        {
            this.SetContent(text);
            if (!string.IsNullOrEmpty(text) && this.IsAtLeastHeightAllowed)
            {
                this.currentChunkData.SetSpecifyRowHeightType(SpecifyRowHeightType.AtLeast, this.CurrentRowIndex, this.CurrentRowSpan);
            }
        }

        protected override void SetImageContent(string content)
        {
            this.SetContent(content);
            if (this.IsAtLeastHeightAllowed)
            {
                this.currentChunkData.SetSpecifyRowHeightType(SpecifyRowHeightType.AtLeast, this.CurrentRowIndex, this.CurrentRowSpan);
            }
        }

        private void UpdateCurrentChunkInfo()
        {
            this.currentChunkData.UpdateDataIndexes(this.CurrentColIndex, this.CurrentRowIndex, this.CurrentColSpan, this.CurrentRowSpan);
            if (base.CurrentStyle != null)
            {
                int backColorIndex = (base.CurrentStyle.BackColor == Color.Transparent) ? 0 : base.GetBackColorIndex();
                this.currentChunkData.UpdateColors(backColorIndex, (base.CurrentStyle.Sides == BorderSide.None) ? 0 : base.GetBorderColorIndex(), base.GetForeColorIndex());
                this.currentChunkData.UpdateStyle(base.CurrentStyle);
            }
        }

        protected override void WriteContent()
        {
            Margins minMargins = this.PrintingSystem.PageSettings.MinMargins;
            int headerY = ConvertToTwips((float) minMargins.Top, 100f);
            int footerY = this.footerY + ConvertToTwips((float) minMargins.Bottom, 100f);
            foreach (RtfChunkData data in this.mainChunkDataList)
            {
                base.writer.Write(data.GetResultContent(headerY, footerY).ToString());
            }
            base.writer.Write("}");
        }

        protected override void WriteDocumentHeaderFooter()
        {
            Margins minMargins = this.PrintingSystem.PageSettings.MinMargins;
            if (base.RtfExportOptions.EmptyFirstPageHeaderFooter)
            {
                base.writer.WriteLine(RtfTags.SpecialFirstPageHeaderFooter);
            }
            bool flag = base.RtfExportOptions.ExportWatermarks && (!ImageSource.IsNullOrEmpty(this.PrintingSystem.Watermark.ActualImageSource) || !string.IsNullOrEmpty(this.PrintingSystem.Watermark.Text));
            if ((this.headerChunkData != null) | flag)
            {
                base.writer.WriteLine(string.Format(RtfTags.HeaderY, ConvertToTwips((float) minMargins.Top, 100f)));
                base.writer.Write("{");
                base.writer.Write(RtfTags.Header);
                base.writer.Write(RtfTags.WhitespaceParagraph);
                if (this.headerChunkData != null)
                {
                    base.writer.Write(this.headerChunkData.GetResultContent(0, 0).ToString());
                }
                if (flag)
                {
                    this.WriteWatermark();
                }
                base.writer.WriteLine("}");
            }
            if (this.footerChunkData != null)
            {
                base.writer.WriteLine(string.Format(RtfTags.FooterY, this.footerY + ConvertToTwips((float) minMargins.Bottom, 100f)));
                base.writer.Write("{");
                base.writer.Write(RtfTags.Footer);
                base.writer.Write(RtfTags.WhitespaceParagraph);
                base.writer.Write(this.footerChunkData.GetResultContent(0, 0).ToString());
                base.writer.WriteLine("}");
            }
        }

        protected override void WriteHeader()
        {
            base.WriteHeader();
            if (!base.RtfExportOptions.ExportToClipboard)
            {
                this.WritePageBounds();
                this.WriteMargins();
            }
            this.WritePageNumberingInfo();
            this.WriteDocumentHeaderFooter();
            base.writer.WriteLine(RtfTags.NoGrowAutoFit);
            base.writer.WriteLine("{");
        }

        protected override void WriteMargins()
        {
            int num = ConvertToTwips((float) this.margins.Left, 100f);
            int num2 = ConvertToTwips((float) this.margins.Right, 100f);
            int num3 = ConvertToTwips((float) this.margins.Top, 100f);
            int num4 = ConvertToTwips((float) this.margins.Bottom, 100f);
            base.writer.WriteLine(string.Format(RtfTags.Margins, new object[] { num, num2, num3, num4 }));
        }

        protected override void WritePageBounds()
        {
            base.writer.WriteLine(RtfTags.SectionDefault);
            if (this.PrintingSystem.PageSettings.Landscape)
            {
                base.writer.WriteLine(RtfTags.PageLandscape);
            }
            int num = ConvertToTwips((float) this.pageBounds.Width, 100f);
            int num2 = ConvertToTwips((float) this.pageBounds.Height, 100f);
            base.writer.WriteLine(string.Format(RtfTags.PageSize, num, num2));
        }

        protected override void WritePageNumberingInfo()
        {
            if (!string.IsNullOrEmpty(base.PageNumberingInfo))
            {
                base.writer.WriteLine(base.PageNumberingInfo);
            }
        }

        private void WriteWatermark()
        {
            if (!ImageSource.IsNullOrEmpty(this.PrintingSystem.Watermark.ActualImageSource))
            {
                RtfDocumentProviderBase.ImageWatermarkRtfExportProvider provider = new RtfDocumentProviderBase.ImageWatermarkRtfExportProvider(this.PrintingSystem.Watermark, this.PrintingSystem.PageSettings.Data);
                base.writer.Write(provider.GetRtfContent(base.RtfExportContext));
            }
            if (!string.IsNullOrEmpty(this.PrintingSystem.Watermark.Text))
            {
                RtfDocumentProviderBase.TextWatermarkRtfExportProvider provider2 = new RtfDocumentProviderBase.TextWatermarkRtfExportProvider(this.PrintingSystem.Watermark, this.PrintingSystem.PageSettings.Data);
                base.writer.Write(provider2.GetRtfContent(base.RtfExportContext));
            }
        }

        private PrintingSystemBase PrintingSystem =>
            base.RtfExportContext.PrintingSystem;

        public override BrickViewData CurrentData =>
            (BrickViewData) this.currentInfo.Object;

        private int CurrentRowIndex =>
            this.currentInfo.RowIndex;

        private int CurrentColIndex =>
            this.currentInfo.ColIndex;

        private int CurrentColSpan =>
            this.currentInfo.ColSpan;

        private int CurrentRowSpan =>
            this.currentInfo.RowSpan;

        private bool CurrentCanShrinkAndGrow =>
            (!(this.CurrentData.TableCell is IRichTextBrick) || !((IRichTextBrick) this.CurrentData.TableCell).CanShrinkAndGrow) ? ((this.CurrentData.TableCell is LabelBrick) && ((LabelBrick) this.CurrentData.TableCell).CanShrinkAndGrow) : true;

        private bool IsAtLeastHeightAllowed =>
            !base.RtfExportOptions.KeepRowHeight && (!this.CurrentCanShrinkAndGrow && !(this.CurrentData.TableCell is CharacterCombTextBrick));

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RtfExportProvider.<>c <>9 = new RtfExportProvider.<>c();
            public static Func<ILayoutControl, int> <>9__30_0;
            public static Func<ILayoutControl, float> <>9__30_1;

            internal int <GetBottomMarginContent>b__30_0(ILayoutControl x) => 
                x.Top;

            internal float <GetBottomMarginContent>b__30_1(ILayoutControl x) => 
                x.BoundsF.Bottom;
        }

        private class RtfLayoutControlComparer : IComparer<ILayoutControl>
        {
            private int Compare(int y1, int y2) => 
                (y1 <= y2) ? ((y1 >= y2) ? 0 : -1) : 1;

            int IComparer<ILayoutControl>.Compare(ILayoutControl xControl, ILayoutControl yControl) => 
                this.Compare(xControl.Top, yControl.Top);
        }
    }
}

