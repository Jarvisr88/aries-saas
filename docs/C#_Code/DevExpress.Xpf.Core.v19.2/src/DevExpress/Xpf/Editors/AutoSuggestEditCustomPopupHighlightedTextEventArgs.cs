namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class AutoSuggestEditCustomPopupHighlightedTextEventArgs : EventArgs
    {
        public AutoSuggestEditCustomPopupHighlightedTextEventArgs(string text)
        {
            this.Text = text;
        }

        public string Text { get; set; }

        public bool Handled { get; set; }
    }
}

