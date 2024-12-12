namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows.Input;

    public class ProcessActivatingKeyAction : CellEditorAction
    {
        private KeyEventArgs e;

        public ProcessActivatingKeyAction(InplaceEditorBase editor, KeyEventArgs e) : base(editor)
        {
            this.e = e;
        }

        public override void Execute()
        {
            base.editor.ProcessActivatingKey(this.e);
        }
    }
}

