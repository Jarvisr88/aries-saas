namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfCIDType0Font : PdfType0Font
    {
        private const string fontFileSubtype = "CIDFontType0C";
        private readonly PdfType1FontFileData fontFileData;
        private readonly byte[] compactFontFileData;
        private readonly byte[] openTypeFontFileData;
        private double[] widths;

        internal PdfCIDType0Font(int objectNumber, string baseFont, PdfReaderStream toUnicode, PdfReaderDictionary fontDescriptor, PdfCompositeFontEncoding encoding, PdfReaderDictionary dictionary) : base(objectNumber, baseFont, toUnicode, fontDescriptor, encoding, dictionary)
        {
            PdfReaderStream stream = fontDescriptor.GetStream("FontFile3");
            if (stream == null)
            {
                this.fontFileData = PdfType1FontFileData.Parse(fontDescriptor);
                if ((this.fontFileData == null) && (!(this.ActualEncoding is PdfPredefinedCompositeFontEncoding) && !(this.ActualEncoding is PdfIdentityEncoding)))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            else
            {
                this.openTypeFontFileData = base.GetOpenTypeFontFileData(stream, true);
                if (this.openTypeFontFileData == null)
                {
                    this.compactFontFileData = PdfFontFile.GetValidCFFData(stream.UncompressedData);
                }
                else
                {
                    this.compactFontFileData = PdfFontFile.GetCFFData(this.openTypeFontFileData);
                }
            }
        }

        protected override PdfFontProgramFacade CreateFontProgramFacade() => 
            (this.compactFontFileData == null) ? ((this.fontFileData == null) ? ((PdfFontProgramFacade) PdfNonEmbeddedFontProgramFacade.Create(this)) : ((PdfFontProgramFacade) PdfType1FontProgramFacade.Create(this, this.fontFileData))) : ((PdfFontProgramFacade) PdfCFFFontProgramFacade.Create(this, this.compactFontFileData));

        protected internal override void UpdateFontDescriptorDictionary(PdfWriterDictionary dictionary)
        {
            base.UpdateFontDescriptorDictionary(dictionary);
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
                    dictionary2.AddName("Subtype", "CIDFontType0C");
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

        protected override string CIDSubType =>
            "CIDFontType0";

        protected internal override IEnumerable<double> GlyphWidths
        {
            get
            {
                if (this.widths == null)
                {
                    IDictionary<int, double> widths = base.Widths;
                    ICollection<int> keys = widths.Keys;
                    int count = keys.Count;
                    if (count <= 0)
                    {
                        this.widths = new double[1];
                    }
                    else
                    {
                        int defaultWidth = base.DefaultWidth;
                        List<int> list = new List<int>(keys);
                        int num3 = list[0];
                        int num4 = list[count - 1];
                        int num5 = (num4 - num3) + 1;
                        this.widths = new double[num5];
                        int index = 0;
                        for (int i = num3; i <= num4; i++)
                        {
                            double num8;
                            this.widths[index] = widths.TryGetValue(i, out num8) ? num8 : ((double) defaultWidth);
                            index++;
                        }
                    }
                }
                return this.widths;
            }
        }
    }
}

