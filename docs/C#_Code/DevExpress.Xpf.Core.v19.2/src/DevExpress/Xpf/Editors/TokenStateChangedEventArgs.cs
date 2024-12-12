namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class TokenStateChangedEventArgs : EventArgs
    {
        public TokenStateChangedEventArgs(object value, ButtonEdit token)
        {
            this.Value = value;
            this.Token = token;
        }

        public ButtonEdit Token { get; private set; }

        public object Value { get; private set; }
    }
}

