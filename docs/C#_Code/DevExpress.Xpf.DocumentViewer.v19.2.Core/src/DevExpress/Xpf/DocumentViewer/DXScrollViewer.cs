namespace DevExpress.Xpf.DocumentViewer
{
    using DevExpress.Data.Utils;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Editors.Flyout;
    using DevExpress.Xpf.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;
    using System.Windows.Threading;

    public class DXScrollViewer : ScrollViewer
    {
        public static readonly DependencyProperty CanMouseScrollProperty;
        public static readonly DependencyProperty ShowFlyoutOnScrollProperty;
        private static readonly TimeSpan FlyoutHideInterval = TimeSpan.FromMilliseconds(1000.0);

        static DXScrollViewer()
        {
            Type ownerType = typeof(DXScrollViewer);
            CanMouseScrollProperty = DependencyPropertyManager.Register("CanMouseScroll", typeof(bool), ownerType, new FrameworkPropertyMetadata(true, FrameworkPropertyMetadataOptions.None, (obj, args) => ((DXScrollViewer) obj).OnCanMouseScrollChanged((bool) args.NewValue)));
            ShowFlyoutOnScrollProperty = DependencyPropertyManager.Register("ShowFlyoutOnScroll", typeof(bool), ownerType, new PropertyMetadata(true));
        }

        public DXScrollViewer()
        {
            this.ShowFlyoutLocker = new Locker();
            this.HideFlyoutTimer = new DispatcherTimer();
            Action<DXScrollViewer, object, EventArgs> onEventAction = <>c.<>9__34_0;
            if (<>c.<>9__34_0 == null)
            {
                Action<DXScrollViewer, object, EventArgs> local1 = <>c.<>9__34_0;
                onEventAction = <>c.<>9__34_0 = (scrollviewer, sender, handler) => scrollviewer.OnHideFlyoutTimerTick();
            }
            this.HideFlyoutTimerHandler = new WeakEventHandler<DXScrollViewer, EventArgs, EventHandler>(this, onEventAction, <>c.<>9__34_1 ??= delegate (WeakEventHandler<DXScrollViewer, EventArgs, EventHandler> h, object sender) {
                ((DispatcherTimer) sender).Tick -= h.Handler;
            }, <>c.<>9__34_2 ??= h => new EventHandler(h.OnEvent));
            this.HideFlyoutTimer.Tick += this.HideFlyoutTimerHandler.Handler;
        }

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.UnsubscribeFromEvents();
            this.VerticalScrollBar = (ScrollBar) base.GetTemplateChild("PART_VerticalScrollBar");
            this.FlyoutControl = (DevExpress.Xpf.Editors.Flyout.FlyoutControl) base.GetTemplateChild("PART_FlyoutControl");
            this.SubscribeToEvents();
        }

        protected virtual void OnCanMouseScrollChanged(bool newValue)
        {
        }

        private void OnHideFlyoutTimerTick()
        {
            Action<DevExpress.Xpf.Editors.Flyout.FlyoutControl> action = <>c.<>9__36_0;
            if (<>c.<>9__36_0 == null)
            {
                Action<DevExpress.Xpf.Editors.Flyout.FlyoutControl> local1 = <>c.<>9__36_0;
                action = <>c.<>9__36_0 = x => x.IsOpen = false;
            }
            this.FlyoutControl.Do<DevExpress.Xpf.Editors.Flyout.FlyoutControl>(action);
        }

        protected override void OnMouseLeftButtonDown(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonDown(e);
            if (this.CanMouseScroll)
            {
                this.PreviousPosition = new Point?(e.GetPosition(this));
                base.CaptureMouse();
            }
        }

        protected override void OnMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnMouseLeftButtonUp(e);
            if (this.CanMouseScroll)
            {
                e.Handled = true;
            }
        }

        protected override void OnPreviewMouseLeftButtonUp(MouseButtonEventArgs e)
        {
            base.OnPreviewMouseLeftButtonUp(e);
            this.PreviousPosition = null;
            base.ReleaseMouseCapture();
        }

        protected override void OnPreviewMouseMove(MouseEventArgs e)
        {
            base.OnPreviewMouseMove(e);
            if (this.PreviousPosition != null)
            {
                Point position = e.GetPosition(this);
                double num = position.X - this.PreviousPosition.Value.X;
                double num2 = position.Y - this.PreviousPosition.Value.Y;
                base.ScrollToVerticalOffset(base.VerticalOffset - num2);
                base.ScrollToHorizontalOffset(base.HorizontalOffset - num);
                this.PreviousPosition = new Point?(position);
            }
        }

        private void OnVerticalScrollBarMouseLeftButtonDown(object sender, MouseButtonEventArgs e)
        {
            this.ShowFlyoutLocker.LockOnce();
        }

        private void OnVerticalScrollBarMouseLeftButtonUp(object sender, MouseButtonEventArgs e)
        {
            this.ShowFlyoutLocker.Unlock();
        }

        private void OnVerticalScrollBarValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            if (this.ShowFlyoutOnScroll && this.ShowFlyoutLocker.IsLocked)
            {
                Action<DevExpress.Xpf.Editors.Flyout.FlyoutControl> action = <>c.<>9__39_0;
                if (<>c.<>9__39_0 == null)
                {
                    Action<DevExpress.Xpf.Editors.Flyout.FlyoutControl> local1 = <>c.<>9__39_0;
                    action = <>c.<>9__39_0 = x => x.IsOpen = true;
                }
                this.FlyoutControl.Do<DevExpress.Xpf.Editors.Flyout.FlyoutControl>(action);
                this.HideFlyoutTimer.Stop();
                this.HideFlyoutTimer.Interval = FlyoutHideInterval;
                this.HideFlyoutTimer.Start();
            }
        }

        protected virtual void SubscribeToEvents()
        {
            this.VerticalScrollBar.Do<ScrollBar>(delegate (ScrollBar x) {
                x.ValueChanged += new RoutedPropertyChangedEventHandler<double>(this.OnVerticalScrollBarValueChanged);
            });
            this.VerticalScrollBar.Do<ScrollBar>(delegate (ScrollBar x) {
                x.PreviewMouseLeftButtonDown += new MouseButtonEventHandler(this.OnVerticalScrollBarMouseLeftButtonDown);
            });
            this.VerticalScrollBar.Do<ScrollBar>(delegate (ScrollBar x) {
                x.PreviewMouseLeftButtonUp += new MouseButtonEventHandler(this.OnVerticalScrollBarMouseLeftButtonUp);
            });
        }

        protected virtual void UnsubscribeFromEvents()
        {
            this.VerticalScrollBar.Do<ScrollBar>(delegate (ScrollBar x) {
                x.ValueChanged -= new RoutedPropertyChangedEventHandler<double>(this.OnVerticalScrollBarValueChanged);
            });
            this.VerticalScrollBar.Do<ScrollBar>(delegate (ScrollBar x) {
                x.PreviewMouseLeftButtonDown -= new MouseButtonEventHandler(this.OnVerticalScrollBarMouseLeftButtonDown);
            });
            this.VerticalScrollBar.Do<ScrollBar>(delegate (ScrollBar x) {
                x.PreviewMouseLeftButtonUp -= new MouseButtonEventHandler(this.OnVerticalScrollBarMouseLeftButtonUp);
            });
        }

        private WeakEventHandler<DXScrollViewer, EventArgs, EventHandler> HideFlyoutTimerHandler { get; set; }

        public bool ShowFlyoutOnScroll
        {
            get => 
                (bool) base.GetValue(ShowFlyoutOnScrollProperty);
            set => 
                base.SetValue(ShowFlyoutOnScrollProperty, value);
        }

        public bool CanMouseScroll
        {
            get => 
                (bool) base.GetValue(CanMouseScrollProperty);
            set => 
                base.SetValue(CanMouseScrollProperty, value);
        }

        private Locker ShowFlyoutLocker { get; set; }

        private Point? PreviousPosition { get; set; }

        private DevExpress.Xpf.Editors.Flyout.FlyoutControl FlyoutControl { get; set; }

        private ScrollBar VerticalScrollBar { get; set; }

        private DispatcherTimer HideFlyoutTimer { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXScrollViewer.<>c <>9 = new DXScrollViewer.<>c();
            public static Action<DXScrollViewer, object, EventArgs> <>9__34_0;
            public static Action<WeakEventHandler<DXScrollViewer, EventArgs, EventHandler>, object> <>9__34_1;
            public static Func<WeakEventHandler<DXScrollViewer, EventArgs, EventHandler>, EventHandler> <>9__34_2;
            public static Action<FlyoutControl> <>9__36_0;
            public static Action<FlyoutControl> <>9__39_0;

            internal void <.cctor>b__7_0(DependencyObject obj, DependencyPropertyChangedEventArgs args)
            {
                ((DXScrollViewer) obj).OnCanMouseScrollChanged((bool) args.NewValue);
            }

            internal void <.ctor>b__34_0(DXScrollViewer scrollviewer, object sender, EventArgs handler)
            {
                scrollviewer.OnHideFlyoutTimerTick();
            }

            internal void <.ctor>b__34_1(WeakEventHandler<DXScrollViewer, EventArgs, EventHandler> h, object sender)
            {
                ((DispatcherTimer) sender).Tick -= h.Handler;
            }

            internal EventHandler <.ctor>b__34_2(WeakEventHandler<DXScrollViewer, EventArgs, EventHandler> h) => 
                new EventHandler(h.OnEvent);

            internal void <OnHideFlyoutTimerTick>b__36_0(FlyoutControl x)
            {
                x.IsOpen = false;
            }

            internal void <OnVerticalScrollBarValueChanged>b__39_0(FlyoutControl x)
            {
                x.IsOpen = true;
            }
        }
    }
}

