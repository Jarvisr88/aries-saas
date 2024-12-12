namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;

    public class PdfDocumentContent
    {
        private readonly PdfDocumentPosition documentPosition;
        private readonly PdfDocumentContentType contentType;
        private readonly bool selected;

        public PdfDocumentContent(PdfDocumentPosition documentPosition, PdfDocumentContentType contentType, bool selected)
        {
            this.documentPosition = documentPosition;
            this.contentType = contentType;
            this.selected = selected;
        }

        public PdfDocumentPosition DocumentPosition =>
            this.documentPosition;

        public PdfDocumentContentType ContentType =>
            this.contentType;

        public bool IsSelected =>
            this.selected;

        internal PdfCursor Cursor
        {
            get
            {
                if (this.contentType == PdfDocumentContentType.Annotation)
                {
                    return PdfCursor.Annotation;
                }
                if (this.selected)
                {
                    return PdfCursor.SelectionContext;
                }
                PdfDocumentContentType contentType = this.contentType;
                return ((contentType == PdfDocumentContentType.Text) ? PdfCursor.TextSelection : ((contentType != PdfDocumentContentType.Image) ? PdfCursor.Default : PdfCursor.ImageSelection));
            }
        }
    }
}

