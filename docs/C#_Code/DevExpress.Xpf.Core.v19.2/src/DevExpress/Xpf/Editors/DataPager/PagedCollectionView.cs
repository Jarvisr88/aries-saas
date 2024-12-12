namespace DevExpress.Xpf.Editors.DataPager
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class PagedCollectionView : List<object>, IPagedCollectionView
    {
        public PagedCollectionView()
        {
        }

        public PagedCollectionView(IEnumerable source)
        {
        }

        public bool MoveToFirstPage()
        {
            this.PageIndex = 0;
            return true;
        }

        public bool MoveToPage(int pageIndex)
        {
            this.PageIndex = pageIndex;
            return true;
        }

        public void Refresh()
        {
        }

        public bool CanChangePage =>
            true;

        public int ItemCount =>
            0x19;

        public int PageIndex { get; set; }

        public int PageSize { get; set; }

        public int TotalItemCount =>
            100;
    }
}

