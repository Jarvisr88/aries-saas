namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Runtime.CompilerServices;

    [Obsolete]
    public class BarManagerInfo
    {
        public BarManagerInfo(BarManager manager);

        public BarManager Manager { get; private set; }
    }
}

