namespace DevExpress.Printing.Native.PrintEditor
{
    using DevExpress.Printing;
    using System;
    using System.Drawing.Printing;

    public interface IPrintForm
    {
        void AddPrinterItem(PrinterItem item);
        void SetPrintRange(System.Drawing.Printing.PrintRange printRange);
        void SetSelectedPrinter(string printerName);

        bool AllowSomePages { get; set; }

        bool CanDuplex { get; set; }

        bool Collate { get; set; }

        short Copies { get; set; }

        PrintDocument Document { get; set; }

        string PaperSource { get; set; }

        string PageRangeText { get; set; }

        string PrintFileName { get; set; }

        System.Drawing.Printing.PrintRange PrintRange { get; }

        bool PrintToFile { get; set; }

        System.Drawing.Printing.Duplex Duplex { get; set; }
    }
}

