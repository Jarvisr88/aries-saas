namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;

    public class PdfGraphicsAddLinkToUriCommand : PdfGraphicsAddLinkCommand
    {
        private readonly string uri;

        public PdfGraphicsAddLinkToUriCommand(RectangleF linkArea, string uri) : base(linkArea)
        {
            this.uri = uri;
        }

        protected override PdfLinkAnnotation CreateLinkAnnotation(PdfRectangle rect, PdfGraphicsCommandConstructor constructor, PdfPage page) => 
            new PdfLinkAnnotation(page, rect, this.uri);
    }
}

