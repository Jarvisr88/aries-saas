namespace DevExpress.Data.Helpers
{
    using System;
    using System.Collections;

    public class ListIDictionary : ListAdapterBase<IDictionary>, IList, ICollection, IEnumerable
    {
        private object[] array;

        public ListIDictionary(IDictionary source);
        protected void EnsureArray(ref object[] array, int count);
        internal bool IsEmpty();
        void ICollection.CopyTo(Array array, int index);
        int IList.IndexOf(object value);

        object IList.this[int index] { get; set; }

        int ICollection.Count { get; }

        bool ICollection.IsSynchronized { get; }

        object ICollection.SyncRoot { get; }
    }
}

