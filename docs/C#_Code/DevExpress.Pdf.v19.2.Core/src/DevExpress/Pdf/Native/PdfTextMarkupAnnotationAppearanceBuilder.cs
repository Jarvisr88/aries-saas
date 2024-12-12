namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public class PdfTextMarkupAnnotationAppearanceBuilder : PdfMarkupAnnotationAppearanceBuilder<PdfTextMarkupAnnotation>
    {
        public PdfTextMarkupAnnotationAppearanceBuilder(PdfTextMarkupAnnotation annotation) : base(annotation)
        {
        }

        protected override void RebuildAppearance(PdfFormCommandConstructor constructor)
        {
            base.RebuildAppearance(constructor);
            using (PdfTextMarkupAppearanceBuilderStrategy strategy = PdfTextMarkupAppearanceBuilderStrategy.Create(base.Annotation, constructor))
            {
                strategy.RebuildAppearance();
            }
        }
    }
}

