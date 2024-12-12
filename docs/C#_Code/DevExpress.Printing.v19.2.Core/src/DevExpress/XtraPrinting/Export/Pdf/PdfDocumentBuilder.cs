namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Printing.Core.NativePdfExport;
    using DevExpress.Printing.Native;
    using DevExpress.Printing.StreamingPagination;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.IO;
    using System.Linq;

    public static class PdfDocumentBuilder
    {
        public static void CreateDocument(Stream stream, IStreamingDocument document, PdfExportOptions pdfOptions)
        {
            ValidateOptions(pdfOptions);
            PrintingSystemBase printingSystem = document.PrintingSystem;
            PdfExportProvider provider = new PdfExportProvider(stream, document, pdfOptions, printingSystem);
            using (IStreamingPageExporter exporter = document.CreateExporter(provider))
            {
                try
                {
                    PageIndexValidator validator = new PageIndexValidator(pdfOptions.PageRange);
                    foreach (Page page in document.EnumeratePages())
                    {
                        if (printingSystem.CancelPending)
                        {
                            break;
                        }
                        if (!page.IsFake && validator.ValidateIndex(page.Index))
                        {
                            exporter.Export(page);
                        }
                    }
                    exporter.Finish(validator.ValidatedIndexes);
                }
                finally
                {
                    provider.Flush();
                    printingSystem.ProgressReflector.MaximizeRange();
                }
            }
        }

        public static void CreateDocument(Stream stream, Document document, PdfExportOptions pdfOptions)
        {
            CreateDocument(stream, document, pdfOptions, false);
        }

        public static void CreateDocument(Stream stream, Document document, PdfExportOptions pdfOptions, bool flushPageContent)
        {
            ValidateOptions(pdfOptions);
            PrintingSystemBase printingSystem = document.PrintingSystem;
            int[] pageIndices = ExportOptionsHelper.GetPageIndices(pdfOptions, document.PageCount);
            using (IPdfGraphics graphics = GetPdfGraphics(stream, pdfOptions, printingSystem, new PageRangeIndexMapper(pdfOptions, pageIndices)))
            {
                if (flushPageContent)
                {
                    graphics.FlushPageContent();
                }
                try
                {
                    printingSystem.ProgressReflector.InitializeRange(pageIndices.Length + 3);
                    int index = 0;
                    while (true)
                    {
                        if ((index >= pageIndices.Length) || printingSystem.CancelPending)
                        {
                            if ((pageIndices.Length != 0) && (document.BookmarkNodes.Count > 0))
                            {
                                graphics.AddOutlineEntries(document.RootBookmark, pageIndices);
                            }
                            break;
                        }
                        ExportPage(printingSystem, graphics, document.Pages[pageIndices[index]]);
                        index++;
                    }
                }
                finally
                {
                    graphics.Flush();
                    printingSystem.ProgressReflector.MaximizeRange();
                }
            }
        }

        private static void ExportPage(PrintingSystemBase ps, IPdfGraphics gr, Page page)
        {
            gr.AddPage(page.PageSize, page.Index);
            ((PageExporter) ps.ExportersFactory.GetExporter(page)).DrawPage(gr, PointF.Empty);
        }

        private static IPdfGraphics GetPdfGraphics(Stream stream, PdfExportOptions pdfOptions, PrintingSystemBase ps, PageRangeIndexMapper pageIndexMapper) => 
            PrintingSettings.UseNewPdfExport ? ((IPdfGraphics) new DevExpress.Printing.Core.NativePdfExport.PdfGraphics(stream, pdfOptions, ps, pageIndexMapper)) : ((IPdfGraphics) new DevExpress.XtraPrinting.Export.Pdf.PdfGraphics(stream, pdfOptions, ps, pageIndexMapper));

        private static void ValidateOptions(PdfExportOptions pdfOptions)
        {
            IList<string> validationErrors = pdfOptions.Validate();
            if (validationErrors.Count > 0)
            {
                throw new PdfExportException(validationErrors);
            }
        }

        private class PdfExportProvider : IStreamingPageExportProvider, IDisposable
        {
            private Stream stream;
            private PdfExportOptions pdfOptions;
            private PrintingSystemBase ps;
            private IStreamingDocument document;
            private IPdfGraphics gr;
            private DeferredPageRangeIndexMapper pageIndexMapper;

            public PdfExportProvider(Stream stream, IStreamingDocument document, PdfExportOptions pdfOptions, PrintingSystemBase ps)
            {
                this.stream = stream;
                this.pdfOptions = pdfOptions;
                this.document = document;
                this.ps = ps;
                this.pageIndexMapper = new DeferredPageRangeIndexMapper(pdfOptions);
            }

            public void Dispose()
            {
                if (this.gr != null)
                {
                    this.gr.Dispose();
                }
                this.gr = null;
            }

            public void Export(Page page)
            {
                this.PdfGraphics.FlushPageContent();
                PdfDocumentBuilder.ExportPage(this.ps, this.PdfGraphics, page);
            }

            public void FinalizeBuildBookmarks(IList<int> pageIndexes)
            {
                this.pageIndexMapper.SetPageIndices(pageIndexes.ToArray<int>());
                if ((pageIndexes.Count > 0) && (this.document.BookmarkNodes.Count > 0))
                {
                    this.gr.AddOutlineEntries(this.document.RootBookmark, pageIndexes.ToArray<int>());
                }
            }

            public void FinalizeBuildDocument()
            {
                this.document.FinishBuild();
            }

            public void Flush()
            {
                this.PdfGraphics.Flush();
            }

            protected IPdfGraphics PdfGraphics
            {
                get
                {
                    if (this.gr == null)
                    {
                        IPdfGraphics graphics1 = PrintingSettings.UseNewPdfExport ? ((IPdfGraphics) new PdfDeferredGraphics(this.stream, this.pdfOptions, this.ps, this.pageIndexMapper)) : ((IPdfGraphics) new DevExpress.XtraPrinting.Export.Pdf.PdfGraphics(this.stream, this.pdfOptions, this.ps, this.pageIndexMapper));
                        this.gr = graphics1;
                    }
                    return this.gr;
                }
            }

            public bool AllowCleanupBands =>
                true;
        }
    }
}

