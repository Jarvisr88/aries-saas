namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Data.Linq;
    using DevExpress.Mvvm.Native;
    using DevExpress.Utils;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.GridData;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Text;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Media;
    using System.Windows.Threading;

    public abstract class DataViewBehavior
    {
        private readonly DataViewBase view;
        private bool isUpdateScrollInfoNeeded = true;
        private bool lockTopRowIndexUpdate;
        private bool useLegacyColumnVisibleIndexes;
        private RebuildColumnsLayoutHelperBase rebuildColumnsLayoutHelper;
        private Dictionary<ServiceSummaryItemKey, ServiceSummaryItem> emptyServiceSummaryDictionary = new Dictionary<ServiceSummaryItemKey, ServiceSummaryItem>();

        protected DataViewBehavior(DataViewBase view)
        {
            Guard.ArgumentNotNull(view, "view");
            this.view = view;
        }

        internal void ApplyColumnVisibleIndex(BaseColumn column, int oldVisibleIndex)
        {
            this.RebuildColumnsLayoutHelper.ApplyColumnVisibleIndex(column, oldVisibleIndex);
        }

        protected internal virtual void ApplyResize(BaseColumn column, double value, double maxWidth)
        {
        }

        protected internal virtual void BeforeShowEditForm()
        {
        }

        protected internal virtual double CalcColumnMaxWidth(ColumnBase column) => 
            0.0;

        protected internal virtual double CalcVerticalDragIndicatorSize(UIElement panel, Point point, double width) => 
            width;

        internal virtual bool CanAdjustScrollbarCore() => 
            false;

        internal virtual bool CanBestFitAllColumns() => 
            false;

        internal virtual bool CanBestFitColumnCore(BaseColumn column) => 
            false;

        public virtual void ChangeHorizontalOffsetBy(double delta)
        {
            this.View.DataPresenter.SetHorizontalOffsetForce(this.View.DataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset + delta);
        }

        public virtual void ChangeVerticalOffsetBy(double delta)
        {
            this.View.DataPresenter.SetVerticalOffsetForce(this.View.DataPresenter.ScrollInfoCore.VerticalScrollInfo.Offset + delta);
        }

        internal virtual bool CheckNavigationStyle(int newValue) => 
            this.View.NavigationStyle == GridViewNavigationStyle.None;

        internal virtual void CopyToDetail(DataControlBase dataControl)
        {
        }

        protected internal virtual Point CorrectDragIndicatorLocation(UIElement panel, Point point) => 
            point;

        protected internal virtual Size CorrectMeasureResult(double scrollOffset, Size constraint, Size result) => 
            result;

        internal virtual GridViewNavigationBase CreateAdditionalRowNavigation() => 
            new DummyNavigation(this.View);

        internal abstract GridViewNavigationBase CreateCellNavigation();
        internal virtual MasterDetailProviderBase CreateMasterDetailProvider() => 
            NullDetailProvider.Instance;

        private RebuildColumnsLayoutHelperBase CreateRebuildColumnsLayoutHelper(bool UseLegacyColumnVisibleIndexes) => 
            !UseLegacyColumnVisibleIndexes ? ((RebuildColumnsLayoutHelperBase) new DevExpress.Xpf.Grid.Native.RebuildColumnsLayoutHelper(this.View)) : ((RebuildColumnsLayoutHelperBase) new RebuildColumnsLayoutHelperLegacy(this.View));

        protected internal virtual RowData CreateRowDataCore(DataTreeBuilder treeBuilder, bool updateOnlyData) => 
            new RowData(treeBuilder, updateOnlyData, true, true);

        internal abstract GridViewNavigationBase CreateRowNavigation();
        protected internal virtual bool EndRowEdit()
        {
            this.View.IsFocusedRowModified = false;
            if (this.DataControl == null)
            {
                return false;
            }
            bool isCurrentRowEditing = this.View.DataProviderBase.IsCurrentRowEditing;
            bool result = false;
            this.DataControl.UpdateFocusedRowDataposponedAction.PerformLockedAction(delegate {
                result = this.View.DataProviderBase.EndCurrentRowEdit();
            });
            if ((this.DataControl.DataProviderBase.IsICollectionView & isCurrentRowEditing) && (this.DataControl.DataProviderBase.DataController.DataSource is ICollectionViewHelper))
            {
                this.DataControl.UpdateTotalSummary();
            }
            return result;
        }

        internal virtual void EnsureSurroundingsActualSize(Size finalSize)
        {
        }

        internal virtual Style GetActualCellStyle(ColumnBase column) => 
            (column.CellStyle == null) ? this.View.CellStyle : column.CellStyle;

        protected internal virtual ControlTemplate GetActualColumnChooserTemplate() => 
            (this.View.ColumnChooserColumnDisplayMode != ColumnChooserColumnDisplayMode.ShowHiddenColumnsOnly) ? this.View.ExtendedColumnChooserTemplate : this.View.ColumnChooserTemplate;

        internal virtual FixedStyle GetActualColumnFixed(BaseColumn column) => 
            column.Fixed;

        protected internal virtual FrameworkElement GetAdditionalRowElement(int rowHandle) => 
            null;

        internal Tuple<ColumnBase, BandedViewDropPlace> GetColumnDropTarget(ColumnBase source, int targetVisibleIndex, HeaderPresenterType moveFrom) => 
            this.RebuildColumnsLayoutHelper.GetColumnDropTarget(source, targetVisibleIndex, moveFrom);

        protected internal abstract void GetDataRowText(StringBuilder sb, int rowHandle);
        protected internal abstract double GetFixedExtent();
        protected internal virtual double GetFixedNoneContentWidth(double totalWidth, int rowHandle) => 
            totalWidth;

        protected internal virtual IndicatorState GetIndicatorState(RowData rowData) => 
            IndicatorState.None;

        protected internal virtual bool GetIsCellSelected(int rowHandle, ColumnBase column) => 
            false;

        protected internal virtual RowData GetRowData(int rowHandle)
        {
            RowData data;
            return (!this.View.VisualDataTreeBuilder.Rows.TryGetValue(rowHandle, out data) ? null : ((!ReferenceEquals(data.View, this.View) || (data.RowHandle.Value != rowHandle)) ? null : data));
        }

        protected internal virtual DependencyObject GetRowState(int rowHandle) => 
            null;

        internal virtual IDictionary<ServiceSummaryItemKey, ServiceSummaryItem> GetServiceSummaries() => 
            this.emptyServiceSummaryDictionary;

        [IteratorStateMachine(typeof(<GetServiceUnboundColumns>d__168))]
        internal virtual IEnumerable<IColumnInfo> GetServiceUnboundColumns() => 
            new <GetServiceUnboundColumns>d__168(-2);

        protected internal int GetTopRowHandle() => 
            this.DataControl.GetRowHandleByVisibleIndexCore(this.View.IsPagingMode ? this.View.FirstVisibleIndexOnPage : 0);

        internal virtual int GetValueForSelectionAnchorRowHandle(int value) => 
            ((value == -2147483648) || !this.View.IsFocusedRowInCurrentPageBounds()) ? this.GetTopRowHandle() : value;

        protected internal virtual KeyValuePair<DataViewBase, int> GetViewAndVisibleIndex(double verticalOffset, bool calcDataArea = true) => 
            new KeyValuePair<DataViewBase, int>(this.View, 0);

        internal virtual bool HasRowConditions() => 
            false;

        private int IndexToHandle(int index) => 
            this.DataControl.GetRowHandleByVisibleIndexCore(index);

        protected internal virtual bool IsAdditionalRow(int rowHandle) => 
            this.IsAdditionalRowCore(rowHandle);

        internal bool IsAdditionalRowCore(int rowHandle) => 
            (rowHandle == -2147483645) || (rowHandle == -2147483647);

        protected internal virtual bool IsAdditionalRowData(RowData rowData) => 
            this.IsAdditionalRow(rowData.RowHandle.Value);

        protected internal virtual bool IsAlternateRow(int rowHandle) => 
            false;

        private bool IsFirst(int index) => 
            this.DataControl.IsFirst(index);

        protected internal virtual bool IsFirstColumn(BaseColumn column, bool isPrinting) => 
            false;

        internal virtual bool IsFirstOrTreeColumn(BaseColumn column, bool isPrinting) => 
            this.IsFirstColumn(column, isPrinting);

        private bool IsLast(int index) => 
            this.DataControl.IsLast(index);

        internal virtual bool IsRowIndicator(DependencyObject originalSource) => 
            false;

        protected internal void IterateCells(int startRowHandle, int startColumnIndex, int endRowHandle, int endColumnIndex, Action<int, ColumnBase> action)
        {
            int num;
            int num2;
            int num3;
            int num4;
            this.ValidateIndexes(startRowHandle, startColumnIndex, endRowHandle, endColumnIndex, out num, out num2, out num3, out num4);
            int visibleIndex = num;
            while (visibleIndex < (num3 + 1))
            {
                int num6 = num2;
                while (true)
                {
                    if (num6 >= (num4 + 1))
                    {
                        visibleIndex++;
                        break;
                    }
                    if ((num6 >= 0) && (num6 <= (this.View.VisibleColumnsCore.Count - 1)))
                    {
                        action(this.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex), this.View.VisibleColumnsCore[num6]);
                    }
                    num6++;
                }
            }
        }

        internal virtual void MakeCellVisible()
        {
        }

        internal virtual void MoveNextRow()
        {
            this.MoveNextRowCore();
        }

        protected bool MoveNextRowCore()
        {
            if (this.View.IsAdditionalRowFocused || !this.View.IsFocusedRowInCurrentPageBounds())
            {
                return false;
            }
            if (!this.DataControl.NavigateToFirstChildDetailRow())
            {
                if (this.View.IsBottomNewItemRowFocused)
                {
                    return false;
                }
                int index = (this.FocusedRowHandle == -2147483648) ? -1 : this.CurrentIndex;
                if (this.IsLast(index))
                {
                    this.View.CommitEditing();
                    return false;
                }
                int num2 = index + 1;
                this.View.SetFocusedRowHandle(this.IndexToHandle(num2));
            }
            return true;
        }

        internal virtual void MovePrevRow()
        {
            this.MovePrevRowCore();
        }

        protected bool MovePrevRowCore()
        {
            if (this.View.IsAdditionalRowFocused || !this.View.IsFocusedRowInCurrentPageBounds())
            {
                return false;
            }
            if (!this.DataControl.NavigateToPreviousInnerDetailRow())
            {
                int index = (this.FocusedRowHandle == -2147483648) ? 1 : this.CurrentIndex;
                if (this.IsFirst(index))
                {
                    return !this.View.CommitEditing();
                }
                int num2 = index - 1;
                this.View.SetFocusedRowHandle(this.IndexToHandle(num2));
            }
            return true;
        }

        internal virtual void NotifyBandsLayoutChanged()
        {
        }

        internal virtual void NotifyFixedLeftBandsChanged()
        {
        }

        internal virtual void NotifyFixedNoneBandsChanged()
        {
        }

        internal virtual void NotifyFixedRightBandsChanged()
        {
        }

        internal virtual void OnAfterMouseLeftButtonDown(IDataViewHitInfo hitInfo)
        {
        }

        internal virtual void OnBandsLayoutChanged()
        {
        }

        protected internal virtual void OnCancelRowEdit()
        {
            if (!this.View.AreUpdateRowButtonsShown)
            {
                this.View.IsFocusedRowModified = false;
            }
            else
            {
                RowData rowData = this.GetRowData(this.FocusedRowHandle);
                this.View.IsFocusedRowModified = (rowData != null) ? rowData.UpdateRowButtonsWasChanged : false;
            }
        }

        internal virtual void OnCellContentPresenterRowChanged(FrameworkElement presenter)
        {
        }

        internal virtual void OnColumnResizerDoubleClick(BaseColumn column)
        {
        }

        protected internal virtual void OnCurrentCellEditCancelled()
        {
            if (!this.View.AreUpdateRowButtonsShown)
            {
                this.View.IsFocusedRowModified = this.View.DataProviderBase.IsCurrentRowEditing;
            }
            else
            {
                RowData rowData = this.GetRowData(this.FocusedRowHandle);
                this.View.IsFocusedRowModified = (rowData != null) ? rowData.UpdateRowButtonsWasChanged : this.View.DataProviderBase.IsCurrentRowEditing;
            }
        }

        internal virtual void OnDoubleClick(MouseButtonEventArgs e)
        {
            if (!e.Handled)
            {
                this.View.SelectionStrategy.OnDoubleClick(e);
            }
        }

        protected internal virtual void OnEditorActivated()
        {
            this.View.IsEditing = true;
        }

        protected internal virtual void OnFocusedRowCellModified()
        {
            this.View.IsFocusedRowModified = true;
            if (this.View.EnableImmediatePosting)
            {
                Action<InplaceEditorBase> action = <>c.<>9__73_0;
                if (<>c.<>9__73_0 == null)
                {
                    Action<InplaceEditorBase> local1 = <>c.<>9__73_0;
                    action = <>c.<>9__73_0 = x => x.PostEditor(false);
                }
                this.View.CurrentCellEditor.Do<InplaceEditorBase>(action);
                if ((this.View.ValidationError == null) && (this.View.CurrentCellEditor != null))
                {
                    this.View.CurrentCellEditor.Edit.IsValueChanged = false;
                }
                if ((this.View.DataControl != null) && (this.View.DataControl.CurrentColumn != null))
                {
                    this.View.DataControl.CurrentColumn.UpdateIsChecked(false);
                }
                if ((this.View.DataControl.CurrentColumn != null) && (!string.IsNullOrEmpty(this.View.GroupRowCheckBoxFieldNameCore) && (this.View.DataControl.CurrentColumn.FieldName == this.View.GroupRowCheckBoxFieldNameCore)))
                {
                    this.DataControl.UpdateGroupRowChecked(this.View.FocusedRowHandle, this.View.GroupRowCheckBoxFieldNameCore);
                }
            }
        }

        protected internal virtual void OnGotKeyboardFocus()
        {
        }

        protected internal virtual void OnHideEditor(CellEditorBase editor)
        {
            this.View.IsEditing = false;
        }

        internal virtual void OnMouseLeftButtonUp()
        {
        }

        internal virtual void OnResizingComplete()
        {
        }

        protected internal virtual void OnShowEditor(CellEditorBase editor)
        {
        }

        internal void OnTopRowIndexChanged()
        {
            if (this.View.DataPresenter == null)
            {
                this.lockTopRowIndexUpdate = true;
            }
            else if ((this.View.TopRowIndex != ((IScrollInfoOwner) this.View.DataPresenter).Offset) && this.isUpdateScrollInfoNeeded)
            {
                this.lockTopRowIndexUpdate = false;
                this.OnTopRowIndexChangedCore();
            }
        }

        protected internal virtual void OnTopRowIndexChangedCore()
        {
            Func<int> fallback = <>c.<>9__65_1;
            if (<>c.<>9__65_1 == null)
            {
                Func<int> local1 = <>c.<>9__65_1;
                fallback = <>c.<>9__65_1 = () => 0;
            }
            double scrollIndex = (double) this.View.DataProviderBase.Return<DataProviderBase, int>(x => x.ConvertVisibleIndexToScrollIndex(this.View.TopRowIndex, this.View.AllowFixedGroupsCore), fallback);
            this.View.DataPresenter.Do<DataPresenterBase>(x => x.SetDefineScrollOffset(scrollIndex, false));
        }

        internal virtual void OnViewMouseLeave()
        {
        }

        internal virtual void OnViewMouseMove(MouseEventArgs e)
        {
        }

        protected internal abstract bool OnVisibleColumnsAssigned(bool changed);
        internal virtual void ProcessMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            this.View.InplaceEditorOwner.ProcessMouseLeftButtonUp(e);
            this.View.CanSelectLocker.DoLockedAction(() => this.View.SelectionStrategy.OnMouseLeftButtonUp(e));
        }

        internal virtual void ProcessPreviewKeyDown(KeyEventArgs e)
        {
            DataViewBase focusedView = this.View.FocusedView;
            if (focusedView.SelectionStrategy.ShouldInvertSelectionOnPreviewKeyDown(e))
            {
                focusedView.CanSelectLocker.DoLockedAction(() => focusedView.SelectionStrategy.OnInvertSelection());
                e.Handled = true;
            }
            if (((((ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)) && (e.Key == Key.V)) || (ModifierKeysHelper.IsShiftPressed(ModifierKeysHelper.GetKeyboardModifiers(e)) && (e.Key == Key.Insert))) && !ModifierKeysHelper.IsAltPressed(ModifierKeysHelper.GetKeyboardModifiers(e))) && !this.View.IsKeyboardFocusInSearchPanel()) && (e.OriginalSource is DependencyObject))
            {
                Predicate<DependencyObject> predicate = <>c.<>9__96_1;
                if (<>c.<>9__96_1 == null)
                {
                    Predicate<DependencyObject> local1 = <>c.<>9__96_1;
                    predicate = <>c.<>9__96_1 = x => x is InplaceEditorBase;
                }
                if (LayoutHelper.FindLayoutOrVisualParentObject((DependencyObject) e.OriginalSource, predicate, false, null) != null)
                {
                    bool flag = this.DataControl.RaisePastingFromClipboard();
                    flag ??= this.View.RaisePastingFromClipboard();
                    e.Handled = flag;
                }
            }
        }

        internal void RebuildColumnChooserColumns()
        {
            this.RebuildColumnChooserColumnsCore();
        }

        protected virtual void RebuildColumnChooserColumnsCore()
        {
            List<ColumnBase> list = new List<ColumnBase>();
            foreach (ColumnBase base2 in this.View.ColumnsCore)
            {
                if (!base2.Visible && base2.ShowInColumnChooser)
                {
                    list.Add(base2);
                }
            }
            list.Sort(this.View.ColumnChooserColumnsSortOrderComparer);
            if (!ListHelper.AreEqual<ColumnBase>(this.View.ColumnChooserColumns, list))
            {
                this.View.ColumnChooserColumns = new ReadOnlyCollection<ColumnBase>(list);
            }
        }

        internal void RebuildColumns()
        {
            this.RebuildColumnsLayoutHelper.RebuildColumns();
        }

        protected internal virtual void ResetHeadersChildrenCache()
        {
        }

        internal virtual void ResetServiceSummaryCache()
        {
        }

        internal virtual void SetActualShowIndicator(bool showIndicator)
        {
        }

        internal virtual void SetVerticalScrollBarWidth(double width)
        {
        }

        protected internal virtual void StopSelection()
        {
        }

        internal virtual double TreeHeaderIndentsWidth(BaseColumn column) => 
            0.0;

        internal virtual void UpdateActualProperties()
        {
        }

        internal virtual void UpdateAdditionalFocusedRowData()
        {
        }

        protected internal virtual void UpdateAdditionalRowsData()
        {
        }

        protected internal virtual void UpdateBandsLayoutProperties()
        {
            if ((this.DataControl != null) && (this.DataControl.BandsLayoutCore != null))
            {
                this.DataControl.BandsLayoutCore.HeaderImageStyle = this.View.ColumnHeaderImageStyle;
            }
        }

        protected internal virtual void UpdateCellData()
        {
            this.View.HeadersData.UpdateCellData();
            UpdateRowDataDelegate updateMethod = <>c.<>9__61_0;
            if (<>c.<>9__61_0 == null)
            {
                UpdateRowDataDelegate local1 = <>c.<>9__61_0;
                updateMethod = <>c.<>9__61_0 = rowData => rowData.UpdateCellData();
            }
            this.UpdateRowData(updateMethod, true, true);
            Action<ColumnsRowDataBase> action1 = <>c.<>9__61_1;
            if (<>c.<>9__61_1 == null)
            {
                Action<ColumnsRowDataBase> local2 = <>c.<>9__61_1;
                action1 = <>c.<>9__61_1 = rowData => rowData.UpdateCellData();
            }
            this.UpdateServiceRowData(action1);
        }

        protected internal abstract void UpdateCellData(ColumnsRowDataBase rowData);
        internal virtual void UpdateColumnsLayout()
        {
        }

        internal void UpdateColumnsPositions()
        {
            this.RebuildColumnsLayoutHelper.UpdateColumnsPositions(this.View.VisibleColumnsCore);
        }

        protected internal virtual void UpdateColumnsViewInfo(bool updateDataPropertiesOnly)
        {
            this.View.UpdateColumns(column => column.UpdateViewInfo(updateDataPropertiesOnly));
            Action<DataControlBase> action = <>c.<>9__94_1;
            if (<>c.<>9__94_1 == null)
            {
                Action<DataControlBase> local1 = <>c.<>9__94_1;
                action = <>c.<>9__94_1 = x => x.FilteredComponent.RaiseFilterColumnsChanged();
            }
            this.DataControl.Do<DataControlBase>(action);
        }

        internal virtual void UpdateFixedAreaColumnsCount(int fixedLeftColumnsCount, int fixedNoneColumnsCount, int fixedRightColumnsCount)
        {
        }

        protected internal virtual void UpdateFixedNoneContentWidth(ColumnsRowDataBase rowData)
        {
        }

        protected internal virtual void UpdateLastPostition(IndependentMouseEventArgs e)
        {
        }

        internal void UpdateRowButtonsControl()
        {
            if ((this.View.FocusedRowHandle >= 0) || (this.View.FocusedRowHandle == -2147483647))
            {
                RowData rowData = this.View.GetRowData(this.View.FocusedRowHandle);
                if (rowData != null)
                {
                    rowData.UpdateCommitRow();
                    if (this.View.AreUpdateRowButtonsShown)
                    {
                        this.View.LockEditorClose = true;
                        this.View.ScrollIntoViewCore(rowData.RowHandle.Value, true, () => this.view.UpdateRowRectangleHelper.UpdatePosition(this.View));
                        this.View.LockEditorClose = false;
                    }
                }
                this.view.UpdateRowRectangleHelper.UpdatePosition(this.View);
                this.view.EnqueueImmediateAction(() => this.view.UpdateRowRectangleHelper.UpdatePosition(this.View));
            }
        }

        protected internal virtual void UpdateRowData(UpdateRowDataDelegate updateMethod, bool updateInvisibleRows = true, bool updateFocusedRow = true)
        {
            RowDataItemsEnumerator enumerator = new RowDataItemsEnumerator(this.View.RootRowsContainer);
            while (enumerator.MoveNext())
            {
                if (!updateInvisibleRows && !enumerator.Current.GetIsVisible())
                {
                    continue;
                }
                updateMethod(enumerator.Current);
            }
            if (updateFocusedRow)
            {
                RowData focusedRowData = this.View.FocusedRowData;
                if (focusedRowData != null)
                {
                    updateMethod(focusedRowData);
                }
            }
        }

        internal virtual void UpdateSecondaryScrollInfoCore(double secondaryOffset, bool allowUpdateViewportVisibleColumns)
        {
        }

        protected internal virtual void UpdateServiceRowData(Action<ColumnsRowDataBase> updateMethod)
        {
        }

        internal void UpdateTopRowIndex()
        {
            if (this.lockTopRowIndexUpdate)
            {
                this.View.Dispatcher.BeginInvoke(() => this.OnTopRowIndexChanged(), new object[0]);
            }
            else
            {
                this.isUpdateScrollInfoNeeded = false;
                this.View.TopRowIndex = (this.view.DataProviderBase != null) ? this.View.DataProviderBase.ConvertScrollIndexToVisibleIndex(this.View.ScrollInfoOwner.Offset, this.View.AllowFixedGroupsCore) : 0;
                this.isUpdateScrollInfoNeeded = true;
            }
        }

        internal virtual void UpdateViewportVisibleColumns()
        {
        }

        protected internal void UpdateViewRowData(UpdateRowDataDelegate updateMethod)
        {
            this.UpdateRowData(updateMethod, true, false);
        }

        internal virtual bool UseRowDetailsTemplate(int rowHandle) => 
            false;

        protected internal void ValidateIndexes(int startRowHandle, int startColumnIndex, int endRowHandle, int endColumnIndex, out int startIndex, out int colStart, out int endIndex, out int colEnd)
        {
            startIndex = this.DataControl.GetRowVisibleIndexByHandleCore(startRowHandle);
            endIndex = this.DataControl.GetRowVisibleIndexByHandleCore(endRowHandle);
            colStart = startColumnIndex;
            colEnd = endColumnIndex;
            if (colStart > colEnd)
            {
                int num = colEnd;
                colEnd = colStart;
                colStart = num;
            }
            if (startIndex > endIndex)
            {
                int num2 = endIndex;
                endIndex = startIndex;
                startIndex = num2;
            }
        }

        internal virtual void VisibleColumnsChanged()
        {
        }

        protected internal virtual int? VisibleComparisonCore(BaseColumn x, BaseColumn y) => 
            null;

        public DataViewBase View =>
            this.view;

        protected internal DataControlBase DataControl =>
            this.View.DataControl;

        internal virtual bool IsNavigationLocked =>
            false;

        internal virtual bool IsAutoFilterRowFocused =>
            false;

        internal virtual bool CanShowFixedColumnMenu =>
            false;

        internal virtual bool AllowResizingCore =>
            false;

        internal virtual bool UpdateAllowResizingOnWidthChanging =>
            true;

        internal virtual bool AutoWidthCore =>
            false;

        internal virtual bool AllowColumnResizingCore =>
            false;

        internal abstract double HorizontalViewportCore { get; }

        internal virtual bool IsNewItemRowVisible =>
            false;

        protected internal virtual bool IsNewItemRowEditing =>
            false;

        internal virtual bool AutoMoveRowFocusCore =>
            true;

        protected internal virtual Style AutoFilterRowCellStyle =>
            null;

        protected internal virtual DispatcherTimer ScrollTimer =>
            null;

        protected internal virtual bool AllowCascadeUpdate =>
            false;

        protected internal virtual double ScrollAnimationDuration =>
            0.0;

        protected internal virtual bool AllowScrollAnimation =>
            false;

        protected internal virtual DevExpress.Xpf.Grid.ScrollAnimationMode ScrollAnimationMode =>
            DevExpress.Xpf.Grid.ScrollAnimationMode.EaseOut;

        protected internal virtual bool AllowPerPixelScrolling =>
            false;

        protected internal virtual bool KeepViewportOnDataUpdate =>
            false;

        internal virtual HorizontalNavigationStrategyBase NavigationStrategyBase =>
            HorizontalNavigationStrategyBase.NormalHorizontalNavigationStrategyBaseInstance;

        protected RebuildColumnsLayoutHelperBase RebuildColumnsLayoutHelper
        {
            get
            {
                if ((this.View.UseLegacyColumnVisibleIndexes != this.useLegacyColumnVisibleIndexes) || (this.rebuildColumnsLayoutHelper == null))
                {
                    this.useLegacyColumnVisibleIndexes = this.View.UseLegacyColumnVisibleIndexes;
                    this.rebuildColumnsLayoutHelper = this.CreateRebuildColumnsLayoutHelper(this.useLegacyColumnVisibleIndexes);
                }
                return this.rebuildColumnsLayoutHelper;
            }
        }

        private int FocusedRowHandle =>
            this.DataControl.viewCore.FocusedRowHandle;

        internal int CurrentIndex =>
            this.DataControl.DataProviderBase.GetRowVisibleIndexByHandle(this.FocusedRowHandle);

        protected internal virtual double FirstColumnIndent =>
            0.0;

        protected internal virtual double NewItemRowIndent =>
            0.0;

        protected internal virtual double NewItemRowCellIndent =>
            0.0;

        protected internal virtual Brush ActualAlternateRowBackground =>
            null;

        protected internal virtual bool HasFormatConditions =>
            false;

        public virtual Point LastMousePosition { get; protected set; }

        internal virtual bool UseMouseUpFocusedEditorShowModeStrategy =>
            this.View.ShowSelectionRectangle && (this.View.GetActualSelectionMode() == MultiSelectMode.Row);

        internal virtual bool CanShowDetailColumnHeadersControl =>
            this.View.ShowColumnHeaders;

        protected internal virtual bool HasRowTemplateSelector =>
            false;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DataViewBehavior.<>c <>9 = new DataViewBehavior.<>c();
            public static UpdateRowDataDelegate <>9__61_0;
            public static Action<ColumnsRowDataBase> <>9__61_1;
            public static Func<int> <>9__65_1;
            public static Action<InplaceEditorBase> <>9__73_0;
            public static Action<DataControlBase> <>9__94_1;
            public static Predicate<DependencyObject> <>9__96_1;

            internal void <OnFocusedRowCellModified>b__73_0(InplaceEditorBase x)
            {
                x.PostEditor(false);
            }

            internal int <OnTopRowIndexChangedCore>b__65_1() => 
                0;

            internal bool <ProcessPreviewKeyDown>b__96_1(DependencyObject x) => 
                x is InplaceEditorBase;

            internal void <UpdateCellData>b__61_0(RowData rowData)
            {
                rowData.UpdateCellData();
            }

            internal void <UpdateCellData>b__61_1(ColumnsRowDataBase rowData)
            {
                rowData.UpdateCellData();
            }

            internal void <UpdateColumnsViewInfo>b__94_1(DataControlBase x)
            {
                x.FilteredComponent.RaiseFilterColumnsChanged();
            }
        }

        [CompilerGenerated]
        private sealed class <GetServiceUnboundColumns>d__168 : IEnumerable<IColumnInfo>, IEnumerable, IEnumerator<IColumnInfo>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private IColumnInfo <>2__current;
            private int <>l__initialThreadId;

            [DebuggerHidden]
            public <GetServiceUnboundColumns>d__168(int <>1__state)
            {
                this.<>1__state = <>1__state;
                this.<>l__initialThreadId = Environment.CurrentManagedThreadId;
            }

            private bool MoveNext()
            {
                if (this.<>1__state == 0)
                {
                    this.<>1__state = -1;
                }
                return false;
            }

            [DebuggerHidden]
            IEnumerator<IColumnInfo> IEnumerable<IColumnInfo>.GetEnumerator()
            {
                DataViewBehavior.<GetServiceUnboundColumns>d__168 d__;
                if ((this.<>1__state != -2) || (this.<>l__initialThreadId != Environment.CurrentManagedThreadId))
                {
                    d__ = new DataViewBehavior.<GetServiceUnboundColumns>d__168(0);
                }
                else
                {
                    this.<>1__state = 0;
                    d__ = this;
                }
                return d__;
            }

            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator() => 
                this.System.Collections.Generic.IEnumerable<DevExpress.Xpf.GridData.IColumnInfo>.GetEnumerator();

            [DebuggerHidden]
            void IEnumerator.Reset()
            {
                throw new NotSupportedException();
            }

            [DebuggerHidden]
            void IDisposable.Dispose()
            {
            }

            IColumnInfo IEnumerator<IColumnInfo>.Current =>
                this.<>2__current;

            object IEnumerator.Current =>
                this.<>2__current;
        }
    }
}

