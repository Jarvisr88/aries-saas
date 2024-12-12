namespace DevExpress.XtraPrinting.BrickExporters
{
    using DevExpress.DocumentView;
    using DevExpress.XtraPrinting;
    using System;

    internal class DeferredPageItem : IPageItem
    {
        private readonly int index;
        private readonly int pageCount;

        public DeferredPageItem(Document document, long pageId);
        public DeferredPageItem(int pageIndex, int pageCount);

        public int Index { get; }

        public int OriginalIndex { get; }

        public int OriginalPageCount { get; }

        public int PageCount { get; }
    }
}

