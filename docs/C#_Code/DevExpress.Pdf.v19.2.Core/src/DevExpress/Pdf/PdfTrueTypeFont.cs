namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfTrueTypeFont : PdfSimpleFont
    {
        internal const string Name = "TrueType";
        internal const string FontFile2DictionaryKey = "FontFile2";
        private const string length1DictionaryKey = "Length1";
        private readonly byte[] fontFileData;
        private readonly byte[] openTypeFontFileData;
        private readonly PdfType1FontFileData type1FontFileData;
        private readonly PdfMetadata metadata;
        private readonly byte[] compactFontFileData;

        internal PdfTrueTypeFont(string baseFont, PdfSimpleFontEncoding fontEncoding, PdfFontDescriptor fontDescriptor, int firstChar, double[] widths) : base(baseFont, fontEncoding, fontDescriptor, firstChar, widths)
        {
        }

        internal PdfTrueTypeFont(int objectNumber, string baseFont, PdfReaderStream toUnicode, PdfReaderDictionary fontDescriptor, PdfSimpleFontEncoding encoding, int? firstChar, int? lastChar, double[] widths) : base(objectNumber, baseFont, toUnicode, fontDescriptor, encoding, firstChar, lastChar, widths)
        {
            if (fontDescriptor != null)
            {
                PdfReaderStream stream = fontDescriptor.GetStream("FontFile2");
                if (stream != null)
                {
                    byte[] uncompressedData = stream.UncompressedData;
                    if (PdfFontFile.IsOpenType(uncompressedData))
                    {
                        this.openTypeFontFileData = uncompressedData;
                        this.compactFontFileData = PdfFontFile.GetCFFData(this.openTypeFontFileData);
                    }
                    else
                    {
                        this.fontFileData = uncompressedData;
                        if (PdfType1FontFileData.IsPfbData(uncompressedData))
                        {
                            this.type1FontFileData = new PdfType1FontFileData(uncompressedData, uncompressedData.Length, 0, 0);
                        }
                    }
                }
                else
                {
                    this.openTypeFontFileData = base.GetOpenTypeFontFileData(fontDescriptor, true);
                    if (this.openTypeFontFileData != null)
                    {
                        this.compactFontFileData = PdfFontFile.GetCFFData(this.openTypeFontFileData);
                    }
                    else
                    {
                        PdfReaderStream stream2 = fontDescriptor.GetStream("FontFile3");
                        if (stream2 != null)
                        {
                            if (stream2.Dictionary.GetName("Subtype") != "Type1C")
                            {
                                PdfDocumentStructureReader.ThrowIncorrectDataException();
                            }
                            byte[] uncompressedData = stream2.UncompressedData;
                            try
                            {
                                PdfType1FontCompactFontProgram.Parse(uncompressedData);
                                this.compactFontFileData = uncompressedData;
                            }
                            catch
                            {
                                this.type1FontFileData = new PdfType1FontFileData(uncompressedData, uncompressedData.Length, 0, 0);
                            }
                        }
                    }
                }
                this.metadata = fontDescriptor.GetMetadata();
            }
        }

        protected override PdfFontProgramFacade CreateFontProgramFacade()
        {
            PdfFontProgramFacade facade;
            if (this.compactFontFileData != null)
            {
                return PdfCFFFontProgramFacade.Create(this, this.compactFontFileData);
            }
            if (this.type1FontFileData == null)
            {
                if (this.fontFileData == null)
                {
                    return PdfNonEmbeddedFontProgramFacade.Create(this);
                }
                try
                {
                    facade = PdfTrueTypeFontProgramFacade.Create(this, this.fontFileData);
                }
                catch
                {
                    facade = PdfNonEmbeddedFontProgramFacade.Create(this);
                }
            }
            else if (!PdfFontFile.IsTrueType(this.type1FontFileData.Data))
            {
                goto TR_0001;
            }
            else
            {
                try
                {
                    facade = PdfTrueTypeFontProgramFacade.Create(this, this.type1FontFileData.Data);
                }
                catch
                {
                    goto TR_0001;
                }
            }
            return facade;
        TR_0001:
            return PdfType1FontProgramFacade.Create(this, this.type1FontFileData);
        }

        protected internal override void UpdateFontDescriptorDictionary(PdfWriterDictionary dictionary)
        {
            base.UpdateFontDescriptorDictionary(dictionary);
            if (this.fontFileData == null)
            {
                base.WriteOpenTypeFontData(dictionary, this.openTypeFontFileData);
            }
            else
            {
                WriteFontData(dictionary, this.fontFileData);
            }
            dictionary.Add("Metadata", this.metadata);
        }

        internal static void WriteFontData(PdfWriterDictionary dictionary, byte[] fontFileData)
        {
            PdfObjectCollection objects = dictionary.Objects;
            PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
            dictionary2.Add("Length1", fontFileData.Length);
            dictionary.Add("FontFile2", objects.AddStream(dictionary2, fontFileData));
        }

        public byte[] FontFileData =>
            this.fontFileData;

        public byte[] OpenTypeFontFileData =>
            this.openTypeFontFileData;

        public PdfMetadata Metadata =>
            this.metadata;

        protected internal override string Subtype =>
            "TrueType";
    }
}

