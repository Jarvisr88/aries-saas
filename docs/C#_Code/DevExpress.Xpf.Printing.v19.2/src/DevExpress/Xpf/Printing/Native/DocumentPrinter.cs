namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Printing;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Printing;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Threading;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Documents;
    using System.Windows.Documents.Serialization;
    using System.Windows.Threading;
    using System.Windows.Xps;
    using System.Windows.Xps.Serialization;

    internal class DocumentPrinter
    {
        private XpsDocumentWriter writer;
        private PrintTicket documentPrintTicket;
        private ReadonlyPageData[] pageData;
        private int startPageIndex;

        public event AsyncCompletedEventHandler PrintCompleted;

        [SecuritySafeCritical]
        private static void ApplyPageDataToTicket(PrintTicket ticket, ReadonlyPageData pageData)
        {
            Size pageSize = GetPageSize(pageData);
            ticket.PageMediaSize = new PageMediaSize(DrawingConverter.FromPaperKind(pageData.PaperKind), pageSize.Width, pageSize.Height);
            ticket.PageOrientation = new System.Printing.PageOrientation?(pageData.Landscape ? System.Printing.PageOrientation.Landscape : System.Printing.PageOrientation.Portrait);
        }

        public void CancelPrint()
        {
            if (this.writer != null)
            {
                this.writer.CancelAsync();
            }
        }

        private static PrintQueue GetDefaultPrintQueue()
        {
            using (PrintQueueCollection queues = new LocalPrintServer().GetPrintQueues())
            {
                if (queues.Count<PrintQueue>() == 0)
                {
                    throw new NotSupportedException(PrintingLocalizer.GetString(PrintingStringId.Exception_NoPrinterFound));
                }
            }
            return LocalPrintServer.GetDefaultPrintQueue();
        }

        internal static string GetJobDescriptionOrDefault(string jobDescription) => 
            jobDescription ?? PrintingLocalizer.GetString(PrintingStringId.DefaultPrintJobDescription);

        private static int[] GetPageIndexes(PageRange pageRange)
        {
            List<int> list = new List<int>();
            for (int i = pageRange.PageFrom; i <= pageRange.PageTo; i++)
            {
                list.Add(i - 1);
            }
            return list.ToArray();
        }

        internal static Size GetPageSize(ReadonlyPageData pageData)
        {
            Size size = new Size((double) GraphicsUnitConverter.Convert(pageData.Bounds.Width, 100f, 96f), (double) GraphicsUnitConverter.Convert(pageData.Bounds.Height, 100f, 96f));
            if (pageData.Landscape)
            {
                size.Width = size.Height;
                size.Height = size.Width;
            }
            return size;
        }

        private void OnAsyncPrintJobCompleted(Exception error, bool cancelled, object userState)
        {
            this.OnPrintJobCompleted();
            if (this.PrintCompleted != null)
            {
                this.PrintCompleted(this, new AsyncCompletedEventArgs(error, cancelled, userState));
            }
        }

        private void OnPrintJobCompleted()
        {
            if (this.writer != null)
            {
                this.writer.WritingCompleted -= new WritingCompletedEventHandler(this.writer_WritingCompleted);
                this.writer.WritingCancelled -= new WritingCancelledEventHandler(this.writer_WritingCancelled);
                this.writer.WritingProgressChanged -= new WritingProgressChangedEventHandler(this.writer_WritingProgressChanged);
                this.writer.WritingPrintTicketRequired -= new WritingPrintTicketRequiredEventHandler(this.writer_WritingPrintTicketRequired);
                this.writer = null;
            }
        }

        private void Print(DocumentPaginator paginator, XpsDocumentWriter writer, bool asyncMode)
        {
            this.writer = writer;
            writer.WritingCompleted += new WritingCompletedEventHandler(this.writer_WritingCompleted);
            writer.WritingCancelled += new WritingCancelledEventHandler(this.writer_WritingCancelled);
            writer.WritingProgressChanged += new WritingProgressChangedEventHandler(this.writer_WritingProgressChanged);
            if (PrintHelper.UsePrintTickets)
            {
                writer.WritingPrintTicketRequired += new WritingPrintTicketRequiredEventHandler(this.writer_WritingPrintTicketRequired);
            }
            if (asyncMode)
            {
                try
                {
                    writer.WriteAsync(paginator);
                }
                catch (PrintSystemException exception)
                {
                    this.OnAsyncPrintJobCompleted(exception, false, null);
                }
            }
            else
            {
                try
                {
                    writer.Write(paginator);
                }
                finally
                {
                    this.OnPrintJobCompleted();
                }
            }
        }

        public bool PrintDialog(DocumentPaginator paginator, ReadonlyPageData[] pageData, string jobDescription, bool asyncMode)
        {
            Guard.ArgumentNotNull(paginator, "paginator");
            Guard.ArgumentNotNull(pageData, "pageData");
            if (paginator.PageCount == 0)
            {
                throw new InvalidOperationException("A document must contain at least one page to be printed.");
            }
            if (this.Active)
            {
                throw new InvalidOperationException("Can't start printing while another printing job is in progress");
            }
            if (pageData.Length == 0)
            {
                throw new ArgumentException("pageData can not be empty");
            }
            XpsDocumentWriter writer = null;
            PageRange pageRange = new PageRange();
            PageRangeSelection allPages = PageRangeSelection.AllPages;
            this.pageData = pageData;
            writer = this.ShowPrintDialog(pageData[0], jobDescription, ref allPages, ref pageRange, (uint) paginator.PageCount);
            if (writer == null)
            {
                this.OnAsyncPrintJobCompleted(null, true, false);
                return false;
            }
            DocumentPaginator paginator2 = (allPages == PageRangeSelection.AllPages) ? paginator : new PageRangeCustomPaginator(paginator, GetPageIndexes(pageRange));
            this.startPageIndex = (allPages == PageRangeSelection.AllPages) ? 0 : (pageRange.PageFrom - 1);
            this.Print(paginator2, writer, asyncMode);
            return true;
        }

        public void PrintDirect(DocumentPaginator paginator, ReadonlyPageData[] pageData, string jobDescription, bool asyncMode)
        {
            this.PrintDirect(paginator, pageData, jobDescription, GetDefaultPrintQueue(), asyncMode);
        }

        [SecuritySafeCritical]
        public void PrintDirect(DocumentPaginator paginator, ReadonlyPageData[] pageData, string jobDescription, PrintQueue queue, bool asyncMode)
        {
            Guard.ArgumentNotNull(paginator, "paginator");
            Guard.ArgumentNotNull(pageData, "pageData");
            Guard.ArgumentNotNull(queue, "queue");
            if (paginator.PageCount == 0)
            {
                throw new InvalidOperationException("A document must contain at least one page to be printed.");
            }
            if (pageData.Length == 0)
            {
                throw new ArgumentException("pageData can not be empty");
            }
            if (this.Active)
            {
                throw new InvalidOperationException("Can't start printing while another printing job is in progress");
            }
            this.documentPrintTicket = queue.UserPrintTicket;
            this.pageData = pageData;
            queue.CurrentJobSettings.Description = GetJobDescriptionOrDefault(jobDescription);
            if (PrintHelper.UsePrintTickets)
            {
                ApplyPageDataToTicket(queue.UserPrintTicket, pageData[0]);
            }
            XpsDocumentWriter writer = PrintQueue.CreateXpsDocumentWriter(queue);
            this.Print(paginator, writer, asyncMode);
        }

        [SecuritySafeCritical]
        private XpsDocumentWriter ShowPrintDialog(ReadonlyPageData pageData, string jobDescription, ref PageRangeSelection pageRangeSelection, ref PageRange pageRange, uint maxPage)
        {
            System.Windows.Controls.PrintDialog dialog = new System.Windows.Controls.PrintDialog {
                UserPageRangeEnabled = true,
                MaxPage = maxPage
            };
            if (PrintHelper.UsePrintTickets)
            {
                ApplyPageDataToTicket(dialog.PrintTicket, pageData);
            }
            bool? nullable = dialog.ShowDialog();
            bool flag = false;
            if ((nullable.GetValueOrDefault() == flag) ? (nullable != null) : false)
            {
                return null;
            }
            dialog.PrintQueue.CurrentJobSettings.Description = GetJobDescriptionOrDefault(jobDescription);
            this.documentPrintTicket = dialog.PrintTicket;
            pageRangeSelection = dialog.PageRangeSelection;
            pageRange = dialog.PageRange;
            return PrintQueue.CreateXpsDocumentWriter(dialog.PrintQueue);
        }

        private void writer_WritingCancelled(object sender, WritingCancelledEventArgs e)
        {
            this.OnPrintJobCompleted();
        }

        private void writer_WritingCompleted(object sender, WritingCompletedEventArgs e)
        {
            this.OnAsyncPrintJobCompleted(e.Error, e.Cancelled, e.UserState);
        }

        [SecuritySafeCritical]
        private void writer_WritingPrintTicketRequired(object sender, WritingPrintTicketRequiredEventArgs e)
        {
            if (e.CurrentPrintTicketLevel == PrintTicketLevel.FixedDocumentSequencePrintTicket)
            {
                if (this.documentPrintTicket != null)
                {
                    e.CurrentPrintTicket = this.documentPrintTicket;
                }
            }
            else if (e.CurrentPrintTicketLevel == PrintTicketLevel.FixedPagePrintTicket)
            {
                PrintTicket ticket = new PrintTicket();
                ApplyPageDataToTicket(ticket, (this.pageData.Length > 1) ? this.pageData[(e.Sequence - 1) + this.startPageIndex] : this.pageData[0]);
                e.CurrentPrintTicket = ticket;
            }
        }

        private void writer_WritingProgressChanged(object sender, WritingProgressChangedEventArgs e)
        {
            TimeSpan timeout = new TimeSpan(0x186a0L);
            ThreadStart method = <>c.<>9__21_0;
            if (<>c.<>9__21_0 == null)
            {
                ThreadStart local1 = <>c.<>9__21_0;
                method = <>c.<>9__21_0 = delegate {
                };
            }
            Dispatcher.CurrentDispatcher.Invoke(method, timeout, DispatcherPriority.ApplicationIdle, new object[0]);
        }

        public bool Active =>
            this.writer != null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DocumentPrinter.<>c <>9 = new DocumentPrinter.<>c();
            public static ThreadStart <>9__21_0;

            internal void <writer_WritingProgressChanged>b__21_0()
            {
            }
        }
    }
}

