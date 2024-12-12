namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class TokenTextChangingEventArgs : EventArgs
    {
        public TokenTextChangingEventArgs(string oldText, string newText)
        {
            this.OldText = oldText;
            this.NewText = newText;
        }

        public string OldText { get; private set; }

        public string NewText { get; private set; }

        public string Text { get; set; }

        public bool Handled { get; set; }
    }
}

