namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections.Generic;
    using System.Reflection;

    internal class TTFGlyf : TTFTable
    {
        private TTFGlyphData[] glyphs;
        private List<TTFGlyphData> purgedGlyphs;

        public TTFGlyf(TTFFile ttfFile) : base(ttfFile)
        {
            this.purgedGlyphs = new List<TTFGlyphData>();
        }

        private void AssignGlyphData(int index, TTFGlyphData glyphData)
        {
            this.glyphs[index] = glyphData;
            if (glyphData != null)
            {
                this.purgedGlyphs.Add(glyphData);
            }
        }

        protected override void InitializeTable(TTFTable pattern, TTFInitializeParam param)
        {
            TTFGlyf glyf = pattern as TTFGlyf;
            this.glyphs = new TTFGlyphData[glyf.Glyphs.Length];
            this.purgedGlyphs.Clear();
            foreach (ushort num2 in param.Chars.GlyphIndices)
            {
                this.AssignGlyphData(num2, glyf.Glyphs[num2]);
            }
        }

        protected override void ReadTable(TTFStream ttfStream)
        {
            this.glyphs = new TTFGlyphData[base.Owner.MaxP.NumGlyphs];
            this.purgedGlyphs.Clear();
            int position = ttfStream.Position;
            for (int i = 0; i < base.Owner.MaxP.NumGlyphs; i++)
            {
                uint num3 = base.Owner.Loca[(ushort) i];
                uint num4 = base.Owner.Loca[(ushort) (i + 1)];
                if (num4 != num3)
                {
                    this.AssignGlyphData(i, new TTFGlyphData((int) (num4 - num3)));
                    ttfStream.Seek(position);
                    ttfStream.Move((int) num3);
                    this.glyphs[i].Read(ttfStream);
                }
            }
        }

        protected override void WriteTable(TTFStream ttfStream)
        {
            for (int i = 0; i < this.Count; i++)
            {
                this[i].Write(ttfStream);
            }
        }

        protected internal override string Tag =>
            "glyf";

        public int Count =>
            this.purgedGlyphs.Count;

        public TTFGlyphData this[int index] =>
            this.purgedGlyphs[index];

        public TTFGlyphData[] Glyphs =>
            this.glyphs;

        public override int Length
        {
            get
            {
                int num = 0;
                for (int i = 0; i < this.Count; i++)
                {
                    num += this[i].Size;
                }
                return num;
            }
        }
    }
}

