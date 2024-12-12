namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class BeforeDialogServiceDialogShownEventArgs : EventArgs
    {
        public BeforeDialogServiceDialogShownEventArgs(UITypeEditorValue value)
        {
            this.Value = value;
        }

        public UITypeEditorValue Value { get; private set; }
    }
}

