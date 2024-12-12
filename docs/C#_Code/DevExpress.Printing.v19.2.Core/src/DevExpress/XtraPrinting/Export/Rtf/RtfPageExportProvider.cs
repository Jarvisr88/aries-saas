namespace DevExpress.XtraPrinting.Export.Rtf
{
    using DevExpress.Printing.Native;
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Drawing;
    using DevExpress.XtraPrinting.Export;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.IO;
    using System.Text;

    public class RtfPageExportProvider : RtfDocumentProviderBase, IRtfExportProvider, ITableExportProvider
    {
        private readonly int[] pageIndices;
        private readonly StringBuilder stringBuilder;
        private Page currentPage;
        private ILayoutControl currentInfo;

        public RtfPageExportProvider(Stream stream, Document document, RtfExportOptions options) : base(stream, new RtfExportContext(document.PrintingSystem, options, new RtfExportHelper()))
        {
            this.stringBuilder = new StringBuilder();
            this.pageIndices = PageRangeParser.GetIndices(options.PageRange, document.PageCount);
        }

        protected bool ContentEndsWith(string rtfTag) => 
            (this.stringBuilder.Length >= rtfTag.Length) && (this.stringBuilder.ToString(this.stringBuilder.Length - rtfTag.Length, rtfTag.Length) == rtfTag);

        protected override void GetContent()
        {
            float[] ranges = new float[] { 1f };
            base.RtfExportContext.ProgressReflector.SetProgressRanges(ranges);
            try
            {
                base.RtfExportContext.ProgressReflector.InitializeRange(this.pageIndices.Length);
                for (int i = 0; i < this.pageIndices.Length; i++)
                {
                    int pageIndex = this.pageIndices[i];
                    this.currentPage = base.RtfExportContext.PrintingSystem.Document.Pages[pageIndex];
                    using (RtfPageLayoutBuilder builder = new RtfPageLayoutBuilder(this.currentPage, base.RtfExportContext))
                    {
                        LayoutControlCollection controls = builder.BuildLayoutControls();
                        if (i != 0)
                        {
                            this.SetContent(RtfTags.SectionStart);
                        }
                        this.WritePageBounds();
                        if (base.RtfExportOptions.ExportWatermarks && base.RtfExportContext.PrintingSystem.Watermark.NeedDraw(pageIndex, base.RtfExportContext.PrintingSystem.Document.PageCount))
                        {
                            this.WriteWatermark();
                        }
                        for (int j = 0; j < controls.Count; j++)
                        {
                            this.currentInfo = controls[j];
                            base.SetCurrentCell();
                        }
                    }
                    ProgressReflector progressReflector = base.RtfExportContext.ProgressReflector;
                    progressReflector.RangeValue++;
                }
            }
            finally
            {
                base.RtfExportContext.ProgressReflector.MaximizeRange();
            }
        }

        public override Rectangle GetFrameBounds()
        {
            Rectangle rectangle = Rectangle.Round(RectHelper.AdjustBorderRect(new FrameBoundsCorrector(GraphicsUnitConverter.Convert(this.CurrentData.BoundsF, (float) 96f, (float) 1440f), base.CurrentStyle.Sides, base.BorderWidth, base.CurrentStyle.Padding).Bounds, base.CurrentStyle.Sides, base.BorderWidth, base.CurrentStyle.BorderStyle));
            return new Rectangle(base.RightToLeftLayout ? (((ConvertToTwips((float) this.currentPage.PageData.Bounds.Width, 100f) - rectangle.Width) - rectangle.Left) - this.TwipsMargins.Right) : (rectangle.Left - this.TwipsMargins.Left), rectangle.Top, rectangle.Width, -rectangle.Height);
        }

        public override RtfDocumentProviderBase.PrintingParagraphAppearance GetParagraphAppearance()
        {
            int borderColorIndex = base.GetBorderColorIndex();
            int backColorIndex = base.GetBackColorIndex();
            return new RtfDocumentProviderBase.PrintingParagraphAppearance(this.GetFrameBounds(), this.IsRightToLeftLayout(this.CurrentData.TableCell) ? BrickStyle.SwapRightAndLeftSides(base.CurrentStyle.Sides) : base.CurrentStyle.Sides, (int) base.BorderWidth, borderColorIndex, backColorIndex);
        }

        private bool HasRealBorder(BorderSide side) => 
            base.CurrentStyle.Sides.HasFlag(side) && ((base.CurrentStyle.BorderWidth > 0f) && !DXColor.IsTransparentColor(base.CurrentStyle.BorderColor));

        private bool IsImageObject(ITableCell tableCell)
        {
            Brick brick = ((tableCell is CellWrapper) ? ((Brick) ((CellWrapper) tableCell).InnerCell) : ((Brick) tableCell)) as Brick;
            return ((brick != null) && ((brick is ImageBrick) || ((brick is BarCodeBrick) || (brick is ShapeBrick))));
        }

        private bool IsRightToLeftLayout(ITableCell tableCell)
        {
            Brick brick = ((tableCell is CellWrapper) ? ((Brick) ((CellWrapper) tableCell).InnerCell) : ((Brick) tableCell)) as Brick;
            return ((brick != null) && brick.RightToLeftLayout);
        }

        protected override void SetAngle(float angle)
        {
        }

        private void SetBorderCore(string borderTag, int borderSpace)
        {
            this.SetContent(borderTag);
            this.SetBorderStyle(borderSpace);
        }

        protected void SetBorders()
        {
            if (base.CurrentStyle != null)
            {
                BorderSide sides = this.IsRightToLeftLayout(this.CurrentData.TableCell) ? BrickStyle.SwapRightAndLeftSides(base.CurrentStyle.Sides) : base.CurrentStyle.Sides;
                if (this.IsImageObject(this.CurrentData.TableCell))
                {
                    this.SetImageBorders(sides);
                }
                else
                {
                    this.SetCommonBorders(sides, base.CurrentStyle.Padding);
                }
            }
        }

        private void SetBorderStyle(int borderSpace)
        {
            this.SetContent(RtfExportBorderHelper.GetFullBorderStyle(base.BorderStyle, (int) base.BorderWidth, base.GetBorderColorIndex(), borderSpace));
        }

        private void SetCellFont()
        {
            this.SetContent(string.Format(RtfTags.Color, base.GetForeColorIndex()));
            this.SetContent(base.GetFontString());
        }

        private void SetCellHAlign()
        {
            this.SetContent(RtfAlignmentConverter.ToHorzRtfAlignment(base.CurrentStyle.TextAlignment));
        }

        private void SetCellPaddings()
        {
            if (!this.IsImageObject(this.CurrentData.TableCell))
            {
                PaddingInfo padding = base.CurrentStyle.Padding;
                if (padding.Left > 0)
                {
                    this.SetContent(string.Format(RtfTags.LeftIndent, ConvertToTwips((float) padding.Left, padding.Dpi)));
                }
                if (padding.Right > 0)
                {
                    this.SetContent(string.Format(RtfTags.RightIndent, ConvertToTwips((float) padding.Right, padding.Dpi)));
                }
            }
        }

        public override void SetCellStyle()
        {
            this.SetFrameHeader();
            this.SetBorders();
            this.SetFrameColor();
        }

        protected override void SetCellUnion()
        {
            if (!this.ContentEndsWith(RtfTags.ParagraphEnd))
            {
                this.SetContent(RtfTags.ParagraphEnd);
            }
            this.SetContent("}");
        }

        private void SetCommonBorder(BorderSide side, string borderTag, int paddingInTwips)
        {
            if (this.HasRealBorder(side))
            {
                this.SetBorderCore(borderTag, paddingInTwips);
            }
            else if (paddingInTwips > 0)
            {
                this.SetFakeBorder(borderTag, paddingInTwips);
            }
        }

        private void SetCommonBorders(BorderSide sides, PaddingInfo padding)
        {
            this.SetCommonBorder(BorderSide.Top, RtfTags.TopBorder, ConvertToTwips((float) padding.Top, padding.Dpi));
            this.SetCommonBorder(BorderSide.Bottom, RtfTags.BottomBorder, ConvertToTwips((float) padding.Bottom, padding.Dpi));
            this.SetCommonBorder(BorderSide.Left, RtfTags.LeftBorder, ConvertToTwips((float) padding.Left, padding.Dpi));
            this.SetCommonBorder(BorderSide.Right, RtfTags.RightBorder, ConvertToTwips((float) padding.Right, padding.Dpi));
        }

        protected override void SetContent(string content)
        {
            this.stringBuilder.Append(content);
        }

        private void SetFakeBorder(string borderTag, int borderSpace)
        {
            this.SetContent(borderTag);
            this.SetFakeBorderStyle(borderSpace);
        }

        private void SetFakeBorderStyle(int borderSpace)
        {
            this.SetContent(RtfExportBorderHelper.GetFullBorderStyle(BorderDashStyle.Solid, 1, base.GetFakeBorderColorIndex(), borderSpace));
        }

        private void SetFrame()
        {
            this.SetContent(RtfTags.DefaultFrame);
            this.SetContent(RtfTags.RelativeFrameToPage);
        }

        private void SetFrameColor()
        {
            this.SetContent(string.Format(RtfTags.BackgroundPatternColor, base.GetBackColorIndex()));
        }

        private void SetFrameHeader()
        {
            this.SetContent("{");
            this.SetContent(RtfTags.ParagraphDefault);
            this.SetContent(RtfTags.TextUnderneath);
            Rectangle frameBounds = this.GetFrameBounds();
            this.SetContent(string.Format(RtfTags.ObjectBounds, new object[] { frameBounds.Left, frameBounds.Top, frameBounds.Width, frameBounds.Height }));
            this.SetFrame();
            this.SetCellHAlign();
            this.SetCellPaddings();
            base.SetDirection();
        }

        protected override void SetFrameText(string Text)
        {
            this.SetCellFont();
            if (Text == string.Empty)
            {
                this.OverrideFontSizeToPreventDisappearBottomBorder();
            }
            this.SetContent(" ");
            this.SetContent(Text);
        }

        private void SetImageBorder(string borderTag)
        {
            this.SetBorderCore(borderTag, 0);
        }

        private void SetImageBorders(BorderSide sides)
        {
            if (this.HasRealBorder(BorderSide.Top))
            {
                this.SetImageBorder(RtfTags.TopBorder);
            }
            if (this.HasRealBorder(BorderSide.Bottom))
            {
                this.SetImageBorder(RtfTags.BottomBorder);
            }
            if (this.HasRealBorder(BorderSide.Left))
            {
                this.SetImageBorder(RtfTags.LeftBorder);
            }
            if (this.HasRealBorder(BorderSide.Right))
            {
                this.SetImageBorder(RtfTags.RightBorder);
            }
        }

        protected override void SetImageContent(string content)
        {
            this.SetContent(content);
        }

        protected override void WriteContent()
        {
            base.writer.WriteLine(this.stringBuilder.ToString());
        }

        protected override void WriteMargins()
        {
            this.SetContent(string.Format(RtfTags.Margins, new object[] { this.TwipsMargins.Left, this.TwipsMargins.Right, this.TwipsMargins.Top, this.TwipsMargins.Bottom }));
        }

        protected override void WritePageBounds()
        {
            this.SetContent(RtfTags.SectionDefault);
            if (this.currentPage.PageData.Landscape)
            {
                this.SetContent(RtfTags.PageLandscape);
            }
            int num = ConvertToTwips((float) this.currentPage.PageData.Bounds.Width, 100f);
            int num2 = ConvertToTwips((float) this.currentPage.PageData.Bounds.Height, 100f);
            this.SetContent(string.Format(RtfTags.PageSize, num, num2));
            this.WriteMargins();
        }

        private void WriteWatermark()
        {
            if (!ImageSource.IsNullOrEmpty(this.currentPage.ActualWatermark.ImageSource))
            {
                RtfDocumentProviderBase.ImageWatermarkRtfExportProvider provider = new RtfDocumentProviderBase.ImageWatermarkRtfExportProvider(this.currentPage);
                this.SetContent(provider.GetRtfContent(base.ExportContext));
            }
            if (!string.IsNullOrEmpty(this.currentPage.ActualWatermark.Text))
            {
                RtfDocumentProviderBase.TextWatermarkRtfExportProvider provider2 = new RtfDocumentProviderBase.TextWatermarkRtfExportProvider(this.currentPage);
                this.SetContent(provider2.GetRtfContent(base.ExportContext));
            }
        }

        public override BrickViewData CurrentData =>
            (BrickViewData) this.currentInfo;

        private Margins TwipsMargins =>
            GraphicsUnitConverter.Convert(this.currentPage.Margins, 100f, 1440f);

        private class FrameBoundsCorrector
        {
            private const int BorderOutSetCompensator = 30;
            private const int PaddingOutSetCompensator = 15;
            private RectangleF bounds;

            public FrameBoundsCorrector(RectangleF bounds, BorderSide borders, float borderWidth, PaddingInfo padding)
            {
                this.bounds = bounds;
                this.CorrectBounds(borders, borderWidth, padding);
            }

            private unsafe void CorrectBounds(BorderSide borders, float borderWidth, PaddingInfo padding)
            {
                if ((borderWidth > 0f) && borders.HasFlag(BorderSide.Left))
                {
                    float x = (padding.Left > 0) ? (15f + borderWidth) : (30f + borderWidth);
                    this.bounds.Offset(x, 0f);
                    RectangleF* efPtr1 = &this.bounds;
                    efPtr1.Width -= x;
                }
                else if (padding.Left > 0)
                {
                    int num2 = 15;
                    this.bounds.Offset((float) num2, 0f);
                    RectangleF* efPtr2 = &this.bounds;
                    efPtr2.Width -= num2;
                }
                if ((borderWidth > 0f) && borders.HasFlag(BorderSide.Right))
                {
                    RectangleF* efPtr3 = &this.bounds;
                    efPtr3.Width -= (padding.Left > 0) ? (15f + borderWidth) : (30f + borderWidth);
                }
                else if (padding.Right > 0)
                {
                    RectangleF* efPtr4 = &this.bounds;
                    efPtr4.Width -= 15f;
                }
            }

            public RectangleF Bounds =>
                this.bounds;
        }
    }
}

