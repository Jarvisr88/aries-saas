namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfBrushVerticallyFlippedTilingPatternConstructor : PdfBrushTilingPatternConstructor
    {
        public PdfBrushVerticallyFlippedTilingPatternConstructor(PdfRectangle tileBounds, PdfShading shading, PdfShading maskShading, PdfTransformationMatrix transform, PdfTransformationMatrix shadingTransformationMatrix) : base(tileBounds, shading, maskShading, transform, new PdfRectangle(tileBounds.Left, tileBounds.Bottom, tileBounds.Right, tileBounds.Top + tileBounds.Height), shadingTransformationMatrix)
        {
        }

        protected override void FillTilingPatternCommands(PdfCommandConstructor constructor)
        {
            PdfRectangle tileBounds = base.TileBounds;
            PdfTransformationMatrix shadingTransform = new PdfTransformationMatrix(1.0, 0.0, 0.0, -1.0, 0.0, tileBounds.Top * 2.0);
            base.AppendTile(constructor, new PdfRectangle(tileBounds.Left, tileBounds.Bottom, tileBounds.Right, tileBounds.Top), new PdfTransformationMatrix());
            base.AppendTile(constructor, new PdfRectangle(tileBounds.Left, tileBounds.Top, tileBounds.Right, base.PatternBounds.Top), shadingTransform);
        }
    }
}

