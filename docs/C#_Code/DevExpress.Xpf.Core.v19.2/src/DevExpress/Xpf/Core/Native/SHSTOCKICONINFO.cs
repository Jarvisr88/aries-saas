namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential, CharSet=CharSet.Unicode)]
    internal struct SHSTOCKICONINFO
    {
        private const int MAX_PATH = 260;
        public uint cbSize;
        public IntPtr hIcon;
        public int iSysIconIndex;
        public int iIcon;
        [MarshalAs(UnmanagedType.ByValTStr, SizeConst=260)]
        public string szPath;
    }
}

