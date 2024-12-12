namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfPolygonAnnotation : PdfPathAnnotation
    {
        internal const string Type = "Polygon";
        private readonly PdfAnnotationBorderEffect borderEffect;
        private readonly PdfPolygonAnnotationIntent polygonIntent;

        internal PdfPolygonAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            this.borderEffect = PdfAnnotationBorderEffect.Parse(dictionary);
            PdfEnumToStringConverter.TryParse<PdfPolygonAnnotationIntent>(base.Intent, out this.polygonIntent, true);
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            if (this.borderEffect != null)
            {
                dictionary.Add("BE", this.borderEffect.ToWritableObject());
            }
            return dictionary;
        }

        public PdfAnnotationBorderEffect BorderEffect =>
            this.borderEffect;

        public PdfPolygonAnnotationIntent PolygonIntent =>
            this.polygonIntent;

        protected override string AnnotationType =>
            "Polygon";
    }
}

