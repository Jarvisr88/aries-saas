namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class CustomizationFormVisibleChangedEventArgs : RoutedEventArgs
    {
        public CustomizationFormVisibleChangedEventArgs(bool newValue)
        {
            this.Value = newValue;
            base.RoutedEvent = DockLayoutManager.CustomizationFormVisibleChangedEvent;
        }

        public bool Value { get; private set; }
    }
}

