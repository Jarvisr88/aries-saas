namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;
    using System.Text;

    public class PdfUriAction : PdfAction
    {
        internal const string Name = "URI";
        private const string uriDictionaryKey = "URI";
        private const string isMapDictionaryKey = "IsMap";
        private readonly string uri;
        private readonly bool isMap;

        internal PdfUriAction(PdfReaderDictionary dictionary) : base(dictionary)
        {
            byte[] bytes = dictionary.GetBytes("URI");
            this.uri = (bytes == null) ? string.Empty : Encoding.UTF8.GetString(bytes, 0, bytes.Length);
            bool? boolean = dictionary.GetBoolean("IsMap");
            this.isMap = (boolean != null) ? boolean.GetValueOrDefault() : false;
        }

        internal PdfUriAction(PdfDocumentCatalog documentCatalog, string uri) : base(documentCatalog)
        {
            this.uri = uri;
        }

        internal PdfUriAction(PdfDocumentCatalog documentCatalog, System.Uri uri) : this(documentCatalog, uri.IsAbsoluteUri ? uri.AbsoluteUri : uri.OriginalString)
        {
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection objects)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(objects);
            dictionary.Add("URI", Encoding.UTF8.GetBytes(this.uri));
            dictionary.Add("IsMap", this.isMap, false);
            return dictionary;
        }

        protected internal override void Execute(IPdfInteractiveOperationController interactiveOperationController, IList<PdfPage> pages)
        {
            interactiveOperationController.OpenUri(this.uri);
        }

        public string Uri =>
            this.uri;

        public bool IsMap =>
            this.isMap;

        protected override string ActionType =>
            "URI";
    }
}

