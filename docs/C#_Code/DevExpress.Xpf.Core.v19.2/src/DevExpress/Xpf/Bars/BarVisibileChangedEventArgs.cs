namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    public class BarVisibileChangedEventArgs : BarControlLoadedEventArgs
    {
        public BarVisibileChangedEventArgs(Bar bar, BarControl barControl, bool oldValue, bool newValue);

        public bool OldValue { get; private set; }

        public bool NewValue { get; private set; }
    }
}

