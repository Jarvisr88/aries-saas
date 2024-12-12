namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfBrushTilingPatternConstructor : PdfBrushPatternConstructor
    {
        private readonly PdfRectangle tileBounds;
        private readonly PdfRectangle patternBounds;
        private readonly PdfShading maskShading;

        protected PdfBrushTilingPatternConstructor(PdfRectangle tileBounds, PdfShading shading, PdfShading maskShading, PdfTransformationMatrix transform, PdfRectangle patternBounds, PdfTransformationMatrix shadingTransformationMatrix) : base(shading, transform, shadingTransformationMatrix)
        {
            this.tileBounds = tileBounds;
            this.patternBounds = patternBounds;
            this.maskShading = maskShading;
        }

        protected void AppendTile(PdfCommandConstructor constructor, PdfRectangle tileBounds, PdfTransformationMatrix shadingTransform)
        {
            constructor.SaveGraphicsState();
            constructor.ModifyTransformationMatrix(PdfTransformationMatrix.Multiply(base.ShadingTransformationMatrix, shadingTransform));
            if (this.maskShading != null)
            {
                PdfLuminositySoftMask mask = PdfLuminositySoftMaskBuilder.CreateSoftMask(this.maskShading, tileBounds, constructor.DocumentCatalog);
                PdfGraphicsStateParameters parameters = new PdfGraphicsStateParameters();
                parameters.SoftMask = mask;
                constructor.SetGraphicsStateParameters(parameters);
            }
            constructor.DrawShading(base.Shading);
            constructor.RestoreGraphicsState();
        }

        protected override PdfPattern CreatePattern(PdfDocumentCatalog documentCatalog)
        {
            PdfRectangle patternBounds = this.PatternBounds;
            PdfTilingPattern pattern = new PdfTilingPattern(base.Transform, patternBounds, patternBounds.Width, patternBounds.Height, documentCatalog);
            using (PdfCommandConstructor constructor = new PdfCommandConstructor(pattern.Resources))
            {
                this.FillTilingPatternCommands(constructor);
                pattern.ReplaceCommands(constructor.Commands);
            }
            return pattern;
        }

        protected abstract void FillTilingPatternCommands(PdfCommandConstructor constructor);

        protected PdfRectangle TileBounds =>
            this.tileBounds;

        protected PdfRectangle PatternBounds =>
            this.patternBounds;
    }
}

