namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public abstract class PdfExportModelFont
    {
        protected const string VendorPrefix = "DEVEXP";
        private readonly Subset subset = new Subset();

        protected PdfExportModelFont(bool shouldUseTwoByteGlyphIndex)
        {
            this.<UseTwoByteCodePoints>k__BackingField = shouldUseTwoByteGlyphIndex;
        }

        public void AddGlyphs(IDictionary<int, PdfExportFontGlyphInfo> glyphs)
        {
            this.subset.AddGlyphs(glyphs);
        }

        public abstract DXGlyph CreateGlyph(int index, char unicode, float width, float advance);
        public abstract float GetGlyphWidth(int mappedIndex);
        public void UpdateFont()
        {
            if (this.subset.HasNewCharacters)
            {
                this.Font.ToUnicode = new PdfCharacterMapping(PdfToUnicodeCMap.CreateCharacterMappingData(this.subset.ToUnicode, "DEVEXP", this.UseTwoByteCodePoints));
                this.UpdateFontFile(this.subset);
                this.subset.HasNewCharacters = false;
            }
        }

        protected virtual void UpdateFontFile(Subset subset)
        {
        }

        public abstract PdfFont Font { get; }

        public bool UseTwoByteCodePoints { get; }

        protected class Subset
        {
            private readonly Dictionary<int, PdfExportFontGlyphInfo> dictionary = new Dictionary<int, PdfExportFontGlyphInfo>();

            public void AddGlyphs(IDictionary<int, PdfExportFontGlyphInfo> glyphInfoes)
            {
                foreach (KeyValuePair<int, PdfExportFontGlyphInfo> pair in glyphInfoes)
                {
                    int key = pair.Key;
                    if (!this.dictionary.ContainsKey(key))
                    {
                        this.dictionary.Add(key, pair.Value);
                        this.HasNewCharacters = true;
                    }
                }
            }

            public bool HasNewCharacters { get; set; }

            public IDictionary<int, string> ToUnicode
            {
                get
                {
                    Func<KeyValuePair<int, PdfExportFontGlyphInfo>, int> keySelector = <>c.<>9__6_0;
                    if (<>c.<>9__6_0 == null)
                    {
                        Func<KeyValuePair<int, PdfExportFontGlyphInfo>, int> local1 = <>c.<>9__6_0;
                        keySelector = <>c.<>9__6_0 = key => key.Key;
                    }
                    return this.dictionary.ToDictionary<KeyValuePair<int, PdfExportFontGlyphInfo>, int, string>(keySelector, (<>c.<>9__6_1 ??= value => value.Value.Unicode));
                }
            }

            public IDictionary<int, double> Widths
            {
                get
                {
                    Func<KeyValuePair<int, PdfExportFontGlyphInfo>, int> keySelector = <>c.<>9__8_0;
                    if (<>c.<>9__8_0 == null)
                    {
                        Func<KeyValuePair<int, PdfExportFontGlyphInfo>, int> local1 = <>c.<>9__8_0;
                        keySelector = <>c.<>9__8_0 = key => key.Key;
                    }
                    return this.dictionary.ToDictionary<KeyValuePair<int, PdfExportFontGlyphInfo>, int, double>(keySelector, (<>c.<>9__8_1 ??= value => ((double) value.Value.Width)));
                }
            }

            public ICollection<int> Indices =>
                this.dictionary.Keys;

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly PdfExportModelFont.Subset.<>c <>9 = new PdfExportModelFont.Subset.<>c();
                public static Func<KeyValuePair<int, PdfExportFontGlyphInfo>, int> <>9__6_0;
                public static Func<KeyValuePair<int, PdfExportFontGlyphInfo>, string> <>9__6_1;
                public static Func<KeyValuePair<int, PdfExportFontGlyphInfo>, int> <>9__8_0;
                public static Func<KeyValuePair<int, PdfExportFontGlyphInfo>, double> <>9__8_1;

                internal int <get_ToUnicode>b__6_0(KeyValuePair<int, PdfExportFontGlyphInfo> key) => 
                    key.Key;

                internal string <get_ToUnicode>b__6_1(KeyValuePair<int, PdfExportFontGlyphInfo> value) => 
                    value.Value.Unicode;

                internal int <get_Widths>b__8_0(KeyValuePair<int, PdfExportFontGlyphInfo> key) => 
                    key.Key;

                internal double <get_Widths>b__8_1(KeyValuePair<int, PdfExportFontGlyphInfo> value) => 
                    (double) value.Value.Width;
            }
        }
    }
}

