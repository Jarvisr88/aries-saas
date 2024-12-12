namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfCustomAnnotation : PdfMarkupAnnotation
    {
        private readonly string subtype;
        private readonly PdfMarkupExternalData exData;

        internal PdfCustomAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            this.subtype = dictionary.GetName("Subtype");
            this.exData = dictionary.GetObject<PdfMarkupExternalData>("ExData", dict => PdfMarkupExternalData.Parse(page, this, dict));
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.Add("ExData", this.exData);
            return dictionary;
        }

        protected override string AnnotationType =>
            this.subtype;
    }
}

