namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using System;
    using System.Windows.Media.Imaging;

    public interface IPdfDocumentSelectionResults
    {
        BitmapSource GetImage(int rotationAngle);

        string Text { get; }

        PdfDocumentContentType ContentType { get; }
    }
}

