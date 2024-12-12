namespace DevExpress.Xpf.Layout.Core.Base
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class BaseChangeableList<T> : BaseReadOnlyList<T>, IChangeableCollection<T>, DevExpress.Xpf.Layout.Core.IReadOnlyCollection<T>, IEnumerable<T>, IEnumerable, ISupportVisitor<T>, ISupportNotification<T>, ICollection, IList where T: class
    {
        [CompilerGenerated]
        private CollectionChangedHandler<T> collectionChangedCore;

        public event CollectionChangedHandler<T> CollectionChanged
        {
            add
            {
                this.collectionChangedCore += value;
            }
            remove
            {
                this.collectionChangedCore -= value;
            }
        }

        private event CollectionChangedHandler<T> collectionChangedCore
        {
            [CompilerGenerated] add
            {
                CollectionChangedHandler<T> collectionChangedCore = this.collectionChangedCore;
                while (true)
                {
                    CollectionChangedHandler<T> comparand = collectionChangedCore;
                    CollectionChangedHandler<T> handler3 = comparand + value;
                    collectionChangedCore = Interlocked.CompareExchange<CollectionChangedHandler<T>>(ref this.collectionChangedCore, handler3, comparand);
                    if (ReferenceEquals(collectionChangedCore, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                CollectionChangedHandler<T> collectionChangedCore = this.collectionChangedCore;
                while (true)
                {
                    CollectionChangedHandler<T> comparand = collectionChangedCore;
                    CollectionChangedHandler<T> handler3 = comparand - value;
                    collectionChangedCore = Interlocked.CompareExchange<CollectionChangedHandler<T>>(ref this.collectionChangedCore, handler3, comparand);
                    if (ReferenceEquals(collectionChangedCore, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public void Add(T element)
        {
            this.OnBeforeElementAdded(element);
            base.List.Add(element);
            this.OnElementAdded(element);
        }

        public void AddRange(DevExpress.Xpf.Layout.Core.IReadOnlyCollection<T> elements)
        {
            this.OnBeforeElementRangeAdded();
            foreach (T local in elements)
            {
                this.Add(local);
            }
            this.OnElementRangeAdded();
        }

        public void AddRange(T[] elements)
        {
            this.OnBeforeElementRangeAdded();
            foreach (T local in elements)
            {
                this.Add(local);
            }
            this.OnElementRangeAdded();
        }

        public void Clear()
        {
            T[] localArray = this.ToArray();
            for (int i = 0; i < localArray.Length; i++)
            {
                this.Remove(localArray[i]);
            }
        }

        public void CopyTo(T[] array, int index)
        {
            base.List.CopyTo(array, index);
        }

        public void CopyTo(Array array, int index)
        {
            base.Collection.CopyTo(array, index);
        }

        protected virtual void OnBeforeElementAdded(T element)
        {
        }

        protected virtual void OnBeforeElementRangeAdded()
        {
        }

        protected virtual void OnBeforeElementRemoved(T element)
        {
        }

        protected override void OnDispose()
        {
            this.collectionChangedCore = null;
            base.OnDispose();
        }

        protected virtual void OnElementAdded(T element)
        {
            this.RaiseCollectionChanged(new CollectionChangedEventArgs<T>(element, CollectionChangedType.ElementAdded));
        }

        protected virtual void OnElementRangeAdded()
        {
        }

        protected virtual void OnElementRemoved(T element)
        {
            this.RaiseCollectionChanged(new CollectionChangedEventArgs<T>(element, CollectionChangedType.ElementRemoved));
        }

        protected virtual void RaiseCollectionChanged(CollectionChangedEventArgs<T> ea)
        {
            if (this.collectionChangedCore != null)
            {
                this.collectionChangedCore(ea);
            }
        }

        public bool Remove(T element)
        {
            this.OnBeforeElementRemoved(element);
            bool flag = base.List.Remove(element);
            if (flag)
            {
                this.OnElementRemoved(element);
            }
            return flag;
        }

        int IList.Add(object value)
        {
            this.Add(value as T);
            return base.List.IndexOf((T) value);
        }

        bool IList.Contains(object value) => 
            base.List.Contains((T) value);

        int IList.IndexOf(object value) => 
            base.List.IndexOf((T) value);

        void IList.Insert(int index, object value)
        {
            T item = (T) value;
            base.List.Insert(index, item);
            if (item != null)
            {
                this.OnElementAdded(item);
            }
        }

        void IList.Remove(object value)
        {
            this.Remove((T) value);
        }

        void IList.RemoveAt(int index)
        {
            T element = base[index];
            base.List.RemoveAt(index);
            if (element != null)
            {
                this.OnElementRemoved(element);
            }
        }

        public T[] ToArray()
        {
            T[] array = new T[base.Collection.Count];
            this.CopyTo(array, 0);
            return array;
        }

        public bool IsReadOnly =>
            base.List.IsReadOnly;

        public bool IsSynchronized =>
            base.Collection.IsSynchronized;

        public object SyncRoot =>
            base.Collection.SyncRoot;

        bool IList.IsFixedSize =>
            false;

        object IList.this[int index]
        {
            get => 
                base.List[index];
            set => 
                base.List[index] = (T) value;
        }
    }
}

