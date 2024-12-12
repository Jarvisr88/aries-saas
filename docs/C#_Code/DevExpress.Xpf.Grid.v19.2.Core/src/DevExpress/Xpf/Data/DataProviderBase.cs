namespace DevExpress.Xpf.Data
{
    using DevExpress.Data;
    using DevExpress.Data.Access;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.UI.Native.ViewGenerator;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Data.Native;
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.GridData;
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading.Tasks;
    using System.Windows;

    public abstract class DataProviderBase
    {
        protected readonly WeakReference selfReference;
        private object dataSource;
        private static readonly int[] emptyIndices = new int[0];
        private PropertyDescriptorCollection properties;
        private Dictionary<DataColumnInfo, PropertyValidator> validationAttributes;
        private int propertyValidatorsCount;
        protected static readonly ErrorInfo ValidErrorInfo;

        static DataProviderBase()
        {
            ErrorInfo info1 = new ErrorInfo();
            info1.ErrorType = ErrorType.None;
            ValidErrorInfo = info1;
        }

        protected DataProviderBase()
        {
            this.selfReference = new WeakReference(this);
        }

        public abstract void BeginCurrentRowEdit();
        public abstract void BeginUpdate();
        public abstract CriteriaOperator CalcColumnFilterCriteriaByValue(ColumnBase column, object columnValue);
        internal abstract void CancelAllGetRows();
        public abstract void CancelCurrentRowEdit();
        public abstract bool CanColumnSortCore(string fieldName);
        public bool CanGroupCollectionView() => 
            (this.CollectionViewSource != null) ? this.CollectionViewSource.CanGroup : true;

        public bool CanSortCollectionView() => 
            (this.CollectionViewSource != null) ? this.CollectionViewSource.CanSort : true;

        internal virtual bool CanValidateColumn(DataColumnInfo columnInfo) => 
            !columnInfo.Unbound;

        public abstract void ChangeGroupExpanded(int controllerRow, bool recursive);
        public virtual void ClearDetailInfo()
        {
        }

        public abstract void CollapseAll();
        private bool ColumnSupportsValidation(DataColumnInfo ci)
        {
            if ((ci == null) || ((ci.PropertyDescriptor == null) || ci.Unbound))
            {
                return false;
            }
            if ((this.validationAttributes != null) && !this.validationAttributes.ContainsKey(ci))
            {
                this.ResetValidationAttributes();
            }
            this.InitializeValidationAttributes();
            return (this.validationAttributes.ContainsKey(ci) && (this.validationAttributes[ci] != null));
        }

        internal virtual int ConvertScrollIndexToVisibleIndex(int scrollIndex, bool allowFixedGroups) => 
            scrollIndex;

        internal virtual int ConvertVisibleIndexToScrollIndex(int visibleIndex, bool allowFixedGroups) => 
            visibleIndex;

        public abstract object CorrectFilterValueType(Type columnFilteredType, object filteredValue, IFormatProvider provider = null);
        private DataErrorValidator CreatePropertyDataErrorValidator(DataColumnInfo columnInfo)
        {
            Func<DataColumnInfo, PropertyDataErrorValidator> evaluator = <>c.<>9__202_0;
            if (<>c.<>9__202_0 == null)
            {
                Func<DataColumnInfo, PropertyDataErrorValidator> local1 = <>c.<>9__202_0;
                evaluator = <>c.<>9__202_0 = y => new PropertyDataErrorValidator(y.Name);
            }
            return columnInfo.If<DataColumnInfo>(new Func<DataColumnInfo, bool>(this.CanValidateColumn)).With<DataColumnInfo, PropertyDataErrorValidator>(evaluator);
        }

        private DataErrorValidator CreateRowDataErrorValidator() => 
            new RowDataErrorValidator();

        internal virtual RowValidationObjectAccessor CreateRowValidationObjectAccessor(int controllerRow)
        {
            if (!this.DataController.IsControllerRowValid(controllerRow))
            {
                return null;
            }
            int listSourceRow = this.DataController.GetListSourceRowIndex(controllerRow);
            if (listSourceRow == -2147483648)
            {
                return null;
            }
            BaseDataControllerHelper helper = this.DataController.Helper;
            return new RowValidationObjectAccessor(() => helper.GetRow(listSourceRow) as INotifyDataErrorInfo, () => helper.GetRowDXErrorInfo(listSourceRow), () => helper.GetRowErrorInfo(listSourceRow));
        }

        internal virtual RowValidationObjectAccessor CreateRowValidationObjectAccessor(object row) => 
            new RowValidationObjectAccessor(() => row as INotifyDataErrorInfo, () => row as IDXDataErrorInfo, () => row as IDataErrorInfo);

        public abstract void DeleteRow(RowHandle rowHandle);
        public abstract void DoRefresh();
        public abstract bool EndCurrentRowEdit();
        public abstract void EndUpdate();
        protected internal void EndUpdateBase(DataViewBase view)
        {
            if (view != null)
            {
                view.UpdateScrollBarAnnotations();
                view.UpdateSearchResult(true);
                view.UpdateWatchErrors();
                view.UpdateIsCheckedForHeaderColumns();
            }
        }

        internal abstract void EnsureAllRowsLoaded(int firstRowIndex, int rowsCount);
        internal abstract void EnsureRowLoaded(int rowHandle);
        public abstract void ExpandAll();
        private static MultiErrorInfo Extract(ErrorInfo info)
        {
            ErrorInfoContainer container = info as ErrorInfoContainer;
            return ((container == null) ? ((info != null) ? MultiErrorInfo.Create(info.ErrorText, info.ErrorType) : MultiErrorInfo.Create()) : container.Finalize());
        }

        protected internal virtual void FetchMoreRows()
        {
        }

        public abstract int FindRowByRowValue(object value);
        public abstract int FindRowByValue(string fieldName, object value, params OperationCompleted[] completed);
        protected internal abstract void ForceClearData();
        internal abstract DataColumnInfo GetActualColumnInfo(string fieldName);
        public abstract int GetActualRowLevel(int rowHandle, int level);
        public abstract object GetCellValueByListIndex(int listSourceRowIndex, string fieldName);
        public abstract int GetChildRowCount(int rowHandle);
        public abstract int GetChildRowHandle(int rowHandle, int childIndex);
        public abstract int GetControllerRow(int visibleIndex);
        public abstract int GetControllerRowByGroupRow(int groupRowHandle);
        public virtual IEnumerable<int> GetDetailContainersIndices() => 
            emptyIndices;

        public virtual ErrorInfo GetErrorInfo(RowHandle rowHandle) => 
            new ErrorInfoContainer(this.GetErrorInfoCore(this.CreateRowValidationObjectAccessor(rowHandle.Value), this.CreateRowDataErrorValidator()));

        public virtual ErrorInfo GetErrorInfo(RowHandle rowHandle, string fieldName) => 
            new ErrorInfoContainer(this.GetErrorInfoCore(rowHandle.Value, this.Columns[fieldName], this.CreateRowValidationObjectAccessor(rowHandle.Value)));

        protected internal virtual ErrorInfo GetErrorInfo(int rowHandle, object row) => 
            new ErrorInfoContainer(this.GetErrorInfoCore(this.CreateRowValidationObjectAccessor(row), this.CreateRowDataErrorValidator()));

        protected internal virtual ErrorInfo GetErrorInfo(int rowHandle, DataColumnInfo ci, object row) => 
            new ErrorInfoContainer(this.GetErrorInfoCore(rowHandle, ci, this.CreateRowValidationObjectAccessor(row)));

        private MultiErrorInfo GetErrorInfoCore(RowValidationObjectAccessor accessor, DataErrorValidator dataErrorValidator) => 
            ((accessor == null) || (dataErrorValidator == null)) ? MultiErrorInfo.Create() : dataErrorValidator.Validate(accessor, this.ValidatesOnNotifyDataErrors);

        private MultiErrorInfo GetErrorInfoCore(int rowHandle, DataColumnInfo ci, RowValidationObjectAccessor accessor)
        {
            Func<MultiErrorInfo, bool> evaluator = <>c.<>9__196_0;
            if (<>c.<>9__196_0 == null)
            {
                Func<MultiErrorInfo, bool> local1 = <>c.<>9__196_0;
                evaluator = <>c.<>9__196_0 = x => x.HasErrors();
            }
            MultiErrorInfo local2 = this.GetValidationAttributeErrorInfo(rowHandle, ci).If<MultiErrorInfo>(evaluator);
            MultiErrorInfo errorInfoCore = local2;
            if (local2 == null)
            {
                MultiErrorInfo local3 = local2;
                errorInfoCore = this.GetErrorInfoCore(accessor, this.CreatePropertyDataErrorValidator(ci));
            }
            return errorInfoCore;
        }

        protected internal virtual object GetFormatInfoCellValueByListIndex(int listIndex, string fieldName) => 
            this.GetCellValueByListIndex(listIndex, fieldName);

        internal abstract int GetGroupIndex(string fieldName);
        public abstract object GetGroupRowValue(int rowHandle);
        public abstract object GetGroupRowValue(int rowHandle, ColumnBase column);
        public abstract int GetListIndexByRowHandle(int rowHandle);
        internal static Type GetListItemPropertyType(IList list, ICollectionView collectionView)
        {
            if (list == null)
            {
                return null;
            }
            Type listType = null;
            listType = ((collectionView == null) || (collectionView.SourceCollection == null)) ? ListDataControllerHelper.GetListType(list) : collectionView.SourceCollection.GetType();
            if (listType.IsArray && (list.Count == 0))
            {
                return listType.GetElementType();
            }
            Type indexerPropertyType = ListDataControllerHelper.GetIndexerPropertyType(listType);
            if (indexerPropertyType == null)
            {
                indexerPropertyType = GenericTypeHelper.GetGenericIListTypeArgument(listType);
                if (indexerPropertyType == typeof(object))
                {
                    indexerPropertyType = null;
                }
            }
            return indexerPropertyType;
        }

        internal MultiErrorInfo GetMultiErrorInfo(RowHandle rowHandle) => 
            Extract(this.GetErrorInfo(rowHandle));

        internal MultiErrorInfo GetMultiErrorInfo(RowHandle rowHandle, string fieldName) => 
            Extract(this.GetErrorInfo(rowHandle, fieldName));

        internal MultiErrorInfo GetMultiErrorInfo(int rowHandle, object row) => 
            Extract(this.GetErrorInfo(rowHandle, row));

        internal MultiErrorInfo GetMultiErrorInfo(int rowHandle, DataColumnInfo ci, object row) => 
            Extract(this.GetErrorInfo(rowHandle, ci, row));

        public virtual object GetNodeIdentifier(int rowHandle) => 
            this.GetListIndexByRowHandle(rowHandle);

        public abstract int GetParentRowHandle(int rowHandle);
        public int GetParentRowIndex(int visibleIndex)
        {
            int parentRowHandle = this.GetParentRowHandle(this.GetControllerRow(visibleIndex));
            return this.GetRowVisibleIndexByHandle(parentRowHandle);
        }

        internal PropertyDescriptorCollection GetProperties()
        {
            if (this.properties == null)
            {
                List<PropertyDescriptor> list = new List<PropertyDescriptor>();
                int num = 0;
                while (true)
                {
                    if (num >= this.Columns.Count)
                    {
                        int num2 = 0;
                        while (true)
                        {
                            if (num2 >= this.DetailColumns.Count)
                            {
                                this.properties = new PropertyDescriptorCollection(list.ToArray());
                                break;
                            }
                            DataColumnInfo info2 = this.DetailColumns[num2];
                            list.Add(new RowPropertyDescriptor(info2));
                            num2++;
                        }
                        break;
                    }
                    DataColumnInfo info = this.Columns[num];
                    list.Add(new RowPropertyDescriptor(info));
                    num++;
                }
            }
            return this.properties;
        }

        public abstract object GetRowByListIndex(int listSourceRowIndex);
        public abstract RowDetailContainer GetRowDetailContainer(int controllerRow, Func<RowDetailContainer> createContainerDelegate, bool createNewIfNotExist);
        public abstract int GetRowHandleByListIndex(int listIndex);
        public abstract int GetRowLevelByControllerRow(int rowHandle);
        public int GetRowLevelByVisibleIndex(int visibleIndex) => 
            this.GetRowLevelByControllerRow(this.GetControllerRow(visibleIndex));

        [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Instead use the GetListIndexByRowHandle method. For detailed information, see the list of breaking changes in DXperience v2012 vol 1.")]
        public int GetRowListIndex(int rowHandle) => 
            this.GetListIndexByRowHandle(rowHandle);

        public virtual IEnumerable<int> GetRowListIndicesWithExpandedDetails() => 
            emptyIndices;

        public abstract DependencyObject GetRowState(int controllerRow, bool createNewIfNotExist);
        public abstract object GetRowValue(int rowHandle);
        public abstract object GetRowValue(int rowHandle, DataColumnInfo info);
        public abstract object GetRowValue(int rowHandle, string fieldName);
        public abstract object GetRowValue(int rowHandle, object row, string fieldName);
        protected virtual object GetRowValueForValidationAttribute(int controllerRow, string columnName) => 
            this.DataController.GetRowValue(controllerRow, columnName);

        public abstract int GetRowVisibleIndexByHandle(int rowHandle);
        internal virtual int GetSummaryRowCountBeforeRow(int rowHandle) => 
            0;

        internal abstract object GetSummaryValue(string propertyName, SummaryItemType summaryType, ColumnValuesArguments args, bool ignoreNulls = true);
        public abstract object GetTotalSummaryValue(DevExpress.Xpf.Grid.SummaryItemBase item);
        protected UnboundColumnInfoCollection GetUnboundColumnsCore(IEnumerable<IColumnInfo> columns)
        {
            UnboundColumnInfoCollection infos = new UnboundColumnInfoCollection();
            foreach (IColumnInfo info in columns)
            {
                if ((info.UnboundType != UnboundColumnType.Bound) && !string.IsNullOrEmpty(info.FieldName))
                {
                    bool visible = !(info is ConditionalFormattingColumnInfo);
                    infos.Add(new UnboundColumnInfo(info.FieldName, info.UnboundType, false, info.UnboundExpression, visible));
                }
            }
            return ((infos.Count <= 0) ? null : infos);
        }

        public abstract object[] GetUniqueColumnValues(ColumnBase column, bool includeFilteredOut, bool roundDateTime, bool implyNullLikeEmptyStringWhenFiltering = false, CriteriaOperator columnFilter = null);
        internal abstract System.Threading.Tasks.Task<UniqueValues> GetUniqueColumnValuesWithCounts(string propertyName, ColumnValuesArguments args, bool includeCounts, bool allowThrottle);
        private MultiErrorInfo GetValidationAttributeErrorInfo(int controllerRow, DataColumnInfo ci) => 
            MultiErrorInfo.Create((((ci != null) && (this.ValidationOwner != null)) && this.ValidationOwner.CalculateValidationAttribute(ci.Name, controllerRow)) ? this.GetValidationAttributesErrorText(controllerRow, ci) : null, ErrorType.Default);

        public string GetValidationAttributesErrorText(int controllerRow, DataColumnInfo columnInfo) => 
            this.ColumnSupportsValidation(columnInfo) ? this.GetValidationAttributesErrorTextCore(controllerRow, columnInfo, this.GetRowValueForValidationAttribute(controllerRow, columnInfo.Name)) : string.Empty;

        public string GetValidationAttributesErrorText(object value, int controllerRow, string columnName)
        {
            DataColumnInfo ci = this.Columns[columnName];
            return (this.ColumnSupportsValidation(ci) ? this.GetValidationAttributesErrorTextCore(controllerRow, ci, value) : string.Empty);
        }

        private string GetValidationAttributesErrorTextCore(int controllerRow, DataColumnInfo ci, object value)
        {
            object rowValue = this.GetRowValue(controllerRow);
            if ((rowValue == null) || AsyncServerModeDataController.IsNoValue(rowValue))
            {
                return string.Empty;
            }
            object obj3 = value;
            try
            {
                obj3 = ci.ConvertValue(value, true);
            }
            catch
            {
                return string.Empty;
            }
            return this.GetValidationAttributesErrorTextCore(obj3, rowValue, ci);
        }

        public string GetValidationAttributesErrorTextCore(object value, object instance, DataColumnInfo ci)
        {
            if (!this.ColumnSupportsValidation(ci) || AsyncServerModeDataController.IsNoValue(instance))
            {
                return string.Empty;
            }
            ComplexPropertyDescriptor propertyDescriptor = ci.PropertyDescriptor as ComplexPropertyDescriptor;
            if (propertyDescriptor != null)
            {
                instance = propertyDescriptor.GetOwnerOfLast(instance);
                if (instance == null)
                {
                    return string.Empty;
                }
            }
            return this.validationAttributes[ci].GetErrorText(value, instance);
        }

        public virtual object GetVisibleIndexByScrollIndex(int scrollIndex) => 
            scrollIndex;

        public object GetWpfRow(RowHandle rowHandle, int listSourceRowIndex = -1) => 
            new RowTypeDescriptor(this.selfReference, rowHandle, listSourceRowIndex);

        public bool HasValidationAttributes()
        {
            this.InitializeValidationAttributes();
            return (this.propertyValidatorsCount > 0);
        }

        private void InitializeValidationAttributes()
        {
            if (this.validationAttributes == null)
            {
                this.validationAttributes = new Dictionary<DataColumnInfo, PropertyValidator>();
                this.propertyValidatorsCount = 0;
                Type itemType = this.ItemType;
                foreach (DataColumnInfo info in this.Columns)
                {
                    PropertyValidator validator = DataColumnAttributesExtensions.CreatePropertyValidator(info.PropertyDescriptor, itemType);
                    this.validationAttributes.Add(info, validator);
                    if (validator != null)
                    {
                        this.propertyValidatorsCount++;
                    }
                }
            }
        }

        protected internal virtual void InvalidateRowPropertyDescriptors()
        {
            this.properties = null;
        }

        public virtual void InvalidateVisibleIndicesCache()
        {
        }

        public bool IsColumnReadonly(string fieldName, bool requireColumn)
        {
            if (!this.AllowEdit)
            {
                return true;
            }
            DataColumnInfo info = this.Columns[fieldName];
            return ((info != null) ? info.ReadOnly : requireColumn);
        }

        internal virtual bool IsFilteredByRowHandle(int rowHandle) => 
            false;

        public abstract bool IsGroupRow(int visibleIndex);
        public abstract bool IsGroupRowExpanded(int controllerRow);
        public abstract bool IsGroupRowHandle(int rowHandle);
        public abstract bool IsGroupVisible(GroupRowInfo groupInfo);
        public abstract bool IsRowVisible(int rowHandle);
        internal virtual bool IsSummaryItemExists(SummaryItemCollectionType collectionType, DevExpress.Xpf.Grid.SummaryItemBase item) => 
            true;

        internal virtual bool IsUnboundColumnInfo(DataColumnInfo info) => 
            (info != null) && info.Unbound;

        public abstract bool IsValidRowHandle(int rowHandle);
        public abstract void MakeRowVisible(int rowHandle);
        protected internal virtual void OnAsyncTotalsReceived()
        {
            if (!this.IsValidRowHandle(this.CurrentControllerRow))
            {
                this.CurrentControllerRow = this.GetControllerRow(0);
            }
        }

        protected internal abstract void OnDataSourceChanged();
        public virtual bool OnShowingGroupFooter(int rowHandle, int level) => 
            false;

        public abstract void RefreshRow(int rowHandle);
        public virtual void RemoveRowDetailContainer(int controllerRow)
        {
        }

        public virtual void RePopulateColumns()
        {
            this.ResetValidationAttributes();
            this.DataController.RePopulateColumns(false);
        }

        public void ResetValidationAttributes()
        {
            this.validationAttributes = null;
            this.propertyValidatorsCount = 0;
        }

        internal abstract void ScheduleAutoPopulateColumns();
        public abstract void SetCellValueByListIndex(int listSourceRowIndex, string fieldName, object value);
        public abstract void SetRowValue(RowHandle rowHandle, DataColumnInfo info, object value);
        public void SetRowValue(int rowHandle, string fieldName, object value)
        {
            this.SetRowValue(new RowHandle(rowHandle), this.Columns[fieldName], value);
        }

        public abstract void Synchronize(IList<GridSortInfo> sortList, int groupCount, CriteriaOperator filterCriteria, bool sortUpdate);
        public abstract void SynchronizeSummary();
        protected internal virtual void ThrowNotSupportedExceptionIfInServerMode()
        {
            if (this.IsServerMode || this.IsAsyncServerMode)
            {
                throw new NotSupportedInMasterDetailException("Server and Instant Feedback UI modes are not supported in master-detail mode. For a complete list of limitations, please see http://go.devexpress.com/XPF-MasterDetail-Limitations.aspx");
            }
        }

        public abstract bool TryGetGroupSummaryValue(int rowHandle, DevExpress.Xpf.Grid.SummaryItemBase item, out object value);
        public abstract void UpdateGroupSummary();
        protected internal virtual void UpdateLoadingRowState(LoadingRowData rowData)
        {
        }

        public abstract void UpdateTotalSummary();
        protected internal void UpdateVisibleItems()
        {
            int? oldRowHandle = null;
            this.UpdateVisibleItems(ListChangedType.Reset, -1, oldRowHandle);
        }

        protected internal virtual void UpdateVisibleItems(ListChangedType changedType, int newRowHandle, int? oldRowHandle)
        {
        }

        protected internal virtual void UpdateVisibleItemsItem(int controllerRowHandle)
        {
            if ((this.VisibleItems != null) && ((controllerRowHandle >= 0) && (controllerRowHandle < this.VisibleItems.Count)))
            {
                this.VisibleItems[controllerRowHandle] = this.GetRowValue(controllerRowHandle);
            }
        }

        public object DataSource
        {
            get => 
                this.dataSource;
            set
            {
                if (this.DataSource != value)
                {
                    this.dataSource = value;
                    this.OnDataSourceChanged();
                }
            }
        }

        internal Type ItemType
        {
            get
            {
                try
                {
                    return this.ItemTypeCore;
                }
                catch (Exception)
                {
                    return null;
                }
            }
        }

        internal virtual IRefreshable RefreshableSource =>
            null;

        internal virtual BindingListAdapterBase BindingListAdapter =>
            null;

        protected abstract Type ItemTypeCore { get; }

        public abstract bool AutoExpandAllGroups { get; set; }

        public abstract int GroupedColumnCount { get; }

        internal virtual bool SubscribeRowChangedForVisibleRows =>
            false;

        internal virtual bool IsSelfManagedItemsSource =>
            true;

        public abstract ISummaryItemOwner TotalSummaryCore { get; }

        public abstract ISummaryItemOwner GroupSummaryCore { get; }

        public abstract int DataRowCount { get; }

        public abstract int ListSourceRowCount { get; }

        public abstract bool IsCurrentRowEditing { get; }

        public abstract int VisibleCount { get; }

        public abstract int CurrentControllerRow { get; set; }

        public abstract int CurrentIndex { get; }

        public abstract DataColumnInfoCollection Columns { get; }

        public abstract DataColumnInfoCollection DetailColumns { get; }

        public abstract bool IsReady { get; }

        public ICollectionView CollectionViewSource =>
            this.DataSource as ICollectionView;

        protected internal abstract BaseGridController DataController { get; }

        internal abstract bool IsServerMode { get; }

        internal abstract bool IsICollectionView { get; }

        internal abstract bool IsAsyncServerMode { get; }

        internal abstract bool IsAsyncOperationInProgress { get; set; }

        internal abstract bool IsRefreshInProgress { get; }

        public abstract CriteriaOperator FilterCriteria { get; set; }

        public abstract bool IsUpdateLocked { get; }

        public abstract DevExpress.Data.ValueComparer ValueComparer { get; }

        internal virtual VirtualSourceBase VirtualSource =>
            null;

        internal PagedSourceBase PagedSource =>
            (PagedSourceBase) this.VirtualSource;

        internal IVirtualSourceAccess VirtualSourceAccess =>
            this.VirtualSource;

        internal bool IsVirtualSource =>
            this.VirtualSource != null;

        internal bool IsPagedSource =>
            this.VirtualSource is PagedSourceBase;

        public abstract ISelectionController Selection { get; }

        public abstract bool AllowEdit { get; }

        public virtual ObservableCollectionCore<object> VisibleItems =>
            null;

        public virtual string ItemChangedPropertyDescriptorName =>
            string.Empty;

        public virtual bool ShowGroupSummaryFooter =>
            false;

        protected internal virtual bool AllowUpdateFocusedRowData =>
            true;

        internal virtual bool CanFilter =>
            true;

        internal virtual IValidationAttributeOwner ValidationOwner =>
            this.DataController.VisualClient as IValidationAttributeOwner;

        internal virtual bool ValidatesOnNotifyDataErrors =>
            false;

        public virtual bool? AllowLiveDataShaping =>
            null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataProviderBase.<>c <>9 = new DataProviderBase.<>c();
            public static Func<MultiErrorInfo, bool> <>9__196_0;
            public static Func<DataColumnInfo, PropertyDataErrorValidator> <>9__202_0;

            internal PropertyDataErrorValidator <CreatePropertyDataErrorValidator>b__202_0(DataColumnInfo y) => 
                new PropertyDataErrorValidator(y.Name);

            internal bool <GetErrorInfoCore>b__196_0(MultiErrorInfo x) => 
                x.HasErrors();
        }

        private sealed class ErrorInfoContainer : ErrorInfo
        {
            private readonly MultiErrorInfo info;

            internal ErrorInfoContainer(MultiErrorInfo info)
            {
                MultiErrorInfo info1 = info;
                if (info == null)
                {
                    MultiErrorInfo local1 = info;
                    info1 = MultiErrorInfo.Create();
                }
                this.info = info1;
                base.ErrorText = info.ErrorText;
                base.ErrorType = info.ErrorType;
            }

            internal MultiErrorInfo Finalize() => 
                ((base.ErrorText != this.info.ErrorText) || (base.ErrorType != this.info.ErrorType)) ? MultiErrorInfo.Create(base.ErrorText, base.ErrorType) : this.info;
        }
    }
}

