namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PageSetupDialogShowingEventArgs : RoutedEventArgs
    {
        private readonly PdfPrinterSettings printerSettings;

        public PageSetupDialogShowingEventArgs() : base(PdfViewerControl.PageSetupDialogShowingEvent)
        {
            this.printerSettings = new PdfPrinterSettings();
        }

        public System.Windows.WindowStartupLocation WindowStartupLocation { get; set; }

        public PdfPrinterSettings PrinterSettings =>
            this.printerSettings;
    }
}

