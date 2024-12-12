namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid;
    using System;

    internal class ScrollOnePageUpAction : IAction
    {
        private DataViewBase view;

        public ScrollOnePageUpAction(DataViewBase view)
        {
            this.view = view;
        }

        public void Execute()
        {
            if (this.View.ImmediateActionsManager.FindActionOfType(typeof(ScrollOneItemAfterPageUpPageDownAction)) == null)
            {
                int offsetDeltaForPageUp = ScrollActionsHelper.GetOffsetDeltaForPageUp(this.View);
                bool needToAdjustScroll = true;
                if (offsetDeltaForPageUp >= 0)
                {
                    offsetDeltaForPageUp = -1;
                    needToAdjustScroll = false;
                }
                DataViewBase view = null;
                int visibleIndex = 0;
                this.View.GetFirstScrollRowViewAndVisibleIndex(out view, out visibleIndex);
                this.View.RootDataPresenter.SetDefineScrollOffset((double) ((((int) Math.Ceiling(this.View.RootDataPresenter.ActualScrollOffset)) + offsetDeltaForPageUp) - ScrollActionsHelper.GetGroupSummaryRowCountBeforeRow(this.View, visibleIndex, needToAdjustScroll)), false);
                this.View.AddScrollOneItemAfterPageUpAction(view, visibleIndex, needToAdjustScroll, 0, 0);
            }
        }

        public DataViewBase View =>
            this.view;
    }
}

