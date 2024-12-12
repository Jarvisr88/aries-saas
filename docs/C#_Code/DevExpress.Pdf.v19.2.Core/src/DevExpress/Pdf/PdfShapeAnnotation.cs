namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfShapeAnnotation : PdfMarkupAnnotation
    {
        private const string interiorColorDictionaryKey = "IC";
        private readonly PdfAnnotationBorderStyle borderStyle;
        private readonly PdfColor interiorColor;
        private readonly PdfAnnotationBorderEffect borderEffect;
        private readonly PdfRectangle padding;

        protected PdfShapeAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
            this.borderStyle = PdfAnnotationBorderStyle.Parse(dictionary);
            this.interiorColor = ParseColor(dictionary, "IC");
            this.borderEffect = PdfAnnotationBorderEffect.Parse(dictionary);
            this.padding = dictionary.GetPadding(dictionary.GetRectangle("Rect"));
        }

        protected override PdfWriterDictionary CreateDictionary(PdfObjectCollection collection)
        {
            PdfWriterDictionary dictionary = base.CreateDictionary(collection);
            dictionary.Add("BS", this.borderStyle);
            if (this.interiorColor != null)
            {
                dictionary.Add("IC", this.interiorColor.ToWritableObject());
            }
            if (this.borderEffect != null)
            {
                dictionary.Add("BE", this.borderEffect.ToWritableObject());
            }
            dictionary.Add("RD", this.padding);
            return dictionary;
        }

        public PdfAnnotationBorderStyle BorderStyle =>
            this.borderStyle;

        public PdfColor InteriorColor =>
            this.interiorColor;

        public PdfAnnotationBorderEffect BorderEffect =>
            this.borderEffect;

        public PdfRectangle Padding =>
            this.padding;
    }
}

