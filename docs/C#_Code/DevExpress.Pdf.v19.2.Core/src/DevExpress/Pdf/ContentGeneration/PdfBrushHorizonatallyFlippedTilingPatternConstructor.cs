namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfBrushHorizonatallyFlippedTilingPatternConstructor : PdfBrushTilingPatternConstructor
    {
        public PdfBrushHorizonatallyFlippedTilingPatternConstructor(PdfRectangle tileBounds, PdfShading shading, PdfShading maskShading, PdfTransformationMatrix transform, PdfTransformationMatrix shadingTransformationMatrix) : base(tileBounds, shading, maskShading, transform, new PdfRectangle(tileBounds.Left, tileBounds.Bottom, tileBounds.Width + tileBounds.Right, tileBounds.Top), shadingTransformationMatrix)
        {
        }

        protected override void FillTilingPatternCommands(PdfCommandConstructor constructor)
        {
            PdfRectangle tileBounds = base.TileBounds;
            PdfTransformationMatrix shadingTransform = new PdfTransformationMatrix(-1.0, 0.0, 0.0, 1.0, tileBounds.Right * 2.0, 0.0);
            base.AppendTile(constructor, new PdfRectangle(tileBounds.Left, tileBounds.Bottom, tileBounds.Right, tileBounds.Top), new PdfTransformationMatrix());
            base.AppendTile(constructor, new PdfRectangle(tileBounds.Right, tileBounds.Bottom, base.PatternBounds.Right, tileBounds.Top), shadingTransform);
        }
    }
}

