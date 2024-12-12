namespace DevExpress.Mvvm.UI.Native
{
    using System;
    using System.Diagnostics;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Shell;

    public static class TaskbarInfoApplicator
    {
        private const string TaskbarListInterfaceGuid = "56FDF342-FD6D-11d0-958A-006097C9A090";
        private const string TaskbarListObjectGuid = "56FDF344-FD6D-11d0-958A-006097C9A090";
        private static readonly Version osVersion = Environment.OSVersion.Version;

        [Conditional("DEBUG")]
        private static void Assert(Exception exception, string message)
        {
            if (exception != null)
            {
                throw new Exception(message, exception);
            }
        }

        public static void SetTaskbarItemInfo(Window window, TaskbarItemInfo itemInfo)
        {
            if (IsOSWindows7OrNewer && UserIsLoggedIn())
            {
                window.TaskbarItemInfo = itemInfo;
            }
        }

        [SecuritySafeCritical]
        private static bool UserIsLoggedIn()
        {
            try
            {
                ITaskbarList o = (ITaskbarList) Activator.CreateInstance(Type.GetTypeFromCLSID(new Guid("56FDF344-FD6D-11d0-958A-006097C9A090")));
                try
                {
                    o.HrInit();
                }
                catch (NotImplementedException)
                {
                    return false;
                }
                finally
                {
                    Marshal.ReleaseComObject(o);
                }
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private static bool IsOSWindows7OrNewer =>
            osVersion >= new Version(6, 1);

        [ComImport, SecurityCritical, Guid("56FDF342-FD6D-11d0-958A-006097C9A090"), InterfaceType(ComInterfaceType.InterfaceIsIUnknown), SuppressUnmanagedCodeSecurity]
        private interface ITaskbarList
        {
            void HrInit();
            void AddTab(IntPtr hwnd);
            void DeleteTab(IntPtr hwnd);
            void ActivateTab(IntPtr hwnd);
            void SetActiveAlt(IntPtr hwnd);
        }
    }
}

