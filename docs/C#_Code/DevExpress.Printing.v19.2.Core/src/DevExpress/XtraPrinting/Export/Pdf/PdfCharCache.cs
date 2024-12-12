namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class PdfCharCache : IEnumerable
    {
        private List<char> list = new List<char>();
        private List<ushort> glyphIndices = new List<ushort>();
        private List<char[]> glyphToUnicode = new List<char[]>();
        private ushort[] glyphIndicesArray;
        private char[][] glyphToUnicodeArray;
        private char[] defaultValue = new char[0];

        private void AddGlyph(ushort glyph, char[] c)
        {
            this.glyphIndices.Add(glyph);
            this.glyphToUnicode.Add(c);
            this.glyphIndicesArray = null;
            this.glyphToUnicodeArray = null;
        }

        private void AddUnique(char ch)
        {
            if (!this.list.Contains(ch))
            {
                this.list.Add(ch);
            }
        }

        private void AddUniqueGlyph(ushort glyph)
        {
            this.AddUniqueGlyph(glyph, this.defaultValue);
        }

        internal bool AddUniqueGlyph(ushort glyph, char[] c)
        {
            if (this.glyphIndices.Contains(glyph))
            {
                return false;
            }
            this.AddGlyph(glyph, c);
            return true;
        }

        internal void CalculateGlyphIndeces(TTFFile ttfFile)
        {
            if (this.ShouldExpandCompositeGlyphs)
            {
                this.RegisterGlyphs(ttfFile.CreateGlyphIndices(this));
            }
            Array.Sort<ushort, char[]>(this.GlyphIndices, this.GlyphToUnicode);
        }

        internal ICollection<KeyValuePair<ushort, char[]>> GetCharMap()
        {
            List<KeyValuePair<ushort, char[]>> list = new List<KeyValuePair<ushort, char[]>>();
            for (int i = 0; i < this.GlyphIndices.Length; i++)
            {
                if ((this.GlyphIndices[i] != 0) && (this.GlyphToUnicode[i] != this.defaultValue))
                {
                    list.Add(new KeyValuePair<ushort, char[]>(this.GlyphIndices[i], this.GlyphToUnicode[i]));
                }
            }
            return list;
        }

        private void RegisterGlyphs(TextRun textRun)
        {
            for (int i = 0; i < textRun.Glyphs.Length; i++)
            {
                char[] chArray;
                if (textRun.CharMap.TryGetValue(textRun.Glyphs[i], out chArray))
                {
                    this.AddUniqueGlyph(textRun.Glyphs[i], chArray);
                }
            }
        }

        private void RegisterGlyphs(ushort[] glyphs)
        {
            foreach (ushort num2 in glyphs)
            {
                this.AddUniqueGlyph(num2);
            }
        }

        private void RegisterString(string string_)
        {
            foreach (char ch in string_)
            {
                this.AddUnique(ch);
            }
        }

        public void RegisterTextRun(TextRun textRun)
        {
            if (textRun.HasGlyphs)
            {
                this.RegisterGlyphs(textRun);
            }
            else
            {
                this.RegisterString(textRun.Text);
            }
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.list.GetEnumerator();

        public int Count =>
            this.list.Count;

        public char this[int index] =>
            this.list[index];

        internal ushort[] GlyphIndices
        {
            get
            {
                this.glyphIndicesArray ??= this.glyphIndices.ToArray();
                return this.glyphIndicesArray;
            }
        }

        private char[][] GlyphToUnicode
        {
            get
            {
                this.glyphToUnicodeArray ??= this.glyphToUnicode.ToArray();
                return this.glyphToUnicodeArray;
            }
        }

        protected virtual bool ShouldExpandCompositeGlyphs =>
            true;
    }
}

