namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;
    using System.Windows.Media;
    using System.Windows.Media.Animation;
    using System.Windows.Shapes;

    public class UpdateRowRectangleHelper
    {
        static UpdateRowRectangleHelper()
        {
            AllowAnimations = true;
        }

        protected Rect BoundsRelativeTo(FrameworkElement element, Visual relativeTo) => 
            element.TransformToVisual(relativeTo).TransformBounds(LayoutInformation.GetLayoutSlot(element));

        public virtual void CreateRectangle(DataViewBase view)
        {
            if (view.RootView.IsLoaded)
            {
                Path path1 = new Path();
                path1.Visibility = Visibility.Collapsed;
                Path path = path1;
                path.Style = ((ITableView) view).UpdateRowRectangleStyle;
                view.RootView.UpdateRowRectangle = path;
            }
        }

        public virtual void UpdatePosition(DataViewBase view)
        {
            ITableView view2 = view as ITableView;
            ITableView view3 = (view != null) ? (view.RootView as ITableView) : null;
            if ((view2 == null) || (!view.AreUpdateRowButtonsShown || ((view.DataControl == null) || (view.FocusedRowElement == null))))
            {
                Path updateRowRectangle = view.UpdateRowRectangle;
                if (updateRowRectangle != null)
                {
                    if (!AllowAnimations)
                    {
                        updateRowRectangle.Visibility = Visibility.Collapsed;
                    }
                    else if (updateRowRectangle.Visibility == Visibility.Visible)
                    {
                        DoubleAnimation animation = new DoubleAnimation(1.0, 0.0, this.AnimationDuration);
                        animation.Completed += (s, e) => (updateRowRectangle.Visibility = Visibility.Collapsed);
                        updateRowRectangle.BeginAnimation(UIElement.OpacityProperty, animation);
                    }
                }
            }
            else
            {
                Path updateRowRectangle = view.RootView.UpdateRowRectangle;
                if (updateRowRectangle == null)
                {
                    if (!view.RootView.IsLoaded)
                    {
                        return;
                    }
                    this.CreateRectangle(view.RootView);
                    updateRowRectangle = view.RootView.UpdateRowRectangle;
                }
                if (updateRowRectangle != null)
                {
                    RectangleGeometry geometry = new RectangleGeometry(this.BoundsRelativeTo(view.RootView, view.RootView));
                    FrameworkElement rowElementByRowHandle = view.GetRowElementByRowHandle(view.FocusedRowHandle);
                    if (rowElementByRowHandle == null)
                    {
                        updateRowRectangle.Visibility = Visibility.Collapsed;
                    }
                    else
                    {
                        if (view.IsHorizontalScrollBarVisible || ((view3 != null) && view3.ShowDataNavigator))
                        {
                            FrameworkElement treeRoot = LayoutHelper.FindElementByName(view.RootView, "gridScroll");
                            if (treeRoot != null)
                            {
                                FrameworkElement element = LayoutHelper.FindElementByName(treeRoot, "PART_HorizontalScrollBar");
                                RectangleGeometry geometry3 = new RectangleGeometry(this.BoundsRelativeTo(element, view.RootView));
                                if ((element != null) && (ScrollBarExtensions.GetScrollBarMode(element) == ScrollBarMode.Standard))
                                {
                                    geometry.Rect = new Rect(geometry.Rect.X, geometry.Rect.Y, geometry.Rect.Width, geometry3.Rect.Y - geometry.Rect.Y);
                                }
                            }
                        }
                        RectangleGeometry geometry2 = new RectangleGeometry(LayoutHelper.GetRelativeElementRect(rowElementByRowHandle, view.RootView));
                        if (view2.ShowIndicator)
                        {
                            geometry2.Rect = new Rect(geometry2.Rect.X + view2.IndicatorWidth, geometry2.Rect.Y, geometry2.Rect.Width - view2.IndicatorWidth, geometry2.Rect.Height);
                        }
                        double offsetByRowElement = view.GetOffsetByRowElement(rowElementByRowHandle);
                        geometry2.Rect = new Rect(geometry2.Rect.X + offsetByRowElement, geometry2.Rect.Y, geometry2.Rect.Width - offsetByRowElement, geometry2.Rect.Height);
                        if (view2.ShowHorizontalLines)
                        {
                            geometry2.Rect = new Rect(geometry2.Rect.X, geometry2.Rect.Y - 1.0, geometry2.Rect.Width, geometry2.Rect.Height + 1.0);
                        }
                        if (!view2.ShowIndicator)
                        {
                            updateRowRectangle.Data = new CombinedGeometry(GeometryCombineMode.Exclude, geometry, geometry2);
                        }
                        else
                        {
                            RectangleGeometry geometry4 = new RectangleGeometry(LayoutHelper.GetRelativeElementRect(rowElementByRowHandle, view.RootView));
                            geometry4.Rect = new Rect(geometry4.Rect.X, geometry4.Rect.Y, view2.IndicatorWidth - 1.0, geometry4.Rect.Height);
                            updateRowRectangle.Data = new CombinedGeometry(GeometryCombineMode.Exclude, geometry, new CombinedGeometry(GeometryCombineMode.Union, geometry2, geometry4));
                        }
                        if (!AllowAnimations)
                        {
                            updateRowRectangle.Visibility = Visibility.Visible;
                        }
                        else if (updateRowRectangle.Visibility != Visibility.Visible)
                        {
                            DoubleAnimation animation = new DoubleAnimation(0.0, 1.0, this.AnimationDuration);
                            updateRowRectangle.Opacity = 0.0;
                            updateRowRectangle.Visibility = Visibility.Visible;
                            updateRowRectangle.BeginAnimation(UIElement.OpacityProperty, animation);
                        }
                    }
                }
            }
        }

        public static bool AllowAnimations { get; set; }

        public virtual Duration AnimationDuration =>
            new Duration(new TimeSpan(0, 0, 0, 0, 50));
    }
}

