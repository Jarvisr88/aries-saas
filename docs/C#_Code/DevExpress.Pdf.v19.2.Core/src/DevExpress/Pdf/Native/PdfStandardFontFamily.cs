namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public abstract class PdfStandardFontFamily
    {
        private static readonly IDictionary<string, PdfStandardFontFamily> nameMapping;

        static PdfStandardFontFamily()
        {
            PdfStandardFontFamily family = new PdfHelveticaFontFamily();
            PdfStandardFontFamily family2 = new PdfTimesFontFamily();
            PdfStandardFontFamily family3 = new PdfZapfFontFamily();
            PdfStandardFontFamily family4 = new PdfSymbolFontFamily();
            PdfStandardFontFamily family5 = new PdfCourierFontFamily();
            nameMapping = new Dictionary<string, PdfStandardFontFamily>();
            nameMapping.Add(PdfFontNamesHelper.GetNormalizedFontFamily("TimesNewRoman"), family2);
            nameMapping.Add(PdfFontNamesHelper.GetNormalizedFontFamily("Times-Roman"), family2);
            nameMapping.Add(PdfFontNamesHelper.GetNormalizedFontFamily("Times"), family2);
            nameMapping.Add(PdfFontNamesHelper.GetNormalizedFontFamily("Courier"), family5);
            nameMapping.Add(PdfFontNamesHelper.GetNormalizedFontFamily("CourierNew"), family5);
            nameMapping.Add(PdfFontNamesHelper.GetNormalizedFontFamily("Arial"), family);
            nameMapping.Add("arialmt", family);
            nameMapping.Add(PdfFontNamesHelper.GetNormalizedFontFamily("Helvetica"), family);
            nameMapping.Add(PdfFontNamesHelper.GetNormalizedFontFamily("ZapfDingbats"), family3);
            nameMapping.Add(PdfFontNamesHelper.GetNormalizedFontFamily("Symbol"), family4);
        }

        protected PdfStandardFontFamily()
        {
        }

        private static PdfStandardFontFamily GetFamily(string fontFamily)
        {
            PdfStandardFontFamily family;
            return (!nameMapping.TryGetValue(PdfFontNamesHelper.GetNormalizedFontFamily(fontFamily), out family) ? null : family);
        }

        private PdfFontDescriptorData GetFontDescriptorData(PdfFontStyle fontStyle) => 
            this.FontDescriptorProvider.GetData(fontStyle);

        public static PdfFontDescriptorData GetFontDescriptorData(string baseFont) => 
            GetFamily(baseFont)?.GetFontDescriptorData(GetStyle(baseFont));

        private IPdfGlyphWidthProvider GetGlyphWidthProvider(PdfFontStyle fontStyle) => 
            this.GlyphWidthProvider.GetData(fontStyle);

        public static IPdfGlyphWidthProvider GetGlyphWidthProvider(string baseFont) => 
            GetFamily(baseFont)?.GetGlyphWidthProvider(GetStyle(baseFont));

        private static PdfFontStyle GetStyle(string baseFont)
        {
            string normalizedFontStyle = PdfFontNamesHelper.GetNormalizedFontStyle(baseFont);
            PdfFontStyle regular = PdfFontStyle.Regular;
            if (PdfFontNamesHelper.ContainsBoldStyle(normalizedFontStyle))
            {
                regular |= PdfFontStyle.Bold;
            }
            if (PdfFontNamesHelper.ContainsItalicStyle(normalizedFontStyle))
            {
                regular |= PdfFontStyle.Italic;
            }
            return regular;
        }

        protected abstract PdfStandardFontFamilyDataProvider<IPdfGlyphWidthProvider> GlyphWidthProvider { get; }

        protected abstract PdfStandardFontFamilyDataProvider<PdfFontDescriptorData> FontDescriptorProvider { get; }

        protected class PdfMonospacedGlyphWidthProvider : IPdfGlyphWidthProvider
        {
            private readonly short glyphWidth;

            public PdfMonospacedGlyphWidthProvider(short glyphWidth)
            {
                this.glyphWidth = glyphWidth;
            }

            public short GetGlyphWidth(string glyphName) => 
                this.glyphWidth;
        }

        protected abstract class PdfStandardFontFamilyDataProvider<T>
        {
            protected PdfStandardFontFamilyDataProvider()
            {
            }

            public T GetData(PdfFontStyle fontStyle) => 
                !fontStyle.HasFlag(PdfFontStyle.Bold) ? (!fontStyle.HasFlag(PdfFontStyle.Italic) ? this.Regular : this.Italic) : (fontStyle.HasFlag(PdfFontStyle.Italic) ? this.BoldItalic : this.Bold);

            protected abstract T Regular { get; }

            protected virtual T Bold =>
                this.Regular;

            protected virtual T Italic =>
                this.Regular;

            protected virtual T BoldItalic =>
                this.Regular;
        }

        protected class PdfVariableGlyphWidthProvider : IPdfGlyphWidthProvider
        {
            private readonly IDictionary<string, short> dictionary;

            public PdfVariableGlyphWidthProvider(IDictionary<string, short> dictionary)
            {
                this.dictionary = dictionary;
            }

            public short GetGlyphWidth(string glyphName)
            {
                short num;
                return (!this.dictionary.TryGetValue(glyphName, out num) ? 0 : num);
            }
        }
    }
}

