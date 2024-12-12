namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfBrushNonFlippedTilingPatternConstructor : PdfBrushTilingPatternConstructor
    {
        public PdfBrushNonFlippedTilingPatternConstructor(PdfRectangle tileBounds, PdfShading shading, PdfShading maskShading, PdfTransformationMatrix transform, PdfTransformationMatrix shadingTransformationMatrix) : base(tileBounds, shading, maskShading, transform, tileBounds, shadingTransformationMatrix)
        {
        }

        protected override void FillTilingPatternCommands(PdfCommandConstructor constructor)
        {
            base.AppendTile(constructor, base.TileBounds, new PdfTransformationMatrix());
        }
    }
}

