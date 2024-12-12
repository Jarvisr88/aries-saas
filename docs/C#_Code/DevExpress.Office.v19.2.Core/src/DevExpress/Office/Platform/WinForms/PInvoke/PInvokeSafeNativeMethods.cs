namespace DevExpress.Office.Platform.WinForms.PInvoke
{
    using System;
    using System.Runtime.InteropServices;

    internal static class PInvokeSafeNativeMethods
    {
        [DllImport("User32.dll")]
        public static extern IntPtr LoadImage(IntPtr hinst, string lpszName, uint type, int cxDesired, int cyDesired, uint fuLoad);
    }
}

