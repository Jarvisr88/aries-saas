namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public abstract class PdfGraphicsAddLinkCommand : PdfGraphicsCommand
    {
        private readonly RectangleF linkArea;

        protected PdfGraphicsAddLinkCommand(RectangleF linkArea)
        {
            this.linkArea = linkArea;
        }

        protected abstract PdfLinkAnnotation CreateLinkAnnotation(PdfRectangle rect, PdfGraphicsCommandConstructor constructor, PdfPage page);
        public override void Execute(PdfGraphicsCommandConstructor constructor, PdfPage page)
        {
            this.CreateLinkAnnotation(constructor.TransformRectangle(this.linkArea), constructor, page);
        }
    }
}

