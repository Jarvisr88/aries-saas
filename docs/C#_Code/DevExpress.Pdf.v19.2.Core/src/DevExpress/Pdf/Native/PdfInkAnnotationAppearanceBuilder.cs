namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfInkAnnotationAppearanceBuilder : PdfMarkupAnnotationAppearanceBuilder<PdfInkAnnotation>
    {
        public PdfInkAnnotationAppearanceBuilder(PdfInkAnnotation markupAnnotation) : base(markupAnnotation)
        {
        }

        protected override PdfRectangle GetFormBBox() => 
            base.Annotation.Rect;

        protected override void RebuildAppearance(PdfFormCommandConstructor constructor)
        {
            base.RebuildAppearance(constructor);
            constructor.SetColorForStrokingOperations(base.Annotation.Color);
            if (base.Annotation.BorderStyle != null)
            {
                constructor.SetLineWidth(base.Annotation.BorderStyle.Width);
                constructor.SetLineStyle(base.Annotation.BorderStyle.LineStyle);
            }
            foreach (PdfPoint[] pointArray in base.Annotation.Inks)
            {
                constructor.DrawLines(pointArray);
            }
        }
    }
}

