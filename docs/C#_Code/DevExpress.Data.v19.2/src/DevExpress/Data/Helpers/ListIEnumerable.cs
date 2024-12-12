namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class ListIEnumerable : ListAdapterBase<IEnumerable>, IList, ICollection, IEnumerable
    {
        private List<object> list;

        public ListIEnumerable(IEnumerable source);
        internal bool IsEmpty();
        void ICollection.CopyTo(Array array, int index);
        int IList.IndexOf(object value);

        object IList.this[int index] { get; set; }

        int ICollection.Count { get; }

        bool ICollection.IsSynchronized { get; }

        object ICollection.SyncRoot { get; }
    }
}

