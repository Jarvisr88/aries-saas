namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Data.Helpers;
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Filtering.Native;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Threading;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Markup;

    public abstract class ExcelColumnFilterInfoBase : ListColumnFilterInfoBase, INotifyPropertyChanged
    {
        private Locker ClearFilterLocker;
        private bool _IsWaitIndicatorVisible;
        private ExcelColumnFilterValuesListBase _Items;
        private string _SearchText;
        private bool PopupInitialized;
        private ExcelColumnCustomFilterInfo _CustomFilterInfo;
        private ExcelColumnFilterType _FilterType;
        private BaseEditSettings _ValueColumnEditSettings;
        private ICommand _ClearFilterCommand;
        protected PopupBaseEdit Popup;

        public event PropertyChangedEventHandler PropertyChanged;

        public ExcelColumnFilterInfoBase(ColumnBase column) : base(column)
        {
            this.ClearFilterLocker = new Locker();
            this.ClearFilterCommand = new DelegateCommand(new Action(this.ClearFilter));
        }

        protected override void AfterPopupOpening(PopupBaseEdit popup)
        {
            base.AfterPopupOpening(popup);
            this.CreateColumnFilterInfos(null);
        }

        protected override bool AllowColumnFilterValues() => 
            this.CanShowValues();

        public override bool CanShowFilterPopup() => 
            this.CanShowValues() || this.CanShowRules();

        protected virtual bool CanShowRules() => 
            this.CreateRulesOperators().ToList<ClauseType>().Count > 0;

        protected virtual bool CanShowValues() => 
            (!this.GetIsNullableColumn() || !base.Column.AllowFilter(AllowedUnaryFilters.IsNullOrEmpty)) ? this.CanShowValuesCore() : true;

        protected abstract bool CanShowValuesCore();
        private void ClearFilter()
        {
            this.ClearRulesFilter();
            this.ClearValuesFilter();
            Func<CriteriaOperator> getOperator = <>c.<>9__72_0;
            if (<>c.<>9__72_0 == null)
            {
                Func<CriteriaOperator> local1 = <>c.<>9__72_0;
                getOperator = <>c.<>9__72_0 = (Func<CriteriaOperator>) (() => null);
            }
            this.UpdateColumnFilterIfNeeded(getOperator);
        }

        protected override void ClearPopupData(PopupBaseEdit popup)
        {
            if (this.FilterItems != null)
            {
                this.FilterItems.IsCheckedChanged -= new EventHandler(this.Items_IsCheckedChanged);
            }
            this.FilterSettings = null;
            this.PopupInitialized = false;
            this.Popup = null;
            this.FilterItems = null;
            this._SearchText = null;
            this.CustomFilterInfo = null;
            this.FilterType = ExcelColumnFilterType.FilterValues;
        }

        private void ClearRulesFilter()
        {
            this.CustomFilterInfo.Reset();
        }

        private void ClearValuesFilter()
        {
            this.ClearFilterLocker.DoLockedAction(delegate {
                this.SearchText = null;
                this.FilterItems.ChangeSelectAll(false, true);
            });
        }

        private bool CreateAndFillFilterItemsCollection(IList<object> columnFilterValues, CriteriaOperator columnFilter)
        {
            ExcelColumnFilterValuesListBase list = this.CreateList();
            list.AddSelectAllItem(base.View.GetDefaultFilterItemLocalizedString(DefaultFilterItem.PopupFilterAll), ReferenceEquals(columnFilter, null), true);
            list.AddSearchAddFilterItem(base.View.GetLocalizedString(GridControlStringId.AddCurrentSelectionToFilter));
            bool shouldCreateBlanks = false;
            this.FillList(list, columnFilterValues, out shouldCreateBlanks);
            if (shouldCreateBlanks && base.Column.AllowFilter(AllowedUnaryFilters.IsNullOrEmpty))
            {
                list.AddSelectBlanksItem(base.View.GetDefaultFilterItemLocalizedString(DefaultFilterItem.PopupFilterBlanks));
            }
            bool flag2 = false;
            this.FilterItems = list;
            flag2 = this.UpdateSelectedItems(columnFilterValues, columnFilter);
            this.UpdateExpandState();
            return flag2;
        }

        private void CreateColumnFilterInfos(object[] values)
        {
            CriteriaOperator columnFilterCriteria = base.View.DataControl.GetColumnFilterCriteria(base.Column);
            IList<object> columnFilterValues = (this.FilterSettings != null) ? this.FilterSettings.FilterItems : ((values != null) ? values.ToList<object>() : new List<object>());
            bool useActualColumnFilter = this.CreateAndFillFilterItemsCollection(columnFilterValues, columnFilterCriteria);
            this.SubscribeEvents();
            this.CreateCustomFilterInfo(values, useActualColumnFilter, columnFilterCriteria);
            this.SetCurrentStateFromFilterSettings();
        }

        internal override PopupBaseEdit CreateColumnFilterPopup()
        {
            PopupBaseEdit edit1;
            DataViewBase view = base.View;
            if (view != null)
            {
                edit1 = view.CreateExcelColumnFilterPopupEditor();
            }
            else
            {
                DataViewBase local1 = view;
                edit1 = null;
            }
            this.Popup = edit1;
            if (this.Popup != null)
            {
                this.Popup.IsTextEditable = false;
                this.Popup.ShowNullText = false;
                SetDefaultPopupSize(this.Popup, base.Column);
                this.PopupInitialized = false;
            }
            return this.Popup;
        }

        public CriteriaOperator CreateCustomFilterCriteria(string fieldName, object value, ClauseType clauseType) => 
            base.View.CreateAutoFilterCriteria(fieldName, value, clauseType);

        private void CreateCustomFilterInfo(object[] values, bool useActualColumnFilter, CriteriaOperator columnFilter)
        {
            this.CustomFilterInfo = new ExcelColumnCustomFilterInfo(this, values, columnFilter);
            if (!useActualColumnFilter && (this.CustomFilterInfo.FilterCriteria != null))
            {
                this.FilterType = ExcelColumnFilterType.FilterRules;
            }
        }

        protected internal virtual GridFilterColumn CreateFilterColumn() => 
            base.Column.View.DataControl.CreateGridFilterColumn(base.Column, false);

        protected override IList CreateFilterList() => 
            new List<object>();

        protected abstract ExcelColumnFilterValuesListBase CreateList();
        private ControlTemplate CreatePopupTemplate() => 
            XamlReader.Parse("<ControlTemplate xmlns=\"http://schemas.microsoft.com/winfx/2006/xaml/presentation\" xmlns:dxg=\"http://schemas.devexpress.com/winfx/2008/xaml/grid\">\r\n                                        <dxg:ExcelColumnFilterContentPresenter ColumnFilterInfo=\"{Binding Path=DataContext, RelativeSource={RelativeSource TemplatedParent}}\"/>\r\n                                    </ControlTemplate>") as ControlTemplate;

        [IteratorStateMachine(typeof(<CreateRulesOperators>d__84))]
        protected internal IEnumerable<ClauseType> CreateRulesOperators()
        {
            <CreateRulesOperators>d__84 d__1 = new <CreateRulesOperators>d__84(-2);
            d__1.<>4__this = this;
            return d__1;
        }

        private void CustomFilterInfo_FilterCriteriaChanged(object sender, EventArgs e)
        {
            this.ClearValuesFilter();
            this.UpdateColumnFilterIfNeeded(new Func<CriteriaOperator>(this.GetFilterCriteriaCore));
        }

        protected abstract void FillList(ExcelColumnFilterValuesListBase list, IList<object> columnFilterValues, out bool shouldCreateBlanks);
        protected abstract string GetActualSearchText();
        protected abstract List<DateTime> GetAllDates(ExcelColumnFilterValuesListBase items, out bool hasBlankItem);
        protected override CriteriaOperator GetColumnCriteriaOperator()
        {
            if (this.CanShowValuesCore())
            {
                return base.GetColumnCriteriaOperator();
            }
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(base.Column.FieldName) };
            return new FunctionOperator(FunctionOperatorType.IsNullOrEmpty, operands);
        }

        private List<CriteriaOperator> GetCriteriaOperatorsFromCustomItem(ExcelColumnFilterValue customItem)
        {
            CriteriaOperator editValue = (CriteriaOperator) customItem.EditValue;
            GroupOperator operator2 = editValue as GroupOperator;
            if ((operator2 != null) && (operator2.OperatorType == GroupOperatorType.Or))
            {
                return operator2.Operands.ToList<CriteriaOperator>();
            }
            List<CriteriaOperator> list1 = new List<CriteriaOperator>();
            list1.Add(editValue);
            return list1;
        }

        protected internal abstract string GetDisplayText(object value, string originalDisplayText);
        protected override string GetEditValueString(object item) => 
            item as string;

        protected override ExcelColumnFilterSettings GetExcelColumnCustomFilterSettings() => 
            this.FilterSettings;

        protected internal override CriteriaOperator GetFilterCriteria() => 
            this.GetFilterCriteria(this.GetSelectedItemsForView());

        protected abstract CriteriaOperator GetFilterCriteria(List<ExcelColumnFilterValue> selectedItems);
        protected override CriteriaOperator GetFilterCriteriaCore()
        {
            ExcelColumnFilterType filterType = this.FilterType;
            return (((filterType == ExcelColumnFilterType.FilterValues) || (filterType != ExcelColumnFilterType.FilterRules)) ? this.GetFilterCriteriaFromValues() : this.CustomFilterInfo.FilterCriteria);
        }

        protected virtual CriteriaOperator GetFilterCriteriaFromValues()
        {
            List<ExcelColumnFilterValue> selectedItemsForView = this.GetSelectedItemsForView();
            List<object> selectedItems = (selectedItemsForView == null) ? null : selectedItemsForView.Cast<object>().ToList<object>();
            return base.View.GetCheckedFilterPopupFilterCriteria(base.Column, selectedItems);
        }

        protected override bool GetIncludeFilteredOut() => 
            base.Column.GetShowAllTableValuesInFilterPopup() || base.Column.IsFiltered;

        private bool GetIsCustomCriteriaItemChecked(ExcelColumnFilterValue customItem, List<CriteriaOperator> customOperators)
        {
            bool flag;
            using (List<CriteriaOperator>.Enumerator enumerator = this.GetCriteriaOperatorsFromCustomItem(customItem).GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        CriteriaOperator current = enumerator.Current;
                        if (customOperators.Contains(current))
                        {
                            continue;
                        }
                        flag = false;
                    }
                    else
                    {
                        return true;
                    }
                    break;
                }
            }
            return flag;
        }

        protected internal virtual ClauseType? GetRulesDefaultFilterType(GridFilterColumn filterColumn = null)
        {
            filterColumn ??= this.CreateFilterColumn();
            return FilterClauseHelper.GetDefaultOperation(base.Column, true, false);
        }

        protected internal override IEnumerable GetSelectedItems(IEnumerable items, CriteriaOperator op)
        {
            if (op == null)
            {
                return null;
            }
            bool hasBlankItem = false;
            List<DateTime> allDates = this.GetAllDates(this.FilterItems, out hasBlankItem);
            IEnumerable<object> selectedItems = null;
            List<CriteriaOperator> customOperators = null;
            selectedItems = ((allDates.Count + (hasBlankItem ? 1 : 0)) <= 0) ? ColumnFilterInfoHelper.GetSelectedItems(op, out customOperators) : MultiselectRoundedDateTimeFilterHelper.GetCheckedDates(op, base.Column.FieldName, allDates).Cast<object>();
            List<object> list3 = new List<object>();
            foreach (object obj2 in ColumnFilterInfoHelper.AddNullOrEmptyOperator(selectedItems, op))
            {
                ExcelColumnFilterValue value2 = this.FilterItems.FindItem(obj2, base.Column.ColumnFilterMode);
                if (value2 != null)
                {
                    list3.Add(obj2);
                }
            }
            if ((customOperators != null) && (customOperators.Count > 0))
            {
                foreach (ExcelColumnFilterValue value3 in this.FilterItems.GetItemsWithCriteriaOperator())
                {
                    if (this.GetIsCustomCriteriaItemChecked(value3, customOperators))
                    {
                        list3.Add(value3.EditValue);
                    }
                }
            }
            return (((list3.Count != this.FilterItems.FilterValuesCount) || base.Column.IsFiltered) ? list3 : null);
        }

        protected abstract IEnumerable<ExcelColumnFilterValue> GetSelectedItemsCore();
        private List<ExcelColumnFilterValue> GetSelectedItemsForView()
        {
            if (this.IsFilterListContainsEmptyCriteria())
            {
                return null;
            }
            IEnumerable<ExcelColumnFilterValue> source = null;
            source = !this.FilterItems.AllItemsVisible ? (this.FilterItems.SelectedItemsAddedToFilter ? this.GetSelectedItemsCore() : this.GetVisibleSelectedItemsCore()) : this.GetSelectedItemsCore();
            return source.ToList<ExcelColumnFilterValue>();
        }

        protected object GetValue(object item)
        {
            ExcelColumnFilterValue value2 = item as ExcelColumnFilterValue;
            return value2?.GetComputedValue();
        }

        protected abstract BaseEditSettings GetValueEditSettings();
        protected abstract IEnumerable<ExcelColumnFilterValue> GetVisibleSelectedItemsCore();
        protected bool IsFilterListContainsEmptyCriteria() => 
            (this.FilterItems != null) ? (!this.FilterItems.AllItemsVisible ? (this.FilterItems.SelectedItemsAddedToFilter && this.FilterItems.IsAllItemsChecked) : (this.FilterItems.IsSelectAll || this.FilterItems.IsSelectNone)) : true;

        private void Items_IsCheckedChanged(object sender, EventArgs e)
        {
            this.UpdateValuesFilter();
        }

        protected void OnPropertyChanged(string propName)
        {
            if (this.PropertyChanged != null)
            {
                this.PropertyChanged(this, new PropertyChangedEventArgs(propName));
            }
        }

        private void RaiseIsFilteredChanged()
        {
            this.OnPropertyChanged("IsFiltered");
        }

        private void SetCurrentStateFromFilterSettings()
        {
            if (!this.CanShowRules() || !this.CanShowValues())
            {
                if (!this.CanShowValues())
                {
                    this.AllowedFilterTypes = 1;
                    this.FilterType = ExcelColumnFilterType.FilterRules;
                }
                if (!this.CanShowRules())
                {
                    this.AllowedFilterTypes = 0;
                    this.FilterType = ExcelColumnFilterType.FilterValues;
                }
            }
            if (this.FilterSettings != null)
            {
                if (this.AllowedFilterTypes == null)
                {
                    this.AllowedFilterTypes = this.FilterSettings.AllowedFilterTypes;
                }
                if (this.AllowedFilterTypes != null)
                {
                    this.FilterType = this.AllowedFilterTypes.Value;
                }
                else if (this.FilterSettings.DefaultFilterType != null)
                {
                    this.FilterType = this.FilterSettings.DefaultFilterType.Value;
                }
            }
        }

        internal static void SetDefaultPopupSize(PopupBaseEdit popup, ColumnBase column)
        {
            bool flag = ThemeManager.GetIsTouchlineTheme(column.View) || ThemeManager.GetIsTouchEnabled(column.View);
            popup.PopupHeight = popup.PopupMinHeight = flag ? ((double) 390) : ((double) 310);
            popup.PopupMinWidth = flag ? ((double) 320) : ((double) 260);
        }

        protected override void ShowWaitIndicator(IList list)
        {
            this.IsWaitIndicatorVisible = true;
        }

        private void SubscribeEvents()
        {
            this.FilterItems.IsCheckedChanged += new EventHandler(this.Items_IsCheckedChanged);
        }

        protected override void UpdateColumnFilterIfNeeded(Func<CriteriaOperator> getOperator)
        {
            base.UpdateColumnFilterIfNeeded(getOperator);
            this.RaiseIsFilteredChanged();
        }

        protected virtual void UpdateExpandState()
        {
        }

        protected abstract void UpdateFilter(string oldFilter, string newFilter);
        protected override void UpdatePopupData(PopupBaseEdit popup)
        {
            if (!this.PopupInitialized)
            {
                popup.DataContext = this;
                popup.PopupContentTemplate = this.CreatePopupTemplate();
                this.ValueColumnEditSettings = this.GetValueEditSettings();
                this.PopupInitialized = true;
                this.UpdatePopupData(popup, null);
            }
        }

        protected override void UpdatePopupData(PopupBaseEdit popup, object[] values)
        {
            bool isColumnFilterItemsLoading = false;
            IList list = base.GetColumnFilterItems(values, new Func<IList>(this.CreateFilterList), new Action<object[]>(this.SortColumnFilterItems), this.GetIncludeFilteredOut(), this.RoundDateTimeValues, this.ImplyNullLikeEmptyStringWhenFiltering, this.AddNullItem, out isColumnFilterItemsLoading);
            base.IsColumnFilterItemsLoading = isColumnFilterItemsLoading;
            ExcelColumnFilterSettings settings = new ExcelColumnFilterSettings {
                FilterItems = list as List<object>,
                DefaultRulesFilterType = this.GetRulesDefaultFilterType(null)
            };
            this.FilterSettings = settings;
            if (base.IsColumnFilterItemsLoading)
            {
                this.ShowWaitIndicator(list);
            }
            else if (this.IsWaitIndicatorVisible)
            {
                this.IsWaitIndicatorVisible = false;
                this.CreateColumnFilterInfos(values);
                this.RaiseIsFilteredChanged();
            }
        }

        private bool UpdateSelectedItems(IEnumerable items, CriteriaOperator op)
        {
            if (op == null)
            {
                this.FilterItems.ChangeSelectAll(false);
                return false;
            }
            IEnumerable enumerable = base.View.GetCheckedFilterPopupSelectedItems(base.Column, items, op);
            if (enumerable == null)
            {
                return false;
            }
            bool flag = false;
            List<ExcelColumnFilterValue> itemsWithCriteriaOperator = this.FilterItems.GetItemsWithCriteriaOperator();
            foreach (object obj2 in enumerable)
            {
                ExcelColumnFilterValue value2 = null;
                if (obj2 is CriteriaOperator)
                {
                    CriteriaOperator operatorValue = (CriteriaOperator) obj2;
                    value2 = itemsWithCriteriaOperator.FirstOrDefault<ExcelColumnFilterValue>(i => i.EditValue == operatorValue);
                }
                else
                {
                    value2 = this.FilterItems.FindItem(obj2, base.Column.ColumnFilterMode);
                }
                if (value2 != null)
                {
                    value2.IsChecked = new bool?(flag = true);
                }
            }
            return flag;
        }

        protected override void UpdateSizeGripVisibility(PopupBaseEdit popup)
        {
            popup.ShowSizeGrip = true;
        }

        protected internal void UpdateValuesFilter()
        {
            this.ClearFilterLocker.DoIfNotLocked(delegate {
                this.ClearRulesFilter();
                this.UpdateColumnFilterIfNeeded(new Func<CriteriaOperator>(this.GetFilterCriteriaCore));
            });
        }

        protected override bool AddNullItem =>
            true;

        public abstract bool ShowSearchPanelScopeSelector { get; }

        public bool IsWaitIndicatorVisible
        {
            get => 
                this._IsWaitIndicatorVisible;
            private set
            {
                this._IsWaitIndicatorVisible = value;
                this.OnPropertyChanged("IsWaitIndicatorVisible");
            }
        }

        public ExcelColumnFilterValuesListBase FilterItems
        {
            get => 
                this._Items;
            set
            {
                this._Items = value;
                this.OnPropertyChanged("FilterItems");
            }
        }

        public ExcelColumnFilterType? AllowedFilterTypes { get; set; }

        public abstract bool IsHierarchicalView { get; }

        public abstract string ChildPropertyName { get; }

        public ExcelColumnCustomFilterInfo CustomFilterInfo
        {
            get => 
                this._CustomFilterInfo;
            private set
            {
                if (!ReferenceEquals(this._CustomFilterInfo, value))
                {
                    if (this._CustomFilterInfo != null)
                    {
                        this._CustomFilterInfo.FilterCriteriaChanged -= new EventHandler(this.CustomFilterInfo_FilterCriteriaChanged);
                    }
                    if (value != null)
                    {
                        value.FilterCriteriaChanged += new EventHandler(this.CustomFilterInfo_FilterCriteriaChanged);
                    }
                    this._CustomFilterInfo = value;
                    this.OnPropertyChanged("CustomFilterInfo");
                }
            }
        }

        public ExcelColumnFilterType FilterType
        {
            get => 
                this._FilterType;
            set
            {
                this._FilterType = value;
                this.OnPropertyChanged("FilterType");
            }
        }

        public BaseEditSettings ValueColumnEditSettings
        {
            get => 
                this._ValueColumnEditSettings;
            set
            {
                this._ValueColumnEditSettings = value;
                this.OnPropertyChanged("ValueColumnEditSettings");
            }
        }

        public string SearchText
        {
            get => 
                this._SearchText;
            set
            {
                if (this._SearchText != value)
                {
                    string oldFilter = this._SearchText;
                    this._SearchText = value;
                    this.FilterItems.LockCheckedChanged = true;
                    this.UpdateFilter(oldFilter, value);
                    this.FilterItems.LockCheckedChanged = false;
                    this.OnPropertyChanged("SearchText");
                    this.OnPropertyChanged("ActualSearchText");
                    this.UpdateExpandState();
                }
            }
        }

        public string ActualSearchText =>
            this.GetActualSearchText();

        public bool IsFiltered =>
            ((this.CustomFilterInfo == null) || (this.CustomFilterInfo.FilterCriteria == null)) ? !this.IsFilterListContainsEmptyCriteria() : true;

        public ICommand ClearFilterCommand
        {
            get => 
                this._ClearFilterCommand;
            set
            {
                this._ClearFilterCommand = value;
                this.OnPropertyChanged("ClearFilterCommand");
            }
        }

        public ExcelColumnFilterSettings FilterSettings { get; private set; }

        public abstract ExcelDateColumnFilterScope SearchScope { get; set; }

        protected override bool CalcColumnCriteriaOperator =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelColumnFilterInfoBase.<>c <>9 = new ExcelColumnFilterInfoBase.<>c();
            public static Func<CriteriaOperator> <>9__72_0;
            public static Func<ClauseType, bool> <>9__84_0;

            internal CriteriaOperator <ClearFilter>b__72_0() => 
                null;

            internal bool <CreateRulesOperators>b__84_0(ClauseType op) => 
                !FilterControlHelper.IsCollectionClause(op);
        }

        [CompilerGenerated]
        private sealed class <CreateRulesOperators>d__84 : IEnumerable<ClauseType>, IEnumerable, IEnumerator<ClauseType>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private ClauseType <>2__current;
            private int <>l__initialThreadId;
            public ExcelColumnFilterInfoBase <>4__this;
            private GridFilterColumn <filterColumn>5__1;
            private IEnumerator<ClauseType> <>7__wrap1;

            [DebuggerHidden]
            public <CreateRulesOperators>d__84(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private void <>m__Finally1()
            {
                this.<>1__state = -1;
                if (this.<>7__wrap1 != null)
                {
                    this.<>7__wrap1.Dispose();
                }
            }

            private bool MoveNext()
            {
                bool flag;
                try
                {
                    int num = this.<>1__state;
                    if (num == 0)
                    {
                        this.<>1__state = -1;
                        this.<filterColumn>5__1 = this.<>4__this.Column.View.DataControl.CreateGridFilterColumn(this.<>4__this.Column, false);
                        Func<ClauseType, bool> predicate = ExcelColumnFilterInfoBase.<>c.<>9__84_0;
                        if (ExcelColumnFilterInfoBase.<>c.<>9__84_0 == null)
                        {
                            Func<ClauseType, bool> local1 = ExcelColumnFilterInfoBase.<>c.<>9__84_0;
                            predicate = ExcelColumnFilterInfoBase.<>c.<>9__84_0 = new Func<ClauseType, bool>(this.<CreateRulesOperators>b__84_0);
                        }
                        this.<>7__wrap1 = FilterControlHelper.GetListOperationsByFilterColumn(this.<filterColumn>5__1).Where<ClauseType>(predicate).GetEnumerator();
                        this.<>1__state = -3;
                    }
                    else if (num == 1)
                    {
                        this.<>1__state = -3;
                    }
                    else
                    {
                        return false;
                    }
                    while (true)
                    {
                        if (!this.<>7__wrap1.MoveNext())
                        {
                            this.<>m__Finally1();
                            this.<>7__wrap1 = null;
                            flag = false;
                        }
                        else
                        {
                            ClauseType current = this.<>7__wrap1.Current;
                            if (!this.<filterColumn>5__1.IsValidClause(current))
                            {
                                continue;
                            }
                            this.<>2__current = current;
                            this.<>1__state = 1;
                            flag = true;
                        }
                        break;
                    }
                }
                fault
                {
                    this.System.IDisposable.Dispose();
                }
                return flag;
            }

            [DebuggerHidden]
            IEnumerator<ClauseType> IEnumerable<ClauseType>.GetEnumerator()
            {
                ExcelColumnFilterInfoBase.<CreateRulesOperators>d__84 d__;
                if ((this.<>1__state == -2) && (this.<>l__initialThreadId == Environment.CurrentManagedThreadId))
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                else
                {
                    d__ = new ExcelColumnFilterInfoBase.<CreateRulesOperators>d__84(0) {
                        <>4__this = this.<>4__this
                    };
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Data.Filtering.Helpers.ClauseType>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
                int num = this.<>1__state;
                if ((num == -3) || (num == 1))
                {
                    try
                    {
                    }
                    finally
                    {
                        this.<>m__Finally1();
                    }
                }
            }

            ClauseType IEnumerator<ClauseType>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

