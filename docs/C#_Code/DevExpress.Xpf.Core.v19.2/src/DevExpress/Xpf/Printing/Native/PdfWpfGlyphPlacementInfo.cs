namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Media;

    internal class PdfWpfGlyphPlacementInfo : IPdfGlyphPlacementInfo
    {
        private readonly string text;
        private readonly float scaleFactor;
        private readonly GlyphRun run;

        public PdfWpfGlyphPlacementInfo(GlyphRun run, float scaleFactor)
        {
            this.text = new string(run.Characters.ToArray<char>());
            this.run = run;
            this.scaleFactor = scaleFactor;
        }

        public IList<IList<DXCluster>> GetClusters()
        {
            List<ClusterInfo> list = new List<ClusterInfo>();
            int startIndex = 0;
            IList<ushort> clusterMap = this.run.ClusterMap;
            bool[] flagArray = new bool[this.run.GlyphIndices.Count];
            if ((clusterMap == null) || (clusterMap.Count == 0))
            {
                for (int j = 0; j < flagArray.Length; j++)
                {
                    flagArray[j] = true;
                    list.Add(new ClusterInfo(j, j));
                }
            }
            else
            {
                int num3 = 0;
                while (true)
                {
                    if (num3 >= this.run.ClusterMap.Count)
                    {
                        int num4 = 1;
                        int num5 = clusterMap[0];
                        while (true)
                        {
                            if (num4 >= clusterMap.Count)
                            {
                                list.Add(new ClusterInfo(startIndex, clusterMap.Count - 1));
                                break;
                            }
                            if (num5 != clusterMap[num4])
                            {
                                list.Add(new ClusterInfo(startIndex, num4 - 1));
                                startIndex = num4;
                                num5 = clusterMap[num4];
                            }
                            num4++;
                        }
                        break;
                    }
                    flagArray[this.run.ClusterMap[num3]] = true;
                    num3++;
                }
            }
            List<ClusterInfo> list3 = new List<ClusterInfo>();
            startIndex = 0;
            for (int i = 1; i < flagArray.Length; i++)
            {
                if (flagArray[i])
                {
                    list3.Add(new ClusterInfo(startIndex, i - 1));
                    startIndex = i;
                }
            }
            list3.Add(new ClusterInfo(startIndex, flagArray.Length - 1));
            List<DXCluster> item = new List<DXCluster>();
            StringView view = new StringView(this.Text, 0, this.Text.Length);
            int num7 = 0;
            while (num7 < list3.Count)
            {
                ClusterInfo info = list[num7];
                ClusterInfo info2 = list3[num7];
                DXGlyph[] glyphs = new DXGlyph[info2.Length];
                int index = 0;
                while (true)
                {
                    if (index >= glyphs.Length)
                    {
                        DXLineBreakpoint breakpoint = new DXLineBreakpoint();
                        item.Add(new DXCluster(glyphs, view.SubView(info.StartIndex, info.Length), breakpoint, (byte) this.run.BidiLevel, false));
                        num7++;
                        break;
                    }
                    int num9 = index + info2.StartIndex;
                    Point point = (this.run.GlyphOffsets == null) ? new Point(0.0, 0.0) : this.run.GlyphOffsets[num9];
                    glyphs[index] = new DXGlyph(this.run.GlyphIndices[num9], ((float) this.run.AdvanceWidths[num9]) * this.scaleFactor, new DXGlyphOffset(((float) point.X) * this.scaleFactor, ((float) point.Y) * this.scaleFactor));
                    index++;
                }
            }
            if ((this.run.BidiLevel % 2) != 0)
            {
                item.Reverse();
            }
            List<IList<DXCluster>> list1 = new List<IList<DXCluster>>();
            list1.Add(item);
            return list1;
        }

        public string Text =>
            this.text;

        private class ClusterInfo
        {
            private readonly int startIndex;
            private readonly int endIndex;

            public ClusterInfo(int startIndex, int endIndex)
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

