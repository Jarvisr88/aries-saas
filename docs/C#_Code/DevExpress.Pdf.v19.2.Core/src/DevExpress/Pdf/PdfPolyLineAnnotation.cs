namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPolyLineAnnotation : PdfUnclosedPathAnnotation
    {
        internal const string Type = "PolyLine";
        private readonly PdfPolyLineAnnotationIntent polyLineIntent;

        internal PdfPolyLineAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            this.polyLineIntent = PdfEnumToStringConverter.Parse<PdfPolyLineAnnotationIntent>(base.Intent, true);
        }

        public PdfPolyLineAnnotationIntent PolyLineIntent =>
            this.polyLineIntent;

        protected override string AnnotationType =>
            "PolyLine";
    }
}

