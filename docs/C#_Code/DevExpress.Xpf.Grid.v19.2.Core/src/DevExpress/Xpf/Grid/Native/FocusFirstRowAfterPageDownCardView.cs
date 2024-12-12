namespace DevExpress.Xpf.Grid.Native
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Grid;
    using System;

    internal class FocusFirstRowAfterPageDownCardView : IAction
    {
        private DataViewBase view;

        public FocusFirstRowAfterPageDownCardView(DataViewBase view)
        {
            this.view = view;
        }

        public void Execute()
        {
            this.view.MoveFocusedRowToLastScrollRow();
        }
    }
}

