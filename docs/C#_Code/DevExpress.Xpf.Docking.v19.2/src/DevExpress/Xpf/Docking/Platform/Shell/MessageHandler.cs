namespace DevExpress.Xpf.Docking.Platform.Shell
{
    using DevExpress.Xpf.Docking.Platform.Win32;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal delegate IntPtr MessageHandler(WM uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
}

