namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PdfPrintPageEventArgs : RoutedEventArgs
    {
        public PdfPrintPageEventArgs(DevExpress.Pdf.PdfPrintPageEventArgs args) : base(PdfViewerControl.PrintPageEvent)
        {
            this.PageNumber = args.PageNumber;
            this.PageCount = args.PageCount;
            this.HasMorePages = args.HasMorePages;
            this.PageSettings = args.PageSettings;
            this.PageBounds = args.PageBounds;
            this.MarginBounds = args.MarginBounds;
            this.Graphics = args.Graphics;
        }

        public int PageNumber { get; private set; }

        public int PageCount { get; private set; }

        public bool HasMorePages { get; set; }

        public bool Cancel { get; set; }

        public System.Drawing.Printing.PageSettings PageSettings { get; private set; }

        public Rectangle PageBounds { get; private set; }

        public Rectangle MarginBounds { get; private set; }

        public System.Drawing.Graphics Graphics { get; private set; }
    }
}

