﻿namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    internal class RealTimeSourceDesignTimeWrapper : IBindingList, IList, ICollection, IEnumerable, ITypedList
    {
        private object dataSource;
        private string _DisplayableProperties;
        private PropertyDescriptorCollection _Descriptors;

        public event ListChangedEventHandler ListChanged;

        public RealTimeSourceDesignTimeWrapper(object source, string dispProps);
        private PropertyDescriptorCollection GetDescriptors();
        private PropertyDescriptorCollection GetDescriptorsCore();
        internal static RealTimePropertyDescriptor GetMessagingDescriptorIfUnsafe(string name, PropertyDescriptor prototype);
        private void InvalidateDescriptors();
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

        [RefreshProperties(RefreshProperties.All)]
        public string DisplayableProperties { get; set; }

        public object DataSource { get; set; }

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
    }
}

