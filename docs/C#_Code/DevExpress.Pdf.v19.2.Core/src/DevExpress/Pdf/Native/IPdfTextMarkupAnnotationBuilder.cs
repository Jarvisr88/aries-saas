namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System.Collections.Generic;

    public interface IPdfTextMarkupAnnotationBuilder : IPdfMarkupAnnotationBuilder, IPdfAnnotationBuilder
    {
        IList<PdfQuadrilateral> Quads { get; }

        PdfTextMarkupAnnotationType Style { get; }
    }
}

