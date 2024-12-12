namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Windows;
    using System.Windows.Input;
    using System.Windows.Media.Animation;

    public class FloatingWindowContainer : FloatingContainer
    {
        internal bool allowProcessClosing = true;
        private int lockFloatingBoundsChanging;

        static FloatingWindowContainer()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingWindowContainer), new FrameworkPropertyMetadata(typeof(FloatingWindowContainer)));
            Thickness defaultValue = new Thickness();
            FloatingContainerControl.ContentOffsetProperty.OverrideMetadata(typeof(FloatingWindowContainer), new FrameworkPropertyMetadata(defaultValue, FrameworkPropertyMetadataOptions.Inherits, (d, e) => ((FloatingWindowContainer) d).OnContentOffsetChanged(e)));
        }

        public override void Activate()
        {
            if (this.Window != null)
            {
                this.Window.Activate();
            }
        }

        protected override void AddDecoratorToContentContainer(NonLogicalDecorator decorator)
        {
            this.Window.Content = decorator;
        }

        protected override void CloseCore()
        {
            base.CloseCore();
            if ((this.Window != null) && !base.IsClosingCanceled)
            {
                this.Window.Closing -= new CancelEventHandler(this.Window_Closing);
                this.Window.KeyDown -= new KeyEventHandler(this.OnWindowKeyDown);
                this.Window.Close();
                this.Window = null;
            }
        }

        protected override UIElement CreateContentContainer()
        {
            this.Window = this.CreateWindow();
            this.Window.Focusable = base.ContainerFocusable;
            this.Window.Closing += new CancelEventHandler(this.Window_Closing);
            this.Window.KeyDown += new KeyEventHandler(this.OnWindowKeyDown);
            this.OnSizeToContentChangedCore(base.SizeToContent);
            return this.Window;
        }

        protected virtual WindowContentHolder CreateWindow() => 
            new WindowContentHolder(this);

        private Size EnsureAutoSize(Size size)
        {
            if (base.SizeToContent != SizeToContent.Manual)
            {
                Size layoutAutoSize = base.GetLayoutAutoSize();
                if (layoutAutoSize != Size.Empty)
                {
                    double width = layoutAutoSize.Width;
                    double height = layoutAutoSize.Height;
                    height = Math.Min(SystemParameters.WorkArea.Height, height);
                    width = Math.Min(SystemParameters.WorkArea.Width, width);
                    if (base.SizeToContent == SizeToContent.Width)
                    {
                        width = base.MeasureAutoSize(size.Width, width, this.Window.Width);
                    }
                    if (base.SizeToContent == SizeToContent.Height)
                    {
                        height = base.MeasureAutoSize(size.Height, height, this.Window.Height);
                    }
                    if (base.SizeToContent == SizeToContent.WidthAndHeight)
                    {
                        width = base.MeasureAutoSize(size.Width, width, this.Window.Width);
                        height = base.MeasureAutoSize(size.Height, height, this.Window.Height);
                    }
                    size = new Size(width, height);
                    if (base.FloatSize != size)
                    {
                        this.lockFloatingBoundsChanging++;
                        if (base.FloatSize != new Size(0.0, 0.0))
                        {
                            base.FloatSize = size;
                        }
                        if (this.Window.Width != width)
                        {
                            this.Window.Width = width;
                        }
                        if (this.Window.Height != height)
                        {
                            this.Window.Height = height;
                        }
                        this.lockFloatingBoundsChanging--;
                    }
                }
            }
            return size;
        }

        protected override FloatingMode GetFloatingMode() => 
            FloatingMode.Window;

        [SecuritySafeCritical]
        private static DevExpress.Xpf.Core.NativeMethods.RECT GetScreenRect(ref Rect wBounds)
        {
            DevExpress.Xpf.Core.NativeMethods.RECT rcWork = new DevExpress.Xpf.Core.NativeMethods.RECT(wBounds);
            IntPtr handle = DevExpress.Xpf.Core.NativeMethods.MonitorFromRect(ref rcWork, 2);
            if (handle != IntPtr.Zero)
            {
                DevExpress.Xpf.Core.NativeMethods.MONITORINFOEX info = new DevExpress.Xpf.Core.NativeMethods.MONITORINFOEX();
                DevExpress.Xpf.Core.NativeMethods.GetMonitorInfo(new HandleRef(null, handle), info);
                rcWork = info.rcWork;
            }
            return rcWork;
        }

        private void OnContentOffsetChanged(DependencyPropertyChangedEventArgs args)
        {
        }

        private void OnOpened()
        {
            this.UpdateFloatingBoundsCore(new Rect(base.FloatLocation, base.FloatSize));
            if ((base.ContainerStartupLocation == WindowStartupLocation.CenterOwner) && (this.Window.Owner != null))
            {
                DevExpress.Xpf.Core.Native.DpiScale dpi = WindowUtility.GetDpi(this.Window);
                Rect rect3 = DpiHelper.DeviceRectToLogical(this.Window.Owner.GetBounds(), dpi.DpiScaleX, dpi.DpiScaleY);
                Rect rect4 = DpiHelper.DeviceRectToLogical(this.Window.GetBounds(), dpi.DpiScaleX, dpi.DpiScaleY);
                double num = rect3.Left + ((rect3.Width - rect4.Width) * 0.5);
                double num2 = rect3.Top + ((rect3.Height - rect4.Height) * 0.5);
                this.Window.Left = num;
                this.Window.Top = num2;
                this.Window.CheckRelativeLocation();
            }
            base.EnsureMinSize();
            if (base.AllowShowAnimations)
            {
                this.Window.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(1.0, new Duration(TimeSpan.FromMilliseconds(150.0))));
            }
        }

        protected override void OnSizeToContentChangedCore(SizeToContent newVal)
        {
            base.OnSizeToContentChangedCore(newVal);
            if ((this.Window != null) && !base.AllowSizing)
            {
                this.Window.SizeToContent = newVal;
            }
        }

        protected virtual void OnWindowKeyDown(object sender, KeyEventArgs e)
        {
            if (base.CloseOnEscape && (e.Key == Key.Escape))
            {
                e.Handled = true;
                base.ProcessHiding();
            }
        }

        protected override Point ScreenToLogical(Point point)
        {
            PresentationSource source = PresentationSource.FromVisual(this.Window);
            return ((source == null) ? point : source.CompositionTarget.TransformFromDevice.Transform(point));
        }

        private bool TryAsyncShowModal()
        {
            bool flag = false;
            if (base.Owner != null)
            {
                flag = (GetFloatingContainer(base.Owner) != null) | base.Owner.GetType().Name.EndsWith("AvalonAdapter");
            }
            if (flag)
            {
                base.Dispatcher.BeginInvoke(delegate {
                    if (!this.Window.IsVisible)
                    {
                        base.isAutoSizeUpdating++;
                        this.Window.IsVisibleChanged += new DependencyPropertyChangedEventHandler(this.Window_IsVisibleChanged);
                        this.Window.ShowDialog();
                    }
                }, new object[0]);
            }
            return flag;
        }

        protected override void UpdateFloatingBoundsCore(Rect bounds)
        {
            if (this.IsAlive && (this.lockFloatingBoundsChanging <= 0))
            {
                this.lockFloatingBoundsChanging++;
                bounds = new Rect(bounds.Location, this.EnsureAutoSize(bounds.Size));
                base.ActualSize = bounds.Size;
                this.Window.UseScreenCoordinates = base.UseScreenCoordinates;
                this.Window.SetFloatingBounds(base.Owner, bounds);
                this.lockFloatingBoundsChanging--;
            }
        }

        protected override void UpdateIsOpenCore(bool isOpen)
        {
            if (!isOpen)
            {
                this.Window.Hide();
            }
            else
            {
                base.isAutoSizeUpdating++;
                bool flag = false;
                try
                {
                    this.UpdateStartupLocation();
                    this.Window.ShowActivated = base.ShowActivated;
                    if (base.AllowShowAnimations)
                    {
                        this.Window.Opacity = 0.0;
                    }
                    if (base.ShowModal && this.TryAsyncShowModal())
                    {
                        flag = true;
                    }
                    else
                    {
                        this.Window.Show();
                    }
                }
                finally
                {
                    if (!flag)
                    {
                        this.OnOpened();
                    }
                    base.isAutoSizeUpdating--;
                }
            }
        }

        protected virtual void UpdateStartupLocation()
        {
            this.Window.Width = base.FloatSize.Width;
            this.Window.Height = base.FloatSize.Height;
            this.Window.SetStartupLocation(base.Owner, base.ContainerStartupLocation);
        }

        private void Window_Closing(object sender, CancelEventArgs e)
        {
            if (this.allowProcessClosing)
            {
                e.Cancel = true;
                base.IsOpen = false;
            }
        }

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            this.Window.IsVisibleChanged -= new DependencyPropertyChangedEventHandler(this.Window_IsVisibleChanged);
            this.OnOpened();
            base.isAutoSizeUpdating--;
        }

        public WindowContentHolder Window { get; private set; }

        protected override bool IsAlive =>
            this.Window != null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FloatingWindowContainer.<>c <>9 = new FloatingWindowContainer.<>c();

            internal void <.cctor>b__0_0(DependencyObject d, DependencyPropertyChangedEventArgs e)
            {
                ((FloatingWindowContainer) d).OnContentOffsetChanged(e);
            }
        }
    }
}

