namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows.Controls;

    public class NotifyItemsProviderSelectionChangedEventArgs : EventArgs
    {
        public NotifyItemsProviderSelectionChangedEventArgs(ListBoxItem item, bool isSelected)
        {
            this.Item = item;
            this.IsSelected = isSelected;
        }

        public bool IsSelected { get; private set; }

        public ListBoxItem Item { get; private set; }
    }
}

