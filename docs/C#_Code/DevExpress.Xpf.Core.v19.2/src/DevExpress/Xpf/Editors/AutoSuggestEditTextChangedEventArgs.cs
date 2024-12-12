namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class AutoSuggestEditTextChangedEventArgs : EventArgs
    {
        public AutoSuggestEditTextChangedEventArgs(string text, AutoSuggestEditChangeTextReason reason)
        {
            this.<Text>k__BackingField = text;
            this.<Reason>k__BackingField = reason;
        }

        public string Text { get; }

        public AutoSuggestEditChangeTextReason Reason { get; }
    }
}

