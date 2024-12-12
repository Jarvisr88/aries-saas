namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfPathGradientBrushContainer : PdfBrushContainer
    {
        private readonly DXPathGradientBrush brush;

        public PdfPathGradientBrushContainer(DXPathGradientBrush brush)
        {
            this.brush = brush;
        }

        public override PdfTransparentColor GetColor(PdfGraphicsCommandConstructor commandConstructor) => 
            new PdfTransparentColor(0xff, new PdfPathGradientPatternConstructor(this.brush, commandConstructor.BBox, GetActualTransformationMatrix(commandConstructor, this.brush)).CreatePattern(commandConstructor.DocumentCatalog), new double[0]);
    }
}

