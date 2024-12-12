namespace DevExpress.Pdf.Native
{
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;

    public class PdfWidgetShaper : DXShaper
    {
        private readonly DXShaper shaper;
        private readonly float charSpacing;
        private readonly float wordSpacing;
        private readonly float horizontalScalingFactor = 1f;

        public PdfWidgetShaper(DXShaper shaper, PdfInteractiveFormFieldTextState textState)
        {
            this.shaper = shaper;
            if (textState != null)
            {
                this.horizontalScalingFactor = (float) (textState.HorizontalScaling * 0.01);
                this.charSpacing = ((float) textState.CharacterSpacing) * this.horizontalScalingFactor;
                this.wordSpacing = ((float) textState.WordSpacing) * this.horizontalScalingFactor;
            }
        }

        public override IList<DXCluster> GetTextRuns(string text, bool directionRightToLeft, float fontSizeInPoints, bool useKerning)
        {
            IList<DXCluster> list = this.shaper.GetTextRuns(text.Replace('\t', ' '), directionRightToLeft, fontSizeInPoints, useKerning);
            List<DXCluster> list2 = new List<DXCluster>(list.Count);
            foreach (DXCluster cluster in list)
            {
                List<DXGlyph> glyphs = new List<DXGlyph>(cluster.Glyphs.Count);
                if (cluster.Text.GetText() == " ")
                {
                    int num = 0;
                    while (true)
                    {
                        if (num >= (cluster.Glyphs.Count - 1))
                        {
                            DXGlyph glyph = cluster.Glyphs[cluster.Glyphs.Count - 1];
                            glyphs.Add(new DXGlyph(glyph.Index, ((glyph.Advance * this.horizontalScalingFactor) + this.wordSpacing) + this.charSpacing, glyph.Offset));
                            break;
                        }
                        glyphs.Add(cluster.Glyphs[num]);
                        num++;
                    }
                }
                else
                {
                    foreach (DXGlyph glyph2 in cluster.Glyphs)
                    {
                        float advance = glyph2.Advance * this.horizontalScalingFactor;
                        if (advance != 0f)
                        {
                            advance += this.charSpacing;
                        }
                        glyphs.Add(new DXGlyph(glyph2.Index, advance, glyph2.Offset));
                    }
                }
                list2.Add(new DXCluster(glyphs, cluster.Text, cluster.Breakpoint, cluster.BidiLevel, false));
            }
            return list2;
        }
    }
}

