namespace DevExpress.Xpf.Editors.Helpers
{
    using System;
    using System.Runtime.CompilerServices;

    public class ItemsProviderViewRefreshedEventArgs : ItemsProviderChangedEventArgs
    {
        public ItemsProviderViewRefreshedEventArgs(object handle)
        {
            this.Handle = handle;
        }

        public object Handle { get; private set; }
    }
}

