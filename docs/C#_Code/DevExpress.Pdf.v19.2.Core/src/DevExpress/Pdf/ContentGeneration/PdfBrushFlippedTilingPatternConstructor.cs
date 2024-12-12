namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfBrushFlippedTilingPatternConstructor : PdfBrushTilingPatternConstructor
    {
        public PdfBrushFlippedTilingPatternConstructor(PdfRectangle tileBounds, PdfShading shading, PdfShading maskShading, PdfTransformationMatrix transform, PdfTransformationMatrix shadingTransformationMatrix) : base(tileBounds, shading, maskShading, transform, new PdfRectangle(tileBounds.Left, tileBounds.Bottom, tileBounds.Width + tileBounds.Right, tileBounds.Top + tileBounds.Height), shadingTransformationMatrix)
        {
        }

        protected override void FillTilingPatternCommands(PdfCommandConstructor constructor)
        {
            PdfRectangle tileBounds = base.TileBounds;
            PdfTransformationMatrix shadingTransform = new PdfTransformationMatrix(-1.0, 0.0, 0.0, 1.0, tileBounds.Right * 2.0, 0.0);
            PdfTransformationMatrix matrix2 = new PdfTransformationMatrix(1.0, 0.0, 0.0, -1.0, 0.0, tileBounds.Top * 2.0);
            PdfRectangle patternBounds = base.PatternBounds;
            base.AppendTile(constructor, new PdfRectangle(tileBounds.Left, tileBounds.Bottom, tileBounds.Right, tileBounds.Top), new PdfTransformationMatrix());
            base.AppendTile(constructor, new PdfRectangle(tileBounds.Right, tileBounds.Bottom, patternBounds.Right, tileBounds.Top), shadingTransform);
            base.AppendTile(constructor, new PdfRectangle(tileBounds.Left, tileBounds.Top, tileBounds.Right, patternBounds.Top), matrix2);
            base.AppendTile(constructor, new PdfRectangle(tileBounds.Right, tileBounds.Top, patternBounds.Right, patternBounds.Top), PdfTransformationMatrix.Multiply(matrix2, shadingTransform));
        }
    }
}

