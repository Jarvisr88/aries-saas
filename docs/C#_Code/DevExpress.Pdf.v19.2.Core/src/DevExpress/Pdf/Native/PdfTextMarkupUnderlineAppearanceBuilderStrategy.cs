namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfTextMarkupUnderlineAppearanceBuilderStrategy : PdfTextMarkupAppearanceBuilderStrategy
    {
        private const double lineWidthFactor = 0.066666666666666666;
        private readonly double heightFactor;

        public PdfTextMarkupUnderlineAppearanceBuilderStrategy(PdfTextMarkupAnnotation annotation, PdfFormCommandConstructor constructor, double heightFactor) : base(annotation, constructor)
        {
            this.heightFactor = heightFactor;
        }

        protected override void BeginRebuildAppearance()
        {
            base.BeginRebuildAppearance();
            PdfCommandConstructor constructor = base.Constructor;
            PdfAnnotation annotation = base.Annotation;
            PdfRectangle rect = annotation.Rect;
            if ((rect.Left != 0.0) || (rect.Bottom != 0.0))
            {
                constructor.ModifyTransformationMatrix(new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, -rect.Left, -rect.Bottom));
            }
            constructor.SetColorForStrokingOperations(annotation.Color);
        }

        protected override void RebuildQuad(PdfQuadrilateral quad)
        {
            PdfCommandConstructor constructor = base.Constructor;
            constructor.SetLineWidth(PdfMathUtils.Min(PdfPoint.Distance(quad.P3, quad.P1), PdfPoint.Distance(quad.P4, quad.P2)) * 0.066666666666666666);
            constructor.DrawLine(PdfPoint.Lerp(quad.P3, quad.P1, this.heightFactor), PdfPoint.Lerp(quad.P4, quad.P2, this.heightFactor));
        }
    }
}

