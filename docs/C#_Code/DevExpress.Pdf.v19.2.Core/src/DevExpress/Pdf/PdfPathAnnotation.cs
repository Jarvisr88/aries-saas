namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public abstract class PdfPathAnnotation : PdfMarkupAnnotation
    {
        private const string interiorColorDictionaryKey = "IC";
        private const string measureDictionaryKey = "Measure";
        private readonly PdfPoint[] vertices;
        private readonly PdfAnnotationBorderStyle borderStyle;
        private readonly PdfColor interiorColor;
        private readonly PdfRectilinearMeasure measure;

        protected PdfPathAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            this.vertices = PdfDocumentReader.CreatePointArray(dictionary.GetArray(this.VerticesDictionaryKey));
            this.borderStyle = PdfAnnotationBorderStyle.Parse(dictionary);
            this.interiorColor = ParseColor(dictionary, "IC");
            PdfReaderDictionary dictionary2 = dictionary.GetDictionary("Measure");
            if (dictionary2 != null)
            {
                this.measure = new PdfRectilinearMeasure(dictionary2);
            }
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            if (this.vertices != null)
            {
                dictionary.Add(this.VerticesDictionaryKey, new PdfWritablePointsArray(this.vertices));
            }
            dictionary.Add("BS", this.borderStyle);
            if (this.interiorColor != null)
            {
                dictionary.Add("IC", this.interiorColor.ToWritableObject());
            }
            dictionary.Add("Measure", this.measure);
            return dictionary;
        }

        public IList<PdfPoint> Vertices =>
            this.vertices;

        public PdfAnnotationBorderStyle BorderStyle =>
            this.borderStyle;

        public PdfColor InteriorColor =>
            this.interiorColor;

        public PdfRectilinearMeasure Measure =>
            this.measure;

        protected virtual string VerticesDictionaryKey =>
            "Vertices";
    }
}

