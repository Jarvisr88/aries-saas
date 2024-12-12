namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    public class BarControlLoadedEventArgs : EventArgs
    {
        public BarControlLoadedEventArgs(DevExpress.Xpf.Bars.Bar bar, DevExpress.Xpf.Bars.BarControl barControl);

        public DevExpress.Xpf.Bars.Bar Bar { get; private set; }

        public DevExpress.Xpf.Bars.BarControl BarControl { get; private set; }
    }
}

