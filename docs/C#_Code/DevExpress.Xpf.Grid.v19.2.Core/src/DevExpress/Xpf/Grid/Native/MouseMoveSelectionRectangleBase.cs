namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Utils;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Windows;

    public abstract class MouseMoveSelectionRectangleBase : MouseMoveSelectionBase
    {
        private SelectionRectangleTableViewHelper helper = new SelectionRectangleTableViewHelper();
        private bool isFirst;
        private Point startPointFirst;
        private bool startMouseSelection;
        private bool isStop;

        protected MouseMoveSelectionRectangleBase()
        {
        }

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

        protected void OnMouseDownCore(DataViewBase tableView, int rowHandle, ColumnBase column, bool isLastRow)
        {
            this.isStop = false;
            this.isFirst = true;
            int commonVisibleIndex = tableView.DataControl.GetCommonVisibleIndex(rowHandle);
            double offset = tableView.RootDataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset;
            double x = ((ITableView) tableView.RootView).TableViewBehavior.LastMousePosition.X;
            SelectionAnchorCell cell1 = new SelectionAnchorCell(tableView, rowHandle, column);
            cell1.OffsetX = offset;
            cell1.CoordinateX = x;
            cell1.RowVisibleIndex = commonVisibleIndex;
            cell1.IsLastRow = isLastRow;
            tableView.SelectionAnchor = cell1;
            tableView.RootView.ScrollTimer.Start();
            this.helper.OnMouseDown(tableView, rowHandle, column);
            this.startPointFirst = this.helper.StartPoint;
        }

        public override void OnMouseUp(DataViewBase tableView)
        {
            tableView.RootView.ScrollTimer.Stop();
            this.startMouseSelection = false;
            this.helper.OnMouseUp(tableView);
            this.Stop();
        }

        protected void Stop()
        {
            this.isStop = true;
        }

        public override void UpdateSelection(ITableView tableView)
        {
            if (!this.isStop)
            {
                if (this.isFirst)
                {
                    Point transformPoint = this.helper.GetTransformPoint(tableView.ViewBase, tableView.TableViewBehavior.LastMousePosition);
                    if ((Math.Abs((double) (this.startPointFirst.X - transformPoint.X)) <= SystemParameters.MinimumVerticalDragDistance) && (Math.Abs((double) (this.startPointFirst.Y - transformPoint.Y)) <= SystemParameters.MinimumHorizontalDragDistance))
                    {
                        this.helper.OnMouseDown(tableView.ViewBase, this.helper.RowHandle, tableView.ViewBase.SelectionRectangleAnchor.Column);
                        return;
                    }
                    this.isFirst = false;
                }
                this.UpdateSelectionCore(tableView);
                this.helper.UpdateSelection(tableView.ViewBase, tableView.TableViewBehavior, tableView.ShowIndicator ? tableView.IndicatorWidth : 0.0);
            }
        }

        private void UpdateSelectionCore(ITableView tableView)
        {
            DataViewBase viewBase = tableView.ViewBase;
            bool calcDataArea = false;
            if (viewBase.SelectionAnchor != null)
            {
                calcDataArea = !viewBase.SelectionAnchor.IsLastRow;
            }
            KeyValuePair<DataViewBase, int> viewAndVisibleIndex = viewBase.GetViewAndVisibleIndex(tableView.TableViewBehavior.LastMousePosition.Y, calcDataArea);
            DataViewBase key = viewAndVisibleIndex.Key;
            int visibleIndex = viewAndVisibleIndex.Value;
            if ((visibleIndex >= 0) || (visibleIndex == -2147483648))
            {
                if (!this.startMouseSelection)
                {
                    viewBase.SelectionStrategy.StartMouseSelection();
                    this.startMouseSelection = true;
                }
                int rowHandleByVisibleIndexCore = key.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex);
                int commonVisibleIndex = key.DataControl.GetCommonVisibleIndex(rowHandleByVisibleIndexCore);
                double offset = key.RootDataPresenter.ScrollInfoCore.HorizontalScrollInfo.Offset;
                double x = ((ITableView) key.RootView).TableViewBehavior.LastMousePosition.X;
                ColumnBase columnByCoordinateWithOffset = ((TableViewBehavior) key.ViewBehavior).GetColumnByCoordinateWithOffset(offset + x, new double?(x));
                this.CaptureMouse(key);
                SelectionAnchorCell cell1 = new SelectionAnchorCell(key, rowHandleByVisibleIndexCore, columnByCoordinateWithOffset);
                cell1.OffsetX = offset;
                cell1.CoordinateX = x;
                cell1.RowVisibleIndex = commonVisibleIndex;
                key.SelectionOldCell = cell1;
                ((ITableView) key.SelectionAnchor.View).TableViewBehavior.UpdateSelectionRectCore(rowHandleByVisibleIndexCore, columnByCoordinateWithOffset);
            }
        }

        public override bool CanScrollVertically =>
            true;

        public override bool AllowNavigation =>
            false;
    }
}

