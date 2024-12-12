namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Utils;
    using DevExpress.Utils.Serializing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native.TOC;
    using System;
    using System.Drawing;

    [BrickExporter(typeof(TableOfContentsLineBrickExporter))]
    public class TableOfContentsLineBrick : PanelBrick
    {
        private string caption;
        private float textDivisionRatio;
        private char leaderSymbol;

        public TableOfContentsLineBrick() : this(NullBrickOwner.Instance)
        {
        }

        public TableOfContentsLineBrick(IBrickOwner brickOwner) : base(brickOwner)
        {
            this.textDivisionRatio = 1f;
            Guard.ArgumentNotNull(brickOwner, "brickOwner");
            this.caption = brickOwner.Text;
        }

        private TableOfContentsLineBrick(TableOfContentsLineBrick tocLineBrick) : base(tocLineBrick)
        {
            this.textDivisionRatio = 1f;
            this.caption = tocLineBrick.Caption;
            this.textDivisionRatio = tocLineBrick.TextDivisionRatio;
        }

        public override object Clone() => 
            new TableOfContentsLineBrick(this);

        protected virtual ITextGenerator CreateTextGenerator(Measurer measurer, GraphicsUnit graphicsUnit, char leaderSymbol, Font font, BrickStyle style, string caption, float textDivisionPosition)
        {
            TableOfContentsLineTextGenerator generator1 = new TableOfContentsLineTextGenerator();
            generator1.Measurer = measurer;
            generator1.GraphicsUnit = graphicsUnit;
            generator1.LeaderSymbol = leaderSymbol;
            generator1.Font = font;
            generator1.Style = style;
            generator1.Caption = caption;
            generator1.TextDivisionPosition = textDivisionPosition;
            return generator1;
        }

        protected virtual Page GetAssociatedPage(PageList pages) => 
            this.NavigationPair?.GetPage(pages);

        private float GetMaxCaptionTextWidth() => 
            GraphicsUnitConverter.DipToDoc((float) Math.Floor((double) GraphicsUnitConverter.DocToDip(this.CaptionBrick.Padding.DeflateWidth(this.CaptionBrick.Width, 300f))));

        private unsafe PaddingInfo GetPaddingsWithBorder(BorderSide sides, int borderWidth, float dpi)
        {
            PaddingInfo info = new PaddingInfo(base.Padding, dpi);
            if (sides.HasFlag(BorderSide.Left))
            {
                PaddingInfo* infoPtr1 = &info;
                infoPtr1.Left += borderWidth;
            }
            if (sides.HasFlag(BorderSide.Right))
            {
                PaddingInfo* infoPtr2 = &info;
                infoPtr2.Right += borderWidth;
            }
            if (sides.HasFlag(BorderSide.Top))
            {
                PaddingInfo* infoPtr3 = &info;
                infoPtr3.Top += borderWidth;
            }
            if (sides.HasFlag(BorderSide.Bottom))
            {
                PaddingInfo* infoPtr4 = &info;
                infoPtr4.Bottom += borderWidth;
            }
            return info;
        }

        private float GetPageNumberBrickWidth(string pageNumberText, Measurer measurer)
        {
            SizeF ef = measurer.MeasureString(pageNumberText, this.PageNumberBrick.Font, (PointF) Point.Empty, this.PageNumberBrick.StringFormat.Value, GraphicsUnit.Document);
            return GraphicsUnitConverter.DipToDoc((float) Math.Ceiling((double) GraphicsUnitConverter.DocToDip(this.PageNumberBrick.Padding.InflateWidth(ef.Width, 300f))));
        }

        private void Initialize()
        {
            base.Style.StringFormat = new BrickStringFormat(base.Style.StringFormat.ChangeFormatFlags(base.Style.StringFormat.FormatFlags | StringFormatFlags.MeasureTrailingSpaces), StringTrimming.Word);
            if (this.Bricks.Count == 0)
            {
                this.Bricks.Add(new TextBrick());
                this.Bricks.Add(new TextBrick());
                PaddingInfo info = this.GetPaddingsWithBorder(base.Sides, (int) base.BorderWidth, base.Padding.Dpi);
                this.InitializeCaptionBrick(new PaddingInfo(info.Left, 0, info.Top, info.Bottom, info.Dpi));
                this.InitializePageNumberBrick(new PaddingInfo(0, info.Right, info.Top, info.Bottom, info.Dpi));
            }
            base.Padding = new PaddingInfo(0, base.Padding.Dpi);
        }

        private void InitializeCaptionBrick(PaddingInfo paddingWithBorder)
        {
            BrickStyle style1 = new BrickStyle(base.Style);
            style1.TextAlignment = TextAlignment.BottomLeft;
            style1.BorderWidth = 0f;
            style1.Padding = new PaddingInfo(paddingWithBorder, this.CaptionBrick.Padding.Dpi);
            style1.StringFormat = new BrickStringFormat(base.Style.StringFormat, StringAlignment.Near, StringAlignment.Far);
            BrickStyle style = style1;
            this.CaptionBrick.Style = style;
            this.CaptionBrick.NavigationPair = this.NavigationPair;
            this.CaptionBrick.Target = base.Target;
        }

        private void InitializePageNumberBrick(PaddingInfo paddingWithBorder)
        {
            BrickStyle style1 = new BrickStyle(base.Style);
            style1.TextAlignment = TextAlignment.BottomRight;
            style1.BorderWidth = 0f;
            style1.Padding = new PaddingInfo(paddingWithBorder, this.PageNumberBrick.Padding.Dpi);
            style1.StringFormat = new BrickStringFormat(base.Style.StringFormat, StringAlignment.Far, StringAlignment.Far);
            BrickStyle style = style1;
            this.PageNumberBrick.Style = style;
            this.PageNumberBrick.NavigationPair = this.NavigationPair;
            this.PageNumberBrick.Target = base.Target;
        }

        private bool IsDeserializedDocument() => 
            this.PrintingSystem.Document is DeserializedDocument;

        protected override void OnSetPrintingSystem(bool cacheStyle)
        {
            if (!ReferenceEquals(base.BrickOwner, NullBrickOwner.Instance) || this.IsDeserializedDocument())
            {
                float num3;
                base.OnSetPrintingSystem(cacheStyle);
                this.Initialize();
                IPageInfoFormatProvider printingSystem = this.PrintingSystem;
                string text = Page.ConvertPageNumberValueToString(this.PrintingSystem.Pages.GetPageIndexByID(this.NavigationPair.PageID) + printingSystem.StartPageNumber, printingSystem.PageInfo, printingSystem.Format);
                Measurer measurer = ((IPrintingSystemContext) this.PrintingSystem).Measurer;
                SizeF ef = measurer.MeasureString(text, base.Style.Font, (PointF) Point.Empty, base.Style.StringFormat.Value, GraphicsUnit.Document);
                float width = (float) Math.Ceiling((double) this.PageNumberBrick.Padding.InflateWidth(ef.Width, 300f));
                if (width <= base.Width)
                {
                    num3 = base.Width - width;
                }
                else
                {
                    width = 0f;
                    num3 = 0f;
                    this.PageNumberBrick.IsVisible = false;
                    this.CaptionBrick.IsVisible = false;
                }
                float x = num3;
                num3 = this.CaptionBrick.Padding.DeflateWidth(num3, 300f);
                float num5 = this.CaptionBrick.Padding.InflateHeight(measurer.MeasureString(this.Caption, base.Style.Font, num3, base.Style.StringFormat.Value, GraphicsUnit.Document).Height, 300f) + 1f;
                if (num5 > base.Height)
                {
                    base.Height = num5;
                }
                this.InitialRect = new RectangleF(this.InitialRect.X, this.InitialRect.Y, this.InitialRect.Width, base.Height);
                this.CaptionBrick.InitialRect = new RectangleF(this.CaptionBrick.InitialRect.X, this.CaptionBrick.InitialRect.Y, num3, base.Height);
                this.PageNumberBrick.InitialRect = new RectangleF(x, this.PageNumberBrick.InitialRect.Y, width, base.Height);
                this.CaptionBrick.Text = this.CreateTextGenerator(measurer, this.PrintingSystem.Graph.PageUnit, this.LeaderSymbol, base.Style.Font, base.Style, this.Caption, this.TextDivisionPosition).GenerateText(this.CaptionBrick.Width);
                this.PageNumberBrick.Text = text;
            }
        }

        protected internal override void PerformLayout(IPrintingSystemContext context)
        {
            IPageInfoFormatProvider printingSystem = context.PrintingSystem;
            string pageNumberText = Page.ConvertPageNumberValueToString(context.PrintingSystem.Pages.GetPageIndexByID(this.NavigationPair.PageID) + printingSystem.StartPageNumber, printingSystem.PageInfo, printingSystem.Format);
            if (!ReferenceEquals(base.BrickOwner, NullBrickOwner.Instance) || this.IsDeserializedDocument())
            {
                if (!ReferenceEquals(this.NavigationPair, BrickPagePair.Empty) && string.IsNullOrEmpty(base.Url))
                {
                    string tagByIndices = DocumentMapTreeViewNodeHelper.GetTagByIndices(BrickPagePairHelper.IndicesFromArray(this.NavigationPair.BrickIndices), this.NavigationPair.PageIndex);
                    VisualBrick brick = this.NavigationPair.GetBrick(context.PrintingSystem.Pages) as VisualBrick;
                    if (brick != null)
                    {
                        if (string.IsNullOrEmpty(brick.AnchorName))
                        {
                            brick.AnchorName = tagByIndices;
                        }
                        else
                        {
                            tagByIndices = brick.AnchorName;
                        }
                    }
                    base.Url = tagByIndices;
                    this.CaptionBrick.Url = tagByIndices;
                    this.PageNumberBrick.Url = tagByIndices;
                }
                Measurer measurer = context.Measurer;
                ITextGenerator generator = this.CreateTextGenerator(measurer, this.PrintingSystem.Graph.PageUnit, this.LeaderSymbol, base.Style.Font, base.Style, this.Caption, this.TextDivisionPosition);
                this.PageNumberBrick.Width = this.GetPageNumberBrickWidth(pageNumberText, measurer);
                this.PageNumberBrick.X = base.Width - this.PageNumberBrick.Width;
                this.CaptionBrick.Width = base.Width - this.PageNumberBrick.Width;
                this.CaptionBrick.Text = generator.GenerateText(this.GetMaxCaptionTextWidth());
                this.PageNumberBrick.Text = pageNumberText;
                base.PerformLayout(context);
            }
        }

        [XtraSerializableProperty]
        public char LeaderSymbol
        {
            get => 
                this.leaderSymbol;
            set => 
                this.leaderSymbol = value;
        }

        public override string BrickType =>
            "TableOfContentsLine";

        public override string Text
        {
            get => 
                this.CaptionBrick.Text + this.PageNumberBrick.Text;
            set
            {
                throw new NotSupportedException("Text");
            }
        }

        public float TextDivisionRatio
        {
            get => 
                this.textDivisionRatio;
            set
            {
                if ((value <= 0f) || (value > 1f))
                {
                    throw new ArgumentOutOfRangeException("TextDivisionRatio");
                }
                this.textDivisionRatio = value;
            }
        }

        internal TextBrick CaptionBrick =>
            (TextBrick) this.Bricks[0];

        internal TextBrick PageNumberBrick =>
            (TextBrick) this.Bricks[1];

        [XtraSerializableProperty]
        public string Caption
        {
            get => 
                this.caption;
            set => 
                this.caption = value;
        }

        private float TextDivisionPosition =>
            this.CaptionBrick.Width * this.TextDivisionRatio;

        public override BrickPagePair NavigationPair
        {
            get => 
                base.NavigationPair;
            set
            {
                base.NavigationPair = value;
                base.Url = string.Empty;
            }
        }
    }
}

