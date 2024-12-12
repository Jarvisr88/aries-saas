namespace DevExpress.Pdf.Interop
{
    using System;
    using System.Runtime.InteropServices;
    using System.Text;

    internal static class Kernel32Interop
    {
        [DllImport("kernel32.dll", CharSet=CharSet.Unicode, SetLastError=true)]
        public static extern int FormatMessage(FormatMessageFlags dwFlags, IntPtr lpSource_mustBeNull, uint dwMessageId, int dwLanguageId, StringBuilder lpBuffer, int nSize, IntPtr[] arguments);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern bool FreeLibrary(IntPtr hModule);
        [DllImport("kernel32", CharSet=CharSet.Ansi)]
        public static extern IntPtr GetProcAddress(IntPtr hModule, string procName);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalFree(IntPtr hMem);
        [DllImport("kernel32.dll")]
        public static extern IntPtr GlobalLock(IntPtr hMem);
        [return: MarshalAs(UnmanagedType.Bool)]
        [DllImport("kernel32.dll")]
        public static extern bool GlobalUnlock(IntPtr hMem);
        [DllImport("kernel32.dll", SetLastError=true)]
        public static extern IntPtr LoadLibraryEx(string lpFileName, IntPtr hReservedNull, int dwFlags);
        [DllImport("kernel32.dll")]
        public static extern uint WaitForMultipleObjects(int count, [In, MarshalAs(UnmanagedType.LPArray)] IntPtr[] handles, [MarshalAs(UnmanagedType.Bool)] bool waitAll, int milliseconds);
    }
}

