namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using DevExpress.Utils;
    using System;
    using System.Collections;
    using System.Data;

    internal class DataTableDragDropAdapter : IList, ICollection, IEnumerable
    {
        private readonly DataView dataView;

        public DataTableDragDropAdapter(DataView dataView)
        {
            Guard.ArgumentNotNull(dataView, "dataView");
            this.dataView = dataView;
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.InnerList.CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.InnerList.GetEnumerator();

        int IList.Add(object value) => 
            this.InnerList.Add(value);

        void IList.Clear()
        {
            this.InnerList.Clear();
        }

        bool IList.Contains(object value) => 
            this.InnerList.Contains(value);

        int IList.IndexOf(object value) => 
            this.InnerList.IndexOf(value);

        void IList.Insert(int index, object value)
        {
            if (!(value is DataRow))
            {
                this.InnerList.Insert(index, value);
            }
            else
            {
                DataRow row = (DataRow) value;
                this.dataView.Table.Rows.InsertAt(row, index);
            }
        }

        void IList.Remove(object value)
        {
            this.InnerList.Remove(value);
        }

        void IList.RemoveAt(int index)
        {
            this.InnerList.RemoveAt(index);
        }

        private IList InnerList =>
            this.dataView;

        object IList.this[int index]
        {
            get => 
                this.InnerList[index];
            set => 
                this.InnerList[index] = value;
        }

        bool IList.IsReadOnly =>
            this.InnerList.IsReadOnly;

        bool IList.IsFixedSize =>
            this.InnerList.IsFixedSize;

        int ICollection.Count =>
            this.InnerList.Count;

        object ICollection.SyncRoot =>
            this.InnerList.SyncRoot;

        bool ICollection.IsSynchronized =>
            this.InnerList.IsSynchronized;
    }
}

