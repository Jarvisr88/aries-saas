namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Threading;

    internal class WindowArranger : WindowArrangerBase
    {
        protected internal WindowArranger(WindowArrangerContainer parent, SplashScreenLocation childLocation, SplashScreenArrangeMode arrangeMode) : base(parent, childLocation, arrangeMode)
        {
        }

        private void ChildSizeChanged(object sender, EventArgs e)
        {
            if (!base.SkipArrange && (base.Parent.IsInitialized && ((base.Child.Window != null) && ((base.lastChildPos.Width != base.Child.Window.ActualWidth) || (base.lastChildPos.Height != base.Child.Window.ActualHeight)))))
            {
                if (base.lastParentPos.IsEmpty)
                {
                    SplashScreenHelper.InvokeAsync(base.Parent, () => this.UpdateNextParentRectAndChildPosition(true), DispatcherPriority.Normal, AsyncInvokeMode.AllowSyncInvoke);
                }
                else
                {
                    base.UpdateChildLocation();
                }
            }
        }

        protected override void CompleteInitializationOverride()
        {
            base.CompleteInitializationOverride();
            if (base.Child.Window.Dispatcher.CheckAccess())
            {
                base.UpdateChildLocation();
            }
            SplashScreenHelper.InvokeAsync(base.Parent, () => this.UpdateNextParentRectAndChildPosition(true), DispatcherPriority.Normal, AsyncInvokeMode.AllowSyncInvoke);
        }

        private void OnParentSizeOrPositionChanged(object sender, EventArgs e)
        {
            if (!base.SkipArrange)
            {
                this.UpdateNextParentRectAndChildPosition(false);
            }
        }

        protected override void SubscribeChildEventsOverride()
        {
            base.Child.Window.SizeChanged += new SizeChangedEventHandler(this.ChildSizeChanged);
            base.Child.Window.ContentRendered += new EventHandler(this.ChildSizeChanged);
        }

        protected override void SubscribeParentEventsOverride()
        {
            if (base.childLocation != SplashScreenLocation.CenterScreen)
            {
                if (base.Parent.Window != null)
                {
                    base.Parent.Window.LocationChanged += new EventHandler(this.OnParentSizeOrPositionChanged);
                    base.Parent.Window.SizeChanged += new SizeChangedEventHandler(this.OnParentSizeOrPositionChanged);
                }
                else if (base.Parent.Form != null)
                {
                    base.Parent.Form.LocationChanged += new EventHandler(this.OnParentSizeOrPositionChanged);
                    base.Parent.Form.SizeChanged += new EventHandler(this.OnParentSizeOrPositionChanged);
                }
                if ((base.childLocation == SplashScreenLocation.CenterContainer) && ((base.ParentContainer != null) && !ReferenceEquals(base.Parent.Window, base.ParentContainer)))
                {
                    base.ParentContainer.SizeChanged += new SizeChangedEventHandler(this.OnParentSizeOrPositionChanged);
                    base.ParentContainer.LayoutUpdated += new EventHandler(this.OnParentSizeOrPositionChanged);
                }
            }
        }

        protected override void UnsubscribeChildEventsOverride()
        {
            base.Child.Window.SizeChanged -= new SizeChangedEventHandler(this.ChildSizeChanged);
            base.Child.Window.ContentRendered -= new EventHandler(this.ChildSizeChanged);
        }

        protected override void UnsubscribeParentEventsOverride()
        {
            if (base.childLocation != SplashScreenLocation.CenterScreen)
            {
                if (base.Parent.Window != null)
                {
                    base.Parent.Window.LocationChanged -= new EventHandler(this.OnParentSizeOrPositionChanged);
                    base.Parent.Window.SizeChanged -= new SizeChangedEventHandler(this.OnParentSizeOrPositionChanged);
                }
                else if (base.Parent.Form != null)
                {
                    base.Parent.Form.LocationChanged -= new EventHandler(this.OnParentSizeOrPositionChanged);
                    base.Parent.Form.SizeChanged -= new EventHandler(this.OnParentSizeOrPositionChanged);
                }
                if ((base.childLocation == SplashScreenLocation.CenterContainer) && ((base.ParentContainer != null) && !ReferenceEquals(base.Parent.Window, base.ParentContainer)))
                {
                    try
                    {
                        base.ParentContainer.SizeChanged -= new SizeChangedEventHandler(this.OnParentSizeOrPositionChanged);
                        base.ParentContainer.LayoutUpdated -= new EventHandler(this.OnParentSizeOrPositionChanged);
                    }
                    catch
                    {
                    }
                }
            }
        }

        private void UpdateNextParentRectAndChildPosition(bool skipSizeCheck)
        {
            if (!base.SkipArrange && base.Parent.IsInitialized)
            {
                base.UpdateParentLocation();
                if ((skipSizeCheck || (base.lastParentPos != base.nextParentPos)) && !base.nextParentPos.IsEmpty)
                {
                    SplashScreenHelper.InvokeAsync(base.Child, () => base.UpdateChildLocation(), DispatcherPriority.Normal, AsyncInvokeMode.AllowSyncInvoke);
                }
            }
        }
    }
}

