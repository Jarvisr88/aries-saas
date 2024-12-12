namespace DMEWorks.Forms
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;

    public class ArrayViewBase<T> : IList, ICollection, IEnumerable, IList<T>, ICollection<T>, IEnumerable<T>, ITypedList, IBindingList
    {
        private T[] original;
        private T[] filtered;
        private Func<T, bool> filter;
        private readonly IReadOnlyList<LinqPropertyDescriptor<T>> properties;
        private ListSortDirection sortDirection;
        private LinqPropertyDescriptor<T> sortProperty;
        private object syncRoot;
        [CompilerGenerated]
        private ListChangedEventHandler ListChanged;

        public event ListChangedEventHandler ListChanged
        {
            [CompilerGenerated] add
            {
                ListChangedEventHandler listChanged = this.ListChanged;
                while (true)
                {
                    ListChangedEventHandler comparand = listChanged;
                    ListChangedEventHandler handler3 = comparand + value;
                    listChanged = Interlocked.CompareExchange<ListChangedEventHandler>(ref this.ListChanged, handler3, comparand);
                    if (ReferenceEquals(listChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                ListChangedEventHandler listChanged = this.ListChanged;
                while (true)
                {
                    ListChangedEventHandler comparand = listChanged;
                    ListChangedEventHandler handler3 = comparand - value;
                    listChanged = Interlocked.CompareExchange<ListChangedEventHandler>(ref this.ListChanged, handler3, comparand);
                    if (ReferenceEquals(listChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public ArrayViewBase(params string[] properties)
        {
            this.original = new T[0];
            this.properties = Array.AsReadOnly<LinqPropertyDescriptor<T>>(LinqPropertyDescriptor<T>.CreateDescriptors(properties));
        }

        public ArrayViewBase(IEnumerable<T> collection, params string[] properties)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            this.original = collection.ToArray<T>();
            this.properties = Array.AsReadOnly<LinqPropertyDescriptor<T>>(LinqPropertyDescriptor<T>.CreateDescriptors(properties));
        }

        private static T[] Append(T[] array, IEnumerable<T> collection)
        {
            ICollection<T> is2 = (collection as ICollection<T>) ?? collection.ToList<T>();
            T[] destinationArray = new T[array.Length + is2.Count];
            Array.Copy(array, 0, destinationArray, 0, array.Length);
            is2.CopyTo(destinationArray, array.Length);
            return destinationArray;
        }

        public void AppendRange(IEnumerable<T> collection)
        {
            if (collection == null)
            {
                throw new ArgumentNullException("collection");
            }
            this.SetOriginal(ArrayViewBase<T>.Append(this.original, collection));
        }

        protected void ApplyFilterCore(Func<T, bool> value)
        {
            this.filter = value;
            this.ResetView();
        }

        protected void ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            LinqPropertyDescriptor<T> descriptor = property as LinqPropertyDescriptor<T>;
            if (descriptor != null)
            {
                Array.Sort<T>(this.original, descriptor.GetComparer(direction));
                this.sortDirection = direction;
                this.sortProperty = descriptor;
                this.ResetView();
            }
        }

        public void Clear()
        {
            this.SetOriginal(new T[0]);
        }

        public bool Contains(T item) => 
            this.InternalFiltered.Contains<T>(item);

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.InternalFiltered.CopyTo(array, arrayIndex);
        }

        protected IEnumerable<LambdaExpression> GetLambdaExpressions()
        {
            Func<LinqPropertyDescriptor<T>, LambdaExpression> selector = <>c<T>.<>9__9_0;
            if (<>c<T>.<>9__9_0 == null)
            {
                Func<LinqPropertyDescriptor<T>, LambdaExpression> local1 = <>c<T>.<>9__9_0;
                selector = <>c<T>.<>9__9_0 = p => p.ToExpression();
            }
            return this.properties.Select<LinqPropertyDescriptor<T>, LambdaExpression>(selector);
        }

        public int IndexOf(T item) => 
            Array.IndexOf<T>(this.InternalFiltered, item);

        private void OnListChanged(ListChangedEventArgs args)
        {
            ListChangedEventHandler listChanged = this.ListChanged;
            if (listChanged != null)
            {
                listChanged(this, args);
            }
        }

        private void ResetView()
        {
            this.filtered = null;
            this.OnListChanged(new ListChangedEventArgs(ListChangedType.Reset, -1));
        }

        private void SetOriginal(T[] value)
        {
            this.original = value;
            this.ApplySort(this.sortProperty, this.sortDirection);
            this.ResetView();
        }

        void ICollection<T>.Add(T item)
        {
            ArrayViewBase<T>.ThrowNotSupportedException();
        }

        void ICollection<T>.Clear()
        {
            ArrayViewBase<T>.ThrowNotSupportedException();
        }

        bool ICollection<T>.Remove(T item)
        {
            ArrayViewBase<T>.ThrowNotSupportedException();
            return false;
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => 
            this.InternalFiltered.GetEnumerator();

        void IList<T>.Insert(int index, T item)
        {
            ArrayViewBase<T>.ThrowNotSupportedException();
        }

        void IList<T>.RemoveAt(int index)
        {
            ArrayViewBase<T>.ThrowNotSupportedException();
        }

        void ICollection.CopyTo(Array array, int index)
        {
            this.InternalFiltered.CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.InternalFiltered.GetEnumerator();

        int IList.Add(object value)
        {
            ArrayViewBase<T>.ThrowNotSupportedException();
            return -1;
        }

        bool IList.Contains(object value) => 
            this.InternalFiltered.Contains(value);

        int IList.IndexOf(object value) => 
            this.InternalFiltered.IndexOf(value);

        void IList.Insert(int index, object value)
        {
            ArrayViewBase<T>.ThrowNotSupportedException();
        }

        void IList.Remove(object value)
        {
            ArrayViewBase<T>.ThrowNotSupportedException();
        }

        void IList.RemoveAt(int index)
        {
            ArrayViewBase<T>.ThrowNotSupportedException();
        }

        void IBindingList.AddIndex(PropertyDescriptor property)
        {
        }

        object IBindingList.AddNew()
        {
            ArrayViewBase<T>.ThrowNotSupportedException();
            return null;
        }

        void IBindingList.ApplySort(PropertyDescriptor property, ListSortDirection direction)
        {
            this.ApplySort(property, direction);
        }

        int IBindingList.Find(PropertyDescriptor property, object key) => 
            -1;

        void IBindingList.RemoveIndex(PropertyDescriptor property)
        {
        }

        void IBindingList.RemoveSort()
        {
            if (this.sortProperty != null)
            {
                this.sortProperty = null;
                this.ResetView();
            }
        }

        PropertyDescriptorCollection ITypedList.GetItemProperties(PropertyDescriptor[] listAccessors) => 
            ((listAccessors == null) || (listAccessors.Length == 0)) ? new PropertyDescriptorCollection(this.properties.ToArray<LinqPropertyDescriptor<T>>()) : new PropertyDescriptorCollection(null);

        string ITypedList.GetListName(PropertyDescriptor[] listAccessors) => 
            string.Empty;

        private static void ThrowNotSupportedException()
        {
            throw new NotSupportedException("Collection is readonly");
        }

        private T[] InternalFiltered
        {
            get
            {
                this.filtered ??= ((this.filter == null) ? this.original : this.original.Where<T>(this.filter).ToArray<T>());
                return this.filtered;
            }
        }

        public T this[int index] =>
            this.InternalFiltered[index];

        public int Count =>
            this.InternalFiltered.Length;

        bool IList.IsFixedSize =>
            true;

        bool IList.IsReadOnly =>
            true;

        object IList.this[int index]
        {
            get => 
                this[index];
            set => 
                ArrayViewBase<T>.ThrowNotSupportedException();
        }

        bool ICollection.IsSynchronized =>
            false;

        object ICollection.SyncRoot
        {
            get
            {
                if (this.syncRoot == null)
                {
                    Interlocked.CompareExchange<object>(ref this.syncRoot, new object(), null);
                }
                return this.syncRoot;
            }
        }

        T IList<T>.this[int index]
        {
            get => 
                this[index];
            set => 
                ArrayViewBase<T>.ThrowNotSupportedException();
        }

        bool ICollection<T>.IsReadOnly =>
            true;

        bool IBindingList.AllowEdit =>
            false;

        bool IBindingList.AllowNew =>
            false;

        bool IBindingList.AllowRemove =>
            false;

        bool IBindingList.IsSorted =>
            this.sortProperty != null;

        ListSortDirection IBindingList.SortDirection =>
            this.sortDirection;

        PropertyDescriptor IBindingList.SortProperty =>
            this.sortProperty;

        bool IBindingList.SupportsChangeNotification =>
            true;

        bool IBindingList.SupportsSearching =>
            false;

        bool IBindingList.SupportsSorting =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ArrayViewBase<T>.<>c <>9;
            public static Func<LinqPropertyDescriptor<T>, LambdaExpression> <>9__9_0;

            static <>c()
            {
                ArrayViewBase<T>.<>c.<>9 = new ArrayViewBase<T>.<>c();
            }

            internal LambdaExpression <GetLambdaExpressions>b__9_0(LinqPropertyDescriptor<T> p) => 
                p.ToExpression();
        }
    }
}

