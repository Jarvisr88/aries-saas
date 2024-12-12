namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfSimpleFont : PdfFont
    {
        private const string firstCharDictionaryKey = "FirstChar";
        private const string lastCharDictionaryKey = "LastChar";
        private const string widthsDictionaryKey = "Widths";
        internal const string CourierNewFontName = "CourierNew";
        internal const string HelveticaFontName = "Helvetica";
        internal const string HelveticaBoldFontName = "Helvetica-Bold";
        internal const string ArialFontName = "Arial";
        internal const string TimesNewRomanFontName = "TimesNewRoman";
        protected const string ArialBoldFontName = "Arial,Bold";
        protected const string TimesNewRomanBoldFontName = "TimesNewRoman,Bold";
        private readonly PdfSimpleFontEncoding encoding;
        private readonly int? firstChar;
        private readonly int? lastChar;
        private readonly double[] widths;
        private readonly PdfFontDescriptor predefinedFontDescriptor;
        private double[] actualWidths;

        protected PdfSimpleFont(string baseFont, PdfSimpleFontEncoding encoding, PdfFontDescriptor fontDescriptor, int firstChar, double[] widths) : base(baseFont, fontDescriptor)
        {
            this.encoding = encoding;
            this.firstChar = new int?(firstChar);
            this.lastChar = 0xff;
            this.widths = widths;
        }

        protected PdfSimpleFont(int objectNumber, string baseFont, PdfReaderStream toUnicode, PdfReaderDictionary fontDescriptor, PdfSimpleFontEncoding encoding, int? firstChar, int? lastChar, double[] widths) : base(objectNumber, baseFont, toUnicode, fontDescriptor)
        {
            if (fontDescriptor == null)
            {
                PdfFontDescriptorData fontDescriptorData = PdfStandardFontFamily.GetFontDescriptorData(baseFont);
                if (fontDescriptorData != null)
                {
                    this.predefinedFontDescriptor = new PdfFontDescriptor(fontDescriptorData);
                    this.predefinedFontDescriptor.SetFont(this);
                }
            }
            this.encoding = encoding;
            this.firstChar = firstChar;
            this.lastChar = lastChar;
            this.widths = widths;
        }

        internal static PdfSimpleFont Create(string subtype, string baseFont, PdfReaderDictionary dictionary)
        {
            double[] numArray;
            PdfSimpleFontEncoding encoding = PdfSimpleFontEncoding.Create(baseFont, GetEncodingValue(dictionary));
            PdfReaderStream toUnicode = dictionary.GetStream("ToUnicode");
            int? integer = dictionary.GetInteger("FirstChar");
            int? lastChar = dictionary.GetInteger("LastChar");
            IList<object> array = dictionary.GetArray("Widths");
            if (array == null)
            {
                numArray = null;
            }
            else
            {
                try
                {
                    int count = array.Count;
                    numArray = new double[count];
                    for (int i = 0; i < count; i++)
                    {
                        numArray[i] = dictionary.Objects.GetDouble(array[i]);
                    }
                }
                catch
                {
                    numArray = null;
                }
            }
            PdfReaderDictionary fontDescriptor = dictionary.GetDictionary("FontDescriptor");
            if (subtype == "Type1")
            {
                return new PdfType1Font(dictionary, baseFont, toUnicode, fontDescriptor, encoding, integer, lastChar, numArray);
            }
            if (subtype == "MMType1")
            {
                if ((integer == null) || ((lastChar == null) || ((array == null) || (fontDescriptor == null))))
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return new PdfMMType1Font(dictionary, baseFont, toUnicode, fontDescriptor, encoding, integer.Value, lastChar.Value, numArray);
            }
            if (subtype == "TrueType")
            {
                return new PdfTrueTypeFont(dictionary.Number, baseFont, toUnicode, fontDescriptor, encoding, integer, lastChar, numArray);
            }
            if (subtype != "Type3")
            {
                return new PdfUnknownFont(dictionary, baseFont, toUnicode, fontDescriptor, encoding, integer, lastChar, numArray);
            }
            if ((integer == null) || ((lastChar == null) || (array == null)))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return new PdfType3Font(dictionary.Number, toUnicode, fontDescriptor, encoding, integer.Value, lastChar.Value, numArray, dictionary);
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.AddNullable<int>("FirstChar", this.firstChar);
            dictionary.AddNullable<int>("LastChar", this.lastChar);
            dictionary.Add("Widths", this.widths);
            dictionary.AddIfPresent("FontDescriptor", objects.AddObject((PdfObject) base.RawFontDescriptor));
            return dictionary;
        }

        protected override string GetCharacterUnicodeFallback(byte[] character)
        {
            short num3;
            short num = 0;
            int length = character.Length;
            for (int i = 0; i < length; i++)
            {
                num = (short) ((num << 8) + character[i]);
            }
            if (!PdfUnicodeConverter.TryGetGlyphCode(this.Encoding.GetGlyphName((byte) num), out num3))
            {
                num3 = num;
            }
            return new string((char) ((ushort) num3), 1);
        }

        internal override double GetCharacterWidth(int charCode)
        {
            int index = charCode - ((this.firstChar != null) ? this.firstChar.Value : 0);
            double[] widths = this.Widths;
            return (((widths == null) || ((index < 0) || (index >= widths.Length))) ? ((this.FontDescriptor == null) ? 0.0 : this.FontDescriptor.MissingWidth) : widths[index]);
        }

        public override PdfFontDescriptor FontDescriptor =>
            this.predefinedFontDescriptor ?? base.FontDescriptor;

        public PdfSimpleFontEncoding Encoding =>
            this.encoding;

        public int FirstChar
        {
            get
            {
                int? firstChar = this.firstChar;
                return ((firstChar != null) ? firstChar.GetValueOrDefault() : 0);
            }
        }

        public int LastChar
        {
            get
            {
                if (this.lastChar != null)
                {
                    int num = this.lastChar.Value;
                    if (num >= this.FirstChar)
                    {
                        return num;
                    }
                }
                return 0xff;
            }
        }

        public double[] Widths
        {
            get
            {
                if (this.actualWidths == null)
                {
                    this.actualWidths = this.widths;
                    if (this.actualWidths == null)
                    {
                        int firstChar = this.FirstChar;
                        int num2 = (this.LastChar - firstChar) + 1;
                        IPdfGlyphWidthProvider glyphWidthProvider = PdfStandardFontFamily.GetGlyphWidthProvider(base.BaseFont);
                        if (glyphWidthProvider != null)
                        {
                            this.actualWidths = new double[num2];
                            byte code = (byte) firstChar;
                            int index = 0;
                            while (index < num2)
                            {
                                string glyphName = this.encoding.GetGlyphName(code);
                                this.actualWidths[index] = glyphWidthProvider.GetGlyphWidth(glyphName);
                                index++;
                                code = (byte) (code + 1);
                            }
                        }
                    }
                }
                return this.actualWidths;
            }
        }

        protected internal override IEnumerable<double> GlyphWidths =>
            this.Widths;

        protected internal override PdfEncoding ActualEncoding =>
            this.encoding;
    }
}

