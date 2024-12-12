namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public interface IPdfInteractiveOperationController
    {
        void EnsureVisible(int pageIndex, PdfRectangle bounds, bool inCenter);
        PdfPoint GetClientPoint(PdfDocumentPosition position);
        void GoToFirstPage();
        void GoToLastPage();
        void GoToNextPage();
        void GoToPreviousPage();
        void OpenDocument(string documentPath, PdfTarget target, bool openInNewWindow);
        void OpenUri(string uri);
        void ResetForm();
        void ResetFormExcludingFields(IEnumerable<PdfInteractiveFormField> fields);
        void ResetFormFields(IEnumerable<PdfInteractiveFormField> fields);
        void ShowDocumentPosition(PdfTarget target);
        void ShowPrintDialog();
    }
}

