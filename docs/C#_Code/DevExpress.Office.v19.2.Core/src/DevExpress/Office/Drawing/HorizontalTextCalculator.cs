namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class HorizontalTextCalculator : TextLayoutCalculator
    {
        public HorizontalTextCalculator(TextRendererContext textRendererContext) : base(textRendererContext)
        {
            this.Graphics = base.CreateTempGraphics();
            base.DisallowSpacesOnNewLine = true;
        }

        protected override void AddBulletCore(FontInfo bulletFont, string text, DrawingColor color, IDrawingFill fill)
        {
            RunLayoutInfo info = new RunLayoutInfo(null, bulletFont, bulletFont, text) {
                Bounds = new RectangleF(base.X, base.Y, this.GetRunGrowthDimensionCore(text, bulletFont), this.GetRunHeight(bulletFont))
            };
            base.CurrentLine.Bullet = info;
            base.CurrentLine.BulletColor = color;
            base.CurrentLine.BulletFill = fill;
            this.X = Math.Max(base.X + info.Bounds.Width, base.CurrentLine.ParagraphProperties.HasLeftMargin ? Math.Max(base.BulletIndent, base.DocumentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits(base.CurrentLine.ParagraphProperties.Margin.Left)) : 0f);
        }

        protected override void AddIndent(float indent)
        {
            base.X += indent;
        }

        protected override void AddRunLayout(IDrawingTextCharacterProperties runProperties, FontInfo runFontInfo, FontInfo baseFontInfo, string text, float growthDimension)
        {
            float runHeight = this.GetRunHeight(baseFontInfo);
            if (((base.Y + runHeight) < base.ColumnRectangle.Height) || ((base.ParagraphLayoutInfos.Count <= 0) || base.TryGotoNextColumn()))
            {
                RunLayoutInfo item = new RunLayoutInfo(runProperties, runFontInfo, baseFontInfo, text) {
                    Bounds = new RectangleF(base.X, base.Y, growthDimension, runHeight)
                };
                base.CurrentLine.RunLayouts.Add(item);
                base.X += item.Bounds.Width;
            }
        }

        protected override void AddSpacing(float spacing)
        {
            base.Y += spacing;
        }

        protected override void ApplyParagraphAlignment(RunLayoutInfo runLayout, IDrawingTextParagraphProperties paragraphProperties)
        {
            float x = 0f;
            switch (paragraphProperties.TextAlignment)
            {
                case DrawingTextAlignmentType.Center:
                    x = (base.ColumnRectangle.Width / 2f) - (base.X / 2f);
                    break;

                case DrawingTextAlignmentType.Right:
                    x = base.ColumnRectangle.Width - base.X;
                    break;

                default:
                    break;
            }
            runLayout.OffsetX(x);
        }

        protected override float CalcSpacing(ParagraphLayoutInfo paragraphLayoutInfo)
        {
            float num = 0f;
            foreach (RunLayoutInfo info in paragraphLayoutInfo.RunLayouts)
            {
                RectangleF bounds = info.Bounds;
                num = Math.Max(num, bounds.Height);
            }
            return num;
        }

        protected override RectangleF[] CalculateColumnsBounds(RectangleF textOnlyRectangle, int columnsCount, float spaceBetween)
        {
            RectangleF[] efArray = new RectangleF[columnsCount];
            float x = 0f;
            float width = (textOnlyRectangle.Width - ((columnsCount - 1) * spaceBetween)) / ((float) columnsCount);
            for (int i = 0; i < (efArray.Length - 1); i++)
            {
                efArray[i] = new RectangleF(x, 0f, width, textOnlyRectangle.Height);
                x += width + spaceBetween;
            }
            efArray[columnsCount - 1] = new RectangleF(x, 0f, textOnlyRectangle.Width - x, textOnlyRectangle.Height);
            return efArray;
        }

        protected override bool CanAddSymbolToCurrentLine(float growthDimension)
        {
            float num = base.CurrentLine.ParagraphProperties.HasRightMargin ? base.DocumentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits(base.CurrentLine.ParagraphProperties.Margin.Right) : 0f;
            return ((base.X + growthDimension) <= (base.ColumnRectangle.Right - num));
        }

        protected override void CloseLineCore()
        {
            RectangleF bounds;
            float height;
            if (base.CurrentLine.Bullet == null)
            {
                height = 0f;
            }
            else
            {
                bounds = base.CurrentLine.Bullet.Bounds;
                height = bounds.Height;
            }
            float num = height;
            foreach (RunLayoutInfo info in base.CurrentLine.RunLayouts)
            {
                bounds = info.Bounds;
                num = Math.Max(num, bounds.Height);
            }
            base.X = base.ColumnRectangle.Left;
            base.Y += num;
        }

        public override void Dispose()
        {
            base.Dispose();
            if (this.Graphics != null)
            {
                this.Graphics.Dispose();
                this.Graphics = null;
            }
        }

        protected override float GetGrowthDimension() => 
            base.X;

        public override ParagraphLayoutInfoConverter GetLayoutInfoConverter() => 
            new HorizontalParagraphLayoutInfoConverter(this, base.ParagraphLayoutInfos, base.ShapeStyle, (base.WarpGeometry == null) ? null : new GraphicsWarpTransformer(base.DocumentModel, base.WarpGeometry), base.ShouldApplyEffects, base.BlackAndWhitePrintMode);

        protected override float GetRunGrowthDimensionCore(string text, FontInfo fontInfo) => 
            this.GetTextWidth(text, fontInfo.Font);

        private float GetRunHeight(FontInfo fontInfo) => 
            fontInfo.Font.GetHeight(this.Graphics);

        public float GetTextWidth(string text, Font font)
        {
            float num = 0f;
            foreach (char ch in text)
            {
                SizeF ef = this.Graphics.MeasureString(ch.ToString(), font, PointF.Empty, base.StringFormat);
                num += ef.Width;
            }
            return num;
        }

        protected override void OffsetRunLayoutSpacing(RunLayoutInfo runLayout, float spacing)
        {
            runLayout.OffsetY(spacing);
        }

        protected override void SetupStartingPosition()
        {
            base.X = base.ColumnRectangle.Left;
            base.Y = base.ColumnRectangle.Top;
        }

        protected override void StartLineCore(float marL)
        {
            base.X += marL;
            if (base.X < 0f)
            {
                base.X = 0f;
            }
        }

        protected System.Drawing.Graphics Graphics { get; set; }
    }
}

