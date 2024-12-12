namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    public class MouseMoveSelectionRectangleGridCell : MouseMoveSelectionRectangleBase
    {
        public static readonly MouseMoveSelectionRectangleGridCell Instance = new MouseMoveSelectionRectangleGridCell();

        public override void OnMouseDown(DataViewBase tableView, IDataViewHitInfo hitInfo)
        {
            Tuple<int, ColumnBase> actualRowAndColumn = SelectionRectangleTableViewHelper.GetActualRowAndColumn((ITableViewHitInfo) hitInfo, tableView);
            if (actualRowAndColumn == null)
            {
                base.Stop();
            }
            else
            {
                base.OnMouseDownCore(tableView, actualRowAndColumn.Item1, actualRowAndColumn.Item2, hitInfo.RowHandle == -2147483648);
            }
        }

        public override bool CanScrollHorizontally =>
            true;
    }
}

