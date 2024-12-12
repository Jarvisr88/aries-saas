namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid;
    using System;

    internal class ScrollOnePageUpWithAnimationAction : IAction
    {
        private DataViewBase view;

        public ScrollOnePageUpWithAnimationAction(DataViewBase view)
        {
            this.view = view;
        }

        public void Execute()
        {
            int offsetDeltaForPageUp = ScrollActionsHelper.GetOffsetDeltaForPageUp(this.View);
            if (offsetDeltaForPageUp >= 0)
            {
                offsetDeltaForPageUp = -1;
            }
            DataViewBase view = null;
            int visibleIndex = 0;
            this.View.GetFirstScrollRowViewAndVisibleIndex(out view, out visibleIndex);
            int scrollIndex = view.DataControl.FindFirstInnerChildScrollIndex(visibleIndex) + offsetDeltaForPageUp;
            if (scrollIndex <= 0)
            {
                scrollIndex = 0;
            }
            this.View.MoveFocusedRowToScrollIndexForPageUpPageDown(scrollIndex, true);
            this.View.OnPostponedNavigationComplete();
        }

        public DataViewBase View =>
            this.view;
    }
}

