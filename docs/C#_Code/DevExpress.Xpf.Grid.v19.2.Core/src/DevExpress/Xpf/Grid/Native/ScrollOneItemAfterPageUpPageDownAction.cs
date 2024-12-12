namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid;
    using System;
    using System.Windows;

    internal abstract class ScrollOneItemAfterPageUpPageDownAction : IAction
    {
        private int tryCount;
        private DataViewBase view;
        private DataViewBase initialView;
        private int initialVisibleIndex;
        private int previousOffsetDelta;
        private bool needToAdjustScroll;

        public ScrollOneItemAfterPageUpPageDownAction(DataViewBase view, DataViewBase initialView, int initialVisibleIndex, bool needToAdjustScroll, int previousOffsetDelta, int tryCount)
        {
            this.view = view;
            this.initialView = initialView;
            this.initialVisibleIndex = initialVisibleIndex;
            this.needToAdjustScroll = needToAdjustScroll;
            this.previousOffsetDelta = previousOffsetDelta;
            this.tryCount = tryCount;
        }

        protected abstract void AddScrollOneItemAction(int offsetDelta);
        private bool CheckIsBadCondition() => 
            this.TryCount > 100;

        public void Execute()
        {
            if (!this.CheckIsBadCondition())
            {
                int offsetDelta = (this.InitialView.DataControl.FindFirstInnerChildScrollIndex(this.InitialVisibleIndex) - this.GetRealOffset()) + this.GetGroupSummaryRowOffset(this.InitialVisibleIndex);
                bool isOffsetDeltaOutOfRange = this.IsOffsetDeltaOutOfRange(offsetDelta);
                if (!((offsetDelta == 0) | isOffsetDeltaOutOfRange) && (this.NeedToAdjustScroll && ((this.PreviousOffsetDelta * offsetDelta) >= 0)))
                {
                    if (offsetDelta > 0)
                    {
                        offsetDelta = 1;
                    }
                    if (offsetDelta < 0)
                    {
                        offsetDelta = -1;
                    }
                    this.View.RootDataPresenter.SetDefineScrollOffset(this.View.RootDataPresenter.ActualScrollOffset + offsetDelta, false);
                    this.AddScrollOneItemAction(offsetDelta);
                }
                else
                {
                    if (this.View.ShowGroupSummaryFooter && (offsetDelta > 0))
                    {
                        RowData lastFooterRowData = this.GetLastFooterRowData();
                        if ((lastFooterRowData != null) && (!this.IsElementVisible(lastFooterRowData) && (this.tryCount < 8)))
                        {
                            this.view.RootDataPresenter.SetDefineScrollOffset(this.view.RootDataPresenter.ActualScrollOffset + 1.0, false);
                            this.AddScrollOneItemAction(1);
                            return;
                        }
                    }
                    this.MoveFocusedRowToScrollIndex(isOffsetDeltaOutOfRange);
                    this.View.OnPostponedNavigationComplete();
                }
            }
        }

        protected virtual int GetGroupSummaryRowOffset(int visibleIndex) => 
            0;

        private RowData GetLastFooterRowData()
        {
            RowData data = null;
            GridRowsEnumerator enumerator = this.view.RootView.CreateVisibleRowsEnumerator();
            while (enumerator.MoveNext())
            {
                RowData currentRowData = enumerator.CurrentRowData as RowData;
                if ((currentRowData != null) && (currentRowData.MatchKey is GroupSummaryRowKey))
                {
                    data = currentRowData;
                }
            }
            return ((data != null) ? data : null);
        }

        protected abstract int GetRealOffset();
        private bool IsElementVisible(RowData rowData)
        {
            FrameworkElement rowElement = rowData.RowElement;
            return ((rowElement != null) && (rowElement.IsVisible && !this.view.DataPresenter.IsElementPartiallyVisible(rowElement)));
        }

        protected abstract bool IsOffsetDeltaOutOfRange(int offsetDelta);
        protected abstract void MoveFocusedRowToScrollIndex(bool isOffsetDeltaOutOfRange);

        protected DataViewBase View =>
            this.view;

        protected DataViewBase InitialView =>
            this.initialView;

        protected int InitialVisibleIndex =>
            this.initialVisibleIndex;

        protected bool NeedToAdjustScroll =>
            this.needToAdjustScroll;

        protected int PreviousOffsetDelta =>
            this.previousOffsetDelta;

        protected int TryCount =>
            this.tryCount;
    }
}

