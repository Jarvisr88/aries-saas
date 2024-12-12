namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class PdfTextPainter
    {
        private readonly PdfCommandConstructor constructor;

        public PdfTextPainter(PdfCommandConstructor constructor)
        {
            this.constructor = constructor;
        }

        public void DrawLines(IList<IList<DXCluster>> lines, PdfExportFontInfo fontInfo, PdfRectangle layoutRect, PdfStringFormat format)
        {
            this.DrawLines(lines, fontInfo, layoutRect, format, new PdfStringClipper());
        }

        public void DrawLines(IList<IList<DXCluster>> lines, PdfExportFontInfo fontInfo, PdfPoint location, PdfStringFormat format, bool isolateAction = true)
        {
            this.DrawLines(lines, fontInfo, new PdfStringPaintingAtPointStrategy(location, new PdfStringMeasurer(fontInfo, format)), fontInfo.Font.Metrics, new PdfStringClipper(), isolateAction);
        }

        public void DrawLines(IList<IList<DXCluster>> lines, PdfExportFontInfo fontInfo, PdfRectangle layoutRect, PdfStringFormat format, PdfStringClipper stringClipper)
        {
            this.DrawLines(lines, fontInfo, new PdfStringPaintingInsideRectStrategy(layoutRect, new PdfStringMeasurer(fontInfo, format)), fontInfo.Font.Metrics, stringClipper, true);
        }

        private void DrawLines(IList<IList<DXCluster>> lines, PdfExportFontInfo fontInfo, PdfStringPaintingStrategy strategy, PdfFontMetrics metrics, PdfStringClipper stringClipper, bool isolateAction = true)
        {
            int count = (lines == null) ? 0 : lines.Count;
            if (count > 0)
            {
                if (isolateAction)
                {
                    this.constructor.SaveGraphicsState();
                }
                strategy.Clip(this.constructor);
                this.constructor.BeginText();
                float fontSize = fontInfo.FontSize;
                this.constructor.SetTextFont(fontInfo.Font.Font, (double) fontSize);
                if (fontInfo.Font.Simulations.HasFlag(DXFontSimulations.Bold))
                {
                    this.constructor.SetLineWidth((double) (fontSize / 30f));
                    this.constructor.SetTextRenderingMode(PdfTextRenderingMode.FillAndStroke);
                }
                IList<PdfStringLine> list = new List<PdfStringLine>();
                bool flag = fontInfo.Decorations != DXFontDecorations.None;
                double lineSpacing = metrics.GetLineSpacing((double) fontSize);
                PdfTextBuilder builder = fontInfo.Font.Simulations.HasFlag(DXFontSimulations.Italic) ? new PdfObliqueTextBuilder(this.constructor, fontInfo) : new PdfTextBuilder(this.constructor, fontInfo);
                double ascent = metrics.GetAscent((double) fontSize);
                double firstLineVerticalPosition = strategy.GetFirstLineVerticalPosition(count);
                PdfTextLineBounds bounds = stringClipper.CalculateActualLineBounds(firstLineVerticalPosition, lineSpacing, count);
                firstLineVerticalPosition -= ascent;
                double yOffset = firstLineVerticalPosition - (lineSpacing * bounds.FirstLineIndex);
                double num7 = 0.0;
                int firstLineIndex = bounds.FirstLineIndex;
                while (true)
                {
                    if (firstLineIndex >= bounds.LastLineIndex)
                    {
                        fontInfo.Font.AddGlyphs(builder.Subset);
                        this.constructor.EndText();
                        if (list.Count != 0)
                        {
                            this.constructor.SetLineWidth(fontInfo.FontLineSize);
                            foreach (PdfStringLine line in list)
                            {
                                this.constructor.DrawLine(line.Begin, line.End);
                            }
                        }
                        if (isolateAction)
                        {
                            this.constructor.RestoreGraphicsState();
                        }
                        break;
                    }
                    IList<DXCluster> list2 = lines[firstLineIndex];
                    double horizontalPosition = strategy.GetHorizontalPosition(list2);
                    builder.StartNextLine(horizontalPosition - num7, yOffset);
                    if (list2.Any<DXCluster>())
                    {
                        builder.Append(list2);
                        if (flag)
                        {
                            double descent = metrics.GetDescent((double) fontSize);
                            double x = horizontalPosition + strategy.MeasureWidth(list2);
                            double num12 = firstLineVerticalPosition - (lineSpacing * firstLineIndex);
                            if (fontInfo.Decorations.HasFlag(DXFontDecorations.Underline))
                            {
                                double y = num12 - (descent / 2.0);
                                list.Add(new PdfStringLine(new PdfPoint(horizontalPosition, y), new PdfPoint(x, y)));
                            }
                            if (fontInfo.Decorations.HasFlag(DXFontDecorations.Strikeout))
                            {
                                double y = (num12 + (ascent / 2.0)) - descent;
                                list.Add(new PdfStringLine(new PdfPoint(horizontalPosition, y), new PdfPoint(x, y)));
                            }
                        }
                    }
                    yOffset = -lineSpacing;
                    num7 = horizontalPosition;
                    firstLineIndex++;
                }
            }
        }

        public void DrawLines(IList<IList<DXCluster>> lines, PdfExportFontInfo fontInfo, PdfRectangle layoutRect, PdfFontMetrics metrics, PdfStringFormat format, bool isolateAction = true)
        {
            this.DrawLines(lines, fontInfo, new PdfStringPaintingInsideRectStrategy(layoutRect, new PdfStringMeasurer(fontInfo, format)), metrics, new PdfStringClipper(), isolateAction);
        }

        private class PdfObliqueTextBuilder : PdfTextBuilder
        {
            private double previousXOffset;
            private double previousYOffset;

            public PdfObliqueTextBuilder(PdfCommandConstructor commandConstructor, PdfExportFontInfo fontInfo) : base(commandConstructor, fontInfo)
            {
            }

            protected override void StartNextLineCore(double xOffset, double yOffset)
            {
                this.previousXOffset += xOffset;
                this.previousYOffset += yOffset;
                base.CommandConstructor.SetObliqueTextMatrix(this.previousXOffset, this.previousYOffset);
            }
        }
    }
}

