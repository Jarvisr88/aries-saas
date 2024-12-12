namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfOptionalContentUsageCreatorInfo : PdfObject
    {
        private const string creatorDictionaryKey = "Creator";
        private const string incorrectSubtypeDictionaryKey = "SubType";
        private readonly string creator;
        private readonly string contentType;
        private readonly Dictionary<string, object> customProperties;

        internal PdfOptionalContentUsageCreatorInfo(PdfReaderDictionary dictionary) : base(dictionary.Number)
        {
            this.customProperties = new Dictionary<string, object>();
            this.creator = dictionary.GetTextString("Creator");
            if (string.IsNullOrEmpty(this.creator))
            {
                PdfDocumentStructureReader.ThrowIncorrectDataException();
            }
            string contentType = GetContentType(dictionary, "Subtype");
            string text2 = contentType;
            if (contentType == null)
            {
                string local1 = contentType;
                text2 = GetContentType(dictionary, "SubType");
            }
            this.contentType = text2;
            PdfObjectCollection collection = dictionary.Objects;
            foreach (KeyValuePair<string, object> pair in dictionary)
            {
                string key = pair.Key;
                if ((key != "Creator") && ((key != "Subtype") && (key != "SubType")))
                {
                    this.customProperties.Add(key, PdfPrivateData.TryResolve(null, collection, pair.Value));
                }
            }
        }

        private static string GetContentType(PdfReaderDictionary dictionary, string key)
        {
            object obj2;
            if (!dictionary.TryGetValue(key, out obj2) || (obj2 == null))
            {
                return null;
            }
            obj2 = dictionary.Objects.TryResolve(obj2, null);
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

        protected internal override object ToWritableObject(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(objects);
            dictionary.Add("Creator", this.creator);
            dictionary.AddName("Subtype", this.contentType);
            foreach (KeyValuePair<string, object> pair in this.customProperties)
            {
                object obj2 = pair.Value;
                PdfPrivateData data = obj2 as PdfPrivateData;
                dictionary.Add(pair.Key, (data == null) ? obj2 : objects.AddObject((PdfObject) data));
            }
            return dictionary;
        }

        public string Creator =>
            this.creator;

        public string ContentType =>
            this.contentType;

        public IDictionary<string, object> CustomProperties =>
            this.customProperties;
    }
}

