namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ShowInvisibleItemsChangedEventArgs : RoutedEventArgs
    {
        public ShowInvisibleItemsChangedEventArgs(bool? newValue)
        {
            this.Value = newValue;
            base.RoutedEvent = DockLayoutManager.ShowInvisibleItemsChangedEvent;
        }

        public bool? Value { get; private set; }
    }
}

