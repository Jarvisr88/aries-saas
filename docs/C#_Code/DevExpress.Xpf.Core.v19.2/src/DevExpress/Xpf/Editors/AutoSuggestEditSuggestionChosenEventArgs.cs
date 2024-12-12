namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class AutoSuggestEditSuggestionChosenEventArgs : EventArgs
    {
        public AutoSuggestEditSuggestionChosenEventArgs(object selectedItem)
        {
            this.<SelectedItem>k__BackingField = selectedItem;
        }

        public object SelectedItem { get; }
    }
}

