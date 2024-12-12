namespace DevExpress.Xpf.Docking
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public class DockTypeChangedEventArgs : RoutedEventArgs
    {
        public DockTypeChangedEventArgs(Dock value, Dock prev)
        {
            this.Value = value;
            this.PrevValue = prev;
        }

        public Dock Value { get; private set; }

        public Dock PrevValue { get; private set; }
    }
}

