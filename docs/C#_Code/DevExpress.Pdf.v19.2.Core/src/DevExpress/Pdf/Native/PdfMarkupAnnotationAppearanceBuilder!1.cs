namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfMarkupAnnotationAppearanceBuilder<T> : PdfAnnotationAppearanceBuilder<T> where T: PdfMarkupAnnotation
    {
        protected PdfMarkupAnnotationAppearanceBuilder(T markupAnnotation) : base(markupAnnotation)
        {
        }

        protected override void RebuildAppearance(PdfFormCommandConstructor constructor)
        {
            double opacity = base.Annotation.Opacity;
            if (opacity != 1.0)
            {
                PdfGraphicsStateParameters parameters = new PdfGraphicsStateParameters {
                    NonStrokingAlphaConstant = new double?(opacity),
                    StrokingAlphaConstant = new double?(opacity)
                };
                constructor.SetGraphicsStateParameters(parameters);
            }
        }
    }
}

