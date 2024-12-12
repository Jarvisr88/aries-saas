namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal delegate IntPtr MessageHandler(WindowMessageValues uMsg, IntPtr wParam, IntPtr lParam, out bool handled);
}

