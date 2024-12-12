namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct STYLESTRUCT
    {
        internal int StyleOld;
        internal int StyleNew;
    }
}

