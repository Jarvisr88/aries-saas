namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfXObjectImageCachedResource : PdfXObjectCachedResource
    {
        private int xObjectNumber;

        public PdfXObjectImageCachedResource(int width, int height, int xObjectNumber) : base((float) width, (float) height)
        {
            this.xObjectNumber = xObjectNumber;
        }

        public override void Draw(PdfCommandConstructor constructor, PdfRectangle bounds)
        {
            constructor.DrawImage(this.xObjectNumber, bounds);
        }

        public override void Draw(PdfCommandConstructor constructor, PdfRectangle bounds, PdfTransformationMatrix transform)
        {
            constructor.DrawImage(this.xObjectNumber, bounds, transform);
        }
    }
}

