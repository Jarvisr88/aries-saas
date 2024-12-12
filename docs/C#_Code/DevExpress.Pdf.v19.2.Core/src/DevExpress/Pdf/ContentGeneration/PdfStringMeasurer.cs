namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfStringMeasurer
    {
        private readonly PdfFontMetrics metrics;
        private readonly double fontSize;

        public PdfStringMeasurer(PdfExportFontInfo fontInfo, PdfStringFormat format)
        {
            this.metrics = fontInfo.Font.Metrics;
            this.fontSize = fontInfo.FontSize;
            this.<LeadingOffset>k__BackingField = format.LeadingMarginFactor * this.fontSize;
            this.<TrailingOffset>k__BackingField = format.TrailingMarginFactor * this.fontSize;
            this.<Format>k__BackingField = new PdfStringFormat(format);
        }

        public double MeasureStringHeight(int lineCount)
        {
            double num = 0.0;
            if (lineCount > 0)
            {
                double lineSpacing = this.metrics.GetLineSpacing(this.fontSize);
                num = ((lineCount == 1) ? (this.metrics.GetAscent(this.fontSize) + this.metrics.GetDescent(this.fontSize)) : (lineCount * lineSpacing)) + ((this.TrailingOffset * 3.0) / 4.0);
            }
            return num;
        }

        public double MeasureWidth(IList<DXCluster> line)
        {
            double num = 0.0;
            for (int i = 0; i < line.Count; i++)
            {
                DXCluster cluster = line[i];
                num += cluster.Width;
            }
            return num;
        }

        public double LeadingOffset { get; }

        public double TrailingOffset { get; }

        public PdfStringFormat Format { get; }
    }
}

