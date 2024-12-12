namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    internal class WordArtVerticalTextCalculator : TextLayoutCalculator
    {
        public const float VerticalTextKoef = 1.1655f;

        public WordArtVerticalTextCalculator(TextRendererContext textRendererContext) : base(textRendererContext)
        {
            this.Graphics = base.CreateTempGraphics();
        }

        protected override void AddBulletCore(FontInfo bulletFont, string text, DrawingColor color, IDrawingFill fill)
        {
            RunLayoutInfo info = new RunLayoutInfo(null, bulletFont, bulletFont, text) {
                Bounds = new RectangleF(base.X, base.Y, this.GetRunWidth(bulletFont.Font, text), this.GetRunGrowthDimensionCore(text, bulletFont))
            };
            base.CurrentLine.Bullet = info;
            base.CurrentLine.BulletColor = color;
            base.CurrentLine.BulletFill = fill;
            this.Y = Math.Max(base.Y + info.Bounds.Height, base.CurrentLine.ParagraphProperties.HasLeftMargin ? base.DocumentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits(base.CurrentLine.ParagraphProperties.Margin.Left) : 0f);
        }

        protected override void AddIndent(float indent)
        {
            base.Y += indent;
        }

        protected override void AddRunLayout(IDrawingTextCharacterProperties runProperties, FontInfo runFontInfo, FontInfo baseFontInfo, string text, float growthDimension)
        {
            float runWidth = this.GetRunWidth(baseFontInfo.Font, text);
            if (this.CanAddLine(runWidth) || base.TryGotoNextColumn())
            {
                RunLayoutInfo item = new RunLayoutInfo(runProperties, runFontInfo, baseFontInfo, text) {
                    Bounds = new RectangleF(base.X, base.Y, runWidth, growthDimension)
                };
                base.CurrentLine.RunLayouts.Add(item);
                base.Y += item.Bounds.Height;
            }
        }

        protected override void AddSpacing(float spacing)
        {
            base.X += spacing;
        }

        protected override void ApplyParagraphAlignment(RunLayoutInfo runLayout, IDrawingTextParagraphProperties paragraphProperties)
        {
            float y = 0f;
            switch (paragraphProperties.TextAlignment)
            {
                case DrawingTextAlignmentType.Center:
                    y = (base.ColumnRectangle.Height / 2f) - (base.Y / 2f);
                    break;

                case DrawingTextAlignmentType.Right:
                    y = base.ColumnRectangle.Height - base.Y;
                    break;

                default:
                    break;
            }
            runLayout.OffsetY(y);
        }

        protected override float CalcSpacing(ParagraphLayoutInfo paragraphLayoutInfo)
        {
            float num = 0f;
            foreach (RunLayoutInfo info in paragraphLayoutInfo.RunLayouts)
            {
                RectangleF bounds = info.Bounds;
                num = Math.Max(num, bounds.Width);
            }
            return num;
        }

        protected override RectangleF[] CalculateColumnsBounds(RectangleF textOnlyRectangle, int columnsCount, float spaceBetween)
        {
            RectangleF[] efArray = new RectangleF[columnsCount];
            float y = 0f;
            float height = (textOnlyRectangle.Height - ((columnsCount - 1) * spaceBetween)) / ((float) columnsCount);
            for (int i = 0; i < (efArray.Length - 1); i++)
            {
                efArray[i] = new RectangleF(0f, y, textOnlyRectangle.Width, height);
                y += height + spaceBetween;
            }
            efArray[columnsCount - 1] = new RectangleF(0f, y, textOnlyRectangle.Width, textOnlyRectangle.Height - y);
            return efArray;
        }

        protected virtual bool CanAddLine(float runWidth) => 
            (base.X + runWidth) < base.ColumnRectangle.Width;

        protected override bool CanAddSymbolToCurrentLine(float growthDimension)
        {
            float num = base.CurrentLine.ParagraphProperties.HasRightMargin ? base.DocumentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits(base.CurrentLine.ParagraphProperties.Margin.Right) : 0f;
            return ((base.Y + growthDimension) <= (base.ColumnRectangle.Bottom - num));
        }

        protected override void CloseLineCore()
        {
            if (!base.StopCalculateLayout)
            {
                float num = 0f;
                List<RunLayoutInfo> collection = new List<RunLayoutInfo>();
                foreach (RunLayoutInfo info in base.CurrentLine.RunLayouts)
                {
                    RectangleF bounds = info.Bounds;
                    num = Math.Max(num, bounds.Width);
                    if (info.Text.Length <= 1)
                    {
                        collection.Add(info);
                        continue;
                    }
                    RectangleF ef2 = info.Bounds;
                    float height = ef2.Height / ((float) info.Text.Length);
                    float num3 = 0f;
                    foreach (char ch in info.Text)
                    {
                        RunLayoutInfo item = new RunLayoutInfo(info.Properties, info.RunFontInfo, info.BaseFontInfo, ch.ToString()) {
                            Bounds = new RectangleF(ef2.Left, ef2.Top + num3, ef2.Width, height)
                        };
                        collection.Add(item);
                        num3 += height;
                    }
                }
                foreach (RunLayoutInfo info3 in base.CurrentLine.RunLayouts)
                {
                    RectangleF bounds = info3.Bounds;
                    info3.Bounds = new RectangleF(bounds.Left, bounds.Top, num, bounds.Height);
                }
                base.CurrentLine.RunLayouts.Clear();
                base.CurrentLine.RunLayouts.AddRange(collection);
                base.X = this.GetNextLineLeft(num);
                base.Y = base.ColumnRectangle.Top;
            }
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
            base.Y;

        public override ParagraphLayoutInfoConverter GetLayoutInfoConverter() => 
            new WordArtVerticalParagraphLayoutInfoConverter(this, base.ParagraphLayoutInfos, base.ShapeStyle, (base.WarpGeometry == null) ? null : new GraphicsWarpTransformer(base.DocumentModel, base.WarpGeometry), base.ShouldApplyEffects, base.TextRectangle.Width, base.TextRectangle.Height);

        protected virtual float GetNextLineLeft(float lineWidth) => 
            base.X + lineWidth;

        protected override float GetRunGrowthDimensionCore(string text, FontInfo fontInfo) => 
            (fontInfo.Font.GetHeight(this.Graphics) * text.Length) * 1.1655f;

        private float GetRunWidth(Font font, string text)
        {
            float num = 0f;
            foreach (char ch in text)
            {
                SizeF ef = this.Graphics.MeasureString(ch.ToString(), font, PointF.Empty, base.StringFormat);
                num = Math.Max(num, ef.Width);
            }
            return Math.Max(num, font.GetHeight(this.Graphics) * 1.1655f);
        }

        protected override void OffsetRunLayoutSpacing(RunLayoutInfo runLayout, float spacing)
        {
            runLayout.OffsetX(spacing);
        }

        protected override void SetupStartingPosition()
        {
            base.X = base.ColumnRectangle.Left;
            base.Y = base.ColumnRectangle.Top;
        }

        protected override void StartLineCore(float marL)
        {
            base.Y += marL;
            if (base.Y < 0f)
            {
                base.Y = 0f;
            }
        }

        public System.Drawing.Graphics Graphics { get; set; }
    }
}

