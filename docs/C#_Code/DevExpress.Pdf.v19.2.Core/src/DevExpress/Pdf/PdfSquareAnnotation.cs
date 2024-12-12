namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfSquareAnnotation : PdfShapeAnnotation
    {
        internal const string Type = "Square";

        internal PdfSquareAnnotation(PdfPage page, PdfReaderDictionary dictionary) : base(page, dictionary)
        {
        }

        protected internal override IPdfAnnotationAppearanceBuilder CreateAppearanceBuilder(IPdfExportFontProvider fontSearch) => 
            new PdfSquareAnnotationAppearanceBuilder(this);

        protected override string AnnotationType =>
            "Square";
    }
}

