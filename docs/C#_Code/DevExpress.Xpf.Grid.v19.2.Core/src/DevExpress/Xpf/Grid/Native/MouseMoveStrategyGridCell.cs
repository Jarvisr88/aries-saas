namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;

    public class MouseMoveStrategyGridCell : MouseMoveSelectionBase
    {
        public static readonly MouseMoveStrategyGridCell Instance = new MouseMoveStrategyGridCell();
        private int startRowVisibleIndex;
        private double startOffsetX;
        private double startCoordinateX;
        private bool startMouseSelection;

        public override void CaptureMouse(DataViewBase tableView)
        {
            if (!ReferenceEquals(MouseHelper.Captured, tableView))
            {
                if (tableView.RootView.IsKeyboardFocusWithin)
                {
                    MouseHelper.Capture(tableView.RootView);
                }
                else
                {
                    tableView.StopSelection();
                }
            }
        }

        public override void OnMouseDown(DataViewBase tableView, IDataViewHitInfo hitInfo)
        {
            this.startRowVisibleIndex = tableView.DataControl.GetCommonVisibleIndex(hitInfo.RowHandle);
            this.startOffsetX = tableView.RootDataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset;
            this.startCoordinateX = ((ITableView) tableView.RootView).TableViewBehavior.LastMousePosition.X;
            SelectionAnchorCell cell1 = new SelectionAnchorCell(tableView, hitInfo.RowHandle, hitInfo.Column);
            cell1.OffsetX = this.startOffsetX;
            cell1.CoordinateX = this.startCoordinateX;
            cell1.RowVisibleIndex = this.startRowVisibleIndex;
            tableView.SelectionAnchor = cell1;
            tableView.SelectionOldCell = tableView.SelectionAnchor;
            this.startMouseSelection = false;
            tableView.RootView.ScrollTimer.Start();
        }

        public override void OnMouseUp(DataViewBase tableView)
        {
            tableView.RootView.ScrollTimer.Stop();
            this.startMouseSelection = false;
        }

        public override void UpdateSelection(ITableView tableView)
        {
            DataViewBase rootView = tableView.ViewBase.RootView;
            KeyValuePair<DataViewBase, int> viewAndVisibleIndex = rootView.GetViewAndVisibleIndex(((ITableView) rootView).TableViewBehavior.LastMousePosition.Y, true);
            DataViewBase key = viewAndVisibleIndex.Key;
            ITableView view = (key as ITableView) ?? tableView;
            int visibleIndex = viewAndVisibleIndex.Value;
            if (visibleIndex >= 0)
            {
                int rowHandleByVisibleIndexCore = key.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex);
                int commonVisibleIndex = key.DataControl.GetCommonVisibleIndex(rowHandleByVisibleIndexCore);
                double offset = rootView.RootDataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset;
                double x = ((ITableView) rootView).TableViewBehavior.LastMousePosition.X;
                ColumnBase columnByCoordinateWithOffset = ((TableViewBehavior) key.ViewBehavior).GetColumnByCoordinateWithOffset(offset + x, new double?(x));
                if ((commonVisibleIndex != rootView.SelectionOldCell.RowVisibleIndex) || (!ReferenceEquals(rootView.SelectionOldCell.ColumnCore, columnByCoordinateWithOffset) || (Math.Abs((double) ((offset + x) - (rootView.SelectionOldCell.OffsetX + rootView.SelectionOldCell.CoordinateX))) > 3.0)))
                {
                    if ((key != null) && ((key.SelectionAnchor != null) && ((key.DataControl != null) && ((key.SelectionAnchor.ColumnCore != null) && (!key.SelectionAnchor.ColumnCore.AllowFocus && (key.DataControl.CurrentColumn != null))))))
                    {
                        this.startCoordinateX = tableView.TableViewBehavior.GetSelectedColumnOffset(key.DataControl.CurrentColumn) - this.startOffsetX;
                    }
                    if (!this.startMouseSelection)
                    {
                        key.SelectionStrategy.StartMouseSelection();
                        this.startMouseSelection = true;
                    }
                    this.CaptureMouse(key);
                    double num6 = tableView.TableViewBehavior.FixedLeftColumnsWidth();
                    double num7 = ((TableViewBehavior) tableView.ViewBase.RootView.DataControl.FindViewAndVisibleIndexByCommonVisibleIndex(commonVisibleIndex).Key.ViewBehavior).FixedLeftColumnsWidth();
                    double num8 = (this.startCoordinateX > num6) ? this.startOffsetX : 0.0;
                    ((ITableView) key.SelectionAnchor.View).TableViewBehavior.SelectMasterDetailRangeCell(this.startRowVisibleIndex, commonVisibleIndex, this.startCoordinateX + num8, x + ((x > num7) ? offset : 0.0), rootView.DataControl);
                    SelectionAnchorCell cell1 = new SelectionAnchorCell(key, rowHandleByVisibleIndexCore, columnByCoordinateWithOffset);
                    cell1.OffsetX = rootView.RootDataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset;
                    cell1.CoordinateX = ((ITableView) rootView).TableViewBehavior.LastMousePosition.X;
                    cell1.RowVisibleIndex = commonVisibleIndex;
                    rootView.SelectionOldCell = cell1;
                }
            }
        }

        public override bool CanScrollHorizontally =>
            true;

        public override bool CanScrollVertically =>
            true;

        public override bool AllowNavigation =>
            false;
    }
}

