namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using System;

    public abstract class PdfXObjectCachedResource : PdfDisposableObject
    {
        private readonly float width;
        private readonly float height;

        protected PdfXObjectCachedResource(float width, float height)
        {
            this.width = width;
            this.height = height;
        }

        protected override void Dispose(bool disposing)
        {
        }

        public abstract void Draw(PdfCommandConstructor constructor, PdfRectangle bounds);
        public virtual void Draw(PdfCommandConstructor constructor, PdfRectangle bounds, PdfTransformationMatrix transform)
        {
        }

        public float Width =>
            this.width;

        public float Height =>
            this.height;
    }
}

