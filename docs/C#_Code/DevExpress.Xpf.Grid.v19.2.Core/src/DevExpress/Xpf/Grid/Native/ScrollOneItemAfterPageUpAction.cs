namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    internal class ScrollOneItemAfterPageUpAction : ScrollOneItemAfterPageUpPageDownAction
    {
        public ScrollOneItemAfterPageUpAction(DataViewBase view, DataViewBase initialView, int initialVisibleIndex, bool needToAdjustScroll, int previousOffsetDelta, int tryCount) : base(view, initialView, initialVisibleIndex, needToAdjustScroll, previousOffsetDelta, tryCount)
        {
        }

        protected override void AddScrollOneItemAction(int offsetDelta)
        {
            base.View.AddScrollOneItemAfterPageUpAction(base.InitialView, base.InitialVisibleIndex, base.NeedToAdjustScroll, offsetDelta, base.TryCount + 1);
        }

        protected override int GetGroupSummaryRowOffset(int visibleIndex) => 
            -ScrollActionsHelper.GetGroupSummaryRowCountBeforeRow(base.View, visibleIndex, base.NeedToAdjustScroll);

        protected override int GetRealOffset() => 
            (((int) Math.Ceiling(base.View.RootDataPresenter.ActualScrollOffset)) + base.View.ActualFixedTopRowsCount) + ScrollActionsHelper.GetOffsetDeltaForPageDown(base.View);

        protected override bool IsOffsetDeltaOutOfRange(int offsetDelta)
        {
            IScrollInfoOwner rootDataPresenter = base.View.RootDataPresenter;
            return ((offsetDelta < 0) && (rootDataPresenter.Offset == 0));
        }

        protected override void MoveFocusedRowToScrollIndex(bool isOffsetDeltaOutOfRange)
        {
            base.View.MoveFocusedRowToFirstScrollRow();
        }
    }
}

