namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;

    public class PdfTabbedStringFormatter
    {
        private readonly PdfExportFontInfo fontInfo;
        private readonly float tabStopSize;
        private readonly bool shouldUseKerning;
        private readonly bool rightToLeft;

        public PdfTabbedStringFormatter(PdfExportFontInfo fontInfo, float tabStopSize, bool rightToLeft, bool shouldUseKerning)
        {
            this.fontInfo = fontInfo;
            this.tabStopSize = tabStopSize;
            this.rightToLeft = rightToLeft;
            this.shouldUseKerning = shouldUseKerning;
        }

        public IList<DXCluster> FormatString(string line)
        {
            IList<DXCluster> list = this.fontInfo.Font.GetTextRuns(line, this.rightToLeft, this.fontInfo.FontSize, this.shouldUseKerning);
            IList<DXCluster> clusters = new List<DXCluster>(list.Count);
            float num = 0f;
            foreach (DXCluster cluster in list)
            {
                if (!cluster.IsTabCluster || (this.tabStopSize == 0f))
                {
                    clusters.Add(cluster);
                    num += cluster.Width;
                    continue;
                }
                float advance = (((float) Math.Ceiling((double) (num / this.tabStopSize))) * this.tabStopSize) - num;
                if (advance == 0f)
                {
                    advance = this.tabStopSize;
                }
                DXGlyphOffset offset = new DXGlyphOffset();
                DXGlyph[] glyphs = new DXGlyph[] { new DXGlyph(0, advance, offset) };
                clusters.Add(new DXCluster(glyphs, cluster.Text, cluster.Breakpoint, cluster.BidiLevel, true));
                num += advance;
            }
            DXBidiHelper.Reorder(clusters);
            return clusters;
        }
    }
}

