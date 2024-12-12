namespace DevExpress.Xpf.Utils.Themes
{
    using DevExpress.Data.Helpers;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Threading;
    using System.Windows;
    using System.Windows.Threading;

    public class GlobalThemeHelper : DependencyObject
    {
        private static GlobalThemeHelper instance = new GlobalThemeHelper();
        private string applicationThemeName;
        private static readonly Func<Application, Uri> GetStartupUriFunc = ReflectionHelper.CreateFieldGetter<Application, Uri>(typeof(Application), "_startupUri", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly Func<Application, Window> GetMainWindowFunc = ReflectionHelper.CreateFieldGetter<Application, Window>(typeof(Application), "_mainWindow", BindingFlags.NonPublic | BindingFlags.Instance);
        private static readonly Func<Dispatcher, int> GetDisableProcessingCountFromDispatcherFunc = ReflectionHelper.CreateFieldGetter<Dispatcher, int>(typeof(Dispatcher), "_disableProcessingCount", BindingFlags.NonPublic | BindingFlags.Instance);
        private readonly object locker = new object();

        protected GlobalThemeHelper()
        {
        }

        public void AssignApplicationThemeName(FrameworkElement window)
        {
            if (DependencyPropertyHelper.GetValueSource(window, DevExpress.Xpf.Core.ThemeManager.ThemeNameProperty).BaseValueSource == BaseValueSource.Default)
            {
                this.SetGlobalTheme(window);
            }
        }

        private bool CheckThreadExecutable(ThreadState state) => 
            ((state & ThreadState.Running) != ThreadState.Running) || ((state & ThreadState.Background) != ThreadState.Running);

        private bool CheckThreadInDirectAccess(ThreadState state) => 
            (state & ThreadState.WaitSleepJoin) == ThreadState.Running;

        private void EnumerateSafe(WindowCollection windows, Action<Window> action)
        {
            int num = 0;
            while (true)
            {
                Window window;
                while (true)
                {
                    if (num < windows.Count)
                    {
                        window = null;
                        try
                        {
                            window = windows[num];
                            break;
                        }
                        catch (ArgumentOutOfRangeException)
                        {
                        }
                        return;
                    }
                    else
                    {
                        return;
                    }
                    break;
                }
                action(window);
                num++;
            }
        }

        [MethodImpl(MethodImplOptions.NoInlining)]
        private static void InitializeWindowTracker()
        {
            WindowTracker.Initialize();
        }

        private static bool IsDispatcherLocked(Dispatcher dispatcher) => 
            dispatcher.HasShutdownStarted || (dispatcher.HasShutdownFinished || (GetDisableProcessingCountFromDispatcherFunc(dispatcher) > 0));

        private void SetApplicationThemeName(string value)
        {
            if (this.applicationThemeName != value)
            {
                this.applicationThemeName = value;
                InitializeWindowTracker();
                this.SetWindowsApplicationThemeNameInThread();
            }
        }

        private void SetApplicationWindows()
        {
            if (Application.Current != null)
            {
                this.EnumerateSafe(Application.Current.Windows, new Action<Window>(this.AssignApplicationThemeName));
            }
        }

        private void SetGlobalTheme(FrameworkElement window)
        {
            if (string.IsNullOrEmpty(this.ApplicationThemeName))
            {
                this.ThemeManager.ClearThemeName(window);
            }
            else
            {
                this.ThemeManager.SetThemeName(window, this.ApplicationThemeName);
            }
        }

        private void SetNonApplicationWindows()
        {
            if (!SecurityHelper.IsPartialTrust && (Application.Current != null))
            {
                this.EnumerateSafe((WindowCollection) ReflectorHelper.GetValue(Application.Current, "NonAppWindowsInternal", BindingFlags.NonPublic | BindingFlags.Instance), delegate (Window window) {
                    ThreadState threadState = window.Dispatcher.Thread.ThreadState;
                    if (this.CheckThreadExecutable(threadState))
                    {
                        if (this.CheckThreadInDirectAccess(threadState))
                        {
                            object[] args = new object[] { window };
                            window.Dispatcher.Invoke(new Action<Window>(this.AssignApplicationThemeName), args);
                        }
                        else
                        {
                            object[] args = new object[] { window };
                            window.Dispatcher.BeginInvoke(new Action<Window>(this.AssignApplicationThemeName), args);
                        }
                    }
                });
            }
        }

        private void SetWindowsApplicationThemeName()
        {
            object locker = this.locker;
            lock (locker)
            {
                this.SetApplicationWindows();
                this.SetNonApplicationWindows();
            }
        }

        internal void SetWindowsApplicationThemeNameInThread()
        {
            if (Application.Current != null)
            {
                Dispatcher dispatcher = Application.Current.Dispatcher;
                if ((IsAppInitialized && !IsDispatcherLocked(dispatcher)) && dispatcher.CheckAccess())
                {
                    dispatcher.Invoke(new Action(this.SetWindowsApplicationThemeName));
                }
                else
                {
                    dispatcher.BeginInvoke(new Action(this.SetWindowsApplicationThemeName), new object[0]);
                }
            }
        }

        public string ApplicationThemeName
        {
            get => 
                this.applicationThemeName;
            set => 
                this.SetApplicationThemeName(value);
        }

        public static GlobalThemeHelper Instance
        {
            get
            {
                instance ??= new GlobalThemeHelper();
                return instance;
            }
        }

        private IThemeManager ThemeManager =>
            DevExpress.Xpf.Core.ThemeManager.Instance;

        internal static bool IsAppInitialized =>
            (Application.Current != null) && ((GetStartupUriFunc(Application.Current) != null) || (GetMainWindowFunc(Application.Current) != null));
    }
}

