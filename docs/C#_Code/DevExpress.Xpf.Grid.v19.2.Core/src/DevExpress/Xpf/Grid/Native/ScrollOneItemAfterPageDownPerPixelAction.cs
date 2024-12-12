namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Grid;
    using System;

    internal class ScrollOneItemAfterPageDownPerPixelAction : ScrollOneItemAfterPageDownAction
    {
        public ScrollOneItemAfterPageDownPerPixelAction(DataViewBase view, DataViewBase initialView, int initialVisibleIndex, bool needToAdjustScroll, int previousOffsetDelta, int tryCount) : base(view, initialView, initialVisibleIndex, needToAdjustScroll, previousOffsetDelta, tryCount)
        {
        }

        private void AdjustScrollOffset()
        {
            DataViewBase view = null;
            int visibleIndex = 0;
            base.View.GetLastScrollRowViewAndVisibleIndex(out view, out visibleIndex);
            if ((view != null) && (visibleIndex < (base.View.RootDataPresenter.ItemCount - 1)))
            {
                double num2 = base.View.CalcOffsetForBackwardScrolling(visibleIndex + 1);
                base.View.RootDataPresenter.SetDefineScrollOffset(base.View.RootDataPresenter.ActualScrollOffset - num2, false);
            }
        }

        private void MoveFocusedRowToLastRow()
        {
            DataViewBase base2 = base.View.RootView.DataControl.FindLastInnerDetailView();
            DataViewBase view = base.View;
            if ((base2 != null) && (base2.DataControl.VisibleRowCount > 0))
            {
                view = base2;
            }
            view.NavigateToLastRow();
        }

        protected override void MoveFocusedRowToScrollIndex(bool isOffsetDeltaOutOfRange)
        {
            if (isOffsetDeltaOutOfRange)
            {
                this.MoveFocusedRowToLastRow();
            }
            else
            {
                this.AdjustScrollOffset();
                base.MoveFocusedRowToScrollIndex(isOffsetDeltaOutOfRange);
            }
        }
    }
}

