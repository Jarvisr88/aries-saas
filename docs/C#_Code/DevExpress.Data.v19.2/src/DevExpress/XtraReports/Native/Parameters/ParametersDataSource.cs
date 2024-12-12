namespace DevExpress.XtraReports.Native.Parameters
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class ParametersDataSource : IList, ICollection, IEnumerable, ITypedList
    {
        private IEnumerable<IParameter> parameters;
        private List<object> innerList;
        private string listName;

        public ParametersDataSource(IEnumerable<IParameter> parameters, string listName);
        void ICollection.CopyTo(Array array, int index);
        IEnumerator IEnumerable.GetEnumerator();
        int IList.Add(object value);
        void IList.Clear();
        bool IList.Contains(object value);
        int IList.IndexOf(object value);
        void IList.Insert(int index, object value);
        void IList.Remove(object value);
        void IList.RemoveAt(int index);
        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors);
        string ITypedList.GetListName(PropertyDescriptor[] listAccessors);

        public IEnumerable<IParameter> Parameters { get; }

        bool IList.IsFixedSize { get; }

        bool IList.IsReadOnly { get; }

        object IList.this[int index] { get; set; }

        int ICollection.Count { get; }

        bool ICollection.IsSynchronized { get; }

        object ICollection.SyncRoot { get; }
    }
}

