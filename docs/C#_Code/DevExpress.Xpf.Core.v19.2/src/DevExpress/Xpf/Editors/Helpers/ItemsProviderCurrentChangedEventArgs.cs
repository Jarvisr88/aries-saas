namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class ItemsProviderCurrentChangedEventArgs : ItemsProviderChangedEventArgs
    {
        public ItemsProviderCurrentChangedEventArgs(object currentItem)
        {
            this.CurrentItem = currentItem;
        }

        public object CurrentItem { get; private set; }
    }
}

