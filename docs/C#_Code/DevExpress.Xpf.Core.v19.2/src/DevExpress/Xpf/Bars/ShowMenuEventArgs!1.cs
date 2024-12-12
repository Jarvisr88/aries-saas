namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections.ObjectModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ShowMenuEventArgs : RoutedEventArgs
    {
        public ShowMenuEventArgs(T menu);

        public T Menu { get; private set; }

        public bool Show { get; set; }

        public ReadOnlyCollection<BarItem> Items { get; private set; }

        public MenuCustomizations ActionList { get; }

        public IInputElement TargetElement { get; }
    }
}

