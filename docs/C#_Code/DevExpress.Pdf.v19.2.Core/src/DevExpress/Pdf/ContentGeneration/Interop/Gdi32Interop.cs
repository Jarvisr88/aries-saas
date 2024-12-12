namespace DevExpress.Pdf.ContentGeneration.Interop
{
    using System;
    using System.Runtime.InteropServices;

    internal static class Gdi32Interop
    {
        [DllImport("gdi32.dll", SetLastError=true)]
        public static extern uint DeleteEnhMetaFile(IntPtr hemf);
        [DllImport("gdi32.dll", SetLastError=true)]
        public static extern uint DeleteMetaFile(IntPtr hmf);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("gdi32.dll")]
        public static extern bool DeleteObject(IntPtr hObject);
        [DllImport("gdi32.dll", EntryPoint="GetCharacterPlacementW", CharSet=CharSet.Auto)]
        public static extern uint GetCharacterPlacement(IntPtr hdc, string lpString, int nCount, int nMaxExtent, [In, Out] ref GCP_RESULTS lpResults, uint dwFlags);
        [DllImport("gdi32.dll", SetLastError=true)]
        public static extern uint GetEnhMetaFileBits(IntPtr hemf, uint cbBuffer, [Out] byte[] lpbBuffer);
        [DllImport("gdi32.dll")]
        public static extern uint GetFontData(IntPtr hdc, uint dwTable, uint dwOffset, byte[] lpvBuffer, uint cbData);
        [DllImport("gdi32.dll", SetLastError=true)]
        public static extern uint GetMetaFileBitsEx(IntPtr hemf, uint cbBuffer, [Out] byte[] lpbBuffer);
        [DllImport("gdi32.dll")]
        public static extern IntPtr SelectObject(IntPtr hdc, IntPtr hgdiobj);
        [DllImport("gdi32.dll")]
        public static extern uint SetLayout(IntPtr hdc, uint dwLayout);
    }
}

