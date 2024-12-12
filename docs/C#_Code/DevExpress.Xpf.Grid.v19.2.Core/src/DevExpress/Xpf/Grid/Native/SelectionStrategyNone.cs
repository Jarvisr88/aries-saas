namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;

    internal class SelectionStrategyNone : SelectionStrategyBase
    {
        public SelectionStrategyNone(DataViewBase view) : base(view)
        {
        }

        public override void CopyToClipboard()
        {
            if (!base.view.IsInvalidFocusedRowHandle)
            {
                base.DataControl.CopyCurrentItemToClipboard();
            }
        }

        public override SelectionState GetRowSelectionState(int rowHandle) => 
            base.view.IsFocusedView ? ((rowHandle == base.view.FocusedRowHandle) ? SelectionState.Focused : SelectionState.None) : SelectionState.None;

        public override int[] GetSelectedRows()
        {
            if (base.view.IsInvalidFocusedRowHandle || !base.view.IsFocusedView)
            {
                return new int[0];
            }
            return new int[] { base.view.FocusedRowHandle };
        }

        public override bool IsRowSelected(int rowHandle) => 
            base.view.IsFocusedView ? (rowHandle == base.view.FocusedRowHandle) : false;

        protected override void OnAssignedToGridCore()
        {
            base.OnAssignedToGridCore();
            base.UpdateSelectedItems();
        }

        public override void OnBeforeMouseLeftButtonDown(DependencyObject originalSource)
        {
            base.OnBeforeMouseLeftButtonDown(originalSource);
            if (!base.IsControlPressed && !base.IsShiftPressed)
            {
                bool clear = true;
                if ((base.view != null) && ((base.view.RootView != null) && ((base.view.RootView.DataControl != null) && ((base.view.RootView is ITableView) && ((ITableView) base.view.RootView).HasDetailViews))))
                {
                    IDataViewHitInfo info = base.view.RootView.CalcHitInfoCore(originalSource);
                    if (!((info.InRow || info.IsDataArea) || info.IsRowCell))
                    {
                        base.view.RootView.DataControl.UpdateAllDetailDataControls(delegate (DataControlBase dataControl) {
                            if ((dataControl != null) && (dataControl.DataView != null))
                            {
                                clear = clear ? (dataControl.DataView.SelectionStrategy is SelectionStrategyNone) : false;
                            }
                        }, null);
                    }
                }
                if (clear)
                {
                    base.ClearMasterDetailSelection();
                }
            }
        }

        public override void OnDataControlInitialized()
        {
            if (base.DataControl.HasValue(DataControlBase.SelectedItemProperty))
            {
                this.ProcessSelectedItemChanged();
            }
            else
            {
                this.OnDataSourceReset();
            }
            base.OnDataControlInitialized();
        }

        protected override void OnDataSourceResetCore()
        {
            base.OnDataSourceResetCore();
            if (!base.RestoreSelectedItem())
            {
                base.UpdateSelectedItems();
            }
        }

        public override void OnFocusedRowDataChanged()
        {
            this.SetFocusedRowSelected();
        }

        protected override void OnSelectedItemChangedCore()
        {
            if (!base.IsSelectionLocked && (base.DataControl.CurrentItem != base.DataControl.SelectedItem))
            {
                base.SelectedItemChangedLocker.DoLockedActionIfNotLocked(() => base.DataControl.SetCurrentItemCore(base.DataControl.SelectedItem));
            }
        }

        public override void ProcessSelectedItemsChanged()
        {
            base.UpdateSelectedItems();
        }

        public void SetFocusedRowSelected()
        {
            if (base.DataControl.AllowUpdateCurrentItem())
            {
                base.SelectionLocker.DoLockedAction(delegate {
                    if (!base.CanUpdateSelectedItems)
                    {
                        if (this.IsServerMode)
                        {
                            this.UpdateSelectedItem();
                        }
                    }
                    else
                    {
                        IList<object> selectedItems = base.GetSelectedItems();
                        if (!base.IsSameSelection(base.SelectedItems.Cast<object>(), selectedItems))
                        {
                            this.SetFocusedRowSelected(selectedItems);
                        }
                        this.UpdateSelectedItem();
                    }
                });
            }
        }

        private void SetFocusedRowSelected(IList<object> selectedItemsCore)
        {
            if (selectedItemsCore.Count <= 0)
            {
                base.ClearSelectedItems();
            }
            else if (base.SelectedItems.Count == 0)
            {
                base.AddToSelectedItems(selectedItemsCore[0]);
            }
            else if (base.SelectedItems.Count == 1)
            {
                base.ReplaceFirstSelectedItem(selectedItemsCore[0]);
            }
            else
            {
                base.UpdateSelectedItemsCore();
            }
        }

        protected override bool ShouldAddToSelectedItems(int rowHandle) => 
            base.ShouldAddToSelectedItems(rowHandle) || (base.view.IsNewItemRowHandle(rowHandle) && base.view.ViewBehavior.IsNewItemRowEditing);

        public override bool UpdateBorderForFocusedElementCore()
        {
            if (!this.ShowFocusedRectangle)
            {
                return false;
            }
            if (base.DataControl.IsGroupRowHandleCore(base.view.FocusedRowHandle))
            {
                base.view.SetFocusedRectangleOnGroupRow();
                return true;
            }
            if (base.view.IsAdditionalRowFocused)
            {
                base.view.SetFocusedRectangleOnCell();
                return true;
            }
            if (base.view.NavigationStyle == GridViewNavigationStyle.Cell)
            {
                base.view.SetFocusedRectangleOnCell();
            }
            else
            {
                base.view.SetFocusedRectangleOnRow();
            }
            return true;
        }

        protected internal override void UpdateSelectedItem()
        {
            if (base.DataControl.AllowUpdateCurrentItem())
            {
                base.UpdateSelectedItem();
            }
        }

        internal override bool IsFocusedRowHandleLocked =>
            base.UpdateSelectedItemWasLocked;

        protected override bool IsServerMode =>
            base.DataControl.DataProviderBase.IsAsyncServerMode;

        protected virtual bool ShowFocusedRectangle =>
            base.view.ShowFocusedRectangle;
    }
}

