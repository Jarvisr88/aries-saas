namespace DevExpress.Xpf.Docking.Customization
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class DragInfoChangedEventArgs : RoutedEventArgs
    {
        public DragInfoChangedEventArgs(DragInfo info)
        {
            this.Info = info;
            base.RoutedEvent = CustomizationController.DragInfoChangedEvent;
        }

        public DragInfo Info { get; private set; }
    }
}

