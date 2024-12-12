namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfCIDType2Font : PdfType0Font
    {
        private byte[] openTypeFontFileData;
        private byte[] fontFileData;
        private byte[] compactFontFileData;

        protected PdfCIDType2Font(string baseFont, PdfCompositeFontDescriptor fontDescriptor) : base(baseFont, fontDescriptor)
        {
        }

        internal PdfCIDType2Font(int objectNumber, string baseFont, PdfReaderStream toUnicode, PdfReaderDictionary fontDescriptor, PdfCompositeFontEncoding encoding, PdfReaderDictionary dictionary) : base(objectNumber, baseFont, toUnicode, fontDescriptor, encoding, dictionary)
        {
            PdfReaderStream stream = fontDescriptor.GetStream("FontFile2");
            try
            {
                if (stream == null)
                {
                    this.openTypeFontFileData = base.GetOpenTypeFontFileData(fontDescriptor, false);
                    if (this.openTypeFontFileData != null)
                    {
                        this.compactFontFileData = PdfFontFile.GetCFFData(this.openTypeFontFileData);
                    }
                }
                else if (stream.Dictionary.GetName("Subtype") == "Type1C")
                {
                    this.compactFontFileData = stream.UncompressedData;
                }
                else
                {
                    this.fontFileData = stream.UncompressedData;
                    if (this.fontFileData != null)
                    {
                        this.compactFontFileData = PdfFontFile.GetCFFData(this.fontFileData);
                    }
                }
            }
            catch
            {
            }
        }

        protected override PdfWriterDictionary CreateDescendantDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDescendantDictionary(objects);
            if (base.CidToGidMap == null)
            {
                dictionary.Add("CIDToGIDMap", new PdfName("Identity"));
            }
            return dictionary;
        }

        protected override PdfFontProgramFacade CreateFontProgramFacade()
        {
            if (this.compactFontFileData != null)
            {
                return PdfCFFFontProgramFacade.Create(this, this.compactFontFileData);
            }
            if ((this.fontFileData == null) && (this.openTypeFontFileData == null))
            {
                return PdfNonEmbeddedFontProgramFacade.Create(this);
            }
            byte[] fontFileData = this.fontFileData;
            if (this.fontFileData == null)
            {
                byte[] local1 = this.fontFileData;
                fontFileData = this.openTypeFontFileData;
            }
            return PdfTrueTypeFontProgramFacade.Create(this, fontFileData);
        }

        protected internal override void UpdateFontDescriptorDictionary(PdfWriterDictionary dictionary)
        {
            PdfObjectCollection objects = dictionary.Objects;
            if (this.fontFileData != null)
            {
                PdfTrueTypeFont.WriteFontData(dictionary, this.fontFileData);
            }
            else if ((this.openTypeFontFileData != null) || (this.compactFontFileData == null))
            {
                base.WriteOpenTypeFontData(dictionary, this.openTypeFontFileData);
            }
            else
            {
                PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
                dictionary2.Add("Subtype", new PdfName("Type1C"));
                dictionary.Add("FontFile2", objects.AddStream(dictionary2, this.compactFontFileData));
            }
        }

        public byte[] OpenTypeFontFileData
        {
            get => 
                this.openTypeFontFileData;
            internal set
            {
                this.openTypeFontFileData = value;
                this.compactFontFileData = PdfFontFile.GetCFFData(value);
                base.FontProgramFacade = PdfCFFFontProgramFacade.Create(this, this.compactFontFileData);
            }
        }

        public byte[] FontFileData
        {
            get => 
                this.fontFileData;
            internal set
            {
                this.fontFileData = value;
                base.FontProgramFacade = PdfTrueTypeFontProgramFacade.Create(this, this.fontFileData);
            }
        }

        protected override string CIDSubType =>
            "CIDFontType2";
    }
}

