namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;

    public abstract class PdfTextMarkupAppearanceBuilderStrategy : IDisposable
    {
        private const double underlineFactor = 0.1388888888888889;
        private const double strikeoutFactor = 0.43055555555555558;
        private readonly PdfTextMarkupAnnotation annotation;
        private readonly PdfFormCommandConstructor constructor;

        protected PdfTextMarkupAppearanceBuilderStrategy(PdfTextMarkupAnnotation annotation, PdfFormCommandConstructor constructor)
        {
            this.annotation = annotation;
            this.constructor = constructor;
        }

        protected virtual void BeginRebuildAppearance()
        {
            this.constructor.SaveGraphicsState();
        }

        public static PdfTextMarkupAppearanceBuilderStrategy Create(PdfTextMarkupAnnotation annotation, PdfFormCommandConstructor constructor)
        {
            switch (annotation.Type)
            {
                case PdfTextMarkupAnnotationType.Underline:
                    return new PdfTextMarkupUnderlineAppearanceBuilderStrategy(annotation, constructor, 0.1388888888888889);

                case PdfTextMarkupAnnotationType.Squiggly:
                    return new PdfTextMarkupSquigglyAppearanceBuilderStrategy(annotation, constructor);

                case PdfTextMarkupAnnotationType.StrikeOut:
                    return new PdfTextMarkupUnderlineAppearanceBuilderStrategy(annotation, constructor, 0.43055555555555558);
            }
            return new PdfTextMarkupHighlightAppearanceBuilderStrategy(annotation, constructor);
        }

        public virtual void Dispose()
        {
        }

        protected virtual void EndRebuildAppearance()
        {
            this.constructor.RestoreGraphicsState();
        }

        public void RebuildAppearance()
        {
            if (this.annotation.Quads != null)
            {
                this.BeginRebuildAppearance();
                foreach (PdfQuadrilateral quadrilateral in this.annotation.Quads)
                {
                    this.RebuildQuad(quadrilateral);
                }
                this.EndRebuildAppearance();
            }
        }

        protected abstract void RebuildQuad(PdfQuadrilateral quad);

        protected PdfFormCommandConstructor Constructor =>
            this.constructor;

        protected PdfTextMarkupAnnotation Annotation =>
            this.annotation;
    }
}

