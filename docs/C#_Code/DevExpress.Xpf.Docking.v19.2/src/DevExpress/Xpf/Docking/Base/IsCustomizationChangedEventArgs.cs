namespace DevExpress.Xpf.Docking.Base
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class IsCustomizationChangedEventArgs : RoutedEventArgs
    {
        public IsCustomizationChangedEventArgs(bool newValue)
        {
            this.Value = newValue;
            base.RoutedEvent = DockLayoutManager.IsCustomizationChangedEvent;
        }

        public bool Value { get; private set; }
    }
}

