namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfCircleAnnotationAppearanceBuilder : PdfShapeAnnotationAppearanceBuilder
    {
        public PdfCircleAnnotationAppearanceBuilder(PdfCircleAnnotation squareAnnotation) : base(squareAnnotation)
        {
        }

        protected override void RebuildShapeAppearance(PdfFormCommandConstructor constructor, PdfRectangle rect)
        {
            constructor.AppendEllipse(rect);
            constructor.ClosePath();
        }
    }
}

