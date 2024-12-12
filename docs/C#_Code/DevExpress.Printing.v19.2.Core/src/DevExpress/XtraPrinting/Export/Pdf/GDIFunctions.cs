namespace DevExpress.XtraPrinting.Export.Pdf
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [SuppressUnmanagedCodeSecurity]
    internal static class GDIFunctions
    {
        [DllImport("GDI32.dll")]
        public static extern bool DeleteObject(IntPtr hgdiobj);
        [DllImport("gdi32.dll", CharSet=CharSet.Auto, SetLastError=true)]
        public static extern uint GetCharacterPlacement(IntPtr hdc, string lpString, int nCount, int nMaxExtent, [In, Out] ref GCP_RESULTS lpResults, uint dwFlags);
        [DllImport("GDI32.dll")]
        public static extern uint GetFontData(IntPtr hdc, uint dwTable, uint dwOffset, byte[] lpvBuffer, uint cbData);
        [DllImport("gdi32.dll", SetLastError=true)]
        public static extern uint GetFontLanguageInfo(IntPtr hdc);
        [DllImport("gdi32.dll", SetLastError=true)]
        public static extern int GetTextCharset(IntPtr hdc);
        [DllImport("GDI32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("gdi32.dll")]
        public static extern bool SetLayout(IntPtr hdc, uint flags);
        [DllImport("gdi32.dll", SetLastError=true)]
        public static extern bool TranslateCharsetInfo(uint pSrc, [In, Out] ref CHARSETINFO lpCs, uint dwFlags);
    }
}

