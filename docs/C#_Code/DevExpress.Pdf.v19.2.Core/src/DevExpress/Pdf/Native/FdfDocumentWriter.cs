namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;
    using System.IO;

    internal class FdfDocumentWriter : PdfObjectWriter
    {
        private const string fdfVersion_1_2 = "1.2";
        private const string fdfDictionaryKey = "FDF";
        private const string versionDictionaryKey = "Version";
        private const string kidsDictionaryKey = "Kids";
        private const string fieldsDictionaryKey = "Fields";
        private readonly PdfObjectCollection objects;
        private readonly PdfFormData formData;

        private FdfDocumentWriter(Stream stream, PdfFormData formData) : base(stream)
        {
            this.objects = new PdfObjectCollection(base.Stream);
            this.formData = formData;
        }

        private static PdfWriterDictionary CreateDictionary(PdfFormData formData, PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            if (formData.Name != null)
            {
                dictionary.AddASCIIString("T", formData.Name);
            }
            IDictionary<string, PdfFormData> kids = formData.Kids;
            if (kids == null)
            {
                if (formData.Value != null)
                {
                    dictionary.Add("V", formData.Value);
                }
            }
            else
            {
                IList<object> enumerable = new List<object>();
                foreach (KeyValuePair<string, PdfFormData> pair in kids)
                {
                    PdfFormData data = pair.Value;
                    if (!data.IsPasswordFormField)
                    {
                        enumerable.Add(CreateDictionary(data, collection));
                    }
                }
                dictionary.Add("Kids", new PdfWritableArray(enumerable));
            }
            return dictionary;
        }

        private static PdfWriterDictionary CreateRootDictionary(PdfFormData formData, PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = new PdfWriterDictionary(collection);
            IDictionary<string, PdfFormData> kids = formData.Kids;
            if (kids != null)
            {
                IList<object> enumerable = new List<object>();
                foreach (KeyValuePair<string, PdfFormData> pair in kids)
                {
                    PdfFormData data = pair.Value;
                    if (!data.IsPasswordFormField)
                    {
                        enumerable.Add(CreateDictionary(data, collection));
                    }
                }
                dictionary.Add("Fields", new PdfWritableArray(enumerable));
            }
            return dictionary;
        }

        private void Write()
        {
            base.Stream.WriteString("%FDF-1.2\r\n");
            PdfWriterDictionary dictionary = new PdfWriterDictionary(this.objects);
            dictionary.Add("FDF", CreateRootDictionary(this.formData, this.objects));
            dictionary.AddName("Version", "1.2");
            PdfObjectReference reference = this.objects.AddDictionary(dictionary);
            using (IEnumerator<PdfObjectContainer> enumerator = this.objects.EnumeratorOfContainers)
            {
                while (enumerator.MoveNext())
                {
                    this.WriteIndirectObject(enumerator.Current);
                }
            }
            base.Stream.WriteString("trailer\r\n");
            PdfWriterDictionary dictionary2 = new PdfWriterDictionary(null);
            dictionary2.Add("Root", reference);
            base.Stream.WriteObject(dictionary2, -1);
            base.WriteEndOfDocument();
        }

        public static void Write(Stream stream, PdfFormData formData)
        {
            BufferedStream stream2 = new BufferedStream(stream);
            new FdfDocumentWriter(stream2, formData).Write();
            stream2.Flush();
        }
    }
}

