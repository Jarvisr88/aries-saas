namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    public class VirtualizedHorizontalNavigationStrategy : NormalHorizontalNavigationStrategy
    {
        public static readonly VirtualizedHorizontalNavigationStrategy VirtualizedHorizontalNavigationStrategyInstance = new VirtualizedHorizontalNavigationStrategy();

        private VirtualizedHorizontalNavigationStrategy()
        {
        }

        private int FindLastNavigationIndex(DataViewBase view, bool isTabNavigation) => 
            this.FindPrevNavigationIndex(view, isTabNavigation, view.VisibleColumnsCore.Count);

        private int FindPrevNavigationIndex(DataViewBase view, bool isTabNavigation, int currentIndex)
        {
            for (int i = currentIndex - 1; i > -1; i--)
            {
                if (view.IsColumnNavigatable(view.VisibleColumnsCore[i], isTabNavigation))
                {
                    return i;
                }
            }
            return -2147483648;
        }

        internal override bool IsBeginNavigationIndex(DataViewBase view, bool isTabNavigation) => 
            view.NavigationIndex == base.FindFirstNavigationIndex(view, isTabNavigation);

        internal override bool IsEndNavigationIndex(DataViewBase view, bool isTabNavigation) => 
            view.NavigationIndex == this.FindLastNavigationIndex(view, isTabNavigation);

        public override void MakeCellVisible(TableViewBehavior view)
        {
        }

        public override void MoveFirstNavigationIndex(DataViewBase view)
        {
            this.MoveFirstNavigationIndex(view, false);
        }

        internal override void MoveFirstNavigationIndex(DataViewBase view, bool isTabNavigation)
        {
            if (view.IsExpandableRowFocused())
            {
                base.MoveFirstNavigationIndex(view, isTabNavigation);
            }
            else
            {
                base.MoveFirstNavigationIndexCore(view, isTabNavigation);
            }
        }

        public override void MoveLastNavigationIndex(DataViewBase view)
        {
            this.MoveLastNavigationIndex(view, false);
        }

        internal override void MoveLastNavigationIndex(DataViewBase view, bool isTabNavigation)
        {
            if (view.IsExpandableRowFocused())
            {
                base.MoveLastNavigationIndex(view, isTabNavigation);
            }
            else
            {
                int num = this.FindLastNavigationIndex(view, isTabNavigation);
                if (num != -2147483648)
                {
                    view.NavigationIndex = num;
                }
            }
        }

        public override void MoveNextNavigationIndex(DataViewBase view)
        {
            this.MoveNextNavigationIndex(view, false);
        }

        internal override void MoveNextNavigationIndex(DataViewBase view, bool isTabNavigation)
        {
            if (view.NavigationIndex < 0)
            {
                view.MoveFirstNavigationIndex();
            }
            else if (view.NavigationIndex < (view.VisibleColumnsCore.Count - 1))
            {
                int num = base.FindNextNavigationIndex(view, isTabNavigation, view.NavigationIndex);
                if (num != -2147483648)
                {
                    view.NavigationIndex = num;
                }
                else
                {
                    int num2 = base.FindFirstNavigationIndex(view, isTabNavigation);
                    if ((num2 != -2147483648) && view.CanMoveNextRow())
                    {
                        view.MoveNextRow();
                        view.NavigationIndex = num2;
                    }
                }
            }
        }

        public override void MovePrevNavigationIndex(DataViewBase view)
        {
            this.MovePrevNavigationIndex(view, false);
        }

        internal override void MovePrevNavigationIndex(DataViewBase view, bool isTabNavigation)
        {
            if (view.NavigationIndex < 0)
            {
                view.MoveFirstNavigationIndex();
            }
            else if (view.NavigationIndex > 0)
            {
                int num = this.FindPrevNavigationIndex(view, isTabNavigation, view.NavigationIndex);
                if (num != -2147483648)
                {
                    view.NavigationIndex = num;
                }
                else
                {
                    int num2 = this.FindLastNavigationIndex(view, isTabNavigation);
                    if ((num2 != -2147483648) && view.CanMovePrevRow())
                    {
                        view.MovePrevRow();
                        view.NavigationIndex = num2;
                    }
                }
            }
        }

        public override bool OnBeforeChangePixelScrollOffset(DataViewBase view)
        {
            bool flag;
            try
            {
                view.UpdateButtonsModeAllowRequestUI++;
                flag = view.RequestUIUpdate();
            }
            finally
            {
                view.UpdateButtonsModeAllowRequestUI--;
            }
            return flag;
        }

        public override void OnInvalidateHorizontalScrolling(DataViewBase view)
        {
            view.RowsStateDirty = true;
        }

        public override void OnNavigationIndexChanged(DataViewBase view)
        {
            if (view.NavigationIndex != -2147483648)
            {
                ((ITableView) view).TableViewBehavior.MakeColumnVisible(view.DataControl.CurrentColumn);
            }
        }

        public override void UpdateFixedNoneCellData(ColumnsRowDataBase rowData, TableViewBehavior behavior)
        {
            rowData.UpdateFixedNoneCellData(true);
        }

        public override void UpdateViewportVisibleColumns(TableViewBehavior behavior)
        {
            behavior.UpdateViewportVisibleColumnsCore();
        }
    }
}

