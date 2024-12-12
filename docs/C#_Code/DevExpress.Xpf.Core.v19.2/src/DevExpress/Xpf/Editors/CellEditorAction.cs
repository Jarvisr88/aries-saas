namespace DevExpress.Xpf.Editors
{
    using System;

    public abstract class CellEditorAction : IAction
    {
        protected InplaceEditorBase editor;

        public CellEditorAction(InplaceEditorBase editor)
        {
            this.editor = editor;
        }

        public abstract void Execute();
    }
}

