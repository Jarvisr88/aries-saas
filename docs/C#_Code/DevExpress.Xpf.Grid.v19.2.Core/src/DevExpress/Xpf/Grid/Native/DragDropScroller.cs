namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Windows;

    public static class DragDropScroller
    {
        public static bool IsDragging(DependencyObject sender)
        {
            FrameworkElement topLevelVisual = LayoutHelper.GetTopLevelVisual(sender);
            return ((topLevelVisual != null) ? DragManager.GetIsDragging(topLevelVisual) : false);
        }

        public static void StartScrolling(DataViewBase view)
        {
            if ((view is ITableView) && ((ITableView) view).AllowScrollHeaders)
            {
                view.ScrollTimer.Start();
            }
        }

        public static void StopScrolling(DataViewBase view)
        {
            if (view is ITableView)
            {
                view.ScrollTimer.Stop();
            }
        }
    }
}

