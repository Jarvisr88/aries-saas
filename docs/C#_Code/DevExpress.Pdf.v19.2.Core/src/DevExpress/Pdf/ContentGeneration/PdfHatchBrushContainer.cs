namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfHatchBrushContainer : PdfBrushContainer
    {
        private readonly DXHatchBrush brush;

        public PdfHatchBrushContainer(DXHatchBrush brush)
        {
            this.brush = brush;
        }

        public override PdfTransparentColor GetColor(PdfGraphicsCommandConstructor commandConstructor) => 
            new PdfTransparentColor(0xff, commandConstructor.GraphicsDocument.HatchPatternCache.GetPattern(this.brush, commandConstructor.BBox), new double[0]);
    }
}

