namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public abstract class PdfAnnotationAppearanceBuilder<T> : IPdfAnnotationAppearanceBuilder where T: PdfAnnotation
    {
        private readonly T annotation;

        protected PdfAnnotationAppearanceBuilder(T annotation)
        {
            this.annotation = annotation;
        }

        protected virtual PdfTransformationMatrix CreateFormMatrix() => 
            new PdfTransformationMatrix();

        protected virtual PdfRectangle GetFormBBox() => 
            this.annotation.GetAppearanceFormBoundingBox();

        protected abstract void RebuildAppearance(PdfFormCommandConstructor constructor);
        public void RebuildAppearance(PdfForm form)
        {
            using (PdfFormCommandConstructor constructor = new PdfFormCommandConstructor(form))
            {
                form.BBox = this.GetFormBBox();
                form.Matrix = this.CreateFormMatrix();
                this.RebuildAppearance(constructor);
                form.ReplaceCommands(constructor.Commands);
            }
        }

        protected T Annotation =>
            this.annotation;
    }
}

