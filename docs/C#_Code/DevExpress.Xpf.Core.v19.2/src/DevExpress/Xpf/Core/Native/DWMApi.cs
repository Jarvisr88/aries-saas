namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Security;

    [SecuritySafeCritical]
    internal static class DWMApi
    {
        [SecuritySafeCritical, DllImport("dwmapi.dll")]
        internal static extern int DwmExtendFrameIntoClientArea(IntPtr hWnd, ref MARGINS margins);
        [SecuritySafeCritical, DllImport("dwmapi.dll")]
        private static extern IntPtr DwmIsCompositionEnabled(ref bool pfEnabled);

        public static bool IsCompositionEnabled { get; }
    }
}

