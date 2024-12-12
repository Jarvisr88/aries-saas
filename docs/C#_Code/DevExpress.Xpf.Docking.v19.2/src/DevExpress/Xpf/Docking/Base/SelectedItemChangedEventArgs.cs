namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SelectedItemChangedEventArgs : RoutedEventArgs
    {
        public SelectedItemChangedEventArgs(BaseLayoutItem item, BaseLayoutItem oldItem)
        {
            base.RoutedEvent = LayoutGroup.SelectedItemChangedEvent;
            this.Item = item;
            this.OldItem = oldItem;
        }

        public BaseLayoutItem Item { get; private set; }

        public BaseLayoutItem OldItem { get; private set; }
    }
}

