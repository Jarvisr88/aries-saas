namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class AfterDialogServiceDialogClosedEventArgs : EventArgs
    {
        public AfterDialogServiceDialogClosedEventArgs(UITypeEditorValue value, object result)
        {
            this.Value = value;
            this.Result = result;
        }

        public UITypeEditorValue Value { get; private set; }

        public object Result { get; private set; }

        public bool Handled { get; set; }

        public bool? PostValue { get; set; }
    }
}

