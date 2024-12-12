namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Collections.Generic;
    using System.Windows.Media.Imaging;

    public interface IPdfDocument
    {
        IEnumerable<int> CalcPrintPages(IEnumerable<PdfOutlineTreeListItem> selectedItems, bool useAsRange);
        bool CanPrintPages(IEnumerable<PdfOutlineTreeListItem> selectedItems, bool useAsRange);
        IEnumerable<PdfFileAttachmentListItem> CreateAttachments();
        BitmapSource CreateBitmap(int pageIndex, int largestEdgeLength);
        IEnumerable<PdfOutlineTreeListItem> CreateOutlines();
        IPdfDocumentProperties GetDocumentProperties();
        IPdfDocumentOutlinesViewerProperties GetOutlinesViewerProperties();
        string GetText(PdfDocumentArea area);
        string GetText(PdfDocumentPosition start, PdfDocumentPosition end);
        PdfDocumentContent HitTest(PdfDocumentPosition position);
        void NavigateToOutline(PdfOutlineTreeListItem item);
        PdfTextSearchResults PerformSearch(TextSearchParameter searchParameter);
        void PerformSelection(PdfDocumentSelectionParameter selectionParameter);
        void Print(PdfPrinterSettings print, int currentPageNumber, bool showPrintStatus);
        [Obsolete("Use the Print(PdfPrinterSettings print, int currentPageNumber, bool showPrintStatus) overload of this method instead.")]
        void Print(PdfPrinterSettings print, int currentPageNumber, bool showPrintStatus, int maxPrintingDpi);
        void SetCurrentPage(int index, bool allowCurrentPageHighlighting);
        void UpdateDocumentRotateAngle(double rotateAngle);
        void UpdateDocumentSelection();

        IPdfDocumentSelectionResults SelectionResults { get; }

        bool HasInteractiveForm { get; }

        bool HasSelection { get; }

        bool HasOutlines { get; }

        bool HasAttachments { get; }

        PdfCaret Caret { get; }

        IEnumerable<IPdfPage> Pages { get; }

        long ImageCacheSize { get; set; }

        bool IsLoaded { get; }

        bool IsLoadingFailed { get; }

        bool IsDocumentModified { get; }
    }
}

