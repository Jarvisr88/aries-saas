namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;

    public class PdfObjectList<T> : PdfObject, IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable where T: PdfObject
    {
        private readonly List<T> objectList;

        public PdfObjectList(PdfObjectCollection objects)
        {
            this.objectList = new List<T>();
        }

        public void Add(T item)
        {
            this.objectList.Add(item);
        }

        void ICollection<T>.Clear()
        {
            this.objectList.Clear();
        }

        bool ICollection<T>.Contains(T item) => 
            this.objectList.Contains(item);

        void ICollection<T>.CopyTo(T[] array, int arrayIndex)
        {
            this.objectList.CopyTo(array, arrayIndex);
        }

        bool ICollection<T>.Remove(T item) => 
            this.objectList.Remove(item);

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => 
            this.objectList.GetEnumerator();

        int IList<T>.IndexOf(T item) => 
            this.objectList.IndexOf(item);

        void IList<T>.Insert(int index, T item)
        {
            this.objectList.Insert(index, item);
        }

        void IList<T>.RemoveAt(int index)
        {
            this.objectList.RemoveAt(index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.objectList.GetEnumerator();

        protected internal override object ToWritableObject(PdfObjectCollection objects) => 
            new PdfWritableObjectArray((IEnumerable<PdfObject>) this.objectList, objects);

        public T this[int index]
        {
            get => 
                this.objectList[index];
            set => 
                this.objectList[index] = value;
        }

        public int Count =>
            this.objectList.Count;

        bool ICollection<T>.IsReadOnly =>
            this.objectList.IsReadOnly;
    }
}

