namespace DevExpress.Utils
{
    using DevExpress.Internal;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Reflection;
    using System.Runtime.InteropServices;

    [Serializable, ComVisible(false)]
    public class DXCollectionBase<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection
    {
        private readonly List<T> list;
        internal DXCollectionUniquenessProvider<T> uniquenessProvider;

        public DXCollectionBase() : this(DXCollectionUniquenessProviderType.MinimizeMemoryUsage)
        {
        }

        protected DXCollectionBase(DXCollectionUniquenessProviderType uniquenessProviderType)
        {
            this.list = new List<T>();
            this.UniquenessProviderType = uniquenessProviderType;
        }

        protected DXCollectionBase(IEnumerable<T> collection)
        {
            this.list = new List<T>(collection);
            this.uniquenessProvider = EmptyUniquenessProvider<T>.Instance;
        }

        public DXCollectionBase(int capacity) : this(capacity, DXCollectionUniquenessProviderType.MinimizeMemoryUsage)
        {
        }

        protected DXCollectionBase(int capacity, DXCollectionUniquenessProviderType uniquenessProviderType)
        {
            this.list = new List<T>(capacity);
            this.UniquenessProviderType = uniquenessProviderType;
        }

        public virtual int Add(T value) => 
            this.AddIfNotAlreadyAdded(value);

        protected virtual int AddCore(T value)
        {
            this.OnValidate(value);
            if (!this.OnInsert(this.InnerList.Count, value))
            {
                return -1;
            }
            int count = this.InnerList.Count;
            this.InnerList.Add(value);
            try
            {
                this.OnInsertComplete(count, value);
            }
            catch (Exception)
            {
                this.InnerList.RemoveAt(count);
                throw;
            }
            return count;
        }

        protected internal virtual int AddIfNotAlreadyAdded(T obj)
        {
            int objectIndex = this.uniquenessProvider.LookupObjectIndex(obj);
            return ((objectIndex >= 0) ? objectIndex : this.AddCore(obj));
        }

        public virtual void AddRange(ICollection collection)
        {
            this.AddRangeCore(collection);
        }

        protected virtual void AddRangeCore(ICollection collection)
        {
            foreach (T local in collection)
            {
                this.Add(local);
            }
        }

        protected internal int BinarySearch(T item, IComparer<T> comparer) => 
            this.list.BinarySearch(item, comparer);

        public void Clear()
        {
            if (this.OnClear())
            {
                this.InnerList.Clear();
                this.OnClearComplete();
            }
        }

        public virtual bool Contains(T value) => 
            this.InnerList.Contains(value);

        public void CopyTo(T[] array, int index)
        {
            this.InnerList.CopyTo(array, index);
        }

        protected internal DXCollectionUniquenessProvider<T> CreateUniquenessProvider(DXCollectionUniquenessProviderType strategy)
        {
            switch (strategy)
            {
                case DXCollectionUniquenessProviderType.MinimizeMemoryUsage:
                    return new SimpleUniquenessProvider<T>((DXCollectionBase<T>) this);

                case DXCollectionUniquenessProviderType.MaximizePerformance:
                    return new DictionaryUniquenessProvider<T>((DXCollectionBase<T>) this);

                case DXCollectionUniquenessProviderType.MaxPerformanceMinMemory:
                    return new HashSetUniquenessProvider<T>((DXCollectionBase<T>) this);
            }
            return EmptyUniquenessProvider<T>.Instance;
        }

        private static string EnvironmentGetResourceString(string key)
        {
            Type[] types = new Type[] { typeof(string) };
            MethodInfo info = typeof(Environment).GetMethod("GetResourceString", BindingFlags.NonPublic | BindingFlags.Static, null, types, new ParameterModifier[0]);
            if (info == null)
            {
                return key;
            }
            object[] parameters = new object[] { key };
            return (string) info.Invoke(null, parameters);
        }

        public T Find(Predicate<T> match)
        {
            Guard.ArgumentNotNull(match, "match");
            int count = this.Count;
            for (int i = 0; i < count; i++)
            {
                T local = this.list[i];
                if (match(local))
                {
                    return local;
                }
            }
            return default(T);
        }

        protected internal virtual void FindAllCore(DXCollectionBase<T> result, Predicate<T> match)
        {
            Guard.ArgumentNotNull(match, "match");
            int count = this.Count;
            for (int i = 0; i < count; i++)
            {
                T local = this.list[i];
                if (match(local))
                {
                    result.Add(local);
                }
            }
        }

        public void ForEach(Action<T> action)
        {
            Guard.ArgumentNotNull(action, "action");
            int count = this.Count;
            for (int i = 0; i < count; i++)
            {
                action(this.InnerList[i]);
            }
        }

        public IEnumerator<T> GetEnumerator() => 
            this.InnerList.GetEnumerator();

        internal IList<T> GetInnerList() => 
            this.InnerList;

        protected virtual T GetItem(int index)
        {
            if ((index < 0) || (index >= this.InnerList.Count))
            {
                this.ThrowIndexOutOfRangeException();
            }
            return this.InnerList[index];
        }

        public virtual int IndexOf(T value) => 
            this.InnerList.IndexOf(value);

        public void Insert(int index, T value)
        {
            this.InsertIfNotAlreadyInserted(index, value);
        }

        protected internal virtual void InsertCore(int index, T value)
        {
            if ((index < 0) || (index > this.InnerList.Count))
            {
                this.ThrowIndexOutOfRangeException();
            }
            this.OnValidate(value);
            if (this.OnInsert(index, value))
            {
                this.InnerList.Insert(index, value);
                try
                {
                    this.OnInsertComplete(index, value);
                }
                catch (Exception)
                {
                    this.InnerList.RemoveAt(index);
                    throw;
                }
            }
        }

        protected virtual void InsertIfNotAlreadyInserted(int index, T obj)
        {
            if (this.uniquenessProvider.LookupObject(obj))
            {
                throw new ArgumentException("obj");
            }
            this.InsertCore(index, obj);
        }

        internal T InvokeGetItem(int index) => 
            this.GetItem(index);

        protected virtual bool OnClear() => 
            true;

        protected virtual void OnClearComplete()
        {
            this.uniquenessProvider.OnClearComplete();
        }

        protected virtual bool OnInsert(int index, T value) => 
            true;

        protected virtual void OnInsertComplete(int index, T value)
        {
            this.uniquenessProvider.OnInsertComplete(value);
        }

        protected virtual bool OnRemove(int index, T value) => 
            true;

        protected virtual void OnRemoveComplete(int index, T value)
        {
            this.uniquenessProvider.OnRemoveComplete(value);
        }

        protected virtual bool OnSet(int index, T oldValue, T newValue) => 
            !this.uniquenessProvider.LookupObject(newValue);

        protected virtual void OnSetComplete(int index, T oldValue, T newValue)
        {
            this.uniquenessProvider.OnSetComplete(oldValue, newValue);
        }

        protected virtual void OnValidate(T value)
        {
            if (value == null)
            {
                throw new ArgumentNullException("value");
            }
        }

        public virtual bool Remove(T value) => 
            this.RemoveIfAlreadyAdded(value);

        public void RemoveAt(int index)
        {
            this.RemoveAtCore(index);
        }

        protected internal virtual bool RemoveAtCore(int index)
        {
            if ((index < 0) || (index >= this.InnerList.Count))
            {
                this.ThrowIndexOutOfRangeException();
            }
            T local = this.InnerList[index];
            this.OnValidate(local);
            if (!this.OnRemove(index, local))
            {
                return false;
            }
            this.InnerList.RemoveAt(index);
            this.OnRemoveComplete(index, local);
            return true;
        }

        protected internal virtual void RemoveCore(T value)
        {
            this.OnValidate(value);
            int objectIndex = this.uniquenessProvider.LookupObjectIndex(value);
            if (objectIndex < 0)
            {
                this.ThrowArgumentNotFoundException();
            }
            if (this.OnRemove(objectIndex, value))
            {
                this.InnerList.RemoveAt(objectIndex);
                this.OnRemoveComplete(objectIndex, value);
            }
        }

        protected internal virtual bool RemoveIfAlreadyAdded(T obj)
        {
            if (obj == null)
            {
                throw new ArgumentNullException("obj");
            }
            int index = this.List.IndexOf(obj);
            return ((index >= 0) && this.RemoveAtCore(index));
        }

        protected virtual void SetItem(int index, T value)
        {
            if ((index < 0) || (index >= this.InnerList.Count))
            {
                this.ThrowIndexOutOfRangeException();
            }
            this.OnValidate(value);
            T oldValue = this.InnerList[index];
            if (this.OnSet(index, oldValue, value))
            {
                this.InnerList[index] = value;
                try
                {
                    this.OnSetComplete(index, oldValue, value);
                }
                catch (Exception)
                {
                    this.InnerList[index] = oldValue;
                    throw;
                }
            }
        }

        public virtual void Sort(IComparer<T> comparer)
        {
            this.list.Sort(comparer);
        }

        void ICollection<T>.Add(T value)
        {
            this.AddIfNotAlreadyAdded(value);
        }

        void ICollection.CopyTo(Array array, int index)
        {
            ((ICollection) this.InnerList).CopyTo(array, index);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.InnerList.GetEnumerator();

        int IList.Add(object value) => 
            this.AddIfNotAlreadyAdded((T) value);

        bool IList.Contains(object value) => 
            ((IList) this.InnerList).Contains(value);

        int IList.IndexOf(object value) => 
            ((IList) this.InnerList).IndexOf(value);

        void IList.Insert(int index, object value)
        {
            this.InsertIfNotAlreadyInserted(index, (T) value);
        }

        void IList.Remove(object value)
        {
            this.RemoveIfAlreadyAdded((T) value);
        }

        private void ThrowArgumentNotFoundException()
        {
            throw new ArgumentException(DXCollectionBase<T>.EnvironmentGetResourceString("Arg_RemoveArgNotFound"));
        }

        private void ThrowIndexOutOfRangeException()
        {
            throw new ArgumentOutOfRangeException("index", DXCollectionBase<T>.EnvironmentGetResourceString("ArgumentOutOfRange_Index"));
        }

        public virtual T[] ToArray() => 
            this.list.ToArray();

        public int Count =>
            this.InnerList.Count;

        [ComVisible(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public int Capacity
        {
            get => 
                this.list.Capacity;
            set => 
                this.list.Capacity = value;
        }

        protected virtual IList<T> InnerList =>
            this.list;

        protected internal IList<T> List =>
            this;

        protected internal DXCollectionUniquenessProvider<T> UniquenessProvider =>
            this.uniquenessProvider;

        [DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DXCollectionUniquenessProviderType UniquenessProviderType
        {
            get => 
                this.uniquenessProvider.Type;
            set
            {
                if ((this.InnerList != null) && (this.Count > 0))
                {
                    throw new ArgumentException("Can't change uniqueness type for non-empty collection");
                }
                this.uniquenessProvider = this.CreateUniquenessProvider(value);
            }
        }

        bool ICollection.IsSynchronized =>
            ((IList) this.InnerList).IsSynchronized;

        object ICollection.SyncRoot =>
            ((IList) this.InnerList).SyncRoot;

        bool ICollection<T>.IsReadOnly =>
            this.IsReadOnly;

        protected virtual bool IsReadOnly =>
            this.InnerList.IsReadOnly;

        T IList<T>.this[int index]
        {
            get => 
                this.GetItem(index);
            set => 
                this.SetItem(index, value);
        }

        bool IList.IsFixedSize =>
            ((IList) this.InnerList).IsFixedSize;

        bool IList.IsReadOnly =>
            this.IsReadOnly;

        object IList.this[int index]
        {
            get => 
                this.GetItem(index);
            set => 
                this.SetItem(index, (T) value);
        }
    }
}

