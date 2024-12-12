namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing.Exports;
    using DevExpress.Xpf.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.XamlExport;
    using System;
    using System.ComponentModel;
    using System.Drawing;
    using System.Drawing.Imaging;
    using System.Drawing.Printing;
    using System.IO;
    using System.Printing;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Markup;

    public abstract class LinkBase : DependencyObject, IDisposable, ILink2, ILink, IDocumentSource
    {
        public event EventHandler<EventArgs> CreateDocumentFinished;

        public event EventHandler<EventArgs> CreateDocumentStarted;

        [Obsolete("This event is now obsolete. Asynchronous printing is no longer supported."), EditorBrowsable(EditorBrowsableState.Never)]
        public event AsyncCompletedEventHandler PrintCompleted;

        protected LinkBase() : this(null, string.Empty)
        {
        }

        protected LinkBase(DevExpress.Xpf.Printing.PrintingSystem ps) : this(ps, string.Empty)
        {
        }

        protected LinkBase(string documentName) : this(null, documentName)
        {
        }

        protected LinkBase(DevExpress.Xpf.Printing.PrintingSystem ps, string documentName)
        {
            if (ps != null)
            {
                this.PrintingSystem = ps;
            }
            else
            {
                this.PrintingSystem = new DevExpress.Xpf.Printing.PrintingSystem();
                this.IsPrintingSystemOwner = true;
            }
            this.PrintingSystem.PageSettingsChanged += new EventHandler(this.ps_PageSettingsChanged);
            this.PrintingSystem.AfterBuildPages += new EventHandler(this.PrintingSystem_AfterBuildPages);
            this.PrintingSystem.ReplaceService<XpsExportServiceBase>(new XpsExportService(this.CreatePaginator()));
            this.DocumentName = documentName;
            this.PaperKind = System.Drawing.Printing.PaperKind.Letter;
            this.Margins = XtraPageSettingsBase.DefaultMargins;
            this.MinMargins = XtraPageSettingsBase.DefaultMinMargins;
        }

        protected virtual void AfterBuildPages()
        {
            this.OnCreateDocumentFinished();
        }

        [Obsolete("This method is now obsolete. Asynchronous printing is no longer supported."), EditorBrowsable(EditorBrowsableState.Never)]
        public void CancelPrintAsync()
        {
            if (this.ActualPrintingContext != null)
            {
                this.ActualPrintingContext.DocumentPrinter.CancelPrint();
            }
        }

        public void CreateDocument()
        {
            this.CreateDocument(false);
        }

        public void CreateDocument(bool buildForInstantPreview)
        {
            this.BuildPagesInBackground = buildForInstantPreview;
            this.CreateDocument(buildForInstantPreview, true);
        }

        private void CreateDocument(bool buildPagesInBackground, bool applyPageSettings)
        {
            this.OnCreateDocumentStarted();
            this.CreateDocumentCore(buildPagesInBackground, applyPageSettings);
        }

        protected abstract void CreateDocumentCore(bool buildPagesInBackground, bool applyPageSettings);
        internal void CreateIfEmpty(bool buildPagesInBackground)
        {
            if (this.PrintingSystem.Document.PageCount == 0)
            {
                this.CreateDocument(buildPagesInBackground);
            }
        }

        protected virtual PageVisualizer CreatePageVisualizer() => 
            new BrickPageVisualizer(TextMeasurementSystem.NativeXpf);

        internal DocumentPaginator CreatePaginator() => 
            new DelegatePaginator(new Func<int, FrameworkElement>(this.VisualizePage), () => this.PrintingSystem.Document.Pages.Count);

        void ILink2.AddSubreport(PrintingSystemBase ps, DocumentBand band, PointF offset)
        {
            throw new NotImplementedException();
        }

        public void Dispose()
        {
            this.Dispose(true);
            GC.SuppressFinalize(this);
        }

        protected virtual void Dispose(bool disposing)
        {
            if (disposing && (this.PrintingSystem != null))
            {
                if (this.PrintingSystem.Document.IsCreating)
                {
                    this.PrintingSystem.Document.StopPageBuilding();
                }
                this.PrintingSystem.PageSettingsChanged -= new EventHandler(this.ps_PageSettingsChanged);
                this.PrintingSystem.AfterBuildPages -= new EventHandler(this.PrintingSystem_AfterBuildPages);
                ImageRepository.Clear(RepositoryImageHelper.GetDocumentId(this.PrintingSystem));
                if (this.IsPrintingSystemOwner)
                {
                    this.PrintingSystem.Dispose();
                    this.PrintingSystem = null;
                }
            }
        }

        public void ExportToCsv(Stream stream)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToCsv(stream);
        }

        public void ExportToCsv(string filePath)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToCsv(filePath);
        }

        public void ExportToCsv(Stream stream, CsvExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToCsv(stream, options);
        }

        public void ExportToCsv(string filePath, CsvExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToCsv(filePath, options);
        }

        public void ExportToDocx(Stream stream)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToDocx(stream);
        }

        public void ExportToDocx(string filePath)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToDocx(filePath);
        }

        public void ExportToDocx(Stream stream, DocxExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToDocx(stream, options);
        }

        public void ExportToDocx(string filePath, DocxExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToDocx(filePath, options);
        }

        public void ExportToHtml(Stream stream)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToHtml(stream);
        }

        public void ExportToHtml(string filePath)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToHtml(filePath);
        }

        public void ExportToHtml(Stream stream, HtmlExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToHtml(stream, options);
        }

        public void ExportToHtml(string filePath, HtmlExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToHtml(filePath, options);
        }

        public void ExportToImage(Stream stream)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToImage(stream);
        }

        public void ExportToImage(string filePath)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToImage(filePath);
        }

        public void ExportToImage(Stream stream, ImageExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToImage(stream, options);
        }

        public void ExportToImage(Stream stream, ImageFormat format)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToImage(stream, format);
        }

        public void ExportToImage(string filePath, ImageExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToImage(filePath, options);
        }

        public void ExportToImage(string filePath, ImageFormat format)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToImage(filePath, format);
        }

        public void ExportToMht(Stream stream)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToMht(stream);
        }

        public void ExportToMht(string filePath)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToMht(filePath);
        }

        public void ExportToMht(Stream stream, MhtExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToMht(stream, options);
        }

        public void ExportToMht(string filePath, MhtExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToMht(filePath, options);
        }

        public void ExportToPdf(Stream stream)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToPdf(stream);
        }

        public void ExportToPdf(string filePath)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToPdf(filePath);
        }

        public void ExportToPdf(Stream stream, PdfExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToPdf(stream, options);
        }

        public void ExportToPdf(string filePath, PdfExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToPdf(filePath, options);
        }

        public void ExportToRtf(Stream stream)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToRtf(stream);
        }

        public void ExportToRtf(string filePath)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToRtf(filePath);
        }

        public void ExportToRtf(Stream stream, RtfExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToRtf(stream, options);
        }

        public void ExportToRtf(string filePath, RtfExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToRtf(filePath, options);
        }

        public void ExportToText(Stream stream)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToText(stream);
        }

        public void ExportToText(string filePath)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToText(filePath);
        }

        public void ExportToText(Stream stream, TextExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToText(stream, options);
        }

        public void ExportToText(string filePath, TextExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToText(filePath, options);
        }

        public void ExportToXls(Stream stream)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToXls(stream);
        }

        public void ExportToXls(string filePath)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToXls(filePath);
        }

        public void ExportToXls(Stream stream, XlsExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToXls(stream, options);
        }

        public void ExportToXls(string filePath, XlsExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToXls(filePath, options);
        }

        public void ExportToXlsx(Stream stream)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToXlsx(stream);
        }

        public void ExportToXlsx(string filePath)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToXlsx(filePath);
        }

        public void ExportToXlsx(Stream stream, XlsxExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToXlsx(stream, options);
        }

        public void ExportToXlsx(string filePath, XlsxExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToXlsx(filePath, options);
        }

        public void ExportToXps(Stream stream, XpsExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToXps(stream, options);
        }

        public void ExportToXps(string filePath, XpsExportOptions options)
        {
            this.CreateIfEmpty(false);
            this.PrintingSystem.ExportToXps(filePath, options);
        }

        private void LinkBase_CreateDocumentFinished(object sender, EventArgs e)
        {
            this.CreateDocumentFinished -= new EventHandler<EventArgs>(this.LinkBase_CreateDocumentFinished);
            this.StartPrintAsync();
        }

        protected void OnCreateDocumentFinished()
        {
            if (this.CreateDocumentFinished != null)
            {
                this.CreateDocumentFinished(this, EventArgs.Empty);
            }
        }

        protected void OnCreateDocumentStarted()
        {
            if (this.CreateDocumentStarted != null)
            {
                this.CreateDocumentStarted(this, EventArgs.Empty);
            }
        }

        private void PrepareToPrint()
        {
            this.CreateIfEmpty(false);
        }

        public bool? Print()
        {
            this.PrepareToPrint();
            return PrintHelperInternal.Print(this.PrintingSystem, ActiveWindowFinderBase<ActiveWindowFinder>.Instance.GetActiveWindow(null));
        }

        [Obsolete("This method is now obsolete. Asynchronous printing is no longer supported."), EditorBrowsable(EditorBrowsableState.Never)]
        public void PrintAsync()
        {
            this.PrintAsyncCore(PrintMode.Dialog, null);
        }

        private void PrintAsyncCore(PrintMode printMode, PrintQueue printQueue)
        {
            this.ActualPrintingContext = new PrintingContext(new DocumentPrinter(), printMode, printQueue);
            if (!this.IsDocumentEmpty)
            {
                this.StartPrintAsync();
            }
            else
            {
                this.CreateDocumentFinished += new EventHandler<EventArgs>(this.LinkBase_CreateDocumentFinished);
                this.CreateDocument(true);
            }
        }

        public void PrintDirect()
        {
            this.PrintDirect((string) null);
        }

        [Obsolete("This method is now obsolete. Use the DevExpress.Xpf.Printing.LinkBase.PrintDirect(string printerName) method instead."), EditorBrowsable(EditorBrowsableState.Never)]
        public void PrintDirect(PrintQueue queue)
        {
            this.PrepareToPrint();
            new DocumentPrinter().PrintDirect(this.CreatePaginator(), PrintHelper.CollectPageData(this.PrintingSystem.Pages.ToArray()), this.PrintingSystem.Document.Name, queue, false);
        }

        public void PrintDirect(string printerName)
        {
            this.PrepareToPrint();
            PrintHelperInternal.PrintDirect(this.PrintingSystem, printerName);
        }

        [Obsolete("This method is now obsolete. Asynchronous printing is no longer supported."), EditorBrowsable(EditorBrowsableState.Never)]
        public void PrintDirectAsync()
        {
            this.PrintAsyncCore(PrintMode.Direct, null);
        }

        [Obsolete("This method is now obsolete. Asynchronous printing is no longer supported."), EditorBrowsable(EditorBrowsableState.Never)]
        public void PrintDirectAsync(PrintQueue queue)
        {
            this.PrintAsyncCore(PrintMode.Direct, queue);
        }

        private void printer_PrintCompleted(object sender, AsyncCompletedEventArgs e)
        {
            this.ActualPrintingContext.DocumentPrinter.PrintCompleted -= new AsyncCompletedEventHandler(this.printer_PrintCompleted);
            this.ActualPrintingContext = null;
            if (this.PrintCompleted != null)
            {
                this.PrintCompleted(this, new AsyncCompletedEventArgs(e.Error, e.Cancelled, e.UserState));
            }
        }

        private void PrintingSystem_AfterBuildPages(object sender, EventArgs e)
        {
            this.AfterBuildPages();
        }

        private void ps_PageSettingsChanged(object sender, EventArgs e)
        {
            if (!this.SuppressAutoRebuildOnPageSettingsChange)
            {
                this.CreateDocument(this.BuildPagesInBackground, false);
            }
        }

        public Window ShowPrintPreview(FrameworkElement owner) => 
            this.ShowPrintPreview(owner, null);

        public Window ShowPrintPreview(FrameworkElement owner, string title) => 
            PrintHelper.ShowPrintPreview(owner, this, title);

        public void ShowPrintPreviewDialog(Window owner)
        {
            this.ShowPrintPreviewDialog(owner, null);
        }

        public void ShowPrintPreviewDialog(Window owner, string title)
        {
            PrintHelper.ShowPrintPreviewDialog(owner, this, title);
        }

        public Window ShowRibbonPrintPreview(FrameworkElement owner) => 
            this.ShowRibbonPrintPreview(owner, null);

        public Window ShowRibbonPrintPreview(FrameworkElement owner, string title) => 
            PrintHelper.ShowRibbonPrintPreview(owner, this, title);

        public void ShowRibbonPrintPreviewDialog(Window owner)
        {
            this.ShowRibbonPrintPreviewDialog(owner, null);
        }

        public void ShowRibbonPrintPreviewDialog(Window owner, string title)
        {
            PrintHelper.ShowRibbonPrintPreviewDialog(owner, this, title);
        }

        private void StartPrintAsync()
        {
            this.ActualPrintingContext.DocumentPrinter.PrintCompleted += new AsyncCompletedEventHandler(this.printer_PrintCompleted);
            this.ActualPrintingContext.Print(this.CreatePaginator(), PrintHelper.CollectPageData(this.PrintingSystem.Pages.ToArray()), this.PrintingSystem.Document.Name, true);
        }

        public void StopPageBuilding()
        {
            if (this.PrintingSystem != null)
            {
                this.PrintingSystem.Document.StopPageBuilding();
            }
        }

        protected internal FrameworkElement VisualizePage(int pageIndex)
        {
            PageVisualizer visualizer = this.CreatePageVisualizer();
            try
            {
                return visualizer.Visualize((PSPage) this.PrintingSystem.Pages[pageIndex], pageIndex, this.PrintingSystem.Pages.Count);
            }
            catch (XamlParseException)
            {
                return new PageWithRedCross();
            }
        }

        private bool IsDocumentEmpty =>
            this.PrintingSystem.Document.PageCount == 0;

        private PrintingContext ActualPrintingContext { get; set; }

        [Description("Gets the Printing System used to create and print a document for this link.")]
        public DevExpress.Xpf.Printing.PrintingSystem PrintingSystem { get; private set; }

        IPrintingSystem ILink.PrintingSystem =>
            this.PrintingSystem;

        PrintingSystemBase IDocumentSource.PrintingSystemBase =>
            this.PrintingSystem;

        [Description("Gets or sets a value indicating whether it is necessary to suppress rebuilding the document every time a link's page settings are changed.")]
        public bool SuppressAutoRebuildOnPageSettingsChange { get; set; }

        [Description("Specifies whether content bricks, which are outside the right page margin, should be split across pages, or moved in their entirety to the next page.")]
        public DevExpress.XtraPrinting.VerticalContentSplitting VerticalContentSplitting { get; set; }

        [Description("Gets or sets a value indicating whether the page orientation is landscape.")]
        public bool Landscape { get; set; }

        [Description("Gets or sets the type of paper for the document.")]
        public System.Drawing.Printing.PaperKind PaperKind { get; set; }

        [Description("Gets or sets a size of custom paper (measured in hundredths of an inch).")]
        public System.Drawing.Size CustomPaperSize { get; set; }

        [Description("Gets or sets the margins of a report page (measured in hundredths of an inch).")]
        public System.Drawing.Printing.Margins Margins { get; set; }

        [Description("Specifies the minimum printer margin values.")]
        public System.Drawing.Printing.Margins MinMargins { get; set; }

        [Description("Gets or sets the name of the document.")]
        public string DocumentName { get; set; }

        internal bool IsPrintingSystemOwner { get; private set; }

        internal bool BuildPagesInBackground { get; private set; }

        private class PrintingContext
        {
            private readonly DevExpress.Xpf.Printing.Native.DocumentPrinter documentPrinter;
            private readonly DevExpress.Xpf.Printing.LinkBase.PrintMode printMode;
            private readonly System.Printing.PrintQueue printQueue;

            public PrintingContext(DevExpress.Xpf.Printing.Native.DocumentPrinter documentPrinter, DevExpress.Xpf.Printing.LinkBase.PrintMode printMode, System.Printing.PrintQueue printQueue)
            {
                Guard.ArgumentNotNull(documentPrinter, "documentPrinter");
                this.documentPrinter = documentPrinter;
                this.printMode = printMode;
                this.printQueue = printQueue;
            }

            public void Print(DocumentPaginator paginator, ReadonlyPageData[] pageData, string documentName, bool asyncMode)
            {
                if (this.PrintMode == DevExpress.Xpf.Printing.LinkBase.PrintMode.Dialog)
                {
                    this.DocumentPrinter.PrintDialog(paginator, pageData, documentName, asyncMode);
                }
                else if (this.PrintQueue == null)
                {
                    this.DocumentPrinter.PrintDirect(paginator, pageData, documentName, asyncMode);
                }
                else
                {
                    this.DocumentPrinter.PrintDirect(paginator, pageData, documentName, this.PrintQueue, asyncMode);
                }
            }

            public DevExpress.Xpf.Printing.Native.DocumentPrinter DocumentPrinter =>
                this.documentPrinter;

            public DevExpress.Xpf.Printing.LinkBase.PrintMode PrintMode =>
                this.printMode;

            public System.Printing.PrintQueue PrintQueue =>
                this.printQueue;
        }

        private enum PrintMode
        {
            Dialog,
            Direct
        }
    }
}

