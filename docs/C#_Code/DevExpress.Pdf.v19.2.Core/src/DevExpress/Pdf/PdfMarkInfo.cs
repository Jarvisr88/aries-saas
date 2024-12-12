namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfMarkInfo
    {
        private const string markedDictionaryKey = "Marked";
        private const string userPropertiesDictionaryKey = "UserProperties";
        private const string suspectsDictionaryKey = "Suspects";
        private readonly bool isTagged;
        private readonly bool containsUserProperties;
        private readonly bool containsTagSuspects;

        internal PdfMarkInfo(PdfReaderDictionary dictionary)
        {
            object obj2;
            if (dictionary.TryGetValue("Marked", out obj2))
            {
                obj2 = dictionary.Objects.TryResolve(obj2, null);
                if (obj2 != null)
                {
                    if (obj2 as bool)
                    {
                        this.isTagged = (bool) obj2;
                    }
                    else
                    {
                        PdfName name = obj2 as PdfName;
                        if (name == null)
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        string str = name.Name;
                        if (str == "true")
                        {
                            this.isTagged = true;
                        }
                        else if (str != "false")
                        {
                            PdfDocumentStructureReader.ThrowIncorrectDataException();
                        }
                        else
                        {
                            this.isTagged = false;
                        }
                    }
                }
            }
            bool? boolean = dictionary.GetBoolean("UserProperties");
            this.containsUserProperties = (boolean != null) ? boolean.GetValueOrDefault() : false;
            boolean = dictionary.GetBoolean("Suspects");
            this.containsTagSuspects = (boolean != null) ? boolean.GetValueOrDefault() : false;
        }

        internal PdfMarkInfo(bool isTagged)
        {
            this.isTagged = isTagged;
        }

        internal PdfWriterDictionary Write()
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(null);
            dictionary.Add("Marked", this.isTagged, false);
            dictionary.Add("UserProperties", this.containsUserProperties, false);
            dictionary.Add("Suspects", this.containsTagSuspects, false);
            return dictionary;
        }

        public bool IsTagged =>
            this.isTagged;

        public bool ContainsUserProperties =>
            this.containsUserProperties;

        public bool ContainsTagSuspects =>
            this.containsTagSuspects;
    }
}

