namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Bars.Native;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform.Win32;
    using DevExpress.Xpf.Layout.Core.Platform;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class HwndHostEventAccumulator : IHwndHostEventListener
    {
        [ThreadStatic]
        private static HwndHostEventAccumulator instance;
        private bool hasListeners;
        private WeakList<DockLayoutManager> listeners;

        void IHwndHostEventListener.OnMouseDown(DependencyObject hwndHost, MouseButtonEventArgs eventArgs)
        {
            if (!(hwndHost is AutoHideWindowHost))
            {
                this.OnMouseDown(hwndHost, eventArgs);
            }
        }

        private void OnHasListenersChanged()
        {
            if (this.HasListeners)
            {
                HwndHostEventProvider.Register(this);
            }
            else
            {
                HwndHostEventProvider.Unregister(this);
            }
        }

        private void OnMouseDown(DependencyObject hwndHost, MouseButtonEventArgs eventArgs)
        {
            if (this.listeners != null)
            {
                Point pos = NativeHelper.GetMousePositionSafe();
                Predicate<DependencyObject> predicate = <>c.<>9__9_0;
                if (<>c.<>9__9_0 == null)
                {
                    Predicate<DependencyObject> local1 = <>c.<>9__9_0;
                    predicate = <>c.<>9__9_0 = x => (DockLayoutManager.GetLayoutItem(x) != null) && (DockLayoutManager.GetDockLayoutManager(x) != null);
                }
                DependencyObject obj2 = LayoutHelper.FindLayoutOrVisualParentObject(hwndHost, predicate, true, null);
                if (obj2 != null)
                {
                    DockLayoutManager dockLayoutManager = DockLayoutManager.GetDockLayoutManager(obj2);
                    if (this.listeners.Contains(dockLayoutManager))
                    {
                        LayoutView view = dockLayoutManager.GetView(DockLayoutManager.GetLayoutItem(obj2).GetRoot()) as LayoutView;
                        if (view != null)
                        {
                            view.OnMouseEvent(MouseEventType.MouseDown, new DevExpress.Xpf.Layout.Core.Platform.MouseEventArgs(view.RootUIElement.PointFromScreen(pos), EventArgsHelper.GetChangedButtons(eventArgs)));
                        }
                    }
                }
                Dispatcher.CurrentDispatcher.BeginInvoke(delegate {
                    Action<DockLayoutManager> <>9__2;
                    Action<DockLayoutManager> action = <>9__2;
                    if (<>9__2 == null)
                    {
                        Action<DockLayoutManager> local1 = <>9__2;
                        action = <>9__2 = x => x.ForceCollapseByMouseCheck(new Point?(pos));
                    }
                    this.listeners.ForEach<DockLayoutManager>(action);
                }, DispatcherPriority.Input, new object[0]);
            }
        }

        public static void Register(DockLayoutManager listener)
        {
            instance ??= new HwndHostEventAccumulator();
            instance.RegisterCore(listener);
        }

        private void RegisterCore(DockLayoutManager listener)
        {
            this.listeners ??= new WeakList<DockLayoutManager>();
            this.listeners.Add(listener);
            this.HasListeners = this.listeners.Any<DockLayoutManager>();
        }

        public static void Unregister(DockLayoutManager listener)
        {
            if (instance == null)
            {
            }
            else
            {
                instance.UnregisterCore(listener);
            }
        }

        private void UnregisterCore(DockLayoutManager listener)
        {
            this.listeners ??= new WeakList<DockLayoutManager>();
            this.listeners.Remove(listener);
            this.HasListeners = this.listeners.Any<DockLayoutManager>();
        }

        private bool HasListeners
        {
            get => 
                this.hasListeners;
            set
            {
                if (this.hasListeners != value)
                {
                    this.hasListeners = value;
                    this.OnHasListenersChanged();
                }
            }
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly HwndHostEventAccumulator.<>c <>9 = new HwndHostEventAccumulator.<>c();
            public static Predicate<DependencyObject> <>9__9_0;

            internal bool <OnMouseDown>b__9_0(DependencyObject x) => 
                (DockLayoutManager.GetLayoutItem(x) != null) && (DockLayoutManager.GetDockLayoutManager(x) != null);
        }
    }
}

