namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfSolidBrushContainer : PdfBrushContainer
    {
        private readonly DXSolidBrush brush;

        public PdfSolidBrushContainer(DXSolidBrush brush)
        {
            this.brush = brush;
        }

        public override PdfTransparentColor GetColor(PdfGraphicsCommandConstructor commandConstructor)
        {
            ARGBColor color = this.brush.Color;
            return new PdfTransparentColor(color.A, PdfGraphicsConverter.ConvertColorToColorComponents(color));
        }

        public DXSolidBrush Brush =>
            this.brush;
    }
}

