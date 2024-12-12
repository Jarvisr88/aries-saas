namespace DevExpress.Data.Helpers
{
    using DevExpress.Utils.Filtering.Internal;
    using System;
    using System.Collections;

    public class ListIGroupValues : ListAdapterBase<IGroupValues>, IList, ICollection, IEnumerable
    {
        public ListIGroupValues(IGroupValues source);
        void ICollection.CopyTo(Array array, int index);
        int IList.IndexOf(object value);

        object IList.this[int index] { get; set; }

        int ICollection.Count { get; }

        bool ICollection.IsSynchronized { get; }

        object ICollection.SyncRoot { get; }
    }
}

