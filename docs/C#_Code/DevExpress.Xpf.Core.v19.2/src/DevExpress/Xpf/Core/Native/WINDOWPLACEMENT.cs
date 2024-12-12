namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal class WINDOWPLACEMENT
    {
        public int length;
        public int flags;
        public ShowWindowOptions showCmd;
        public POINT ptMinPosition;
        public POINT ptMaxPosition;
        public RECT rcNormalPosition;
        public WINDOWPLACEMENT();
    }
}

