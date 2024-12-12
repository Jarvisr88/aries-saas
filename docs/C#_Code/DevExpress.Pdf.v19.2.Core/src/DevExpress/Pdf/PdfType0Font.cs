namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public abstract class PdfType0Font : PdfFont
    {
        internal const string Name = "Type0";
        protected const string Subtype0Name = "CIDFontType0";
        protected const string Subtype2Name = "CIDFontType2";
        protected const string CidToGIDMapDictionaryKey = "CIDToGIDMap";
        private const string descendantFontsDictionaryKey = "DescendantFonts";
        private const string cidSystemInfoDictionaryKey = "CIDSystemInfo";
        private const string defaultWidthDictionaryKey = "DW";
        private const string widthsDictionaryKey = "W";
        private readonly PdfCompositeFontEncoding encoding;
        private readonly string cidBaseFont;
        private readonly PdfCIDSystemInfo systemInfo;
        private readonly int defaultWidth;
        private readonly short[] cidToGidMap;
        private readonly Lazy<PdfCIDCharset> charset;
        private IDictionary<int, double> widths;

        protected PdfType0Font(string baseFont, PdfCompositeFontDescriptor fontDescriptor) : base(baseFont, fontDescriptor)
        {
            this.widths = new SortedDictionary<int, double>();
            this.encoding = PdfIdentityEncoding.HorizontalIdentity;
            this.cidBaseFont = baseFont;
            this.systemInfo = new PdfCIDSystemInfo("Adobe", "Identity", 0);
            this.defaultWidth = 0x3e8;
            this.charset = new Lazy<PdfCIDCharset>(() => PdfCIDCharset.GetPredefinedCharset(this));
        }

        protected PdfType0Font(int objectNumber, string baseFont, PdfReaderStream toUnicode, PdfReaderDictionary fontDescriptor, PdfCompositeFontEncoding encoding, PdfReaderDictionary dictionary) : base(objectNumber, baseFont, toUnicode, fontDescriptor)
        {
            object obj2;
            this.widths = new SortedDictionary<int, double>();
            this.encoding = encoding;
            this.cidBaseFont = dictionary.GetName("BaseFont");
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("CIDSystemInfo");
            if ((this.cidBaseFont == null) || (dictionary2 == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            this.systemInfo = new PdfCIDSystemInfo(dictionary2);
            int? integer = dictionary.GetInteger("DW");
            this.defaultWidth = (integer != null) ? integer.GetValueOrDefault() : 0x3e8;
            PdfObjectCollection objects = dictionary.Objects;
            IList<object> array = dictionary.GetArray("W");
            if (array != null)
            {
                int num = -1;
                int num2 = -1;
                foreach (object obj3 in array)
                {
                    object obj4 = objects.TryResolve(obj3, null);
                    if (num < 0)
                    {
                        num = ConvertToInt(obj4);
                        continue;
                    }
                    if (num2 >= 0)
                    {
                        double width = objects.GetDouble(obj3);
                        int key = num;
                        while (true)
                        {
                            if (key > num2)
                            {
                                num = -1;
                                num2 = -1;
                                break;
                            }
                            this.AddWidth(key, width);
                            key++;
                        }
                        continue;
                    }
                    if (obj4 is int)
                    {
                        num2 = ConvertToInt(obj4);
                        continue;
                    }
                    IList<object> list2 = obj4 as IList<object>;
                    if (list2 == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    foreach (object obj5 in list2)
                    {
                        this.AddWidth(num++, objects.GetDouble(obj5));
                    }
                    num = -1;
                }
                if (num >= 0)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
            }
            if (dictionary.TryGetValue("CIDToGIDMap", out obj2))
            {
                obj2 = objects.TryResolve(obj2, null);
                PdfName name = obj2 as PdfName;
                if (name != null)
                {
                    if (name.Name != "Identity")
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                }
                else
                {
                    PdfReaderStream stream = obj2 as PdfReaderStream;
                    if (stream == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    byte[] uncompressedData = stream.UncompressedData;
                    int length = uncompressedData.Length;
                    if ((length % 2) > 0)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    int num6 = length / 2;
                    this.cidToGidMap = new short[num6];
                    int index = 0;
                    int num8 = 0;
                    while (index < num6)
                    {
                        int num9 = uncompressedData[num8++] << 8;
                        this.cidToGidMap[index] = (short) (num9 + uncompressedData[num8++]);
                        index++;
                    }
                }
            }
            this.charset = new Lazy<PdfCIDCharset>(() => PdfCIDCharset.GetPredefinedCharset(this));
        }

        private void AddWidth(int key, double width)
        {
            if (!this.widths.ContainsKey(key))
            {
                this.widths.Add(key, width);
            }
        }

        private static int ConvertToInt(object value)
        {
            if (!(value is int))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return (int) value;
        }

        internal static PdfType0Font Create(string baseFont, PdfReaderDictionary dictionary)
        {
            object obj2;
            PdfObjectCollection objects = dictionary.Objects;
            PdfReaderStream toUnicode = null;
            if (dictionary.TryGetValue("ToUnicode", out obj2))
            {
                obj2 = objects.TryResolve(obj2, null);
                PdfName name = obj2 as PdfName;
                if (name == null)
                {
                    toUnicode = obj2 as PdfReaderStream;
                }
                else
                {
                    string str2 = name.Name;
                    if ((str2 != "Identity-H") && (str2 != "Identity-V"))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                }
            }
            IList<object> array = dictionary.GetArray("DescendantFonts");
            if ((array == null) || (array.Count != 1))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfReaderDictionary dictionary2 = objects.TryResolve(array[0], null) as PdfReaderDictionary;
            if (dictionary2 == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            string str = dictionary2.GetName("Subtype");
            PdfReaderDictionary fontDescriptor = dictionary2.GetDictionary("FontDescriptor");
            if (string.IsNullOrEmpty(str) || (fontDescriptor == null))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            PdfCompositeFontEncoding encoding = PdfCompositeFontEncoding.Create(dictionary.Objects, GetEncodingValue(dictionary));
            if (str == "CIDFontType0")
            {
                return new PdfCIDType0Font(dictionary.Number, baseFont, toUnicode, fontDescriptor, encoding, dictionary2);
            }
            if (str == "CIDFontType2")
            {
                return new PdfCIDType2Font(dictionary.Number, baseFont, toUnicode, fontDescriptor, encoding, dictionary2);
            }
            PdfDocumentStructureReader.ThrowIncorrectDataException();
            return null;
        }

        protected virtual PdfWriterDictionary CreateDescendantDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "Font");
            dictionary.AddName("Subtype", this.CIDSubType);
            dictionary.Add("BaseFont", new PdfName(this.cidBaseFont));
            dictionary.Add("FontDescriptor", this.FontDescriptor);
            dictionary.Add("CIDSystemInfo", this.systemInfo);
            dictionary.Add("DW", this.defaultWidth);
            if ((this.widths != null) && (this.widths.Count > 0))
            {
                List<object> list = new List<object>();
                int item = -1;
                List<object> list2 = new List<object>();
                foreach (KeyValuePair<int, double> pair in this.widths)
                {
                    if (item < 0)
                    {
                        item = pair.Key;
                        list.Add(item);
                        list2.Add(pair.Value);
                        continue;
                    }
                    if (pair.Key == ++item)
                    {
                        list2.Add(pair.Value);
                        continue;
                    }
                    list.Add(list2);
                    item = pair.Key;
                    list.Add(item);
                    new List<object>().Add(pair.Value);
                }
                list.Add(list2);
                dictionary.Add("W", list);
            }
            if (this.cidToGidMap != null)
            {
                int length = this.cidToGidMap.Length;
                byte[] data = new byte[length * 2];
                int index = 0;
                while (true)
                {
                    if (index >= length)
                    {
                        dictionary.Add("CIDToGIDMap", objects.AddStream(data));
                        break;
                    }
                    int num4 = index * 2;
                    short num5 = this.cidToGidMap[index];
                    data[num4] = (byte) (num5 >> 8);
                    data[num4 + 1] = (byte) (num5 % 0x100);
                    index++;
                }
            }
            return dictionary;
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            object[] objArray1 = new object[] { objects.AddDictionary(this.CreateDescendantDictionary(objects)) };
            dictionary.Add("DescendantFonts", objArray1);
            return dictionary;
        }

        protected override PdfFontDescriptor CreateFontDescriptor(PdfReaderDictionary dictionary) => 
            new PdfCompositeFontDescriptor(this, dictionary);

        protected override string GetCharacterUnicodeFallback(byte[] character)
        {
            PdfCIDCharset charset = this.charset.Value;
            return ((charset == null) ? System.Text.Encoding.BigEndianUnicode.GetString(character) : new string((char) ((ushort) charset.GetUnicode(this.encoding.GetCID(character))), 1));
        }

        internal override double GetCharacterWidth(int charCode)
        {
            double defaultWidth = this.defaultWidth;
            if ((defaultWidth == 0.0) && (this.FontDescriptor != null))
            {
                defaultWidth = this.FontDescriptor.AvgWidth;
            }
            double num2 = 0.0;
            if (!this.widths.TryGetValue(charCode, out num2))
            {
                num2 = defaultWidth;
            }
            return num2;
        }

        public PdfCompositeFontEncoding Encoding =>
            this.encoding;

        public string CIDBaseFont =>
            this.cidBaseFont;

        public PdfCIDSystemInfo SystemInfo =>
            this.systemInfo;

        public int DefaultWidth =>
            this.defaultWidth;

        public IDictionary<int, double> Widths
        {
            get => 
                this.widths;
            internal set => 
                this.widths = value;
        }

        public short[] CidToGidMap =>
            this.cidToGidMap;

        protected internal override IEnumerable<double> GlyphWidths =>
            this.Widths.Values;

        protected internal override PdfEncoding ActualEncoding =>
            this.encoding;

        protected internal override string Subtype =>
            "Type0";

        protected abstract string CIDSubType { get; }
    }
}

