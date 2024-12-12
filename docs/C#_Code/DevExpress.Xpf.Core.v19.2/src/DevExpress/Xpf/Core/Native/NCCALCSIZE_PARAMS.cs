namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    internal struct NCCALCSIZE_PARAMS
    {
        [MarshalAs(UnmanagedType.ByValArray, SizeConst=3)]
        public RECT[] rgrc;
        public WINDOWPOS lppos;
    }
}

