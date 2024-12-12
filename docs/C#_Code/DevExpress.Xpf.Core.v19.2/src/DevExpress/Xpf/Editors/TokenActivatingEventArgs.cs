namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class TokenActivatingEventArgs : EventArgs
    {
        public TokenActivatingEventArgs(object value)
        {
            this.Value = value;
        }

        public object Value { get; private set; }

        public bool IsCancel { get; set; }
    }
}

