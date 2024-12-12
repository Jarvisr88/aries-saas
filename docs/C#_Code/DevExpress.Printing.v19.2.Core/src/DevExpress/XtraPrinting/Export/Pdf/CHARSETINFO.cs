namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct CHARSETINFO
    {
        public int ciCharset;
        public int ciACP;
        public tagFONTSIGNATURE fs;
    }
}

