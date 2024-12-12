namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal interface IStreamingPageBuildEngine : IPageBuildEngine
    {
        event Action0 AfterBuild;

        IEnumerable<Page> EnumeratePages(IAfterPrintOnPageProcessor processor);
        int[] GetPageBandsIndexes(Page page);
        void OnAfterBuild();
        void RemovePageBandsIndexes(Page page);

        int BuiltPageCount { get; }
    }
}

