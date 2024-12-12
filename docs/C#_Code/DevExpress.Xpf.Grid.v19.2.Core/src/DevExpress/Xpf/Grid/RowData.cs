namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Editors.Validation;
    using DevExpress.Xpf.Grid.Native;
    using DevExpress.XtraEditors.DXErrorProvider;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Data;

    public class RowData : ColumnsRowDataBase, ISupportVisibleIndex
    {
        public static readonly DependencyProperty RowDataProperty = DependencyProperty.RegisterAttached("RowData", typeof(RowData), typeof(RowData), new FrameworkPropertyMetadata(null, FrameworkPropertyMetadataOptions.Inherits));
        internal const string DataContextPropertyName = "DataContext";
        private const string RowHandlePropertyName = "RowHandle";
        public static readonly DependencyProperty CurrentRowDataProperty = DependencyPropertyManager.RegisterAttached("CurrentRowData", typeof(RowData), typeof(RowData), new PropertyMetadata(null, new PropertyChangedCallback(RowData.OnCurrentRowDataChanged)));
        private DevExpress.Xpf.Data.RowHandle rowHandle;
        private object dataContext;
        private object row;
        private DependencyObject rowState;
        private DevExpress.Xpf.Grid.RowPosition rowPosition;
        private bool showBottomLine;
        private bool evenRow;
        private bool alternateRow;
        private bool isSelected;
        private bool isFocused;
        private bool isRowExpanded;
        private bool isRowVisible;
        private bool isExpanding;
        private DevExpress.Xpf.Grid.SelectionState selectionState;
        private int nextRowLevel;
        private bool showRowBreak;
        private bool isEditFormVisible;
        private bool isReady;
        private DevExpress.Xpf.Grid.FixedRowPosition fixedRowPosition;
        private bool isLastFixedRow;
        internal NodeContainer parentNodeContainer;
        private DataControllerValuesContainer controllerValues;
        private int level;
        private bool updateOnlyData;
        private bool updateSelectionState;
        private bool updateError;
        private bool customCellValidate;
        private IRowStateClient rowStateClient;
        private readonly Dictionary<ColumnBase, object> updateRowButtonsCache;
        internal readonly Dictionary<ColumnBase, object> CancelValuesCache;
        private RowDataPropertyChangeSubscriber subscriber;
        internal readonly Locker conditionalFormattingLocker;
        private VersionedFormatInfoProvider formatInfoProvider;
        internal readonly Locker GetValueLeaveInvalidEditorLocker;
        private DevExpress.Xpf.Grid.Native.AnimationController animationControllerCore;
        private ConditionalClientAppearanceUpdaterBase conditionalUpdater;

        static RowData()
        {
            BaseEditHelper.GetValidationErrorPropertyKey().OverrideMetadata(typeof(RowData), new FrameworkPropertyMetadata((d, e) => ((RowData) d).UpdateClientValidationError()));
        }

        public RowData(DataTreeBuilder treeBuilder, bool updateOnlyData = false, bool updateSelectionState = true, bool updateError = true) : base(treeBuilder, null)
        {
            this.rowPosition = DevExpress.Xpf.Grid.RowPosition.Bottom;
            this.isRowVisible = true;
            this.isReady = true;
            this.customCellValidate = true;
            this.updateRowButtonsCache = new Dictionary<ColumnBase, object>();
            this.CancelValuesCache = new Dictionary<ColumnBase, object>();
            this.conditionalFormattingLocker = new Locker();
            this.GetValueLeaveInvalidEditorLocker = new Locker();
            Func<RowData, DataTreeBuilder> treeBuilderAccessor = <>c.<>9__134_0;
            if (<>c.<>9__134_0 == null)
            {
                Func<RowData, DataTreeBuilder> local1 = <>c.<>9__134_0;
                treeBuilderAccessor = <>c.<>9__134_0 = x => x.treeBuilder;
            }
            this.formatInfoProvider = new VersionedFormatInfoProvider(new DataTreeBuilderFormatInfoProvider<RowData>(this, treeBuilderAccessor, <>c.<>9__134_1 ??= (x, fieldName) => x.treeBuilder.GetCellValue(x, fieldName)));
            this.formatInfoProvider.IsCachingEnabled = false;
            this.updateOnlyData = updateOnlyData;
            this.updateSelectionState = updateSelectionState;
            this.updateError = updateError;
            this.UseInUpdateQueue = true;
            SetRowDataInternal(base.WholeRowElement, this);
        }

        protected override void ApplyRowDataToAdditionalElement()
        {
            base.ApplyRowDataToAdditionalElement();
            SetRowData(base.AdditionalElement, this);
            SetCurrentRowData(base.AdditionalElement, this);
        }

        internal void AssignFrom(int rowHandle)
        {
            this.AssignFromCore(rowHandle, -1, null);
        }

        internal virtual void AssignFrom(RowsContainer parentRowsContainer, NodeContainer parentNodeContainer, RowNode rowNode, bool forceUpdate)
        {
            DevExpress.Xpf.Grid.DataRowNode node = (DevExpress.Xpf.Grid.DataRowNode) rowNode;
            if (!ReferenceEquals(base.View, node.View))
            {
                forceUpdate = true;
            }
            base.UpdateTreeBuilder(((DataNodeContainer) parentNodeContainer).treeBuilder);
            this.ControllerValues = node.ControllerValues;
            bool flag = !ReferenceEquals(base.node, rowNode) || this.IsDirty;
            if (flag)
            {
                base.node = rowNode;
                this.parentNodeContainer = parentNodeContainer;
            }
            this.SyncWithNode();
            if (flag)
            {
                this.RowHandle = this.DataRowNode.RowHandle;
                this.IsLastFixedRow = this.DataRowNode.IsLastFixedRow;
                if (ReferenceEquals(base.CellData, null) | forceUpdate)
                {
                    this.customCellValidate = false;
                    base.UpdateCellData();
                }
                this.customCellValidate = true;
                if (this.UpdateImmediately)
                {
                    this.RefreshData();
                }
                else
                {
                    base.treeBuilder.UpdateRowData(this);
                    this.UpdateMasterDetailInfo(true, false);
                    this.IsReady = false;
                }
                if (forceUpdate)
                {
                    this.UpdateEditorButtonVisibilities();
                }
            }
            this.AssignFromCore(parentNodeContainer, rowNode);
            base.View.ViewBehavior.UpdateFixedNoneContentWidth(this);
            this.ValidateRowsContainer();
            if (this.updateSelectionState)
            {
                this.UpdateIndicatorState();
                this.UpdateSelectionState();
                this.UpdateNextRowLevel();
                this.UpdateIsMasterRowExpanded();
                this.UpdateShowRowBreak();
            }
        }

        protected virtual void AssignFromCore(NodeContainer nodeContainer, RowNode rowNode)
        {
        }

        internal void AssignFromCore(int rowHandle, int listSourceRowIndex, ColumnBase column)
        {
            this.ControllerValues = DataIteratorBase.CreateValuesContainer(base.treeBuilder, new DevExpress.Xpf.Data.RowHandle(rowHandle));
            this.RowHandle = this.ControllerValues.RowHandle;
            this.UpdateDataCore(column, listSourceRowIndex);
        }

        protected virtual void CacheRowData()
        {
            this.VisualDataTreeBuilder.CacheRowData(this);
        }

        internal void ClearBindingValues(ColumnBase column)
        {
            this.IterateNotNullData(delegate (ColumnBase cellData, GridColumnData index) {
                if (ReferenceEquals(column, cellData))
                {
                    index.ClearBindingValue();
                }
            });
        }

        internal override void ClearRow()
        {
            this.Row = null;
            if (this.DataRowNode != null)
            {
                this.DataRowNode.Clear();
            }
        }

        protected internal override GridColumnData CreateGridCellDataCore() => 
            new EditGridCellData(this);

        protected override RowDataBase.NotImplementedRowDataReusingStrategy CreateReusingStrategy(Func<FrameworkElement> createRowElementDelegate) => 
            new RowDataReusingStrategy(this);

        protected virtual FrameworkElement CreateRowElement() => 
            base.View.CreateRowElement(this);

        protected internal virtual void EnsureRowLoaded()
        {
            base.View.DataProviderBase.EnsureRowLoaded(this.RowHandle.Value);
        }

        internal static RowData FindRowData(DependencyObject d) => 
            DataControlBase.FindElementWithAttachedPropertyValue<RowData>(d, CurrentRowDataProperty);

        internal IConditionalFormattingClientBase GetConditionalFormattingClient() => 
            this.rowStateClient as IConditionalFormattingClientBase;

        public static RowData GetCurrentRowData(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (RowData) element.GetValue(CurrentRowDataProperty);
        }

        internal virtual IEnumerable<RowDataBase> GetCurrentViewChildItems() => 
            RowDataBase.EmptyEnumerable;

        protected internal override double GetFixedNoneContentWidth(double totalWidth) => 
            base.View.ViewBehavior.GetFixedNoneContentWidth(totalWidth, this.RowHandle.Value);

        internal Locker GetFormatCachingLocker() => 
            this.formatInfoProvider.CachingLocker;

        protected internal override bool GetIsFocusable() => 
            base.GetIsFocusable() || base.View.ViewBehavior.IsAdditionalRow(this.RowHandle.Value);

        internal bool GetIsReady() => 
            (base.DataControl != null) && ((base.DataControl.DataProviderBase != null) && (this.IsGroupRowInAsyncServerMode || GetIsReady(this.Row)));

        internal static bool GetIsReady(object value) => 
            !AsyncServerModeDataController.IsNoValue(value);

        private bool GetRootDataPresenterAdjustmentInProgress() => 
            (base.View.RootDataPresenter != null) ? base.View.RootDataPresenter.AdjustmentInProgress : false;

        public static RowData GetRowData(DependencyObject element)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            return (RowData) element.GetValue(RowDataProperty);
        }

        protected internal virtual double GetRowIndent(ColumnBase column)
        {
            double rowIndent = this.GetRowIndent(base.View.ViewBehavior.IsFirstOrTreeColumn(column, false));
            if ((column != null) && !this.HasTreeColumn())
            {
                rowIndent += column.ActualBandRightSeparatorWidthCore + column.ActualBandLeftSeparatorWidthCore;
            }
            return rowIndent;
        }

        protected internal virtual double GetRowIndent(bool isFirstColumn) => 
            !(this.IsNewItemRow & isFirstColumn) ? 0.0 : base.View.ViewBehavior.NewItemRowIndent;

        protected internal virtual double GetRowLeftMargin(GridColumnData cellData) => 
            0.0;

        protected internal virtual bool GetShowBottomLine() => 
            (this.Level > 0) && ((this.RowPosition == DevExpress.Xpf.Grid.RowPosition.Bottom) || (this.RowPosition == DevExpress.Xpf.Grid.RowPosition.Single));

        protected internal virtual double GetTreeColumnOffset(ColumnBase column) => 
            0.0;

        internal FormatValueProvider GetValueProvider(string fieldName) => 
            this.formatInfoProvider.GetValueProvider(fieldName);

        protected virtual bool HasTreeColumn() => 
            false;

        internal void InvalidateCellsPanel()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.InvalidateCellsPanel();
            }
        }

        internal override bool IsFocusedRow() => 
            base.View.IsFocusedView && (this.RowHandle.Value == base.View.CalcActualFocusedRowHandle());

        internal bool IsRowInView()
        {
            if (this.FixedRowPosition != DevExpress.Xpf.Grid.FixedRowPosition.None)
            {
                return true;
            }
            if (this.VisibleIndex == -1)
            {
                return false;
            }
            int rowVisibleIndexByHandleCore = base.DataControl.GetRowVisibleIndexByHandleCore(this.RowHandle.Value);
            int num2 = base.DataControl.FindFirstInnerChildScrollIndex(rowVisibleIndexByHandleCore);
            int num3 = base.DataControl.CalcTotalLevel(rowVisibleIndexByHandleCore);
            double currentOffset = base.View.RootDataPresenter.CurrentOffset;
            double viewportHeight = ((IScrollInfo) base.View.RootDataPresenter).ViewportHeight;
            if (!base.View.RootView.ViewBehavior.AllowPerPixelScrolling)
            {
                viewportHeight++;
            }
            return (num2 < (((currentOffset + base.View.ActualFixedTopRowsCount) + viewportHeight) - num3));
        }

        internal bool IsSameRow(object row) => 
            ((row == null) || !row.GetType().IsValueType) ? (row == this.Row) : Equals(row, this.Row);

        private void IterateNotNullData(Action<ColumnBase, GridColumnData> updateMethod)
        {
            base.View.layoutUpdatedLocker.DoLockedAction(() => IterateNotNullDataCore<GridColumnData>(this.CellDataCache, updateMethod));
        }

        protected static void IterateNotNullDataCore<TData>(Dictionary<ColumnBase, TData> cache, Action<ColumnBase, TData> updateMethod) where TData: GridColumnData
        {
            IEnumerator<KeyValuePair<ColumnBase, TData>> enumerator = (IEnumerator<KeyValuePair<ColumnBase, TData>>) cache.GetEnumerator();
            while (true)
            {
                try
                {
                    if (!enumerator.MoveNext())
                    {
                        break;
                    }
                }
                catch (InvalidOperationException)
                {
                    break;
                }
                updateMethod(enumerator.Current.Key, enumerator.Current.Value);
            }
        }

        private static void IterateNotNullDataCore<TData>(IEnumerable<ColumnBase> columns, Dictionary<ColumnBase, TData> cache, Action<ColumnBase, TData> updateMethod) where TData: GridColumnData
        {
            foreach (ColumnBase base2 in columns)
            {
                TData valueOrDefault = cache.GetValueOrDefault<ColumnBase, TData>(base2);
                if (valueOrDefault != null)
                {
                    updateMethod(base2, valueOrDefault);
                }
            }
        }

        internal Tuple<bool, object> NeedUpdateRowButtonsInit(ColumnBase column)
        {
            if (((!this.IsFocused || !base.View.AreUpdateRowButtonsShown) && !base.View.UpdateRowButtonsLocker.IsLocked) || !this.updateRowButtonsCache.ContainsKey(column))
            {
                return new Tuple<bool, object>(true, null);
            }
            return new Tuple<bool, object>(false, this.updateRowButtonsCache[column]);
        }

        protected internal virtual void OnActualHeaderWidthChange()
        {
        }

        private void OnAlternateRowChanged()
        {
            this.UpdateClientAlternateBackground();
        }

        private static void OnCurrentRowDataChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            if (d is INotifyCurrentRowDataChanged)
            {
                ((INotifyCurrentRowDataChanged) d).OnCurrentRowDataChanged();
            }
        }

        protected virtual void OnDataContextChanged()
        {
        }

        internal void OnErrorsChanged(string propertyName)
        {
            if (base.DataControl != null)
            {
                <>c__DisplayClass292_0 class_;
                Action method = delegate {
                    IterateNotNullDataCore<GridColumnData>(this.DataControl.GetPropertyChangeDependency(propertyName).Columns, this.CellDataCache, (c, d) => class_.UpdateCellDataError(c, d, true));
                    this.UpdateRowDataError();
                };
                if (base.Dispatcher.CheckAccess())
                {
                    method();
                }
                else
                {
                    base.Dispatcher.BeginInvoke(method, new object[0]);
                }
            }
        }

        protected override void OnFixedLeftCellDataChanged(IList<GridColumnData> oldValue)
        {
            base.OnFixedLeftCellDataChanged(oldValue);
            this.UpdateClientFixedLeftCellData(oldValue);
        }

        protected override void OnFixedNoneCellDataChanged()
        {
            base.OnFixedNoneCellDataChanged();
            this.UpdateClientFixedNoneCellData();
        }

        protected override void OnFixedNoneContentWidthCahnged()
        {
            base.OnFixedNoneContentWidthCahnged();
            this.UpdateClientFixedNoneContentWidth();
        }

        protected override void OnFixedRightCellDataChanged(IList<GridColumnData> oldValue)
        {
            base.OnFixedRightCellDataChanged(oldValue);
            this.UpdateClientFixedRightCellData(oldValue);
        }

        protected internal virtual void OnHeaderCaptionChanged()
        {
        }

        internal virtual void OnIsReadyChanged()
        {
            Action<ColumnBase, GridColumnData> updateMethod = <>c.<>9__293_0;
            if (<>c.<>9__293_0 == null)
            {
                Action<ColumnBase, GridColumnData> local1 = <>c.<>9__293_0;
                updateMethod = <>c.<>9__293_0 = (column, cellData) => ((GridCellData) cellData).UpdateIsReady();
            }
            this.IterateNotNullData(updateMethod);
        }

        private static void OnIsReadyChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RowData) d).OnIsReadyChanged();
        }

        protected virtual void OnIsRowExpandedChanged()
        {
        }

        protected virtual void OnIsRowVisibleChanged()
        {
        }

        private void OnRowChanged(object oldRow, object newRow)
        {
            this.updateRowButtonsCache.Clear();
            this.formatInfoProvider.Clear();
            if (this.IsAsyncServerMode && base.GetIsVisible())
            {
                this.UpdateIsReady();
            }
            this.UnsubcribeRowObject(oldRow);
            this.SubcribeRowObject(newRow);
            if ((newRow != null) || this.IsNewItemRow)
            {
                this.UpdateContent();
            }
            Action<ColumnBase, GridColumnData> updateMethod = <>c.<>9__285_0;
            if (<>c.<>9__285_0 == null)
            {
                Action<ColumnBase, GridColumnData> local1 = <>c.<>9__285_0;
                updateMethod = <>c.<>9__285_0 = (column, cellData) => ((GridCellData) cellData).OnRowChanged();
            }
            this.IterateNotNullData(updateMethod);
        }

        private static void OnRowChanged(DependencyObject d, DependencyPropertyChangedEventArgs e)
        {
            ((RowData) d).OnRowChanged(e.OldValue, e.NewValue);
        }

        private void OnRowChanging(object oldRow, object newRow)
        {
            this.StopAnimation();
            this.IterateNotNullData((column, cellData) => ((GridCellData) cellData).OnRowChanging(oldRow, newRow));
        }

        protected virtual void OnRowHandleChanged(DevExpress.Xpf.Data.RowHandle newValue)
        {
            this.UpdateEvenRow();
            this.RowHandleCore = newValue;
            this.UpdateClientRowHandle(newValue);
            if (this.IsNewItemRow && (this.Row == null))
            {
                this.UpdateContent();
            }
        }

        private void OnRowPositionChanged()
        {
            this.ShowBottomLine = this.GetShowBottomLine();
            this.UpdateClientRowPosition();
        }

        internal void OnRowPropertyChanged(PropertyChangedEventArgs e)
        {
            if ((base.DataControl == null) || !base.DataControl.DataProviderBase.IsRefreshInProgress)
            {
                Action method = delegate {
                    if (this.DataControl == null)
                    {
                        this.UnsubcribeRowObject(this.Row);
                    }
                    else if (!this.ScheduleUpdateRow(e.PropertyName))
                    {
                        if ((e != null) && !string.IsNullOrEmpty(e.PropertyName))
                        {
                            this.UpdateDependentCellData(e.PropertyName);
                        }
                        else
                        {
                            this.UpdateDataOnRowPropertyChanged();
                        }
                        this.View.UpdateCellMergingPanels(false);
                    }
                };
                if (base.Dispatcher.CheckAccess())
                {
                    method();
                }
                else
                {
                    base.Dispatcher.BeginInvoke(method, new object[0]);
                }
            }
        }

        protected virtual void OnRowUpdated()
        {
            if (this.UseInUpdateQueue)
            {
                base.DataControl.OnRowUpdated(this.RowHandle.Value);
            }
        }

        private void OnValidationErrorChanged()
        {
            this.UpdateClientValidationError();
        }

        protected override void OnViewChanged()
        {
            base.OnViewChanged();
            if (base.CellDataCache != null)
            {
                Action<ColumnBase, GridColumnData> updateMethod = <>c.<>9__170_0;
                if (<>c.<>9__170_0 == null)
                {
                    Action<ColumnBase, GridColumnData> local1 = <>c.<>9__170_0;
                    updateMethod = <>c.<>9__170_0 = (column, cellData) => ((GridCellData) cellData).OnViewChanged();
                }
                this.IterateNotNullData(updateMethod);
            }
            this.UpdateClientView();
        }

        protected override void OnVisibilityChanged(bool isVisible)
        {
            if (!isVisible)
            {
                this.IsDirty = true;
            }
            if (isVisible)
            {
                this.SubcribeRowObject(this.Row);
            }
            else
            {
                this.UnsubcribeRowObject(this.Row);
            }
            if (isVisible)
            {
                IGridDataRow rowElement = this.RowElement as IGridDataRow;
                if (rowElement != null)
                {
                    rowElement.UpdateContentLayout();
                }
            }
        }

        internal static void ReassignCurrentRowData(DependencyObject sourceElement, DependencyObject targetElement)
        {
            if (targetElement != null)
            {
                SetCurrentRowData(targetElement, GetCurrentRowData(sourceElement));
            }
        }

        internal void RefreshData()
        {
            this.UpdateData();
            this.IsDirty = false;
            this.UpdateIsReady();
        }

        internal void ResetAnimationFormatInfoProvider()
        {
            this.ConditionalUpdater.ResetAnimationFormatInfoProvider();
        }

        internal virtual bool ScheduleUpdateRow(string propertyName) => 
            base.DataControl.ScheduleUpdateSingleRow(this.RowHandle.Value, propertyName, this.Row);

        public static void SetCurrentRowData(DependencyObject element, RowData value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(CurrentRowDataProperty, value);
        }

        public static void SetRowData(DependencyObject element, RowData value)
        {
            if (element == null)
            {
                throw new ArgumentNullException("element");
            }
            element.SetValue(RowDataProperty, value);
        }

        internal static void SetRowDataInternal(DependencyObject element, RowData value)
        {
            SetRowData(element, value);
            SetCurrentRowData(element, value);
        }

        internal static void SetRowHandleBinding(FrameworkElement element)
        {
            element.SetBinding(DataViewBase.RowHandleProperty, new Binding("RowHandle"));
        }

        internal virtual void SetRowStateClient(IRowStateClient rowStateClient)
        {
            this.ValidateSetRowStateClient();
            this.rowStateClient = rowStateClient;
            this.UpdateClientRowHandle(this.RowHandleCore);
            if (this.SelectionState != DevExpress.Xpf.Grid.SelectionState.None)
            {
                this.UpdateClientSelectionState();
            }
            ITableView view = base.View as ITableView;
            if (view != null)
            {
                this.UpdateClientScrollingMargin();
                if (!view.ShowHorizontalLines)
                {
                    this.UpdateClientHorizontalLineVisibility();
                }
                if (!view.ShowVerticalLines)
                {
                    this.UpdateClientVerticalLineVisibility();
                }
                this.UpdateClientFixedLineWidth();
                this.UpdateClientFixedLineVisibility();
                this.UpdateClientFixedNoneContentWidth();
                if (!view.ActualShowIndicator)
                {
                    this.UpdateClientShowIndicator();
                }
                this.UpdateClientIndicatorWidth();
                if (base.IndicatorState != IndicatorState.None)
                {
                    this.UpdateClientIndicatorState();
                }
                if (this.HasValidationErrorInternal)
                {
                    this.UpdateClientValidationError();
                }
                if ((base.DataControl != null) && (base.DataControl.BandsLayoutCore != null))
                {
                    this.UpdateCellsPanel();
                    if (base.DataControl.BandsLayoutCore.FixedNoneVisibleBands != null)
                    {
                        this.UpdateClientFixedNoneBands();
                    }
                    if (base.DataControl.BandsLayoutCore.FixedRightVisibleBands != null)
                    {
                        this.UpdateClientFixedRightBands();
                    }
                    if (base.DataControl.BandsLayoutCore.FixedLeftVisibleBands != null)
                    {
                        this.UpdateClientFixedLeftBands();
                    }
                }
                if (this.AlternateRow)
                {
                    this.UpdateClientAlternateBackground();
                }
                if (view.ActualShowDetailButtons)
                {
                    this.UpdateClientDetailExpandButtonVisibility();
                }
                if (this.DetailIndents != null)
                {
                    this.UpdateClientDetailViewIndents();
                }
                this.UpdateClientMinHeight();
                this.UpdateClientRowStyle();
                this.UpdateClientAppearance();
                this.UpdateClientInlineEditForm();
            }
            if (this.Level > 0)
            {
                this.UpdateClientLevel();
            }
            this.UpdateClientRowPosition();
            if (!base.View.IsKeyboardFocusWithinView)
            {
                this.UpdateClientFocusWithinState();
            }
            if (base.FixedNoneCellData != null)
            {
                this.UpdateClientFixedNoneCellData();
            }
            if (base.FixedLeftCellData != null)
            {
                this.UpdateClientFixedLeftCellData(null);
            }
            if (base.FixedRightCellData != null)
            {
                this.UpdateClientFixedRightCellData(null);
            }
            this.UpdateClientView();
            this.UpdateClientShowRowBreak();
        }

        internal void SetUpdateRowButtonsCache(ColumnBase column, object value)
        {
            if (!this.updateRowButtonsCache.ContainsKey(column))
            {
                this.updateRowButtonsCache.Add(column, value);
            }
            else
            {
                this.updateRowButtonsCache[column] = value;
            }
        }

        protected override bool ShouldUpdateCellDataCore(ColumnBase column, GridColumnData data) => 
            data.Data != this.DataContext;

        internal void StartAnimation(IList<IList<AnimationTimeline>> animations)
        {
            if (this.rowStateClient != null)
            {
                this.AnimationController.Start(animations, this.rowStateClient.CreateAnimationConnector());
            }
        }

        internal void StopAnimation()
        {
            if (this.IsAnimationControllerInited)
            {
                this.AnimationController.Flush();
            }
        }

        private void SubcribeRowObject(object obj)
        {
            if (this.subscriber != null)
            {
                this.subscriber.SubcribePropertyChanged(obj);
            }
        }

        private bool SupportsDataErrorInfo() => 
            (this.Row is IDataErrorInfo) || ((this.Row is IDXDataErrorInfo) || ((this.Row is INotifyDataErrorInfo) || (base.DataControl.DataProviderBase.HasValidationAttributes() || base.View.AllowLeaveInvalidEditor)));

        private bool SupportValidateCell() => 
            (base.View != null) && base.View.SupportValidateCell();

        protected virtual void SyncWithNode()
        {
        }

        private void UnsubcribeRowObject(object obj)
        {
            if (this.subscriber != null)
            {
                this.subscriber.UnsubcribePropertyChanged(obj);
            }
        }

        internal void UpatePropertyChangeSubscriptionMode()
        {
            ItemPropertyNotificationMode none = ItemPropertyNotificationMode.None;
            if (base.DataControl != null)
            {
                if (base.DataControl.DataProviderBase.SubscribeRowChangedForVisibleRows)
                {
                    none |= ItemPropertyNotificationMode.PropertyChanged;
                }
                if ((base.DataControl.DataView != null) && base.DataControl.DataView.ValidatesOnNotifyDataErrors)
                {
                    none |= ItemPropertyNotificationMode.ErrorsChanged;
                }
            }
            if ((this.subscriber == null) || (none != this.subscriber.NotificationMode))
            {
                this.UnsubcribeRowObject(this.Row);
                this.subscriber = new RowDataPropertyChangeSubscriber(this, none);
                this.SubcribeRowObject(this.Row);
            }
        }

        internal bool UpdateButtonIsFocused() => 
            (this.rowStateClient != null) && this.rowStateClient.UpdateButtonIsFocused();

        internal void UpdateButtonTabPress(bool prev)
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateButtonTabPress(prev);
            }
        }

        internal void UpdateCancelValuesCache()
        {
            this.CancelValuesCache.Clear();
            foreach (GridColumnData data in base.CellData)
            {
                this.CancelValuesCache.Add(data.Column, data.Value);
            }
        }

        internal void UpdateCellBackgroundAppearance()
        {
            Action<ColumnBase, GridColumnData> updateMethod = <>c.<>9__259_0;
            if (<>c.<>9__259_0 == null)
            {
                Action<ColumnBase, GridColumnData> local1 = <>c.<>9__259_0;
                updateMethod = <>c.<>9__259_0 = (column, data) => data.UpdateCellBackgroundAppearance();
            }
            this.IterateNotNullData(updateMethod);
        }

        internal override void UpdateCellData(ColumnBase column, GridColumnData cellData)
        {
            base.UpdateCellData(column, cellData);
            GridCellData data = (GridCellData) cellData;
            base.treeBuilder.UpdateCellData(this, data, column);
        }

        protected internal void UpdateCellDataEditorsDisplayText()
        {
            Action<ColumnBase, GridColumnData> updateMethod = <>c.<>9__300_0;
            if (<>c.<>9__300_0 == null)
            {
                Action<ColumnBase, GridColumnData> local1 = <>c.<>9__300_0;
                updateMethod = <>c.<>9__300_0 = delegate (ColumnBase column, GridColumnData cellData) {
                    GridCellData data = cellData as GridCellData;
                    if (data != null)
                    {
                        data.UpdateEditorDisplayText();
                    }
                };
            }
            this.IterateNotNullData(updateMethod);
        }

        internal virtual void UpdateCellDataError(ColumnBase column, GridColumnData cellData, bool customValidate = true)
        {
            if (this.updateError)
            {
                GridCellData data = (GridCellData) cellData;
                if (this.SupportsDataErrorInfo() || data.HasErrorCore)
                {
                    data.UpdateCellError(this.RowHandle, column, customValidate);
                }
            }
        }

        protected internal void UpdateCellDataLanguage()
        {
            if (base.View != null)
            {
                Action<ColumnBase, GridColumnData> updateMethod = <>c.<>9__140_0;
                if (<>c.<>9__140_0 == null)
                {
                    Action<ColumnBase, GridColumnData> local1 = <>c.<>9__140_0;
                    updateMethod = <>c.<>9__140_0 = (column, cellData) => ((GridCellData) cellData).UpdateLanguage();
                }
                this.IterateNotNullData(updateMethod);
            }
        }

        protected internal void UpdateCellDataValues()
        {
            Action<ColumnBase, GridColumnData> updateMethod = <>c.<>9__298_0;
            if (<>c.<>9__298_0 == null)
            {
                Action<ColumnBase, GridColumnData> local1 = <>c.<>9__298_0;
                updateMethod = <>c.<>9__298_0 = (column, cellData) => cellData.UpdateValue(false);
            }
            this.IterateNotNullData(updateMethod);
        }

        internal void UpdateCellForegroundAppearance()
        {
            Action<ColumnBase, GridColumnData> updateMethod = <>c.<>9__260_0;
            if (<>c.<>9__260_0 == null)
            {
                Action<ColumnBase, GridColumnData> local1 = <>c.<>9__260_0;
                updateMethod = <>c.<>9__260_0 = (column, data) => data.UpdateCellForegroundAppearance();
            }
            this.IterateNotNullData(updateMethod);
        }

        internal void UpdateCellsPanel()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateCellsPanel();
            }
        }

        internal void UpdateClientAlternateBackground()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateAlternateBackground();
            }
        }

        internal void UpdateClientAppearance()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateAppearance();
            }
        }

        internal virtual void UpdateClientCheckBoxSelector()
        {
        }

        internal void UpdateClientDetailExpandButtonVisibility()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateDetailExpandButtonVisibility();
            }
        }

        internal void UpdateClientDetailViewIndents()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateDetailViewIndents();
            }
        }

        internal void UpdateClientFixedLeftBands()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateFixedLeftBands();
            }
        }

        private void UpdateClientFixedLeftCellData(IList<GridColumnData> oldValue)
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateFixedLeftCellData(oldValue);
            }
        }

        internal void UpdateClientFixedLineHeight()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateFixedLineHeight();
            }
        }

        internal void UpdateClientFixedLineVisibility()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateFixedLineVisibility();
            }
        }

        internal void UpdateClientFixedLineWidth()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateFixedLineWidth();
            }
        }

        internal void UpdateClientFixedNoneBands()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateFixedNoneBands();
            }
        }

        private void UpdateClientFixedNoneCellData()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateFixedNoneCellData();
            }
        }

        internal void UpdateClientFixedNoneContentWidth()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateFixedNoneContentWidth();
            }
        }

        internal void UpdateClientFixedRightBands()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateFixedRightBands();
            }
        }

        private void UpdateClientFixedRightCellData(IList<GridColumnData> oldValue)
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateFixedRightCellData(oldValue);
            }
        }

        private void UpdateClientFixedRow()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateFixedRow();
            }
        }

        internal void UpdateClientFixRowButtonVisibility()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateFixRowButtonVisibility();
            }
        }

        internal void UpdateClientFixRowButtonWidth()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateFixRowButtonWidth();
            }
        }

        internal virtual void UpdateClientFocusWithinState()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateFocusWithinState();
            }
            DataViewBase view = base.View;
            if (view == null)
            {
                DataViewBase local1 = view;
            }
            else
            {
                view.UpdateRowFocusWithinState(this);
            }
        }

        internal virtual void UpdateClientGroupRowStyle()
        {
        }

        internal virtual void UpdateClientGroupRowTemplateSelector()
        {
        }

        internal virtual void UpdateClientGroupValueTemplateSelector()
        {
        }

        internal void UpdateClientHorizontalLineVisibility()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateHorizontalLineVisibility();
            }
        }

        internal void UpdateClientIndentScrolling()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateIndentScrolling();
            }
        }

        protected override void UpdateClientIndicatorState()
        {
            base.UpdateClientIndicatorState();
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateIndicatorState();
            }
        }

        internal void UpdateClientIndicatorWidth()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateIndicatorWidth();
            }
        }

        internal void UpdateClientInlineEditForm()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateInlineEditForm();
            }
        }

        internal virtual void UpdateClientIsFocused()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateIsFocused();
            }
        }

        private void UpdateClientLevel()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateLevel();
            }
        }

        internal void UpdateClientMinHeight()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateMinHeight();
            }
        }

        internal void UpdateClientOffsetLevel()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateOffsetLevel();
            }
        }

        private void UpdateClientRowHandle(DevExpress.Xpf.Data.RowHandle newValue)
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateRowHandle(newValue);
            }
            if (base.AdditionalElement != null)
            {
                DataViewBase.SetRowHandle(base.AdditionalElement, newValue);
            }
        }

        protected void UpdateClientRowPosition()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateRowPosition();
            }
        }

        internal void UpdateClientRowStyle()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateRowStyle();
            }
        }

        internal void UpdateClientScrollingMargin()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateScrollingMargin();
            }
        }

        private void UpdateClientSelectionState()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateSelectionState(this.SelectionState);
            }
            DataViewBase view = base.View;
            if (view == null)
            {
                DataViewBase local1 = view;
            }
            else
            {
                view.UpdateRowFocusWithinState(this);
            }
        }

        internal void UpdateClientShowIndicator()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateShowIndicator();
            }
        }

        private void UpdateClientShowRowBreak()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateShowRowBreak();
            }
        }

        internal virtual void UpdateClientSummary()
        {
        }

        private void UpdateClientValidationError()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateValidationError();
            }
        }

        protected internal virtual void UpdateClientVerticalLineVisibility()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateVerticalLineVisibility();
            }
        }

        private void UpdateClientView()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateView();
            }
        }

        internal void UpdateCommitRow()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateCommitRow();
            }
        }

        internal void UpdateCompactMode(DataTemplate template)
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateCompactMode(template);
            }
        }

        internal void UpdateContent()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateContent();
            }
        }

        protected internal virtual void UpdateData()
        {
            this.UpdateDataCore(null, -1);
        }

        protected internal virtual void UpdateDataCore(ColumnBase column, int listSourceRowIndex)
        {
            this.OnRowUpdated();
            this.formatInfoProvider.IsCachingEnabled = base.View.HasDataUpdateFormatConditions();
            bool flag = this.UpdateRowObjects(listSourceRowIndex);
            if ((!this.IsAsyncServerMode || this.GetIsReady()) && !this.updateOnlyData)
            {
                this.UpdateRowDataError();
                if (!this.IsGroupRowInAsyncServerMode & flag)
                {
                    if (column != null)
                    {
                        this.UpdateCellData(column, base.GetCellDataByColumn(column));
                    }
                    else
                    {
                        this.IterateNotNullData((col, cellData) => this.UpdateCellData(col, cellData));
                    }
                }
                this.UpdateGroupSummaryData();
                this.UpdateContent();
                this.RaiseContentChanged();
                base.treeBuilder.UpdateRowData(this);
                if (this.IsFocused && this.IsAsyncServerMode)
                {
                    base.DataControl.UpdateCurrentItem();
                }
                this.UpdateMasterDetailInfo(false, true);
                this.UpatePropertyChangeSubscriptionMode();
            }
        }

        internal void UpdateDataErrors(bool customValidate = true)
        {
            this.IterateNotNullData((column, cellData) => this.UpdateCellDataError(column, cellData, customValidate));
            this.UpdateRowDataError();
        }

        protected internal virtual void UpdateDataOnRowPropertyChanged()
        {
            this.UpdateData();
        }

        private void UpdateDependentCellData(DataDependentEntity dependency)
        {
            if (dependency.HasRowConditions)
            {
                this.UpdateData();
            }
            else
            {
                this.UpdateRowObjects(-1);
                this.IterateNotNullData(delegate (ColumnBase column, GridColumnData cellData) {
                    if (dependency.Columns.Contains(column))
                    {
                        cellData.UpdateValue(false);
                    }
                    else if (column.HasTemplateSelector)
                    {
                        cellData.OnDataChanged();
                    }
                });
                bool flag = this.SupportsDataErrorInfo();
                if (flag || this.SupportValidateCell())
                {
                    this.IterateNotNullData((column, cellData) => ((GridCellData) cellData).UpdateCellError(this.RowHandle, column, true));
                }
                if (flag)
                {
                    this.UpdateRowDataError();
                }
            }
        }

        internal virtual void UpdateDependentCellData(IEnumerable<string> propertyNames)
        {
            DataDependentEntity dependency = DataDependentEntity.Combine(from x in propertyNames select base.DataControl.GetPropertyChangeDependency(x));
            this.UpdateDependentCellData(dependency);
        }

        internal void UpdateDependentCellData(string propertyName)
        {
            this.UpdateDependentCellData(base.DataControl.GetPropertyChangeDependency(propertyName));
        }

        internal void UpdateDetails()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateDetails();
            }
        }

        protected internal virtual void UpdateEditorButtonVisibilities()
        {
            Action<ColumnBase, GridColumnData> updateMethod = <>c.<>9__137_0;
            if (<>c.<>9__137_0 == null)
            {
                Action<ColumnBase, GridColumnData> local1 = <>c.<>9__137_0;
                updateMethod = <>c.<>9__137_0 = (column, cellData) => ((GridCellData) cellData).UpdateEditorButtonVisibility();
            }
            this.IterateNotNullData(updateMethod);
        }

        protected internal virtual void UpdateEditorHighlightingText()
        {
            if (base.View != null)
            {
                Action<ColumnBase, GridColumnData> updateMethod = <>c.<>9__138_0;
                if (<>c.<>9__138_0 == null)
                {
                    Action<ColumnBase, GridColumnData> local1 = <>c.<>9__138_0;
                    updateMethod = <>c.<>9__138_0 = (column, cellData) => ((GridCellData) cellData).UpdateEditorHighlightingText(false);
                }
                this.IterateNotNullData(updateMethod);
            }
        }

        protected internal virtual void UpdateEditorHighlightingText(TextHighlightingProperties textHighlightingProperties, string[] columns)
        {
            if ((base.View != null) && ((columns != null) && (columns.Length != 0)))
            {
                this.IterateNotNullData(delegate (ColumnBase column, GridColumnData cellData) {
                    if (columns.Contains<string>(cellData.Column.FieldName))
                    {
                        ((GridCellData) cellData).UpdateEditorHighlightingText(false, textHighlightingProperties);
                    }
                });
            }
        }

        protected virtual void UpdateEvenRow()
        {
            if (this.RowHandle != null)
            {
                this.EvenRow = base.treeBuilder.IsEvenRow(this);
            }
        }

        protected internal override bool UpdateFixedRowPosition()
        {
            if (this.FixedRowPosition == base.node.FixedRowPosition)
            {
                return false;
            }
            this.FixedRowPosition = base.node.FixedRowPosition;
            return true;
        }

        protected internal override void UpdateFullState()
        {
            this.UpdateIsSelected();
            this.UpdateIsFocusedCore();
            this.UpdateIndicatorState();
            this.UpdateIsAlternateRow();
            this.IterateNotNullData((column, cellData) => ((GridCellData) cellData).UpdateFullState(this.RowHandle.Value));
        }

        internal virtual void UpdateGroupColumnSummaryElementStyle()
        {
        }

        protected internal virtual void UpdateGroupSummaryData()
        {
        }

        internal void UpdateHorizontalLineVisibility()
        {
            this.UpdateShowRowBreak();
            this.UpdateClientHorizontalLineVisibility();
        }

        protected internal virtual void UpdateIndentSelectionState()
        {
        }

        internal void UpdateIndicatorContentTemplate()
        {
            if (this.rowStateClient != null)
            {
                this.rowStateClient.UpdateIndicatorContentTemplate();
            }
        }

        internal void UpdateIndicatorState()
        {
            base.IndicatorState = base.View.ViewBehavior.GetIndicatorState(this);
        }

        protected void UpdateIsAlternateRow()
        {
            this.AlternateRow = base.View.ViewBehavior.IsAlternateRow(this.RowHandle.Value);
        }

        internal void UpdateIsDirty()
        {
            this.IsDirty = !this.IsRowInView();
        }

        protected internal virtual void UpdateIsFocused()
        {
            this.UpdateIsFocusedCore();
            this.UpdateSelectionState();
            this.UpdateIsFocusedCell();
        }

        internal void UpdateIsFocusedCell()
        {
            this.IterateNotNullData((column, cellData) => ((GridCellData) cellData).UpdateIsFocusedCell(this.RowHandle.Value));
        }

        private void UpdateIsFocusedCore()
        {
            this.IsFocused = base.View.IsFocusedView && (base.View.FocusedRowHandle == this.RowHandle.Value);
        }

        private void UpdateIsMasterRowExpanded()
        {
            base.IsMasterRowExpanded = base.DataControl.MasterDetailProvider.IsMasterRowExpanded(this.RowHandle.Value, null);
        }

        private void UpdateIsReady()
        {
            this.IsReady = this.GetIsReady();
        }

        protected internal virtual void UpdateIsSelected()
        {
            this.UpdateIsSelected(base.View.IsRowSelected(this.RowHandle.Value));
            this.IterateNotNullData((column, cellData) => ((GridCellData) cellData).UpdateIsSelected(this.RowHandle.Value));
        }

        protected internal virtual void UpdateIsSelected(bool forceIsSelected)
        {
            this.IsSelected = forceIsSelected;
        }

        private void UpdateLevel()
        {
            int level = this.controllerValues.Level;
            if (this.level != level)
            {
                this.level = level;
                base.NotifyPropertyChanged("Level");
                this.UpdateClientLevel();
                this.ShowBottomLine = this.GetShowBottomLine();
            }
        }

        internal override void UpdateLineLevel()
        {
            if (this.parentNodeContainer != null)
            {
                this.RowPosition = ((DataNodeContainer) this.parentNodeContainer).GetRowPosition(base.node);
            }
            bool isLastRow = false;
            if ((((this.RowPosition != DevExpress.Xpf.Grid.RowPosition.Bottom) && (this.RowPosition != DevExpress.Xpf.Grid.RowPosition.Single)) || base.View.ShowFixedTotalSummary) || base.View.ShowTotalSummary)
            {
                base.LineLevel = 0;
                base.DetailLevel = base.GetDetailLevel(this, 0, ref isLastRow);
            }
            else if (base.DataControl.VisibleRowCount > (base.DataControl.GetRowVisibleIndexByHandleCore(this.RowHandle.Value) + 1))
            {
                base.LineLevel = 0;
                base.DetailLevel = base.GetDetailLevel(this, 0, ref isLastRow);
            }
            else
            {
                DataViewBase targetView = null;
                int targetVisibleIndex = -1;
                if (base.DataControl.DataControlParent.FindMasterRow(out targetView, out targetVisibleIndex))
                {
                    RowData rowData = targetView.GetRowData(targetView.DataControl.GetRowHandleByVisibleIndexCore(targetVisibleIndex));
                    if (rowData != null)
                    {
                        isLastRow = (rowData.RowPosition == DevExpress.Xpf.Grid.RowPosition.Bottom) || (rowData.RowPosition == DevExpress.Xpf.Grid.RowPosition.Single);
                        base.LineLevel = this.GetLineLevel(this, 0, 0);
                        base.DetailLevel = base.GetDetailLevel(this, 0, ref isLastRow);
                        if (isLastRow)
                        {
                            base.LineLevel = base.DetailLevel;
                        }
                        return;
                    }
                }
                base.LineLevel = 0;
                base.DetailLevel = 0;
            }
        }

        protected virtual void UpdateMasterDetailInfo(bool updateRowObjectIfRowExpanded, bool updateDetailRow)
        {
            if (base.treeBuilder.SupportsMasterDetail)
            {
                base.View.DataControl.MasterDetailProvider.UpdateMasterDetailInfo(this, updateDetailRow);
                if (updateRowObjectIfRowExpanded && this.IsRowExpanded)
                {
                    this.UpdateRowObjects(-1);
                }
                this.UpdateIsMasterRowExpanded();
            }
        }

        protected virtual void UpdateNextRowLevel()
        {
            if ((this.RowHandle != null) && ((base.View != null) && ((base.View.DataControl != null) && (base.View.DataControl.DataProviderBase != null))))
            {
                this.NextRowLevel = base.treeBuilder.GetRowLevelByVisibleIndex(base.treeBuilder.GetRowVisibleIndexByHandleCore(this.RowHandle.Value) + 1);
            }
            else
            {
                this.NextRowLevel = 0;
            }
        }

        internal void UpdateObservablePropertyScheme()
        {
            this.SubcribeRowObject(this.Row);
        }

        protected internal void UpdatePrintingMergeValue()
        {
            Action<ColumnBase, GridColumnData> updateMethod = <>c.<>9__301_0;
            if (<>c.<>9__301_0 == null)
            {
                Action<ColumnBase, GridColumnData> local1 = <>c.<>9__301_0;
                updateMethod = <>c.<>9__301_0 = delegate (ColumnBase column, GridColumnData cellData) {
                    GridCellData data = cellData as GridCellData;
                    if (data != null)
                    {
                        data.UpdatePrintingMergeValue();
                    }
                };
            }
            this.IterateNotNullData(updateMethod);
        }

        internal override void UpdateRow()
        {
            this.Row = base.treeBuilder.GetRowValue(this);
        }

        internal virtual void UpdateRowDataError()
        {
            if (this.updateError && (this.SupportsDataErrorInfo() || ((BaseEditHelper.GetValidationError(this) != null) || base.View.SupportValidateRow())))
            {
                base.treeBuilder.UpdateRowDataError(this);
            }
        }

        protected bool UpdateRowObjects(int listSourceRowIndex)
        {
            object row = this.Row;
            this.RowState = base.DataControl.GetRowState(this.RowHandle.Value, false);
            if (!this.IsGroupRowInAsyncServerMode)
            {
                this.UpdateRow();
                if ((this.Row == null) && (listSourceRowIndex != -1))
                {
                    this.Row = base.View.DataProviderBase.GetRowByListIndex(listSourceRowIndex);
                }
            }
            if (this.RowHandle.Value != -2147483645)
            {
                this.DataContext = base.treeBuilder.GetWpfRow(this, listSourceRowIndex);
            }
            if (!this.IsGroupRowInAsyncServerMode && (this.IsSameRow(row) && (!DevExpress.Xpf.Grid.DataRowNode.DisableUpdateOptimization && !base.View.ShouldUpdateRow(this.Row))))
            {
                return false;
            }
            this.ConditionalUpdater.OnDataChanged();
            return true;
        }

        internal void UpdateSelectionState()
        {
            if (!this.formatInfoProvider.IsCachingEnabled || ((base.DataControl == null) || (this.Row == base.treeBuilder.GetRowValue(this))))
            {
                this.SelectionState = base.View.GetRowSelectionState(this.RowHandle.Value, (this.RowElement != null) && this.RowElement.IsMouseOver);
                RowData data1 = this;
                if (<>c.<>9__167_0 == null)
                {
                    data1 = (RowData) (<>c.<>9__167_0 = (column, cellData) => ((GridCellData) cellData).UpdateSelectionState());
                }
                <>c.<>9__167_0.IterateNotNullData((Action<ColumnBase, GridColumnData>) data1);
            }
        }

        private void UpdateShowRowBreak()
        {
            BaseGridController dataController = base.DataControl?.DataProviderBase.DataController;
            bool flag = (base.View is ITableView) && ((ITableView) base.View).ShowHorizontalLines;
            if (!((((dataController != null) && dataController.AllowPartialGrouping) && (this.RowHandle != null)) & flag))
            {
                this.ShowRowBreak = false;
            }
            else
            {
                int controllerDataRowHanlde = this.RowHandle.Value;
                VisibleIndexCollection visibleIndexes = dataController.GetVisibleIndexes();
                if (visibleIndexes.IsSingleGroupRow(controllerDataRowHanlde))
                {
                    this.ShowRowBreak = true;
                }
                else
                {
                    int rowHandleByVisibleIndexCore = base.treeBuilder.GetRowHandleByVisibleIndexCore(base.treeBuilder.GetRowVisibleIndexByHandleCore(controllerDataRowHanlde) + 1);
                    this.ShowRowBreak = base.DataControl.IsGroupRowHandleCore(rowHandleByVisibleIndexCore) || visibleIndexes.IsSingleGroupRow(rowHandleByVisibleIndexCore);
                }
            }
        }

        protected virtual void ValidateRowsContainer()
        {
        }

        internal virtual void ValidateSetRowStateClient()
        {
            if (this.rowStateClient != null)
            {
                throw new InvalidOperationException();
            }
        }

        internal DevExpress.Xpf.Data.RowHandle RowHandleCore { get; private set; }

        [Description("Gets the row's handle.")]
        public virtual DevExpress.Xpf.Data.RowHandle RowHandle
        {
            get => 
                this.rowHandle;
            internal set
            {
                if (!ReferenceEquals(this.rowHandle, value))
                {
                    this.rowHandle = value;
                    base.RaisePropertyChanged("RowHandle");
                    this.OnRowHandleChanged(this.rowHandle);
                }
            }
        }

        [Description("Gets or sets the row's data context.")]
        public object DataContext
        {
            get => 
                this.dataContext;
            set
            {
                if (this.dataContext != value)
                {
                    this.dataContext = value;
                    this.OnDataContextChanged();
                    base.RaisePropertyChanged("DataContext");
                }
            }
        }

        [Description("Gets a row object that corresponds to the row.")]
        public object Row
        {
            get => 
                this.row;
            set
            {
                if (this.row != value)
                {
                    object row = this.row;
                    this.OnRowChanging(this.row, value);
                    this.row = value;
                    base.RaisePropertyChanged("Row");
                    this.OnRowChanged(row, this.row);
                }
            }
        }

        [Description("Gets  an object that stores a row's state.")]
        public DependencyObject RowState
        {
            get
            {
                this.rowState ??= base.DataControl.GetRowState(this.RowHandle.Value, true);
                return this.rowState;
            }
            private set
            {
                if (!ReferenceEquals(this.rowState, value))
                {
                    this.rowState = value;
                    base.RaisePropertyChanged("RowState");
                }
            }
        }

        [Description("Gets the row's position within a View.")]
        public DevExpress.Xpf.Grid.RowPosition RowPosition
        {
            get => 
                this.rowPosition;
            private set
            {
                if (this.rowPosition != value)
                {
                    this.rowPosition = value;
                    base.RaisePropertyChanged("RowPosition");
                    this.OnRowPositionChanged();
                }
            }
        }

        [Description("Indicates whether the row's bottom line is visible.")]
        public bool ShowBottomLine
        {
            get => 
                this.showBottomLine;
            protected set
            {
                if (this.showBottomLine != value)
                {
                    this.showBottomLine = value;
                    base.RaisePropertyChanged("ShowBottomLine");
                }
            }
        }

        [Description("Gets whether the row's visible index is even.")]
        public bool EvenRow
        {
            get => 
                this.evenRow;
            protected set
            {
                if (this.evenRow != value)
                {
                    this.evenRow = value;
                    base.RaisePropertyChanged("EvenRow");
                }
            }
        }

        [Description("Gets whether the row uses the alternate background.")]
        public bool AlternateRow
        {
            get => 
                this.alternateRow;
            protected set
            {
                if (this.alternateRow != value)
                {
                    this.alternateRow = value;
                    base.RaisePropertyChanged("AlternateRow");
                    this.OnAlternateRowChanged();
                }
            }
        }

        [Description("Gets whether the row is selected.")]
        public bool IsSelected
        {
            get => 
                this.isSelected;
            private set
            {
                if (this.isSelected != value)
                {
                    this.isSelected = value;
                    base.RaisePropertyChanged("IsSelected");
                    this.UpdateSelectionState();
                }
            }
        }

        [Description("Gets whether the row is focused.")]
        public bool IsFocused
        {
            get => 
                this.isFocused;
            private set
            {
                if (this.isFocused != value)
                {
                    this.isFocused = value;
                    base.RaisePropertyChanged("IsFocused");
                    this.UpdateSelectionState();
                    this.UpdateClientIsFocused();
                    this.UpdateEditorButtonVisibilities();
                }
            }
        }

        [Description("Gets whether a group row is expanded.")]
        public bool IsRowExpanded
        {
            get => 
                this.isRowExpanded;
            internal set
            {
                if (this.isRowExpanded != value)
                {
                    this.isRowExpanded = value;
                    base.RaisePropertyChanged("IsRowExpanded");
                    base.RaisePropertyChanged("CollapseBottomLine");
                    this.OnIsRowExpandedChanged();
                }
            }
        }

        [Description("This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public bool CollapseBottomLine =>
            this.isRowExpanded && (this.SelectionState == DevExpress.Xpf.Grid.SelectionState.None);

        [Description("This member supports the internal infrastructure and is not intended to be used directly from your code.")]
        public bool IsRowVisible
        {
            get => 
                this.isRowVisible;
            internal set
            {
                if (this.isRowVisible != value)
                {
                    this.isRowVisible = value;
                    base.RaisePropertyChanged("IsRowVisible");
                    this.OnIsRowVisibleChanged();
                }
            }
        }

        [Description("Gets whether a group row is being expanded.")]
        public bool IsExpanding
        {
            get => 
                this.isExpanding;
            internal set
            {
                if (this.isExpanding != value)
                {
                    this.isExpanding = value;
                    base.RaisePropertyChanged("IsExpanding");
                }
            }
        }

        [Description("Gets a value that indicates the row's selection state.")]
        public DevExpress.Xpf.Grid.SelectionState SelectionState
        {
            get => 
                this.selectionState;
            private set
            {
                if (this.selectionState != value)
                {
                    this.selectionState = value;
                    base.RaisePropertyChanged("SelectionState");
                    base.RaisePropertyChanged("CollapseBottomLine");
                    this.UpdateClientSelectionState();
                }
            }
        }

        [Description("Gets the next row's nesting level.")]
        public int NextRowLevel
        {
            get => 
                this.nextRowLevel;
            protected set
            {
                if (this.nextRowLevel != value)
                {
                    this.nextRowLevel = value;
                    base.RaisePropertyChanged("NextRowLevel");
                }
            }
        }

        [Description("This member supports the internal infrastructure, and is not intended to be used directly from your code.")]
        public bool ShowRowBreak
        {
            get => 
                this.showRowBreak;
            private set
            {
                if (this.showRowBreak != value)
                {
                    this.showRowBreak = value;
                    this.UpdateClientShowRowBreak();
                    base.RaisePropertyChanged("ShowRowBreak");
                }
            }
        }

        [Browsable(false)]
        public bool IsEditFormVisible
        {
            get => 
                this.isEditFormVisible;
            internal set
            {
                if (this.isEditFormVisible != value)
                {
                    this.isEditFormVisible = value;
                    base.RaisePropertyChanged("IsEditFormVisible");
                }
            }
        }

        [Description("Gets whether the row's data has been loaded or not.")]
        public bool IsReady
        {
            get => 
                this.isReady;
            private set
            {
                if (this.isReady != value)
                {
                    this.isReady = value;
                    this.OnIsReadyChanged();
                    base.RaisePropertyChanged("IsReady");
                }
            }
        }

        public DevExpress.Xpf.Grid.FixedRowPosition FixedRowPosition
        {
            get => 
                this.fixedRowPosition;
            private set
            {
                if (this.fixedRowPosition != value)
                {
                    this.fixedRowPosition = value;
                    this.UpdateClientFixedRow();
                    base.RaisePropertyChanged("FixedRowPosition");
                }
            }
        }

        public bool IsLastFixedRow
        {
            get => 
                this.isLastFixedRow;
            private set
            {
                if (this.isLastFixedRow != value)
                {
                    this.isLastFixedRow = value;
                    this.UpdateClientFixedRow();
                    base.RaisePropertyChanged("IsLastFixedRow");
                }
            }
        }

        protected bool IsNewItemRow =>
            (this.RowHandle != null) && (this.RowHandle.Value == -2147483647);

        internal DevExpress.Xpf.Grid.DataRowNode DataRowNode =>
            (DevExpress.Xpf.Grid.DataRowNode) base.node;

        internal virtual FrameworkElement RowElement =>
            base.WholeRowElement;

        internal DevExpress.Xpf.Grid.Native.VisualDataTreeBuilder VisualDataTreeBuilder =>
            base.treeBuilder as DevExpress.Xpf.Grid.Native.VisualDataTreeBuilder;

        internal bool CanUpdateErrors =>
            this.updateError;

        internal DataControllerValuesContainer ControllerValues
        {
            get
            {
                if (this.controllerValues == null)
                {
                    this.controllerValues = DataIteratorBase.CreateValuesContainer(base.treeBuilder, this.RowHandle);
                    this.UpdateLevel();
                }
                return this.controllerValues;
            }
            private set
            {
                if (!ReferenceEquals(value, this.controllerValues))
                {
                    this.controllerValues = value;
                    this.UpdateLevel();
                }
            }
        }

        internal override object MatchKey =>
            this.RowHandle;

        [Description("Gets the row's grouping level.")]
        public override int Level =>
            this.level;

        [Description("Gets the row's visible position within a View.")]
        public int ControllerVisibleIndex =>
            this.ControllerValues.VisibleIndex;

        protected override bool UpdateOnlyData =>
            this.updateOnlyData;

        private bool IsAsyncServerMode =>
            (base.DataControl != null) && ((base.DataControl.DataProviderBase != null) && base.DataControl.DataProviderBase.IsAsyncServerMode);

        internal bool IsGroupRowInAsyncServerMode =>
            this.IsAsyncServerMode && base.DataControl.IsGroupRowHandleCore(this.RowHandle.Value);

        internal bool UseInUpdateQueue { get; set; }

        internal bool IsDirty { get; private set; }

        internal bool CustomCellValidate =>
            this.customCellValidate;

        private bool UpdateImmediately =>
            this.UpdateImmediatelyCore || ((base.node == null) || (!base.View.MasterRootNodeContainer.IsScrolling && !this.GetRootDataPresenterAdjustmentInProgress()));

        protected virtual bool UpdateImmediatelyCore =>
            !base.View.RootView.ViewBehavior.AllowCascadeUpdate;

        internal BaseValidationError ValidationErrorInternal =>
            BaseEdit.GetValidationError(this);

        internal bool HasValidationErrorInternal =>
            this.ValidationErrorInternal != null;

        internal IList<DetailIndent> DetailIndents
        {
            get
            {
                if (base.DataControl == null)
                {
                    return null;
                }
                DetailDescriptorBase ownerDetailDescriptor = base.DataControl.OwnerDetailDescriptor;
                return ownerDetailDescriptor?.DetailViewIndents;
            }
        }

        internal bool UpdateRowButtonsWasChanged { get; set; }

        internal VersionedFormatInfoProvider FormatInfoProvider =>
            this.formatInfoProvider;

        protected internal override DevExpress.Xpf.Grid.FixedRowPosition FixedRowPositionCore =>
            this.FixedRowPosition;

        private bool IsAnimationControllerInited =>
            this.animationControllerCore != null;

        internal DevExpress.Xpf.Grid.Native.AnimationController AnimationController
        {
            get
            {
                this.animationControllerCore ??= new DevExpress.Xpf.Grid.Native.AnimationController();
                return this.animationControllerCore;
            }
            set => 
                this.animationControllerCore = value;
        }

        private ConditionalClientAppearanceUpdaterBase ConditionalUpdater
        {
            get
            {
                this.conditionalUpdater ??= new ConditionalRowAppearanceUpdater(this);
                return this.conditionalUpdater;
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RowData.<>c <>9 = new RowData.<>c();
            public static Func<RowData, DataTreeBuilder> <>9__134_0;
            public static Func<RowData, string, object> <>9__134_1;
            public static Action<ColumnBase, GridColumnData> <>9__137_0;
            public static Action<ColumnBase, GridColumnData> <>9__138_0;
            public static Action<ColumnBase, GridColumnData> <>9__140_0;
            public static Action<ColumnBase, GridColumnData> <>9__167_0;
            public static Action<ColumnBase, GridColumnData> <>9__170_0;
            public static Action<ColumnBase, GridColumnData> <>9__259_0;
            public static Action<ColumnBase, GridColumnData> <>9__260_0;
            public static Action<ColumnBase, GridColumnData> <>9__285_0;
            public static Action<ColumnBase, GridColumnData> <>9__293_0;
            public static Action<ColumnBase, GridColumnData> <>9__298_0;
            public static Action<ColumnBase, GridColumnData> <>9__300_0;
            public static Action<ColumnBase, GridColumnData> <>9__301_0;

            internal void <.cctor>b__7_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((RowData) d).UpdateClientValidationError();
            }

            internal DataTreeBuilder <.ctor>b__134_0(RowData x) => 
                x.treeBuilder;

            internal object <.ctor>b__134_1(RowData x, string fieldName) => 
                x.treeBuilder.GetCellValue(x, fieldName);

            internal void <OnIsReadyChanged>b__293_0(ColumnBase column, GridColumnData cellData)
            {
                ((GridCellData) cellData).UpdateIsReady();
            }

            internal void <OnRowChanged>b__285_0(ColumnBase column, GridColumnData cellData)
            {
                ((GridCellData) cellData).OnRowChanged();
            }

            internal void <OnViewChanged>b__170_0(ColumnBase column, GridColumnData cellData)
            {
                ((GridCellData) cellData).OnViewChanged();
            }

            internal void <UpdateCellBackgroundAppearance>b__259_0(ColumnBase column, GridColumnData data)
            {
                data.UpdateCellBackgroundAppearance();
            }

            internal void <UpdateCellDataEditorsDisplayText>b__300_0(ColumnBase column, GridColumnData cellData)
            {
                GridCellData data = cellData as GridCellData;
                if (data != null)
                {
                    data.UpdateEditorDisplayText();
                }
            }

            internal void <UpdateCellDataLanguage>b__140_0(ColumnBase column, GridColumnData cellData)
            {
                ((GridCellData) cellData).UpdateLanguage();
            }

            internal void <UpdateCellDataValues>b__298_0(ColumnBase column, GridColumnData cellData)
            {
                cellData.UpdateValue(false);
            }

            internal void <UpdateCellForegroundAppearance>b__260_0(ColumnBase column, GridColumnData data)
            {
                data.UpdateCellForegroundAppearance();
            }

            internal void <UpdateEditorButtonVisibilities>b__137_0(ColumnBase column, GridColumnData cellData)
            {
                ((GridCellData) cellData).UpdateEditorButtonVisibility();
            }

            internal void <UpdateEditorHighlightingText>b__138_0(ColumnBase column, GridColumnData cellData)
            {
                ((GridCellData) cellData).UpdateEditorHighlightingText(false);
            }

            internal void <UpdatePrintingMergeValue>b__301_0(ColumnBase column, GridColumnData cellData)
            {
                GridCellData data = cellData as GridCellData;
                if (data != null)
                {
                    data.UpdatePrintingMergeValue();
                }
            }

            internal void <UpdateSelectionState>b__167_0(ColumnBase column, GridColumnData cellData)
            {
                ((GridCellData) cellData).UpdateSelectionState();
            }
        }

        protected class RowDataReusingStrategy : RowDataBase.NotImplementedRowDataReusingStrategy
        {
            protected readonly RowData rowData;

            public RowDataReusingStrategy(RowData rowData)
            {
                this.rowData = rowData;
            }

            internal override void AssignFrom(RowsContainer parentRowsContainer, NodeContainer parentNodeContainer, RowNode rowNode, bool forceUpdate = false)
            {
                this.rowData.AssignFrom(parentRowsContainer, parentNodeContainer, rowNode, forceUpdate);
            }

            internal override void CacheRowData()
            {
                this.rowData.CacheRowData();
            }

            internal override FrameworkElement CreateRowElement() => 
                this.rowData.CreateRowElement();
        }
    }
}

