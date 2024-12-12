namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public class SelectionRectangleTableViewHelper : SelectionRectangleHelper
    {
        private double scrollOffsetVertical;
        private double scrollOffsetHorizontal;

        public static Tuple<int, ColumnBase> GetActualRowAndColumn(ITableViewHitInfo hitInfo, DataViewBase view)
        {
            int rowHandle = 0;
            ColumnBase column = null;
            if (hitInfo.IsRowCell)
            {
                rowHandle = hitInfo.RowHandle;
                column = hitInfo.Column;
            }
            else if (!hitInfo.IsRowCell && hitInfo.InRow)
            {
                rowHandle = hitInfo.RowHandle;
                column = view.VisibleColumnsCore.Last<ColumnBase>();
            }
            else if (hitInfo.IsDataArea && (hitInfo.RowHandle == -2147483648))
            {
                ITableView view2 = (ITableView) view;
                KeyValuePair<DataViewBase, int> viewAndVisibleIndex = view.GetViewAndVisibleIndex(view.ViewBehavior.LastMousePosition.Y, true);
                int visibleIndex = 0;
                if (viewAndVisibleIndex.Key == null)
                {
                    visibleIndex = view.DataControl.LastRowIndex;
                }
                else
                {
                    visibleIndex = viewAndVisibleIndex.Value;
                    if (visibleIndex < 0)
                    {
                        return null;
                    }
                }
                rowHandle = view.DataControl.GetRowHandleByVisibleIndexCore(visibleIndex);
                if ((rowHandle == -2147483647) && (visibleIndex > 0))
                {
                    rowHandle = view.DataControl.GetRowHandleByVisibleIndexCore(view.DataControl.LastRowIndex - 1);
                }
                column = view2.TableViewBehavior.GetColumn(view2.TableViewBehavior.LastMousePosition.X);
            }
            return new Tuple<int, ColumnBase>(rowHandle, column);
        }

        public override void OnMouseDown(DataViewBase view, int rowHandle = 0, ColumnBase column = null)
        {
            DataViewBase key = view.GetViewAndVisibleIndex(view.RootView.ViewBehavior.LastMousePosition.Y, false).Key;
            this.RowHandle = rowHandle;
            base.startPointView = base.endPointView = base.GetTransformPoint(view, view.ViewBehavior);
            view.SelectionRectangleAnchor = new StartPointSelectionRectangleInfo(rowHandle, Mouse.GetPosition(key.GetRowElementByRowHandle(rowHandle)), ((IScrollInfo) view.RootView.DataPresenter).HorizontalOffset, base.startPointView, key, column);
            base.UpdateSelectionRect(view);
            IScrollInfo dataPresenter = view.RootView.DataPresenter;
            this.scrollOffsetVertical = dataPresenter.VerticalOffset;
            this.scrollOffsetHorizontal = dataPresenter.HorizontalOffset;
        }

        public override void UpdateSelection(DataViewBase view, DataViewBehavior behavior, double indicatorWidth = 0.0)
        {
            if ((behavior != null) && ((view != null) && ((view.RootView != null) && ((view.RootView.DataControl != null) && ((view.RootView.DataPresenter != null) && ((view.SelectionRectangleAnchor != null) && ((view.SelectionRectangleAnchor.View != null) && (view.SelectionRectangleAnchor.View.DataProviderBase != null))))))))
            {
                DataViewBase key = view.GetViewAndVisibleIndex(behavior.LastMousePosition.Y, false).Key;
                if (key != null)
                {
                    base.IndicatorWidth = indicatorWidth;
                    base.endPointView = base.GetTransformPoint(key, behavior);
                    Point minTransformPoint = GetMinTransformPoint(key, base.IndicatorWidth);
                    Point maxTransformPoint = GetMaxTransformPoint(key);
                    IScrollInfo dataPresenter = view.RootView.DataPresenter;
                    FrameworkElement rowElementByRowHandle = view.SelectionRectangleAnchor.View.GetRowElementByRowHandle(view.SelectionRectangleAnchor.StartRowHandle);
                    if (view.SelectionRectangleAnchor.View.DataProviderBase.IsRowVisible(view.SelectionRectangleAnchor.StartRowHandle) && (rowElementByRowHandle != null))
                    {
                        Point point4 = rowElementByRowHandle.TransformToAncestor(view.RootView.DataControl).Transform(new Point(indicatorWidth, 0.0));
                        base.startPointView = new Point(base.startPointView.X, point4.Y + view.SelectionRectangleAnchor.OffsetRow.Y);
                    }
                    else
                    {
                        bool flag = dataPresenter.VerticalOffset > this.scrollOffsetVertical;
                        base.startPointView.Y = flag ? minTransformPoint.Y : maxTransformPoint.Y;
                    }
                    double num = ((TableViewBehavior) behavior).HorizontalOffset - view.SelectionRectangleAnchor.StartHorizontalOffset;
                    if (Math.Abs(num) > 0.1)
                    {
                        base.startPointView = new Point(view.SelectionRectangleAnchor.StartXY.X - num, base.startPointView.Y);
                    }
                    else if (Math.Abs(num) > dataPresenter.ViewportWidth)
                    {
                        bool flag2 = dataPresenter.HorizontalOffset > this.scrollOffsetHorizontal;
                        base.startPointView.X = flag2 ? minTransformPoint.X : maxTransformPoint.X;
                    }
                    base.ValidateAndUpdateRectangle(minTransformPoint, maxTransformPoint, key);
                }
            }
        }

        public int RowHandle { get; private set; }

        private class StartPointInfo
        {
            private int _startRowHandle;
            private Point _offsetRow;
            private double _startHorizontalOffset;
            private Point _startXY;
            private DataViewBase _view;

            public StartPointInfo(int rowHandle, Point offsetRow, double startHorizontalOffset, Point startClickPosition, DataViewBase view)
            {
                this._startRowHandle = rowHandle;
                this._offsetRow = offsetRow;
                this._startHorizontalOffset = startHorizontalOffset;
                this._startXY = startClickPosition;
                this._view = view;
            }

            public int StartRowHandle =>
                this._startRowHandle;

            public Point OffsetRow =>
                this._offsetRow;

            public double StartHorizontalOffset =>
                this._startHorizontalOffset;

            public Point StartXY =>
                this._startXY;

            public DataViewBase View =>
                this._view;
        }
    }
}

