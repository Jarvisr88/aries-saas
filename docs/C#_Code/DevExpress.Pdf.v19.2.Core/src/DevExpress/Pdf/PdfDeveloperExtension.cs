namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfDeveloperExtension
    {
        private const string baseVersionKey = "BaseVersion";
        private const string extensionLevelKey = "ExtensionLevel";
        private readonly PdfFileVersion baseVersion;
        private readonly int extensionLevel;

        private PdfDeveloperExtension(PdfFileVersion baseVersion, int extensionLevel)
        {
            this.baseVersion = baseVersion;
            this.extensionLevel = extensionLevel;
        }

        private static PdfDeveloperExtension Create(PdfReaderDictionary dictionary)
        {
            string name = dictionary.GetName("BaseVersion");
            int? integer = dictionary.GetInteger("ExtensionLevel");
            return ((string.IsNullOrEmpty(name) || (integer == null)) ? null : new PdfDeveloperExtension(PdfDocumentReader.FindVersion(name), integer.Value));
        }

        internal static Dictionary<string, PdfDeveloperExtension> Parse(PdfReaderDictionary dictionary)
        {
            if (dictionary == null)
            {
                return null;
            }
            Dictionary<string, PdfDeveloperExtension> dictionary2 = new Dictionary<string, PdfDeveloperExtension>();
            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                string key = pair.Key;
                if (key != "Type")
                {
                    PdfReaderDictionary dictionary3 = dictionary.GetDictionary(pair.Key);
                    if (dictionary3 != null)
                    {
                        PdfDeveloperExtension extension = Create(dictionary3);
                        if (extension != null)
                        {
                            dictionary2.Add(key, extension);
                        }
                    }
                }
            }
            return dictionary2;
        }

        internal PdfDictionary Write(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.AddName("BaseVersion", "1.7");
            dictionary.Add("ExtensionLevel", this.extensionLevel);
            return dictionary;
        }

        public PdfFileVersion BaseVersion =>
            this.baseVersion;

        public int ExtensionLevel =>
            this.extensionLevel;
    }
}

