namespace DevExpress.Xpf.Printing
{
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraReports;
    using System;
    using System.ComponentModel;
    using System.IO;
    using System.Printing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;

    public static class PrintHelper
    {
        private static bool usePrintTickets = true;

        [Obsolete("This event is now obsolete. Asynchronous printing is no longer supported."), EditorBrowsable(EditorBrowsableState.Never)]
        public static event AsyncCompletedEventHandler PrintCompleted;

        internal static ReadonlyPageData[] CollectPageData(Page[] pages)
        {
            ReadonlyPageData[] dataArray = new ReadonlyPageData[pages.Length];
            for (int i = 0; i < pages.Length; i++)
            {
                dataArray[i] = pages[i].PageData;
            }
            return dataArray;
        }

        private static PrintableControlLink CreateLink(IPrintableControl source, string documentName)
        {
            PrintableControlLink link1 = new PrintableControlLink(source);
            link1.DocumentName = documentName;
            return link1;
        }

        internal static LegacyPrintableComponentLink CreateLink(IPrintable source, string documentName) => 
            new LegacyPrintableComponentLink(source, documentName);

        public static void ExportToCsv(IPrintableControl source, Stream stream)
        {
            ExportToCsv(source, stream, new CsvExportOptions());
        }

        public static void ExportToCsv(IPrintableControl source, string filePath)
        {
            ExportToCsv(source, filePath, new CsvExportOptions());
        }

        public static void ExportToCsv(IPrintable source, Stream stream)
        {
            ExportToCsv(source, stream, new CsvExportOptions());
        }

        public static void ExportToCsv(IPrintable source, string filePath)
        {
            ExportToCsv(source, filePath, new CsvExportOptions());
        }

        public static void ExportToCsv(IPrintableControl source, Stream stream, CsvExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToCsv(stream, options);
            }
        }

        public static void ExportToCsv(IPrintableControl source, string filePath, CsvExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToCsv(filePath, options);
            }
        }

        public static void ExportToCsv(IPrintable source, Stream stream, CsvExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToCsv(stream, options);
            }
        }

        public static void ExportToCsv(IPrintable source, string filePath, CsvExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToCsv(filePath, options);
            }
        }

        public static void ExportToDocx(IPrintableControl source, Stream stream)
        {
            ExportToDocx(source, stream, new DocxExportOptions());
        }

        public static void ExportToDocx(IPrintableControl source, string filePath)
        {
            ExportToDocx(source, filePath, new DocxExportOptions());
        }

        public static void ExportToDocx(IPrintable source, Stream stream)
        {
            ExportToDocx(source, stream, new DocxExportOptions());
        }

        public static void ExportToDocx(IPrintable source, string filePath)
        {
            ExportToDocx(source, filePath, new DocxExportOptions());
        }

        public static void ExportToDocx(IPrintableControl source, Stream stream, DocxExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToDocx(stream, options);
            }
        }

        public static void ExportToDocx(IPrintableControl source, string filePath, DocxExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToDocx(filePath, options);
            }
        }

        public static void ExportToDocx(IPrintable source, Stream stream, DocxExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToDocx(stream, options);
            }
        }

        public static void ExportToDocx(IPrintable source, string filePath, DocxExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToDocx(filePath, options);
            }
        }

        public static void ExportToHtml(IPrintableControl source, Stream stream)
        {
            ExportToHtml(source, stream, new HtmlExportOptions());
        }

        public static void ExportToHtml(IPrintableControl source, string filePath)
        {
            ExportToHtml(source, filePath, new HtmlExportOptions());
        }

        public static void ExportToHtml(IPrintable source, Stream stream)
        {
            ExportToHtml(source, stream, new HtmlExportOptions());
        }

        public static void ExportToHtml(IPrintable source, string filePath)
        {
            ExportToHtml(source, filePath, new HtmlExportOptions());
        }

        public static void ExportToHtml(IPrintableControl source, Stream stream, HtmlExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToHtml(stream, options);
            }
        }

        public static void ExportToHtml(IPrintableControl source, string filePath, HtmlExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToHtml(filePath, options);
            }
        }

        public static void ExportToHtml(IPrintable source, Stream stream, HtmlExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToHtml(stream, options);
            }
        }

        public static void ExportToHtml(IPrintable source, string filePath, HtmlExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToHtml(filePath, options);
            }
        }

        public static void ExportToImage(IPrintableControl source, Stream stream)
        {
            ExportToImage(source, stream, new ImageExportOptions());
        }

        public static void ExportToImage(IPrintableControl source, string filePath)
        {
            ExportToImage(source, filePath, new ImageExportOptions());
        }

        public static void ExportToImage(IPrintable source, Stream stream)
        {
            ExportToImage(source, stream, new ImageExportOptions());
        }

        public static void ExportToImage(IPrintable source, string filePath)
        {
            ExportToImage(source, filePath, new ImageExportOptions());
        }

        public static void ExportToImage(IPrintableControl source, Stream stream, ImageExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToImage(stream, options);
            }
        }

        public static void ExportToImage(IPrintableControl source, string filePath, ImageExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToImage(filePath, options);
            }
        }

        public static void ExportToImage(IPrintable source, Stream stream, ImageExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToImage(stream, options);
            }
        }

        public static void ExportToImage(IPrintable source, string filePath, ImageExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToImage(filePath, options);
            }
        }

        public static void ExportToMht(IPrintableControl source, Stream stream)
        {
            ExportToMht(source, stream, new MhtExportOptions());
        }

        public static void ExportToMht(IPrintableControl source, string filePath)
        {
            ExportToMht(source, filePath, new MhtExportOptions());
        }

        public static void ExportToMht(IPrintable source, Stream stream)
        {
            ExportToMht(source, stream, new MhtExportOptions());
        }

        public static void ExportToMht(IPrintable source, string filePath)
        {
            ExportToMht(source, filePath, new MhtExportOptions());
        }

        public static void ExportToMht(IPrintableControl source, Stream stream, MhtExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToMht(stream, options);
            }
        }

        public static void ExportToMht(IPrintableControl source, string filePath, MhtExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToMht(filePath, options);
            }
        }

        public static void ExportToMht(IPrintable source, Stream stream, MhtExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToMht(stream, options);
            }
        }

        public static void ExportToMht(IPrintable source, string filePath, MhtExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToMht(filePath, options);
            }
        }

        public static void ExportToPdf(IPrintableControl source, Stream stream)
        {
            ExportToPdf(source, stream, new PdfExportOptions());
        }

        public static void ExportToPdf(IPrintableControl source, string filePath)
        {
            ExportToPdf(source, filePath, new PdfExportOptions());
        }

        public static void ExportToPdf(IPrintable source, Stream stream)
        {
            ExportToPdf(source, stream, new PdfExportOptions());
        }

        public static void ExportToPdf(IPrintable source, string filePath)
        {
            ExportToPdf(source, filePath, new PdfExportOptions());
        }

        public static void ExportToPdf(IPrintableControl source, Stream stream, PdfExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToPdf(stream, options);
            }
        }

        public static void ExportToPdf(IPrintableControl source, string filePath, PdfExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToPdf(filePath, options);
            }
        }

        public static void ExportToPdf(IPrintable source, Stream stream, PdfExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToPdf(stream, options);
            }
        }

        public static void ExportToPdf(IPrintable source, string filePath, PdfExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToPdf(filePath, options);
            }
        }

        public static void ExportToRtf(IPrintableControl source, Stream stream)
        {
            ExportToRtf(source, stream, new RtfExportOptions());
        }

        public static void ExportToRtf(IPrintableControl source, string filePath)
        {
            ExportToRtf(source, filePath, new RtfExportOptions());
        }

        public static void ExportToRtf(IPrintable source, Stream stream)
        {
            ExportToRtf(source, stream, new RtfExportOptions());
        }

        public static void ExportToRtf(IPrintable source, string filePath)
        {
            ExportToRtf(source, filePath, new RtfExportOptions());
        }

        public static void ExportToRtf(IPrintableControl source, Stream stream, RtfExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToRtf(stream, options);
            }
        }

        public static void ExportToRtf(IPrintableControl source, string filePath, RtfExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToRtf(filePath, options);
            }
        }

        public static void ExportToRtf(IPrintable source, Stream stream, RtfExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToRtf(stream, options);
            }
        }

        public static void ExportToRtf(IPrintable source, string filePath, RtfExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToRtf(filePath, options);
            }
        }

        public static void ExportToText(IPrintableControl source, Stream stream)
        {
            ExportToText(source, stream, new TextExportOptions());
        }

        public static void ExportToText(IPrintableControl source, string filePath)
        {
            ExportToText(source, filePath, new TextExportOptions());
        }

        public static void ExportToText(IPrintable source, Stream stream)
        {
            ExportToText(source, stream, new TextExportOptions());
        }

        public static void ExportToText(IPrintable source, string filePath)
        {
            ExportToText(source, filePath, new TextExportOptions());
        }

        public static void ExportToText(IPrintableControl source, Stream stream, TextExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToText(stream, options);
            }
        }

        public static void ExportToText(IPrintableControl source, string filePath, TextExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToText(filePath, options);
            }
        }

        public static void ExportToText(IPrintable source, Stream stream, TextExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToText(stream, options);
            }
        }

        public static void ExportToText(IPrintable source, string filePath, TextExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToText(filePath, options);
            }
        }

        public static void ExportToXls(IPrintableControl source, Stream stream)
        {
            ExportToXls(source, stream, new XlsExportOptions());
        }

        public static void ExportToXls(IPrintableControl source, string filePath)
        {
            ExportToXls(source, filePath, new XlsExportOptions());
        }

        public static void ExportToXls(IPrintable source, Stream stream)
        {
            ExportToXls(source, stream, new XlsExportOptions());
        }

        public static void ExportToXls(IPrintable source, string filePath)
        {
            ExportToXls(source, filePath, new XlsExportOptions());
        }

        public static void ExportToXls(IPrintableControl source, Stream stream, XlsExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToXls(stream, options);
            }
        }

        public static void ExportToXls(IPrintableControl source, string filePath, XlsExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToXls(filePath, options);
            }
        }

        public static void ExportToXls(IPrintable source, Stream stream, XlsExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToXls(stream, options);
            }
        }

        public static void ExportToXls(IPrintable source, string filePath, XlsExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToXls(filePath, options);
            }
        }

        public static void ExportToXlsx(IPrintableControl source, Stream stream)
        {
            ExportToXlsx(source, stream, new XlsxExportOptions());
        }

        public static void ExportToXlsx(IPrintableControl source, string filePath)
        {
            ExportToXlsx(source, filePath, new XlsxExportOptions());
        }

        public static void ExportToXlsx(IPrintable source, Stream stream)
        {
            ExportToXlsx(source, stream, new XlsxExportOptions());
        }

        public static void ExportToXlsx(IPrintable source, string filePath)
        {
            ExportToXlsx(source, filePath, new XlsxExportOptions());
        }

        public static void ExportToXlsx(IPrintableControl source, Stream stream, XlsxExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToXlsx(stream, options);
            }
        }

        public static void ExportToXlsx(IPrintableControl source, string filePath, XlsxExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToXlsx(filePath, options);
            }
        }

        public static void ExportToXlsx(IPrintable source, Stream stream, XlsxExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToXlsx(stream, options);
            }
        }

        public static void ExportToXlsx(IPrintable source, string filePath, XlsxExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToXlsx(filePath, options);
            }
        }

        public static void ExportToXps(IPrintableControl source, Stream stream)
        {
            ExportToXps(source, stream, new XpsExportOptions());
        }

        public static void ExportToXps(IPrintableControl source, string filePath)
        {
            ExportToXps(source, filePath, new XpsExportOptions());
        }

        public static void ExportToXps(IPrintable source, Stream stream)
        {
            ExportToXps(source, stream, new XpsExportOptions());
        }

        public static void ExportToXps(IPrintable source, string filePath)
        {
            ExportToXps(source, filePath, new XpsExportOptions());
        }

        public static void ExportToXps(IPrintableControl source, Stream stream, XpsExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToXps(stream, options);
            }
        }

        public static void ExportToXps(IPrintableControl source, string filePath, XpsExportOptions options)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.ExportToXps(filePath, options);
            }
        }

        public static void ExportToXps(IPrintable source, Stream stream, XpsExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToXps(stream, options);
            }
        }

        public static void ExportToXps(IPrintable source, string filePath, XpsExportOptions options)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.ExportToXps(filePath, options);
            }
        }

        private static void link_PrintCompleted(object sender, AsyncCompletedEventArgs e)
        {
            DevExpress.Xpf.Printing.LinkBase base2 = (DevExpress.Xpf.Printing.LinkBase) sender;
            base2.PrintCompleted -= new AsyncCompletedEventHandler(PrintHelper.link_PrintCompleted);
            base2.Dispose();
            if (PrintCompleted != null)
            {
                PrintCompleted(null, new AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }

        public static bool? Print(IPrintableControl source)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                return link.Print();
            }
        }

        public static bool? Print(IPrintable source)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                return link.Print();
            }
        }

        public static bool? Print(IReport source)
        {
            if (source.PrintingSystemBase.Document.PageCount == 0)
            {
                source.CreateDocument(false);
            }
            return PrintHelperInternal.Print(source.PrintingSystemBase, ActiveWindowFinderBase<ActiveWindowFinder>.Instance.GetActiveWindow(null));
        }

        [Obsolete("This method is now obsolete. Asynchronous printing is no longer supported. "), EditorBrowsable(EditorBrowsableState.Never)]
        public static void PrintAsync(IPrintableControl source)
        {
            PrintableControlLink link = new PrintableControlLink(source);
            link.PrintCompleted += new AsyncCompletedEventHandler(PrintHelper.link_PrintCompleted);
            link.PrintAsync();
        }

        [Obsolete("This method is now obsolete. Asynchronous printing is no longer supported. "), EditorBrowsable(EditorBrowsableState.Never)]
        public static void PrintAsync(IPrintable source)
        {
            LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source);
            link.PrintCompleted += new AsyncCompletedEventHandler(PrintHelper.link_PrintCompleted);
            link.PrintAsync();
        }

        public static void PrintDirect(IPrintableControl source)
        {
            PrintDirect(source, (string) null);
        }

        public static void PrintDirect(IPrintable source)
        {
            PrintDirect(source, (string) null);
        }

        public static void PrintDirect(IReport source)
        {
            PrintDirect(source, (string) null);
        }

        [Obsolete("This method is now obsolete. Use the DevExpress.Xpf.Printing.PrintHelper.PrintDirect(IPrintableControl source, string printerName) method instead"), EditorBrowsable(EditorBrowsableState.Never)]
        public static void PrintDirect(IPrintableControl source, PrintQueue queue)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.PrintDirect(queue);
            }
        }

        public static void PrintDirect(IPrintableControl source, string printerName)
        {
            using (PrintableControlLink link = new PrintableControlLink(source))
            {
                link.PrintDirect(printerName);
            }
        }

        [Obsolete("This method is now obsolete. Use the DevExpress.Xpf.Printing.PrintHelper.PrintDirect(IPrintable source, string printerName) method instead"), EditorBrowsable(EditorBrowsableState.Never)]
        public static void PrintDirect(IPrintable source, PrintQueue queue)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.PrintDirect(queue);
            }
        }

        public static void PrintDirect(IPrintable source, string printerName)
        {
            using (LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source))
            {
                link.PrintDirect();
            }
        }

        [Obsolete("This method is now obsolete. Use the DevExpress.Xpf.Printing.PrintHelper.PrintDirect(IReport source, string printerName) method instead"), EditorBrowsable(EditorBrowsableState.Never)]
        public static void PrintDirect(IReport source, PrintQueue queue)
        {
            new XtraReportPreviewModel(source).PrintDirect(queue);
        }

        public static void PrintDirect(IReport source, string printerName)
        {
            if (source.PrintingSystemBase.Document.PageCount == 0)
            {
                source.CreateDocument(false);
            }
            PrintHelperInternal.PrintDirect(source.PrintingSystemBase, printerName);
        }

        [Obsolete("This method is now obsolete. Asynchronous printing is no longer supported. "), EditorBrowsable(EditorBrowsableState.Never)]
        public static void PrintDirectAsync(IPrintableControl source)
        {
            PrintableControlLink link = new PrintableControlLink(source);
            link.PrintCompleted += new AsyncCompletedEventHandler(PrintHelper.link_PrintCompleted);
            link.PrintDirectAsync();
        }

        [Obsolete("This method is now obsolete. Asynchronous printing is no longer supported. "), EditorBrowsable(EditorBrowsableState.Never)]
        public static void PrintDirectAsync(IPrintable source)
        {
            LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source);
            link.PrintCompleted += new AsyncCompletedEventHandler(PrintHelper.link_PrintCompleted);
            link.PrintDirectAsync();
        }

        [Obsolete("This method is now obsolete. Asynchronous printing is no longer supported. "), EditorBrowsable(EditorBrowsableState.Never)]
        public static void PrintDirectAsync(IPrintableControl source, PrintQueue queue)
        {
            PrintableControlLink link = new PrintableControlLink(source);
            link.PrintCompleted += new AsyncCompletedEventHandler(PrintHelper.link_PrintCompleted);
            link.PrintDirectAsync(queue);
        }

        [Obsolete("This method is now obsolete. Asynchronous printing is no longer supported. "), EditorBrowsable(EditorBrowsableState.Never)]
        public static void PrintDirectAsync(IPrintable source, PrintQueue queue)
        {
            LegacyPrintableComponentLink link = new LegacyPrintableComponentLink(source);
            link.PrintCompleted += new AsyncCompletedEventHandler(PrintHelper.link_PrintCompleted);
            link.PrintDirectAsync(queue);
        }

        public static Window ShowPrintPreview(FrameworkElement owner, IPrintableControl source) => 
            ShowPrintPreview(owner, source, null, null);

        public static Window ShowPrintPreview(FrameworkElement owner, DevExpress.Xpf.Printing.LinkBase link) => 
            ShowPrintPreview(owner, link, null);

        public static Window ShowPrintPreview(FrameworkElement owner, IPrintable source) => 
            ShowPrintPreview(owner, source, null);

        public static Window ShowPrintPreview(FrameworkElement owner, IReport report) => 
            ShowPrintPreview(owner, report, null);

        public static Window ShowPrintPreview(FrameworkElement owner, IPrintableControl source, string documentName) => 
            ShowPrintPreview(owner, source, documentName, null);

        public static Window ShowPrintPreview(FrameworkElement owner, DevExpress.Xpf.Printing.LinkBase link, string title)
        {
            Window activeWindow = ActiveWindowFinderBase<ActiveWindowFinder>.Instance.GetActiveWindow(owner);
            return new LinkPreviewHelper(link, null).ShowDocumentPreview(activeWindow, title);
        }

        public static Window ShowPrintPreview(FrameworkElement owner, IPrintable source, string documentName) => 
            ShowPrintPreview(owner, source, documentName, null);

        public static Window ShowPrintPreview(FrameworkElement owner, IReport report, string title)
        {
            Window activeWindow = ActiveWindowFinderBase<ActiveWindowFinder>.Instance.GetActiveWindow(owner);
            return new ReportPreviewHelper(report).ShowDocumentPreview(activeWindow, title);
        }

        public static Window ShowPrintPreview(FrameworkElement owner, IPrintableControl source, string documentName, string title) => 
            ShowPrintPreview(owner, CreateLink(source, documentName), title);

        public static Window ShowPrintPreview(FrameworkElement owner, IPrintable source, string documentName, string title)
        {
            Window activeWindow = ActiveWindowFinderBase<ActiveWindowFinder>.Instance.GetActiveWindow(owner);
            return new LinkPreviewHelper(CreateLink(source, documentName), null).ShowDocumentPreview(activeWindow, title);
        }

        public static void ShowPrintPreviewDialog(Window owner, IPrintableControl source)
        {
            ShowPrintPreviewDialog(owner, source, null, null);
        }

        public static void ShowPrintPreviewDialog(Window owner, DevExpress.Xpf.Printing.LinkBase link)
        {
            ShowPrintPreviewDialog(owner, link, null);
        }

        public static void ShowPrintPreviewDialog(Window owner, IPrintable source)
        {
            ShowPrintPreviewDialog(owner, source, null);
        }

        public static void ShowPrintPreviewDialog(Window owner, IReport report)
        {
            ShowPrintPreviewDialog(owner, report, null);
        }

        public static void ShowPrintPreviewDialog(Window owner, IPrintableControl source, string documentName)
        {
            ShowPrintPreviewDialog(owner, source, documentName, null);
        }

        public static void ShowPrintPreviewDialog(Window owner, DevExpress.Xpf.Printing.LinkBase link, string title)
        {
            new LinkPreviewHelper(link, null).ShowDocumentPreviewDialog(owner, title);
        }

        public static void ShowPrintPreviewDialog(Window owner, IPrintable source, string documentName)
        {
            ShowPrintPreviewDialog(owner, source, documentName, null);
        }

        public static void ShowPrintPreviewDialog(Window owner, IReport report, string title)
        {
            new ReportPreviewHelper(report).ShowDocumentPreviewDialog(owner, title);
        }

        public static void ShowPrintPreviewDialog(Window owner, IPrintableControl source, string documentName, string title)
        {
            using (DevExpress.Xpf.Printing.LinkBase base2 = CreateLink(source, documentName))
            {
                ShowPrintPreviewDialog(owner, base2, title);
            }
        }

        internal static void ShowPrintPreviewDialog(Window owner, LegacyPrintableComponentLink link, string title, string themeName)
        {
            new LinkPreviewHelper(link, themeName).ShowDocumentPreviewDialog(owner, title);
        }

        public static void ShowPrintPreviewDialog(Window owner, IPrintable source, string documentName, string title)
        {
            ShowPrintPreviewDialog(owner, source, documentName, title, null);
        }

        internal static void ShowPrintPreviewDialog(Window owner, IPrintable source, string documentName, string title, string themeName)
        {
            using (LegacyPrintableComponentLink link = CreateLink(source, documentName))
            {
                new LinkPreviewHelper(link, themeName).ShowDocumentPreviewDialog(owner, title);
            }
        }

        public static Window ShowRibbonPrintPreview(FrameworkElement owner, IPrintableControl source) => 
            ShowRibbonPrintPreview(owner, source, null, null);

        public static Window ShowRibbonPrintPreview(FrameworkElement owner, DevExpress.Xpf.Printing.LinkBase link) => 
            ShowRibbonPrintPreview(owner, link, null);

        public static Window ShowRibbonPrintPreview(FrameworkElement owner, IPrintable source) => 
            ShowRibbonPrintPreview(owner, source, null);

        public static Window ShowRibbonPrintPreview(FrameworkElement owner, IReport report) => 
            ShowRibbonPrintPreview(owner, report, null);

        public static Window ShowRibbonPrintPreview(FrameworkElement owner, IPrintableControl source, string documentName) => 
            ShowRibbonPrintPreview(owner, source, documentName, null);

        public static Window ShowRibbonPrintPreview(FrameworkElement owner, DevExpress.Xpf.Printing.LinkBase link, string title)
        {
            Window activeWindow = ActiveWindowFinderBase<ActiveWindowFinder>.Instance.GetActiveWindow(owner);
            return new LinkPreviewHelper(link, null).ShowRibbonDocumentPreview(activeWindow, title);
        }

        public static Window ShowRibbonPrintPreview(FrameworkElement owner, IPrintable source, string documentName) => 
            ShowRibbonPrintPreview(owner, source, documentName, null);

        public static Window ShowRibbonPrintPreview(FrameworkElement owner, IReport report, string title)
        {
            Window activeWindow = ActiveWindowFinderBase<ActiveWindowFinder>.Instance.GetActiveWindow(owner);
            return new ReportPreviewHelper(report).ShowRibbonDocumentPreview(activeWindow, title);
        }

        public static Window ShowRibbonPrintPreview(FrameworkElement owner, IPrintableControl source, string documentName, string title) => 
            ShowRibbonPrintPreview(owner, CreateLink(source, documentName), title);

        public static Window ShowRibbonPrintPreview(FrameworkElement owner, IPrintable source, string documentName, string title)
        {
            Window activeWindow = ActiveWindowFinderBase<ActiveWindowFinder>.Instance.GetActiveWindow(owner);
            return new LinkPreviewHelper(CreateLink(source, documentName), null).ShowRibbonDocumentPreview(activeWindow, title);
        }

        public static void ShowRibbonPrintPreviewDialog(Window owner, IPrintableControl source)
        {
            ShowRibbonPrintPreviewDialog(owner, source, null, null);
        }

        public static void ShowRibbonPrintPreviewDialog(Window owner, DevExpress.Xpf.Printing.LinkBase link)
        {
            ShowRibbonPrintPreviewDialog(owner, link, null);
        }

        public static void ShowRibbonPrintPreviewDialog(Window owner, IPrintable source)
        {
            ShowRibbonPrintPreviewDialog(owner, source, null);
        }

        public static void ShowRibbonPrintPreviewDialog(Window owner, IReport report)
        {
            ShowRibbonPrintPreviewDialog(owner, report, null);
        }

        public static void ShowRibbonPrintPreviewDialog(Window owner, IPrintableControl source, string documentName)
        {
            ShowRibbonPrintPreviewDialog(owner, source, documentName, null);
        }

        public static void ShowRibbonPrintPreviewDialog(Window owner, DevExpress.Xpf.Printing.LinkBase link, string title)
        {
            new LinkPreviewHelper(link, null).ShowRibbonDocumentPreviewDialog(owner, title);
        }

        public static void ShowRibbonPrintPreviewDialog(Window owner, IPrintable source, string documentName)
        {
            ShowRibbonPrintPreviewDialog(owner, source, documentName, null);
        }

        public static void ShowRibbonPrintPreviewDialog(Window owner, IReport report, string title)
        {
            new ReportPreviewHelper(report).ShowRibbonDocumentPreviewDialog(owner, title);
        }

        public static void ShowRibbonPrintPreviewDialog(Window owner, IPrintableControl source, string documentName, string title)
        {
            using (DevExpress.Xpf.Printing.LinkBase base2 = CreateLink(source, documentName))
            {
                ShowRibbonPrintPreviewDialog(owner, base2, title);
            }
        }

        internal static void ShowRibbonPrintPreviewDialog(Window owner, LegacyPrintableComponentLink link, string title, string themeName)
        {
            new LinkPreviewHelper(link, themeName).ShowRibbonDocumentPreviewDialog(owner, title);
        }

        public static void ShowRibbonPrintPreviewDialog(Window owner, IPrintable source, string documentName, string title)
        {
            ShowRibbonPrintPreviewDialog(owner, source, documentName, title, null);
        }

        internal static void ShowRibbonPrintPreviewDialog(Window owner, IPrintable source, string documentName, string title, string themeName)
        {
            using (LegacyPrintableComponentLink link = CreateLink(source, documentName))
            {
                new LinkPreviewHelper(link, themeName).ShowRibbonDocumentPreviewDialog(owner, title);
            }
        }

        [Obsolete("test"), EditorBrowsable(EditorBrowsableState.Never)]
        public static bool UsePrintTickets
        {
            get => 
                usePrintTickets;
            set => 
                usePrintTickets = value;
        }
    }
}

