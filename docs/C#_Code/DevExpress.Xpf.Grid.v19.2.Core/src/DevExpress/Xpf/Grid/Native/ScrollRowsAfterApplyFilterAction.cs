namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid;
    using System;

    internal class ScrollRowsAfterApplyFilterAction : IAction
    {
        private DataViewBase view;

        public ScrollRowsAfterApplyFilterAction(DataViewBase view)
        {
            this.view = view;
        }

        public void Execute()
        {
            this.view.ScrollAnimationLocker.DoLockedAction(() => this.view.ScrollIntoView(this.view.FocusedRowHandle));
        }
    }
}

