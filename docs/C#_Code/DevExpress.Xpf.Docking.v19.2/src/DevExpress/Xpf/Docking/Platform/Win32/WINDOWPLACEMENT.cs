namespace DevExpress.Xpf.Docking.Platform.Win32
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal class WINDOWPLACEMENT
    {
        public int length = Marshal.SizeOf(typeof(DevExpress.Xpf.Docking.Platform.Win32.WINDOWPLACEMENT));
        public int flags;
        public SW showCmd;
        public DevExpress.Xpf.Core.NativeMethods.POINT ptMinPosition;
        public DevExpress.Xpf.Core.NativeMethods.POINT ptMaxPosition;
        public DevExpress.Xpf.Core.NativeMethods.RECT rcNormalPosition;
    }
}

