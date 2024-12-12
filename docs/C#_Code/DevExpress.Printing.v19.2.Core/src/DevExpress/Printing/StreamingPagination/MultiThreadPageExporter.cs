namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.Printing.Native.StreamingPagination;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Threading;

    internal class MultiThreadPageExporter : StreamingPageExporter
    {
        private readonly BucketQueue<Page> queue;
        private readonly Thread thread;

        public MultiThreadPageExporter(IStreamingPageExportProvider exportProvider, IStreamingNavigationBuilder navBuilder, IStreamingDocument document, PageBuildEngine engine) : base(exportProvider, navBuilder, document, engine)
        {
            this.queue = new BucketQueue<Page>(1, 20, 2);
            this.thread = new Thread(new ThreadStart(this.Work));
            this.thread.IsBackground = true;
            this.thread.Start();
        }

        public override void Export(Page page)
        {
            this.queue.Enqueue(page);
            if (base.AllowCleanupBands)
            {
                base.document.CleanupDocument(base.Engine, page, false);
            }
        }

        public override void Finish(IList<int> pageIndexes)
        {
            this.queue.Complete();
            this.thread.Join();
            base.Finish(pageIndexes);
        }

        protected override void OnPageExported(Page page)
        {
            page.ClearContent();
        }

        private void Work()
        {
            foreach (Page page in this.queue.DequeueItems<Page>())
            {
                base.Export(page);
            }
        }
    }
}

