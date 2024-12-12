namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfLinearGradientPatternConstructor
    {
        private readonly DXLinearGradientBrush brush;
        private readonly PdfTransformationMatrix actualBrushTransform;

        protected PdfLinearGradientPatternConstructor(DXLinearGradientBrush brush, PdfTransformationMatrix actualBrushTransform)
        {
            this.brush = brush;
            this.actualBrushTransform = actualBrushTransform;
        }

        public static PdfLinearGradientPatternConstructor Create(DXLinearGradientBrush brush, PdfRectangle bBox, PdfTransformationMatrix actualBrushTransform) => 
            (brush.InterpolationColors != null) ? ((PdfLinearGradientPatternConstructor) new PdfMultiColorLinearGradientPatternConstructor(brush, actualBrushTransform)) : ((PdfLinearGradientPatternConstructor) new PdfTwoColorLinearGradientPatternConstructor(brush, actualBrushTransform));

        protected abstract PdfLinearGradientShadingBuilder CreateBuilder();
        protected abstract PdfLinearGradientShadingBuilder CreateMaskBuilder();
        public virtual PdfPattern CreatePattern(PdfGraphicsCommandConstructor commandConstructor)
        {
            PdfDocumentCatalog documentCatalog = commandConstructor.DocumentCatalog;
            PdfRectangle gradientRect = PdfGraphicsConverter.ConvertRectangle(this.brush.Rectangle);
            PdfAxialShading shading = this.CreateBuilder().CreateShading(documentCatalog, gradientRect);
            PdfLinearGradientShadingBuilder builder = this.CreateMaskBuilder();
            return PdfBrushPatternConstructor.CreatePattern(this.brush.WrapMode, this.actualBrushTransform, gradientRect, shading, builder?.CreateShading(documentCatalog, gradientRect), documentCatalog, new PdfTransformationMatrix());
        }

        protected DXLinearGradientBrush Brush =>
            this.brush;

        protected PdfTransformationMatrix ActualBrushTransform =>
            this.actualBrushTransform;

        public abstract byte Alpha { get; }
    }
}

