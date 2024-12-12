namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfPenExportVisitor : IDXBrushVisitor
    {
        private readonly PdfGraphicsCommandConstructor constructor;
        private readonly bool shouldSetNonStrokingColor;

        public PdfPenExportVisitor(PdfGraphicsCommandConstructor constructor, bool shouldSetNonStrokingColor)
        {
            this.constructor = constructor;
            this.shouldSetNonStrokingColor = shouldSetNonStrokingColor;
        }

        private void SetColor(PdfBrushContainer container)
        {
            PdfTransparentColor color = container.GetColor(this.constructor);
            if (this.shouldSetNonStrokingColor)
            {
                this.constructor.SetColorForNonStrokingOperations(color);
            }
            this.constructor.SetColorForStrokingOperations(color);
        }

        public void Visit(DXHatchBrush brush)
        {
            this.SetColor(new PdfHatchBrushContainer(brush));
        }

        public void Visit(DXLinearGradientBrush brush)
        {
            this.SetColor(new PdfLinearGradientBrushContainer(brush));
        }

        public void Visit(DXPathGradientBrush brush)
        {
            this.SetColor(new PdfPathGradientBrushContainer(brush));
        }

        public void Visit(DXSolidBrush brush)
        {
            this.SetColor(new PdfSolidBrushContainer(brush));
        }

        public void Visit(DXTextureBrush brush)
        {
            this.SetColor(new PdfTextureBrushContainer(brush));
        }
    }
}

