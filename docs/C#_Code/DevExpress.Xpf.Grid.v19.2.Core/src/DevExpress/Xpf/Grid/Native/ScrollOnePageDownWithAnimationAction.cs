namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Collections.Generic;

    internal class ScrollOnePageDownWithAnimationAction : IAction
    {
        private DataViewBase view;

        public ScrollOnePageDownWithAnimationAction(DataViewBase view)
        {
            this.view = view;
        }

        public void Execute()
        {
            int offsetDeltaForPerPixelPageDown = ScrollActionsHelper.GetOffsetDeltaForPerPixelPageDown(this.View);
            if (offsetDeltaForPerPixelPageDown <= 0)
            {
                offsetDeltaForPerPixelPageDown = 1;
            }
            List<int> parentFixedRowsScrollIndexes = this.View.DataControl.GetParentFixedRowsScrollIndexes(this.View.DataProviderBase.CurrentIndex);
            int scrollIndex = ScrollActionsHelper.CalcLastScrollRowFirstInnerChildScrollIndex(this.View) + offsetDeltaForPerPixelPageDown;
            scrollIndex = ScrollActionsHelper.FindNearestScrollableRow(this.View, scrollIndex);
            int num3 = scrollIndex;
            while (parentFixedRowsScrollIndexes.Contains(scrollIndex))
            {
                scrollIndex = this.SkipGroupSummaryRows(++scrollIndex, true);
            }
            IScrollInfoOwner rootDataPresenter = this.View.RootDataPresenter;
            if (scrollIndex >= rootDataPresenter.ItemCount)
            {
                scrollIndex = rootDataPresenter.ItemCount - 1;
                scrollIndex = this.SkipGroupSummaryRows(scrollIndex, false);
                while (parentFixedRowsScrollIndexes.Contains(scrollIndex))
                {
                    scrollIndex = this.SkipGroupSummaryRows(--scrollIndex, false);
                }
            }
            this.View.MoveFocusedRowToScrollIndexForPageUpPageDown(scrollIndex, false);
            this.View.OnPostponedNavigationComplete();
        }

        private int SkipGroupSummaryRows(int index, bool forward)
        {
            while (this.View.DataProviderBase.GetVisibleIndexByScrollIndex(index) is GroupSummaryRowKey)
            {
                index = !forward ? (index - 1) : (index + 1);
            }
            return index;
        }

        public DataViewBase View =>
            this.view;
    }
}

