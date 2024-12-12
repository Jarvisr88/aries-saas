namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform.Win32;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;
    using System.Windows.Threading;

    public class AdornerWindow : Window, IDisposable
    {
        private bool isOpenCore;
        private bool isDisposingCore;
        private MatrixTransform transform;
        private bool IsPerMonitorDpiAware = DevExpress.Xpf.Docking.Platform.Win32.DpiHelper.IsPerMonitorDpiAware;
        private Window eventTarget;
        private List<Window> subscriptions;
        private const double Indent = 50.0;

        public AdornerWindow(IAdornerWindowClient client, UIElement container)
        {
            this.Manager = DockLayoutManager.GetDockLayoutManager(container);
            this.Manager.SizeChanged += new SizeChangedEventHandler(this.OnManagerSizeChanged);
            WindowHelper.BindFlowDirection(this, this.Manager);
            this.Client = client;
            base.ShowActivated = false;
            base.AllowsTransparency = true;
            base.ShowInTaskbar = false;
            base.Background = Brushes.Transparent;
            base.Topmost = true;
            base.WindowStartupLocation = WindowStartupLocation.Manual;
            base.WindowStyle = WindowStyle.None;
            base.ResizeMode = ResizeMode.NoResize;
            this.View = GetView(this.Manager, this.Client);
            this.RootElement = new AdornerWindowContent(this.View);
            this.CheckTransform();
            base.Content = this.RootElement;
            this.SubscribeEvents();
            base.Loaded += new RoutedEventHandler(this.OnLoaded);
        }

        private void AdornerWindow_MouseUp(object sender, MouseButtonEventArgs e)
        {
            if (!this.Container.Win32DragService.IsInEvent)
            {
                this.View.OnMouseUp(EventArgsHelper.Convert(this.RootElement, e));
            }
        }

        private void AdornerWindow_PreviewKey(object sender, KeyEventArgs e)
        {
            if (this.Manager != null)
            {
                this.Manager.RaiseEvent(e);
                this.Manager.ProcessKey(e);
            }
        }

        private void AdornerWindow_PreviewMouseDown(object sender, MouseButtonEventArgs e)
        {
            this.View.OnMouseDown(EventArgsHelper.Convert(this.RootElement, e));
        }

        private void CheckTransform()
        {
            FrameworkElement window = DevExpress.Xpf.Docking.WindowServiceHelper.GetWindow(this.Manager);
            if ((window == null) || !this.Manager.IsDescendantOf(window))
            {
                window = LayoutHelper.GetTopLevelVisual(this.Manager);
            }
            if (window != null)
            {
                this.transform = this.Manager.TransformToVisual(window) as MatrixTransform;
                if ((this.transform != null) && !this.transform.Matrix.IsIdentity)
                {
                    Matrix matrix = this.transform.Matrix;
                    this.transform = new MatrixTransform(Math.Abs(matrix.M11), matrix.M12, matrix.M21, Math.Abs(matrix.M22), 0.0, 0.0);
                    if (!ReferenceEquals(this.transform, ((AdornerWindowContent) this.RootElement).LayoutTransform))
                    {
                        ((AdornerWindowContent) this.RootElement).LayoutTransform = this.transform;
                    }
                }
            }
        }

        private bool EnsureUnSubscribed(Window ownerWindow)
        {
            this.subscriptions ??= new List<Window>();
            return !this.subscriptions.Contains(ownerWindow);
        }

        internal double GetAdornerIndentWithoutTransform() => 
            50.0;

        internal double GetAdornerIndentWithTransform()
        {
            double x = 50.0;
            if ((this.transform != null) && !this.transform.Matrix.IsIdentity)
            {
                x = this.transform.Transform(new Point(x, x)).X;
            }
            return x;
        }

        private Window GetEventTarget()
        {
            if (this.eventTarget == null)
            {
                this.eventTarget = this.Client as FloatingPaneWindow;
                this.eventTarget ??= base.Owner;
            }
            return this.eventTarget;
        }

        private static IView GetView(DockLayoutManager manager, IAdornerWindowClient client)
        {
            IView view = client as IView;
            if (view == null)
            {
                FloatingPaneWindow window = client as FloatingPaneWindow;
                if (window != null)
                {
                    view = manager.GetView(window.Container as IUIElement);
                }
            }
            return view;
        }

        private void ModifyWindowStyle()
        {
            IntPtr handle = new WindowInteropHelper(this).Handle;
            NativeHelper.SetWindowLongSafe(handle, -20, NativeHelper.GetWindowLongSafe(handle, -20) | 0x80);
        }

        protected void OnIsOpenChanged()
        {
            if (!this.IsOpen)
            {
                this.UnSubscribeOwnerWindowEvents(this.GetEventTarget());
                base.Topmost = false;
                base.Hide();
            }
            else
            {
                this.UpdateFloatingBounds(this.IsPerMonitorDpiAware);
                base.Topmost = true;
                base.Show();
                this.UpdateFloatingBounds(false);
                this.SubscribeOwnerWindowEvents(this.GetEventTarget());
            }
        }

        private void OnLoaded(object sender, RoutedEventArgs e)
        {
            this.ModifyWindowStyle();
        }

        private void OnManagerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.CheckTransform();
        }

        private void Owner_LocationChanged(object sender, EventArgs e)
        {
            if (!this.Container.IsTransparencyDisabled)
            {
                this.UpdateFloatingBounds(false);
            }
        }

        private void Owner_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            if (!this.Container.IsTransparencyDisabled)
            {
                this.UpdateFloatingBounds(false);
            }
        }

        private void SetPosition(Rect bounds)
        {
            double dpiFactor = 1.0;
            if (this.IsPerMonitorDpiAware)
            {
                dpiFactor = DevExpress.Xpf.Docking.Platform.Win32.DpiHelper.GetDpiFactor((this.Client as Visual) ?? ((this.Client as LayoutView).RootElement as Visual), this);
            }
            base.Left = bounds.Left * dpiFactor;
            base.Top = bounds.Top * dpiFactor;
            base.Width = bounds.Width * dpiFactor;
            base.Height = bounds.Height * dpiFactor;
        }

        private void SubscribeEvents()
        {
            base.PreviewKeyDown += new KeyEventHandler(this.AdornerWindow_PreviewKey);
            base.PreviewKeyUp += new KeyEventHandler(this.AdornerWindow_PreviewKey);
            base.PreviewMouseDown += new MouseButtonEventHandler(this.AdornerWindow_PreviewMouseDown);
            base.MouseUp += new MouseButtonEventHandler(this.AdornerWindow_MouseUp);
        }

        private void SubscribeOwnerWindowEvents(Window ownerWindow)
        {
            if ((ownerWindow != null) && this.EnsureUnSubscribed(ownerWindow))
            {
                this.subscriptions.Add(ownerWindow);
                ownerWindow.SizeChanged += new SizeChangedEventHandler(this.Owner_SizeChanged);
                ownerWindow.LocationChanged += new EventHandler(this.Owner_LocationChanged);
            }
        }

        void IDisposable.Dispose()
        {
            if (!this.isDisposingCore)
            {
                this.isDisposingCore = true;
                this.isOpenCore = false;
                this.UnSubscribeEvents();
                this.UnSubscribeOwnerWindowEvents(this.GetEventTarget());
                base.Close();
                this.Manager.SizeChanged -= new SizeChangedEventHandler(this.OnManagerSizeChanged);
                this.Manager = null;
                this.RootElement = null;
                this.Client = null;
            }
            GC.SuppressFinalize(this);
        }

        private void TrySetOwner()
        {
            if ((base.Owner == null) && (this.Container != null))
            {
                Window window = DevExpress.Xpf.Docking.WindowServiceHelper.GetWindow(this.Container);
                if ((window != null) && window.IsVisible)
                {
                    this.SubscribeOwnerWindowEvents(this.GetEventTarget());
                }
            }
        }

        private void UnSubscribeEvents()
        {
            base.PreviewKeyDown -= new KeyEventHandler(this.AdornerWindow_PreviewKey);
            base.PreviewKeyUp -= new KeyEventHandler(this.AdornerWindow_PreviewKey);
            base.PreviewMouseDown -= new MouseButtonEventHandler(this.AdornerWindow_PreviewMouseDown);
            base.MouseUp -= new MouseButtonEventHandler(this.AdornerWindow_MouseUp);
        }

        private void UnSubscribeOwnerWindowEvents(Window ownerWindow)
        {
            if (ownerWindow != null)
            {
                if (this.subscriptions != null)
                {
                    this.subscriptions.Remove(ownerWindow);
                }
                ownerWindow.SizeChanged -= new SizeChangedEventHandler(this.Owner_SizeChanged);
                ownerWindow.LocationChanged -= new EventHandler(this.Owner_LocationChanged);
            }
        }

        public void UpdateFloatingBounds(bool pending = false)
        {
            this.TrySetOwner();
            Rect bounds = this.Client.Bounds;
            if ((this.transform != null) && !this.transform.Matrix.IsIdentity)
            {
                Point point = this.transform.Transform(new Point(bounds.Width, bounds.Height));
                bounds.Width = point.X;
                bounds.Height = point.Y;
            }
            double adornerIndentWithTransform = this.GetAdornerIndentWithTransform();
            bounds = Rect.Inflate(bounds, adornerIndentWithTransform, adornerIndentWithTransform);
            if (pending)
            {
                base.Dispatcher.BeginInvoke(() => this.SetPosition(bounds), DispatcherPriority.Background, new object[0]);
            }
            else
            {
                this.SetPosition(bounds);
            }
        }

        public IView View { get; private set; }

        public bool IsDisposing =>
            this.isDisposingCore;

        public DockLayoutManager Manager { get; private set; }

        public UIElement RootElement { get; private set; }

        public IAdornerWindowClient Client { get; internal set; }

        public bool IsOpen
        {
            get => 
                this.isOpenCore;
            set
            {
                if (this.IsOpen != value)
                {
                    this.isOpenCore = value;
                    this.OnIsOpenChanged();
                }
            }
        }

        public DockLayoutManager Container =>
            DockLayoutManager.GetDockLayoutManager(this);
    }
}

