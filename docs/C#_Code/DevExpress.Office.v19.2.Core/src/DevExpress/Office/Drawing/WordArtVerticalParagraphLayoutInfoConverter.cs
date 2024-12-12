namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;

    internal class WordArtVerticalParagraphLayoutInfoConverter : ParagraphLayoutInfoConverter
    {
        private readonly WordArtVerticalTextCalculator layoutCalculator;
        private Matrix wrapMatrix;

        public WordArtVerticalParagraphLayoutInfoConverter(WordArtVerticalTextCalculator layoutCalculator, List<ParagraphLayoutInfo> paragraphLayouts, ShapeStyle shapeStyle, GraphicsWarpTransformer warpTransformer, bool shouldApplyEffects, float width, float height) : base(paragraphLayouts, shapeStyle, warpTransformer, shouldApplyEffects)
        {
            this.layoutCalculator = layoutCalculator;
            this.StringFormat = new System.Drawing.StringFormat(System.Drawing.StringFormat.GenericTypographic);
            this.wrapMatrix = new Matrix();
            this.wrapMatrix.RotateAt(-90f, new PointF(width / 2f, height / 2f));
        }

        private static void AddDash(List<GraphicsPath> graphicsPaths, float top, float bottom, int left, int dashHeight, int spaceHeight, int width)
        {
            AddDashDot(graphicsPaths, top, bottom, left, dashHeight, 0, spaceHeight, width, 0);
        }

        private static void AddDashDot(List<GraphicsPath> graphicsPaths, float top, float bottom, int left, int dashHeight, int dotWidth, int spaceHeight, int width, int dotsCount)
        {
            float num = top;
            while (num < bottom)
            {
                AddGraphicsPathWithRectangle(graphicsPaths, (float) left, num, (float) width, Math.Min((float) dashHeight, bottom - num));
                num += dashHeight + spaceHeight;
                for (int i = 0; (i < dotsCount) && (num < bottom); i++)
                {
                    AddGraphicsPathWithRectangle(graphicsPaths, (float) left, num, (float) width, Math.Min((float) dotWidth, bottom - num));
                    num += dotWidth + spaceHeight;
                }
            }
        }

        private static void AddGraphicsPathWithRectangle(List<GraphicsPath> graphicsPaths, float left, float top, float width, float height)
        {
            GraphicsPath item = new GraphicsPath(FillMode.Winding);
            item.AddRectangle(new RectangleF(left, top, width, height));
            graphicsPaths.Add(item);
        }

        protected override void AddUnderlines(List<ParagraphLayoutInfoConverter.UnderlineInfo> underlines, RectangleF paragraphBounds, float excelBaseLine)
        {
            if (underlines.Count != 0)
            {
                RectangleF bounds;
                float maxValue = float.MaxValue;
                float num2 = 0f;
                float num3 = 0f;
                float num4 = 0f;
                foreach (ParagraphLayoutInfoConverter.UnderlineInfo info in underlines)
                {
                    RunLayoutInfo runLayoutInfo = info.RunLayoutInfo;
                    maxValue = Math.Min(maxValue, info.GraphicsPath.GetBounds().Left);
                    bounds = runLayoutInfo.Bounds;
                    num2 += bounds.Height;
                }
                foreach (ParagraphLayoutInfoConverter.UnderlineInfo info3 in underlines)
                {
                    RunLayoutInfo runLayoutInfo = info3.RunLayoutInfo;
                    FontInfo runFontInfo = runLayoutInfo.RunFontInfo;
                    int underlineThickness = runFontInfo.UnderlineThickness;
                    num3 += runLayoutInfo.Bounds.Height * underlineThickness;
                    bounds = runLayoutInfo.Bounds;
                    num4 += bounds.Height * (underlineThickness + runFontInfo.UnderlinePosition);
                }
                int realThickness = Math.Max(1, (int) Math.Round((double) (num3 / num2)));
                int underlinePosition = ((int) Math.Round((double) (num4 / num2))) - realThickness;
                float top = underlines[0].RunLayoutInfo.Bounds.Top;
                for (int i = 0; i < underlines.Count; i++)
                {
                    if ((i > 0) && base.AreUnderlinesNotSame(underlines[i - 1], underlines[i]))
                    {
                        ParagraphLayoutInfoConverter.UnderlineInfo underline = underlines[i - 1];
                        this.ProcessUnderline(underline, top, underlines[0].RunLayoutInfo.Bounds, excelBaseLine, maxValue, underlinePosition, realThickness);
                        top = underlines[i].RunLayoutInfo.Bounds.Top;
                    }
                }
                this.ProcessUnderline(underlines[underlines.Count - 1], top, underlines[0].RunLayoutInfo.Bounds, excelBaseLine, maxValue, underlinePosition, realThickness);
            }
        }

        private void AddWave(List<GraphicsPath> graphicsPaths, float top, float bottom, float left, int lineLength, int lineWidth)
        {
            GraphicsPath gp = new GraphicsPath(FillMode.Alternate);
            this.AddWave(gp, top, bottom, left, lineLength, lineWidth);
            gp.CloseFigure();
            graphicsPaths.Add(gp);
        }

        private void AddWave(GraphicsPath gp, float top, float bottom, float left, int lineLength, int lineWidth)
        {
            int num = (lineLength - lineWidth) + 1;
            List<PointF> list = new List<PointF> {
                new PointF((left + num) - 1f, top)
            };
            while (top < bottom)
            {
                list.Add(new PointF(left, (top + num) - 2f));
                list.Add(new PointF((left + num) - 1f, (((top + num) - 2f) + num) - 1f));
                top += (num - 2) + num;
            }
            gp.AddLines(list.ToArray());
            for (int i = (list.Count - 1) / 2; i >= 0; i--)
            {
                PointF tf = list[i];
                PointF tf2 = list[(list.Count - 1) - i];
                list[i] = new PointF(tf2.X + lineWidth, tf2.Y);
                list[(list.Count - 1) - i] = new PointF(tf.X + lineWidth, tf.Y);
            }
            gp.AddLines(list.ToArray());
        }

        protected override void BeforeCloseBucket(List<ParagraphLayoutInfoConverter.BucketElement> bucket)
        {
            if (base.WarpTransformer != null)
            {
                foreach (ParagraphLayoutInfoConverter.BucketElement element in bucket)
                {
                    element.RunPathInfo.GraphicsPath.Transform(this.wrapMatrix);
                }
            }
        }

        protected override float CalcRealBaseLine(RunLayoutInfo runLayout, float excelBaseLine) => 
            excelBaseLine;

        protected override float CalculateExcelBaseLine(ParagraphLayoutInfo paragraphLayoutInfo)
        {
            float num = 0f;
            foreach (RunLayoutInfo info in paragraphLayoutInfo.RunLayouts)
            {
                num = Math.Max(num, base.CalcGdiBaseLine(info.BaseFontInfo));
            }
            return num;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                if (this.StringFormat != null)
                {
                    this.StringFormat.Dispose();
                    this.StringFormat = null;
                }
                if (this.wrapMatrix != null)
                {
                    this.wrapMatrix.Dispose();
                    this.wrapMatrix = null;
                }
            }
            base.Dispose(disposing);
        }

        protected override RectangleF GetBrushPenBounds(RectangleF paragraphBounds, RectangleF runLayoutBounds) => 
            (base.WarpTransformer == null) ? runLayoutBounds : new RectangleF(PointF.Empty, this.layoutCalculator.TextRectangle.Size);

        public override float GetCurrentBaseLine() => 
            base.CurrentBucketElement.RunBaseLine;

        protected override GraphicsPath GetRunTextGraphicsPath(RunLayoutInfo runLayout, string text, Font font, float realBaseLine)
        {
            float emSize = base.DocumentModel.LayoutUnitConverter.PointsToLayoutUnitsF(font.SizeInPoints);
            float num2 = base.CalcGdiBaseLine(runLayout.RunFontInfo);
            float left = runLayout.Bounds.Left;
            float top = runLayout.Bounds.Top;
            GraphicsPath path = new GraphicsPath(FillMode.Winding);
            float num5 = base.DocumentModel.LayoutUnitConverter.PointsToLayoutUnitsF((runLayout.Properties != null) ? ((float) runLayout.Properties.Spacing) : ((float) 0)) / 100f;
            bool flag = (runLayout.Properties != null) && runLayout.Properties.NormalizeHeight;
            float num6 = font.GetHeight(this.layoutCalculator.Graphics) * 1.1655f;
            float num8 = (((runLayout.Properties != null) ? ((float) runLayout.Properties.Baseline) : ((float) 0)) * base.DocumentModel.LayoutUnitConverter.PointsToLayoutUnitsF(runLayout.BaseFontInfo.SizeInPoints)) / 100000f;
            Matrix matrix = new Matrix();
            foreach (char ch in text)
            {
                string str2 = ch.ToString();
                if (!string.IsNullOrWhiteSpace(str2))
                {
                    GraphicsPath graphicsPath = new GraphicsPath(FillMode.Winding);
                    graphicsPath.AddString(str2, font.FontFamily, (int) font.Style, emSize, new PointF(left, top), this.StringFormat);
                    if (flag)
                    {
                        base.NormalizeHeight(graphicsPath, runLayout, num2);
                    }
                    matrix.Reset();
                    RectangleF bounds = graphicsPath.GetBounds();
                    matrix.Translate(((((realBaseLine - bounds.Width) / 2f) + num8) - bounds.Left) + left, 0f);
                    graphicsPath.Transform(matrix);
                    path.AddPath(graphicsPath, false);
                    graphicsPath.Dispose();
                }
                top += num6 + num5;
            }
            matrix.Dispose();
            return path;
        }

        protected override List<GraphicsPath> GetStrikethroughGraphicsPaths(RunLayoutInfo runLayout, float realBaseLine, GraphicsPath runTextGraphicsPath)
        {
            RectangleF bounds = runLayout.Bounds;
            FontInfo runFontInfo = runLayout.RunFontInfo;
            float top = (bounds.Top + base.CalcGdiBaseLine(runLayout.RunFontInfo)) - runFontInfo.StrikeoutPosition;
            List<GraphicsPath> graphicsPaths = new List<GraphicsPath>();
            int strikeoutThickness = runFontInfo.StrikeoutThickness;
            float num3 = runLayout.RunFontInfo.Font.GetHeight(this.layoutCalculator.Graphics) * 1.1655f;
            float num4 = base.DocumentModel.LayoutUnitConverter.PointsToLayoutUnitsF((runLayout.Properties != null) ? ((float) runLayout.Properties.Spacing) : ((float) 0)) / 100f;
            RectangleF ef2 = runTextGraphicsPath.GetBounds();
            foreach (char ch in runLayout.Text)
            {
                string text = ch.ToString();
                SizeF ef3 = this.layoutCalculator.Graphics.MeasureString(text, runLayout.RunFontInfo.Font, PointF.Empty, this.StringFormat);
                float width = ef3.Width;
                float left = (ef2.Left + (ef2.Width / 2f)) - (width / 2f);
                DrawingTextStrikeType strikethrough = runLayout.Properties.Strikethrough;
                switch (strikethrough)
                {
                    case DrawingTextStrikeType.None:
                        break;

                    case DrawingTextStrikeType.Single:
                        AddGraphicsPathWithRectangle(graphicsPaths, left, top, width, (float) strikeoutThickness);
                        break;

                    case DrawingTextStrikeType.Double:
                        AddGraphicsPathWithRectangle(graphicsPaths, left, top + strikeoutThickness, width, (float) strikeoutThickness);
                        AddGraphicsPathWithRectangle(graphicsPaths, left, top - strikeoutThickness, width, (float) strikeoutThickness);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
                top += num3 + num4;
            }
            return graphicsPaths;
        }

        private List<GraphicsPath> GetUnderlineGraphicsPaths(DrawingTextUnderlineType underlineType, float top, float bottom, float textBaseLine, int underlinePosition, int underlineThickness)
        {
            List<GraphicsPath> graphicsPaths = new List<GraphicsPath>();
            int lineWidth = (underlineThickness + 1) >> 1;
            int width = underlineThickness + lineWidth;
            int left = ((((int) textBaseLine) - underlinePosition) - underlineThickness) - underlineThickness;
            int num4 = left + lineWidth;
            int dashHeight = underlineThickness * 3;
            int spaceHeight = underlineThickness * 2;
            int num7 = underlineThickness * 7;
            int num8 = underlineThickness * 4;
            float height = bottom - top;
            switch (underlineType)
            {
                case DrawingTextUnderlineType.Words:
                case DrawingTextUnderlineType.Single:
                    AddGraphicsPathWithRectangle(graphicsPaths, (float) left, top, (float) underlineThickness, height);
                    break;

                case DrawingTextUnderlineType.Double:
                {
                    GraphicsPath item = new GraphicsPath(FillMode.Winding);
                    item.AddRectangle(new RectangleF((float) left, top, (float) lineWidth, height));
                    item.AddRectangle(new RectangleF((float) ((left + lineWidth) + underlineThickness), top, (float) lineWidth, height));
                    graphicsPaths.Add(item);
                    break;
                }
                case DrawingTextUnderlineType.Heavy:
                    AddGraphicsPathWithRectangle(graphicsPaths, (float) num4, top, (float) width, height);
                    break;

                case DrawingTextUnderlineType.Dotted:
                    AddDash(graphicsPaths, top, bottom, left, underlineThickness, underlineThickness, underlineThickness);
                    break;

                case DrawingTextUnderlineType.HeavyDotted:
                    AddDash(graphicsPaths, top, bottom, num4, underlineThickness, underlineThickness, width);
                    break;

                case DrawingTextUnderlineType.Dashed:
                    AddDash(graphicsPaths, top, bottom, left, dashHeight, spaceHeight, underlineThickness);
                    break;

                case DrawingTextUnderlineType.HeavyDashed:
                    AddDash(graphicsPaths, top, bottom, num4, dashHeight, spaceHeight, width);
                    break;

                case DrawingTextUnderlineType.LongDashed:
                    AddDash(graphicsPaths, top, bottom, left, num7, num8, underlineThickness);
                    break;

                case DrawingTextUnderlineType.HeavyLongDashed:
                    AddDash(graphicsPaths, top, bottom, num4, num7, num8, width);
                    break;

                case DrawingTextUnderlineType.DotDash:
                    AddDashDot(graphicsPaths, top, bottom, left, dashHeight, underlineThickness, spaceHeight, underlineThickness, 1);
                    break;

                case DrawingTextUnderlineType.HeavyDotDash:
                    AddDashDot(graphicsPaths, top, bottom, num4, dashHeight, underlineThickness, spaceHeight, width, 1);
                    break;

                case DrawingTextUnderlineType.DotDotDash:
                    AddDashDot(graphicsPaths, top, bottom, left, dashHeight, underlineThickness, spaceHeight, underlineThickness, 2);
                    break;

                case DrawingTextUnderlineType.HeavyDotDotDash:
                    AddDashDot(graphicsPaths, top, bottom, num4, dashHeight, underlineThickness, spaceHeight, width, 2);
                    break;

                case DrawingTextUnderlineType.Wavy:
                    this.AddWave(graphicsPaths, top, bottom, (float) (left - lineWidth), spaceHeight, 1);
                    break;

                case DrawingTextUnderlineType.HeavyWavy:
                    this.AddWave(graphicsPaths, top, bottom, (float) (left - lineWidth), spaceHeight, lineWidth);
                    break;

                case DrawingTextUnderlineType.DoubleWavy:
                {
                    GraphicsPath gp = new GraphicsPath(FillMode.Winding);
                    this.AddWave(gp, top, bottom, (float) (left + underlineThickness), underlineThickness, 1);
                    gp.CloseFigure();
                    this.AddWave(gp, top, bottom, (float) left, underlineThickness, 1);
                    gp.CloseFigure();
                    graphicsPaths.Add(gp);
                    break;
                }
                default:
                    break;
            }
            return graphicsPaths;
        }

        private void ProcessUnderline(ParagraphLayoutInfoConverter.UnderlineInfo underline, float top, RectangleF paragraphBounds, float excelBaseLine, float baseline, int underlinePosition, int realThickness)
        {
            RunLayoutInfo runLayoutInfo = underline.RunLayoutInfo;
            Brush fill = GetUnderlineBrush(runLayoutInfo.Properties, paragraphBounds, base.ShapeStyle, underline.TextBrush);
            PenInfo penInfo = underline.PenInfo;
            ContainerEffect effects = runLayoutInfo.Properties.Effects;
            RectangleF bounds = runLayoutInfo.Bounds;
            float bottom = bounds.Bottom;
            foreach (GraphicsPath path in this.GetUnderlineGraphicsPaths(runLayoutInfo.Properties.Underline, top, bottom, baseline, underlinePosition, realThickness))
            {
                TextLinePathInfo runPathInfo = new TextLinePathInfo(path, fill, penInfo);
                RectangleF ef2 = new RectangleF(bounds.Left, top, bounds.Width, bottom);
                base.BucketLinesPathInfos.Add(new ParagraphLayoutInfoConverter.BucketElement(runPathInfo, ef2, effects, excelBaseLine));
            }
        }

        private System.Drawing.StringFormat StringFormat { get; set; }
    }
}

