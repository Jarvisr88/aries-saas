namespace DevExpress.Xpf.Core
{
    using DevExpress.Data;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Input;
    using System.Windows.Media.Animation;
    using System.Windows.Threading;

    public class FloatingAdornerContainer : FloatingContainer
    {
        private bool _CanUseSizingMargin = true;
        private int lockFloatingBoundsChanging;

        static FloatingAdornerContainer()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(FloatingAdornerContainer), new FrameworkPropertyMetadata(typeof(FloatingAdornerContainer)));
        }

        protected virtual bool AcceptAdorner(Adorner adorner) => 
            adorner is DevExpress.Xpf.Core.PlacementAdorner;

        public override void Activate()
        {
            base.SetValue(FloatingContainer.IsActiveProperty, true);
        }

        protected override void AddDecoratorToContentContainer(NonLogicalDecorator decorator)
        {
            this.ContentHolder.Child = decorator;
            this.PlacementAdorner.Register(this.ContentHolder);
            this.PlacementAdorner.SetVisible(this.ContentHolder, true);
        }

        protected override void CalcLeftOffset(double absWChange, ref double dx, ref double sx)
        {
            if (this.InvertLeftAndRightOffsets)
            {
                sx = -absWChange;
            }
            else
            {
                absWChange = CheckWChange(base.FloatLocation.X, absWChange, this.GetWorkingArea(), this.GetSizingMargin());
                dx = absWChange;
                sx = -absWChange;
            }
        }

        protected override void CalcRightOffset(double absWChange, ref double dx, ref double sx)
        {
            if (!this.InvertLeftAndRightOffsets)
            {
                sx = absWChange;
            }
            else
            {
                absWChange = -CheckWChange(base.FloatLocation.X, -absWChange, this.GetWorkingArea(), this.GetSizingMargin());
                dx = absWChange;
                sx = absWChange;
            }
        }

        protected override void CalcTopOffset(double absHChange, ref double dy, ref double sy)
        {
            absHChange = CheckHChange(base.FloatLocation.Y, absHChange, this.GetWorkingArea(), this.GetSizingMargin());
            base.CalcTopOffset(absHChange, ref dy, ref sy);
        }

        protected override void CloseCore()
        {
            base.CloseCore();
            if (!base.IsClosingCanceled)
            {
                this.PlacementAdorner.Unregister(this.ContentHolder);
                this.RemoveDelayedExecute();
                this.ContentHolder = null;
                this.Deactivate();
            }
        }

        protected virtual AdornerContentHolder CreateAdornerContentHolder() => 
            new AdornerContentHolder(this);

        protected override UIElement CreateContentContainer()
        {
            this.PlacementAdorner = this.FindPlacementAdorner(base.Owner);
            if (this.PlacementAdorner == null)
            {
                this.PlacementAdorner = this.CreatePlacementAdorner();
                this.PlacementAdorner.KeyDown += new KeyEventHandler(this.OnPlacementAdornerKeyDown);
                this.PlacementAdorner.PlacementSurface.SizeChanged += new SizeChangedEventHandler(this.OnPlacementAdornerSizeChanged);
            }
            this.ContentHolder = this.CreateAdornerContentHolder();
            this.ContentHolder.Focusable = base.ContainerFocusable;
            return this.ContentHolder;
        }

        protected virtual DevExpress.Xpf.Core.PlacementAdorner CreatePlacementAdorner() => 
            new DevExpress.Xpf.Core.PlacementAdorner(base.Owner);

        protected void Deactivate()
        {
            if (base.DeactivateOnClose && ((this.PlacementAdorner != null) && this.PlacementAdorner.IsActivated))
            {
                this.PlacementAdorner.Deactivate();
            }
        }

        private Size EnsureAutoSize(Size size)
        {
            if (base.SizeToContent != SizeToContent.Manual)
            {
                Size layoutAutoSize = base.GetLayoutAutoSize();
                if (layoutAutoSize != Size.Empty)
                {
                    Rect boundsInContainer = this.PlacementAdorner.GetBoundsInContainer(this.ContentHolder);
                    double width = layoutAutoSize.Width;
                    double height = layoutAutoSize.Height;
                    double realSize = (boundsInContainer.Width == 0.0) ? double.NaN : boundsInContainer.Width;
                    double num4 = (boundsInContainer.Height == 0.0) ? double.NaN : boundsInContainer.Height;
                    if (base.SizeToContent == SizeToContent.Width)
                    {
                        width = base.MeasureAutoSize(size.Width, width, realSize);
                    }
                    if (base.SizeToContent == SizeToContent.Height)
                    {
                        height = base.MeasureAutoSize(size.Height, height, num4);
                    }
                    if (base.SizeToContent == SizeToContent.WidthAndHeight)
                    {
                        width = base.MeasureAutoSize(size.Width, width, realSize);
                        height = base.MeasureAutoSize(size.Height, height, num4);
                    }
                    size = new Size(width, height);
                    if (base.FloatSize != size)
                    {
                        this.lockFloatingBoundsChanging++;
                        if (base.FloatSize != new Size(0.0, 0.0))
                        {
                            base.FloatSize = size;
                        }
                        if (realSize != width)
                        {
                            boundsInContainer.Width = width;
                        }
                        if (num4 != height)
                        {
                            boundsInContainer.Height = height;
                        }
                        this.PlacementAdorner.SetBoundsInContainer(this.ContentHolder, boundsInContainer);
                        this.lockFloatingBoundsChanging--;
                    }
                }
            }
            return size;
        }

        private void EnsureRelativeLocation(Point floatLocation)
        {
            this.lockFloatingBoundsChanging++;
            base.FloatLocation = floatLocation;
            this.lockFloatingBoundsChanging--;
        }

        private DevExpress.Xpf.Core.PlacementAdorner FindPlacementAdorner(UIElement owner)
        {
            AdornerLayer layer = AdornerHelper.FindAdornerLayer(owner);
            Adorner[] array = new Adorner[0];
            if (layer != null)
            {
                array = layer.GetAdorners(owner);
            }
            return ((array != null) ? ((DevExpress.Xpf.Core.PlacementAdorner) Array.Find<Adorner>(array, adorner => this.AcceptAdorner(adorner))) : null);
        }

        protected override FloatingMode GetFloatingMode() => 
            FloatingMode.Adorner;

        private Thickness GetSizingMargin()
        {
            if (this.UseSizingMargin)
            {
                return new Thickness(5.0, 5.0, 20.0, 30.0);
            }
            return new Thickness();
        }

        protected Rect GetWorkingArea() => 
            new Rect(this.PlacementAdorner.PlacementSurface.RenderSize);

        protected void Hide()
        {
            this.PlacementAdorner.SetVisible(this.ContentHolder, false);
        }

        protected override UIElement InitDialogCorrectOwner(UIElement element) => 
            LayoutHelper.GetTopContainerWithAdornerLayer(element);

        public override void OnApplyTemplate()
        {
            base.OnApplyTemplate();
            this.DelayedExecute(() => this.UpdateStartupLocation());
        }

        protected override void OnHided()
        {
            this.Deactivate();
            base.OnHided();
        }

        protected override void OnIsOpenChanged(bool isOpen)
        {
            base.OnIsOpenChanged(isOpen);
            if (this.PlacementAdorner != null)
            {
                this.PlacementAdorner.PlacementSurface.SizeChanged -= new SizeChangedEventHandler(this.OnPlacementAdornerSizeChanged);
                this.PlacementAdorner.KeyDown -= new KeyEventHandler(this.OnPlacementAdornerKeyDown);
                if (isOpen)
                {
                    this.PlacementAdorner.KeyDown += new KeyEventHandler(this.OnPlacementAdornerKeyDown);
                    this.PlacementAdorner.PlacementSurface.SizeChanged += new SizeChangedEventHandler(this.OnPlacementAdornerSizeChanged);
                }
            }
        }

        protected override void OnLocationChanged(Point newLocation)
        {
            base.FloatLocation = CheckLocation(newLocation, this.GetWorkingArea(), this.GetSizingMargin());
        }

        protected void OnOpened()
        {
            this.UpdateStartupLocation();
            if (base.ContainerFocusable)
            {
                this.PostFocus();
            }
            base.EnsureMinSize();
            if (base.AllowShowAnimations)
            {
                this.ContentHolder.BeginAnimation(UIElement.OpacityProperty, new DoubleAnimation(1.0, new Duration(TimeSpan.FromMilliseconds(150.0))));
            }
        }

        protected virtual void OnPlacementAdornerKeyDown(object sender, KeyEventArgs e)
        {
            if (base.CloseOnEscape && (e.Key == Key.Escape))
            {
                e.Handled = true;
                base.ProcessHiding();
            }
        }

        protected virtual void OnPlacementAdornerSizeChanged(object sender, SizeChangedEventArgs e)
        {
            base.FloatLocation = CheckLocation(base.FloatLocation, this.GetWorkingArea(), this.GetSizingMargin());
        }

        protected override void OnSizeToContentChangedCore(SizeToContent newVal)
        {
            base.OnSizeToContentChangedCore(newVal);
            this.DelayedExecute(delegate {
                if (newVal != SizeToContent.Manual)
                {
                    this.isAutoSizeUpdating++;
                }
                this.UpdateStartupLocation();
                if (newVal != SizeToContent.Manual)
                {
                    this.isAutoSizeUpdating--;
                }
            });
        }

        protected void PostFocus()
        {
            if (base.Content is FrameworkElement)
            {
                base.Dispatcher.BeginInvoke(new Action(this.PostFocusCore), DispatcherPriority.Loaded, new object[0]);
            }
        }

        private void PostFocusCore()
        {
            Predicate<FrameworkElement> predicate = <>c.<>9__43_0;
            if (<>c.<>9__43_0 == null)
            {
                Predicate<FrameworkElement> local1 = <>c.<>9__43_0;
                predicate = <>c.<>9__43_0 = e => e.Focusable;
            }
            FrameworkElement elementToFocus = LayoutHelper.FindElement(base.Content as FrameworkElement, predicate);
            if (elementToFocus != null)
            {
                this.PostFocusCore(elementToFocus);
            }
        }

        private void PostFocusCore(FrameworkElement elementToFocus)
        {
            Keyboard.Focus(elementToFocus);
            elementToFocus.Focus();
        }

        protected void Show()
        {
            this.PlacementAdorner.SetVisible(this.ContentHolder, true);
        }

        protected override void UpdateFloatingBoundsCore(Rect bounds)
        {
            if (this.lockFloatingBoundsChanging <= 0)
            {
                this.lockFloatingBoundsChanging++;
                bounds = new Rect(bounds.Location, this.EnsureAutoSize(bounds.Size));
                base.ActualSize = bounds.Size;
                this.PlacementAdorner.SetBoundsInContainer(this.ContentHolder, bounds);
                this.lockFloatingBoundsChanging--;
            }
        }

        protected override void UpdateIsOpenCore(bool isOpen)
        {
            if (isOpen && !this.PlacementAdorner.IsActivated)
            {
                this.PlacementAdorner.Activate();
            }
            if (!isOpen)
            {
                this.Hide();
            }
            else
            {
                base.isAutoSizeUpdating++;
                try
                {
                    if (base.AllowShowAnimations)
                    {
                        this.ContentHolder.Opacity = 0.0;
                    }
                    this.Show();
                }
                finally
                {
                    this.OnOpened();
                    base.isAutoSizeUpdating--;
                }
            }
        }

        protected virtual void UpdateStartupLocation()
        {
            if ((base.ContainerStartupLocation != WindowStartupLocation.Manual) && (base.ContainerStartupLocation != WindowStartupLocation.CenterScreen))
            {
                base.ActualSize = this.EnsureAutoSize(base.FloatSize);
                Point location = new Point((base.Owner.ActualWidth - base.ActualSize.Width) * 0.5, (base.Owner.ActualHeight - base.ActualSize.Height) * 0.5);
                this.UpdateFloatingBoundsCore(new Rect(location, base.FloatSize));
                if (base.FloatLocation != location)
                {
                    this.EnsureRelativeLocation(location);
                }
            }
        }

        protected internal AdornerContentHolder ContentHolder { get; set; }

        protected internal DevExpress.Xpf.Core.PlacementAdorner PlacementAdorner { get; set; }

        public bool InvertLeftAndRightOffsets =>
            (base.FlowDirection == FlowDirection.RightToLeft) && (this.PlacementAdorner.FlowDirection == base.FlowDirection);

        public bool UseSizingMargin
        {
            get => 
                this._CanUseSizingMargin;
            set => 
                this._CanUseSizingMargin = value;
        }

        protected override bool IsAlive =>
            this.ContentHolder != null;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly FloatingAdornerContainer.<>c <>9 = new FloatingAdornerContainer.<>c();
            public static Predicate<FrameworkElement> <>9__43_0;

            internal bool <PostFocusCore>b__43_0(FrameworkElement e) => 
                e.Focusable;
        }
    }
}

