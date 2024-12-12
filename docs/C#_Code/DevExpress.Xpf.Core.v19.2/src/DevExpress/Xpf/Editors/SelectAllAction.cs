namespace DevExpress.Xpf.Editors
{
    using System;

    public class SelectAllAction : CellEditorAction
    {
        public SelectAllAction(InplaceEditorBase editor) : base(editor)
        {
        }

        public override void Execute()
        {
            base.editor.SelectAll();
        }
    }
}

