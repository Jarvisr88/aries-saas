namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ItemIsVisibleChangedEventArgs : RoutedEventArgs
    {
        public ItemIsVisibleChangedEventArgs(BaseLayoutItem item, bool isVisible)
        {
            this.Item = item;
            this.IsVisible = isVisible;
            base.RoutedEvent = DockLayoutManager.ItemIsVisibleChangedEvent;
        }

        public bool IsVisible { get; private set; }

        public BaseLayoutItem Item { get; private set; }
    }
}

