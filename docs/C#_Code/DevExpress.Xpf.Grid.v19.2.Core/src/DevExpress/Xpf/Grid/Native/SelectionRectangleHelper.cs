namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class SelectionRectangleHelper
    {
        protected Point startPointView;
        protected Point endPointView;
        protected Rect actualRect;

        protected SelectionRectangleHelper()
        {
        }

        internal static void CreateSelectionRectangle(DataViewBase view)
        {
            Border border = new Border {
                Style = view.SelectionRectangleStyle
            };
            view.SelectionRectangle = border;
        }

        public static Point GetMaxTransformPoint(DataViewBase view) => 
            view.RootView.ScrollContentPresenter.TransformToAncestor(view.RootView).Transform(new Point(view.RootView.ScrollContentPresenter.ActualWidth, view.RootView.ScrollContentPresenter.ActualHeight));

        public static Point GetMinTransformPoint(DataViewBase view, double indicatorWidth) => 
            view.RootView.ScrollContentPresenter.TransformToAncestor(view.RootView).Transform(new Point(indicatorWidth, 0.0));

        protected Point GetTransformPoint(DataViewBase view, DataViewBehavior behavior) => 
            this.GetTransformPoint(view.RootView, behavior.View.RootView.ViewBehavior.LastMousePosition);

        public Point GetTransformPoint(DataViewBase view, Point point)
        {
            Point minTransformPoint = GetMinTransformPoint(view, this.IndicatorWidth);
            Point maxTransformPoint = GetMaxTransformPoint(view);
            Point point4 = view.RootView.ScrollContentPresenter.TransformToAncestor(view.RootView.DataControl).Transform(point);
            if (point4.Y > maxTransformPoint.Y)
            {
                point4.Y = maxTransformPoint.Y;
            }
            if (point4.Y < minTransformPoint.Y)
            {
                point4.Y = minTransformPoint.Y;
            }
            if (point4.X > maxTransformPoint.X)
            {
                point4.X = maxTransformPoint.X;
            }
            if (point4.X < minTransformPoint.X)
            {
                point4.X = minTransformPoint.X;
            }
            return point4;
        }

        public abstract void OnMouseDown(DataViewBase view, int rowIndex = 0, ColumnBase column = null);
        public void OnMouseUp(DataViewBase view)
        {
            view.SelectionRectangle = null;
        }

        protected void UpdateActualRect()
        {
            double x;
            double y;
            double num3;
            double num4;
            if (this.endPointView.X < this.startPointView.X)
            {
                x = this.endPointView.X;
                num3 = this.startPointView.X - this.endPointView.X;
            }
            else
            {
                x = this.startPointView.X;
                num3 = this.endPointView.X - this.startPointView.X;
            }
            if (this.endPointView.Y < this.startPointView.Y)
            {
                y = this.endPointView.Y;
                num4 = this.startPointView.Y - this.endPointView.Y;
            }
            else
            {
                y = this.startPointView.Y;
                num4 = this.endPointView.Y - this.startPointView.Y;
            }
            if ((num3 != 0.0) || (num4 != 0.0))
            {
                if (num3 < 1.0)
                {
                    num3 = 1.0;
                }
                if (num4 < 1.0)
                {
                    num4 = 1.0;
                }
            }
            this.actualRect = new Rect(x, y, num3, num4);
        }

        public abstract void UpdateSelection(DataViewBase view, DataViewBehavior behavior, double indicatorWidth = 0.0);
        protected void UpdateSelectionRect(DataViewBase view)
        {
            if (view.SelectionRectangle == null)
            {
                CreateSelectionRectangle(view);
            }
            Border selectionRectangle = view.SelectionRectangle;
            this.UpdateActualRect();
            view.RootView.SelectionRectanglAdorner.UpdateLocation(new Point(this.actualRect.X, this.actualRect.Y));
            selectionRectangle.Width = this.actualRect.Width;
            selectionRectangle.Height = this.actualRect.Height;
            if ((this.actualRect.Width <= 0.0) && (this.actualRect.Height <= 0.0))
            {
                selectionRectangle.Visibility = Visibility.Hidden;
            }
            else
            {
                selectionRectangle.Visibility = Visibility.Visible;
            }
        }

        protected void ValidateAndUpdateRectangle(Point min, Point max, DataViewBase view)
        {
            this.UpdateActualRect();
            if ((min.Y > this.actualRect.Y) || (min.X > this.actualRect.X))
            {
                if ((min.X - this.actualRect.X) > 0.0)
                {
                    this.startPointView.X = min.X;
                }
                if ((min.Y - this.actualRect.Y) > 0.0)
                {
                    this.startPointView.Y = min.Y;
                }
            }
            else if ((max.Y < (this.actualRect.Y + this.actualRect.Height)) || (max.X < (this.actualRect.X + this.actualRect.Width)))
            {
                if ((max.X - (this.actualRect.X + this.actualRect.Width)) < 0.0)
                {
                    this.startPointView.X = max.X;
                }
                if ((max.Y - (this.actualRect.Y + this.actualRect.Height)) < 0.0)
                {
                    this.startPointView.Y = max.Y;
                }
            }
            this.UpdateSelectionRect(view);
        }

        protected double IndicatorWidth { get; set; }

        public Point StartPoint =>
            this.startPointView;

        public Point EndPoint =>
            this.endPointView;
    }
}

