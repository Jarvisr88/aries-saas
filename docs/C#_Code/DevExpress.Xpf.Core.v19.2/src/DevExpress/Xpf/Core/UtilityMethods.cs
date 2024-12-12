namespace DevExpress.Xpf.Core
{
    using System;
    using System.Security;

    internal static class UtilityMethods
    {
        [SecurityCritical]
        internal static bool ModifyStyle(IntPtr hWnd, int styleToRemove, int styleToAdd)
        {
            int windowLong = NativeMethods.GetWindowLong(hWnd, -16);
            int dwNewLong = (windowLong & ~styleToRemove) | styleToAdd;
            if (dwNewLong == windowLong)
            {
                return false;
            }
            NativeMethods.SetWindowLong(hWnd, -16, dwNewLong);
            return true;
        }
    }
}

