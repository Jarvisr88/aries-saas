namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Forms;
    using System.Windows.Interop;
    using System.Windows.Threading;

    internal static class SplashScreenHelper
    {
        internal const int WS_EX_TRANSPARENT = 0x20;
        internal const int WS_EX_TOOLWINDOW = 0x80;
        private const int GWL_EXSTYLE = -20;
        private const int GWL_HWNDPARENT = -8;

        public static bool ApplicationHasActiveWindow() => 
            GetApplicationActiveWindow(false) != null;

        [DllImport("user32.dll")]
        private static extern bool EnableWindow(IntPtr hWnd, bool bEnable);
        public static T FindParameter<T>(object parameter, T fallbackValue = null)
        {
            if (parameter is T)
            {
                return (T) parameter;
            }
            if (parameter is object[])
            {
                foreach (object obj2 in (object[]) parameter)
                {
                    if (obj2 is T)
                    {
                        return (T) obj2;
                    }
                }
            }
            return fallbackValue;
        }

        public static IList<T> FindParameters<T>(object parameter)
        {
            if (parameter is T)
            {
                List<T> list1 = new List<T>();
                list1.Add((T) parameter);
                return list1;
            }
            List<T> list = new List<T>();
            if (parameter is object[])
            {
                foreach (object obj2 in (object[]) parameter)
                {
                    if (obj2 is T)
                    {
                        list.Add((T) obj2);
                    }
                }
            }
            return ((list.Count > 0) ? list : null);
        }

        public static Window GetApplicationActiveWindow(bool mainWindowIfNull)
        {
            Window window2;
            if ((System.Windows.Application.Current == null) || !System.Windows.Application.Current.Dispatcher.CheckAccess())
            {
                return null;
            }
            using (IEnumerator enumerator = System.Windows.Application.Current.Windows.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        Window current = (Window) enumerator.Current;
                        if (!current.Dispatcher.CheckAccess() || !current.IsActive)
                        {
                            continue;
                        }
                        window2 = current;
                    }
                    else
                    {
                        if (!mainWindowIfNull)
                        {
                            return null;
                        }
                        Func<System.Windows.Application, Window> evaluator = <>c.<>9__15_0;
                        if (<>c.<>9__15_0 == null)
                        {
                            Func<System.Windows.Application, Window> local1 = <>c.<>9__15_0;
                            evaluator = <>c.<>9__15_0 = x => x.MainWindow;
                        }
                        return System.Windows.Application.Current.Return<System.Windows.Application, Window>(evaluator, null);
                    }
                    break;
                }
            }
            return window2;
        }

        private static int GetToolWindowStyle(Window window) => 
            (DXSplashScreen.UseDefaultAltTabBehavior || ((window.WindowStyle != WindowStyle.None) || !(window is DXSplashScreen.SplashScreenWindow))) ? 0 : 0x80;

        [DllImport("user32.dll", SetLastError=true)]
        private static extern int GetWindowLong(IntPtr hWnd, int nIndex);
        internal static bool HasAccess(Window window) => 
            !DXSplashScreen.DisableThreadingProblemsDetection || ((window != null) && ((window.Dispatcher != null) && window.Dispatcher.CheckAccess()));

        public static void InvokeAsync(Control dispatcherObject, Action action, AsyncInvokeMode mode = 1)
        {
            if ((dispatcherObject != null) && !dispatcherObject.IsDisposed)
            {
                if ((mode == AsyncInvokeMode.AllowSyncInvoke) && !dispatcherObject.InvokeRequired)
                {
                    action();
                }
                else
                {
                    dispatcherObject.BeginInvoke(action);
                }
            }
        }

        public static void InvokeAsync(WindowContainer container, Action action, DispatcherPriority priority = 9, AsyncInvokeMode mode = 1)
        {
            if ((container != null) && container.IsInitialized)
            {
                if (container.Window != null)
                {
                    InvokeAsync(container.Window, action, priority, mode);
                }
                else
                {
                    InvokeAsync(container.Form, action, mode);
                }
            }
        }

        public static void InvokeAsync(DispatcherObject dispatcherObject, Action action, DispatcherPriority priority = 9, AsyncInvokeMode mode = 1)
        {
            if ((dispatcherObject != null) && (dispatcherObject.Dispatcher != null))
            {
                if ((mode == AsyncInvokeMode.AllowSyncInvoke) && dispatcherObject.Dispatcher.CheckAccess())
                {
                    action();
                }
                else
                {
                    dispatcherObject.Dispatcher.BeginInvoke(action, priority, new object[0]);
                }
            }
        }

        [SecuritySafeCritical]
        public static bool PatchWindowStyle(Window window, bool hasOwner)
        {
            if (!HasAccess(window))
            {
                return false;
            }
            WindowInteropHelper helper = new WindowInteropHelper(window);
            if (helper.Handle == IntPtr.Zero)
            {
                return false;
            }
            int windowLong = GetWindowLong(helper.Handle, -20);
            SetWindowLong(helper.Handle, -20, (int) (windowLong | (hasOwner ? 0x20 : GetToolWindowStyle(window))));
            return true;
        }

        [SecuritySafeCritical]
        public static void SetParent(Window window, IntPtr parentHandle)
        {
            if (HasAccess(window))
            {
                if (window.IsVisible)
                {
                    SetWindowLong(new WindowInteropHelper(window).Handle, -8, parentHandle);
                }
                else
                {
                    new WindowInteropHelper(window).Owner = parentHandle;
                }
            }
        }

        [SecuritySafeCritical]
        internal static void SetWindowEnabled(IntPtr windowHandle, bool isEnabled)
        {
            if (windowHandle != IntPtr.Zero)
            {
                EnableWindow(windowHandle, isEnabled);
            }
        }

        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, int dwNewLong);
        [DllImport("user32.dll")]
        private static extern int SetWindowLong(IntPtr hWnd, int nIndex, IntPtr dwNewLong);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly SplashScreenHelper.<>c <>9 = new SplashScreenHelper.<>c();
            public static Func<System.Windows.Application, Window> <>9__15_0;

            internal Window <GetApplicationActiveWindow>b__15_0(System.Windows.Application x) => 
                x.MainWindow;
        }
    }
}

