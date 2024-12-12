namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    internal class ScrollOneItemAfterPageDownAction : ScrollOneItemAfterPageUpPageDownAction
    {
        public ScrollOneItemAfterPageDownAction(DataViewBase view, DataViewBase initialView, int initialVisibleIndex, bool needToAdjustScroll, int previousOffsetDelta, int tryCount) : base(view, initialView, initialVisibleIndex, needToAdjustScroll, previousOffsetDelta, tryCount)
        {
        }

        protected override void AddScrollOneItemAction(int offsetDelta)
        {
            base.View.AddScrollOneItemAfterPageDownAction(base.InitialView, base.InitialVisibleIndex, base.NeedToAdjustScroll, offsetDelta, base.TryCount + 1);
        }

        protected override int GetGroupSummaryRowOffset(int visibleIndex) => 
            ScrollActionsHelper.GetGroupSummaryRowCountAfterRow(base.View, visibleIndex, base.NeedToAdjustScroll, true);

        protected override int GetRealOffset() => 
            ((int) Math.Ceiling(base.View.RootDataPresenter.ActualScrollOffset)) + base.View.ActualFixedTopRowsCount;

        protected override bool IsOffsetDeltaOutOfRange(int offsetDelta)
        {
            IScrollInfoOwner rootDataPresenter = base.View.RootDataPresenter;
            return ((offsetDelta > 0) && (rootDataPresenter.Offset >= (rootDataPresenter.ItemCount - rootDataPresenter.ItemsOnPage)));
        }

        protected override void MoveFocusedRowToScrollIndex(bool isOffsetDeltaOutOfRange)
        {
            base.View.MoveFocusedRowToLastScrollRow();
        }
    }
}

