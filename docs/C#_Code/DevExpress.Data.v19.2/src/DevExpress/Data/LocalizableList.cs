namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.ComponentModel;

    public abstract class LocalizableList : LocalizableTypedList, IList, ICollection, IEnumerable
    {
        private IList list;

        public LocalizableList(ITypedList typedList, IList list);
        void ICollection.CopyTo(Array array, int index);
        IEnumerator IEnumerable.GetEnumerator();
        int IList.Add(object value);
        void IList.Clear();
        bool IList.Contains(object value);
        int IList.IndexOf(object value);
        void IList.Insert(int index, object value);
        void IList.Remove(object value);
        void IList.RemoveAt(int index);

        bool IList.IsReadOnly { get; }

        bool IList.IsFixedSize { get; }

        int ICollection.Count { get; }

        object ICollection.SyncRoot { get; }

        bool ICollection.IsSynchronized { get; }

        object IList.this[int index] { get; set; }
    }
}

