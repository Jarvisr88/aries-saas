namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public class PdfGraphicsDrawImageWithPointCommand : PdfGraphicsCommand
    {
        private readonly PdfXObjectCachedResource imageResource;
        private readonly PointF location;

        public PdfGraphicsDrawImageWithPointCommand(PdfXObjectCachedResource imageResource, PointF location)
        {
            this.imageResource = imageResource;
            this.location = location;
        }

        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            constructor.DrawXObject(this.imageResource, this.location);
        }
    }
}

