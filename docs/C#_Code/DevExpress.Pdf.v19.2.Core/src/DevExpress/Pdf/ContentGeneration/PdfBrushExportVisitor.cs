namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using System;

    public class PdfBrushExportVisitor : IDXBrushVisitor
    {
        private readonly PdfGraphicsCommandConstructor constructor;

        public PdfBrushExportVisitor(PdfGraphicsCommandConstructor constructor)
        {
            this.constructor = constructor;
        }

        public void Visit(DXHatchBrush brush)
        {
            this.constructor.SetBrush(new PdfHatchBrushContainer(brush));
        }

        public void Visit(DXLinearGradientBrush brush)
        {
            this.constructor.SetBrush(new PdfLinearGradientBrushContainer(brush));
        }

        public void Visit(DXPathGradientBrush brush)
        {
            this.constructor.SetBrush(new PdfPathGradientBrushContainer(brush));
        }

        public void Visit(DXSolidBrush brush)
        {
            this.constructor.SetBrush(new PdfSolidBrushContainer(brush));
        }

        public void Visit(DXTextureBrush brush)
        {
            this.constructor.SetBrush(new PdfTextureBrushContainer(brush));
        }
    }
}

