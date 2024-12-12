namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    internal class HorizontalParagraphLayoutInfoConverter : ParagraphLayoutInfoConverter
    {
        private readonly HorizontalTextCalculator layoutCalculator;

        public HorizontalParagraphLayoutInfoConverter(HorizontalTextCalculator layoutCalculator, List<ParagraphLayoutInfo> paragraphLayouts, ShapeStyle shapeStyle, GraphicsWarpTransformer warpTransformer, bool shouldApplyEffects) : base(paragraphLayouts, shapeStyle, warpTransformer, shouldApplyEffects)
        {
            this.layoutCalculator = layoutCalculator;
            this.StringFormat = new System.Drawing.StringFormat(System.Drawing.StringFormat.GenericTypographic);
        }

        public HorizontalParagraphLayoutInfoConverter(HorizontalTextCalculator layoutCalculator, List<ParagraphLayoutInfo> paragraphLayouts, ShapeStyle shapeStyle, GraphicsWarpTransformer warpTransformer, bool shouldApplyEffects, bool blackAndWhitePrintMode) : base(paragraphLayouts, shapeStyle, warpTransformer, shouldApplyEffects, blackAndWhitePrintMode)
        {
            this.layoutCalculator = layoutCalculator;
            this.StringFormat = new System.Drawing.StringFormat(System.Drawing.StringFormat.GenericTypographic);
        }

        private static void AddDash(List<GraphicsPath> graphicsPaths, float left, float right, int top, int dashWidth, int spaceWidth, int height)
        {
            AddDashDot(graphicsPaths, left, right, top, dashWidth, 0, spaceWidth, height, 0);
        }

        private static void AddDashDot(List<GraphicsPath> graphicsPaths, float left, float right, int top, int dashWidth, int dotWidth, int spaceWidth, int height, int dotsCount)
        {
            float num = left;
            while (num < right)
            {
                AddGraphicsPathWithRectangle(graphicsPaths, num, (float) top, Math.Min((float) dashWidth, right - num), height);
                num += dashWidth + spaceWidth;
                for (int i = 0; (i < dotsCount) && (num < right); i++)
                {
                    AddGraphicsPathWithRectangle(graphicsPaths, num, (float) top, Math.Min((float) dotWidth, right - num), height);
                    num += dotWidth + spaceWidth;
                }
            }
        }

        private static void AddGraphicsPathWithRectangle(List<GraphicsPath> graphicsPaths, float left, float top, float width, int height)
        {
            GraphicsPath item = new GraphicsPath(FillMode.Winding);
            item.AddRectangle(new RectangleF(left, top, width, (float) height));
            graphicsPaths.Add(item);
        }

        protected override void AddUnderlines(List<ParagraphLayoutInfoConverter.UnderlineInfo> underlines, RectangleF paragraphBounds, float excelBaseLine)
        {
            if (underlines.Count != 0)
            {
                RectangleF bounds;
                float num = 0f;
                float num2 = 0f;
                float num3 = 0f;
                float num4 = 0f;
                foreach (ParagraphLayoutInfoConverter.UnderlineInfo info in underlines)
                {
                    num = Math.Max(info.Baseline, num);
                    bounds = info.RunLayoutInfo.Bounds;
                    num2 += bounds.Width;
                }
                foreach (ParagraphLayoutInfoConverter.UnderlineInfo info2 in underlines)
                {
                    RunLayoutInfo runLayoutInfo = info2.RunLayoutInfo;
                    FontInfo runFontInfo = runLayoutInfo.RunFontInfo;
                    int underlineThickness = runFontInfo.UnderlineThickness;
                    num3 += runLayoutInfo.Bounds.Width * underlineThickness;
                    bounds = runLayoutInfo.Bounds;
                    num4 += bounds.Width * (underlineThickness + runFontInfo.UnderlinePosition);
                }
                int realThickness = Math.Max(1, (int) Math.Round((double) (num3 / num2)));
                int underlinePosition = ((int) Math.Round((double) (num4 / num2))) - realThickness;
                float left = underlines[0].RunLayoutInfo.Bounds.Left;
                for (int i = 0; i < underlines.Count; i++)
                {
                    if ((i > 0) && base.AreUnderlinesNotSame(underlines[i - 1], underlines[i]))
                    {
                        ParagraphLayoutInfoConverter.UnderlineInfo underline = underlines[i - 1];
                        this.ProcessUnderline(underline, left, paragraphBounds, excelBaseLine, num, underlinePosition, realThickness);
                        left = underlines[i].RunLayoutInfo.Bounds.Left;
                    }
                }
                this.ProcessUnderline(underlines[underlines.Count - 1], left, paragraphBounds, excelBaseLine, num, underlinePosition, realThickness);
            }
        }

        private void AddWave(List<GraphicsPath> graphicsPaths, float left, float right, float top, int lineLength, int lineWidth)
        {
            GraphicsPath gp = new GraphicsPath(FillMode.Alternate);
            this.AddWave(gp, left, right, top, lineLength, lineWidth);
            gp.CloseFigure();
            graphicsPaths.Add(gp);
        }

        private void AddWave(GraphicsPath gp, float left, float right, float top, int lineLength, int lineWidth)
        {
            List<PointF> list = new List<PointF> {
                new PointF(left, top + 1f)
            };
            int num = (lineLength - lineWidth) + 1;
            while (left < right)
            {
                list.Add(new PointF((left + num) - 2f, (top + num) - 1f));
                list.Add(new PointF((((left + num) - 2f) + num) - 1f, top));
                left += (num - 2) + num;
            }
            gp.AddLines(list.ToArray());
            for (int i = (list.Count - 1) / 2; i >= 0; i--)
            {
                PointF tf = list[i];
                PointF tf2 = list[(list.Count - 1) - i];
                list[i] = new PointF(tf2.X, tf2.Y + lineWidth);
                list[(list.Count - 1) - i] = new PointF(tf.X, tf.Y + lineWidth);
            }
            gp.AddLines(list.ToArray());
        }

        protected override void BeforeCloseBucket(List<ParagraphLayoutInfoConverter.BucketElement> bucketLinesPathInfos)
        {
        }

        protected override float CalcRealBaseLine(RunLayoutInfo runLayout, float excelBaseLine)
        {
            float num = (runLayout.Properties != null) ? ((float) runLayout.Properties.Baseline) : ((float) 0);
            return (runLayout.Bounds.Top + (excelBaseLine - ((num * base.DocumentModel.LayoutUnitConverter.PointsToLayoutUnitsF(runLayout.BaseFontInfo.SizeInPoints)) / 100000f)));
        }

        protected override float CalculateExcelBaseLine(ParagraphLayoutInfo paragraphLayoutInfo)
        {
            float num = 0f;
            foreach (RunLayoutInfo info in paragraphLayoutInfo.RunLayouts)
            {
                num = Math.Max(num, base.CalcExcelBaseLine(info.BaseFontInfo));
            }
            return num;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.StringFormat != null))
            {
                this.StringFormat.Dispose();
                this.StringFormat = null;
            }
            base.Dispose(disposing);
        }

        protected override RectangleF GetBrushPenBounds(RectangleF paragraphBounds, RectangleF runLayoutBounds) => 
            (base.WarpTransformer == null) ? paragraphBounds : new RectangleF(PointF.Empty, this.layoutCalculator.TextRectangle.Size);

        public override float GetCurrentBaseLine() => 
            base.CurrentBucketElement.RunBaseLine;

        protected override GraphicsPath GetRunTextGraphicsPath(RunLayoutInfo runLayout, string text, Font font, float realBaseLine)
        {
            float emSize = base.DocumentModel.LayoutUnitConverter.PointsToLayoutUnitsF(font.SizeInPoints);
            float num2 = base.CalcGdiBaseLine(runLayout.RunFontInfo);
            float left = runLayout.Bounds.Left;
            float y = realBaseLine - num2;
            GraphicsPath path = new GraphicsPath(FillMode.Winding);
            float num5 = base.DocumentModel.LayoutUnitConverter.PointsToLayoutUnitsF((runLayout.Properties != null) ? ((float) runLayout.Properties.Spacing) : ((float) 0)) / 100f;
            bool flag = (runLayout.Properties != null) && runLayout.Properties.NormalizeHeight;
            foreach (char ch in text)
            {
                string str2 = ch.ToString();
                if (!string.IsNullOrWhiteSpace(str2))
                {
                    GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
                    graphicsPath.AddString(str2, font.FontFamily, (int) font.Style, emSize, new PointF(left, y), this.StringFormat);
                    if (flag)
                    {
                        base.NormalizeHeight(graphicsPath, runLayout, realBaseLine);
                    }
                    path.AddPath(graphicsPath, false);
                    graphicsPath.Dispose();
                }
                left += this.layoutCalculator.GetTextWidth(str2, font) + num5;
            }
            return path;
        }

        protected override List<GraphicsPath> GetStrikethroughGraphicsPaths(RunLayoutInfo runLayout, float realBaseLine, GraphicsPath runTextGraphicsPath)
        {
            RectangleF bounds = runLayout.Bounds;
            FontInfo runFontInfo = runLayout.RunFontInfo;
            float top = realBaseLine - runFontInfo.StrikeoutPosition;
            List<GraphicsPath> graphicsPaths = new List<GraphicsPath>();
            int strikeoutThickness = runFontInfo.StrikeoutThickness;
            switch (runLayout.Properties.Strikethrough)
            {
                case DrawingTextStrikeType.None:
                    break;

                case DrawingTextStrikeType.Single:
                    AddGraphicsPathWithRectangle(graphicsPaths, bounds.Left, top, bounds.Width, strikeoutThickness);
                    break;

                case DrawingTextStrikeType.Double:
                    AddGraphicsPathWithRectangle(graphicsPaths, bounds.Left, top + strikeoutThickness, bounds.Width, strikeoutThickness);
                    AddGraphicsPathWithRectangle(graphicsPaths, bounds.Left, top - strikeoutThickness, bounds.Width, strikeoutThickness);
                    break;

                default:
                    throw new ArgumentOutOfRangeException();
            }
            return graphicsPaths;
        }

        private List<GraphicsPath> GetUnderlineGraphicsPaths(DrawingTextUnderlineType underlineType, float left, float right, float textBaseLine, int underlinePosition, int underlineThickness)
        {
            List<GraphicsPath> graphicsPaths = new List<GraphicsPath>();
            int lineWidth = (underlineThickness + 1) >> 1;
            int height = underlineThickness + lineWidth;
            int top = ((int) textBaseLine) + underlinePosition;
            int num4 = top - underlineThickness;
            int dashWidth = underlineThickness * 3;
            int spaceWidth = underlineThickness * 2;
            int num7 = underlineThickness * 7;
            int num8 = underlineThickness * 4;
            float width = right - left;
            switch (underlineType)
            {
                case DrawingTextUnderlineType.Words:
                case DrawingTextUnderlineType.Single:
                    AddGraphicsPathWithRectangle(graphicsPaths, left, (float) top, width, underlineThickness);
                    break;

                case DrawingTextUnderlineType.Double:
                {
                    GraphicsPath item = new GraphicsPath(FillMode.Winding);
                    item.AddRectangle(new RectangleF(left, (float) num4, width, (float) lineWidth));
                    item.AddRectangle(new RectangleF(left, (float) ((top + underlineThickness) - lineWidth), width, (float) lineWidth));
                    graphicsPaths.Add(item);
                    break;
                }
                case DrawingTextUnderlineType.Heavy:
                    AddGraphicsPathWithRectangle(graphicsPaths, left, (float) num4, width, height);
                    break;

                case DrawingTextUnderlineType.Dotted:
                    AddDash(graphicsPaths, left, right, top, underlineThickness, underlineThickness, underlineThickness);
                    break;

                case DrawingTextUnderlineType.HeavyDotted:
                    AddDash(graphicsPaths, left, right, num4, underlineThickness, underlineThickness, height);
                    break;

                case DrawingTextUnderlineType.Dashed:
                    AddDash(graphicsPaths, left, right, top, dashWidth, spaceWidth, underlineThickness);
                    break;

                case DrawingTextUnderlineType.HeavyDashed:
                    AddDash(graphicsPaths, left, right, num4, dashWidth, spaceWidth, height);
                    break;

                case DrawingTextUnderlineType.LongDashed:
                    AddDash(graphicsPaths, left, right, top, num7, num8, underlineThickness);
                    break;

                case DrawingTextUnderlineType.HeavyLongDashed:
                    AddDash(graphicsPaths, left, right, num4, num7, num8, height);
                    break;

                case DrawingTextUnderlineType.DotDash:
                    AddDashDot(graphicsPaths, left, right, top, dashWidth, underlineThickness, spaceWidth, underlineThickness, 1);
                    break;

                case DrawingTextUnderlineType.HeavyDotDash:
                    AddDashDot(graphicsPaths, left, right, num4, dashWidth, underlineThickness, spaceWidth, height, 1);
                    break;

                case DrawingTextUnderlineType.DotDotDash:
                    AddDashDot(graphicsPaths, left, right, top, dashWidth, underlineThickness, spaceWidth, underlineThickness, 2);
                    break;

                case DrawingTextUnderlineType.HeavyDotDotDash:
                    AddDashDot(graphicsPaths, left, right, num4, dashWidth, underlineThickness, spaceWidth, height, 2);
                    break;

                case DrawingTextUnderlineType.Wavy:
                    this.AddWave(graphicsPaths, left, right, (float) (top - lineWidth), spaceWidth, 1);
                    break;

                case DrawingTextUnderlineType.HeavyWavy:
                    this.AddWave(graphicsPaths, left, right, (float) (top - lineWidth), spaceWidth, lineWidth);
                    break;

                case DrawingTextUnderlineType.DoubleWavy:
                {
                    GraphicsPath gp = new GraphicsPath(FillMode.Winding);
                    this.AddWave(gp, left, right, (float) num4, underlineThickness, 1);
                    gp.CloseFigure();
                    this.AddWave(gp, left, right, (float) top, underlineThickness, 1);
                    gp.CloseFigure();
                    graphicsPaths.Add(gp);
                    break;
                }
                default:
                    break;
            }
            return graphicsPaths;
        }

        private void ProcessUnderline(ParagraphLayoutInfoConverter.UnderlineInfo underline, float left, RectangleF paragraphBounds, float excelBaseLine, float baseline, int underlinePosition, int realThickness)
        {
            RunLayoutInfo runLayoutInfo = underline.RunLayoutInfo;
            Brush fill = GetUnderlineBrush(runLayoutInfo.Properties, paragraphBounds, base.ShapeStyle, underline.TextBrush);
            PenInfo penInfo = underline.PenInfo;
            ContainerEffect effects = runLayoutInfo.Properties.Effects;
            RectangleF bounds = runLayoutInfo.Bounds;
            float right = bounds.Right;
            foreach (GraphicsPath path in this.GetUnderlineGraphicsPaths(runLayoutInfo.Properties.Underline, left, right, baseline, underlinePosition, realThickness))
            {
                TextLinePathInfo runPathInfo = new TextLinePathInfo(path, fill, penInfo);
                RectangleF ef2 = new RectangleF(left, bounds.Top, right - left, bounds.Height);
                base.BucketLinesPathInfos.Add(new ParagraphLayoutInfoConverter.BucketElement(runPathInfo, ef2, effects, excelBaseLine));
            }
        }

        private System.Drawing.StringFormat StringFormat { get; set; }
    }
}

