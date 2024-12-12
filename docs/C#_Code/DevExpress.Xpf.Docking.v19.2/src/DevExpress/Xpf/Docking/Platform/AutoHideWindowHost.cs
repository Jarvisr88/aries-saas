namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform.Win32;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Content")]
    public class AutoHideWindowHost : HwndHost
    {
        public static readonly DependencyProperty ContentProperty;
        public static readonly DependencyProperty BackgroundProperty;
        private bool isContentRendered;
        private readonly Locker windowPosLocker = new Locker();
        private HwndSource hwndHost;
        private AutoHideWindowRoot _windowRoot;

        static AutoHideWindowHost()
        {
            DependencyPropertyRegistrator<AutoHideWindowHost> registrator = new DependencyPropertyRegistrator<AutoHideWindowHost>();
            registrator.Register<object>("Content", ref ContentProperty, null, (d, e) => ((AutoHideWindowHost) d).OnContentChanged(e.OldValue, e.NewValue), null);
            registrator.Register<Brush>("Background", ref BackgroundProperty, null, (d, e) => ((AutoHideWindowHost) d).OnBackgroundChanged((Brush) e.OldValue, (Brush) e.NewValue), null);
        }

        public AutoHideWindowHost()
        {
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            this.WindowRoot.Arrange(new Rect(finalSize));
            return finalSize;
        }

        protected override HandleRef BuildWindowCore(HandleRef hwndParent)
        {
            HwndSourceParameters parameters = new HwndSourceParameters {
                ParentWindow = hwndParent.Handle,
                WindowStyle = 0x46000000,
                Width = 1,
                Height = 1
            };
            this.hwndHost = new HwndSource(parameters);
            this.hwndHost.RootVisual = this.WindowRoot;
            this.hwndHost.ContentRendered += new EventHandler(this.OnContentRendered);
            if ((this.hwndHost.CompositionTarget != null) && (this.Background is SolidColorBrush))
            {
                this.hwndHost.CompositionTarget.BackgroundColor = ((SolidColorBrush) this.Background).Color;
            }
            return new HandleRef(this, this.hwndHost.Handle);
        }

        protected override void DestroyWindowCore(HandleRef hwnd)
        {
            if (this.hwndHost != null)
            {
                this.hwndHost.Dispose();
                this.hwndHost = null;
            }
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                base.ClearValue(ContentProperty);
            }
            base.Dispose(disposing);
        }

        protected override bool HasFocusWithinCore() => 
            false;

        protected override HitTestResult HitTestCore(PointHitTestParameters hitTestParameters) => 
            this.WindowRoot.Hit(hitTestParameters);

        protected override Size MeasureOverride(Size constraint)
        {
            this.WindowRoot.Measure(constraint);
            return this.WindowRoot.DesiredSize;
        }

        protected virtual void OnBackgroundChanged(Brush oldValue, Brush newValue)
        {
            if (((this.hwndHost != null) && (this.hwndHost.CompositionTarget != null)) && (this.Background is SolidColorBrush))
            {
                this.hwndHost.CompositionTarget.BackgroundColor = ((SolidColorBrush) this.Background).Color;
            }
        }

        protected virtual void OnContentChanged(object oldValue, object newValue)
        {
            if (oldValue is UIElement)
            {
                this.WindowRoot.Child = null;
            }
            if (newValue is UIElement)
            {
                this.WindowRoot.Child = (UIElement) newValue;
                DockLayoutManager.SetDockLayoutManager(this.WindowRoot, DockLayoutManager.GetDockLayoutManager(this));
            }
        }

        private void OnContentRendered(object sender, EventArgs e)
        {
            this.isContentRendered = true;
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            AutoHidePane pane = LayoutHelper.FindParentObject<AutoHidePane>(this);
            if (pane != null)
            {
                this.WindowRoot.Pane = pane;
                bool flag = AutoHideTray.GetOrientation(pane) == Orientation.Vertical;
                BindingHelper.SetBinding(this, flag ? FrameworkElement.WidthProperty : FrameworkElement.HeightProperty, pane, "PanelSize");
            }
        }

        protected override IntPtr WndProc(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if (msg != 70)
            {
                return base.WndProc(hwnd, msg, wParam, lParam, ref handled);
            }
            if (this.isContentRendered && !this.windowPosLocker.IsLocked)
            {
                this.windowPosLocker.Lock();
                try
                {
                    NativeHelper.SetWindowPosSafe(this.hwndHost.Handle, IntPtr.Zero, 0, 0, 0, 0, 3);
                }
                finally
                {
                    this.windowPosLocker.Unlock();
                }
            }
            return IntPtr.Zero;
        }

        private AutoHideWindowRoot WindowRoot
        {
            get
            {
                if (this._windowRoot == null)
                {
                    AutoHideWindowRoot root1 = new AutoHideWindowRoot();
                    root1.Host = this;
                    this._windowRoot = root1;
                    KeyboardNavigation.SetTabNavigation(this._windowRoot, KeyboardNavigationMode.Cycle);
                    base.AddLogicalChild(this._windowRoot);
                }
                return this._windowRoot;
            }
        }

        public object Content
        {
            get => 
                base.GetValue(ContentProperty);
            set => 
                base.SetValue(ContentProperty, value);
        }

        public Brush Background
        {
            get => 
                (Brush) base.GetValue(BackgroundProperty);
            set => 
                base.SetValue(BackgroundProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AutoHideWindowHost.<>c <>9 = new AutoHideWindowHost.<>c();

            internal void <.cctor>b__2_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHideWindowHost) d).OnContentChanged(e.OldValue, e.NewValue);
            }

            internal void <.cctor>b__2_1(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((AutoHideWindowHost) d).OnBackgroundChanged((Brush) e.OldValue, (Brush) e.NewValue);
            }
        }

        public class AutoHideWindowRoot : Decorator
        {
            public HitTestResult Hit(PointHitTestParameters hitTestParameters) => 
                HitTestHelper.GetHitResult(this, hitTestParameters.HitPoint);

            public AutoHidePane Pane { get; set; }

            public AutoHideWindowHost Host { get; set; }
        }
    }
}

