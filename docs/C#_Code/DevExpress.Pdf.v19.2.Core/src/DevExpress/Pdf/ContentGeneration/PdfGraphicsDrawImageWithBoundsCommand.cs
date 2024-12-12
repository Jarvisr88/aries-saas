namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public class PdfGraphicsDrawImageWithBoundsCommand : PdfGraphicsCommand
    {
        private readonly PdfXObjectCachedResource imageResource;
        private readonly RectangleF bounds;

        public PdfGraphicsDrawImageWithBoundsCommand(PdfXObjectCachedResource imageResource, RectangleF bounds)
        {
            this.imageResource = imageResource;
            this.bounds = bounds;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            constructor.DrawXObject(this.imageResource, this.bounds);
        }
    }
}

