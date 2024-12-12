namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfFont : PdfObject
    {
        internal const string DictionaryType = "Font";
        internal const string BaseFontDictionaryKey = "BaseFont";
        protected const string FontDescriptorDictionaryKey = "FontDescriptor";
        protected const string ToUnicodeDictionaryKey = "ToUnicode";
        protected const string FontFile3DictionaryKey = "FontFile3";
        private const string encodingDictionaryKey = "Encoding";
        private const string openTypeFontSubtype = "OpenType";
        private const int subsetNameLength = 6;
        private const int subsetPrefixLength = 7;
        private readonly string baseFont;
        private readonly string subsetName;
        private readonly string fontName;
        private readonly PdfFontDescriptor fontDescriptor;
        private readonly string registrationName;
        private PdfCharacterMapping toUnicode;
        private PdfFontProgramFacade fontProgramFacade;
        private Lazy<PdfFontMetricsMetadata> metrics;
        private Lazy<PdfToUnicodeCMap> cmap;

        private PdfFont(int objectNumber, string baseFont) : base(objectNumber)
        {
            this.subsetName = string.Empty;
            this.registrationName = Guid.NewGuid().ToString("N").Substring(0, 0x1f);
            this.baseFont = baseFont;
            if ((baseFont.Length >= 7) && (baseFont[6] == '+'))
            {
                this.subsetName = baseFont.Substring(0, 6);
                foreach (char ch in this.subsetName)
                {
                    if (!char.IsUpper(ch))
                    {
                        this.subsetName = string.Empty;
                        break;
                    }
                }
            }
            this.fontName = string.IsNullOrEmpty(this.subsetName) ? baseFont : baseFont.Substring(7);
            this.metrics = new Lazy<PdfFontMetricsMetadata>(new Func<PdfFontMetricsMetadata>(this.CreateValidatedMetrics));
        }

        protected PdfFont(string baseFont, PdfFontDescriptor fontDescriptor) : this(-1, baseFont)
        {
            this.fontDescriptor = fontDescriptor;
            if (fontDescriptor != null)
            {
                fontDescriptor.SetFont(this);
            }
        }

        protected PdfFont(int objectNumber, string baseFont, PdfReaderStream toUnicodeStream, PdfReaderDictionary fontDescriptorDictionary) : this(objectNumber, baseFont)
        {
            if (fontDescriptorDictionary != null)
            {
                this.fontDescriptor = this.CreateFontDescriptor(fontDescriptorDictionary);
            }
            if (toUnicodeStream != null)
            {
                try
                {
                    this.SetCMap(new PdfCharacterMapping(toUnicodeStream.UncompressedData));
                }
                catch
                {
                }
            }
        }

        protected virtual PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "Font");
            dictionary.Add("Subtype", new PdfName(this.Subtype));
            if (this.baseFont != null)
            {
                dictionary.Add("BaseFont", new PdfName(this.baseFont));
            }
            object obj2 = this.ActualEncoding.Write(objects);
            if (obj2 != null)
            {
                dictionary.Add("Encoding", obj2);
            }
            if (this.toUnicode != null)
            {
                dictionary.Add("ToUnicode", objects.AddStream(this.toUnicode.Data));
            }
            return dictionary;
        }

        internal static PdfFont CreateFont(PdfReaderDictionary fontDictionary)
        {
            string name = fontDictionary.GetName("Subtype");
            string fontName = GetFontName(fontDictionary, "BaseFont");
            if ((name == null) && (fontName == null))
            {
                return null;
            }
            if ((name != "Type3") && (fontName == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return (((name == null) || (name != "Type0")) ? ((PdfFont) PdfSimpleFont.Create(name, fontName, fontDictionary)) : ((PdfFont) PdfType0Font.Create(fontName, fontDictionary)));
        }

        protected virtual PdfFontDescriptor CreateFontDescriptor(PdfReaderDictionary dictionary) => 
            new PdfFontDescriptor(this, dictionary);

        protected abstract PdfFontProgramFacade CreateFontProgramFacade();
        protected virtual PdfFontMetricsMetadata CreateValidatedMetrics()
        {
            PdfRectangle fontBBox = this.FontProgramFacade.FontBBox;
            double ascent = 0.0;
            double descent = 0.0;
            PdfFontDescriptor fontDescriptor = this.FontDescriptor;
            if ((this.FontProgramFacade.Top != null) && (this.FontProgramFacade.Bottom != null))
            {
                ascent = this.FontProgramFacade.Top.Value;
                descent = this.FontProgramFacade.Bottom.Value;
            }
            if (((ascent == 0.0) || (descent == 0.0)) && (fontDescriptor != null))
            {
                PdfRectangle rectangle2 = fontDescriptor.FontBBox;
                if (rectangle2 != null)
                {
                    ascent = rectangle2.Top;
                    descent = rectangle2.Bottom;
                }
                if ((ascent - descent) > 2048.0)
                {
                    ascent = (ascent / 2048.0) * 1000.0;
                    descent = (descent / 2048.0) * 1000.0;
                }
            }
            if (((ascent == 0.0) || (descent == 0.0)) && (fontBBox != null))
            {
                ascent = fontBBox.Top;
                descent = fontBBox.Bottom;
            }
            if (((ascent == 0.0) || (descent == 0.0)) && (fontDescriptor != null))
            {
                ascent = fontDescriptor.Ascent;
                descent = fontDescriptor.Descent;
            }
            return new PdfFontMetricsMetadata(ascent, descent, 1000.0);
        }

        internal string GetCharacterUnicode(byte[] character)
        {
            PdfToUnicodeCMap local2;
            if (this.cmap != null)
            {
                local2 = this.cmap.Value;
            }
            else
            {
                Lazy<PdfToUnicodeCMap> cmap = this.cmap;
                local2 = null;
            }
            PdfToUnicodeCMap map = local2;
            if ((map != null) && !map.IsEmpty)
            {
                string unicode = map.GetUnicode(character);
                if (!string.IsNullOrEmpty(unicode))
                {
                    return unicode;
                }
            }
            return this.GetCharacterUnicodeFallback(character);
        }

        protected abstract string GetCharacterUnicodeFallback(byte[] character);
        internal abstract double GetCharacterWidth(int charCode);
        internal static object GetEncodingValue(PdfReaderDictionary dictionary)
        {
            object obj2;
            return (dictionary.TryGetValue("Encoding", out obj2) ? dictionary.Objects.TryResolve(obj2, null) : null);
        }

        internal static string GetFontName(PdfReaderDictionary dictionary, string dictionaryKey)
        {
            object obj2;
            if (!dictionary.TryGetValue(dictionaryKey, out obj2))
            {
                return null;
            }
            obj2 = dictionary.Objects.TryResolve(obj2, null);
            if (obj2 == null)
            {
                return null;
            }
            PdfName name = obj2 as PdfName;
            if (name != null)
            {
                return name.Name;
            }
            byte[] buffer = obj2 as byte[];
            if (buffer == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return PdfDocumentReader.ConvertToString(buffer);
        }

        protected byte[] GetOpenTypeFontFileData(PdfReaderDictionary dictionary, bool suppressException) => 
            this.GetOpenTypeFontFileData(dictionary.GetStream("FontFile3"), suppressException);

        protected byte[] GetOpenTypeFontFileData(PdfReaderStream stream, bool suppressException)
        {
            if (stream == null)
            {
                return null;
            }
            if (stream.Dictionary.GetName("Subtype") != "OpenType")
            {
                if (suppressException)
                {
                    return null;
                }
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return stream.UncompressedData;
        }

        private void SetCMap(PdfCharacterMapping mapping)
        {
            this.toUnicode = mapping;
            this.cmap = new Lazy<PdfToUnicodeCMap>(delegate {
                PdfToUnicodeCMap map;
                try
                {
                    map = PdfToUnicodeCMap.Parse(mapping.Data);
                }
                catch
                {
                    return null;
                }
                return map;
            });
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.CreateDictionary(objects);

        protected internal virtual void UpdateFontDescriptorDictionary(PdfWriterDictionary dictionary)
        {
        }

        protected bool WriteOpenTypeFontData(PdfWriterDictionary dictionary, byte[] openTypeFontData)
        {
            if (openTypeFontData == null)
            {
                return false;
            }
            PdfObjectCollection objects = dictionary.Objects;
            PdfWriterDictionary dictionary2 = new PdfWriterDictionary(objects);
            dictionary2.AddName("Subtype", "OpenType");
            dictionary.Add("FontFile3", objects.AddStream(dictionary2, openTypeFontData));
            return true;
        }

        protected PdfFontDescriptor RawFontDescriptor =>
            this.fontDescriptor;

        internal string RegistrationName =>
            string.IsNullOrEmpty(this.registrationName) ? this.baseFont : this.registrationName;

        public string BaseFont =>
            this.baseFont;

        public string SubsetName =>
            this.subsetName;

        public string FontName =>
            this.fontName;

        public PdfCharacterMapping ToUnicode
        {
            get => 
                this.toUnicode;
            internal set => 
                this.SetCMap(value);
        }

        public virtual PdfFontDescriptor FontDescriptor =>
            this.fontDescriptor;

        protected internal virtual double WidthFactor =>
            0.001;

        protected internal virtual double HeightFactor =>
            0.001;

        protected internal virtual bool HasSizeAttributes =>
            true;

        protected internal abstract string Subtype { get; }

        protected internal abstract IEnumerable<double> GlyphWidths { get; }

        protected internal abstract PdfEncoding ActualEncoding { get; }

        internal bool IsSymbolic =>
            (this.fontDescriptor != null) && (this.fontDescriptor.Flags.HasFlag(PdfFontFlags.Symbolic) && !this.fontDescriptor.Flags.HasFlag(PdfFontFlags.Nonsymbolic));

        internal PdfFontProgramFacade FontProgramFacade
        {
            get
            {
                this.fontProgramFacade ??= this.CreateFontProgramFacade();
                return this.fontProgramFacade;
            }
            set
            {
                this.fontProgramFacade = value;
                this.metrics = new Lazy<PdfFontMetricsMetadata>(new Func<PdfFontMetricsMetadata>(this.CreateValidatedMetrics));
            }
        }

        internal PdfFontMetricsMetadata Metrics =>
            this.metrics.Value;
    }
}

