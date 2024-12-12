namespace DevExpress.ReportServer.Printing
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class PageListEnumerator : IEnumerator
    {
        private readonly IEnumerator<IndexedCacheItem<RemotePageInfo>> enumerator;
        private readonly IList<IndexedCacheItem<RemotePageInfo>> list;

        public PageListEnumerator(IList<IndexedCacheItem<RemotePageInfo>> list)
        {
            this.list = list;
            this.enumerator = list.GetEnumerator();
        }

        public bool MoveNext() => 
            this.enumerator.MoveNext();

        public void Reset()
        {
            this.enumerator.Reset();
        }

        public object Current =>
            this.enumerator.Current.Value.Page;
    }
}

