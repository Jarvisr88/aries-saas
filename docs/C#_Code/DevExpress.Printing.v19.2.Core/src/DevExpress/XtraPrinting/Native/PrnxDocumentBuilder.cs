namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.Printing.StreamingPagination;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Caching;
    using System;
    using System.Linq;

    public class PrnxDocumentBuilder
    {
        public static void CreateDocument(DocumentStorage storage, IStreamingDocument document, bool buildContinuousInfo)
        {
            CreateDocument(storage, document, buildContinuousInfo, null);
        }

        internal static void CreateDocument(DocumentStorage storage, IStreamingDocument document, bool buildContinuousInfo, PrnxExportProvider prnxExportProvider)
        {
            PrintingSystemBase printingSystem = document.PrintingSystem;
            PrnxExportProvider provider = prnxExportProvider ?? new PrnxExportProvider(storage, document, printingSystem, buildContinuousInfo);
            using (IStreamingPageExporter exporter = document.CreateExporter(provider))
            {
                try
                {
                    foreach (Page page in document.EnumeratePages())
                    {
                        if (!page.IsFake)
                        {
                            exporter.Export(page);
                        }
                    }
                    exporter.Finish(Enumerable.Range(0, document.BuiltPageCount).ToList<int>());
                }
                finally
                {
                    provider.Flush();
                    printingSystem.ProgressReflector.MaximizeRange();
                }
            }
        }
    }
}

