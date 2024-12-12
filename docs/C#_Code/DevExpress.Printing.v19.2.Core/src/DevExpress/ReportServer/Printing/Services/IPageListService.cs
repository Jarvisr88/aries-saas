namespace DevExpress.ReportServer.Printing.Services
{
    using DevExpress.ReportServer.IndexedCache;
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;

    public interface IPageListService : ICache<Page>, IDisposable, IEnumerable
    {
        event ExceptionEventHandler RequestPagesException;

        bool PagesShouldBeLoaded(params int[] pageIndexes);
    }
}

