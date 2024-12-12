namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;

    public class MouseMoveSelectionRowIndicator : MouseMoveSelectionBase
    {
        public static readonly MouseMoveSelectionRowIndicator Instance = new MouseMoveSelectionRowIndicator();

        public override void CaptureMouse(DataViewBase tableView)
        {
            DataViewBase rootView = tableView.RootView;
            if (!ReferenceEquals(MouseHelper.Captured, rootView))
            {
                if (rootView.IsKeyboardFocusWithin)
                {
                    MouseHelper.Capture(rootView);
                }
                else
                {
                    rootView.StopSelection();
                }
            }
        }

        public override void OnMouseDown(DataViewBase tableView, IDataViewHitInfo hitInfo)
        {
            int commonVisibleIndex = tableView.DataControl.GetCommonVisibleIndex(hitInfo.RowHandle);
            double offset = tableView.RootDataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset;
            double x = ((ITableView) tableView.RootView).TableViewBehavior.LastMousePosition.X;
            SelectionAnchorCell cell1 = new SelectionAnchorCell(tableView, hitInfo.RowHandle, hitInfo.Column);
            cell1.OffsetX = offset;
            cell1.CoordinateX = x;
            cell1.RowVisibleIndex = commonVisibleIndex;
            tableView.SelectionAnchor = cell1;
            tableView.RootView.ScrollTimer.Start();
        }

        public override void OnMouseUp(DataViewBase tableView)
        {
            tableView.RootView.ScrollTimer.Stop();
        }

        public override void UpdateSelection(ITableView tableView)
        {
            DataViewBase rootView = tableView.ViewBase.RootView;
            KeyValuePair<DataViewBase, int> viewAndVisibleIndex = rootView.GetViewAndVisibleIndex(((ITableView) rootView).TableViewBehavior.LastMousePosition.Y, true);
            DataViewBase key = viewAndVisibleIndex.Key;
            int visibleIndex = viewAndVisibleIndex.Value;
            if (visibleIndex >= 0)
            {
                int rowHandleByVisibleIndexCore = key.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex);
                if ((key.SelectionOldCell == null) || (!ReferenceEquals(key, key.SelectionOldCell.View) || (rowHandleByVisibleIndexCore != key.SelectionOldCell.RowHandleCore)))
                {
                    this.CaptureMouse(key);
                    int commonVisibleIndex = key.SelectionAnchor.View.DataControl.GetCommonVisibleIndex(key.SelectionAnchor.RowHandleCore);
                    key.SelectionStrategy.SelectOnlyThisMasterDetailRange(commonVisibleIndex, key.DataControl.GetCommonVisibleIndex(rowHandleByVisibleIndexCore));
                    key.SelectionOldCell = new SelectionAnchorCell(key, rowHandleByVisibleIndexCore, null);
                }
            }
        }

        public override bool CanScrollHorizontally =>
            false;

        public override bool CanScrollVertically =>
            true;

        public override bool AllowNavigation =>
            false;
    }
}

