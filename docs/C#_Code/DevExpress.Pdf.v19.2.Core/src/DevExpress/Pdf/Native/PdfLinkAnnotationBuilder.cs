namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfLinkAnnotationBuilder : PdfAnnotationBuilder, IPdfLinkAnnotationBuilder, IPdfAnnotationBuilder
    {
        public PdfLinkAnnotationBuilder(PdfRectangle rect, PdfDestinationObject destinationObject) : base(rect)
        {
            base.Border = new PdfAnnotationBorder(0.0);
            this.<Destination>k__BackingField = destinationObject;
        }

        public PdfLinkAnnotationBuilder(PdfRectangle rect, string uri) : base(rect)
        {
            base.Border = new PdfAnnotationBorder(0.0);
            this.<Uri>k__BackingField = uri;
        }

        public PdfDestinationObject Destination { get; }

        public string Uri { get; }
    }
}

