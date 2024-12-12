namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PdfFontDescriptor : PdfObject
    {
        internal const int FontWeightNormal = 400;
        internal const string FontDescriptorDictionaryType = "FontDescriptor";
        internal const string FlagsDictionaryKey = "Flags";
        internal const string ItalicAngleDictionaryKey = "ItalicAngle";
        internal const string AscentDictionaryKey = "Ascent";
        internal const string DescentDictionaryKey = "Descent";
        internal const string FontBBoxDictionaryKey = "FontBBox";
        internal const string FontWeightDictionaryKey = "FontWeight";
        internal const string CapHeightDictionaryKey = "CapHeight";
        internal const string StemVDictionaryKey = "StemV";
        private const string fontNameDictionaryKey = "FontName";
        private const string fontFamilyDictionaryKey = "FontFamily";
        private const string fontStretchDictionaryKey = "FontStretch";
        private const string leadingDictionaryKey = "Leading";
        private const string xHeightDictionaryKey = "XHeight";
        private const string stemHDictionaryKey = "StemH";
        private const string avgWidthDictionaryKey = "AvgWidth";
        private const string maxWidthDictionaryKey = "MaxWidth";
        private const string missingWidthDictionaryKey = "MissingWidth";
        private const string charSetDictionaryKey = "CharSet";
        private const string cidSetKey = "CIDSet";
        private const string fontWeightRegular = "Regular";
        private readonly PdfFontStretch fontStretch;
        private readonly int fontWeight;
        private readonly PdfFontFlags flags;
        private readonly PdfRectangle fontBBox;
        private readonly double italicAngle;
        private readonly double ascent;
        private readonly double descent;
        private readonly double leading;
        private readonly double capHeight;
        private readonly double xHeight;
        private readonly double stemV;
        private readonly double stemH;
        private readonly double avgWidth;
        private readonly double maxWidth;
        private readonly double missingWidth;
        private readonly IList<string> charSet;
        private readonly byte[] cidSetData;
        private PdfFont font;
        private string fontName;
        private string fontFamily;
        private IDictionary<short, short> cidMapping;

        internal unsafe PdfFontDescriptor(IPdfFontDescriptorBuilder descriptorBuilder)
        {
            this.fontWeight = 400;
            this.fontFamily = descriptorBuilder.FontFamily;
            this.flags = descriptorBuilder.Flags;
            this.italicAngle = descriptorBuilder.ItalicAngle;
            this.fontWeight = descriptorBuilder.Bold ? 700 : 400;
            this.capHeight = descriptorBuilder.CapHeight;
            this.fontStretch = PdfFontStretch.Normal;
            this.ascent = Math.Round(descriptorBuilder.Ascent);
            this.descent = Math.Round(descriptorBuilder.Descent);
            this.xHeight = descriptorBuilder.XHeight;
            this.stemV = descriptorBuilder.StemV;
            this.stemH = descriptorBuilder.StemH;
            PdfRectangle bBox = descriptorBuilder.BBox;
            this.fontBBox = new PdfRectangle(Math.Round(bBox.Left), Math.Round(bBox.Bottom), Math.Round(bBox.Right), Math.Round(bBox.Top));
            int numGlyphs = descriptorBuilder.NumGlyphs;
            int num2 = (int) Math.Ceiling((double) (((double) numGlyphs) / 8.0));
            this.cidSetData = new byte[num2];
            int num3 = numGlyphs % 8;
            int num4 = num2 - ((num3 > 0) ? 1 : 0);
            for (int i = 0; i < num4; i++)
            {
                this.cidSetData[i] = 0xff;
            }
            int index = num2 - 1;
            byte num6 = 0x80;
            for (int j = 0; j < num3; j++)
            {
                byte* numPtr1 = &(this.cidSetData[index]);
                numPtr1[0] = (byte) (numPtr1[0] | num6);
                num6 = (byte) (num6 >> 1);
            }
            this.UnpackCIDMapping();
        }

        internal PdfFontDescriptor(PdfFont font, PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            object obj2;
            PdfName name;
            this.fontWeight = 400;
            this.font = font;
            string fontName = PdfFont.GetFontName(dictionary, "FontName");
            string baseFont = fontName;
            if (fontName == null)
            {
                string local1 = fontName;
                baseFont = font.BaseFont;
            }
            this.fontName = baseFont;
            PdfObjectCollection objects = dictionary.Objects;
            if (dictionary.TryGetValue("FontFamily", out obj2))
            {
                obj2 = objects.TryResolve(obj2, null);
                if (obj2 != null)
                {
                    byte[] buffer = obj2 as byte[];
                    if (buffer != null)
                    {
                        this.fontFamily = PdfDocumentReader.ConvertToString(buffer);
                    }
                    else
                    {
                        name = obj2 as PdfName;
                        if (name == null)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        this.fontFamily = name.Name;
                    }
                }
            }
            this.fontStretch = PdfEnumToStringConverter.Parse<PdfFontStretch>(dictionary.GetName("FontStretch"), true);
            int? integer = dictionary.GetInteger("Flags");
            this.flags = (integer != null) ? ((PdfFontFlags) integer.Value) : PdfFontFlags.None;
            if (!dictionary.TryGetValue("FontWeight", out obj2))
            {
                obj2 = 400;
            }
            else
            {
                obj2 = objects.TryResolve(obj2, null);
                if (obj2 != null)
                {
                    name = obj2 as PdfName;
                    if (name != null)
                    {
                        string str = name.Name;
                        if (str == "Italic")
                        {
                            this.flags |= PdfFontFlags.Italic;
                        }
                        else if (str == "Bold")
                        {
                            this.fontWeight = 700;
                            goto TR_0011;
                        }
                        this.fontWeight = 400;
                    }
                    else
                    {
                        this.fontWeight = PdfDocumentReader.ConvertToInteger(obj2);
                        if (this.fontWeight == 0)
                        {
                            this.fontWeight = 400;
                        }
                        else if (this.fontWeight < 0)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                    }
                }
            }
        TR_0011:
            this.fontBBox = dictionary.GetRectangle("FontBBox");
            if ((this.fontBBox == null) && font.HasSizeAttributes)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            double? number = dictionary.GetNumber("ItalicAngle");
            this.italicAngle = (number != null) ? number.GetValueOrDefault() : 0.0;
            number = dictionary.GetNumber("Ascent");
            this.ascent = (number != null) ? number.GetValueOrDefault() : 0.0;
            if (this.ascent < 0.0)
            {
                this.ascent = -this.ascent;
            }
            number = dictionary.GetNumber("Descent");
            this.descent = (number != null) ? number.GetValueOrDefault() : 0.0;
            if (this.descent > 0.0)
            {
                this.descent = -this.descent;
            }
            number = dictionary.GetNumber("Leading");
            this.leading = (number != null) ? number.GetValueOrDefault() : 0.0;
            number = dictionary.GetNumber("CapHeight");
            this.capHeight = (number != null) ? number.GetValueOrDefault() : 0.0;
            number = dictionary.GetNumber("XHeight");
            this.xHeight = (number != null) ? number.GetValueOrDefault() : 0.0;
            number = dictionary.GetNumber("StemV");
            this.stemV = (number != null) ? number.GetValueOrDefault() : 0.0;
            number = dictionary.GetNumber("StemH");
            this.stemH = (number != null) ? number.GetValueOrDefault() : 0.0;
            number = dictionary.GetNumber("AvgWidth");
            this.avgWidth = (number != null) ? number.GetValueOrDefault() : 0.0;
            number = dictionary.GetNumber("MaxWidth");
            this.maxWidth = (number != null) ? number.GetValueOrDefault() : 0.0;
            number = dictionary.GetNumber("MissingWidth");
            this.missingWidth = (number != null) ? number.GetValueOrDefault() : 0.0;
            byte[] bytes = dictionary.GetBytes("CharSet");
            if (bytes != null)
            {
                try
                {
                    this.charSet = PdfDocumentReader.IsUnicode(bytes) ? PdfCharSetStringParser.Parse(PdfDocumentReader.ConvertToUnicodeString(bytes)) : ((IList<string>) PdfObjectParser.ParseNameArray(bytes));
                }
                catch
                {
                }
            }
            if (dictionary.TryGetValue("CIDSet", out obj2))
            {
                PdfReaderStream stream = objects.TryResolve(obj2, null) as PdfReaderStream;
                if (stream != null)
                {
                    try
                    {
                        this.cidSetData = stream.UncompressedData;
                        this.UnpackCIDMapping();
                    }
                    catch
                    {
                    }
                }
            }
        }

        protected virtual PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("Type", "FontDescriptor");
            dictionary.AddName("FontName", this.fontName);
            dictionary.Add("FontFamily", this.fontFamily, null);
            dictionary.AddEnumName<PdfFontStretch>("FontStretch", this.fontStretch);
            dictionary.Add("FontWeight", this.fontWeight, 400);
            dictionary.Add("Flags", (int) this.flags);
            dictionary.Add("ItalicAngle", this.italicAngle);
            dictionary.Add("Leading", this.leading, 0.0);
            dictionary.Add("XHeight", this.xHeight, 0.0);
            dictionary.Add("StemH", this.stemH, 0.0);
            dictionary.Add("AvgWidth", this.avgWidth, 0.0);
            dictionary.Add("MaxWidth", this.maxWidth, 0.0);
            dictionary.Add("MissingWidth", this.missingWidth, 0.0);
            if (this.font.HasSizeAttributes)
            {
                dictionary.Add("FontBBox", this.fontBBox);
                dictionary.Add("Ascent", this.ascent);
                dictionary.Add("Descent", this.descent);
                dictionary.Add("CapHeight", this.capHeight);
                dictionary.Add("StemV", this.stemV);
            }
            if (this.charSet != null)
            {
                bool flag = false;
                StringBuilder builder = new StringBuilder();
                foreach (string str2 in this.charSet)
                {
                    builder.Append("/");
                    builder.Append(str2);
                    if (!flag)
                    {
                        foreach (char ch in str2)
                        {
                            if (ch >= '\x00fe')
                            {
                                flag = true;
                                break;
                            }
                        }
                    }
                }
                string str = builder.ToString();
                if (flag)
                {
                    dictionary.Add("CharSet", "(" + str + ")");
                }
                else
                {
                    dictionary.AddASCIIString("CharSet", str);
                }
            }
            if (this.cidSetData != null)
            {
                dictionary.Add("CIDSet", objects.AddStream(this.cidSetData));
            }
            this.font.UpdateFontDescriptorDictionary(dictionary);
            return dictionary;
        }

        internal void SetFont(PdfFont font)
        {
            this.font = font;
            if (string.IsNullOrEmpty(this.fontName))
            {
                this.fontName = font.BaseFont;
            }
        }

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            this.CreateDictionary(objects);

        private void UnpackCIDMapping()
        {
            if (this.cidSetData != null)
            {
                this.cidMapping = new Dictionary<short, short>();
                short num = 0;
                short num2 = 0;
                byte[] cidSetData = this.cidSetData;
                int index = 0;
                while (index < cidSetData.Length)
                {
                    byte num5 = cidSetData[index];
                    int num6 = 0;
                    while (true)
                    {
                        if (num6 >= 8)
                        {
                            index++;
                            break;
                        }
                        if ((num5 & 0x80) != 0)
                        {
                            short num1 = (short) (num + 1);
                            this.cidMapping[num2] = num = num1;
                        }
                        num6++;
                        num2 = (short) (num2 + 1);
                        num5 = (byte) (num5 << 1);
                    }
                }
            }
        }

        public PdfFontStretch FontStretch =>
            this.fontStretch;

        public int FontWeight =>
            this.fontWeight;

        public PdfFontFlags Flags =>
            this.flags;

        public PdfRectangle FontBBox =>
            this.fontBBox;

        public double ItalicAngle =>
            this.italicAngle;

        public double Ascent =>
            this.ascent;

        public double Descent =>
            this.descent;

        public double Leading =>
            this.leading;

        public double CapHeight =>
            this.capHeight;

        public double XHeight =>
            this.xHeight;

        public double StemV =>
            this.stemV;

        public double StemH =>
            this.stemH;

        public double AvgWidth =>
            this.avgWidth;

        public double MaxWidth =>
            this.maxWidth;

        public double MissingWidth =>
            this.missingWidth;

        public IList<string> CharSet =>
            this.charSet;

        public IDictionary<short, short> CIDMapping =>
            this.cidMapping;

        public string FontName =>
            this.fontName;

        public string FontFamily
        {
            get => 
                this.fontFamily;
            internal set => 
                this.fontFamily = value;
        }
    }
}

