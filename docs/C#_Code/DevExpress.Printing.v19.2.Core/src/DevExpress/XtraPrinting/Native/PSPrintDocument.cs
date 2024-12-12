namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing.Native;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Printing;

    public class PSPrintDocument : PrintDocument, IPrintDocumentExtension
    {
        private Queue<int> pageIndices = new Queue<int>();
        private Document doc;
        private Color backColor;
        private PrintingSystemBase ps;
        private Dictionary<ReadonlyPageData, PaperData> paperData = new Dictionary<ReadonlyPageData, PaperData>();
        private Func<bool> predicateMargins;
        private string pageRange = string.Empty;

        public PSPrintDocument(PrintingSystemBase ps, Color backColor, PrintController printController, PrinterSettings printerSettings, Func<bool> predicateMargins)
        {
            this.ps = ps;
            this.doc = ps.Document;
            this.backColor = backColor;
            base.DocumentName = ps.Document.Name;
            base.PrinterSettings = printerSettings;
            base.DefaultPageSettings = (PageSettings) base.PrinterSettings.DefaultPageSettings.Clone();
            PageSettingsHelper.ResetUserSetPageSettings(this);
            if (!string.IsNullOrEmpty(ps.PageSettings.PrinterName))
            {
                PageSettingsHelper.SetPrinterName(base.PrinterSettings, ps.PageSettings.PrinterName);
            }
            if (printController != null)
            {
                base.PrintController = printController;
            }
            this.predicateMargins = predicateMargins;
        }

        private Margins EnsureMinMargins(Graphics graph)
        {
            Page currentPage = this.CurrentPage;
            ReadonlyPageData pageData = currentPage.PageData;
            Margins minMargins = DeviceCaps.GetMinMargins(graph);
            if (!this.paperData.ContainsKey(pageData) || (this.paperData[pageData].MinMargins != null))
            {
                return minMargins;
            }
            this.paperData[pageData].MinMargins = minMargins;
            return ((DeviceCaps.CompareMargins(currentPage.Margins, minMargins) <= 0) ? (((this.predicateMargins == null) || !this.predicateMargins()) ? null : minMargins) : minMargins);
        }

        protected override void OnBeginPrint(PrintEventArgs e)
        {
            base.OnBeginPrint(e);
            this.paperData.Clear();
            if (PageSettingsHelper.PrinterExists)
            {
                foreach (Page page in this.ps.Document.Pages)
                {
                    ReadonlyPageData pageData = page.PageData;
                    if (!this.paperData.ContainsKey(pageData))
                    {
                        this.paperData.Add(pageData, new PaperData(PageSizeInfo.GetPaperSize(pageData.PaperKind, pageData.Size, base.PrinterSettings.PaperSizes)));
                    }
                }
            }
            PrintRange printRange = base.PrinterSettings.PrintRange;
            if ((printRange != PrintRange.AllPages) && ((printRange != PrintRange.SomePages) && (printRange != PrintRange.CurrentPage)))
            {
                this.pageIndices = new Queue<int>(PageRangeParser.GetIndices(string.Empty, this.ps.Pages.Count));
            }
            else
            {
                this.pageIndices = new Queue<int>(PageRangeParser.GetIndices(this.PageRange, this.ps.Pages.Count));
            }
        }

        protected override void OnPrintPage(PrintPageEventArgs e)
        {
            base.OnPrintPage(e);
            Graphics graph = e.Graphics;
            if ((graph == null) || (this.PageIndex < 0))
            {
                e.HasMorePages = false;
            }
            else
            {
                Margins margins = this.EnsureMinMargins(graph);
                if (margins == null)
                {
                    e.Cancel = true;
                }
                else
                {
                    MarginsF sf = new MarginsF(margins);
                    using (GdiGraphics graphics2 = new GdiGraphics(graph, this.ps))
                    {
                        PageExporter exporter = this.ps.ExportersFactory.GetExporter(this.CurrentPage) as PageExporter;
                        try
                        {
                            exporter.IsPrinting = true;
                            exporter.DrawPage(graphics2, new PointF(-sf.Left, -sf.Top));
                        }
                        finally
                        {
                            exporter.IsPrinting = false;
                        }
                    }
                    this.pageIndices.Dequeue();
                    e.HasMorePages = this.PageIndex >= 0;
                }
            }
        }

        protected override void OnQueryPageSettings(QueryPageSettingsEventArgs e)
        {
            ReadonlyPageData pageData = this.CurrentPage.PageData;
            PaperSize paperSize = this.paperData.ContainsKey(pageData) ? this.paperData[pageData].PaperSize : new PaperSize(pageData.PaperName, pageData.Size.Width, pageData.Size.Height);
            PageSettingsHelper.ChangePageSettings(e.PageSettings, paperSize, pageData);
            this.ps.OnPrintProgress(new PrintProgressEventArgs(e, this.PageIndex));
            base.OnQueryPageSettings(e);
        }

        private int PageIndex =>
            (this.pageIndices.Count > 0) ? this.pageIndices.Peek() : -1;

        private Page CurrentPage =>
            this.doc.Pages[this.PageIndex];

        public string PageRange
        {
            get => 
                this.pageRange;
            set => 
                this.pageRange = value;
        }

        private class PaperData
        {
            private System.Drawing.Printing.PaperSize paperSize;
            private Margins minMargins;

            public PaperData(System.Drawing.Printing.PaperSize paperSize)
            {
                this.paperSize = paperSize;
            }

            public System.Drawing.Printing.PaperSize PaperSize =>
                this.paperSize;

            public Margins MinMargins
            {
                get => 
                    this.minMargins;
                set => 
                    this.minMargins = value;
            }
        }
    }
}

