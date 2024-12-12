namespace DevExpress.Text.Fonts.DirectWrite
{
    using DevExpress.DirectX.Common.DirectWrite;
    using DevExpress.DirectX.StandardInterop.DirectWrite;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Linq;

    public class DirectWriteShaper : DXCTLShaper
    {
        private const int MaxGlyphCount = 0x1000;
        private readonly DWriteTextAnalyzer textAnalyzer;
        private readonly DirectWriteFont font;

        public DirectWriteShaper(DWriteTextAnalyzer textAnalyzer, DirectWriteFont font)
        {
            this.textAnalyzer = textAnalyzer;
            this.font = font;
        }

        public override IList<DXCluster> GetTextRuns(string text, bool directionRightToLeft, float fontSizeInPoints, bool useKerning) => 
            (from r in DirectWriteTextAnalysis.GetRuns(this.textAnalyzer, text, directionRightToLeft) select this.ShapeRun(r, fontSizeInPoints, useKerning)).ToList<DXCluster>();

        public override IEnumerable<IDXTextRun> Itemize(string text, bool directionRightToLeft) => 
            (IEnumerable<IDXTextRun>) DirectWriteTextAnalysis.GetRuns(this.textAnalyzer, text, directionRightToLeft);

        private static IList<DXCluster> ShapeNonVisualRun(DirectWriteTextRun run)
        {
            IList<DXCluster> list = new DXCluster[run.Length];
            int num = run.Offset;
            StringView text = run.Text;
            for (int i = 0; i < run.Length; i++)
            {
                DXGlyphOffset offset = new DXGlyphOffset();
                DXGlyph[] glyphs = new DXGlyph[] { new DXGlyph(0, 0f, offset) };
                list[i] = new DXCluster(glyphs, text.SubView(i, 1), run.Breakpoints[num + i], run.BidiLevel, text[i] == '\t');
            }
            return list;
        }

        private IList<DXCluster> ShapeRun(DirectWriteTextRun run, float fontSize, bool useKerning)
        {
            int num;
            if ((run.Script.script == 1) && (run.Script.shapes == DWRITE_SCRIPT_SHAPES.NoVisual))
            {
                return ShapeNonVisualRun(run);
            }
            DWriteShapingFeatures ligatures = DWriteShapingFeatures.Ligatures;
            if (useKerning)
            {
                ligatures |= DWriteShapingFeatures.Kerning;
            }
            DWRITE_SCRIPT_ANALYSIS script = run.Script;
            DWRITE_SHAPING_TEXT_PROPERTIES[] textProps = new DWRITE_SHAPING_TEXT_PROPERTIES[run.Length];
            bool isRightToLeft = (run.BidiLevel % 2) != 0;
            DWRITE_SHAPING_GLYPH_PROPERTIES[] glyphProps = new DWRITE_SHAPING_GLYPH_PROPERTIES[0x1000];
            short[] glyphIndices = new short[0x1000];
            short[] clusterMap = new short[0x1000];
            DWriteFontFace fontFace = this.font.FontFace;
            StringView text = run.Text;
            string textString = text.GetText();
            this.textAnalyzer.GetGlyphs(textString, fontFace.WrappedObject, false, isRightToLeft, ref script, null, ligatures, null, 0x1000, clusterMap, textProps, glyphIndices, glyphProps, out num);
            float[] glyphAdvances = new float[num];
            DWRITE_GLYPH_OFFSET[] glyphOffsets = new DWRITE_GLYPH_OFFSET[num];
            this.textAnalyzer.GetGlyphPlacements(textString, clusterMap, textProps, glyphIndices, glyphProps, num, fontFace.WrappedObject, fontSize, false, isRightToLeft, ref script, null, ligatures, glyphAdvances, glyphOffsets);
            List<IList<DXGlyph>> list = new List<IList<DXGlyph>>();
            List<DXGlyph> item = new List<DXGlyph>(1);
            for (int i = 0; i < num; i++)
            {
                if (glyphProps[i].IsClusterStart && (item.Count != 0))
                {
                    list.Add(item);
                    item = new List<DXGlyph>(1);
                }
                DWRITE_GLYPH_OFFSET dwrite_glyph_offset = glyphOffsets[i];
                item.Add(new DXGlyph((ushort) glyphIndices[i], glyphAdvances[i], new DXGlyphOffset(dwrite_glyph_offset.AdvanceOffset, dwrite_glyph_offset.AscenderOffset)));
            }
            if (item.Count != 0)
            {
                list.Add(item);
            }
            List<DXCluster> list3 = new List<DXCluster>();
            int num2 = 0;
            int splitOffset = 0;
            int num4 = 0;
            for (int j = 0; j < text.Length; j++)
            {
                if (clusterMap[j] != num2)
                {
                    num2 = clusterMap[j];
                    int num7 = splitOffset;
                    int length = j - splitOffset;
                    list3.Add(new DXCluster(list[num4++], text.SubView(num7, length), run.GetBreakpoint((num7 + length) - 1), run.BidiLevel, false));
                    splitOffset = j;
                }
            }
            if ((text.Length - splitOffset) > 0)
            {
                list3.Add(new DXCluster(list[num4], text.SubView(splitOffset), run.GetBreakpoint(text.Length - 1), run.BidiLevel, false));
            }
            return list3;
        }
    }
}

