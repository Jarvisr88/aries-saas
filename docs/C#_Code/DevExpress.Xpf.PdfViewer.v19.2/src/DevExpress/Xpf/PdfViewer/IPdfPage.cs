namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using System;
    using System.Windows;

    public interface IPdfPage
    {
        PdfDocumentArea PageArea { get; }

        int PageNumber { get; }

        double UserUnit { get; }

        Size PageSize { get; }

        bool IsSelected { get; }
    }
}

