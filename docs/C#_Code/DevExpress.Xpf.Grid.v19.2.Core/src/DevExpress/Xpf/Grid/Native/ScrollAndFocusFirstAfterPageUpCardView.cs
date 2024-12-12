namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid;
    using DevExpress.Xpf.Grid.Hierarchy;
    using System;

    internal class ScrollAndFocusFirstAfterPageUpCardView : IAction
    {
        private DataViewBase view;
        private int visibleIndex;
        private int tryCount;

        public ScrollAndFocusFirstAfterPageUpCardView(DataViewBase view, int visibleIndex, int tryCount)
        {
            this.view = view;
            this.visibleIndex = visibleIndex;
            this.tryCount = tryCount;
        }

        public void Execute()
        {
            double num3 = ((int) this.CardsHierarchyPanel.CalcExtent(this.view.SizeHelper.GetDefineSize(this.view.DataPresenter.LastConstraint), new int?(this.visibleIndex - this.view.PageOffset))) - ((int) ((this.ScrollInfo.Offset + this.ScrollInfo.Viewport) - 1.0));
            if ((num3 == 0.0) || ((this.ScrollInfo.Viewport < 1.0) || (this.tryCount > 100)))
            {
                this.view.MoveFocusedRowToFirstScrollRow();
            }
            else
            {
                if (num3 > 0.0)
                {
                    num3 = 1.0;
                }
                this.ScrollInfo.SetOffset(this.ScrollInfo.Offset + num3);
                int tryCount = this.tryCount + 1;
                this.tryCount = tryCount;
                this.view.EnqueueImmediateAction(new ScrollAndFocusFirstAfterPageUpCardView(this.view, this.visibleIndex, tryCount));
            }
        }

        private DevExpress.Xpf.Grid.Hierarchy.CardsHierarchyPanel CardsHierarchyPanel =>
            (DevExpress.Xpf.Grid.Hierarchy.CardsHierarchyPanel) this.view.DataPresenter.Panel;

        private ScrollInfoBase ScrollInfo =>
            this.view.DataPresenter.ScrollInfoCore.DefineSizeScrollInfo;
    }
}

