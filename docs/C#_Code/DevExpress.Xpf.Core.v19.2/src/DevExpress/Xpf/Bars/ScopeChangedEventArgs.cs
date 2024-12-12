namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class ScopeChangedEventArgs : RoutedEventArgs
    {
        internal ScopeChangedEventArgs(RoutedEvent routedEvent, BarNameScope oldScope, BarNameScope newScope);

        public BarNameScope OldScope { get; private set; }

        public BarNameScope NewScope { get; private set; }
    }
}

