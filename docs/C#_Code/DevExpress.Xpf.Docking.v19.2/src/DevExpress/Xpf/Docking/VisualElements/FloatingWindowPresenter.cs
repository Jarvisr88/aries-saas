namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.Platform;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media.Animation;

    public class FloatingWindowPresenter : FloatPanePresenter
    {
        private FloatingPaneWindow windowCore;
        private DevExpress.Xpf.Docking.Platform.AdornerWindow adornerWindowCore;
        private bool _isLoading;
        private bool isFloatingWindowLocked;
        private bool isTemplateAssigned;
        private bool lockShow;

        protected override void ActivateContentHolder()
        {
            if ((this.Window != null) && !this.Window.IsActive)
            {
                this.Window.Activate();
            }
        }

        protected override void AddDecoratorToContentContainer(NonLogicalDecorator decorator)
        {
            this.Window.Content = decorator;
        }

        protected virtual DevExpress.Xpf.Docking.Platform.AdornerWindow CreateAdornerWindow(FloatingPaneWindow window) => 
            new DevExpress.Xpf.Docking.Platform.AdornerWindow(window, base.Container);

        protected override UIElement CreateContentContainer()
        {
            this.windowCore = this.CreateWindow();
            FloatGroup layoutItem = DockLayoutManager.GetLayoutItem(this) as FloatGroup;
            if (layoutItem != null)
            {
                this.UpdateFloatingBoundsCore(layoutItem.FloatBounds);
            }
            return this.windowCore;
        }

        protected virtual FloatingPaneWindow CreateWindow() => 
            new FloatingPaneWindow(this);

        protected override void DeactivateContentHolder()
        {
            System.Windows.Window owner = this.Window?.Owner;
            Ref.Dispose<FloatingPaneWindow>(ref this.windowCore);
            this.DisposeAdornerWindow(false);
            if (owner != null)
            {
                Func<FloatGroup, bool> predicate = <>c.<>9__30_0;
                if (<>c.<>9__30_0 == null)
                {
                    Func<FloatGroup, bool> local1 = <>c.<>9__30_0;
                    predicate = <>c.<>9__30_0 = x => x.IsActuallyVisible;
                }
                if (!base.Container.FloatGroups.Any<FloatGroup>(predicate))
                {
                    owner.Activate();
                }
            }
        }

        private void DisposeAdornerWindow(bool force = false)
        {
            if (!this.Win32Compatible | force)
            {
                Ref.Dispose<DevExpress.Xpf.Docking.Platform.AdornerWindow>(ref this.adornerWindowCore);
            }
        }

        internal DevExpress.Xpf.Docking.Platform.AdornerWindow EnsureAdornerWindow()
        {
            if (!this.IsAlive)
            {
                return null;
            }
            if (this.Win32Compatible)
            {
                return this.GetWin32AdornerWindow(this.Window);
            }
            if ((this.adornerWindowCore == null) || this.adornerWindowCore.IsDisposing)
            {
                this.adornerWindowCore = this.CreateAdornerWindow(this.Window);
            }
            return this.adornerWindowCore;
        }

        internal override void EnsureOwnerWindow()
        {
            if ((this.Window != null) && base.IsOpen)
            {
                if (base.Container.OwnsFloatWindows)
                {
                    this.Window.UpdateOwnerWindow();
                }
                else
                {
                    this.Window.Owner = null;
                }
            }
        }

        protected virtual DevExpress.Xpf.Docking.Platform.AdornerWindow GetWin32AdornerWindow(FloatingPaneWindow window) => 
            base.Container.Win32AdornerWindowProvider.GetWindow(window, base.Container);

        private bool isHiddenInitialization() => 
            this.IsAlive && this.Window.hiddenInitialization;

        protected override void OnContainerTemplateChanged(DataTemplate newValue)
        {
            if ((base.Content != null) && (newValue != null))
            {
                base.UpdateContainer(base.Content);
            }
        }

        protected override void OnDisposing()
        {
            this.DisposeAdornerWindow(true);
            Ref.Dispose<FloatingPaneWindow>(ref this.windowCore);
        }

        protected override void OnIsOpenChanged(bool isOpen)
        {
            if (this.Window != null)
            {
                if (isOpen)
                {
                    this.Window.UpdateOwnerWindow();
                }
                else
                {
                    this.isFloatingWindowLocked = true;
                    this.Window.Owner = null;
                }
            }
            base.OnIsOpenChanged(isOpen);
        }

        protected override void OnLoaded(object sender, RoutedEventArgs e)
        {
            this._isLoading = true;
            try
            {
                base.OnLoaded(sender, e);
                this.EnsureAdornerWindow();
                this.isFloatingWindowLocked = false;
            }
            finally
            {
                this._isLoading = false;
            }
        }

        protected override void OnUnloaded(object sender, RoutedEventArgs e)
        {
            this.DisposeAdornerWindow(false);
            base.OnUnloaded(sender, e);
        }

        internal override bool TryGetActualRenderSize(out Size autoSize)
        {
            Size size1;
            FloatingPaneWindow window = this.Window;
            if (window != null)
            {
                size1 = window.TranslateWindowSizeToFloatSize();
            }
            else
            {
                size1 = new Size();
            }
            autoSize = size1;
            return (window != null);
        }

        protected override void UpdateFloatingBoundsCore(Rect bounds)
        {
            if (base.IsOpen && (base.IsLoaded && !this._isLoading))
            {
                this.isFloatingWindowLocked = false;
            }
            if (!this.isFloatingWindowLocked)
            {
                if (this.Window.FlowDirection == FlowDirection.RightToLeft)
                {
                    bounds.X = bounds.Left + bounds.Width;
                }
                this.Window.SetFloatingBounds(base.Container, bounds);
            }
        }

        protected override void UpdateIsOpenCore(bool isOpen)
        {
            if ((!base.IsOpen || ((base.Content != null) && (base.ContainerTemplate != null))) && !this.lockShow)
            {
                if (!isOpen)
                {
                    this.Window.Hide();
                }
                else if (!this.Window.IsVisible)
                {
                    bool allowsTransparency = this.Window.AllowsTransparency;
                    bool flag2 = this.Window.ShowActivated = base.Container.IsFloating || (this.Window.WindowState == WindowState.Maximized);
                    if (allowsTransparency)
                    {
                        this.Window.Opacity = 0.0;
                    }
                    this.Window.UpdateRenderOptions();
                    this.lockShow = true;
                    this.Window.LockHelper.Lock(FloatingWindowLock.LockerKey.FloatingBounds);
                    this.Window.Show();
                    if (this.IsAlive)
                    {
                        this.Window.LockHelper.Unlock(FloatingWindowLock.LockerKey.FloatingBounds);
                        this.lockShow = false;
                        if (flag2)
                        {
                            WindowHelper.BringToFront(this.Window);
                        }
                        if (allowsTransparency)
                        {
                            DoubleAnimation animation = new DoubleAnimation(1.0, new Duration(TimeSpan.FromMilliseconds(100.0)));
                            animation.Freeze();
                            this.Window.BeginAnimation(UIElement.OpacityProperty, animation);
                        }
                    }
                }
            }
        }

        protected override void UpdatePresenterContentTemplate()
        {
            base.UpdatePresenterContentTemplate();
            this.isTemplateAssigned = base.ContainerTemplate != null;
            if (this.isTemplateAssigned && this.isHiddenInitialization())
            {
                base.CheckBoundsInContainer();
                this.Window.hiddenInitialization = false;
            }
        }

        private bool Win32Compatible =>
            base.Container.IsTransparencyDisabled && EnvironmentHelper.IsWinXP;

        public DevExpress.Xpf.Docking.Platform.AdornerWindow AdornerWindow =>
            this.EnsureAdornerWindow();

        protected internal FloatingPaneWindow Window =>
            this.windowCore;

        protected override bool IsAlive =>
            this.windowCore != null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FloatingWindowPresenter.<>c <>9 = new FloatingWindowPresenter.<>c();
            public static Func<FloatGroup, bool> <>9__30_0;

            internal bool <DeactivateContentHolder>b__30_0(FloatGroup x) => 
                x.IsActuallyVisible;
        }
    }
}

