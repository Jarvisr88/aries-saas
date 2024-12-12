namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;
    using System.Windows;

    public class HorizontalNavigationStrategyBase
    {
        public static readonly HorizontalNavigationStrategyBase NormalHorizontalNavigationStrategyBaseInstance = new HorizontalNavigationStrategyBase();

        protected HorizontalNavigationStrategyBase()
        {
        }

        protected int FindFirstNavigationIndex(DataViewBase view, bool isTabNavigation) => 
            this.FindNextNavigationIndex(view, isTabNavigation, -1);

        protected int FindNextNavigationIndex(DataViewBase view, bool isTabNavigation, int currentIndex)
        {
            for (int i = currentIndex + 1; i < view.VisibleColumnsCore.Count; i++)
            {
                if (view.IsColumnNavigatable(view.VisibleColumnsCore[i], isTabNavigation))
                {
                    return i;
                }
            }
            return -2147483648;
        }

        public virtual bool IsBeginNavigationIndex(DataViewBase view) => 
            this.IsBeginNavigationIndex(view, false);

        internal virtual bool IsBeginNavigationIndex(DataViewBase view, bool isTabNavigation)
        {
            if (view.DataControl == null)
            {
                return false;
            }
            DependencyObject dependencyObject = view.FindNearRightNavigationIndex(-2147483648, isTabNavigation);
            return ((dependencyObject != null) ? (view.NavigationIndex == ColumnBase.GetNavigationIndex(dependencyObject)) : false);
        }

        public virtual bool IsEndNavigationIndex(DataViewBase view) => 
            this.IsEndNavigationIndex(view, false);

        internal virtual bool IsEndNavigationIndex(DataViewBase view, bool isTabNavigation)
        {
            if (view.DataControl == null)
            {
                return false;
            }
            DependencyObject dependencyObject = view.FindNearLeftNavigationIndex(0x7fffffff, isTabNavigation);
            return ((dependencyObject != null) ? (view.NavigationIndex == ColumnBase.GetNavigationIndex(dependencyObject)) : false);
        }

        public virtual void MoveFirstNavigationIndex(DataViewBase view)
        {
            this.MoveFirstNavigationIndex(view, false);
        }

        internal virtual void MoveFirstNavigationIndex(DataViewBase view, bool isTabNavigation)
        {
            DependencyObject dependencyObject = view.FindNavigationIndex(0, view.NavigationIndex, true, isTabNavigation);
            if (dependencyObject != null)
            {
                view.NavigationIndex = ColumnBase.GetNavigationIndex(dependencyObject);
            }
        }

        internal void MoveFirstNavigationIndexCore(DataViewBase view, bool isTabNavigation)
        {
            int num = this.FindFirstNavigationIndex(view, isTabNavigation);
            if (num != -2147483648)
            {
                view.NavigationIndex = num;
            }
        }

        public virtual void MoveLastNavigationIndex(DataViewBase view)
        {
            this.MoveLastNavigationIndex(view, false);
        }

        internal virtual void MoveLastNavigationIndex(DataViewBase view, bool isTabNavigation)
        {
            DependencyObject dependencyObject = view.FindNavigationIndex(view.NavigationIndex, 0x7fffffff, false, isTabNavigation);
            if (dependencyObject != null)
            {
                view.NavigationIndex = ColumnBase.GetNavigationIndex(dependencyObject);
            }
        }

        public virtual void MoveNextNavigationIndex(DataViewBase view)
        {
            this.MoveNextNavigationIndex(view, false);
        }

        internal virtual void MoveNextNavigationIndex(DataViewBase view, bool isTabNavigation)
        {
            DependencyObject dependencyObject = view.FindNearRightNavigationIndex(view.NavigationIndex, isTabNavigation);
            if (dependencyObject != null)
            {
                view.NavigationIndex = ColumnBase.GetNavigationIndex(dependencyObject);
            }
        }

        public virtual void MovePrevNavigationIndex(DataViewBase view)
        {
            this.MovePrevNavigationIndex(view, false);
        }

        internal virtual void MovePrevNavigationIndex(DataViewBase view, bool isTabNavigation)
        {
            DependencyObject dependencyObject = view.FindNearLeftNavigationIndex(view.NavigationIndex, isTabNavigation);
            if (dependencyObject != null)
            {
                view.NavigationIndex = ColumnBase.GetNavigationIndex(dependencyObject);
            }
        }

        public virtual bool OnBeforeChangePixelScrollOffset(DataViewBase view) => 
            true;

        public virtual void OnInvalidateHorizontalScrolling(DataViewBase view)
        {
        }

        public virtual void OnNavigationIndexChanged(DataViewBase view)
        {
        }
    }
}

