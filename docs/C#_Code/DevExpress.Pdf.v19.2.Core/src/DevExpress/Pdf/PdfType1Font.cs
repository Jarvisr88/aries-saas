namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfType1Font : PdfSimpleFont
    {
        internal const string Name = "Type1";
        internal const string TimesRomanFontName = "Times-Roman";
        internal const string TimesBoldFontName = "Times-Bold";
        internal const string TimesItalicFontName = "Times-Italic";
        internal const string TimesBoldItalicFontName = "Times-BoldItalic";
        internal const string HelveticaObliqueFontName = "Helvetica-Oblique";
        internal const string HelveticaBoldObliqueFontName = "Helvetica-BoldOblique";
        internal const string CourierFontName = "Courier";
        internal const string CourierBoldFontName = "Courier-Bold";
        internal const string CourierObliqueFontName = "Courier-Oblique";
        internal const string CourierBoldObliqueFontName = "Courier-BoldOblique";
        internal const string SymbolFontName = "Symbol";
        internal const string ZapfDingbatsFontName = "ZapfDingbats";
        internal const string FontFileSubtype = "Type1C";
        private readonly PdfType1FontFileData fontFileData;
        private readonly byte[] compactFontFileData;
        private readonly byte[] openTypeFontFileData;
        private readonly PdfMetadata metadata;

        internal PdfType1Font(string baseFont, PdfSimpleFontEncoding fontEncoding, PdfFontDescriptor fontDescriptor, int firstChar, double[] widths) : base(baseFont, fontEncoding, fontDescriptor, firstChar, widths)
        {
        }

        internal PdfType1Font(PdfReaderDictionary dictionary, string baseFont, PdfReaderStream toUnicode, PdfReaderDictionary fontDescriptor, PdfSimpleFontEncoding encoding, int? firstChar, int? lastChar, double[] widths) : base(dictionary.Number, baseFont, toUnicode, fontDescriptor, encoding, firstChar, lastChar, widths)
        {
            if (fontDescriptor != null)
            {
                this.fontFileData = PdfType1FontFileData.Parse(fontDescriptor);
                if (this.fontFileData == null)
                {
                    PdfReaderStream stream = fontDescriptor.GetStream("FontFile3");
                    if (stream != null)
                    {
                        this.openTypeFontFileData = base.GetOpenTypeFontFileData(stream, true);
                        if (this.openTypeFontFileData != null)
                        {
                            this.compactFontFileData = PdfFontFile.GetCFFData(this.openTypeFontFileData);
                        }
                        else
                        {
                            if (stream.Dictionary.GetName("Subtype") != "Type1C")
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            this.compactFontFileData = PdfFontFile.GetValidCFFData(stream.UncompressedData);
                        }
                    }
                }
                this.metadata = fontDescriptor.GetMetadata();
            }
        }

        protected override PdfFontProgramFacade CreateFontProgramFacade() => 
            (this.compactFontFileData == null) ? ((this.fontFileData == null) ? ((PdfFontProgramFacade) PdfNonEmbeddedFontProgramFacade.Create(this)) : ((PdfFontProgramFacade) PdfType1FontProgramFacade.Create(this, this.fontFileData))) : ((PdfFontProgramFacade) PdfCFFFontProgramFacade.Create(this, this.compactFontFileData));

        protected internal override void UpdateFontDescriptorDictionary(PdfWriterDictionary dictionary)
        {
            if (!base.WriteOpenTypeFontData(dictionary, this.openTypeFontFileData))
            {
                if (this.fontFileData != null)
                {
                    this.fontFileData.Write(dictionary);
                }
                else if (this.compactFontFileData != null)
                {
                    PdfObjectCollection objects = dictionary.Objects;
                    PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
                    dictionary2.AddName("Subtype", "Type1C");
                    dictionary.Add("FontFile3", objects.AddStream(dictionary2, this.compactFontFileData));
                }
            }
        }

        public byte[] FontFileData =>
            this.fontFileData?.Data;

        public int PlainTextLength =>
            (this.fontFileData == null) ? 0 : this.fontFileData.PlainTextLength;

        public int CipherTextLength =>
            (this.fontFileData == null) ? 0 : this.fontFileData.CipherTextLength;

        public int NullSegmentLength =>
            (this.fontFileData == null) ? 0 : this.fontFileData.NullSegmentLength;

        public byte[] CompactFontFileData =>
            this.compactFontFileData;

        public byte[] OpenTypeFontFileData =>
            this.openTypeFontFileData;

        public PdfMetadata Metadata =>
            this.metadata;

        protected internal override string Subtype =>
            "Type1";
    }
}

