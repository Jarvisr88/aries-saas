namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows.Input;

    public abstract class SelectionStrategyRowBase : MultiSelectionStrategyBase
    {
        private Locker DoubleClickLocker;

        public SelectionStrategyRowBase(DataViewBase view) : base(view)
        {
            this.DoubleClickLocker = new Locker();
        }

        protected virtual bool CanInvertSelection(IDataViewHitInfo hitInfo) => 
            ((base.IsControlPressed && !base.IsShiftPressed) || this.IsMultipleMode) && (!hitInfo.IsRowCell || ((hitInfo.RowHandle != base.view.FocusedRowHandle) || (!ReferenceEquals(hitInfo.Column, base.view.DataControl.CurrentColumn) || (base.view.ActiveEditor == null))));

        public override bool? GetAllItemsSelected() => 
            AreAllItemsSelected(this.SelectedRowCount, this.TotalRowCount);

        public override bool? GetAllItemsSelected(out bool isSelectedEnabled, out bool isUnselectEnabled)
        {
            isSelectedEnabled = true;
            isUnselectEnabled = true;
            return AreAllItemsSelected(this.SelectedRowCount, this.TotalRowCount);
        }

        protected override object GetSelection() => 
            this.GetSelectedRows();

        internal override void OnAfterMouseLeftButtonDown(IDataViewHitInfo hitInfo, StylusDevice stylus, int clickCount)
        {
            if (!this.ShouldOpenEditor(stylus, clickCount))
            {
                base.OnAfterMouseLeftButtonDown(hitInfo, stylus, clickCount);
            }
        }

        protected override void OnDataSourceResetCore()
        {
            base.OnDataSourceResetCore();
            if ((base.view.SearchPanelColumnProvider != null) && base.view.SearchPanelColumnProvider.IsSearchLookUpMode)
            {
                this.OnDataSourceResetInLookUpMode();
            }
            else if (!base.RestoreSelectedItem())
            {
                if (base.DataControl.AllowUpdateSelectedItems() && base.view.IsRootView)
                {
                    this.SetFocusedRowSelected();
                }
                else
                {
                    this.ProcessSelectedItemsChanged();
                }
            }
        }

        protected virtual void OnDataSourceResetInLookUpMode()
        {
            this.ProcessSelectedItemsChanged();
            base.UpdateSelectedItems();
        }

        public override void OnDoubleClick(MouseButtonEventArgs e)
        {
            if (this.IsMultipleMode && (e.LeftButton == MouseButtonState.Pressed))
            {
                base.view.EditorSetInactiveAfterClick = false;
                this.DoubleClickLocker.DoLockedAction(() => this.DataControl.FindTargetView(e.OriginalSource).InplaceEditorOwner.ProcessMouseLeftButtonDown(e));
            }
        }

        public override void SelectAll()
        {
            this.SelectAllCore();
        }

        protected virtual void SelectAllCore()
        {
            base.view.DataProviderBase.Selection.SelectAll();
        }

        public override void SelectAllMasterDetail(bool? allItemsSelected)
        {
            if (this.IsMultipleMode)
            {
                this.ToggleRowsSelection(allItemsSelected, true, true);
            }
            else
            {
                this.SelectAllCore();
            }
        }

        protected virtual void SelectAllRows()
        {
            base.DoSelectionAction(() => this.SelectDataRows());
        }

        protected void SelectDataRows()
        {
            for (int i = 0; i < this.DataRowCount; i++)
            {
                this.SelectRowCore(i);
            }
        }

        protected internal virtual void SelectRowCore(int rowHandle)
        {
            if (!base.view.CanSelectLocker.IsLocked || base.view.RaiseCanSelectRow(rowHandle))
            {
                base.view.DataProviderBase.Selection.SetSelected(rowHandle, true);
            }
        }

        public abstract void SetFocusedRowSelected();
        protected override bool ShouldInvertSelectionOnSpace() => 
            (base.view.RootView.NavigationStyle == GridViewNavigationStyle.Row) ? (this.IsMultipleMode || !this.IsRowSelected(base.view.FocusedRowHandle)) : false;

        private bool ShouldOpenEditor(StylusDevice stylus, int clickCount)
        {
            if (!this.IsMultipleMode)
            {
                return false;
            }
            if ((clickCount == 2) || this.DoubleClickLocker.IsLocked)
            {
                return true;
            }
            if (stylus == null)
            {
                return false;
            }
            Func<InplaceEditorBase, bool> evaluator = <>c.<>9__31_0;
            if (<>c.<>9__31_0 == null)
            {
                Func<InplaceEditorBase, bool> local1 = <>c.<>9__31_0;
                evaluator = <>c.<>9__31_0 = edit => edit.IsEditorVisible;
            }
            return base.view.CurrentCellEditor.Return<InplaceEditorBase, bool>(evaluator, (<>c.<>9__31_1 ??= () => false));
        }

        public override void ToggleRowsSelection()
        {
            bool enableSelected = true;
            bool enableUnselected = true;
            this.ToggleRowsSelection(this.GetAllItemsSelected(out enableSelected, out enableUnselected), enableSelected, enableUnselected);
        }

        public void ToggleRowsSelection(bool? allItemsSelected, bool enableSelected = true, bool enableUnselected = true)
        {
            if ((allItemsSelected != null) && allItemsSelected.Value)
            {
                this.ClearSelection();
            }
            else if (enableSelected)
            {
                this.SelectAll();
            }
            else
            {
                this.ClearSelection();
            }
        }

        protected internal virtual void UnselectRowCore(int rowHandle)
        {
            if (!base.view.CanSelectLocker.IsLocked || base.view.RaiseCanUnselectRow(rowHandle))
            {
                base.view.DataProviderBase.Selection.SetSelected(rowHandle, false);
            }
        }

        public override void UpdateSelectionRect(int rowHandle, ColumnBase column)
        {
            if ((base.view.SelectionAnchor != null) && (base.view.SelectionOldCell != null))
            {
                int startCommonVisibleIndex = base.view.SelectionAnchor.IsLastRow ? (base.view.SelectionAnchor.RowVisibleIndex + 1) : base.view.SelectionAnchor.RowVisibleIndex;
                this.SelectMasterDetailRangeCell(startCommonVisibleIndex, base.view.SelectionOldCell.RowVisibleIndex, base.view.SelectionAnchor.CoordinateX + base.view.SelectionAnchor.OffsetX, base.view.SelectionOldCell.CoordinateX + base.view.SelectionOldCell.OffsetX, base.view.RootView.DataControl, true, true, base.view.SelectionAnchor.IsLastRow);
            }
        }

        protected override void UpdateVisualState(object oldSelection)
        {
            foreach (int rowHandle in (int[]) oldSelection)
            {
                base.view.UpdateRowDataByRowHandle(rowHandle, rowData => rowData.UpdateIsSelected(this.view.IsRowSelected(rowHandle)));
            }
        }

        protected bool HasValidationError =>
            base.view.HasValidationError && base.view.IsEditing;

        public bool IsMultipleMode =>
            base.view.GetActualSelectionMode() == MultiSelectMode.MultipleRow;

        public bool IsExtendedMode =>
            !this.IsMultipleMode;

        protected int DataRowCount =>
            base.DataControl.DataProviderBase.DataRowCount;

        protected virtual int TotalRowCount
        {
            get
            {
                int visibleRowCount = base.DataControl.VisibleRowCount;
                if (base.DataControl.DataView.ShouldDisplayBottomRow || base.DataControl.DataView.ShouldDisplayLoadingRow)
                {
                    visibleRowCount--;
                }
                return visibleRowCount;
            }
        }

        protected int SelectedRowCount =>
            base.DataControl.DataProviderBase.Selection.Count;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SelectionStrategyRowBase.<>c <>9 = new SelectionStrategyRowBase.<>c();
            public static Func<InplaceEditorBase, bool> <>9__31_0;
            public static Func<bool> <>9__31_1;

            internal bool <ShouldOpenEditor>b__31_0(InplaceEditorBase edit) => 
                edit.IsEditorVisible;

            internal bool <ShouldOpenEditor>b__31_1() => 
                false;
        }
    }
}

