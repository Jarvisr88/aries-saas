namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;

    public interface IStreamingPageExporter : IDisposable
    {
        void Export(Page page);
        void Finish(IList<int> pageIndexes);
    }
}

