namespace DevExpress.Xpf.Docking.Platform.Win32
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public class MONITORINFO
    {
        public int cbSize = Marshal.SizeOf(typeof(MONITORINFO));
        public DevExpress.Xpf.Core.NativeMethods.RECT rcMonitor;
        public DevExpress.Xpf.Core.NativeMethods.RECT rcWork;
        public int dwFlags;
    }
}

