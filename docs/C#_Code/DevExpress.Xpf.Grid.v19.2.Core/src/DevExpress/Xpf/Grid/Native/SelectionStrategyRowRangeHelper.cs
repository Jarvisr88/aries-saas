namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Editors.Helpers;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Input;

    public class SelectionStrategyRowRangeHelper
    {
        private readonly SelectionStrategyRowBase _selectStrategy;
        private HashSet<int> selectedOldRowHandle = new HashSet<int>();
        private List<int> selectedRowHandle = new List<int>();

        public SelectionStrategyRowRangeHelper(SelectionStrategyRowBase selectStrategy)
        {
            this._selectStrategy = selectStrategy;
        }

        public void AddSelected(int rowHandle)
        {
            if (!this.selectedRowHandle.Contains(rowHandle))
            {
                this.selectedRowHandle.Add(rowHandle);
            }
        }

        public void OnAfterMouseLeftButtonDown(IDataViewHitInfo hitInfo, Func<IDataViewHitInfo, bool> IsRemoveSelectedRowHandle, DataControlBase dataControl)
        {
            bool flag = IsRemoveSelectedRowHandle(hitInfo);
            if (!ModifierKeysHelper.IsCtrlPressed(Keyboard.Modifiers) && !ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers))
            {
                if (dataControl != null)
                {
                    dataControl.ClearMasterDetailSelection();
                }
                this._selectStrategy.ClearSelection();
                this.selectedOldRowHandle.Clear();
                this.selectedRowHandle.Clear();
                if (hitInfo.RowHandle != -2147483648)
                {
                    dataControl.SelectItem(hitInfo.RowHandle);
                }
                else if ((dataControl != null) && (dataControl.DataView != null))
                {
                    SelectionAnchorCell cell1 = new SelectionAnchorCell(dataControl.DataView, -2147483648, null);
                    cell1.IsLastRow = true;
                    cell1.RowVisibleIndex = -2147483648;
                    dataControl.DataView.SelectionAnchor = cell1;
                }
            }
            else
            {
                if (ModifierKeysHelper.IsShiftPressed(Keyboard.Modifiers) && ((this.OldRowHandleWithShift != null) && ((dataControl != null) && (dataControl.DataView != null))))
                {
                    int commonVisibleIndex = dataControl.GetCommonVisibleIndex(this.OldRowHandleWithShift.Value);
                    int num2 = dataControl.GetCommonVisibleIndex(hitInfo.RowHandle);
                    int num4 = -1;
                    bool flag2 = true;
                    if (commonVisibleIndex != num2)
                    {
                        int num3;
                        if (commonVisibleIndex > num2)
                        {
                            num3 = num2;
                            num4 = commonVisibleIndex;
                            flag2 = (this.selectedOldRowHandle.Count != 0) && (((IEnumerable<int>) this.selectedOldRowHandle).Min() != num4);
                        }
                        else
                        {
                            num3 = commonVisibleIndex;
                            num4 = num2;
                            flag2 = (this.selectedOldRowHandle.Count != 0) && (((IEnumerable<int>) this.selectedOldRowHandle).Max() != num3);
                        }
                        if (((num3 >= 0) && (num4 >= 0)) & flag2)
                        {
                            for (int i = num3; i <= num4; i++)
                            {
                                if (i != num2)
                                {
                                    int rowHandleByVisibleIndexCore = dataControl.GetRowHandleByVisibleIndexCore(i);
                                    if (this.selectedOldRowHandle.Contains(rowHandleByVisibleIndexCore))
                                    {
                                        this.selectedOldRowHandle.Remove(rowHandleByVisibleIndexCore);
                                        this.selectedRowHandle.Remove(rowHandleByVisibleIndexCore);
                                        if (dataControl.DataView.IsRowSelected(rowHandleByVisibleIndexCore))
                                        {
                                            dataControl.UnselectItem(rowHandleByVisibleIndexCore);
                                        }
                                    }
                                }
                            }
                        }
                    }
                }
                foreach (int num7 in this.selectedRowHandle)
                {
                    this.selectedOldRowHandle.Add(num7);
                }
                int[] selectedRowHandles = dataControl.GetSelectedRowHandles();
                int index = 0;
                while (true)
                {
                    if (index >= selectedRowHandles.Length)
                    {
                        if (flag)
                        {
                            this.selectedOldRowHandle.Remove(hitInfo.RowHandle);
                            this.selectedRowHandle.Remove(hitInfo.RowHandle);
                        }
                        break;
                    }
                    int item = selectedRowHandles[index];
                    this.selectedOldRowHandle.Add(item);
                    index++;
                }
            }
        }

        public void RemoveSelected(int rowHandle)
        {
            this.selectedRowHandle.Remove(rowHandle);
        }

        public void SelectOnlyThisMasterDetailRange(int startCommonVisibleIndex, int endCommonVisibleIndex)
        {
            this._selectStrategy.DoMasterDetailSelectionAction(() => this._selectStrategy.SelectMasterDetailRange(startCommonVisibleIndex, endCommonVisibleIndex));
        }

        public void SelectRowRange(IEnumerable<int> selectedRowsHandles)
        {
            this.selectedRowHandle.Clear();
            this.selectedRowHandle.AddRange(selectedRowsHandles);
            this.SelectRowRangeCore();
        }

        public void SelectRowRange(int startRowHandle, int endRowHandle)
        {
            this.selectedRowHandle.Clear();
            this._selectStrategy.SelectRangeCore(startRowHandle, endRowHandle, rowHandle => this.selectedRowHandle.Add(rowHandle));
            this.SelectRowRangeCore();
        }

        private void SelectRowRangeCore()
        {
            this._selectStrategy.ClearSelection();
            foreach (int num in this.selectedOldRowHandle)
            {
                this._selectStrategy.SelectRow(num);
            }
            foreach (int num2 in this.selectedRowHandle)
            {
                this._selectStrategy.SelectRow(num2);
            }
        }

        public int? OldRowHandleWithShift { get; set; }
    }
}

