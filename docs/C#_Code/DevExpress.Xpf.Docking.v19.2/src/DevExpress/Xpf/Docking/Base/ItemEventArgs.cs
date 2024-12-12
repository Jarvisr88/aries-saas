namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ItemEventArgs : RoutedEventArgs
    {
        public ItemEventArgs(BaseLayoutItem item)
        {
            this.Item = item;
        }

        public BaseLayoutItem Item { get; private set; }
    }
}

