namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid;
    using System;

    internal class ScrollOnePageDownAction : IAction
    {
        private DataViewBase view;

        public ScrollOnePageDownAction(DataViewBase view)
        {
            this.view = view;
        }

        public void Execute()
        {
            if (this.View.ImmediateActionsManager.FindActionOfType(typeof(ScrollOneItemAfterPageUpPageDownAction)) == null)
            {
                int offsetDeltaForPageDown = ScrollActionsHelper.GetOffsetDeltaForPageDown(this.View);
                bool needToAdjustScroll = true;
                if (offsetDeltaForPageDown <= 0)
                {
                    offsetDeltaForPageDown = 1;
                    needToAdjustScroll = false;
                }
                DataViewBase view = null;
                int visibleIndex = 0;
                this.View.GetLastScrollRowViewAndVisibleIndex(out view, out visibleIndex);
                this.View.RootDataPresenter.SetDefineScrollOffset((this.View.RootDataPresenter.ActualScrollOffset + offsetDeltaForPageDown) + ScrollActionsHelper.GetGroupSummaryRowCountAfterRow(this.View, visibleIndex, needToAdjustScroll, true), false);
                this.View.AddScrollOneItemAfterPageDownAction(view, visibleIndex, needToAdjustScroll, 0, 0);
            }
        }

        public DataViewBase View =>
            this.view;
    }
}

