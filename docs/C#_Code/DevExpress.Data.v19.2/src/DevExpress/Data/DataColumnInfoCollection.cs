namespace DevExpress.Data
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;

    public class DataColumnInfoCollection : CollectionBase, IList<DataColumnInfo>, ICollection<DataColumnInfo>, IEnumerable<DataColumnInfo>, IEnumerable
    {
        private Dictionary<string, DataColumnInfo> nameHash;

        public DataColumnInfoCollection();
        public void Add(DataColumnInfo columnInfo);
        public DataColumnInfo Add(PropertyDescriptor descriptor);
        public Dictionary<DataColumnInfo, object> ConvertValues(Dictionary<string, object> columnValues);
        public int GetColumnIndex(string fieldName);
        protected override void OnClearComplete();
        protected override void OnInsertComplete(int position, object item);
        protected override void OnRemoveComplete(int position, object item);
        void ICollection<DataColumnInfo>.Add(DataColumnInfo item);
        bool ICollection<DataColumnInfo>.Contains(DataColumnInfo item);
        void ICollection<DataColumnInfo>.CopyTo(DataColumnInfo[] array, int arrayIndex);
        bool ICollection<DataColumnInfo>.Remove(DataColumnInfo item);
        IEnumerator<DataColumnInfo> IEnumerable<DataColumnInfo>.GetEnumerator();
        int IList<DataColumnInfo>.IndexOf(DataColumnInfo item);
        void IList<DataColumnInfo>.Insert(int index, DataColumnInfo item);
        void IList<DataColumnInfo>.RemoveAt(int index);
        IEnumerator IEnumerable.GetEnumerator();
        public DataColumnInfo[] ToArray();
        private void UpdateColumnIndexes();
        internal void ValidateColumnInfo(DataColumnInfo columnInfo);

        public bool HasUnboundColumns { get; }

        public DataColumnInfo this[int index] { get; }

        public DataColumnInfo this[string name] { get; }

        protected Dictionary<string, DataColumnInfo> NameHash { get; }

        DataColumnInfo IList<DataColumnInfo>.this[int index] { get; set; }

        bool ICollection<DataColumnInfo>.IsReadOnly { get; }

        private class TypedEnumerator : IEnumerator<DataColumnInfo>, IDisposable, IEnumerator
        {
            private IEnumerator enumerator;

            public TypedEnumerator(IEnumerator enumerator);
            public void Dispose();
            public bool MoveNext();
            public void Reset();

            public DataColumnInfo Current { get; }

            object IEnumerator.Current { get; }
        }
    }
}

