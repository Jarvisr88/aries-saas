namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class ItemsProviderOnBusyChangedEventArgs : ItemsProviderChangedEventArgs
    {
        public ItemsProviderOnBusyChangedEventArgs(bool isBusy)
        {
            this.IsBusy = isBusy;
        }

        public bool IsBusy { get; private set; }
    }
}

