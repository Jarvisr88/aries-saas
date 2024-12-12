namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfCircleAnnotation : PdfShapeAnnotation
    {
        internal const string Type = "Circle";

        internal PdfCircleAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
        }

        protected internal override IPdfAnnotationAppearanceBuilder CreateAppearanceBuilder(IPdfExportFontProvider fontSearch) => 
            new PdfCircleAnnotationAppearanceBuilder(this);

        protected override string AnnotationType =>
            "Circle";
    }
}

