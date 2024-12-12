namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Text;

    public class PdfJavaScriptAction : PdfAction
    {
        internal const string Name = "JavaScript";
        private const string jsDictionaryKey = "JS";
        private readonly string javaScript;
        private readonly bool storeAsStream;

        internal PdfJavaScriptAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
            object obj2;
            if (!dictionary.TryGetValue("JS", out obj2))
            {
                this.javaScript = string.Empty;
            }
            else
            {
                byte[] uncompressedData;
                obj2 = dictionary.Objects.TryResolve(obj2, null);
                PdfReaderStream stream = obj2 as PdfReaderStream;
                if (stream != null)
                {
                    uncompressedData = stream.UncompressedData;
                    this.storeAsStream = true;
                }
                else
                {
                    uncompressedData = obj2 as byte[];
                    if (uncompressedData == null)
                    {
                        PdfDocumentStructureReader.ThrowIncorrectDataException();
                    }
                }
                this.javaScript = PdfDocumentReader.ConvertToString(uncompressedData);
            }
        }

        internal PdfJavaScriptAction(string javaScript, PdfDocumentCatalog documentCatalog) : base(documentCatalog)
        {
            this.javaScript = javaScript;
        }

        public PdfJavaScriptAction(string javaScript, PdfDocument document) : base(document)
        {
            this.javaScript = javaScript;
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            if (!this.storeAsStream)
            {
                dictionary.AddASCIIString("JS", this.javaScript);
            }
            else
            {
                int length = this.javaScript.Length;
                byte[] bytes = new byte[] { 0xfe, 0xff };
                Encoding.BigEndianUnicode.GetBytes(this.javaScript, 0, length, bytes, 2);
                dictionary.Add("JS", objects.AddStream(bytes));
            }
            return dictionary;
        }

        public string JavaScript =>
            this.javaScript;

        protected override string ActionType =>
            "JavaScript";
    }
}

