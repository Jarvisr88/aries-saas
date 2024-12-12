namespace DevExpress.Xpf.PdfViewer
{
    using System;

    public interface IPdfDocumentProperties
    {
        string FileName { get; }

        string Title { get; }

        string Author { get; }

        string Subject { get; }

        string Keywords { get; }

        DateTimeOffset? Created { get; }

        DateTimeOffset? Modified { get; }

        string Application { get; }

        string Producer { get; }

        string Version { get; }

        string Location { get; }

        long FileSize { get; }

        int NumberOfPages { get; }
    }
}

