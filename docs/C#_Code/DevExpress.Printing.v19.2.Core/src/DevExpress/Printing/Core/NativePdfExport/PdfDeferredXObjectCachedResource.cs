namespace DevExpress.Printing.Core.NativePdfExport
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Pdf.Native;
    using System;

    public class PdfDeferredXObjectCachedResource : PdfXObjectCachedResource
    {
        private readonly int objectNumber;
        private readonly PdfForm form;

        public PdfDeferredXObjectCachedResource(PdfForm form, int objectNumber, float width, float height) : base(width, height)
        {
            this.objectNumber = objectNumber;
            this.form = form;
        }

        public override void Draw(PdfCommandConstructor constructor, PdfRectangle bounds)
        {
            constructor.DrawForm(this.objectNumber, new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, bounds.Left, bounds.Bottom));
        }
    }
}

