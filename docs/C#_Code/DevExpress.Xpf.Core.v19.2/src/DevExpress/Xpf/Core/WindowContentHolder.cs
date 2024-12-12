namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Data;
    using System.Windows.Input;
    using System.Windows.Interop;
    using System.Windows.Media;

    public class WindowContentHolder : Window
    {
        protected WindowInteropHelper interopHelperCore;
        private int lockFloatingBoundsChanging;
        private bool? IsNonWPFHosted;

        public WindowContentHolder(BaseFloatingContainer container)
        {
            this.Container = container;
            if ((container is FloatingContainer) && (((FloatingContainer) container).Caption != null))
            {
                base.Title = ((FloatingContainer) container).Caption;
            }
            base.ShowInTaskbar = false;
            base.AllowsTransparency = true;
            base.WindowStyle = WindowStyle.None;
            base.ResizeMode = ResizeMode.NoResize;
            base.Background = Brushes.Transparent;
            base.WindowStartupLocation = WindowStartupLocation.Manual;
            ThemedWindowHeaderItemsControlBase.SetAllowHeaderItems(this, false);
            base.SetValue(FloatingContainer.IsActiveProperty, true);
            base.SetValue(FloatingContainer.IsMaximizedProperty, false);
            FrameworkElement content = container.Content as FrameworkElement;
            if (content != null)
            {
                DependencyProperty parameter = typeof(KeyboardNavigation).GetField("ShowKeyboardCuesProperty", System.Reflection.BindingFlags.GetField | System.Reflection.BindingFlags.NonPublic | System.Reflection.BindingFlags.Static).GetValue(null) as DependencyProperty;
                Binding binding = new Binding();
                binding.Path = new PropertyPath(parameter);
                binding.Source = this;
                content.SetBinding(parameter, binding);
            }
        }

        protected internal void CheckRelativeLocation()
        {
            FrameworkElement owner = this.Container.Owner;
            if (owner != null)
            {
                if (!ScreenHelper.IsAttachedToPresentationSource(owner))
                {
                    this.UnSubscribe(base.Owner);
                }
                else
                {
                    Point relativeLocation = this.GetRelativeLocation(owner);
                    if (relativeLocation != this.Container.FloatLocation)
                    {
                        this.EnsureRelativeLocation(relativeLocation);
                    }
                }
            }
        }

        internal static Rect CorrectBounds(FrameworkElement owner, Rect bounds, bool useScreenCoordinates)
        {
            if (owner != null)
            {
                try
                {
                    PresentationSource source = PresentationSource.FromDependencyObject(owner);
                    Point location = bounds.Location;
                    if (!useScreenCoordinates && (source != null))
                    {
                        location = owner.PointToScreen(bounds.Location);
                    }
                    bounds = (source == null) ? new Rect(location, bounds.Size) : new Rect(source.CompositionTarget.TransformFromDevice.Transform(location), bounds.Size);
                }
                catch
                {
                }
            }
            return bounds;
        }

        protected void CorrectBoundsCore(FrameworkElement owner, Rect bounds)
        {
            if (!bounds.Size.IsEmpty && ((bounds.Width != 0.0) || (bounds.Height != 0.0)))
            {
                bounds = CorrectBounds(owner, bounds, this.UseScreenCoordinates);
                this.SetBounds(bounds);
            }
        }

        private void EnsureRelativeLocation(Point floatLocation)
        {
            this.lockFloatingBoundsChanging++;
            this.EnsureRelativeLocationCore(floatLocation);
            this.lockFloatingBoundsChanging--;
        }

        protected virtual void EnsureRelativeLocationCore(Point floatLocation)
        {
            this.Container.FloatLocation = floatLocation;
        }

        protected virtual Point GetRelativeLocation(FrameworkElement owner) => 
            (double.IsNaN(base.Left) || double.IsNaN(base.Top)) ? new Point(0.0, 0.0) : owner.PointFromScreen(new Point(base.Left, base.Top));

        [SecuritySafeCritical]
        protected virtual IntPtr HwndSourceHookHandler(IntPtr hwnd, int msg, IntPtr wParam, IntPtr lParam, ref bool handled)
        {
            if ((msg == 0x86) && ((this.Container != null) && (this.Container.GetValue(FrameworkElement.TagProperty) != null)))
            {
                DependencyObject obj2 = this.Container.GetValue(FrameworkElement.TagProperty) as DependencyObject;
                if (obj2 != null)
                {
                    obj2.SetValue(FloatingContainer.IsActiveProperty, ((int) wParam) > 0);
                }
            }
            return IntPtr.Zero;
        }

        protected override void OnClosed(EventArgs e)
        {
            this.UnSubscribe(base.Owner);
            base.OnClosed(e);
        }

        protected override void OnIsKeyboardFocusWithinChanged(DependencyPropertyChangedEventArgs e)
        {
            base.OnIsKeyboardFocusWithinChanged(e);
            if (!this.Container.UseActiveStateOnly)
            {
                base.SetValue(FloatingContainer.IsActiveProperty, e.NewValue);
            }
        }

        protected override void OnSourceInitialized(EventArgs e)
        {
            base.OnSourceInitialized(e);
            this.interopHelperCore ??= new WindowInteropHelper(this);
            if (this.interopHelperCore.Handle != IntPtr.Zero)
            {
                HwndSource.FromHwnd(this.interopHelperCore.Handle).AddHook(new HwndSourceHook(this.HwndSourceHookHandler));
            }
            if (base.WindowStartupLocation != WindowStartupLocation.Manual)
            {
                FrameworkElement owner = this.Container.Owner;
                if (owner != null)
                {
                    Point floatLocation = owner.PointFromScreen(new Point(base.Left, base.Top));
                    this.EnsureRelativeLocation(floatLocation);
                }
            }
        }

        private void OwnerWindow_LocationChanged(object sender, EventArgs e)
        {
            this.TryCheckRelativeLocationAsync(sender);
        }

        private void OwnerWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            this.TryCheckRelativeLocationAsync(sender);
        }

        protected virtual void SetBounds(Rect bounds)
        {
            base.Left = bounds.Left;
            base.Top = bounds.Top;
            base.Width = bounds.Width;
            base.Height = bounds.Height;
        }

        public void SetFloatingBounds(FrameworkElement owner, Rect bounds)
        {
            if (this.lockFloatingBoundsChanging <= 0)
            {
                this.lockFloatingBoundsChanging++;
                try
                {
                    this.TrySetOwner(owner);
                    this.TryCorrectBoundsAsync(owner, bounds);
                }
                finally
                {
                    this.lockFloatingBoundsChanging--;
                }
            }
        }

        internal static bool SetHwndSourceOwner(Window window, FrameworkElement owner)
        {
            HwndSource source = PresentationSource.FromDependencyObject(owner) as HwndSource;
            if (source == null)
            {
                return false;
            }
            new WindowInteropHelper(window).Owner = source.Handle;
            return true;
        }

        public void SetStartupLocation(FrameworkElement owner, WindowStartupLocation location)
        {
            this.TrySetOwner(owner);
            base.WindowStartupLocation = location;
        }

        protected virtual void Subscribe(Window ownerWindow)
        {
            if (ownerWindow != null)
            {
                ownerWindow.LocationChanged += new EventHandler(this.OwnerWindow_LocationChanged);
                ownerWindow.SizeChanged += new SizeChangedEventHandler(this.OwnerWindow_SizeChanged);
            }
        }

        protected virtual void TryCheckRelativeLocationAsync(object sender)
        {
            this.CheckRelativeLocation();
        }

        protected virtual void TryCorrectBoundsAsync(FrameworkElement owner, Rect bounds)
        {
            this.CorrectBoundsCore(owner, bounds);
        }

        private void TrySetOwner(FrameworkElement owner)
        {
            if ((base.Owner == null) && ((owner != null) && (!ReferenceEquals(base.Owner, owner) && ((this.IsNonWPFHosted == null) || !this.IsNonWPFHosted.Value))))
            {
                Window containerWindow = LayoutHelper.FindParentObject<Window>(owner);
                if (containerWindow == null)
                {
                    if ((this.IsNonWPFHosted == null) && SetHwndSourceOwner(this, owner))
                    {
                        this.IsNonWPFHosted = true;
                    }
                }
                else if (containerWindow.IsVisible)
                {
                    this.TrySetOwnerCore(containerWindow);
                    this.Subscribe(base.Owner);
                }
            }
        }

        protected virtual void TrySetOwnerCore(Window containerWindow)
        {
            base.Owner = containerWindow;
        }

        protected virtual void UnSubscribe(Window ownerWindow)
        {
            if (ownerWindow != null)
            {
                ownerWindow.LocationChanged -= new EventHandler(this.OwnerWindow_LocationChanged);
                ownerWindow.SizeChanged -= new SizeChangedEventHandler(this.OwnerWindow_SizeChanged);
            }
        }

        public BaseFloatingContainer Container { get; private set; }

        internal bool UseScreenCoordinates { get; set; }
    }
}

