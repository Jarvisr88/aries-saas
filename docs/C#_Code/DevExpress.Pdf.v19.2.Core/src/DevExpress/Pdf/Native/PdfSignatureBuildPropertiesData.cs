namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PdfSignatureBuildPropertiesData
    {
        private const string operationSystemKey = "OS";
        private readonly string name;
        private readonly string date;
        private readonly double? revision;
        private readonly bool preRelease;
        private readonly IList<string> os;
        private readonly bool nonEmbeddedFontNoWarning;
        private readonly bool trustedMode;
        private readonly double? version;

        public PdfSignatureBuildPropertiesData(PdfReaderDictionary dictionary)
        {
            this.name = dictionary.GetName("Name");
            this.date = dictionary.GetString("Date");
            this.revision = dictionary.GetNumber("R");
            bool? boolean = dictionary.GetBoolean("PreRelease");
            this.preRelease = (boolean != null) ? boolean.GetValueOrDefault() : false;
            PdfSignatureBuildPropertiesData data1 = this;
            if (<>c.<>9__25_0 == null)
            {
                data1 = (PdfSignatureBuildPropertiesData) (<>c.<>9__25_0 = delegate (object value) {
                    PdfName name = value as PdfName;
                    if (name != null)
                    {
                        return name.Name;
                    }
                    byte[] data = value as byte[];
                    if (data == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                    return PdfDocEncoding.GetString(data);
                });
            }
            <>c.<>9__25_0.os = 1.GetArray<string>("OS", (bool) dictionary, (Func<object, string>) data1);
            if (this.os == null)
            {
                string str = dictionary.GetString("OS");
                if (str != null)
                {
                    string[] textArray1 = new string[] { str };
                    this.os = textArray1;
                }
            }
            boolean = dictionary.GetBoolean("NonEFontNoWarn");
            this.nonEmbeddedFontNoWarning = (boolean != null) ? boolean.GetValueOrDefault() : false;
            boolean = dictionary.GetBoolean("TrustedMode");
            this.trustedMode = (boolean != null) ? boolean.GetValueOrDefault() : false;
            this.version = dictionary.GetNumber("V");
        }

        public string Name =>
            this.name;

        public string Date =>
            this.date;

        public double? Revision =>
            this.revision;

        public bool PreRelease =>
            this.preRelease;

        public IList<string> OS =>
            this.os;

        public bool NonEmbeddedFontNoWarning =>
            this.nonEmbeddedFontNoWarning;

        public bool TrustedMode =>
            this.trustedMode;

        public double? Version =>
            this.version;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfSignatureBuildPropertiesData.<>c <>9 = new PdfSignatureBuildPropertiesData.<>c();
            public static Func<object, string> <>9__25_0;

            internal string <.ctor>b__25_0(object value)
            {
                PdfName name = value as PdfName;
                if (name != null)
                {
                    return name.Name;
                }
                byte[] data = value as byte[];
                if (data == null)
                {
                    PdfDocumentStructureReader.ThrowIncorrectDataException();
                }
                return PdfDocEncoding.GetString(data);
            }
        }
    }
}

