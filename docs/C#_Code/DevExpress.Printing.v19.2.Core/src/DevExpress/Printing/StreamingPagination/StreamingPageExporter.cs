namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal class StreamingPageExporter : IStreamingPageExporter, IDisposable
    {
        protected internal IStreamingDocument document;
        private IStreamingPageExportProvider exportProvider;
        private IStreamingNavigationBuilder navBuilder;
        private StreamingDocumentMapBuilder builder;

        public StreamingPageExporter(IStreamingPageExportProvider exportProvider, IStreamingNavigationBuilder navBuilder, IStreamingDocument document, PageBuildEngine engine)
        {
            this.exportProvider = exportProvider;
            this.document = document;
            this.navBuilder = navBuilder;
            this.Engine = engine;
            this.builder = new StreamingDocumentMapBuilder(document);
        }

        public virtual void Dispose()
        {
            if (this.exportProvider != null)
            {
                this.exportProvider.Dispose();
            }
            this.document = null;
            this.builder = null;
        }

        public virtual void Export(Page page)
        {
            this.ExportInternal(page);
        }

        protected void ExportInternal(Page page)
        {
            if (this.document.NavigationInfo != null)
            {
                this.builder.Build(this.document.NavigationInfo.BookmarkBricks, page);
                if (this.navBuilder != null)
                {
                    this.navBuilder.SetNavigationPairs(this.document.NavigationInfo.NavigationBricks, page);
                }
            }
            this.exportProvider.Export(page);
            this.OnPageExported(page);
        }

        public virtual void Finish(IList<int> pageIndexes)
        {
            this.builder.FinalizeBuild();
            if (this.navBuilder != null)
            {
                this.navBuilder.FinalizeBuild();
            }
            this.exportProvider.FinalizeBuildDocument();
            foreach (Page page in this.document.Pages)
            {
                this.ExportInternal(page);
                pageIndexes.Insert(page.Index, -1);
            }
            this.exportProvider.FinalizeBuildBookmarks(pageIndexes);
        }

        protected virtual void OnPageExported(Page page)
        {
            if (this.AllowCleanupBands)
            {
                this.document.CleanupDocument(this.Engine, page, true);
            }
        }

        protected PageBuildEngine Engine { get; private set; }

        protected bool AllowCleanupBands =>
            this.exportProvider.AllowCleanupBands;
    }
}

