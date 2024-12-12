namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf;
    using System;
    using System.Drawing.Printing;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class PdfQueryPageSettingsEventArgs : RoutedEventArgs
    {
        public PdfQueryPageSettingsEventArgs(DevExpress.Pdf.PdfQueryPageSettingsEventArgs args) : base(PdfViewerControl.QueryPageSettingsEvent)
        {
            this.PageNumber = args.PageNumber;
            this.PageSize = new Size((double) args.PageSize.Width, (double) args.PageSize.Height);
            this.PrintAction = args.PrintAction;
            this.PrintInGrayscale = args.PrintInGrayscale;
            this.PageSettings = args.PageSettings;
        }

        public int PageNumber { get; private set; }

        public Size PageSize { get; private set; }

        public bool PrintInGrayscale { get; set; }

        public System.Drawing.Printing.PageSettings PageSettings { get; set; }

        public System.Drawing.Printing.PrintAction PrintAction { get; private set; }

        public bool Cancel { get; set; }
    }
}

