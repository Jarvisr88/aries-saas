namespace DevExpress.Xpf.Core
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows.Interop;

    internal class NotifyIconHooks
    {
        private const int WH_MOUSE_LL = 14;
        private IntPtr hwndHook = IntPtr.Zero;
        private HookDelegate hookProc;
        private GCHandle hHookProc;

        [SecuritySafeCritical]
        private IntPtr HookProc(int code, IntPtr wParam, IntPtr lParam)
        {
            if (code >= 0)
            {
                int num = wParam.ToInt32();
                if ((num != 0x200) && ((num != 0x20a) && ((num != 0x20e) && (num != 0x205))))
                {
                    IntPtr windowUnderCursor = WpfNotifyIconNativeMethods.GetWindowUnderCursor();
                    if (windowUnderCursor == IntPtr.Zero)
                    {
                        WpfNotifyIcon.CloseContextMenu();
                    }
                    HwndSource source = HwndSource.FromHwnd(windowUnderCursor);
                    if ((source == null) || !source.RootVisual.CheckAccess())
                    {
                        WpfNotifyIcon.CloseContextMenu();
                    }
                }
            }
            return WpfNotifyIconNativeMethods.CallNextHookEx(this.hwndHook, code, (int) wParam, lParam);
        }

        [SecuritySafeCritical]
        internal void SetHook()
        {
            this.UnHook();
            this.hookProc = new HookDelegate(this.HookProc);
            this.hHookProc = GCHandle.Alloc(this.hookProc);
            if (this.hwndHook == IntPtr.Zero)
            {
                this.hwndHook = WpfNotifyIconNativeMethods.SetWindowsHookEx(14, this.hookProc, WpfNotifyIconNativeMethods.GetModuleHandle("user32"), IntPtr.Zero);
            }
            if (this.hwndHook == IntPtr.Zero)
            {
                this.hHookProc.Free();
                this.hHookProc = new GCHandle();
                int error = Marshal.GetLastWin32Error();
                if (error != 0)
                {
                    throw new Win32Exception(error);
                }
            }
        }

        [SecuritySafeCritical]
        internal void UnHook()
        {
            if (this.hwndHook != IntPtr.Zero)
            {
                this.hHookProc.Free();
                this.hHookProc = new GCHandle();
                if (!WpfNotifyIconNativeMethods.UnhookWindowsHookEx(this.hwndHook))
                {
                    int error = Marshal.GetLastWin32Error();
                    if (error != 0)
                    {
                        throw new Win32Exception(error);
                    }
                }
                this.hwndHook = IntPtr.Zero;
            }
        }

        private delegate IntPtr HookDelegate(int nCode, IntPtr wParam, IntPtr lParam);
    }
}

