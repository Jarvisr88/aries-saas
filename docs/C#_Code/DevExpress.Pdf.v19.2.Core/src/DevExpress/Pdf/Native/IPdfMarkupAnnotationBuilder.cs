namespace DevExpress.Pdf.Native
{
    using System;

    public interface IPdfMarkupAnnotationBuilder : IPdfAnnotationBuilder
    {
        DateTimeOffset? CreationDate { get; }

        double Opacity { get; }

        string Subject { get; }

        string Title { get; }
    }
}

