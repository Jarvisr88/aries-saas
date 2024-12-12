namespace DevExpress.Data.Linq.Helpers
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    public class AsyncListDesignTimeWrapper : IBindingList, IList, ICollection, IEnumerable, ITypedList
    {
        private Type _ElementType;
        private bool _AreThreadSafe;
        private PropertyDescriptorCollection _Descriptors;

        public event ListChangedEventHandler ListChanged;

        private PropertyDescriptorCollection GetDescriptors();
        void ICollection.CopyTo(Array array, int index);
        IEnumerator IEnumerable.GetEnumerator();
        int IList.Add(object value);
        void IList.Clear();
        bool IList.Contains(object value);
        int IList.IndexOf(object value);
        void IList.Insert(int index, object value);
        void IList.Remove(object value);
        void IList.RemoveAt(int index);
        void IBindingList.AddIndex(PropertyDescriptor property);
        object IBindingList.AddNew();
        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction);
        int IBindingList.Find(PropertyDescriptor property, object key);
        void IBindingList.RemoveIndex(PropertyDescriptor property);
        void IBindingList.RemoveSort();
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors);
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors);

        public Type ElementType { get; set; }

        public bool AreThreadSafe { get; set; }

        bool IBindingList.AllowEdit { get; }

        bool IBindingList.AllowNew { get; }

        bool IBindingList.AllowRemove { get; }

        bool IBindingList.IsSorted { get; }

        ListSortDirection IBindingList.SortDirection { get; }

        PropertyDescriptor IBindingList.SortProperty { get; }

        bool IBindingList.SupportsChangeNotification { get; }

        bool IBindingList.SupportsSearching { get; }

        bool IBindingList.SupportsSorting { get; }

        bool IList.IsFixedSize { get; }

        bool IList.IsReadOnly { get; }

        object IList.this[int index] { get; set; }

        int ICollection.Count { get; }

        bool ICollection.IsSynchronized { get; }

        object ICollection.SyncRoot { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AsyncListDesignTimeWrapper.<>c <>9;
            public static Func<PropertyDescriptor, bool> <>9__9_0;

            static <>c();
            internal bool <GetDescriptors>b__9_0(PropertyDescriptor pd);
        }
    }
}

