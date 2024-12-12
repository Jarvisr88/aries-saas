namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class AutoSuggestEditQuerySubmittedEventArgs : EventArgs
    {
        public AutoSuggestEditQuerySubmittedEventArgs(string text)
        {
            this.<Text>k__BackingField = text;
        }

        public string Text { get; }
    }
}

