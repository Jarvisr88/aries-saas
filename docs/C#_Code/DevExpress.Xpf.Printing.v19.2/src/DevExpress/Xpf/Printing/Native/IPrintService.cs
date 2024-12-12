namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Printing;
    using System.Windows.Documents;

    public interface IPrintService
    {
        bool? PrintDialog(DocumentPaginator paginator, ReadonlyPageData[] pageData, string jobDescription, bool asyncMode);
        void PrintDirect(DocumentPaginator paginator, ReadonlyPageData[] pageData, string jobDescription, bool asyncMode);
        void PrintDirect(DocumentPaginator paginator, ReadonlyPageData[] pageData, string jobDescription, PrintQueue printQueue, bool asyncMode);
        void PrintDirect(DocumentPaginator paginator, ReadonlyPageData[] pageData, string jobDescription, string printerName, bool asyncMode);
    }
}

