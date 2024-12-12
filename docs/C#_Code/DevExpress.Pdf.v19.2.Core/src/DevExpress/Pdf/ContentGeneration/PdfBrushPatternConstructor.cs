namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfBrushPatternConstructor
    {
        private readonly PdfTransformationMatrix transform;
        private readonly PdfShading shading;
        private readonly PdfTransformationMatrix shadingTransformationMatrix;

        protected PdfBrushPatternConstructor(PdfShading shading, PdfTransformationMatrix transform, PdfTransformationMatrix shadingTransformationMatrix)
        {
            this.shading = shading;
            this.transform = transform;
            this.shadingTransformationMatrix = shadingTransformationMatrix;
        }

        protected abstract PdfPattern CreatePattern(PdfDocumentCatalog documentCatalog);
        public static PdfPattern CreatePattern(DXWrapMode wrapMode, PdfTransformationMatrix transform, PdfRectangle tileBounds, PdfShading shading, PdfShading maskShading, PdfDocumentCatalog documentCatalog, PdfTransformationMatrix shadingTransformationMatrix)
        {
            switch (wrapMode)
            {
                case DXWrapMode.TileFlipX:
                    return new PdfBrushHorizonatallyFlippedTilingPatternConstructor(tileBounds, shading, maskShading, transform, shadingTransformationMatrix).CreatePattern(documentCatalog);

                case DXWrapMode.TileFlipY:
                    return new PdfBrushVerticallyFlippedTilingPatternConstructor(tileBounds, shading, maskShading, transform, shadingTransformationMatrix).CreatePattern(documentCatalog);

                case DXWrapMode.TileFlipXY:
                    return new PdfBrushFlippedTilingPatternConstructor(tileBounds, shading, maskShading, transform, shadingTransformationMatrix).CreatePattern(documentCatalog);

                case DXWrapMode.Clamp:
                    return new PdfBrushShadingPatternConstructor(shading, transform, shadingTransformationMatrix).CreatePattern(documentCatalog);
            }
            return new PdfBrushNonFlippedTilingPatternConstructor(tileBounds, shading, maskShading, transform, shadingTransformationMatrix).CreatePattern(documentCatalog);
        }

        protected PdfTransformationMatrix Transform =>
            this.transform;

        protected PdfShading Shading =>
            this.shading;

        protected PdfTransformationMatrix ShadingTransformationMatrix =>
            this.shadingTransformationMatrix;
    }
}

