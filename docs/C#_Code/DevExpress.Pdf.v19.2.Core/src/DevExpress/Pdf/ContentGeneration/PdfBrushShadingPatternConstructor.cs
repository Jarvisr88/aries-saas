namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfBrushShadingPatternConstructor : PdfBrushPatternConstructor
    {
        public PdfBrushShadingPatternConstructor(PdfShading shading, PdfTransformationMatrix transform, PdfTransformationMatrix shadingTransformationMatrix) : base(shading, transform, shadingTransformationMatrix)
        {
        }

        protected override PdfPattern CreatePattern(PdfDocumentCatalog documentCatalog) => 
            new PdfShadingPattern(base.Shading, PdfTransformationMatrix.Multiply(base.ShadingTransformationMatrix, base.Transform));
    }
}

