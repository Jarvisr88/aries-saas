namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public abstract class GridViewRowNavigationBase : GridViewNavigationBase
    {
        protected GridViewRowNavigationBase(DataViewBase view) : base(view)
        {
        }

        protected void ClearAllCellsState(DependencyObject row)
        {
            GridCellsEnumerator enumerator = new GridCellsEnumerator((FrameworkElement) row);
            while (enumerator.MoveNext())
            {
                DataViewBase.SetIsFocusedCell(enumerator.Current, false);
            }
        }

        protected void ClearAllCellsStates()
        {
            GridRowsEnumerator enumerator = new GridRowsEnumerator(base.View, base.View.RootNodeContainer);
            while (enumerator.MoveNext())
            {
                this.ClearAllCellsState(enumerator.CurrentRow);
                this.SetRowFocus(enumerator.CurrentRow, false);
            }
            base.View.CurrentCell = null;
        }

        protected internal override void ClearAllStates()
        {
            if ((base.View != null) && (base.View.DataControl != null))
            {
                GridRowsEnumerator enumerator = new GridRowsEnumerator(base.View, base.View.RootNodeContainer);
                while (enumerator.MoveNext())
                {
                    this.SetRowFocus(enumerator.CurrentRow, false);
                }
            }
        }

        private bool IsRowAndColumnChanged(int rowNavigationIndex, int cellNavigationIndex) => 
            (base.View.FocusedRowHandle != rowNavigationIndex) && (cellNavigationIndex != base.View.NavigationIndex);

        public override void OnDown()
        {
            if (base.View.AreUpdateRowButtonsShown)
            {
                RowData rowData = base.View.GetRowData(base.View.FocusedRowHandle);
                if ((rowData != null) && !rowData.UpdateButtonIsFocused())
                {
                    if (base.View.IsEditing)
                    {
                        base.View.CloseEditor();
                    }
                    rowData.UpdateButtonTabPress(false);
                    return;
                }
            }
            base.View.MoveNextRow();
        }

        public override void OnEnd(KeyEventArgs e)
        {
            base.View.MoveLastOrLastMasterRow();
        }

        public override void OnHome(KeyEventArgs e)
        {
            base.View.MoveFirstOrFirstMasterRow();
        }

        public override void OnLeft(bool isCtrlPressed)
        {
            if (!base.View.IsAdditionalRowFocused && (base.View.FocusedRowElement != null))
            {
                if (base.View.IsExpandableRowFocused())
                {
                    if (base.View.IsExpanded(base.View.FocusedRowHandle))
                    {
                        base.View.CollapseFocusedRowCore();
                    }
                    else
                    {
                        base.View.MoveParentRow();
                    }
                }
                else
                {
                    IScrollInfo rootDataPresenter = base.View.RootDataPresenter;
                    if (rootDataPresenter.ExtentWidth <= rootDataPresenter.ViewportWidth)
                    {
                        base.View.MoveParentRow();
                    }
                    else
                    {
                        rootDataPresenter.LineLeft();
                    }
                }
            }
        }

        public override bool OnMinus(bool isCtrlPressed) => 
            !base.View.IsAutoFilterRowFocused ? ((!isCtrlPressed || !base.View.DataControl.MasterDetailProvider.SetMasterRowExpanded(base.View.FocusedRowHandle, false, null)) ? base.View.CollapseFocusedRowCore() : true) : false;

        public override void OnPageDown()
        {
            base.View.MoveNextPage();
        }

        public override void OnPageUp()
        {
            base.View.MovePrevPage();
        }

        public override bool OnPlus(bool isCtrlPressed) => 
            !base.View.IsAutoFilterRowFocused ? ((!isCtrlPressed || !base.View.DataControl.MasterDetailProvider.SetMasterRowExpanded(base.View.FocusedRowHandle, true, null)) ? base.View.ExpandFocusedRowCore() : true) : false;

        public override void OnRight(bool isCtrlPressed)
        {
            if (!base.View.IsAdditionalRowFocused && (base.View.FocusedRowElement != null))
            {
                if (!base.View.IsExpandableRowFocused())
                {
                    ((IScrollInfo) base.View.RootDataPresenter).LineRight();
                }
                else if (!base.View.IsExpanded(base.View.FocusedRowHandle))
                {
                    base.View.ExpandFocusedRowCore();
                }
                else
                {
                    base.View.MoveNextRow();
                }
            }
        }

        public override void OnUp(bool isCtrlPressed)
        {
            if (base.View.AreUpdateRowButtonsShown)
            {
                RowData rowData = base.View.GetRowData(base.View.FocusedRowHandle);
                if ((rowData != null) && rowData.UpdateButtonIsFocused())
                {
                    if (base.View.CurrentCellEditor != null)
                    {
                        base.View.CurrentCellEditor.Focus();
                        return;
                    }
                    base.View.MoveLastCell();
                    return;
                }
            }
            base.View.MovePrevRow();
        }

        protected internal override void ProcessMouse(DependencyObject originalSource)
        {
            int handle = base.View.FindRowHandle(originalSource);
            if (handle != -2147483648)
            {
                base.View.CanSelectLocker.DoLockedAction(delegate {
                    Action <>9__1;
                    Action action = <>9__1;
                    if (<>9__1 == null)
                    {
                        Action local1 = <>9__1;
                        action = <>9__1 = () => this.View.FocusViewAndRow(this.View, handle);
                    }
                    this.NavigationMouseLocker.DoLockedAction(action);
                });
            }
            else
            {
                IDataViewHitInfo info = base.View.RootView.CalcHitInfoCore(originalSource);
                if ((info != null) && info.IsDataArea)
                {
                    base.View.CommitEditing();
                }
            }
        }

        protected void ProcessMouseOnCell(DependencyObject cell)
        {
            if (cell != null)
            {
                if (cell != null)
                {
                    int navigationIndex = ColumnBase.GetNavigationIndex(cell);
                    if (base.View.InplaceEditorOwner.IsActiveEditorHaveValidationError())
                    {
                        DependencyObject element = DataViewBase.FindParentRow(cell);
                        if (this.IsRowAndColumnChanged(DataViewBase.GetRowHandle(element).Value, navigationIndex))
                        {
                            base.View.RowsStateDirty = true;
                            return;
                        }
                    }
                    if (!(((base.View.VisibleColumnsCore == null) || ((base.View.VisibleColumnsCore.Count <= navigationIndex) || !base.View.VisibleColumnsCore[navigationIndex].AllowFocus)) ? (base.View.FocusedRowHandle == -2147483645) : true))
                    {
                        if ((base.DataControl.CurrentColumn != null) && !base.DataControl.CurrentColumn.AllowFocus)
                        {
                            base.DataControl.ReInitializeCurrentColumn();
                        }
                        return;
                    }
                    base.View.NavigationIndex = navigationIndex;
                }
                base.View.RowsStateDirty = true;
            }
        }

        protected virtual void SetRowFocus(DependencyObject row, bool focus)
        {
            DataViewBase.SetIsFocusedRow(row, focus);
            if (focus)
            {
                base.View.ProcessFocusedElement();
            }
        }

        protected void SetRowFocusOnCell(DependencyObject row, bool focus)
        {
            if ((DataViewBase.GetIsFocusedRow(row) != focus) || (ReferenceEquals(row, base.View.FocusedRowElement) || focus))
            {
                this.UpdateCellsStateCore(row, focus);
            }
        }

        protected void TabNavigation(bool isShiftPressed)
        {
            if (!isShiftPressed)
            {
                base.View.MoveNextCell(true, false);
            }
            else
            {
                base.View.SelectRowForce();
                base.View.MovePrevCell(true);
            }
        }

        protected void UpdateCellsStateCore(DependencyObject row, bool focusedRow)
        {
            GridCellsEnumerator enumerator = new GridCellsEnumerator((FrameworkElement) row);
            while (enumerator.MoveNext())
            {
                bool focused = (focusedRow && ((enumerator.CurrentNavigationIndex == enumerator.RowCurrentView.NavigationIndex) && (!(enumerator.Current is IRowMarginControl) && (enumerator.Current is UIElement)))) && UIElementHelper.IsVisibleInTree((UIElement) enumerator.Current);
                DataViewBase.SetIsFocusedCell(enumerator.Current, focused);
                if (focused)
                {
                    enumerator.RowCurrentView.CurrentCell = enumerator.Current;
                }
            }
        }

        protected internal sealed override void UpdateRowsState()
        {
            if ((base.View != null) && (base.View.DataControl != null))
            {
                this.UpdateRowsStateCore();
            }
        }

        protected virtual void UpdateRowsStateCore()
        {
            GridRowsEnumerator enumerator = base.View.CreateVisibleRowsEnumerator();
            while (enumerator.MoveNext())
            {
                FrameworkElement currentRow = enumerator.CurrentRow;
                if (currentRow != null)
                {
                    this.SetRowFocus(currentRow, enumerator.CurrentRowData.IsFocusedRow());
                }
            }
        }

        public override bool ShouldRaiseRowAutomationEvents =>
            true;
    }
}

