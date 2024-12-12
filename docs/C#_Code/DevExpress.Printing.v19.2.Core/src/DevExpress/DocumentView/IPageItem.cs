namespace DevExpress.DocumentView
{
    using System;

    public interface IPageItem
    {
        int Index { get; }

        int OriginalIndex { get; }

        int PageCount { get; }

        int OriginalPageCount { get; }
    }
}

