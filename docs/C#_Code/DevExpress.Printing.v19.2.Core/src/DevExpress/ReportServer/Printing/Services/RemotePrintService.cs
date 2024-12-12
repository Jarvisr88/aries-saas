namespace DevExpress.ReportServer.Printing.Services
{
    using System;
    using System.Collections.Generic;
    using System.Drawing.Printing;

    internal class RemotePrintService : IRemotePrintService
    {
        private readonly IPageListService pageListService;
        private Action printAction;
        private readonly Action<int[]> invalidatePagesAction;

        public RemotePrintService(IPageListService pageListService, Action<int[]> invalidatePagesAction)
        {
            this.pageListService = pageListService;
            this.invalidatePagesAction = invalidatePagesAction;
        }

        private static int[] CreatePageIndexes(int from, int to)
        {
            List<int> list = new List<int>();
            for (int i = from; i <= to; i++)
            {
                list.Add(i);
            }
            return list.ToArray();
        }

        public void Print(Action<PrintDocument> printAction, Func<PrintDocument> runDialogAction)
        {
            PrintDocument printDocument = runDialogAction();
            if (printDocument != null)
            {
                this.printAction = delegate {
                    printAction(printDocument);
                    printDocument.Dispose();
                };
                this.PrintPages(printDocument.PrinterSettings.FromPage - 1, printDocument.PrinterSettings.ToPage - 1);
            }
        }

        public void PrintDirect(int fromIndex, int toIndex, Action<string> printDirectAction)
        {
            this.printAction = () => printDirectAction(string.Empty);
            this.PrintPages(fromIndex, toIndex);
        }

        private void PrintPages(int fromIndex, int toIndex)
        {
            int[] indexes = CreatePageIndexes(fromIndex, toIndex);
            if (this.pageListService.PagesShouldBeLoaded(indexes))
            {
                this.pageListService.Ensure(indexes, delegate (int[] result) {
                    this.invalidatePagesAction(indexes);
                    this.printAction();
                });
            }
            else
            {
                this.printAction();
            }
        }
    }
}

