namespace DevExpress.Xpf.Docking.Platform.Shell
{
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Docking.Platform.Win32;
    using System;
    using System.Security;
    using System.Windows;
    using System.Windows.Interop;

    internal static class WindowPlacementHelper
    {
        [SecurityCritical]
        public static WINDOWPLACEMENT GetWindowPlacement(Window window)
        {
            WINDOWPLACEMENT windowPlacement = NativeHelper.GetWindowPlacement(new WindowInteropHelper(window).Handle);
            FloatingPaneWindow window2 = window as FloatingPaneWindow;
            if ((window2 != null) && window2.LockHelper.GetLocker(FloatingWindowLock.LockerKey.ResetMaximized))
            {
                windowPlacement.showCmd = SW.SHOWNORMAL;
            }
            return windowPlacement;
        }
    }
}

