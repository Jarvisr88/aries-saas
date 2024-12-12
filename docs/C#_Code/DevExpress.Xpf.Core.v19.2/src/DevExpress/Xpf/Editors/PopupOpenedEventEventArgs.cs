namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class PopupOpenedEventEventArgs : EventArgs
    {
        public PopupOpenedEventEventArgs(UITypeEditorValue value)
        {
            this.Value = value;
        }

        public UITypeEditorValue Value { get; private set; }
    }
}

