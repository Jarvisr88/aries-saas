namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    internal class PageDataService : IPageDataService, IEnumerable
    {
        private Dictionary<PageDataService.RefKey, CustomPageData> sources;

        public PageDataService();
        public virtual void Clear();
        public virtual CustomPageData GetSource(ReadonlyPageData pageData);
        public virtual void SetSource(ReadonlyPageData pageData, CustomPageData source);
        IEnumerator IEnumerable.GetEnumerator();

        private class RefKey
        {
            private object obj;

            public RefKey(object obj);
            public override bool Equals(object obj);
            public override int GetHashCode();
        }
    }
}

