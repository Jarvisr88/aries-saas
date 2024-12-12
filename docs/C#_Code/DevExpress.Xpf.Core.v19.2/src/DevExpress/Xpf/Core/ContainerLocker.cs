namespace DevExpress.Xpf.Core
{
    using DevExpress.Mvvm.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    internal class ContainerLocker
    {
        private static object locker = new object();
        private static Dictionary<IntPtr, ContainerLockInfo> lockedWindowsDict = new Dictionary<IntPtr, ContainerLockInfo>();
        private static Dictionary<DependencyObject, ContainerLockInfo> lockedContainerDict = new Dictionary<DependencyObject, ContainerLockInfo>();
        private static Dictionary<WindowContainer, ContainerLockInfo> infosFromContainer = new Dictionary<WindowContainer, ContainerLockInfo>();
        private readonly SplashScreenLock lockMode;

        public ContainerLocker(WindowContainer container, SplashScreenLock lockMode)
        {
            this.Container = container;
            this.lockMode = lockMode;
            if (!this.Container.IsInitialized)
            {
                this.Container.Initialized += new EventHandler(this.OnOwnerInitialized);
            }
            else
            {
                this.Initialize();
            }
        }

        private static void DisableWindow(WindowContainer container, SplashScreenLock lockMode)
        {
            if (lockMode == SplashScreenLock.Full)
            {
                SplashScreenHelper.SetWindowEnabled(container.Handle, false);
            }
            else
            {
                UIElement element = (lockMode == SplashScreenLock.InputOnly) ? container.Window : (container.WindowObject as UIElement);
                if (element != null)
                {
                    element.SetCurrentValue(UIElement.IsHitTestVisibleProperty, false);
                    element.PreviewKeyDown += new KeyEventHandler(ContainerLocker.OnWindowKeyDown);
                }
            }
        }

        private static void EnableWindow(WindowContainer container, SplashScreenLock lockMode)
        {
            if (lockMode == SplashScreenLock.Full)
            {
                SplashScreenHelper.SetWindowEnabled(container.Handle, true);
            }
            else
            {
                UIElement element = (lockMode == SplashScreenLock.InputOnly) ? container.Window : (container.WindowObject as UIElement);
                if (element != null)
                {
                    if (element.Dispatcher.CheckAccess())
                    {
                        element.SetCurrentValue(UIElement.IsHitTestVisibleProperty, true);
                    }
                    element.PreviewKeyDown -= new KeyEventHandler(ContainerLocker.OnWindowKeyDown);
                }
            }
        }

        private static SplashScreenLock GetActualLockMode(WindowContainer container, SplashScreenLock lockMode)
        {
            SplashScreenLock none = SplashScreenLock.None;
            if ((lockMode == SplashScreenLock.Full) || ((lockMode == SplashScreenLock.InputOnly) && (container.Form != null)))
            {
                none = SplashScreenLock.Full;
            }
            else if ((lockMode == SplashScreenLock.LoadingContent) && ReferenceEquals(container.WindowObject, container.Window))
            {
                none = SplashScreenLock.InputOnly;
            }
            else if (((lockMode == SplashScreenLock.InputOnly) && (container.Window != null)) || (lockMode == SplashScreenLock.LoadingContent))
            {
                none = lockMode;
            }
            return none;
        }

        private void Initialize()
        {
            if (this.Container != null)
            {
                SplashScreenHelper.InvokeAsync(this.Container, () => LockContainer(this.Container, this.lockMode), DispatcherPriority.Send, AsyncInvokeMode.AllowSyncInvoke);
            }
        }

        private static void LockContainer(WindowContainer container, SplashScreenLock lockMode)
        {
            if ((container != null) && (container.Handle != IntPtr.Zero))
            {
                lockMode = GetActualLockMode(container, lockMode);
                if (lockMode != SplashScreenLock.None)
                {
                    object locker = ContainerLocker.locker;
                    lock (locker)
                    {
                        ContainerLockInfo info;
                        if (lockMode == SplashScreenLock.LoadingContent)
                        {
                            if (!lockedContainerDict.TryGetValue(container.WindowObject, out info))
                            {
                                lockedContainerDict.Add(container.WindowObject, info = new ContainerLockInfo(0, lockMode));
                            }
                        }
                        else if (!lockedWindowsDict.TryGetValue(container.Handle, out info))
                        {
                            lockedWindowsDict.Add(container.Handle, info = new ContainerLockInfo(0, lockMode));
                        }
                        info.LockCounter++;
                        infosFromContainer.Add(container, info);
                        if (info.LockCounter == 1)
                        {
                            DisableWindow(container, lockMode);
                        }
                    }
                }
            }
        }

        private void OnOwnerInitialized(object sender, EventArgs e)
        {
            ((WindowContainer) sender).Initialized -= new EventHandler(this.OnOwnerInitialized);
            this.Initialize();
        }

        private static void OnWindowKeyDown(object sender, KeyEventArgs e)
        {
            e.Handled = true;
        }

        public void Release(bool activateWindowIfNeeded)
        {
            if (this.Container != null)
            {
                this.Container.Initialized -= new EventHandler(this.OnOwnerInitialized);
                WindowContainer container = this.Container;
                this.Container = null;
                if (this.lockMode != SplashScreenLock.None)
                {
                    if ((container.Form != null) && container.Form.IsDisposed)
                    {
                        UnlockContainer(container);
                    }
                    else
                    {
                        SplashScreenHelper.InvokeAsync(container, delegate {
                            bool flag = activateWindowIfNeeded && !SplashScreenHelper.ApplicationHasActiveWindow();
                            UnlockContainer(container);
                            if (flag)
                            {
                                container.ActivateWindow();
                            }
                            else if (Keyboard.FocusedElement == null)
                            {
                                Action<Window> action = <>c.<>9__10_1;
                                if (<>c.<>9__10_1 == null)
                                {
                                    Action<Window> local1 = <>c.<>9__10_1;
                                    action = <>c.<>9__10_1 = x => x.Focus();
                                }
                                SplashScreenHelper.GetApplicationActiveWindow(false).Do<Window>(action);
                            }
                        }, DispatcherPriority.Render, AsyncInvokeMode.AsyncOnly);
                    }
                }
            }
        }

        private static void UnlockContainer(WindowContainer container)
        {
            if ((container != null) && (container.Handle != IntPtr.Zero))
            {
                object locker = ContainerLocker.locker;
                lock (locker)
                {
                    ContainerLockInfo info;
                    if (infosFromContainer.TryGetValue(container, out info))
                    {
                        infosFromContainer.Remove(container);
                        int num = info.LockCounter - 1;
                        info.LockCounter = num;
                        if (num == 0)
                        {
                            if (info.LockMode == SplashScreenLock.LoadingContent)
                            {
                                lockedContainerDict.Remove(container.WindowObject);
                            }
                            else
                            {
                                lockedWindowsDict.Remove(container.Handle);
                            }
                            EnableWindow(container, info.LockMode);
                        }
                    }
                }
            }
        }

        private WindowContainer Container { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContainerLocker.<>c <>9 = new ContainerLocker.<>c();
            public static Action<Window> <>9__10_1;

            internal void <Release>b__10_1(Window x)
            {
                x.Focus();
            }
        }

        private class ContainerLockInfo
        {
            public ContainerLockInfo(int lockCounter, SplashScreenLock lockMode)
            {
                this.LockCounter = lockCounter;
                this.LockMode = lockMode;
            }

            public int LockCounter { get; set; }

            public SplashScreenLock LockMode { get; private set; }
        }
    }
}

