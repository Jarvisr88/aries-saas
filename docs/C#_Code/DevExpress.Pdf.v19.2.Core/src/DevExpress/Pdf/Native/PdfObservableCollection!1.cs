namespace DevExpress.Pdf.Native
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class PdfObservableCollection<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable
    {
        private List<T> collection;
        [CompilerGenerated]
        private EventHandler<PdfObservableCollectionEventArgs<T>> ItemAdded;
        [CompilerGenerated]
        private EventHandler<PdfObservableCollectionEventArgs<T>> ItemRemoved;

        public event EventHandler<PdfObservableCollectionEventArgs<T>> ItemAdded
        {
            [CompilerGenerated] add
            {
                EventHandler<PdfObservableCollectionEventArgs<T>> itemAdded = this.ItemAdded;
                while (true)
                {
                    EventHandler<PdfObservableCollectionEventArgs<T>> comparand = itemAdded;
                    EventHandler<PdfObservableCollectionEventArgs<T>> handler3 = comparand + value;
                    itemAdded = Interlocked.CompareExchange<EventHandler<PdfObservableCollectionEventArgs<T>>>(ref this.ItemAdded, handler3, comparand);
                    if (ReferenceEquals(itemAdded, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                EventHandler<PdfObservableCollectionEventArgs<T>> itemAdded = this.ItemAdded;
                while (true)
                {
                    EventHandler<PdfObservableCollectionEventArgs<T>> comparand = itemAdded;
                    EventHandler<PdfObservableCollectionEventArgs<T>> handler3 = comparand - value;
                    itemAdded = Interlocked.CompareExchange<EventHandler<PdfObservableCollectionEventArgs<T>>>(ref this.ItemAdded, handler3, comparand);
                    if (ReferenceEquals(itemAdded, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event EventHandler<PdfObservableCollectionEventArgs<T>> ItemRemoved
        {
            [CompilerGenerated] add
            {
                EventHandler<PdfObservableCollectionEventArgs<T>> itemRemoved = this.ItemRemoved;
                while (true)
                {
                    EventHandler<PdfObservableCollectionEventArgs<T>> comparand = itemRemoved;
                    EventHandler<PdfObservableCollectionEventArgs<T>> handler3 = comparand + value;
                    itemRemoved = Interlocked.CompareExchange<EventHandler<PdfObservableCollectionEventArgs<T>>>(ref this.ItemRemoved, handler3, comparand);
                    if (ReferenceEquals(itemRemoved, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                EventHandler<PdfObservableCollectionEventArgs<T>> itemRemoved = this.ItemRemoved;
                while (true)
                {
                    EventHandler<PdfObservableCollectionEventArgs<T>> comparand = itemRemoved;
                    EventHandler<PdfObservableCollectionEventArgs<T>> handler3 = comparand - value;
                    itemRemoved = Interlocked.CompareExchange<EventHandler<PdfObservableCollectionEventArgs<T>>>(ref this.ItemRemoved, handler3, comparand);
                    if (ReferenceEquals(itemRemoved, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public PdfObservableCollection(List<T> collection)
        {
            this.collection = collection;
        }

        public void Add(T item)
        {
            this.collection.Add(item);
            this.RaiseItemAdded(item);
        }

        public void Clear()
        {
            T[] localArray = this.collection.ToArray();
            this.collection.Clear();
            foreach (T local in localArray)
            {
                this.RaiseItemRemoved(local);
            }
        }

        public bool Contains(T item) => 
            this.collection.Contains(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.collection.CopyTo(array, arrayIndex);
        }

        public IEnumerator<T> GetEnumerator() => 
            this.collection.GetEnumerator();

        public int IndexOf(T item) => 
            this.collection.IndexOf(item);

        public void Insert(int index, T item)
        {
            this.collection.Insert(index, item);
            this.RaiseItemAdded(item);
        }

        private void RaiseItemAdded(T value)
        {
            if (this.ItemAdded != null)
            {
                this.ItemAdded(this, new PdfObservableCollectionEventArgs<T>(value));
            }
        }

        private void RaiseItemRemoved(T value)
        {
            if (this.ItemRemoved != null)
            {
                this.ItemRemoved(this, new PdfObservableCollectionEventArgs<T>(value));
            }
        }

        public bool Remove(T item)
        {
            bool flag = this.collection.Remove(item);
            if (flag)
            {
                this.RaiseItemRemoved(item);
            }
            return flag;
        }

        public void RemoveAt(int index)
        {
            T local = this.collection[index];
            this.collection.RemoveAt(index);
            this.RaiseItemRemoved(local);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.collection.GetEnumerator();

        public T this[int index]
        {
            get => 
                this.collection[index];
            set => 
                this.collection[index] = value;
        }

        public int Count =>
            this.collection.Count;

        public bool IsReadOnly =>
            false;
    }
}

