namespace DevExpress.Xpf.Docking.Platform.Win32
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct MINMAXINFO
    {
        public DevExpress.Xpf.Core.NativeMethods.POINT ptReserved;
        public DevExpress.Xpf.Core.NativeMethods.POINT ptMaxSize;
        public DevExpress.Xpf.Core.NativeMethods.POINT ptMaxPosition;
        public DevExpress.Xpf.Core.NativeMethods.POINT ptMinTrackSize;
        public DevExpress.Xpf.Core.NativeMethods.POINT ptMaxTrackSize;
    }
}

