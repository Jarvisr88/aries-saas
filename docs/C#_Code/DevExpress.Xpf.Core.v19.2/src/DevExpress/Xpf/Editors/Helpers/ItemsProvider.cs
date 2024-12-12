namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Data.Helpers;
    using DevExpress.Data.Linq;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Threading;

    public class ItemsProvider : IItemsProvider, IItemsProviderCollectionViewSupport, IDataControllerVisualClient, IEnumerable<object>, IEnumerable, IDataControllerAdapter, IWeakEventListener
    {
        private readonly IItemsProviderOwner owner;
        private readonly Dictionary<DisplayTextCacheItem, int> editTextToDisplayTextCache = new Dictionary<DisplayTextCacheItem, int>();
        private BaseListSourceDataController dataController;
        private IEnumerable visibleListSource;
        private CriteriaOperator displayFilterCriteria;
        private string searchText;
        private FilterCondition filterCondition;

        public event ItemsProviderChangedEventHandler ItemsProviderChanged;

        public ItemsProvider(IItemsProviderOwner owner)
        {
            this.InitializeDataControllerLocker = new Locker();
            this.owner = owner;
            this.CollectionViewSupport = this.CreateCollectionViewSupport();
            this.ItemsCache = new DataControllerItemsCache(this, true);
            this.InitializeDataController();
        }

        private void AddItem(int listSourceIndex)
        {
            IList visibleListSource = (IList) this.VisibleListSource;
            int controllerRow = this.DataController.GetControllerRow(listSourceIndex);
            if (controllerRow != -2147483648)
            {
                if (controllerRow == visibleListSource.Count)
                {
                    visibleListSource.Add(this.DataController.GetRowByListSourceIndex(listSourceIndex));
                }
                else
                {
                    visibleListSource.Insert(controllerRow, this.DataController.GetRowByListSourceIndex(listSourceIndex));
                }
            }
        }

        public static bool AreEqual(IList list1, IList list2) => 
            ((list1 != null) || (list2 != null)) ? ((list1 != null) ? ((list2 != null) ? ((list1.Count == list2.Count) ? list1.Cast<object>().All<object>(new Func<object, bool>(list2.Contains)) : false) : (list1.Count == 0)) : (list2.Count == 0)) : true;

        private CriteriaOperator CalcActualFilterOperator()
        {
            List<CriteriaOperator> operands = new List<CriteriaOperator>();
            if (!Equals(this.DisplayFilterCriteria, null))
            {
                operands.Add(this.DisplayFilterCriteria);
            }
            if (!Equals(this.Owner.FilterCriteria, null))
            {
                operands.Add(this.Owner.FilterCriteria);
            }
            return ((operands.Count != 0) ? ((operands.Count > 1) ? CriteriaOperator.And(operands) : operands[0]) : null);
        }

        private void ClearDisplayCache()
        {
            this.ResetCaches();
            this.ResetVisibleListSource();
        }

        private void ClearDisplayCacheAndRaiseListChanged()
        {
            this.ClearDisplayCache();
            this.RaiseDataChanged(ListChangedType.Reset, -1, null);
        }

        public void ClearSort()
        {
            this.DataController.SortInfo.Clear();
        }

        protected virtual DataControllerICollectionViewSupport CreateCollectionViewSupport() => 
            new DataControllerICollectionViewSupport(this);

        protected virtual IEnumerable CreateCollectionWrapper(Type genericType)
        {
            Type[] typeArguments = new Type[] { genericType };
            IList list = (IList) Activator.CreateInstance(((this.DataController.Helper.TypedList != null) ? typeof(TypedListObservableCollection<>) : typeof(ObservableCollection<>)).MakeGenericType(typeArguments));
            foreach (object obj2 in this.Cast<object>())
            {
                list.Add(obj2);
            }
            return list;
        }

        protected virtual BaseListSourceDataController CreateDataController() => 
            this.IsServerMode ? ((BaseListSourceDataController) new ItemsProviderServerModeDataController()) : ((BaseListSourceDataController) new ItemsProviderDataController());

        public CriteriaOperator CreateDisplayFilterCriteria(string searchText, FilterCondition filterCondition)
        {
            if (string.IsNullOrEmpty(searchText))
            {
                return null;
            }
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(this.GetDisplayColumnInfo().Name), searchText };
            return new FunctionOperator(this.GetFunctionOperatorType(filterCondition), operands);
        }

        private DisplayTextCacheItem CreateDisplayTextCacheItem(string text, bool isCaseSensitive, bool autoComplete)
        {
            DisplayTextCacheItem item1 = new DisplayTextCacheItem();
            item1.DisplayText = text;
            item1.AutoComplete = autoComplete;
            return item1;
        }

        protected virtual IEnumerable CreateHierarchicalSource() => 
            this.CollectionViewSupport.HasCollectionView ? this.CollectionViewSupport.CollectionView : this.CreatePlainListSource();

        protected virtual IEnumerable CreatePlainListSource()
        {
            Type sourceGenericType = this.GetSourceGenericType();
            if (sourceGenericType == null)
            {
                sourceGenericType = typeof(object);
            }
            return this.CreateCollectionWrapper(sourceGenericType);
        }

        protected virtual IEnumerable CreateVisibleListSource() => 
            this.Owner.AllowCollectionView ? this.CreateHierarchicalSource() : this.CreatePlainListSource();

        private void DataController_ListChanged(object sender, ListChangedEventArgs e)
        {
            int newIndex = e.NewIndex;
            ListChangedType listChangedType = e.ListChangedType;
            if (listChangedType == ListChangedType.ItemChanged)
            {
                if (!Equals(this.ActualFilterCriteria, null) && (e.PropertyDescriptor != null))
                {
                    listChangedType = ListChangedType.Reset;
                    this.ClearDisplayCache();
                }
                else
                {
                    this.ItemsCache.UpdateItem(newIndex);
                    if (this.ShouldUpdateVisibleListSourceOnUpdateEvents(e.PropertyDescriptor))
                    {
                        this.UpdateItem(newIndex, e.PropertyDescriptor);
                    }
                }
            }
            else if (listChangedType == ListChangedType.ItemAdded)
            {
                this.ItemsCache.UpdateItemOnAdding(newIndex);
                if (this.ShouldUpdateVisibleListSourceOnUpdateEvents(null))
                {
                    this.AddItem(newIndex);
                }
            }
            else if (listChangedType != ListChangedType.ItemDeleted)
            {
                this.ClearDisplayCache();
            }
            else
            {
                this.ItemsCache.UpdateItemOnDeleting(newIndex);
                if (this.ShouldUpdateVisibleListSourceOnUpdateEvents(null))
                {
                    this.RemoveItem(newIndex);
                }
            }
            this.ResetDisplayTextCache();
            this.RaiseDataChanged(listChangedType, newIndex, e.PropertyDescriptor);
        }

        private void DataController_ListSourceChanged(object sender, EventArgs e)
        {
        }

        private void DataController_Refreshed(object sender, EventArgs e)
        {
            this.ClearDisplayCacheAndRaiseListChanged();
        }

        void IDataControllerVisualClient.ColumnsRenewed()
        {
        }

        void IDataControllerVisualClient.RequestSynchronization()
        {
        }

        void IDataControllerVisualClient.RequireSynchronization(IDataSync dataSync)
        {
            dataSync.AllowSyncSortingAndGrouping = false;
            this.ClearDisplayCacheAndRaiseListChanged();
        }

        void IDataControllerVisualClient.UpdateColumns()
        {
        }

        void IDataControllerVisualClient.UpdateLayout()
        {
        }

        void IDataControllerVisualClient.UpdateRow(int controllerRowHandle)
        {
        }

        void IDataControllerVisualClient.UpdateRowIndexes(int newTopRowIndex)
        {
        }

        void IDataControllerVisualClient.UpdateRows(int topRowIndexDelta)
        {
        }

        void IDataControllerVisualClient.UpdateScrollBar()
        {
        }

        void IDataControllerVisualClient.UpdateTotalSummary()
        {
        }

        int IDataControllerAdapter.GetListSourceIndex(object value)
        {
            throw new NotImplementedException();
        }

        object IDataControllerAdapter.GetRow(int listSourceIndex) => 
            this.DataController.GetRowByListSourceIndex(listSourceIndex);

        object IDataControllerAdapter.GetRowValue(int index)
        {
            object rowByListSourceIndex = this.DataController.GetRowByListSourceIndex(index);
            return this.GetEditValueCore(rowByListSourceIndex);
        }

        object IDataControllerAdapter.GetRowValue(object item) => 
            this.GetEditValueCore(item);

        string IItemsProvider.GetDisplayPropertyName(object handle) => 
            string.Empty;

        IEnumerable IItemsProvider.GetVisibleListSource(object hadle) => 
            null;

        void IItemsProvider.RegisterSnapshot(object handle)
        {
        }

        void IItemsProvider.ReleaseSnapshot(object handle)
        {
        }

        void IItemsProvider.SetDisplayFilterCriteria(CriteriaOperator criteria, object handle)
        {
        }

        void IItemsProviderCollectionViewSupport.RaiseCurrentChanged(object currentItem)
        {
            this.RaiseCurrentChanged(currentItem);
        }

        void IItemsProviderCollectionViewSupport.SetCurrentItem(object currentItem)
        {
            this.CollectionViewSupport.SetCurrentItem(currentItem);
        }

        void IItemsProviderCollectionViewSupport.SyncWithCurrentItem()
        {
            this.CollectionViewSupport.SyncWithCurrent();
        }

        public void DoRefresh()
        {
            this.DataController.DoRefresh();
        }

        public void DoSort(ColumnSortOrder sortOrder)
        {
            if (this.DataController.IsReady)
            {
                DataColumnInfo displayColumnInfo = this.GetDisplayColumnInfo();
                if (displayColumnInfo != null)
                {
                    DataColumnSortInfo[] sortInfos = new DataColumnSortInfo[] { new DataColumnSortInfo(displayColumnInfo, sortOrder) };
                    this.DataController.SortInfo.ClearAndAddRange(sortInfos);
                }
            }
        }

        private IList ExtractCollectionViewDataSource(object itemsSource)
        {
            ICollectionView dataSource = itemsSource as ICollectionView;
            if (dataSource == null)
            {
                CollectionViewSource source1 = new CollectionViewSource();
                source1.Source = itemsSource;
                dataSource = source1.View;
            }
            return DataBindingHelper.ExtractDataSource(dataSource, true, false, false);
        }

        protected virtual IList ExtractDataSource()
        {
            object itemsSource = this.Owner.ItemsSource ?? this.Owner.Items;
            return (this.Owner.AllowCollectionView ? this.ExtractCollectionViewDataSource(itemsSource) : this.ExtractSimpleDataSource(itemsSource));
        }

        private IList ExtractSimpleDataSource(object itemsSource)
        {
            ICollectionView source = itemsSource as ICollectionView;
            return ((source == null) ? DataBindingHelper.ExtractDataSource(itemsSource, true, false, false) : DataBindingHelper.ExtractDataSourceFromCollectionView(source, DevExpress.Xpf.Core.Native.ItemPropertyNotificationMode.PropertyChanged));
        }

        public Type FindGenericType(Type sourceType)
        {
            Type type3;
            using (IEnumerator<Type> enumerator = this.GetTypeHierarchy(sourceType).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Type[] interfaces = enumerator.Current.GetInterfaces();
                        Type collectionLikeGenericTypeFromInterfaces = this.GetCollectionLikeGenericTypeFromInterfaces(interfaces);
                        if (collectionLikeGenericTypeFromInterfaces == null)
                        {
                            continue;
                        }
                        type3 = collectionLikeGenericTypeFromInterfaces;
                    }
                    else
                    {
                        if (!sourceType.IsGenericType)
                        {
                            return null;
                        }
                        Type[] genericArguments = sourceType.GetGenericArguments();
                        return ((genericArguments.Length == 1) ? genericArguments[0] : null);
                    }
                    break;
                }
            }
            return type3;
        }

        public virtual int FindItemIndexByText(string text, bool isCaseSensitive, bool autoComplete, object handle, int startItemIndex = -1)
        {
            int num;
            if (text == null)
            {
                return -1;
            }
            string str = isCaseSensitive ? text : text.ToLower();
            DisplayTextCacheItem key = this.CreateDisplayTextCacheItem(str, isCaseSensitive, autoComplete);
            if (!this.editTextToDisplayTextCache.TryGetValue(key, out num))
            {
                num = this.FindItemIndexByTextInternal(str, isCaseSensitive, autoComplete);
                if (!this.IsAsyncServerMode || (num != -2147483638))
                {
                    this.editTextToDisplayTextCache[key] = num;
                }
            }
            return num;
        }

        protected virtual int FindItemIndexByTextInternal(string text, bool isCaseSensitive, bool autoComplete)
        {
            CriteriaOperator criteria = this.GetCompareCriteriaOperator(autoComplete && (text != string.Empty), new OperandProperty(this.DataControllerData.DisplayColumnName), new OperandValue(text));
            PropertyDescriptor[] properties = new PropertyDescriptor[] { this.GetDisplayStringPropertyDescriptor() };
            ExpressionEvaluator evaluator = new ExpressionEvaluator(new PropertyDescriptorCollection(properties), criteria, isCaseSensitive);
            for (int i = 0; i < this.Count; i++)
            {
                if ((bool) evaluator.Evaluate(this[i]))
                {
                    return this.DataController.GetListSourceRowIndex(i);
                }
            }
            return -1;
        }

        private IEnumerable<string> GetAvailableColumns()
        {
            Func<IDataColumnInfo, string> selector = <>c.<>9__154_0;
            if (<>c.<>9__154_0 == null)
            {
                Func<IDataColumnInfo, string> local1 = <>c.<>9__154_0;
                selector = <>c.<>9__154_0 = dataColumnInfo => dataColumnInfo.FieldName;
            }
            return this.DataController.Columns.Cast<IDataColumnInfo>().Select<IDataColumnInfo, string>(selector);
        }

        private Type GetCollectionLikeGenericTypeFromInterfaces(IEnumerable<Type> interfaces)
        {
            Type type2;
            using (IEnumerator<Type> enumerator = interfaces.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Type current = enumerator.Current;
                        if (!current.IsGenericType)
                        {
                            continue;
                        }
                        Type[] genericArguments = current.GetGenericArguments();
                        if ((genericArguments.Length > 1) || !(typeof(IEnumerable<>).MakeGenericType(genericArguments) == current))
                        {
                            continue;
                        }
                        type2 = genericArguments[0];
                    }
                    else
                    {
                        return null;
                    }
                    break;
                }
            }
            return type2;
        }

        private CriteriaOperator GetCompareCriteriaOperator(bool autoComplete, OperandProperty property, OperandValue value)
        {
            if (!autoComplete)
            {
                return new BinaryOperator(property, value, BinaryOperatorType.Equal);
            }
            CriteriaOperator[] operands = new CriteriaOperator[] { property, value };
            return new FunctionOperator(FunctionOperatorType.StartsWith, operands);
        }

        public int GetControllerIndexByIndex(int index, object handle = null) => 
            this.DataController.GetControllerRow(index);

        public int GetCount(object handle) => 
            this.Count;

        private DataColumnInfo GetDisplayColumnInfo() => 
            this.DataController.Columns[this.DataControllerData.DisplayColumnName];

        private PropertyDescriptor GetDisplayStringPropertyDescriptor() => 
            new GetStringFromLookUpValuePropertyDescriptor(this.DataControllerData.DisplayColumnDescriptor);

        protected virtual string GetDisplayTextCore(object displayValue) => 
            Convert.ToString(this.DataController.GetValueEx(this.IndexOfValue(displayValue, null), this.GetDisplayColumnInfo().Name));

        public virtual object GetDisplayValueByEditValue(object editValue, object handle = null)
        {
            object listSourceItemByValue = this.GetListSourceItemByValue(editValue);
            if (this.IsInLookUpMode)
            {
                return this.GetDisplayValueFromItem(listSourceItemByValue);
            }
            object item = listSourceItemByValue;
            if (listSourceItemByValue == null)
            {
                object local1 = listSourceItemByValue;
                item = editValue;
            }
            return this.GetDisplayValueFromItem(item);
        }

        public object GetDisplayValueByIndex(int index, object handle)
        {
            int controllerIndexByIndex = this.GetControllerIndexByIndex(index, null);
            return this.GetDisplayValueFromItem(this.GetItemByControllerIndex(controllerIndexByIndex, null));
        }

        protected virtual object GetDisplayValueCore(object item) => 
            this.DataControllerData.DisplayColumnDescriptor.GetValue(item);

        public object GetDisplayValueFromItem(object item)
        {
            object displayValueCore = this.GetDisplayValueCore(item);
            if (LookUpPropertyDescriptorBase.IsUnsetValue(displayValueCore))
            {
                displayValueCore = null;
            }
            return displayValueCore;
        }

        protected virtual object GetEditValueCore(object item) => 
            this.DataControllerData.ValueColumnDescriptor.GetValue(item);

        [IteratorStateMachine(typeof(<GetEnumerator>d__144))]
        public IEnumerator<object> GetEnumerator()
        {
            <GetEnumerator>d__144 d__1 = new <GetEnumerator>d__144(0);
            d__1.<>4__this = this;
            return d__1;
        }

        protected virtual FunctionOperatorType GetFunctionOperatorType(FilterCondition filterCondition) => 
            (filterCondition == FilterCondition.Contains) ? FunctionOperatorType.Contains : FunctionOperatorType.StartsWith;

        public int GetIndexByControllerIndex(int controllerIndex, object handle = null)
        {
            object itemByControllerIndex = this.GetItemByControllerIndex(controllerIndex, null);
            return this.ItemsCache.IndexByItem(itemByControllerIndex);
        }

        public int GetIndexByItem(object item, object handle = null) => 
            this.ItemsCache.IndexByItem(item);

        public object GetItem(object value, object handle = null)
        {
            int listSourceRow = this.IndexOfValue(value, null);
            int controllerRow = this.DataController.GetControllerRow(listSourceRow);
            return ((listSourceRow > -1) ? this.DataController.GetListSourceRow(controllerRow) : null);
        }

        public object GetItemByControllerIndex(int controllerIndex, object handle = null) => 
            (controllerIndex <= -1) ? null : this.DataController.GetRow(controllerIndex);

        protected virtual object GetListSourceItemByValue(object editValue)
        {
            int listSourceRow = this.ItemsCache.IndexOfValue(editValue);
            return this.DataController.GetRowByListSourceIndex(listSourceRow);
        }

        private object GetRowValueInternal(DataColumnInfo column, int listSourceIndex) => 
            this.DataController.GetListSourceRowValue(listSourceIndex, column.Name);

        public ServerModeDataControllerBase GetServerModeDataController() => 
            null;

        private Type GetSourceGenericType()
        {
            Type sourceType = null;
            BindingListAdapter listSource = this.ListSource as BindingListAdapter;
            if ((listSource != null) && (listSource.OriginalDataSource != null))
            {
                sourceType = listSource.OriginalDataSource.GetType();
            }
            else if (this.ListSource != null)
            {
                sourceType = this.ListSource.GetType();
            }
            return ((sourceType != null) ? this.FindGenericType(sourceType) : null);
        }

        private IEnumerable<Type> GetTypeHierarchy(Type type)
        {
            IList<Type> source = new List<Type>();
            for (Type type2 = type; type2.BaseType != null; type2 = type2.BaseType)
            {
                source.Add(type2);
            }
            return source.Reverse<Type>();
        }

        public object GetValueByIndex(int index, object handle = null)
        {
            object rowByListSourceIndex = this.DataController.GetRowByListSourceIndex(index);
            return this.GetValueFromItem(rowByListSourceIndex, null);
        }

        public DataColumnInfo GetValueColumnInfo() => 
            this.DataController.Columns[this.DataControllerData.ValueColumnName];

        public object GetValueFromItem(object item, object handle = null)
        {
            object editValueCore = this.GetEditValueCore(item);
            if (LookUpPropertyDescriptorBase.IsUnsetValue(editValueCore))
            {
                editValueCore = null;
            }
            return editValueCore;
        }

        public int IndexOfValue(object value, object handle = null) => 
            this.ItemsCache.IndexOfValue(value);

        protected void InitializeDataController()
        {
            this.InitializeDataControllerLocker.DoLockedActionIfNotLocked(new Action(this.InitializeDataControllerInternal));
        }

        protected virtual void InitializeDataControllerInternal()
        {
            this.ListSource = this.ExtractDataSource();
            this.VerifyDataController();
            try
            {
                this.DataController.BeginUpdate();
                this.CollectionViewSupport.Release();
                this.DataController.SetDataSource(this.ListSource);
                this.UpdateDisplayFilter();
            }
            finally
            {
                this.CollectionViewSupport.SyncWithData(this);
                this.CollectionViewSupport.Initialize();
                this.CollectionViewSupport.SyncWithCurrent();
                this.DataController.EndUpdate();
            }
        }

        private ColumnSortOrder ListSortDirectionToColumnSortOrder(ListSortDirection direction) => 
            (direction == ListSortDirection.Ascending) ? ColumnSortOrder.Ascending : ColumnSortOrder.Descending;

        public virtual void ProcessCollectionChanged(NotifyItemsProviderChangedEventArgs e)
        {
            if (e.ChangedType != ListChangedType.ItemChanged)
            {
                this.Reset();
            }
            else
            {
                this.ProcessItemChanged(e);
                this.RaiseDataChanged(ListChangedType.ItemChanged, -1, null);
            }
        }

        protected virtual void ProcessItemChanged(NotifyItemsProviderChangedEventArgs e)
        {
            this.ResetDisplayTextCache();
        }

        public virtual void ProcessSelectionChanged(NotifyItemsProviderSelectionChangedEventArgs e)
        {
            this.RaiseSelectionChanged(this.GetValueFromItem(e.Item, null), e.IsSelected);
        }

        public void RaiseCurrentChanged(object currentItem)
        {
            if (this.ItemsProviderChanged != null)
            {
                this.ItemsProviderChanged(this, new ItemsProviderCurrentChangedEventArgs(currentItem));
            }
        }

        public void RaiseDataChanged(ItemsProviderDataChangedEventArgs args)
        {
            Guard.ArgumentNotNull(args, "args");
            if (this.ItemsProviderChanged != null)
            {
                this.ItemsProviderChanged(this, args);
            }
        }

        public void RaiseDataChanged(ListChangedType changedType = 0, int newIndex = -1, PropertyDescriptor descriptor = null)
        {
            this.RaiseDataChanged(new ItemsProviderDataChangedEventArgs(changedType, newIndex, descriptor));
        }

        public void RaiseSelectionChanged(object editValue, bool isSelected)
        {
            if (this.ItemsProviderChanged != null)
            {
                this.ItemsProviderChanged(this, new ItemsProviderSelectionChangedEventArgs(editValue, isSelected));
            }
        }

        protected virtual void ReleaseDataController()
        {
            this.dataController.ListSourceChanged -= new EventHandler(this.DataController_ListSourceChanged);
            this.dataController.ListChanged -= new ListChangedEventHandler(this.DataController_ListChanged);
            this.dataController.Refreshed -= new EventHandler(this.DataController_Refreshed);
            this.dataController.VisualClient = null;
            this.dataController.DataClient = null;
            this.DataController.Dispose();
            this.dataController = null;
        }

        private void RemoveItem(int listSourceIndex)
        {
            IList visibleListSource = (IList) this.VisibleListSource;
            int controllerRow = this.DataController.GetControllerRow(listSourceIndex);
            if (controllerRow != -2147483648)
            {
                visibleListSource.RemoveAt(controllerRow);
            }
        }

        public void Reset()
        {
            this.ResetCaches();
            this.InitializeDataController();
        }

        public void ResetCaches()
        {
            this.ItemsCache.Reset();
            this.ResetDisplayTextCache();
        }

        public void ResetDisplayTextCache()
        {
            this.editTextToDisplayTextCache.Clear();
        }

        protected void ResetVisibleListSource()
        {
            this.visibleListSource = null;
        }

        public void SetFilterCondition(FilterCondition condition)
        {
            this.filterCondition = condition;
            this.UpdateDisplayFilter();
        }

        public void SetSearchText(string text)
        {
            this.searchText = text;
            this.UpdateDisplayFilter();
        }

        private bool ShouldUpdateVisibleListSourceOnUpdateEvents(PropertyDescriptor changedProperty = null) => 
            (this.visibleListSource != null) ? (!this.Owner.AllowCollectionView ? ((changedProperty == null) ? (this.VisibleListSource is IList) : false) : false) : false;

        IEnumerator IEnumerable.GetEnumerator() => 
            this.GetEnumerator();

        bool IWeakEventListener.ReceiveWeakEvent(Type managerType, object sender, EventArgs e)
        {
            if (!(managerType == typeof(ListChangedEventManager)))
            {
                return false;
            }
            this.ClearDisplayCacheAndRaiseListChanged();
            return true;
        }

        private void UpdateDisplayFilter()
        {
            this.ActualFilterCriteria = this.CalcActualFilterOperator();
            this.DataController.FilterCriteria = this.ActualFilterCriteria;
            this.ResetCaches();
        }

        public void UpdateDisplayMember()
        {
            this.ResetCaches();
            this.DataController.RePopulateColumns();
        }

        public void UpdateFilterCriteria()
        {
            this.ActualFilterCriteria = this.CalcActualFilterOperator();
            this.DataController.FilterCriteria = this.ActualFilterCriteria;
        }

        private void UpdateItem(int listSourceIndex, PropertyDescriptor changedProperty = null)
        {
            IList visibleListSource = (IList) this.VisibleListSource;
            int controllerRow = this.DataController.GetControllerRow(listSourceIndex);
            if (controllerRow != -2147483648)
            {
                object rowByListSourceIndex = this.DataController.GetRowByListSourceIndex(listSourceIndex);
                if ((changedProperty == null) && (rowByListSourceIndex != visibleListSource[controllerRow]))
                {
                    visibleListSource[controllerRow] = rowByListSourceIndex;
                }
            }
        }

        public void UpdateItemsSource()
        {
            this.Reset();
        }

        public void UpdateValueMember()
        {
            this.ResetCaches();
            this.DataController.RePopulateColumns();
        }

        private void VerifyDataController()
        {
            this.DataControllerData.ResetDescriptors();
            if (this.DataController.IsServerMode != this.IsServerMode)
            {
                this.ReleaseDataController();
            }
        }

        private DataControllerICollectionViewSupport CollectionViewSupport { get; set; }

        public DataControllerItemsCache ItemsCache { get; private set; }

        private Locker InitializeDataControllerLocker { get; set; }

        public object CurrentDataViewHandle =>
            null;

        public bool IsInLookUpMode =>
            this.HasValueMember || this.HasDisplayMember;

        public IItemsProviderOwner Owner =>
            this.owner;

        public int Count =>
            this.DataController.VisibleListSourceRowCount;

        public CriteriaOperator DisplayFilterCriteria
        {
            get => 
                this.displayFilterCriteria;
            set
            {
                if (!ReferenceEquals(this.displayFilterCriteria, value))
                {
                    this.displayFilterCriteria = value;
                    this.UpdateDisplayFilter();
                }
            }
        }

        public CriteriaOperator ActualFilterCriteria { get; private set; }

        public IList ListSource { get; private set; }

        public DevExpress.Xpf.Editors.Helpers.DataControllerData DataControllerData =>
            this.DataController.DataClient as DevExpress.Xpf.Editors.Helpers.DataControllerData;

        public IEnumerable VisibleListSource
        {
            get
            {
                IEnumerable visibleListSource = this.visibleListSource;
                if (this.visibleListSource == null)
                {
                    IEnumerable local1 = this.visibleListSource;
                    visibleListSource = this.visibleListSource = this.CreateVisibleListSource();
                }
                return visibleListSource;
            }
        }

        public IEnumerable<string> AvailableColumns =>
            this.GetAvailableColumns();

        public bool HasDisplayMember =>
            !string.IsNullOrEmpty(this.Owner.DisplayMember);

        public bool HasValueMember =>
            !string.IsNullOrEmpty(this.Owner.ValueMember);

        public bool IsServerMode =>
            this.ListSource is IListServer;

        protected BaseListSourceDataController DataController
        {
            get
            {
                if (this.dataController == null)
                {
                    this.dataController = this.CreateDataController();
                    this.dataController.VisualClient = this;
                    this.dataController.DataClient = new DevExpress.Xpf.Editors.Helpers.DataControllerData(this.dataController, this.Owner);
                    this.dataController.ListChanged += new ListChangedEventHandler(this.DataController_ListChanged);
                    this.dataController.ListSourceChanged += new EventHandler(this.DataController_ListSourceChanged);
                    this.dataController.Refreshed += new EventHandler(this.DataController_Refreshed);
                    this.InitializeDataController();
                }
                return this.dataController;
            }
        }

        public object this[int index] =>
            this.GetItemByControllerIndex(index, null);

        bool IDataControllerVisualClient.IsInitializing =>
            false;

        int IDataControllerVisualClient.PageRowCount =>
            this.DataController.VisibleCount;

        int IDataControllerVisualClient.VisibleRowCount =>
            -1;

        int IDataControllerVisualClient.TopRowIndex =>
            0;

        ICollectionViewHelper IItemsProviderCollectionViewSupport.DataSync =>
            this.ListSource as ICollectionViewHelper;

        ICollectionView IItemsProviderCollectionViewSupport.ListSource
        {
            get
            {
                ICollectionViewHelper listSource = this.ListSource as ICollectionViewHelper;
                return listSource?.Collection;
            }
        }

        bool IItemsProviderCollectionViewSupport.IsSynchronizedWithCurrentItem =>
            this.Owner.IsSynchronizedWithCurrentItem;

        int IDataControllerAdapter.VisibleRowCount =>
            this.DataController.VisibleListSourceRowCount;

        bool IDataControllerAdapter.IsOwnSearchProcessing =>
            this.IsAsyncServerMode;

        public bool IsAsyncServerMode =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ItemsProvider.<>c <>9 = new ItemsProvider.<>c();
            public static Func<IDataColumnInfo, string> <>9__154_0;

            internal string <GetAvailableColumns>b__154_0(IDataColumnInfo dataColumnInfo) => 
                dataColumnInfo.FieldName;
        }

        [CompilerGenerated]
        private sealed class <GetEnumerator>d__144 : IEnumerator<object>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private object <>2__current;
            public ItemsProvider <>4__this;
            private int <i>5__1;

            [DebuggerHidden]
            public <GetEnumerator>d__144(int <>1__state)
            {
                this.<>1__state = <>1__state;
            }

            private bool MoveNext()
            {
                int num = this.<>1__state;
                if (num == 0)
                {
                    this.<>1__state = -1;
                    this.<i>5__1 = 0;
                }
                else
                {
                    if (num != 1)
                    {
                        return false;
                    }
                    this.<>1__state = -1;
                    int num2 = this.<i>5__1;
                    this.<i>5__1 = num2 + 1;
                }
                if (this.<i>5__1 >= this.<>4__this.Count)
                {
                    return false;
                }
                this.<>2__current = this.<>4__this.DataController.GetRow(this.<i>5__1);
                this.<>1__state = 1;
                return true;
            }

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            object IEnumerator<object>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }

        private class ItemsProviderDataController : DXGridDataController
        {
            protected override BaseDataControllerHelper CreateHelper() => 
                new ListDataControllerHelper(this);

            protected override System.Windows.Threading.Dispatcher Dispatcher =>
                ((ItemsProvider) base.VisualClient).Owner.Dispatcher;
        }

        private class ItemsProviderServerModeDataController : ServerModeDataController
        {
            protected override BaseDataControllerHelper CreateHelper() => 
                new ListDataControllerHelper(this);
        }
    }
}

