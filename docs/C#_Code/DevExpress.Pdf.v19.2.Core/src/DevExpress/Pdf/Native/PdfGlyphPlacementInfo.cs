namespace DevExpress.Pdf.Native
{
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfGlyphPlacementInfo : IPdfGlyphPlacementInfo
    {
        private const double pdfDPI = 72.0;
        private const double emPerUnit = 1000.0;
        private readonly int[] glyphIndices;
        private readonly int[] glyphPositions;
        private readonly int[] order;
        private readonly double fontSize;
        private readonly double dpiScale;
        private readonly bool shouldProcessVerticalOffset;

        public PdfGlyphPlacementInfo(string text, int[] glyphIndices, int[] glyphPositions, int[] order, double contextDPI, double fontSize, bool shouldProcessVerticalOffset)
        {
            this.<Text>k__BackingField = text;
            this.glyphIndices = glyphIndices;
            this.glyphPositions = glyphPositions;
            this.order = order;
            this.dpiScale = contextDPI / 72.0;
            this.fontSize = fontSize;
            this.shouldProcessVerticalOffset = shouldProcessVerticalOffset;
        }

        public IList<IList<DXCluster>> GetClusters()
        {
            if (string.IsNullOrEmpty(this.Text) || ((this.order == null) || (this.order.Length != this.Text.Length)))
            {
                return null;
            }
            List<DXCluster> item = new List<DXCluster>();
            StringView view = new StringView(this.Text, 0, this.Text.Length);
            bool[] flagArray = new bool[this.glyphIndices.Length];
            int index = 0;
            while (index < this.order.Length)
            {
                int num3 = this.order[index];
                flagArray[num3] = true;
                while ((index < this.order.Length) && (this.order[index] == num3))
                {
                    index++;
                }
            }
            float verticalOffset = 0f;
            int num4 = 0;
            while (num4 < this.glyphIndices.Length)
            {
                List<DXGlyph> glyphs = new List<DXGlyph>();
                bool flag = false;
                int num5 = 0;
                while (true)
                {
                    if (!flag && flagArray[num4])
                    {
                        num5 = num4;
                        flag = true;
                    }
                    ushort num8 = (ushort) this.glyphIndices[num4];
                    if (num4 == (this.glyphIndices.Length - 1))
                    {
                        float advance = 0f;
                        glyphs.Add(new DXGlyph(num8, advance, new DXGlyphOffset(0f, verticalOffset)));
                    }
                    else
                    {
                        int num10 = num4 * (this.shouldProcessVerticalOffset ? 2 : 1);
                        glyphs.Add(new DXGlyph(num8, (float) (((double) this.glyphPositions[num10]) / this.dpiScale), new DXGlyphOffset(0f, verticalOffset)));
                        if (this.shouldProcessVerticalOffset)
                        {
                            verticalOffset += (float) (((double) this.glyphPositions[num10 + 1]) / this.dpiScale);
                        }
                    }
                    num4++;
                    if ((num4 >= this.glyphIndices.Length) || (flagArray[num4] && flag))
                    {
                        int num6 = 0;
                        while (true)
                        {
                            if ((num6 >= this.order.Length) || (this.order[num6] == num5))
                            {
                                int num7 = num6 + 1;
                                while (true)
                                {
                                    if ((num7 >= this.order.Length) || (this.order[num7] != num5))
                                    {
                                        StringView text = view.SubView(num6, num7 - num6);
                                        if ((text.Length != 1) || !IsWritingOrderControl(text[0]))
                                        {
                                            DXLineBreakpoint breakpoint = new DXLineBreakpoint();
                                            item.Add(new DXCluster(glyphs, text, breakpoint, 0, false));
                                        }
                                        break;
                                    }
                                    num7++;
                                }
                                break;
                            }
                            num6++;
                        }
                        break;
                    }
                }
            }
            List<IList<DXCluster>> list1 = new List<IList<DXCluster>>();
            list1.Add(item);
            return list1;
        }

        private static bool IsWritingOrderControl(char c) => 
            (c >= '‪') && (c <= '‮');

        public string Text { get; }
    }
}

