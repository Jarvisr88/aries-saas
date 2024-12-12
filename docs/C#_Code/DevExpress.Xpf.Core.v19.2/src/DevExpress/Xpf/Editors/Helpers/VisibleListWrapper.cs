namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public abstract class VisibleListWrapper : IBindingList, IList, ICollection, IEnumerable, IDisposable, INotifyCollectionChanged
    {
        private bool disposed;

        public event NotifyCollectionChangedEventHandler CollectionChanged;

        private event ListChangedEventHandler Handler;

        public event ListChangedEventHandler ListChanged
        {
            add
            {
                this.Handler += value;
            }
            remove
            {
                this.Handler -= value;
            }
        }

        protected VisibleListWrapper()
        {
            this.AllowEdit = true;
            this.EventLocker = new Locker();
            this.SelectionLocker = new Locker();
        }

        public int Add(object value)
        {
            throw new NotImplementedException();
        }

        public void AddIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public object AddNew()
        {
            throw new NotImplementedException();
        }

        public void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            throw new NotImplementedException();
        }

        public void Clear()
        {
            throw new NotImplementedException();
        }

        public bool Contains(object value) => 
            this.ContainsInternal(value);

        protected abstract bool ContainsInternal(object value);
        public void CopyTo(Array array, int index)
        {
            this.CopyToInternal(array, index);
        }

        protected abstract void CopyToInternal(Array array, int index);
        public void Dispose()
        {
            if (!this.disposed)
            {
                this.disposed = true;
                this.DisposeInternal();
            }
        }

        protected abstract void DisposeInternal();
        public int Find(PropertyDescriptor property, object key)
        {
            throw new NotImplementedException();
        }

        protected abstract int GetCountInternal();
        public IEnumerator GetEnumerator() => 
            this.GetEnumeratorInternal();

        protected abstract IEnumerator GetEnumeratorInternal();
        protected abstract object IndexerGetInternal(int index);
        public int IndexOf(object value) => 
            this.IndexOfInternal(value);

        protected abstract int IndexOfInternal(object value);
        public void Insert(int index, object value)
        {
            throw new NotImplementedException();
        }

        protected void RaiseCollectionChanged(NotifyCollectionChangedEventArgs args)
        {
            NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
            if (collectionChanged != null)
            {
                collectionChanged(this, args);
            }
        }

        protected void RaiseListChanged(ListChangedEventArgs e)
        {
            this.Handler.Do<ListChangedEventHandler>(x => x(this, e));
        }

        public void Refresh()
        {
            this.RefreshInternal();
        }

        protected abstract void RefreshInternal();
        public void Remove(object value)
        {
            throw new NotImplementedException();
        }

        public void RemoveAt(int index)
        {
            throw new NotImplementedException();
        }

        public void RemoveIndex(PropertyDescriptor property)
        {
            throw new NotImplementedException();
        }

        public void RemoveSort()
        {
            throw new NotImplementedException();
        }

        public Locker SelectionLocker { get; private set; }

        public Locker EventLocker { get; private set; }

        public int Count =>
            this.GetCountInternal();

        public object SyncRoot { get; private set; }

        public bool IsSynchronized { get; private set; }

        public object this[int index]
        {
            get => 
                this.IndexerGetInternal(index);
            set
            {
                throw new NotImplementedException();
            }
        }

        public bool IsReadOnly { get; private set; }

        public bool IsFixedSize { get; private set; }

        public bool AllowNew { get; private set; }

        public bool AllowEdit { get; private set; }

        public bool AllowRemove { get; private set; }

        public bool SupportsChangeNotification =>
            true;

        public bool SupportsSearching { get; private set; }

        public bool SupportsSorting { get; private set; }

        public bool IsSorted { get; private set; }

        public PropertyDescriptor SortProperty { get; private set; }

        public ListSortDirection SortDirection { get; private set; }
    }
}

