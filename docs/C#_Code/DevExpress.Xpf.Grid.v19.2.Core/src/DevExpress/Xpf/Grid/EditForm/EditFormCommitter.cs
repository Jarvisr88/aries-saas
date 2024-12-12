namespace DevExpress.Xpf.Grid.EditForm
{
    using DevExpress.Xpf.Grid;
    using System;

    internal class EditFormCommitter : EditFormCommitterBase
    {
        private readonly DataViewBase view;

        public EditFormCommitter(DataViewBase view)
        {
            if (view == null)
            {
                throw new ArgumentNullException("view");
            }
            this.view = view;
        }

        protected override bool CommitCore() => 
            this.view.CommitEditing();
    }
}

