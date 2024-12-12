namespace DevExpress.Data.WcfLinq.Helpers
{
    using System;
    using System.Collections;
    using System.Linq;

    public class EnumeratorForWrapperDataServiceQuery<TElement> : IEnumerator
    {
        private int position;
        private int count;
        private TElement[] data;

        public EnumeratorForWrapperDataServiceQuery(IQueryable<TElement> source, string rootOrderByQuery, string rootFilterQuery, int count, int rootSkipCount);
        public bool MoveNext();
        public void Reset();

        object IEnumerator.Current { get; }

        public TElement Current { get; }
    }
}

