namespace DevExpress.Pdf.Native
{
    using System;
    using System.Globalization;
    using System.Text;

    public class PdfType1FontInfo
    {
        private const string versionDictionaryKey = "version";
        private const string noticeDictionaryKey = "Notice";
        private const string copyrightDictionaryKey = "Copyright";
        private const string fullNameDictionaryKey = "FullName";
        private const string familyNameDictionaryKey = "FamilyName";
        private const string baseFontNameDictionaryKey = "BaseFontName";
        private const string weightDictionaryKey = "Weight";
        private const string italicAngleDictionaryKey = "ItalicAngle";
        private const string isFixedPitchDictionaryKey = "isFixedPitch";
        private const string underlinePositionDictionaryKey = "UnderlinePosition";
        private const string underlineThicknessDictionaryKey = "UnderlineThickness";
        public const double DefaultItalicAngle = 0.0;
        public const double DefaultUnderlinePosition = -100.0;
        public const double DefaultUnderlineThickness = 50.0;
        private string version;
        private string notice;
        private string copyright;
        private string fullName;
        private string familyName;
        private string baseFontName;
        private string weight;
        private double italicAngle;
        private bool isFixedPitch;
        private double underlinePosition;
        private double underlineThickness;

        internal PdfType1FontInfo()
        {
            this.italicAngle = 0.0;
            this.underlinePosition = -100.0;
            this.underlineThickness = 50.0;
        }

        internal PdfType1FontInfo(PdfPostScriptDictionary dictionary)
        {
            Encoding encoding = Encoding.UTF8;
            foreach (PdfPostScriptDictionaryEntry entry in dictionary)
            {
                string key = entry.Key;
                uint num = <PrivateImplementationDetails>.ComputeStringHash(key);
                if (num <= 0x4bdbd6bc)
                {
                    if (num <= 0x193a3366)
                    {
                        if (num == 0xab330d1)
                        {
                            if (key != "Notice")
                            {
                                continue;
                            }
                            this.Notice = ToString(entry.Value);
                            continue;
                        }
                        if (num != 0x193a3366)
                        {
                            continue;
                        }
                        if (key != "BaseFontName")
                        {
                            continue;
                        }
                        this.BaseFontName = ToString(entry.Value);
                        continue;
                    }
                    if (num == 0x1c633342)
                    {
                        if (key != "FamilyName")
                        {
                            continue;
                        }
                        this.FamilyName = ToString(entry.Value);
                        continue;
                    }
                    if (num == 0x4671ae97)
                    {
                        if (key != "version")
                        {
                            continue;
                        }
                        this.Version = ToString(entry.Value);
                        continue;
                    }
                    if (num != 0x4bdbd6bc)
                    {
                        continue;
                    }
                    if (key != "UnderlinePosition")
                    {
                        continue;
                    }
                    this.UnderlinePosition = PdfDocumentReader.ConvertToDouble(entry.Value);
                    continue;
                }
                if (num > 0x986367d0)
                {
                    if (num == 0x993014d9)
                    {
                        if (key != "Weight")
                        {
                            continue;
                        }
                        this.Weight = ToString(entry.Value);
                        continue;
                    }
                    if (num == 0xc45020de)
                    {
                        if (key != "Copyright")
                        {
                            continue;
                        }
                        this.Copyright = ToString(entry.Value);
                        continue;
                    }
                    if (num != 0xe5f4d397)
                    {
                        continue;
                    }
                    if (key != "UnderlineThickness")
                    {
                        continue;
                    }
                    this.UnderlineThickness = PdfDocumentReader.ConvertToDouble(entry.Value);
                    continue;
                }
                if (num == 0x4dd426b5)
                {
                    if (key != "isFixedPitch")
                    {
                        continue;
                    }
                    object obj2 = entry.Value;
                    if (!(obj2 as bool))
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    this.IsFixedPitch = (bool) obj2;
                    continue;
                }
                if (num == 0x84e86f77)
                {
                    if (key != "FullName")
                    {
                        continue;
                    }
                    this.FullName = ToString(entry.Value);
                    continue;
                }
                if ((num == 0x986367d0) && (key == "ItalicAngle"))
                {
                    this.ItalicAngle = PdfDocumentReader.ConvertToDouble(entry.Value);
                }
            }
        }

        internal string Serialize()
        {
            StringBuilder sb = new StringBuilder();
            sb.Append("11 dict dup begin\n");
            SerializeString(sb, "version", this.Version);
            SerializeString(sb, "Notice", this.Notice);
            SerializeString(sb, "Copyright", this.Copyright);
            SerializeString(sb, "FullName", this.FullName);
            SerializeString(sb, "FamilyName", this.FamilyName);
            SerializeString(sb, "BaseFontName", this.BaseFontName);
            SerializeString(sb, "Weight", this.Weight);
            SerializeDouble(sb, "ItalicAngle", this.ItalicAngle);
            sb.Append($"/{"isFixedPitch"} {this.IsFixedPitch ? "true" : "false"} def
");
            SerializeDouble(sb, "UnderlinePosition", this.UnderlinePosition);
            SerializeDouble(sb, "UnderlineThickness", this.UnderlineThickness);
            sb.Append("end");
            return sb.ToString();
        }

        private static void SerializeDouble(StringBuilder sb, string key, double value)
        {
            object[] args = new object[] { key, value };
            sb.Append(string.Format(CultureInfo.InvariantCulture, "/{0} {1} def\n", args));
        }

        private static void SerializeString(StringBuilder sb, string key, string value)
        {
            if (value != null)
            {
                sb.Append($"/{key} ({value}) readonly def
");
            }
        }

        private static string ToString(object value)
        {
            byte[] bytes = value as byte[];
            if (bytes == null)
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            return Encoding.UTF8.GetString(bytes);
        }

        public string Version
        {
            get => 
                this.version;
            internal set => 
                this.version = value;
        }

        public string Notice
        {
            get => 
                this.notice;
            internal set => 
                this.notice = value;
        }

        public string Copyright
        {
            get => 
                this.copyright;
            internal set => 
                this.copyright = value;
        }

        public string FullName
        {
            get => 
                this.fullName;
            internal set => 
                this.fullName = value;
        }

        public string FamilyName
        {
            get => 
                this.familyName;
            internal set => 
                this.familyName = value;
        }

        public string BaseFontName
        {
            get => 
                this.baseFontName;
            internal set => 
                this.baseFontName = value;
        }

        public string Weight
        {
            get => 
                this.weight;
            internal set => 
                this.weight = value;
        }

        public double ItalicAngle
        {
            get => 
                this.italicAngle;
            internal set => 
                this.italicAngle = value;
        }

        public bool IsFixedPitch
        {
            get => 
                this.isFixedPitch;
            internal set => 
                this.isFixedPitch = value;
        }

        public double UnderlinePosition
        {
            get => 
                this.underlinePosition;
            internal set => 
                this.underlinePosition = value;
        }

        public double UnderlineThickness
        {
            get => 
                this.underlineThickness;
            internal set => 
                this.underlineThickness = value;
        }
    }
}

