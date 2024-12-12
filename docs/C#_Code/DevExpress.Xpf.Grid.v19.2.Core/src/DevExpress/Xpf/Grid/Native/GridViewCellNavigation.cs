namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;

    public class GridViewCellNavigation : GridViewRowNavigation
    {
        public GridViewCellNavigation(DataViewBase view) : base(view)
        {
        }

        [CompilerGenerated, DebuggerHidden]
        private void <>n__0(bool isCtrlPressed)
        {
            base.OnUp(isCtrlPressed);
        }

        protected internal override void ClearAllStates()
        {
            base.ClearAllCellsStates();
        }

        private List<List<List<ColumnBase>>> CreateBandRows(IList bands)
        {
            List<List<List<ColumnBase>>> bandRows = new List<List<List<ColumnBase>>>();
            this.CreateBandRowsCore(bands, bandRows);
            return bandRows;
        }

        private void CreateBandRowsCore(IList bands, List<List<List<ColumnBase>>> bandRows)
        {
            foreach (BandBase base2 in bands)
            {
                if (base2.BandsCore.Count != 0)
                {
                    this.CreateBandRowsCore(base2.BandsCore, bandRows);
                    continue;
                }
                if (base2.ActualRows.Count != 0)
                {
                    List<List<ColumnBase>> item = new List<List<ColumnBase>>();
                    int num = 0;
                    while (true)
                    {
                        if (num >= base2.ActualRows.Count)
                        {
                            if (item.Count > 0)
                            {
                                bandRows.Add(item);
                            }
                            break;
                        }
                        List<ColumnBase> list2 = new List<ColumnBase>();
                        foreach (ColumnBase base3 in base2.ActualRows[num].Columns)
                        {
                            if (base.View.IsColumnNavigatable(base3, false))
                            {
                                list2.Add(base3);
                            }
                        }
                        if (list2.Count > 0)
                        {
                            item.Add(list2);
                        }
                        num++;
                    }
                }
            }
        }

        protected void DoBandedViewHorzNavigation(bool isRight)
        {
            try
            {
                if (base.View != null)
                {
                    DataViewBase view = base.View;
                    view.UpdateButtonsModeAllowRequestUI++;
                }
                ColumnBase objA = this.FindNavigationColumn(isRight);
                if ((objA != null) && !ReferenceEquals(objA, base.DataControl.CurrentColumn))
                {
                    base.DataControl.CurrentColumn = objA;
                }
            }
            finally
            {
                if (base.View != null)
                {
                    DataViewBase view = base.View;
                    view.UpdateButtonsModeAllowRequestUI--;
                }
            }
        }

        private void DoVertNavigation(Action action, bool isDown)
        {
            ColumnBase item = base.DataControl.CurrentColumn ?? base.View.VisibleColumnsCore[0];
            int index = item.BandRow.Columns.IndexOf(item);
            if (!base.DataControl.IsGroupRowHandleCore(base.View.FocusedRowHandle))
            {
                int rowIndex = item.ParentBand.ActualRows.IndexOf(item.BandRow);
                if (this.TryDoVertNavigation(item.ParentBand, isDown, rowIndex, index))
                {
                    return;
                }
            }
            int focusedRowHandle = base.View.FocusedRowHandle;
            action();
            if ((focusedRowHandle != base.View.FocusedRowHandle) && (this.UseAdvVertNavigation && !base.DataControl.IsGroupRowHandleCore(base.View.FocusedRowHandle)))
            {
                this.TryDoVertNavigation(item.ParentBand, isDown, isDown ? -1 : item.ParentBand.ActualRows.Count, index);
            }
        }

        private void FindColumn(ColumnBase column, List<List<List<ColumnBase>>> bandRows, out int bandIndex, out int rowIndex, out int columnIndex)
        {
            bandIndex = 0;
            rowIndex = 0;
            columnIndex = 0;
            int num = 0;
            while (num < bandRows.Count)
            {
                int num2 = 0;
                while (true)
                {
                    if (num2 >= bandRows[num].Count)
                    {
                        num++;
                        break;
                    }
                    int num3 = 0;
                    while (true)
                    {
                        if (num3 >= bandRows[num][num2].Count)
                        {
                            num2++;
                            break;
                        }
                        if (bandRows[num][num2][num3] == column)
                        {
                            bandIndex = num;
                            rowIndex = num2;
                            columnIndex = num3;
                            return;
                        }
                        num3++;
                    }
                }
            }
        }

        private ColumnBase FindNavigationColumn(bool isRight)
        {
            int num;
            int num2;
            int num3;
            List<List<List<ColumnBase>>> bandRows = this.CreateBandRows(base.DataControl.BandsLayoutCore.VisibleBands);
            if (bandRows.Count == 0)
            {
                return null;
            }
            this.FindColumn(base.DataControl.CurrentColumn, bandRows, out num, out num2, out num3);
            if (isRight)
            {
                if (num3 < (bandRows[num][num2].Count - 1))
                {
                    return bandRows[num][num2][num3 + 1];
                }
                if ((num >= (bandRows.Count - 1)) && (!base.View.ViewBehavior.AutoMoveRowFocusCore || base.View.IsAdditionalRowFocused))
                {
                    return null;
                }
                if (num < (bandRows.Count - 1))
                {
                    num++;
                }
                else if (base.View.ViewBehavior.AutoMoveRowFocusCore && !base.View.IsAdditionalRowFocused)
                {
                    num = 0;
                }
                if (num2 >= bandRows[num].Count)
                {
                    num2 = bandRows[num].Count - 1;
                }
                return bandRows[num][num2][0];
            }
            if (num3 > 0)
            {
                return bandRows[num][num2][num3 - 1];
            }
            if ((num <= 0) && (!base.View.ViewBehavior.AutoMoveRowFocusCore || base.View.IsAdditionalRowFocused))
            {
                return null;
            }
            if (num > 0)
            {
                num--;
            }
            else if (base.View.ViewBehavior.AutoMoveRowFocusCore && !base.View.IsAdditionalRowFocused)
            {
                num = bandRows.Count - 1;
            }
            if (num2 >= bandRows[num].Count)
            {
                num2 = bandRows[num].Count - 1;
            }
            return bandRows[num][num2][bandRows[num][num2].Count - 1];
        }

        public override bool GetIsFocusedCell(int rowHandle, ColumnBase column) => 
            (base.View.CalcActualFocusedRowHandle() == rowHandle) && ReferenceEquals(base.DataControl.CurrentColumn, column);

        public override void OnDown()
        {
            if (this.UseAdvVertNavigation)
            {
                this.DoVertNavigation(new Action(this.OnDown), true);
            }
            else
            {
                base.OnDown();
            }
        }

        public override void OnEnd(KeyEventArgs e)
        {
            if (!ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
            {
                base.View.MoveLastNavigationIndex();
            }
            else
            {
                base.View.SelectRowForce();
                base.OnEnd(e);
            }
        }

        public override void OnHome(KeyEventArgs e)
        {
            if (!ModifierKeysHelper.IsCtrlPressed(ModifierKeysHelper.GetKeyboardModifiers(e)))
            {
                base.View.MoveFirstNavigationIndex();
            }
            else
            {
                base.View.SelectRowForce();
                base.OnHome(e);
            }
        }

        public override void OnLeft(bool isCtrlPressed)
        {
            if (this.UseAdvHorzNavigation)
            {
                this.DoBandedViewHorzNavigation(false);
            }
            else
            {
                this.OnLeftCore(isCtrlPressed);
            }
        }

        private void OnLeftCore(bool isCtrlPressed)
        {
            if (base.View.IsAdditionalRowFocused && base.View.IsRootView)
            {
                base.View.MovePrevCellCore(false);
            }
            else if (base.View.FocusedRowElement != null)
            {
                if (!base.View.IsExpandableRowFocused())
                {
                    base.View.MovePrevCell();
                }
                else
                {
                    base.OnLeft(isCtrlPressed);
                }
            }
        }

        public override void OnRight(bool isCtrlPressed)
        {
            if (this.UseAdvHorzNavigation)
            {
                this.DoBandedViewHorzNavigation(true);
            }
            else
            {
                this.OnRightCore(isCtrlPressed);
            }
        }

        private void OnRightCore(bool isCtrlPressed)
        {
            if (base.View.IsAdditionalRowFocused && base.View.IsRootView)
            {
                base.View.MoveNextCellCore(false);
            }
            else if (base.View.FocusedRowElement != null)
            {
                if (!base.View.IsExpandableRowFocused())
                {
                    base.View.MoveNextCell();
                }
                else
                {
                    base.OnRight(isCtrlPressed);
                    base.View.MoveFirstNavigationIndex();
                }
            }
        }

        public override void OnTab(bool isShiftPressed)
        {
            if (!base.View.IsAdditionalRowFocused)
            {
                base.TabNavigation(isShiftPressed);
            }
            else if (isShiftPressed)
            {
                base.View.MovePrevCell(true);
            }
            else
            {
                base.View.MoveNextCell(true, false);
            }
        }

        public override void OnUp(bool allowNavigateToAutoFilterRow)
        {
            if (this.UseAdvVertNavigation)
            {
                this.DoVertNavigation(() => this.<>n__0(allowNavigateToAutoFilterRow), false);
            }
            else
            {
                base.OnUp(allowNavigateToAutoFilterRow);
            }
        }

        protected internal override void ProcessMouse(DependencyObject originalSource)
        {
            DependencyObject cell = DataViewBase.FindParentCell(originalSource);
            base.ProcessMouse(originalSource);
            bool flag = base.View.CanMouseNavigationWithUpdateRow() && (base.View.FindRowHandle(originalSource) == base.View.FocusedRowHandle);
            try
            {
                if (flag)
                {
                    DataViewBase view = base.View;
                    view.UpdateButtonsModeAllowRequestUI++;
                }
                base.ProcessMouseOnCell(cell);
            }
            finally
            {
                if (flag)
                {
                    DataViewBase view = base.View;
                    view.UpdateButtonsModeAllowRequestUI--;
                }
            }
        }

        protected override void SetRowFocus(DependencyObject row, bool focus)
        {
            base.SetRowFocusOnCell(row, focus);
            base.SetRowFocus(row, focus);
        }

        private bool TryDoVertNavigation(BandBase band, bool isDown, int rowIndex, int columnIndex)
        {
            ColumnBase base2 = null;
            int num = 0x7fffffff;
            int num2 = isDown ? 1 : -1;
            while (true)
            {
                rowIndex += num2;
                if ((rowIndex < 0) || (rowIndex > (band.ActualRows.Count - 1)))
                {
                    return false;
                }
                int num3 = 0;
                while (true)
                {
                    if (num3 >= band.ActualRows[rowIndex].Columns.Count)
                    {
                        if (base2 == null)
                        {
                            break;
                        }
                        base.DataControl.CurrentColumn = base2;
                        return true;
                    }
                    if (base.View.IsColumnNavigatable(band.ActualRows[rowIndex].Columns[num3], false))
                    {
                        int num4 = Math.Abs((int) (num3 - columnIndex));
                        if (num > num4)
                        {
                            num = num4;
                            base2 = band.ActualRows[rowIndex].Columns[num3];
                        }
                    }
                    num3++;
                }
            }
        }

        public override bool CanSelectCell =>
            true;

        private bool UseAdvVertNavigation =>
            (base.DataControl.BandsLayoutCore != null) && (base.DataControl.BandsLayoutCore.AllowAdvancedVerticalNavigation && (base.View.VisibleColumnsCore.Count != 0));

        protected bool UseAdvHorzNavigation =>
            !base.DataControl.IsGroupRowHandleCore(base.View.FocusedRowHandle) && ((base.DataControl.BandsLayoutCore != null) && base.DataControl.BandsLayoutCore.AllowAdvancedHorizontalNavigation);
    }
}

