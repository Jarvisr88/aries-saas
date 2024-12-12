namespace DevExpress.Xpf.Data.Native
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class PageCache
    {
        private static readonly ExceptionList ExceptionListInstance = new ExceptionList();
        private readonly Dictionary<int, List<object>> pages = new Dictionary<int, List<object>>();

        public void ApplyRows(int startPageIndex, int pageSize, object[] rows, bool hasMorePages, ref bool hasMoreRows)
        {
            int pageCount = rows.Length / pageSize;
            pageCount ??= 1;
            for (int i = 0; i < pageCount; i++)
            {
                this.pages[startPageIndex + i] = new List<object>(rows.Skip<object>((i * pageSize)).Take<object>(pageSize));
            }
            bool hasMoreRowsLocal = hasMoreRows;
            if (this.DiscardActualPages((pageIndex, pageRowCount) => (pageIndex >= (startPageIndex + pageCount)) && (!hasMoreRowsLocal || (pageRowCount == 0))) && (this.MaxPageIndex < (startPageIndex + pageCount)))
            {
                hasMoreRows = false;
            }
            if (!hasMorePages && (this.MaxPageIndex >= (startPageIndex + pageCount)))
            {
                hasMoreRows = false;
            }
            this.DiscardActualPages((pageIndex, pageRowCount) => (pageIndex < startPageIndex) && (pageRowCount < pageSize));
        }

        public void Clear()
        {
            this.pages.Clear();
        }

        public bool ContainsPage(int pageIndex) => 
            this.pages.ContainsKey(pageIndex);

        private bool DiscardActualPages(PageFilterDelegate filter)
        {
            Func<KeyValuePair<int, List<object>>, bool> predicate = <>c.<>9__15_0;
            if (<>c.<>9__15_0 == null)
            {
                Func<KeyValuePair<int, List<object>>, bool> local1 = <>c.<>9__15_0;
                predicate = <>c.<>9__15_0 = x => (x.Value != null) && (x.Value != ExceptionListInstance);
            }
            KeyValuePair<int, List<object>>[] source = (from x in this.pages.Where<KeyValuePair<int, List<object>>>(predicate)
                where filter(x.Key, x.Value.Count)
                select x).ToArray<KeyValuePair<int, List<object>>>();
            if (!source.Any<KeyValuePair<int, List<object>>>())
            {
                return false;
            }
            foreach (KeyValuePair<int, List<object>> pair in source)
            {
                this.RemovePage(pair.Key);
            }
            return true;
        }

        public IList<object> GetPageList(int pageIndex) => 
            this.pages[pageIndex] ?? new List<object>();

        public bool IsPageLoading(int pageIndex) => 
            this.ContainsPage(pageIndex) && (this.pages[pageIndex] == null);

        public void RemovePage(int pageIndex)
        {
            this.pages.Remove(pageIndex);
        }

        public void RemovePageWithException(int pageIndex)
        {
            if (this.ContainsPage(pageIndex) && (this.pages[pageIndex] == ExceptionListInstance))
            {
                this.RemovePage(pageIndex);
            }
        }

        public void SetPageException(int pageIndex)
        {
            this.pages[pageIndex] = ExceptionListInstance;
        }

        public void SetPagePending(int pageIndex)
        {
            this.pages[pageIndex] = null;
        }

        public int MaxPageIndex =>
            this.pages.Keys.Any<int>() ? ((IEnumerable<int>) this.pages.Keys).Max() : -1;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PageCache.<>c <>9 = new PageCache.<>c();
            public static Func<KeyValuePair<int, List<object>>, bool> <>9__15_0;

            internal bool <DiscardActualPages>b__15_0(KeyValuePair<int, List<object>> x) => 
                (x.Value != null) && (x.Value != PageCache.ExceptionListInstance);
        }

        private class ExceptionList : List<object>
        {
        }

        private delegate bool PageFilterDelegate(int pageIndex, int pageRowCount);
    }
}

