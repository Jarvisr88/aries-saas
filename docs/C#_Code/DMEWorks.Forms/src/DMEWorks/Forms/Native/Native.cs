namespace DMEWorks.Forms.Native
{
    using System;
    using System.Runtime.InteropServices;

    internal class Native
    {
        [DllImport("user32.dll")]
        public static extern bool DestroyIcon(IntPtr hIcon);
        [DllImport("shell32.dll", CharSet=CharSet.Auto)]
        public static extern IntPtr ExtractIcon(IntPtr hInst, string lpszExeFileName, int nIconIndex);
    }
}

