namespace DevExpress.Mvvm.Native
{
    using DevExpress.Data.Helpers;
    using DevExpress.Mvvm.DataAnnotations;
    using DevExpress.Mvvm.POCO;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Data;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Threading.Tasks;
    using System.Windows.Data;

    public abstract class BoundCollectionBase<T> : IList<T>, ICollection<T>, IEnumerable<T>, IEnumerable, IList, ICollection, INotifyCollectionChanged, INotifyPropertyChanged, IDisposable
    {
        [ThreadStatic]
        private static Dictionary<Type, Func<object>> ctorCache;
        private static Func<List<T>, T[]> get_items;
        private static Action<List<T>, int> set_size;
        private static Action<List<T>, int> set_version;
        private static Func<List<T>, int> get_version;
        private object source;
        private DataTable dataTable;
        private IList iListSource;
        private IEnumerable iEnumerableSource;
        private bool isSourceReadOnly;
        private bool isSourceFixedSize;
        private Type sourceObjectType;
        private List<T> items;
        private Dictionary<object, T> idCache;
        private Dictionary<object, T> sourceObjectCache;
        [CompilerGenerated]
        private NotifyCollectionChangedEventHandler CollectionChanged;
        [CompilerGenerated]
        private PropertyChangedEventHandler PropertyChanged;
        [CompilerGenerated]
        private EventHandler BeforeClear;
        private Task populateIdCacheTask;
        private Task populateSourceObjectCacheTask;
        private CancellationTokenSource populateIdCacheTaskCancel;
        private CancellationTokenSource populateSourceObjectCacheTaskCancel;
        private bool isIdCacheReady;
        private bool isSourceObjectCacheReady;
        private bool lockSourceCollectionChanged;

        public event EventHandler BeforeClear
        {
            [CompilerGenerated] add
            {
                EventHandler beforeClear = this.BeforeClear;
                while (true)
                {
                    EventHandler comparand = beforeClear;
                    EventHandler handler3 = comparand + value;
                    beforeClear = Interlocked.CompareExchange<EventHandler>(ref this.BeforeClear, handler3, comparand);
                    if (ReferenceEquals(beforeClear, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                EventHandler beforeClear = this.BeforeClear;
                while (true)
                {
                    EventHandler comparand = beforeClear;
                    EventHandler handler3 = comparand - value;
                    beforeClear = Interlocked.CompareExchange<EventHandler>(ref this.BeforeClear, handler3, comparand);
                    if (ReferenceEquals(beforeClear, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event NotifyCollectionChangedEventHandler CollectionChanged
        {
            [CompilerGenerated] add
            {
                NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
                while (true)
                {
                    NotifyCollectionChangedEventHandler comparand = collectionChanged;
                    NotifyCollectionChangedEventHandler handler3 = comparand + value;
                    collectionChanged = Interlocked.CompareExchange<NotifyCollectionChangedEventHandler>(ref this.CollectionChanged, handler3, comparand);
                    if (ReferenceEquals(collectionChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                NotifyCollectionChangedEventHandler collectionChanged = this.CollectionChanged;
                while (true)
                {
                    NotifyCollectionChangedEventHandler comparand = collectionChanged;
                    NotifyCollectionChangedEventHandler handler3 = comparand - value;
                    collectionChanged = Interlocked.CompareExchange<NotifyCollectionChangedEventHandler>(ref this.CollectionChanged, handler3, comparand);
                    if (ReferenceEquals(collectionChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        public event PropertyChangedEventHandler PropertyChanged
        {
            [CompilerGenerated] add
            {
                PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
                while (true)
                {
                    PropertyChangedEventHandler comparand = propertyChanged;
                    PropertyChangedEventHandler handler3 = comparand + value;
                    propertyChanged = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, handler3, comparand);
                    if (ReferenceEquals(propertyChanged, comparand))
                    {
                        return;
                    }
                }
            }
            [CompilerGenerated] remove
            {
                PropertyChangedEventHandler propertyChanged = this.PropertyChanged;
                while (true)
                {
                    PropertyChangedEventHandler comparand = propertyChanged;
                    PropertyChangedEventHandler handler3 = comparand - value;
                    propertyChanged = Interlocked.CompareExchange<PropertyChangedEventHandler>(ref this.PropertyChanged, handler3, comparand);
                    if (ReferenceEquals(propertyChanged, comparand))
                    {
                        return;
                    }
                }
            }
        }

        static BoundCollectionBase()
        {
            BoundCollectionBase<T>.init_functions();
        }

        protected BoundCollectionBase() : this(0)
        {
        }

        protected BoundCollectionBase(int capacity)
        {
            this.isSourceReadOnly = false;
            this.isSourceFixedSize = false;
            this.items = new List<T>(capacity);
            this.idCache = new Dictionary<object, T>(capacity);
            this.sourceObjectCache = new Dictionary<object, T>(capacity);
            this.isIdCacheReady = true;
            this.isSourceObjectCacheReady = true;
        }

        public virtual void Add(T item)
        {
            if (this.IsBound)
            {
                this.SourceAdd(this.GetSourceObjectCore(item));
            }
            this.ItemsInsert(this.items.Count, item);
        }

        public virtual void AddRange(IEnumerable<T> items)
        {
            List<T> newItems = new List<T>(items);
            if (this.IsBound)
            {
                foreach (T local in newItems)
                {
                    this.SourceAdd(this.GetSourceObjectCore(local));
                }
            }
            this.ItemsInsertRange(this.items.Count, newItems);
        }

        public int BinarySearch(T item) => 
            this.items.BinarySearch(item);

        public int BinarySearch(T item, IComparer<T> comparer) => 
            this.items.BinarySearch(item, comparer);

        public int BinarySearch(T item, System.Func<T, T, int> comparer) => 
            this.items.BinarySearch(item, new DelegateComparer<T>(comparer));

        private void CheckCachesState(bool stop = false)
        {
            if (this.UseIdCache || this.UseSourceObjectCache)
            {
                if (!this.isIdCacheReady)
                {
                    if (stop)
                    {
                        this.populateIdCacheTaskCancel.Cancel();
                    }
                    this.populateIdCacheTask.Wait();
                }
                if (!this.isSourceObjectCacheReady)
                {
                    if (stop)
                    {
                        this.populateSourceObjectCacheTaskCancel.Cancel();
                    }
                    this.populateSourceObjectCacheTask.Wait();
                }
            }
        }

        private void CheckSourceObject(object item)
        {
            if (item == null)
            {
                BoundCollectionBase<T>.ThrowItemIsNotBound();
            }
            if ((this.dataTable != null) && !(item is DataRowView))
            {
                BoundCollectionBase<T>.ThrowDataTableSourceObjectIncorrect();
            }
        }

        public virtual void Clear()
        {
            if (this.IsBound)
            {
                this.SourceClear();
            }
            this.ItemsClear();
        }

        public bool Contains(T item)
        {
            if (!this.UseIndexCache)
            {
                return this.items.Contains(item);
            }
            if (item == null)
            {
                return false;
            }
            this.CheckCachesState(false);
            int indexCore = this.GetIndexCore(item);
            return ((indexCore >= 0) && ((indexCore < this.Count) && (this.items[indexCore] == item)));
        }

        public void CopyTo(T[] array, int arrayIndex)
        {
            this.items.CopyTo(array, arrayIndex);
        }

        protected static object CreateInstance(Type type)
        {
            Func<object> func;
            if (!BoundCollectionBase<T>.CtorCache.TryGetValue(type, out func))
            {
                func = Expression.Lambda<Func<object>>(Expression.New(type), new ParameterExpression[0]).Compile();
                BoundCollectionBase<T>.CtorCache.Add(type, func);
            }
            return func();
        }

        protected abstract T CreateItemCore(object sourceObject);
        protected virtual object CreateSourceObjectCore()
        {
            if (!(this.iListSource is IBindingList))
            {
                return ((this.SourceObjectTypeCore != null) ? BoundCollectionBase<T>.CreateInstance(this.SourceObjectTypeCore) : null);
            }
            object res = null;
            this.SourceDoLockedAction(delegate {
                res = ((IBindingList) ((BoundCollectionBase<T>) this).iListSource).AddNew();
                ((BoundCollectionBase<T>) this).iListSource.Remove(res);
                if (((BoundCollectionBase<T>) this).dataTable != null)
                {
                    foreach (DataColumn column in ((BoundCollectionBase<T>) this).dataTable.Columns)
                    {
                        ((DataRowView) res)[column.ColumnName] = DBNull.Value;
                    }
                }
            });
            return res;
        }

        private void Dispose()
        {
            this.CheckCachesState(true);
            this.idCache = null;
            this.sourceObjectCache = null;
        }

        ~BoundCollectionBase()
        {
            this.Dispose();
        }

        public void ForEach(Action<T> action, bool parallel = false)
        {
            this.CheckCachesState(false);
            if (!parallel)
            {
                this.items.ForEach(action);
            }
            else
            {
                T[] itemsArray = BoundCollectionBase<T>.get_items(this.items);
                Parallel.For(0, this.items.Count, (Action<int>) (i => action(itemsArray[i])));
                if (BoundCollectionBase<T>.get_version(this.items) != BoundCollectionBase<T>.get_version(this.items))
                {
                    BoundCollectionBase<T>.ThrowCollectionWasModified();
                }
            }
        }

        protected T GetByIdCore(object id)
        {
            if (id != null)
            {
                T local;
                if (!this.UseIdCache)
                {
                    return this.FirstOrDefault<T>(x => Equals(((BoundCollectionBase<T>) this).GetIdCore(x), id));
                }
                this.CheckCachesState(false);
                if (id == null)
                {
                    return default(T);
                }
                if (this.idCache.TryGetValue(id, out local))
                {
                    return local;
                }
            }
            return default(T);
        }

        protected T GetBySourceObjectCore(object sourceObject)
        {
            if (sourceObject != null)
            {
                T local;
                if (!this.UseSourceObjectCache)
                {
                    return this.FirstOrDefault<T>(x => Equals(((BoundCollectionBase<T>) this).GetSourceObjectCore(x), sourceObject));
                }
                this.CheckCachesState(false);
                if (this.sourceObjectCache.TryGetValue(sourceObject, out local))
                {
                    return local;
                }
            }
            return default(T);
        }

        protected abstract object GetIdCore(T item);
        protected abstract int GetIndexCore(T item);
        private static Type GetPOCOTypeIfNeeded(Type type, bool allowPOCO) => 
            allowPOCO ? (((type == null) || !type.GetCustomAttributes(typeof(POCOViewModelAttribute), true).Any<object>()) ? type : ViewModelSource.GetPOCOType(type, null)) : type;

        protected abstract object GetSourceObjectCore(T item);
        public static Type GetSourceObjectType(object source, bool allowPOCO = true)
        {
            if (source != null)
            {
                Type sourceObjectType = BoundCollectionBase<T>.GetSourceObjectType(source.GetType(), allowPOCO);
                if (sourceObjectType != null)
                {
                    return sourceObjectType;
                }
                if (source is CollectionView)
                {
                    CollectionView view = source as CollectionView;
                    if (view.SourceCollection != null)
                    {
                        bool flag;
                        return BoundCollectionBase<T>.GetPOCOTypeIfNeeded(ListDataControllerHelper.GetRowType(view.SourceCollection.GetType(), out flag), allowPOCO);
                    }
                }
                if (source is IEnumerable)
                {
                    IEnumerable<object> enumerable = ((IEnumerable) source).Cast<object>();
                    if (enumerable.Count<object>() > 0)
                    {
                        return BoundCollectionBase<T>.GetPOCOTypeIfNeeded(enumerable.First<object>().GetType(), allowPOCO);
                    }
                }
            }
            return null;
        }

        public static Type GetSourceObjectType(Type sourceType, bool allowPOCO = true)
        {
            if (sourceType.IsArray)
            {
                return BoundCollectionBase<T>.GetPOCOTypeIfNeeded(sourceType.GetElementType(), allowPOCO);
            }
            if (sourceType.IsGenericType && (sourceType.GetGenericTypeDefinition() == typeof(IEnumerable<>)))
            {
                return BoundCollectionBase<T>.GetPOCOTypeIfNeeded(sourceType.GetGenericArguments()[0], allowPOCO);
            }
            if (sourceType.IsAssignableFrom(typeof(IEnumerable<>)))
            {
                return BoundCollectionBase<T>.GetPOCOTypeIfNeeded(sourceType.GetGenericParameterConstraints()[0], allowPOCO);
            }
            Func<Type, bool> predicate = <>c<T>.<>9__1_0;
            if (<>c<T>.<>9__1_0 == null)
            {
                Func<Type, bool> local1 = <>c<T>.<>9__1_0;
                predicate = <>c<T>.<>9__1_0 = x => x.IsGenericType;
            }
            Func<Type, bool> func2 = <>c<T>.<>9__1_1;
            if (<>c<T>.<>9__1_1 == null)
            {
                Func<Type, bool> local2 = <>c<T>.<>9__1_1;
                func2 = <>c<T>.<>9__1_1 = x => x.GetGenericTypeDefinition() == typeof(IEnumerable<>);
            }
            Type type = sourceType.GetInterfaces().Where<Type>(predicate).FirstOrDefault<Type>(func2);
            return ((type == null) ? null : BoundCollectionBase<T>.GetPOCOTypeIfNeeded(type.GetGenericArguments()[0], allowPOCO));
        }

        private void IdCacheAdd(IEnumerable<T> items)
        {
            if (this.UseIdCache)
            {
                foreach (T local in items)
                {
                    object idCore = this.GetIdCore(local);
                    if (idCore != null)
                    {
                        this.idCache[idCore] = local;
                    }
                }
            }
        }

        private void IdCacheRemove(IEnumerable<T> items)
        {
            if (this.UseIdCache)
            {
                foreach (T local in items)
                {
                    object idCore = this.GetIdCore(local);
                    if (idCore != null)
                    {
                        this.idCache.Remove(idCore);
                    }
                }
            }
        }

        private void IdCacheReplace(T oldItem, T newItem)
        {
            if (this.UseIdCache)
            {
                if (this.GetIdCore(oldItem) != null)
                {
                    this.idCache.Remove(this.GetIdCore(oldItem));
                }
                object idCore = this.GetIdCore(newItem);
                if (idCore != null)
                {
                    this.idCache[idCore] = newItem;
                }
            }
        }

        private void IndexCacheClear()
        {
            if (this.UseIndexCache)
            {
                T[] localArray = BoundCollectionBase<T>.get_items(this.items);
                for (int i = 0; i < this.Count; i++)
                {
                    this.SetIndexCore(localArray[i], -1);
                }
            }
        }

        private void IndexCacheInsert(int index, IEnumerable<T> items)
        {
            if (this.UseIndexCache)
            {
                this.UpdateIndexCacheCore(index, this.Count);
            }
        }

        private void IndexCacheMove(T item, int oldIndex, int newIndex)
        {
            if (this.UseIndexCache && (oldIndex != newIndex))
            {
                if (oldIndex < newIndex)
                {
                    this.UpdateIndexCacheCore(oldIndex, newIndex + 1);
                }
                else
                {
                    this.UpdateIndexCacheCore(newIndex, oldIndex + 1);
                }
            }
        }

        private void IndexCacheRemove(int index, IEnumerable<T> items)
        {
            if (this.UseIndexCache)
            {
                foreach (T local in items)
                {
                    this.SetIndexCore(local, -1);
                }
                this.UpdateIndexCacheCore(index, this.Count);
            }
        }

        private void IndexCacheReplace(int index, T oldItem, T newItem)
        {
            if (this.UseIndexCache)
            {
                this.SetIndexCore(oldItem, -1);
                this.SetIndexCore(newItem, index);
            }
        }

        public int IndexOf(T item)
        {
            if (!this.UseIndexCache)
            {
                return this.items.IndexOf(item);
            }
            if (item == null)
            {
                return -1;
            }
            this.CheckCachesState(false);
            return this.GetIndexCore(item);
        }

        private static void init_functions()
        {
            if (BoundCollectionBase<T>.get_items == null)
            {
                Type type = typeof(List<T>);
                ParameterExpression expression = Expression.Parameter(typeof(List<T>));
                ParameterExpression right = Expression.Parameter(typeof(int));
                MemberExpression left = Expression.Field(expression, type.GetField("_size", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance));
                MemberExpression body = Expression.Field(expression, type.GetField("_version", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance));
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                BoundCollectionBase<T>.get_items = Expression.Lambda<Func<List<T>, T[]>>(Expression.Field(expression, type.GetField("_items", System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Instance)), parameters).Compile();
                ParameterExpression[] expressionArray2 = new ParameterExpression[] { expression };
                BoundCollectionBase<T>.get_version = Expression.Lambda<Func<List<T>, int>>(body, expressionArray2).Compile();
                ParameterExpression[] expressionArray3 = new ParameterExpression[] { expression, right };
                BoundCollectionBase<T>.set_size = Expression.Lambda<Action<List<T>, int>>(Expression.Assign(left, right), expressionArray3).Compile();
                ParameterExpression[] expressionArray4 = new ParameterExpression[] { expression, right };
                BoundCollectionBase<T>.set_version = Expression.Lambda<Action<List<T>, int>>(Expression.Assign(body, right), expressionArray4).Compile();
            }
        }

        public virtual void Insert(int index, T item)
        {
            if (this.IsBound)
            {
                this.SourceInsert(index, this.GetSourceObjectCore(item));
            }
            this.ItemsInsert(index, item);
        }

        public virtual void InsertRange(int index, IEnumerable<T> items)
        {
            List<T> newItems = new List<T>(items);
            if (this.IsBound)
            {
                int num = index;
                foreach (T local in newItems)
                {
                    this.SourceInsert(num, this.GetSourceObjectCore(local));
                    num++;
                }
            }
            this.ItemsInsertRange(index, newItems);
        }

        private void ItemsClear()
        {
            if (this.items.Count != 0)
            {
                this.CheckCachesState(true);
                this.IndexCacheClear();
                this.OnCollectionClearing();
                this.items.Clear();
                this.idCache = new Dictionary<object, T>();
                this.sourceObjectCache = new Dictionary<object, T>();
                this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Reset));
            }
        }

        private void ItemsInsert(int index, T newItem)
        {
            this.CheckCachesState(false);
            this.items.Insert(index, newItem);
            T[] items = new T[] { newItem };
            this.IndexCacheInsert(index, items);
            T[] localArray2 = new T[] { newItem };
            this.IdCacheAdd(localArray2);
            T[] localArray3 = new T[] { newItem };
            this.SourceObjectCacheAdd(localArray3);
            List<T> changedItems = new List<T>();
            changedItems.Add(newItem);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, changedItems, index));
        }

        private void ItemsInsertRange(int index, List<T> newItems)
        {
            this.CheckCachesState(false);
            this.items.InsertRange(index, newItems);
            this.IndexCacheInsert(index, newItems);
            this.IdCacheAdd(newItems);
            this.SourceObjectCacheAdd(newItems);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, newItems, index));
        }

        private void ItemsMove(int oldIndex, int newIndex)
        {
            this.CheckCachesState(false);
            T item = this.items[oldIndex];
            if (oldIndex != newIndex)
            {
                this.items.RemoveAt(oldIndex);
                this.items.Insert(newIndex, item);
                this.IndexCacheMove(item, oldIndex, newIndex);
            }
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Move, item, newIndex, oldIndex));
        }

        private void ItemsRemove(int index)
        {
            this.CheckCachesState(false);
            T item = this.items[index];
            this.items.RemoveAt(index);
            T[] items = new T[] { item };
            this.IndexCacheRemove(index, items);
            T[] localArray2 = new T[] { item };
            this.IdCacheRemove(localArray2);
            T[] localArray3 = new T[] { item };
            this.SourceObjectCacheRemove(localArray3);
            List<T> changedItems = new List<T>();
            changedItems.Add(item);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, changedItems, index));
        }

        private void ItemsRemoveRange(int index, int count)
        {
            this.CheckCachesState(false);
            List<T> items = new List<T>(count);
            for (int i = index; i < count; i++)
            {
                items.Add(this.items[i]);
            }
            this.items.RemoveRange(index, count);
            this.IndexCacheRemove(index, items);
            this.IdCacheRemove(items);
            this.SourceObjectCacheRemove(items);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Remove, items, index));
        }

        private void ItemsReplace(int index, T newItem)
        {
            this.CheckCachesState(false);
            T oldItem = this.items[index];
            this.items[index] = newItem;
            this.IndexCacheReplace(index, oldItem, newItem);
            this.IdCacheReplace(oldItem, newItem);
            this.SourceObjectCacheReplace(oldItem, newItem);
            this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Replace, newItem, oldItem, index));
        }

        public virtual void Move(int oldIndex, int newIndex)
        {
            if (this.IsBound)
            {
                T item = this.items[oldIndex];
                object sourceObjectCore = this.GetSourceObjectCore(item);
                this.SourceRemove(oldIndex);
                this.SourceInsert(newIndex, sourceObjectCore);
            }
            this.ItemsMove(oldIndex, newIndex);
        }

        protected virtual void OnCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if ((e.Action == NotifyCollectionChangedAction.Add) || ((e.Action == NotifyCollectionChangedAction.Remove) || (e.Action == NotifyCollectionChangedAction.Reset)))
            {
                this.RaiseCountChanged();
            }
            this.RaiseItemIndexerChanged();
            this.RaiseCollectionChanged(e);
        }

        protected virtual void OnCollectionClearing()
        {
            this.RaiseBeforeClear();
        }

        private void OnSourceAdd(int index, object newSourceObject)
        {
            T newItem = this.CreateItemCore(newSourceObject);
            this.ItemsInsert(index, newItem);
        }

        private void OnSourceAddRange(int index, IList newSourceObjects)
        {
            List<T> newItems = new List<T>(newSourceObjects.Count);
            foreach (object obj2 in newSourceObjects)
            {
                newItems.Add(this.CreateItemCore(obj2));
            }
            this.ItemsInsertRange(index, newItems);
        }

        private void OnSourceChanged(object oldValue, object newValue)
        {
            IEnumerable enumerable;
            IList iListSource = this.iListSource;
            if (enumerable == null)
            {
                IList local1 = this.iListSource;
                iListSource = (IList) this.iEnumerableSource;
            }
            this.UnsubscribeSource(iListSource);
            this.dataTable = null;
            this.iListSource = null;
            this.iEnumerableSource = null;
            this.isSourceReadOnly = false;
            this.isSourceFixedSize = false;
            this.sourceObjectType = null;
            this.ItemsClear();
            if (newValue != null)
            {
                IList list1 = newValue as IList;
                IList list3 = list1;
                if (list1 == null)
                {
                    IList local2 = list1;
                    list3 = (newValue is IListSource) ? ((IListSource) newValue).GetList() : null;
                }
                this.iListSource = list3;
                if (this.iListSource is DataView)
                {
                    this.dataTable = ((DataView) this.iListSource).Table;
                }
                this.iEnumerableSource = newValue as IEnumerable;
                if (this.iListSource != null)
                {
                    this.isSourceReadOnly = this.iListSource.IsReadOnly;
                    this.isSourceFixedSize = this.iListSource.IsFixedSize;
                }
                else
                {
                    this.isSourceReadOnly = true;
                    this.isSourceFixedSize = true;
                }
                IList source = this.iListSource;
                if (enumerable == null)
                {
                    IList local3 = this.iListSource;
                    source = (IList) this.iEnumerableSource;
                }
                this.SubscribeSource(source);
            }
            this.Populate();
        }

        protected virtual void OnSourceChanging(object oldValue, object newValue)
        {
        }

        private void OnSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            if (!this.lockSourceCollectionChanged)
            {
                if (e.Action == NotifyCollectionChangedAction.Add)
                {
                    if (e.NewItems.Count > 1)
                    {
                        this.OnSourceAddRange(e.NewStartingIndex, e.NewItems);
                    }
                    else
                    {
                        this.OnSourceAdd(e.NewStartingIndex, e.NewItems[0]);
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Remove)
                {
                    if (e.OldItems.Count > 1)
                    {
                        this.OnSourceRemoveRange(e.OldStartingIndex, e.OldItems.Count);
                    }
                    else
                    {
                        this.OnSourceRemove(e.OldStartingIndex);
                    }
                }
                else if (e.Action == NotifyCollectionChangedAction.Replace)
                {
                    this.OnSourceReplace(e.NewStartingIndex, e.NewItems[0]);
                }
                else if (e.Action == NotifyCollectionChangedAction.Move)
                {
                    this.OnSourceMove(e.OldStartingIndex, e.NewStartingIndex);
                }
                else if (e.Action == NotifyCollectionChangedAction.Reset)
                {
                    this.OnSourceReset();
                }
            }
        }

        private void OnSourceCollectionChanged(object sender, ListChangedEventArgs e)
        {
            if (!this.lockSourceCollectionChanged)
            {
                if (e.ListChangedType == ListChangedType.ItemAdded)
                {
                    this.OnSourceAdd(e.NewIndex, this.iListSource[e.NewIndex]);
                }
                else if (e.ListChangedType == ListChangedType.ItemDeleted)
                {
                    this.OnSourceRemove(e.NewIndex);
                }
                else if (e.ListChangedType == ListChangedType.ItemChanged)
                {
                    if (e.PropertyDescriptor == null)
                    {
                        object sourceObjectCore = this.GetSourceObjectCore(this.items[e.NewIndex]);
                        if ((sourceObjectCore != this.iListSource[e.NewIndex]) || !(sourceObjectCore is INotifyPropertyChanged))
                        {
                            this.OnSourceReplace(e.NewIndex, this.iListSource[e.NewIndex]);
                        }
                    }
                }
                else if (e.ListChangedType == ListChangedType.ItemMoved)
                {
                    this.OnSourceMove(e.OldIndex, e.NewIndex);
                }
                else if (e.ListChangedType == ListChangedType.Reset)
                {
                    this.OnSourceReset();
                }
            }
        }

        private void OnSourceMove(int oldIndex, int newIndex)
        {
            this.ItemsMove(oldIndex, newIndex);
        }

        private void OnSourceRemove(int index)
        {
            this.ItemsRemove(index);
        }

        private void OnSourceRemoveRange(int index, int count)
        {
            this.ItemsRemoveRange(index, count);
        }

        private void OnSourceReplace(int index, object newSourceObject)
        {
            this.ItemsReplace(index, this.CreateItemCore(newSourceObject));
        }

        private void OnSourceReset()
        {
            this.ItemsClear();
            this.Populate();
        }

        private void Populate()
        {
            if (this.IsBound)
            {
                if (this.iListSource == null)
                {
                    if (this.iEnumerableSource == null)
                    {
                        BoundCollectionBase<T>.ThrowSourceNotSupported();
                    }
                    this.PopulateFromIEnumerable(this.iEnumerableSource);
                }
                else
                {
                    this.items.Capacity = this.iListSource.Count;
                    if (this.UseParallelGeneration)
                    {
                        this.PopulateFromIListInMultipleThreads(this.iListSource);
                    }
                    else
                    {
                        this.PopulateFromIList(this.iListSource);
                    }
                }
                this.PopulateIdCache();
                this.PopulateSourceObjectCache();
                if (this.Count > 0)
                {
                    this.OnCollectionChanged(new NotifyCollectionChangedEventArgs(NotifyCollectionChangedAction.Add, this.items, 0));
                }
            }
        }

        private void PopulateFromIEnumerable(IEnumerable iEnumerable)
        {
            if (!this.UseIndexCache)
            {
                foreach (object obj3 in iEnumerable)
                {
                    this.items.Add(this.CreateItemCore(obj3));
                }
            }
            else
            {
                int num = 0;
                foreach (object obj2 in iEnumerable)
                {
                    T item = this.CreateItemCore(obj2);
                    this.items.Add(item);
                    this.SetIndexCore(item, num);
                    num++;
                }
            }
        }

        private void PopulateFromIList(IList iList)
        {
            int oldVersion = BoundCollectionBase<T>.get_version(this.items);
            T[] localArray = BoundCollectionBase<T>.get_items(this.items);
            if (!this.UseIndexCache)
            {
                for (int i = 0; i < iList.Count; i++)
                {
                    localArray[i] = this.CreateItemCore(iList[i]);
                }
            }
            else
            {
                for (int i = 0; i < iList.Count; i++)
                {
                    localArray[i] = this.CreateItemCore(iList[i]);
                    this.SetIndexCore(localArray[i], i);
                }
            }
            this.UpdateListVersion(oldVersion, iList.Count);
        }

        private void PopulateFromIListInMultipleThreads(IList iList)
        {
            int oldVersion = BoundCollectionBase<T>.get_version(this.items);
            T[] itemsArray = BoundCollectionBase<T>.get_items(this.items);
            if (this.UseIndexCache)
            {
                Parallel.For(0, iList.Count, delegate (int i) {
                    itemsArray[i] = ((BoundCollectionBase<T>) this).CreateItemCore(iList[i]);
                    ((BoundCollectionBase<T>) this).SetIndexCore(itemsArray[i], i);
                });
            }
            else
            {
                Parallel.For(0, iList.Count, (Action<int>) (i => (itemsArray[i] = ((BoundCollectionBase<T>) this).CreateItemCore(iList[i]))));
            }
            this.UpdateListVersion(oldVersion, iList.Count);
        }

        private void PopulateIdCache()
        {
            if (this.UseIdCache)
            {
                this.isIdCacheReady = false;
                this.idCache = new Dictionary<object, T>(this.Count);
                if (!this.UseParallelGeneration)
                {
                    this.PopulateIdCacheCore(CancellationToken.None);
                    this.isIdCacheReady = true;
                }
                else
                {
                    this.populateIdCacheTaskCancel = new CancellationTokenSource();
                    this.populateIdCacheTask = Task.Factory.StartNew(delegate {
                        base.PopulateIdCacheCore(base.populateIdCacheTaskCancel.Token);
                        base.populateIdCacheTaskCancel = null;
                        base.populateIdCacheTask = null;
                        base.isIdCacheReady = true;
                    }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                }
            }
        }

        private void PopulateIdCacheCore(CancellationToken token)
        {
            T[] localArray = BoundCollectionBase<T>.get_items(this.items);
            for (int i = 0; (i < this.Count) && !token.IsCancellationRequested; i++)
            {
                T item = localArray[i];
                object idCore = this.GetIdCore(item);
                if (idCore != null)
                {
                    this.idCache[idCore] = item;
                }
            }
        }

        private void PopulateSourceObjectCache()
        {
            if (this.UseSourceObjectCache)
            {
                this.isSourceObjectCacheReady = false;
                this.sourceObjectCache = new Dictionary<object, T>(this.Count);
                if (!this.UseParallelGeneration)
                {
                    this.PopulateSourceObjectCacheCore(CancellationToken.None);
                    this.isSourceObjectCacheReady = true;
                }
                else
                {
                    this.populateSourceObjectCacheTaskCancel = new CancellationTokenSource();
                    this.populateSourceObjectCacheTask = Task.Factory.StartNew(delegate {
                        base.PopulateSourceObjectCacheCore(base.populateSourceObjectCacheTaskCancel.Token);
                        base.populateSourceObjectCacheTaskCancel = null;
                        base.populateSourceObjectCacheTask = null;
                        base.isSourceObjectCacheReady = true;
                    }, CancellationToken.None, TaskCreationOptions.LongRunning, TaskScheduler.Default);
                }
            }
        }

        private void PopulateSourceObjectCacheCore(CancellationToken token)
        {
            T[] localArray = BoundCollectionBase<T>.get_items(this.items);
            for (int i = 0; (i < this.Count) && !token.IsCancellationRequested; i++)
            {
                T item = localArray[i];
                object sourceObjectCore = this.GetSourceObjectCore(item);
                if (sourceObjectCore != null)
                {
                    this.sourceObjectCache[sourceObjectCore] = item;
                }
            }
        }

        private void RaiseBeforeClear()
        {
            if (this.BeforeClear != null)
            {
                this.BeforeClear(this, EventArgs.Empty);
            }
        }

        private void RaiseCollectionChanged(NotifyCollectionChangedEventArgs e)
        {
            if (this.CollectionChanged != null)
            {
                this.CollectionChanged(this, e);
            }
        }

        private void RaiseCountChanged()
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("Count"));
            }
        }

        private void RaiseItemIndexerChanged()
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs("Item[]"));
            }
        }

        public void Refresh()
        {
            if (this.IsBound)
            {
                object sourceCore = this.SourceCore;
                this.SourceCore = null;
                this.SourceCore = sourceCore;
            }
        }

        public void RefreshItem(int index)
        {
            if (this.IsBound)
            {
                object sourceObject = (this.iListSource == null) ? this.iEnumerableSource.OfType<object>().ElementAt<object>(index) : this.iListSource[index];
                this.ItemsReplace(index, this.CreateItemCore(sourceObject));
            }
        }

        public virtual bool Remove(T item)
        {
            int index = this.IndexOf(item);
            if (index < 0)
            {
                return false;
            }
            if (this.IsBound)
            {
                this.SourceRemove(index);
            }
            this.ItemsRemove(index);
            return true;
        }

        public virtual void RemoveAt(int index)
        {
            if ((index < 0) || (index >= this.Count))
            {
                BoundCollectionBase<T>.ThrowIndexOutOfRange();
            }
            if (this.IsBound)
            {
                this.SourceRemove(index);
            }
            this.ItemsRemove(index);
        }

        public virtual void RemoveRange(int index, int count)
        {
            if (this.IsBound)
            {
                for (int i = 0; i < count; i++)
                {
                    this.SourceRemove(index);
                }
            }
            this.ItemsRemoveRange(index, count);
        }

        public virtual void Replace(int index, T item)
        {
            if (this.IsBound)
            {
                this.SourceReplace(index, this.GetSourceObjectCore(item));
            }
            this.ItemsReplace(index, item);
        }

        protected abstract void SetIndexCore(T item, int value);
        private void SourceAdd(object item)
        {
            this.CheckSourceObject(item);
            if (this.dataTable != null)
            {
                this.SourceDoLockedAction(() => ((BoundCollectionBase<T>) this).dataTable.Rows.Add(((DataRowView) item).Row));
            }
            else
            {
                this.SourceDoLockedAction(() => ((BoundCollectionBase<T>) this).iListSource.Add(item));
            }
        }

        private void SourceClear()
        {
            if (this.dataTable != null)
            {
                this.SourceDoLockedAction(() => base.dataTable.Rows.Clear());
            }
            else
            {
                this.SourceDoLockedAction(() => base.iListSource.Clear());
            }
        }

        private void SourceDoLockedAction(Action action)
        {
            if (this.iListSource == null)
            {
                BoundCollectionBase<T>.ThrowSourceNotSupported();
            }
            this.lockSourceCollectionChanged = true;
            try
            {
                action();
            }
            finally
            {
                this.lockSourceCollectionChanged = false;
            }
        }

        private void SourceInsert(int index, object item)
        {
            this.CheckSourceObject(item);
            if (this.dataTable != null)
            {
                this.SourceDoLockedAction(() => ((BoundCollectionBase<T>) this).dataTable.Rows.InsertAt(((DataRowView) item).Row, index));
            }
            else
            {
                this.SourceDoLockedAction(() => ((BoundCollectionBase<T>) this).iListSource.Insert(index, item));
            }
        }

        private void SourceObjectCacheAdd(IEnumerable<T> items)
        {
            if (this.UseSourceObjectCache)
            {
                foreach (T local in items)
                {
                    object sourceObjectCore = this.GetSourceObjectCore(local);
                    if (sourceObjectCore != null)
                    {
                        this.sourceObjectCache[sourceObjectCore] = local;
                    }
                }
            }
        }

        private void SourceObjectCacheRemove(IEnumerable<T> items)
        {
            if (this.UseSourceObjectCache)
            {
                foreach (T local in items)
                {
                    object sourceObjectCore = this.GetSourceObjectCore(local);
                    if (sourceObjectCore != null)
                    {
                        this.sourceObjectCache.Remove(sourceObjectCore);
                    }
                }
            }
        }

        private void SourceObjectCacheReplace(T oldItem, T newItem)
        {
            if (this.UseSourceObjectCache)
            {
                object sourceObjectCore = this.GetSourceObjectCore(oldItem);
                if (sourceObjectCore != null)
                {
                    this.sourceObjectCache.Remove(sourceObjectCore);
                }
                sourceObjectCore = this.GetSourceObjectCore(newItem);
                if (sourceObjectCore != null)
                {
                    this.sourceObjectCache[sourceObjectCore] = newItem;
                }
            }
        }

        private void SourceRemove(int index)
        {
            if (this.dataTable != null)
            {
                this.SourceDoLockedAction(() => ((BoundCollectionBase<T>) this).dataTable.Rows.RemoveAt(index));
            }
            else
            {
                this.SourceDoLockedAction(() => ((BoundCollectionBase<T>) this).iListSource.RemoveAt(index));
            }
        }

        private void SourceReplace(int index, object item)
        {
            this.CheckSourceObject(item);
            if (this.dataTable != null)
            {
                this.SourceDoLockedAction(delegate {
                    ((BoundCollectionBase<T>) this).dataTable.Rows.RemoveAt(index);
                    ((BoundCollectionBase<T>) this).dataTable.Rows.InsertAt(((DataRowView) item).Row, index);
                });
            }
            else
            {
                this.SourceDoLockedAction(() => ((BoundCollectionBase<T>) this).iListSource[index] = item);
            }
        }

        private void SubscribeSource(object source)
        {
            if (source is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged) source).CollectionChanged += new NotifyCollectionChangedEventHandler(this.OnSourceCollectionChanged);
            }
            else if (source is IBindingList)
            {
                ((IBindingList) source).ListChanged += new ListChangedEventHandler(this.OnSourceCollectionChanged);
            }
        }

        IEnumerator<T> IEnumerable<T>.GetEnumerator() => 
            this.items.GetEnumerator();

        void ICollection.CopyTo(Array array, int arrayIndex)
        {
            this.items.CopyTo(array, arrayIndex);
        }

        IEnumerator IEnumerable.GetEnumerator() => 
            this.items.GetEnumerator();

        int IList.Add(object value)
        {
            this.Add((T) value);
            return this.IndexOf((T) value);
        }

        bool IList.Contains(object value) => 
            this.Contains((T) value);

        int IList.IndexOf(object value) => 
            this.IndexOf((T) value);

        void IList.Insert(int index, object value)
        {
            this.Insert(index, (T) value);
        }

        void IList.Remove(object value)
        {
            this.Remove((T) value);
        }

        void IDisposable.Dispose()
        {
            this.Dispose();
        }

        private static void ThrowCollectionWasModified()
        {
            throw new InvalidOperationException("The collection was modified");
        }

        private static void ThrowDataTableSourceObjectIncorrect()
        {
            throw new InvalidOperationException("SourceObject should be DataRowView when DataTable is used as Source");
        }

        private static void ThrowIndexOutOfRange()
        {
            throw new IndexOutOfRangeException();
        }

        private static void ThrowItemIsNotBound()
        {
            throw new InvalidOperationException("Item is not bound to SourceObject");
        }

        private static void ThrowSourceNotSupported()
        {
            throw new InvalidOperationException("Source is not supported");
        }

        private void UnsubscribeSource(object source)
        {
            if (source is INotifyCollectionChanged)
            {
                ((INotifyCollectionChanged) source).CollectionChanged -= new NotifyCollectionChangedEventHandler(this.OnSourceCollectionChanged);
            }
            else if (source is IBindingList)
            {
                ((IBindingList) source).ListChanged -= new ListChangedEventHandler(this.OnSourceCollectionChanged);
            }
        }

        private void UpdateIndexCacheCore(int start, int end)
        {
            for (int i = start; i < end; i++)
            {
                this.SetIndexCore(this.items[i], i);
            }
        }

        protected void UpdateItemIdCore(T item, object oldId)
        {
            if (this.UseIdCache)
            {
                this.CheckCachesState(false);
                if (oldId != null)
                {
                    this.idCache.Remove(oldId);
                }
                object idCore = this.GetIdCore(item);
                if (idCore != null)
                {
                    this.idCache[idCore] = item;
                }
            }
        }

        private void UpdateListVersion(int oldVersion, int count)
        {
            int num = BoundCollectionBase<T>.get_version(this.items);
            if (oldVersion != num)
            {
                BoundCollectionBase<T>.ThrowCollectionWasModified();
            }
            BoundCollectionBase<T>.set_version(this.items, num + count);
            BoundCollectionBase<T>.set_size(this.items, count);
        }

        private static Dictionary<Type, Func<object>> CtorCache =>
            BoundCollectionBase<T>.ctorCache ??= new Dictionary<Type, Func<object>>();

        public bool IsBound =>
            this.source != null;

        public int Count =>
            this.items.Count;

        public T this[int index]
        {
            get => 
                this.items[index];
            set => 
                this.Replace(index, value);
        }

        public bool IsReadOnly =>
            this.isSourceReadOnly;

        public bool IsFixedSize =>
            this.isSourceFixedSize;

        public bool IsSynchronized =>
            false;

        public object SyncRoot =>
            ((ICollection) this.items).SyncRoot;

        protected Type SourceObjectTypeCore
        {
            get
            {
                Type sourceObjectType = this.sourceObjectType;
                if (this.sourceObjectType == null)
                {
                    Type local1 = this.sourceObjectType;
                    sourceObjectType = this.sourceObjectType = BoundCollectionBase<T>.GetSourceObjectType(this.iListSource ?? this.iEnumerableSource, true);
                }
                return sourceObjectType;
            }
        }

        protected object SourceCore
        {
            get => 
                this.source;
            set
            {
                if (this.source != value)
                {
                    object source = this.source;
                    this.OnSourceChanging(source, value);
                    this.source = value;
                    this.OnSourceChanged(source, value);
                }
            }
        }

        protected abstract bool UseParallelGeneration { get; }

        protected abstract bool UseIndexCache { get; }

        protected abstract bool UseIdCache { get; }

        protected abstract bool UseSourceObjectCache { get; }

        object IList.this[int index]
        {
            get => 
                this[index];
            set => 
                this[index] = (T) value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BoundCollectionBase<T>.<>c <>9;
            public static Func<Type, bool> <>9__1_0;
            public static Func<Type, bool> <>9__1_1;

            static <>c()
            {
                BoundCollectionBase<T>.<>c.<>9 = new BoundCollectionBase<T>.<>c();
            }

            internal bool <GetSourceObjectType>b__1_0(Type x) => 
                x.IsGenericType;

            internal bool <GetSourceObjectType>b__1_1(Type x) => 
                x.GetGenericTypeDefinition() == typeof(IEnumerable<>);
        }
    }
}

