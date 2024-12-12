namespace DevExpress.Pdf.Native
{
    using System;

    public interface IPdfLinkAnnotationBuilder : IPdfAnnotationBuilder
    {
        PdfDestinationObject Destination { get; }

        string Uri { get; }
    }
}

