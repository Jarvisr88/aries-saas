namespace DevExpress.Pdf.Native
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfDWritePlacementInfo : IPdfGlyphPlacementInfo
    {
        public PdfDWritePlacementInfo(string text, short[] indexes, short[] clusters, float[] widths, DWRITE_GLYPH_OFFSET[] offsets, bool isRTL, DWRITE_SHAPING_GLYPH_PROPERTIES[] shapingProperties, double fontSize)
        {
            this.<Text>k__BackingField = text;
            this.<Indexes>k__BackingField = indexes;
            this.<Clusters>k__BackingField = clusters;
            this.<Widths>k__BackingField = widths;
            this.<Offsets>k__BackingField = offsets;
            this.<IsRTL>k__BackingField = isRTL;
            this.<ShapingProperties>k__BackingField = shapingProperties;
            this.<FontSize>k__BackingField = fontSize;
        }

        public IList<IList<DXCluster>> GetClusters()
        {
            List<PdfClusterInfo> list = new List<PdfClusterInfo>();
            int startIndex = 0;
            short[] clusters = this.Clusters;
            int index = 1;
            int num3 = clusters[0];
            while (index < clusters.Length)
            {
                if (num3 != clusters[index])
                {
                    list.Add(new PdfClusterInfo(startIndex, index - 1));
                    startIndex = index;
                    num3 = clusters[index];
                }
                index++;
            }
            list.Add(new PdfClusterInfo(startIndex, clusters.Length - 1));
            List<PdfClusterInfo> list2 = new List<PdfClusterInfo>();
            startIndex = 0;
            DWRITE_SHAPING_GLYPH_PROPERTIES[] shapingProperties = this.ShapingProperties;
            for (int i = 1; i < shapingProperties.Length; i++)
            {
                if (shapingProperties[i].IsClusterStart)
                {
                    list2.Add(new PdfClusterInfo(startIndex, i - 1));
                    startIndex = i;
                }
            }
            list2.Add(new PdfClusterInfo(startIndex, shapingProperties.Length - 1));
            List<DXCluster> item = new List<DXCluster>();
            StringView view = new StringView(this.Text, 0, this.Text.Length);
            int num5 = 0;
            while (num5 < list2.Count)
            {
                PdfClusterInfo info = list[num5];
                PdfClusterInfo info2 = list2[num5];
                DXGlyph[] glyphs = new DXGlyph[info2.Length];
                int num6 = 0;
                while (true)
                {
                    if (num6 >= glyphs.Length)
                    {
                        DXLineBreakpoint breakpoint = new DXLineBreakpoint();
                        item.Add(new DXCluster(glyphs, view.SubView(info.StartIndex, info.Length), breakpoint, this.IsRTL ? ((byte) 1) : ((byte) 0), false));
                        num5++;
                        break;
                    }
                    int num7 = num6 + info2.StartIndex;
                    DWRITE_GLYPH_OFFSET dwrite_glyph_offset = this.Offsets[num7];
                    glyphs[num6] = new DXGlyph((ushort) this.Indexes[num7], this.Widths[num7], new DXGlyphOffset(dwrite_glyph_offset.AdvanceOffset, dwrite_glyph_offset.AscenderOffset));
                    num6++;
                }
            }
            if (this.IsRTL)
            {
                item.Reverse();
            }
            List<IList<DXCluster>> list1 = new List<IList<DXCluster>>();
            list1.Add(item);
            return list1;
        }

        public short[] Indexes { get; }

        public short[] Clusters { get; }

        public float[] Widths { get; }

        public DWRITE_GLYPH_OFFSET[] Offsets { get; }

        public bool IsRTL { get; }

        public double FontSize { get; }

        public DWRITE_SHAPING_GLYPH_PROPERTIES[] ShapingProperties { get; }

        public string Text { get; }

        private class PdfClusterInfo
        {
            private readonly int startIndex;
            private readonly int endIndex;

            public PdfClusterInfo(int startIndex, int endIndex)
            {
                this.startIndex = startIndex;
                this.endIndex = endIndex;
            }

            public int StartIndex =>
                this.startIndex;

            public int Length =>
                (this.endIndex - this.startIndex) + 1;
        }
    }
}

