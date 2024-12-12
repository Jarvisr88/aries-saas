namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class AutoSuggestEditSuggestionChoosingEventArgs : EventArgs
    {
        public AutoSuggestEditSuggestionChoosingEventArgs(object selectedItem)
        {
            this.SelectedItem = selectedItem;
        }

        public object SelectedItem { get; set; }

        public bool Handled { get; set; }
    }
}

