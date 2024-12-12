namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;

    public class PdfHitTestResult
    {
        public PdfHitTestResult(PdfDocumentPosition position, PdfDocumentContentType contentType, bool isSelected)
        {
            this.DocumentPosition = position;
            this.ContentType = contentType;
            this.IsSelected = isSelected;
        }

        public PdfDocumentPosition DocumentPosition { get; private set; }

        public PdfDocumentContentType ContentType { get; private set; }

        public bool IsSelected { get; private set; }
    }
}

