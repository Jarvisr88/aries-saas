namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;

    public interface IStreamingPageExportProvider : IDisposable
    {
        void Export(Page page);
        void FinalizeBuildBookmarks(IList<int> pageIndexes);
        void FinalizeBuildDocument();

        bool AllowCleanupBands { get; }
    }
}

