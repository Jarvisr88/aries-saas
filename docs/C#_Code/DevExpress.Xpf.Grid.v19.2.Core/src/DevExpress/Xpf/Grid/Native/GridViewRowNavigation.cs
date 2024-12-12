namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    public class GridViewRowNavigation : GridViewRowNavigationBase
    {
        public GridViewRowNavigation(DataViewBase view) : base(view)
        {
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
            this.GridView.TableViewBehavior.MovePrevRow(isCtrlPressed);
        }

        protected ITableView GridView =>
            (ITableView) base.View;
    }
}

