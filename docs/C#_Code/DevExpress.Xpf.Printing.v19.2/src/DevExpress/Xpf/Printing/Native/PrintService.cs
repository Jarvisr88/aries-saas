namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Linq;
    using System.Printing;
    using System.Windows.Documents;

    internal class PrintService : IPrintService
    {
        private PrintQueue GetPrintQueue(string printerName)
        {
            PrintQueue queue2;
            Guard.ArgumentIsNotNullOrEmpty(printerName, "printerName");
            using (PrintServer server = new PrintServer())
            {
                EnumeratedPrintQueueTypes[] enumerationFlag = new EnumeratedPrintQueueTypes[] { EnumeratedPrintQueueTypes.Local | EnumeratedPrintQueueTypes.Connections };
                using (PrintQueueCollection queues = server.GetPrintQueues(enumerationFlag))
                {
                    PrintQueue queue = queues.FirstOrDefault<PrintQueue>(x => string.Compare(printerName, x.FullName, true) == 0);
                    if (queue == null)
                    {
                        throw new ArgumentException($"Print queue '{printerName}' not found.", "printerName");
                    }
                    queue2 = queue;
                }
            }
            return queue2;
        }

        public bool? PrintDialog(DocumentPaginator paginator, ReadonlyPageData[] pageData, string jobDescription, bool asyncMode) => 
            new bool?(new DocumentPrinter().PrintDialog(paginator, pageData, jobDescription, asyncMode));

        public void PrintDirect(DocumentPaginator paginator, ReadonlyPageData[] pageData, string jobDescription, bool asyncMode)
        {
            new DocumentPrinter().PrintDirect(paginator, pageData, jobDescription, asyncMode);
        }

        public void PrintDirect(DocumentPaginator paginator, ReadonlyPageData[] pageData, string jobDescription, PrintQueue printQueue, bool asyncMode)
        {
            new DocumentPrinter().PrintDirect(paginator, pageData, jobDescription, printQueue, asyncMode);
        }

        public void PrintDirect(DocumentPaginator paginator, ReadonlyPageData[] pageData, string jobDescription, string printerName, bool asyncMode)
        {
            this.PrintDirect(paginator, pageData, jobDescription, this.GetPrintQueue(printerName), asyncMode);
        }
    }
}

