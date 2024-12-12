namespace DevExpress.Xpf.Editors.DataPager
{
    using System;

    public interface IPagedCollectionView
    {
        bool MoveToFirstPage();
        bool MoveToPage(int pageIndex);

        bool CanChangePage { get; }

        int ItemCount { get; }

        int PageIndex { get; }

        int PageSize { get; set; }

        int TotalItemCount { get; }
    }
}

