namespace DevExpress.Printing.StreamingPagination
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;

    public interface IStreamingDocument
    {
        void AddBrickToUpdate(PageInfoTextBrickBase brick, int pageIndex);
        IStreamingPageExporter CreateExporter(IStreamingPageExportProvider provider);
        IEnumerable<Page> EnumeratePages();
        void FinishBuild();
        int GetBuiltBandIndex(DocumentBand band);
        ContinuousExportInfo GetContinuousExportInfo();
        int[] GetPageBandsIndexes(Page page);
        void PrepareDocumentSerialization();
        void PrepareSerialization(ContinuousExportInfo info);
        void PrepareSerialization(Page page);
        void RemovePageBandsIndexes(Page page);

        bool BuildContinuousInfo { get; }

        IStreamingNavigationBuilder NavigationBuilder { get; set; }

        float MaxBrickRight { get; }

        int BuiltPageCount { get; }

        PrintingSystemBase PrintingSystem { get; }

        RootDocumentBand Root { get; }

        bool BookmarkDuplicateSuppress { get; }

        BookmarkNode RootBookmark { get; }

        IBookmarkNodeCollection BookmarkNodes { get; }

        DevExpress.XtraPrinting.Native.NavigationInfo NavigationInfo { get; }

        bool AllowMultiThreading { get; }

        IList<Page> Pages { get; }

        PSUpdatedObjects UpdatedObjects { get; }
    }
}

