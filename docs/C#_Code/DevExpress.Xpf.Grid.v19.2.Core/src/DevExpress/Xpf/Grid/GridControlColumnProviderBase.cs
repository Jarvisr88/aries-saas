namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Helpers;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Native;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Collections.Specialized;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    [DXToolboxBrowsable(false)]
    public class GridControlColumnProviderBase : Decorator, ISearchPanelColumnProviderEx, ISearchPanelColumnProviderBase
    {
        public static readonly DependencyProperty AllowGridExtraFilterProperty;
        public static readonly DependencyProperty HighlightColumnProperty;
        public static readonly DependencyProperty ColumnProviderProperty;
        public static readonly DependencyProperty DataControlBaseProperty;
        internal static readonly DependencyPropertyKey ColumnsPropertyKey;
        public static readonly DependencyProperty ColumnsProperty;
        public static readonly DependencyProperty AllowColumnsHighlightingProperty;
        public static readonly DependencyProperty AllowTextHighlightingProperty;
        public static readonly DependencyProperty FilterByColumnsModeProperty;
        public static readonly DependencyProperty CustomColumnsProperty;
        private bool isSearchLookUpMode;
        private bool applyToColumnsFilter;
        private ReadOnlyObservableCollection<CustomFilterColumn> customFilterColumns;
        private IList oldColumns;
        private string oldSearchString;
        private string searchString;
        private CriteriaOperator filterCriteria;
        private CriteriaOperator oldFilterCriteria;
        private volatile bool updateActionEnqueued;
        private Dictionary<ColumnBase, TextHighlightingProperties> allHighlighingProperties = new Dictionary<ColumnBase, TextHighlightingProperties>();
        private List<ColumnBase> columnsForceWithoutPrefix;

        static GridControlColumnProviderBase()
        {
            Type ownerType = typeof(GridControlColumnProviderBase);
            DataControlBaseProperty = DependencyPropertyManager.Register("DataControlBase", typeof(DevExpress.Xpf.Grid.DataControlBase), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((GridControlColumnProviderBase) d).GridControlChanged((DevExpress.Xpf.Grid.DataControlBase) e.OldValue, (DevExpress.Xpf.Grid.DataControlBase) e.NewValue)));
            ColumnProviderProperty = DependencyPropertyManager.RegisterAttached("ColumnProvider", typeof(GridControlColumnProviderBase), ownerType, new FrameworkPropertyMetadata(null, new PropertyChangedCallback(GridControlColumnProviderBase.ColumnProviderPropertyChanged)));
            HighlightColumnProperty = DependencyPropertyManager.RegisterAttached("HighlightColumn", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, new PropertyChangedCallback(GridControlColumnProviderBase.HighlightColumnPropertyChanged)));
            AllowColumnsHighlightingProperty = DependencyPropertyManager.Register("AllowColumnsHighlighting", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((GridControlColumnProviderBase) d).AllowColumnsHighlightingChanged((bool) e.NewValue)));
            AllowGridExtraFilterProperty = DependencyPropertyManager.Register("AllowGridExtraFilter", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((GridControlColumnProviderBase) d).AllowGridExtraFilterChanged((bool) e.NewValue)));
            AllowTextHighlightingProperty = DependencyPropertyManager.Register("AllowTextHighlighting", typeof(bool), ownerType, new FrameworkPropertyMetadata(false, (d, e) => ((GridControlColumnProviderBase) d).AllowTextHighlightingChanged((bool) e.NewValue)));
            ColumnsPropertyKey = DependencyPropertyManager.RegisterReadOnly("Columns", typeof(IList), ownerType, new FrameworkPropertyMetadata(null));
            ColumnsProperty = ColumnsPropertyKey.DependencyProperty;
            FilterByColumnsModeProperty = DependencyPropertyManager.Register("FilterByColumnsMode", typeof(DevExpress.Xpf.Editors.FilterByColumnsMode), ownerType, new FrameworkPropertyMetadata(DevExpress.Xpf.Editors.FilterByColumnsMode.Custom, (d, e) => ((GridControlColumnProviderBase) d).UpdateColumns()));
            CustomColumnsProperty = DependencyPropertyManager.Register("CustomColumns", typeof(ObservableCollection<string>), ownerType, new FrameworkPropertyMetadata(null, (d, e) => ((GridControlColumnProviderBase) d).CustomColumnsChanged((ObservableCollection<string>) e.OldValue, (ObservableCollection<string>) e.NewValue)));
        }

        public GridControlColumnProviderBase()
        {
            this.Columns = new List<ColumnBase>();
        }

        protected bool AllowColumnSearch(ColumnBase column)
        {
            if ((this.DataControlBase == null) || (this.DataControlBase.DataProviderBase == null))
            {
                return true;
            }
            IColumnsServerActions dataController = this.DataControlBase.DataProviderBase.DataController;
            return ((dataController != null) ? dataController.AllowAction(column.FieldName, ColumnServerActionType.Filter) : true);
        }

        protected virtual void AllowColumnsHighlightingChanged(bool value)
        {
            this.ApplyHighlighting();
        }

        protected virtual void AllowGridExtraFilterChanged(bool value)
        {
            this.UpdateGridFilter(false);
        }

        protected virtual void AllowTextHighlightingChanged(bool value)
        {
            this.ApplyHighlighting();
        }

        private void ApplyColumnsHighlighting()
        {
            if (this.DataControlBase != null)
            {
                foreach (GridColumnBase base2 in this.DataControlBase.ColumnsCore)
                {
                    base2.AllowSearchHeaderHighlighting = this.AllowColumnsHighlighting && this.Columns.Contains(base2);
                }
            }
        }

        private void ApplyHighlighting()
        {
            if (((this.DataControlBase != null) && (this.DataControlBase.DataView != null)) && !this.updateActionEnqueued)
            {
                this.updateActionEnqueued = true;
                bool lockScrollBarAnnotation = this.DataControlBase.RestoreLayoutIsLock;
                this.DataControlBase.DataView.ImmediateActionsManager.EnqueueAction(() => this.ApplyHighlightingCore(lockScrollBarAnnotation));
            }
        }

        private void ApplyHighlightingCore(bool lockScrollBarAnnotation)
        {
            this.ResetIncrementalSearch();
            if (this.AllowColumnsHighlighting)
            {
                this.ApplyColumnsHighlighting();
            }
            this.ApplyTextHighlighting();
            if ((this.DataControlBase.DataView.SearchControl != null) && (this.DataControlBase.VisibleRowCount > 0))
            {
                if (lockScrollBarAnnotation)
                {
                    this.DataControlBase.DataView.UpdateFilterOnDeserializationLock();
                }
                this.DataControlBase.DataView.UpdateFilterGrid();
                if (lockScrollBarAnnotation)
                {
                    this.DataControlBase.DataView.UpdateFilterOnDeserializationUnlock(false);
                }
            }
            this.updateActionEnqueued = false;
        }

        private void ApplyTextHighlighting()
        {
            this.allHighlighingProperties.Clear();
            if ((this.DataControlBase != null) && ((this.oldSearchString != null) || (this.searchString != null)))
            {
                if ((this.Highlighting == null) || (this.Highlighting.Count == 0))
                {
                    this.UpdateViewHighlightingText();
                }
                else
                {
                    foreach (GridColumnBase gridColumn in this.DataControlBase.ColumnsCore)
                    {
                        FieldAndHighlightingString str = (from fhr in this.Highlighting
                            where fhr.Field == gridColumn.FieldName
                            select fhr).FirstOrDefault<FieldAndHighlightingString>();
                        if (str != null)
                        {
                            this.allHighlighingProperties.Add(gridColumn, new TextHighlightingProperties(str.HighlightingString, this.FilterCondition));
                        }
                    }
                    this.UpdateViewHighlightingText();
                }
            }
        }

        private static void ColumnProviderPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            GridControlColumnProviderBase newValue = (GridControlColumnProviderBase) e.NewValue;
            if (newValue != null)
            {
                newValue.DataControlBase = (DevExpress.Xpf.Grid.DataControlBase) d;
            }
        }

        private void ColumnsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateColumns();
        }

        private void CustomColumnsChanged(ObservableCollection<string> oldCollection, ObservableCollection<string> newCollection)
        {
            if (oldCollection != null)
            {
                oldCollection.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.CustomColumnsCollectionChanged);
            }
            if (newCollection != null)
            {
                newCollection.CollectionChanged += new NotifyCollectionChangedEventHandler(this.CustomColumnsCollectionChanged);
            }
            this.UpdateColumns();
        }

        private void CustomColumnsCollectionChanged(object sender, NotifyCollectionChangedEventArgs e)
        {
            this.UpdateColumns();
        }

        string ISearchPanelColumnProviderBase.GetSearchText() => 
            this.SearchString;

        bool ISearchPanelColumnProviderBase.UpdateFilter(string searchText, DevExpress.Data.Filtering.FilterCondition filterCondition, CriteriaOperator filterCriteria)
        {
            if (this.DataControlBase != null)
            {
                this.SearchString = this.GetSeatchText(searchText, filterCondition, filterCriteria);
                this.FilterCondition = filterCondition;
                this.FilterCriteria = this.GetFilterCriteria(searchText, filterCondition, filterCriteria);
                this.UpdateColumns();
            }
            return false;
        }

        protected virtual List<FieldAndHighlightingString> FilterCriteriaToHighlighting(CriteriaOperator criteriaOperator)
        {
            List<FieldAndHighlightingString> list = new List<FieldAndHighlightingString>();
            foreach (KeyValuePair<string, CriteriaOperator> pair in CriteriaColumnAffinityResolver.SplitByColumnNames(criteriaOperator, null).Item2)
            {
                string key = pair.Key;
                SearchPanelHighlightingResolver visitor = new SearchPanelHighlightingResolver();
                List<string> values = pair.Value.Accept<List<string>>(visitor);
                list.Add(new FieldAndHighlightingString(key, string.Join("\n", values)));
            }
            return list;
        }

        public List<string> GetAllSearchColumns()
        {
            List<string> list = new List<string>();
            if (this.Columns != null)
            {
                foreach (ColumnBase base2 in this.Columns)
                {
                    list.Add(base2.FieldName);
                }
            }
            if (this.ColumnsForceWithoutPrefix != null)
            {
                foreach (ColumnBase base3 in this.ColumnsForceWithoutPrefix)
                {
                    list.Add(base3.FieldName);
                }
            }
            if (this.CustomColumns != null)
            {
                foreach (string str in this.CustomColumns)
                {
                    list.Add(str);
                }
            }
            return list;
        }

        public static GridControlColumnProviderBase GetColumnProvider(DependencyObject d) => 
            (d != null) ? (d.GetValue(ColumnProviderProperty) as GridControlColumnProviderBase) : null;

        protected virtual CriteriaOperator GetFilterCriteria(string searchText, DevExpress.Data.Filtering.FilterCondition filterCondition, CriteriaOperator filterCriteria)
        {
            if ((this.DataControlBase != null) && (this.DataControlBase.viewCore != null))
            {
                SearchStringToFilterCriteriaWrapper wrapper = this.DataControlBase.viewCore.ConvertSearchStringToFilterCriteria(searchText, filterCriteria, this.GetHighlightingStrings());
                filterCriteria = wrapper.Filter;
                List<FieldAndHighlightingString> highlighting = wrapper.Highlighting;
                List<FieldAndHighlightingString> list2 = highlighting;
                if (highlighting == null)
                {
                    List<FieldAndHighlightingString> local1 = highlighting;
                    list2 = this.FilterCriteriaToHighlighting(filterCriteria);
                }
                this.Highlighting = list2;
                this.ApplyToColumnsFilter = wrapper.ApplyToColumnsFilter;
            }
            return filterCriteria;
        }

        public static bool GetHighlightColumn(DependencyObject d) => 
            (bool) d.GetValue(HighlightColumnProperty);

        private List<FieldAndHighlightingString> GetHighlightingStrings() => 
            this.AllowTextHighlighting ? SearchControlHelper.GetTextHighlightingString(this.searchString, this.Columns, this.FilterCondition, this.DataControlBase.DataView.SearchPanelParseMode == SearchPanelParseMode.Exact) : null;

        protected virtual string GetSeatchText(string searchText, DevExpress.Data.Filtering.FilterCondition filterCondition, CriteriaOperator filterCriteria) => 
            searchText;

        protected internal TextHighlightingProperties GetTextHighlightingProperties(ColumnBase column)
        {
            TextHighlightingProperties properties = null;
            this.allHighlighingProperties.TryGetValue(column, out properties);
            return properties;
        }

        protected virtual void GridControlChanged(DevExpress.Xpf.Grid.DataControlBase oldGrid, DevExpress.Xpf.Grid.DataControlBase grid)
        {
            if (oldGrid != null)
            {
                this.UnsubscribeFromGridControl(oldGrid);
            }
            if (grid != null)
            {
                this.SubscribeToGridControl(grid);
            }
            this.UpdateColumns();
        }

        private static void HighlightColumnPropertyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is GridColumnBase)
            {
                UpdateCustomColumnHighlighting((GridColumnBase) d);
            }
        }

        private void ResetIncrementalSearch()
        {
            if ((this.DataControlBase != null) && (this.DataControlBase.viewCore != null))
            {
                this.DataControlBase.viewCore.ResetIncrementalSearch();
            }
        }

        public static void SetColumnProvider(DependencyObject d, GridControlColumnProviderBase value)
        {
            d.SetValue(ColumnProviderProperty, value);
        }

        public static void SetHighlightColumn(DependencyObject d, bool value)
        {
            d.SetValue(HighlightColumnProperty, value);
        }

        private void SubscribeToGridControl(DevExpress.Xpf.Grid.DataControlBase grid)
        {
            this.DataControlBase = grid;
            grid.ColumnsCore.CollectionChanged += new NotifyCollectionChangedEventHandler(this.ColumnsCollectionChanged);
            SetColumnProvider(grid, this);
            if (grid.viewCore != null)
            {
                grid.viewCore.ApplySearchColumns();
            }
        }

        private void UnsubscribeFromGridControl(DevExpress.Xpf.Grid.DataControlBase grid)
        {
            grid.ColumnsCore.CollectionChanged -= new NotifyCollectionChangedEventHandler(this.ColumnsCollectionChanged);
            grid.ClearValue(ColumnProviderProperty);
        }

        public void UpdateColumnNameCollection()
        {
            if (this.DataControlBase == null)
            {
                this.Columns = null;
                this.oldColumns = null;
            }
            else
            {
                this.oldColumns = this.Columns;
                this.Columns = new List<ColumnBase>();
                DevExpress.Xpf.Editors.FilterByColumnsMode filterByColumnsMode = this.FilterByColumnsMode;
                if (filterByColumnsMode == DevExpress.Xpf.Editors.FilterByColumnsMode.Default)
                {
                    foreach (ColumnBase base2 in this.DataControlBase.ColumnsCore)
                    {
                        if (!string.IsNullOrEmpty(base2.FieldName) && (this.AllowColumnSearch(base2) && base2.ActualAllowSearchPanel))
                        {
                            this.Columns.Add(base2);
                        }
                    }
                }
                else if ((filterByColumnsMode == DevExpress.Xpf.Editors.FilterByColumnsMode.Custom) && (this.CustomColumns != null))
                {
                    foreach (ColumnBase base3 in this.DataControlBase.ColumnsCore)
                    {
                        if (this.AllowColumnSearch(base3) && (base3.AllowSearchPanel == DefaultBoolean.True))
                        {
                            this.Columns.Add(base3);
                            continue;
                        }
                        if (this.CustomColumns.Contains(base3.FieldName) && (this.AllowColumnSearch(base3) && (base3.AllowSearchPanel != DefaultBoolean.False)))
                        {
                            this.Columns.Add(base3);
                        }
                    }
                }
                this.UpdateColumnsPrefix();
            }
        }

        public void UpdateColumns()
        {
            if (this.DataControlBase != null)
            {
                this.UpdateColumnNameCollection();
            }
            this.UpdateGridFilter(false);
        }

        public void UpdateColumns(DevExpress.Xpf.Editors.FilterByColumnsMode mode)
        {
            this.FilterByColumnsMode = mode;
            this.UpdateColumns();
        }

        private void UpdateColumnsPrefix()
        {
            this.columnsForceWithoutPrefix = new List<ColumnBase>();
            List<DataColumnInfo> list = this.DataControlBase.DataProviderBase.Columns.ToArray().ToList<DataColumnInfo>();
            foreach (ColumnBase column in this.DataControlBase.ColumnsCore)
            {
                if (this.Columns.Contains(column) && ((from info in list
                    where ((IDataColumnInfo) info).FieldName == column.FieldName
                    select info).FirstOrDefault<DataColumnInfo>() == null))
                {
                    if (column.IsUnbound)
                    {
                        this.columnsForceWithoutPrefix.Add(column);
                    }
                    this.Columns.Remove(column);
                }
            }
        }

        private static void UpdateCustomColumnHighlighting(GridColumnBase column)
        {
            if (column.OwnerControl != null)
            {
                GridControlColumnProviderBase columnProvider = GetColumnProvider(column.OwnerControl);
                if (columnProvider != null)
                {
                    columnProvider.ApplyHighlighting();
                }
            }
        }

        public void UpdateGridFilter(bool force = false)
        {
            if (this.DataControlBase != null)
            {
                if (!force && ((this.oldFilterCriteria == null) && (this.FilterCriteria == null)))
                {
                    GridSearchControlBase searchControl = this.DataControlBase.DataView.SearchControl as GridSearchControlBase;
                    if (searchControl == null)
                    {
                        return;
                    }
                    if (GridColumnListParser.IsColumnsListsEquals(this.oldColumns, this.Columns))
                    {
                        if (this.SearchString != searchControl.SearchText)
                        {
                            this.SearchString = searchControl.SearchText;
                        }
                    }
                    else if (!string.IsNullOrWhiteSpace(searchControl.SearchText))
                    {
                        searchControl.UpdateColumnProvider();
                        return;
                    }
                }
                if (force)
                {
                    this.DataControlBase.ExtraFilter = null;
                }
                this.UpdateGridFilterCore();
            }
        }

        private void UpdateGridFilterCore()
        {
            CriteriaOperator @operator = this.AllowGridExtraFilter ? this.FilterCriteria : null;
            if (!this.ApplyToColumnsFilter)
            {
                this.DataControlBase.ExtraFilter = @operator;
            }
            else
            {
                this.DataControlBase.FilterCriteria = @operator;
            }
            if ((this.DataControlBase.DataView.SearchControl != null) && (this.DataControlBase.VisibleRowCount > 0))
            {
                this.DataControlBase.DataView.SearchControl.UpdateMRU();
            }
            this.ApplyHighlighting();
        }

        private void UpdateViewHighlightingText()
        {
            if ((this.DataControlBase != null) && (this.DataControlBase.viewCore != null))
            {
                this.DataControlBase.viewCore.UpdateEditorHighlightingText();
            }
        }

        public ObservableCollection<string> CustomColumns
        {
            get => 
                (ObservableCollection<string>) base.GetValue(CustomColumnsProperty);
            set => 
                base.SetValue(CustomColumnsProperty, value);
        }

        public bool IsSearchLookUpMode
        {
            get => 
                this.isSearchLookUpMode;
            set => 
                this.isSearchLookUpMode = value;
        }

        public bool ApplyToColumnsFilter
        {
            get => 
                this.applyToColumnsFilter;
            private set => 
                this.applyToColumnsFilter = value;
        }

        public List<FieldAndHighlightingString> Highlighting { get; set; }

        public IList<CustomFilterColumn> CustomFilterColumns =>
            this.customFilterColumns;

        public IList Columns
        {
            get => 
                (IList) base.GetValue(ColumnsProperty);
            internal set => 
                base.SetValue(ColumnsPropertyKey, value);
        }

        public DevExpress.Xpf.Grid.DataControlBase DataControlBase
        {
            get => 
                (DevExpress.Xpf.Grid.DataControlBase) base.GetValue(DataControlBaseProperty);
            set => 
                base.SetValue(DataControlBaseProperty, value);
        }

        public bool AllowColumnsHighlighting
        {
            get => 
                (bool) base.GetValue(AllowColumnsHighlightingProperty);
            set => 
                base.SetValue(AllowColumnsHighlightingProperty, value);
        }

        public bool AllowTextHighlighting
        {
            get => 
                (bool) base.GetValue(AllowTextHighlightingProperty);
            set => 
                base.SetValue(AllowTextHighlightingProperty, value);
        }

        public bool AllowGridExtraFilter
        {
            get => 
                (bool) base.GetValue(AllowGridExtraFilterProperty);
            set => 
                base.SetValue(AllowGridExtraFilterProperty, value);
        }

        public DevExpress.Xpf.Editors.FilterByColumnsMode FilterByColumnsMode
        {
            get => 
                (DevExpress.Xpf.Editors.FilterByColumnsMode) base.GetValue(FilterByColumnsModeProperty);
            set => 
                base.SetValue(FilterByColumnsModeProperty, value);
        }

        private string SearchString
        {
            get => 
                this.searchString;
            set
            {
                if (value != this.searchString)
                {
                    this.oldSearchString = this.searchString;
                    this.searchString = value;
                }
            }
        }

        private DevExpress.Data.Filtering.FilterCondition FilterCondition { get; set; }

        private CriteriaOperator FilterCriteria
        {
            get => 
                this.filterCriteria;
            set
            {
                this.oldFilterCriteria = this.filterCriteria;
                this.filterCriteria = value;
            }
        }

        bool ISearchPanelColumnProviderEx.IsServerMode =>
            (this.DataControlBase != null) && ((this.DataControlBase.DataProviderBase != null) && (this.DataControlBase.DataProviderBase.IsServerMode || this.DataControlBase.DataProviderBase.IsAsyncServerMode));

        public IList ColumnsForceWithoutPrefix =>
            this.columnsForceWithoutPrefix;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GridControlColumnProviderBase.<>c <>9 = new GridControlColumnProviderBase.<>c();

            internal void <.cctor>b__10_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((GridControlColumnProviderBase) d).GridControlChanged((DataControlBase) e.OldValue, (DataControlBase) e.NewValue);
            }

            internal void <.cctor>b__10_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((GridControlColumnProviderBase) d).AllowColumnsHighlightingChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__10_2(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((GridControlColumnProviderBase) d).AllowGridExtraFilterChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__10_3(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((GridControlColumnProviderBase) d).AllowTextHighlightingChanged((bool) e.NewValue);
            }

            internal void <.cctor>b__10_4(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((GridControlColumnProviderBase) d).UpdateColumns();
            }

            internal void <.cctor>b__10_5(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((GridControlColumnProviderBase) d).CustomColumnsChanged((ObservableCollection<string>) e.OldValue, (ObservableCollection<string>) e.NewValue);
            }
        }
    }
}

