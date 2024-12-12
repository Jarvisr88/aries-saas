namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class ItemsProviderSelectionChangedEventArgs : ItemsProviderChangedEventArgs
    {
        public ItemsProviderSelectionChangedEventArgs(object currentValue, bool isSelected)
        {
            this.CurrentValue = currentValue;
            this.IsSelected = isSelected;
        }

        public object CurrentValue { get; private set; }

        public bool IsSelected { get; private set; }
    }
}

