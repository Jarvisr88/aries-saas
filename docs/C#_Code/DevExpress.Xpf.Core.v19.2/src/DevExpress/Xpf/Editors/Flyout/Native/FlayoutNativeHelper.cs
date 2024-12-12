namespace DevExpress.Xpf.Editors.Flyout.Native
{
    using DevExpress.Xpf.Core.HandleDecorator.Helpers;
    using System;

    public static class FlayoutNativeHelper
    {
        public static void SetPopupTransparent(IntPtr hwnd)
        {
            int windowLong = NativeMethods.GetWindowLong(hwnd, -20);
            NativeMethods.SetWindowLong(hwnd, -20, (IntPtr) (windowLong | 0x20));
        }

        public static void UnsetPopupTransparent(IntPtr hwnd)
        {
            int windowLong = NativeMethods.GetWindowLong(hwnd, -20);
            NativeMethods.SetWindowLong(hwnd, -20, (IntPtr) (windowLong ^ 0x20));
        }
    }
}

