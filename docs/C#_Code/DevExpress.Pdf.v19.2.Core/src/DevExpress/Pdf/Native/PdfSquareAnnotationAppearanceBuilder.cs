namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfSquareAnnotationAppearanceBuilder : PdfShapeAnnotationAppearanceBuilder
    {
        public PdfSquareAnnotationAppearanceBuilder(PdfSquareAnnotation squareAnnotation) : base(squareAnnotation)
        {
        }

        protected override void RebuildShapeAppearance(PdfFormCommandConstructor constructor, PdfRectangle rect)
        {
            constructor.AppendRectangle(rect);
        }
    }
}

