namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Data;
    using DevExpress.Mvvm;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Data;
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.ComponentModel;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public abstract class SelectionStrategyBase
    {
        protected DataViewBase view;
        private object oldSelection;
        protected readonly Locker SelectionLocker = new Locker();
        protected readonly Locker SelectedItemChangedLocker = new Locker();
        internal readonly Locker SelectionChangedLocker = new Locker();
        protected internal RectangleMaster oldMasterRectangleCore;
        protected internal SelectionAnchorCell selectionMasterAnchorCore;

        protected SelectionStrategyBase(DataViewBase view)
        {
            this.view = view;
        }

        private void AddToOriginationSelectedItems(object item)
        {
            this.OriginationSelectionStrategy.SelectionLocker.DoLockedActionIfNotLocked(delegate {
                if (this.OriginationSelectedItems != null)
                {
                    this.OriginationSelectedItems.Add(item);
                }
            });
        }

        protected void AddToSelectedItems(object item)
        {
            if (this.IsDetailDataControl)
            {
                this.AddToOriginationSelectedItems(item);
            }
            this.SelectedItems.Add(item);
        }

        internal static bool? AreAllItemsSelected(int selectedRowCount, int rowCount)
        {
            if (selectedRowCount == 0)
            {
                return false;
            }
            return (selectedRowCount == rowCount);
        }

        private void BeginMasterDetailSelection()
        {
            Action<DataControlBase> updateMethod = <>c.<>9__142_0;
            if (<>c.<>9__142_0 == null)
            {
                Action<DataControlBase> local1 = <>c.<>9__142_0;
                updateMethod = <>c.<>9__142_0 = delegate (DataControlBase detail) {
                    if (detail.DataView.GetActualSelectionMode() != MultiSelectMode.MultipleRow)
                    {
                        detail.DataView.SelectionStrategy.BeginUpdateSelectedItems();
                    }
                };
            }
            this.DataControl.UpdateAllOriginationDataControls(updateMethod);
            Action<DataControlBase> updateOpenDetailMethod = <>c.<>9__142_1;
            if (<>c.<>9__142_1 == null)
            {
                Action<DataControlBase> local2 = <>c.<>9__142_1;
                updateOpenDetailMethod = <>c.<>9__142_1 = detail => detail.BeginSelection();
            }
            this.DataControl.UpdateAllDetailDataControls(updateOpenDetailMethod, null);
        }

        public virtual void BeginSelection()
        {
        }

        private void BeginUpdateSelectedItems()
        {
            if (this.SelectedItems is ILockable)
            {
                ((ILockable) this.SelectedItems).BeginUpdate();
            }
            if (this.IsDetailDataControl && (this.OriginationSelectedItems is ILockable))
            {
                ((ILockable) this.OriginationSelectedItems).BeginUpdate();
            }
        }

        protected virtual bool CanSelectCell(int rowHandle) => 
            this.view.CanSelectCellInRow(rowHandle);

        internal virtual bool CanSelectRowRecursively(int groupRowHandle) => 
            false;

        private void ClearCurrentSelection()
        {
            this.oldSelection = null;
        }

        private void ClearDetailDataControlSelection(DataControlBase dataControl)
        {
            if (ReferenceEquals(dataControl.GetOriginationDataControl(), this.DataControl))
            {
                dataControl.UnselectAll();
            }
        }

        private void ClearDetailsSelection()
        {
            this.DataControl.GetRootDataControl().UpdateAllDetailDataControls(new Action<DataControlBase>(this.ClearDetailDataControlSelection), new Action<DataControlBase>(this.ClearDetailDataControlSelection));
        }

        protected void ClearMasterDetailSelection()
        {
            this.DataControl.ClearMasterDetailSelection();
        }

        private void ClearOriginationSelectedItems()
        {
            if (this.OriginationSelectedItems != null)
            {
                this.OriginationSelectionStrategy.SelectionLocker.DoLockedActionIfNotLocked(delegate {
                    foreach (object obj2 in this.SelectedItems)
                    {
                        this.OriginationSelectedItems.Remove(obj2);
                    }
                });
            }
        }

        protected void ClearSelectedItems()
        {
            if (this.IsDetailDataControl)
            {
                this.ClearOriginationSelectedItems();
            }
            Action<IList> action = <>c.<>9__125_0;
            if (<>c.<>9__125_0 == null)
            {
                Action<IList> local1 = <>c.<>9__125_0;
                action = <>c.<>9__125_0 = x => x.Clear();
            }
            this.SelectedItems.Do<IList>(action);
        }

        public virtual void ClearSelection()
        {
        }

        public virtual void CopyMasterDetailToClipboard()
        {
            this.CopyToClipboard();
        }

        public virtual void CopyToClipboard()
        {
        }

        public virtual void CreateMouseSelectionActions(int rowHandle, bool isIndicator)
        {
        }

        private void DoItemsSelection(IList items)
        {
            this.BeginSelection();
            try
            {
                this.ClearSelection();
                if ((items != null) && (items.Count > 0))
                {
                    this.SelectItemsCore(items);
                }
            }
            finally
            {
                this.EndSelection();
            }
        }

        protected internal void DoMasterDetailSelectionAction(Action action)
        {
            this.BeginMasterDetailSelection();
            try
            {
                action();
            }
            finally
            {
                this.EndMasterDetailSelection();
            }
        }

        protected void DoSelectionAction(Action action)
        {
            this.BeginSelection();
            try
            {
                action();
            }
            finally
            {
                this.EndSelection();
            }
        }

        private void EndMasterDetailSelection()
        {
            Action<DataControlBase> updateOpenDetailMethod = <>c.<>9__143_0;
            if (<>c.<>9__143_0 == null)
            {
                Action<DataControlBase> local1 = <>c.<>9__143_0;
                updateOpenDetailMethod = <>c.<>9__143_0 = detail => detail.EndSelection();
            }
            this.DataControl.UpdateAllDetailDataControls(updateOpenDetailMethod, null);
            Action<DataControlBase> updateMethod = <>c.<>9__143_1;
            if (<>c.<>9__143_1 == null)
            {
                Action<DataControlBase> local2 = <>c.<>9__143_1;
                updateMethod = <>c.<>9__143_1 = delegate (DataControlBase detail) {
                    if (detail.DataView.GetActualSelectionMode() != MultiSelectMode.MultipleRow)
                    {
                        SelectionStrategyBase selectionStrategy = detail.DataView.SelectionStrategy;
                        selectionStrategy.SelectionLocker.DoLockedAction(() => selectionStrategy.EndUpdateSelectedItems());
                    }
                };
            }
            this.DataControl.UpdateAllOriginationDataControls(updateMethod);
        }

        public virtual void EndSelection()
        {
        }

        private void EndUpdateSelectedItems()
        {
            if (this.SelectedItems is ILockable)
            {
                ((ILockable) this.SelectedItems).EndUpdate();
            }
            if (this.IsDetailDataControl && (this.OriginationSelectedItems is ILockable))
            {
                ((ILockable) this.OriginationSelectedItems).EndUpdate();
            }
        }

        protected void ForceCreateSelectedItems()
        {
            this.SelectionLocker.DoLockedAction(delegate {
                this.DataControl.SelectedItems = new ObservableCollectionCore<object>();
                this.UpdateSelectedItems();
            });
        }

        public virtual bool? GetAllItemsSelected() => 
            false;

        public virtual bool? GetAllItemsSelected(out bool isSelectedEnabled, out bool isUnselectEnabled)
        {
            isSelectedEnabled = true;
            isUnselectEnabled = true;
            return false;
        }

        public virtual bool? GetAllItemsSelected(int rowHandle, out bool isEnabled, out bool isUnselectEnabled)
        {
            isEnabled = true;
            isUnselectEnabled = true;
            return false;
        }

        public bool? GetAllItemsSelectedMasterDetail() => 
            AreAllItemsSelected(this.GetGlobalSelectedRowCount(), this.GetGlobalVisibleRowCount());

        public SelectionState GetCellSelectionState(int rowHandle, bool isFocused, bool isSelected) => 
            this.CanSelectCell(rowHandle) ? this.GetCellSelectionStateCore(rowHandle, isFocused, isSelected) : SelectionState.None;

        protected virtual SelectionState GetCellSelectionStateCore(int rowHandle, bool isFocused, bool isSelected) => 
            isFocused ? SelectionState.Focused : SelectionState.None;

        internal virtual CellBase[] GetDetailsSelectedCells() => 
            new CellBase[0];

        internal int GetGlobalSelectedRowCount()
        {
            int selectedRowCount = 0;
            this.DataControl.UpdateAllDetailDataControls(delegate (DataControlBase dataControl) {
                selectedRowCount += dataControl.GetSelectedRowHandles().Length;
            }, null);
            return selectedRowCount;
        }

        private int GetGlobalVisibleRowCount()
        {
            int visibleRowCount = 0;
            this.DataControl.UpdateAllDetailDataControls(delegate (DataControlBase dataControl) {
                visibleRowCount += dataControl.VisibleRowCount;
                if (dataControl.DataView.ShouldDisplayBottomRow || dataControl.DataView.ShouldDisplayLoadingRow)
                {
                    int num = visibleRowCount;
                    visibleRowCount = num - 1;
                }
            }, null);
            return visibleRowCount;
        }

        public abstract SelectionState GetRowSelectionState(int rowHandle);
        public virtual CellBase[] GetSelectedCells() => 
            new CellBase[0];

        protected IList<object> GetSelectedItems()
        {
            List<object> list = new List<object>();
            if (this.CanGetSelectedItems)
            {
                foreach (int num2 in this.GetSelectedRows())
                {
                    if (this.ShouldAddToSelectedItems(num2))
                    {
                        object row = this.DataControl.GetRow(num2);
                        if (row != null)
                        {
                            list.Add(row);
                        }
                    }
                }
            }
            return list;
        }

        public virtual int[] GetSelectedRows() => 
            new int[0];

        public virtual IList<Tuple<DataControlBase, int>> GetSelectedRowsInfo() => 
            new Tuple<DataControlBase, int>[0];

        protected virtual object GetSelection() => 
            null;

        public virtual bool IsCellSelected(int rowHandle, ColumnBase column) => 
            false;

        protected internal virtual bool IsCellSelection() => 
            false;

        public virtual bool IsRowSelected(int rowHandle) => 
            false;

        protected bool IsSameSelection(IEnumerable<object> previousSelection, IEnumerable<object> currentSelection) => 
            (previousSelection != null) && previousSelection.SequenceEqual<object>(currentSelection);

        public virtual void OnAfterMouseLeftButtonDown(IDataViewHitInfo hitInfo)
        {
        }

        internal virtual void OnAfterMouseLeftButtonDown(IDataViewHitInfo hitInfo, StylusDevice stylus, int clickCount)
        {
            this.OnAfterMouseLeftButtonDown(hitInfo);
        }

        public virtual void OnAfterProcessKeyDown(KeyEventArgs e)
        {
        }

        public virtual void OnAssignedToGrid()
        {
            if (this.IsSelectionInitialized)
            {
                this.OnAssignedToGridCore();
            }
        }

        protected virtual void OnAssignedToGridCore()
        {
        }

        public virtual void OnBeforeMouseLeftButtonDown(DependencyObject originalSource)
        {
        }

        internal virtual void OnBeforeMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            this.OnBeforeMouseLeftButtonDown(e.OriginalSource as DependencyObject);
        }

        public virtual void OnBeforeProcessKeyDown(KeyEventArgs e)
        {
        }

        protected internal virtual void OnBeforeProcessMouseDown()
        {
        }

        public virtual void OnDataControlInitialized()
        {
            if (!this.DataControl.HasValue(DataControlBase.SelectedItemsProperty))
            {
                this.ForceCreateSelectedItems();
            }
        }

        public virtual void OnDataSourceReset()
        {
            if (this.IsSelectionInitialized)
            {
                this.OnDataSourceResetCore();
            }
        }

        protected virtual void OnDataSourceResetCore()
        {
            this.ClearCurrentSelection();
        }

        public virtual void OnDoubleClick(MouseButtonEventArgs e)
        {
        }

        public virtual void OnFocusedColumnChanged()
        {
        }

        public virtual void OnFocusedRowDataChanged()
        {
        }

        public virtual void OnFocusedRowHandleChanged(int oldRowHandle)
        {
        }

        public virtual void OnInvertSelection()
        {
        }

        public virtual void OnItemChanged(ListChangedEventArgs e)
        {
        }

        public virtual void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
        }

        public virtual void OnNavigationCanceled()
        {
        }

        public virtual void OnNavigationComplete(bool isTabPressed)
        {
        }

        private void OnRemoveItem(int rowHandle)
        {
            object row = this.DataControl.GetRow(rowHandle);
            if (this.SelectedItems.IndexOf(row) < 0)
            {
                this.OnRemoveItemFromItemsSource();
            }
            else if (!this.IsRowSelected(rowHandle))
            {
                this.RemoveFromSelectedItems(row);
            }
        }

        private void OnRemoveItemFromItemsSource()
        {
            object item = this.SelectedItems.Cast<object>().Except<object>(this.GetSelectedItems()).FirstOrDefault<object>();
            if (item != null)
            {
                this.RemoveFromSelectedItems(item);
            }
        }

        public void OnSelectedItemChanged(object oldValue)
        {
            this.ProcessSelectedItemChanged();
            if (this.IsDetailDataControl)
            {
                this.UpdateSelectedItemInOriginationGrid();
            }
            if (!this.IsOriginationDataControl)
            {
                this.DataControl.RaiseSelectedItemChanged(oldValue);
            }
        }

        protected virtual void OnSelectedItemChangedCore()
        {
            this.SelectedItemChangedLocker.DoLockedActionIfNotLocked(delegate {
                if (this.IsServerMode)
                {
                    this.DataControl.SetCurrentItemCore(this.DataControl.SelectedItem);
                }
                else
                {
                    List<object> items = new List<object>();
                    items.Add(this.DataControl.SelectedItem);
                    this.SelectItems(items);
                }
            });
        }

        public virtual void OnSelectedItemsChanged(NotifyCollectionChangedEventArgs e)
        {
        }

        public virtual void OnSelectionChanged(SelectionChangedEventArgs e)
        {
            this.UpdateBorderForFocusedElement();
            if (this.oldSelection != null)
            {
                this.UpdateVisualState(this.oldSelection);
            }
            this.UpdateCachedSelection();
            this.UpdateVisualState(this.oldSelection);
            this.ProcessSelectionChanged(e);
            this.OriginationSelectionStrategy.SelectionChangedLocker.DoLockedActionIfNotLocked(() => this.RaiseSelectionChanged(e));
        }

        protected virtual void ProcessSelectedItemChanged()
        {
            if (!this.IsSelectionLocked)
            {
                if (this.CanSelectSelectedItem)
                {
                    this.UpdateSelectedItemWasLocked = false;
                    this.OnSelectedItemChangedCore();
                }
                else if (this.DataControl.SelectedItem != null)
                {
                    this.UpdateSelectedItemWasLocked = true;
                }
            }
        }

        public virtual void ProcessSelectedItemsChanged()
        {
        }

        private void ProcessSelectionChanged(SelectionChangedEventArgs e)
        {
            if (this.CanUpdateSelectedItems && !this.view.DataProviderBase.IsGroupRowHandle(e.ControllerRow))
            {
                this.SelectionLocker.DoLockedActionIfNotLocked(delegate {
                    CollectionChangeAction action = e.Action;
                    if (action == CollectionChangeAction.Add)
                    {
                        this.AddToSelectedItems(this.DataControl.GetRow(e.ControllerRow));
                    }
                    else if (action != CollectionChangeAction.Remove)
                    {
                        this.UpdateSelectedItemsCore();
                    }
                    else
                    {
                        this.OnRemoveItem(e.ControllerRow);
                    }
                    this.UpdateSelectedItem();
                });
            }
        }

        public void RaiseSelectionChanged(SelectionChangedEventArgs e)
        {
            this.DataControl.RaiseSelectionChanged(e);
            this.view.RaiseSelectionChanged(e);
        }

        private void RemoveFromOriginationSelectedItems(object item)
        {
            this.OriginationSelectionStrategy.SelectionLocker.DoLockedActionIfNotLocked(delegate {
                if (this.OriginationSelectedItems != null)
                {
                    this.OriginationSelectedItems.Remove(item);
                }
            });
        }

        protected void RemoveFromSelectedItems(object item)
        {
            if (this.IsDetailDataControl)
            {
                this.RemoveFromOriginationSelectedItems(item);
            }
            this.SelectedItems.Remove(item);
        }

        private void ReplaceFirstOriginationSelectedItem(object newItem)
        {
            this.OriginationSelectionStrategy.SelectionLocker.DoLockedActionIfNotLocked(delegate {
                if ((this.OriginationSelectedItems != null) && (this.OriginationSelectedItems.Count > 0))
                {
                    this.OriginationSelectedItems[0] = newItem;
                }
            });
        }

        protected void ReplaceFirstSelectedItem(object newItem)
        {
            if (this.IsDetailDataControl)
            {
                this.ReplaceFirstOriginationSelectedItem(newItem);
            }
            this.SelectedItems[0] = newItem;
        }

        protected internal virtual void ResetSelectionMasterAnchorInternal()
        {
            this.selectionMasterAnchorCore = null;
        }

        protected bool RestoreSelectedItem()
        {
            bool flag = this.UpdateSelectedItemWasLocked && this.CanSelectSelectedItem;
            if (flag)
            {
                this.UpdateSelectedItemWasLocked = false;
                this.OnSelectedItemChangedCore();
            }
            return flag;
        }

        public virtual void SelectAll()
        {
        }

        internal virtual void SelectAllMasterDetail()
        {
            this.DoMasterDetailSelectionAction(delegate {
                bool? allItemsSelected = this.GetAllItemsSelectedMasterDetail();
                this.UpdateAllVisibleDetailDataControls(detail => detail.DataView.SelectionStrategy.SelectAllMasterDetail(allItemsSelected));
            });
        }

        public virtual void SelectAllMasterDetail(bool? allItemsSelected)
        {
        }

        public virtual void SelectCell(int rowHandle, ColumnBase column)
        {
        }

        protected internal virtual void SelectCellCore(int rowHandle, ColumnBase column, bool useSelectionCount)
        {
        }

        public void SelectItem(object item)
        {
            if (!this.IsOriginationDataControl)
            {
                this.SelectRow(this.DataControl.DataProviderBase.FindRowByRowValue(item));
            }
            else
            {
                this.SelectItemInOriginationGrid(item);
            }
        }

        private void SelectItemByCommonVisibleIndex(int commonVisibleIndex)
        {
            KeyValuePair<DataViewBase, int> pair = this.DataControl.FindViewAndVisibleIndexByCommonVisibleIndex(commonVisibleIndex);
            DataControlBase dataControl = pair.Key.DataControl;
            int rowHandleByVisibleIndexCore = dataControl.GetRowHandleByVisibleIndexCore(pair.Value);
            if (dataControl.SelectionMode != MultiSelectMode.MultipleRow)
            {
                dataControl.SelectItem(rowHandleByVisibleIndexCore);
            }
        }

        private void SelectItemInOriginationGrid(object item)
        {
            DataControlBase base2 = this.DataControl.DataControlOwner.FindDetailDataControlByRow(item);
            if (base2 != null)
            {
                base2.DataView.SelectionStrategy.SelectItem(item);
            }
        }

        public void SelectItems(IList items)
        {
            if (!this.IsOriginationDataControl)
            {
                this.DoItemsSelection(items);
            }
            else
            {
                this.SelectItemsInOriginationGrid(items);
            }
        }

        protected internal virtual void SelectItemsCore(IList items)
        {
            Dictionary<object, int> dictionary = new Dictionary<object, int>();
            for (int i = 0; i < this.DataControl.DataProviderBase.ListSourceRowCount; i++)
            {
                int rowHandleByListIndex = this.DataControl.DataProviderBase.GetRowHandleByListIndex(i);
                if (this.DataControl.IsValidRowHandleCore(rowHandleByListIndex))
                {
                    object row = this.DataControl.GetRow(rowHandleByListIndex);
                    if (row != null)
                    {
                        dictionary[row] = rowHandleByListIndex;
                    }
                }
            }
            foreach (object obj3 in items)
            {
                int num3;
                if ((obj3 != null) && dictionary.TryGetValue(obj3, out num3))
                {
                    this.SelectRow(num3);
                }
            }
        }

        private void SelectItemsInDetails(IList selectedItems)
        {
            if (selectedItems != null)
            {
                foreach (object obj2 in selectedItems)
                {
                    this.SelectItemInOriginationGrid(obj2);
                }
            }
        }

        private void SelectItemsInOriginationGrid(IList selectedItems)
        {
            this.SelectionChangedLocker.DoLockedAction(delegate {
                Action <>9__1;
                Action action = <>9__1;
                if (<>9__1 == null)
                {
                    Action local1 = <>9__1;
                    action = <>9__1 = delegate {
                        this.ClearDetailsSelection();
                        this.SelectItemsInDetails(selectedItems);
                    };
                }
                this.DoMasterDetailSelectionAction(action);
            });
            this.DataControl.RaiseSelectionChanged(new SelectionChangedEventArgs(CollectionChangeAction.Refresh, -2147483648));
        }

        protected internal void SelectMasterDetailRange(int startCommonVisibleIndex, int endCommonVisibleIndex)
        {
            this.SelectMasterDetailRangeCore(startCommonVisibleIndex, endCommonVisibleIndex);
        }

        internal virtual void SelectMasterDetailRangeCell(int startCommonVisibleIndex, int endCommonVisibleIndex, double startX, double endX, DataControlBase rootDataControl, bool useSelectionCount, bool selectOtherStrategy, bool startInDataArea = false)
        {
        }

        private void SelectMasterDetailRangeCore(int startCommonVisibleIndex, int endCommonVisibleIndex)
        {
            if (startCommonVisibleIndex == endCommonVisibleIndex)
            {
                this.SelectItemByCommonVisibleIndex(startCommonVisibleIndex);
            }
            else if ((startCommonVisibleIndex >= 0) && (endCommonVisibleIndex >= 0))
            {
                int num2 = Math.Max(startCommonVisibleIndex, endCommonVisibleIndex);
                for (int i = Math.Min(startCommonVisibleIndex, endCommonVisibleIndex); i <= num2; i++)
                {
                    this.SelectItemByCommonVisibleIndex(i);
                }
            }
        }

        internal virtual void SelectOnlyThisMasterDetailRange(int startCommonVisibleIndex, int endCommonVisibleIndex)
        {
            this.DoMasterDetailSelectionAction(delegate {
                if (!this.IsControlPressed)
                {
                    this.ClearMasterDetailSelection();
                }
                this.SelectMasterDetailRange(startCommonVisibleIndex, endCommonVisibleIndex);
            });
        }

        internal virtual void SelectOnlyThisMasterDetailRangeRectangle(int startCommonVisibleIndex, int endCommonVisibleIndex)
        {
            this.DoMasterDetailSelectionAction(delegate {
                if (!this.IsControlPressed)
                {
                    this.ClearMasterDetailSelection();
                }
                this.SelectMasterDetailRange(startCommonVisibleIndex, endCommonVisibleIndex);
            });
        }

        public virtual void SelectOnlyThisRange(int startRowHandle, int endRowHandle)
        {
            this.DoSelectionAction(delegate {
                this.ClearMasterDetailSelection();
                this.SelectRange(startRowHandle, endRowHandle);
            });
        }

        public virtual void SelectRange(int startRowHandle, int endRowHandle)
        {
            this.SelectRangeCore(startRowHandle, endRowHandle, rowHandle => this.SelectRow(rowHandle));
        }

        protected internal void SelectRangeCore(int startRowHandle, int endRowHandle, Action<int> selectRowAction)
        {
            if ((startRowHandle != -2147483648) && (endRowHandle != -2147483648))
            {
                if (startRowHandle == endRowHandle)
                {
                    selectRowAction(startRowHandle);
                }
                else
                {
                    int startIndex = this.DataProviderBase.GetRowVisibleIndexByHandle(startRowHandle);
                    int endIndex = this.DataProviderBase.GetRowVisibleIndexByHandle(endRowHandle);
                    if ((startIndex >= 0) && (endIndex >= 0))
                    {
                        if (startIndex > endIndex)
                        {
                            endIndex = startIndex;
                            startIndex = endIndex;
                        }
                        this.DoSelectionAction(delegate {
                            for (int i = startIndex; i <= endIndex; i++)
                            {
                                int rowHandleByVisibleIndexCore = this.DataControl.GetRowHandleByVisibleIndexCore(i);
                                selectRowAction(rowHandleByVisibleIndexCore);
                            }
                        });
                    }
                }
            }
        }

        public virtual void SelectRow(int rowHandle)
        {
        }

        public virtual void SelectRowForce()
        {
        }

        public virtual void SelectRowRange(IEnumerable<int> selectedRowsHandles)
        {
        }

        public void SelectRowRange(int startRowVisibleIndex, int endRowVisibleIndex)
        {
            int num = Math.Min(startRowVisibleIndex, endRowVisibleIndex);
            int num2 = Math.Max(startRowVisibleIndex, endRowVisibleIndex);
            List<int> selectedRowsHandles = new List<int>(num2 - num);
            for (int i = num; i <= num2; i++)
            {
                selectedRowsHandles.Add(this.view.DataControl.GetRowHandleByVisibleIndexCore(i));
            }
            this.SelectRowRange(selectedRowsHandles);
        }

        internal virtual void SelectRowRecursively(int groupRowHandle)
        {
        }

        public virtual void SetCellsSelection(int startRowHandle, ColumnBase startColumn, int endRowHandle, ColumnBase endColumn, bool setSelected)
        {
        }

        private void SetSelectedItem(object selectedItem)
        {
            SetSelectedItem(this.DataControl, selectedItem);
        }

        private static void SetSelectedItem(DataControlBase dataControl, object selectedItem)
        {
            dataControl.SetCurrentValue(DataControlBase.SelectedItemProperty, selectedItem);
        }

        protected virtual bool ShouldAddToSelectedItems(int rowHandle) => 
            rowHandle >= 0;

        public virtual bool ShouldInvertSelectionOnPreviewKeyDown(KeyEventArgs e) => 
            false;

        internal virtual void StartMouseSelection()
        {
        }

        public virtual void ToggleRowsSelection()
        {
        }

        public virtual void UnselectCell(int rowHandle, ColumnBase column)
        {
        }

        protected internal virtual void UnselectCellCore(int rowHandle, ColumnBase column, bool useSelectionCount)
        {
        }

        public virtual void UnselectCellsByColumn(ColumnBase column)
        {
        }

        public void UnselectItem(object item)
        {
            if (!this.IsOriginationDataControl)
            {
                this.UnselectRow(this.DataControl.DataProviderBase.FindRowByRowValue(item));
            }
            else
            {
                this.UnselectItemInOriginationGrid(item);
            }
        }

        private void UnselectItemInOriginationGrid(object item)
        {
            DataControlBase base2 = this.DataControl.DataControlOwner.FindDetailDataControlByRow(item);
            if (base2 != null)
            {
                base2.DataView.SelectionStrategy.UnselectItem(item);
            }
        }

        public virtual void UnselectRow(int rowHandle)
        {
        }

        internal virtual void UnselectRowRecursively(int groupRowHandle)
        {
        }

        private void UpdateAllVisibleDetailDataControls(Action<DataControlBase> action)
        {
            this.DataControl.UpdateAllDetailDataControls(delegate (DataControlBase detail) {
                List<KeyValuePair<DataViewBase, int>> chain = new List<KeyValuePair<DataViewBase, int>>();
                detail.DataControlParent.CollectViewVisibleIndexChain(chain);
                Func<KeyValuePair<DataViewBase, int>, bool> predicate = <>c.<>9__140_1;
                if (<>c.<>9__140_1 == null)
                {
                    Func<KeyValuePair<DataViewBase, int>, bool> local1 = <>c.<>9__140_1;
                    predicate = <>c.<>9__140_1 = index => index.Value < 0;
                }
                if (chain.Where<KeyValuePair<DataViewBase, int>>(predicate).Count<KeyValuePair<DataViewBase, int>>() == 0)
                {
                    action(detail);
                }
            }, null);
        }

        public virtual void UpdateBorderForFocusedElement()
        {
            if (!this.view.CanUpdateBorderForFocusedElement() || !this.UpdateBorderForFocusedElementCore())
            {
                this.view.ClearFocusedRectangle();
            }
        }

        public virtual bool UpdateBorderForFocusedElementCore() => 
            false;

        public void UpdateCachedSelection()
        {
            this.oldSelection = this.GetSelection();
        }

        protected internal virtual void UpdateSelectedItem()
        {
            if (!this.UpdateSelectedItemWasLocked && !this.view.IsDesignTime)
            {
                object selectedItem = this.DataControl.HasSelectedItems() ? this.SelectedItems[0] : null;
                if ((selectedItem == null) && this.IsServerMode)
                {
                    selectedItem = this.DataControl.CurrentItem;
                }
                if (this.DataControl.SelectedItem != selectedItem)
                {
                    this.SetSelectedItem(selectedItem);
                }
                DataControlBase originationDataControl = this.DataControl.GetOriginationDataControl();
                if (!ReferenceEquals(originationDataControl, this.DataControl) && ((originationDataControl.SelectedItem == null) && (selectedItem != null)))
                {
                    SetSelectedItem(originationDataControl, selectedItem);
                }
            }
        }

        private void UpdateSelectedItemInOriginationGrid()
        {
            this.OriginationSelectionStrategy.SelectedItemChangedLocker.DoLockedActionIfNotLocked(delegate {
                if (!this.view.IsDesignTime)
                {
                    object selectedItem = this.OriginationDataControl.HasSelectedItems() ? this.OriginationSelectedItems[0] : null;
                    if (this.OriginationDataControl.SelectedItem != selectedItem)
                    {
                        SetSelectedItem(this.OriginationDataControl, selectedItem);
                    }
                }
            });
        }

        public void UpdateSelectedItems()
        {
            if (this.CanUpdateSelectedItems)
            {
                this.UpdateSelectedItemsCore();
            }
        }

        protected void UpdateSelectedItemsCore()
        {
            IList<object> selectedItems = this.GetSelectedItems();
            if (!this.IsSameSelection(this.SelectedItems.Cast<object>(), selectedItems))
            {
                this.BeginUpdateSelectedItems();
                try
                {
                    this.ClearSelectedItems();
                    foreach (object obj2 in selectedItems)
                    {
                        this.AddToSelectedItems(obj2);
                    }
                }
                finally
                {
                    this.OriginationSelectionStrategy.SelectionLocker.DoLockedAction(() => this.EndUpdateSelectedItems());
                }
                this.UpdateSelectedItem();
            }
        }

        public virtual void UpdateSelectionRect(int rowHandle, ColumnBase column)
        {
        }

        protected virtual void UpdateVisualState(object oldSelection)
        {
        }

        protected bool IsSelectionInitialized =>
            (this.DataControl != null) && this.DataControl.IsSelectionInitialized;

        protected virtual bool IsServerMode =>
            this.DataControl.IsServerMode;

        protected bool CanGetSelectedItems =>
            !this.view.IsDesignTime && (this.IsSelectionInitialized && !this.IsServerMode);

        protected bool CanUpdateSelectedItems =>
            this.CanGetSelectedItems && (this.SelectedItems != null);

        protected bool CanSelectSelectedItem =>
            this.IsSelectionInitialized && ((this.DataControl.ItemsSource != null) || this.IsOriginationDataControl);

        internal bool IsSelectionLocked =>
            this.SelectionLocker.IsLocked;

        internal virtual bool IsFocusedRowHandleLocked =>
            false;

        protected bool UpdateSelectedItemWasLocked { get; private set; }

        protected DataControlBase DataControl =>
            this.view.DataControl;

        protected DevExpress.Xpf.Data.DataProviderBase DataProviderBase =>
            this.DataControl.DataProviderBase;

        protected IList SelectedItems =>
            this.DataControl.SelectedItems;

        protected bool IsShiftPressed =>
            ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers);

        protected bool IsControlPressed =>
            ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers);

        private DataControlBase OriginationDataControl =>
            this.DataControl.GetOriginationDataControl();

        private IList OriginationSelectedItems =>
            this.OriginationDataControl.SelectedItems;

        private bool IsDetailDataControl =>
            !this.DataControl.IsOriginationDataControl();

        private bool IsOriginationDataControl =>
            this.DataControl.IsOriginationDataControlCore();

        private SelectionStrategyBase OriginationSelectionStrategy =>
            this.OriginationDataControl.DataView.SelectionStrategy;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectionStrategyBase.<>c <>9 = new SelectionStrategyBase.<>c();
            public static Action<IList> <>9__125_0;
            public static Func<KeyValuePair<DataViewBase, int>, bool> <>9__140_1;
            public static Action<DataControlBase> <>9__142_0;
            public static Action<DataControlBase> <>9__142_1;
            public static Action<DataControlBase> <>9__143_0;
            public static Action<DataControlBase> <>9__143_1;

            internal void <BeginMasterDetailSelection>b__142_0(DataControlBase detail)
            {
                if (detail.DataView.GetActualSelectionMode() != MultiSelectMode.MultipleRow)
                {
                    detail.DataView.SelectionStrategy.BeginUpdateSelectedItems();
                }
            }

            internal void <BeginMasterDetailSelection>b__142_1(DataControlBase detail)
            {
                detail.BeginSelection();
            }

            internal void <ClearSelectedItems>b__125_0(IList x)
            {
                x.Clear();
            }

            internal void <EndMasterDetailSelection>b__143_0(DataControlBase detail)
            {
                detail.EndSelection();
            }

            internal void <EndMasterDetailSelection>b__143_1(DataControlBase detail)
            {
                if (detail.DataView.GetActualSelectionMode() != MultiSelectMode.MultipleRow)
                {
                    SelectionStrategyBase selectionStrategy = detail.DataView.SelectionStrategy;
                    selectionStrategy.SelectionLocker.DoLockedAction(() => selectionStrategy.EndUpdateSelectedItems());
                }
            }

            internal bool <UpdateAllVisibleDetailDataControls>b__140_1(KeyValuePair<DataViewBase, int> index) => 
                index.Value < 0;
        }

        private protected class RectangleMaster
        {
            public int StartVisibleIndex { get; set; }

            public int EndVisibleIndex { get; set; }

            public double StartX { get; set; }

            public double EndX { get; set; }
        }
    }
}

