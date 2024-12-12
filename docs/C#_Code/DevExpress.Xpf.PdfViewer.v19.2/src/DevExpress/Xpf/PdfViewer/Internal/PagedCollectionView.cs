namespace DevExpress.Xpf.PdfViewer.Internal
{
    using DevExpress.Xpf.Editors.DataPager;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PagedCollectionView : List<int>, IPagedCollectionView
    {
        private readonly IEnumerable<int> enumerable;

        public PagedCollectionView(IEnumerable<int> enumerable) : base(enumerable)
        {
            this.enumerable = enumerable;
        }

        public bool MoveToFirstPage()
        {
            this.PageIndex = this.enumerable.First<int>();
            return true;
        }

        public bool MoveToPage(int pageIndex)
        {
            if (!this.enumerable.Contains<int>(pageIndex))
            {
                return false;
            }
            this.PageIndex = pageIndex;
            return true;
        }

        public bool CanChangePage =>
            true;

        public int ItemCount =>
            this.enumerable.Count<int>();

        public int PageIndex { get; private set; }

        public int PageSize { get; set; }

        public int TotalItemCount =>
            this.ItemCount;
    }
}

