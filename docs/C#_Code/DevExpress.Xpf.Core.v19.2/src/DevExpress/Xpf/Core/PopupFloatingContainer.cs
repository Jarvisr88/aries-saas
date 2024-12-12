namespace DevExpress.Xpf.Core
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls.Primitives;

    public class PopupFloatingContainer : FloatingContainer
    {
        private int lockFloatingBoundsChanging;

        static PopupFloatingContainer()
        {
            FrameworkElement.DefaultStyleKeyProperty.OverrideMetadata(typeof(PopupFloatingContainer), new FrameworkPropertyMetadata(typeof(PopupFloatingContainer)));
        }

        public PopupFloatingContainer()
        {
            ThemedWindowHeaderItemsControlBase.SetAllowHeaderItems(this, false);
        }

        protected override void AddDecoratorToContentContainer(NonLogicalDecorator decorator)
        {
            this.Popup.Child = decorator;
        }

        protected override void CloseCore()
        {
            base.CloseCore();
            this.Popup = null;
        }

        private Rect CorrectBounds(Rect bounds) => 
            ((base.Owner == null) || (PresentationSource.FromVisual(base.Owner) == null)) ? bounds : new Rect(base.UseScreenCoordinates ? base.Owner.PointFromScreen(bounds.Location) : bounds.Location, bounds.Size);

        public override Point CorrectRightToLeftLocation(Point location) => 
            new Point(location.X + base.FloatSize.Width, location.Y);

        protected override UIElement CreateContentContainer()
        {
            System.Windows.Controls.Primitives.Popup popup1 = new System.Windows.Controls.Primitives.Popup();
            popup1.Placement = PlacementMode.Relative;
            popup1.AllowsTransparency = true;
            popup1.PlacementTarget = base.Owner;
            this.Popup = popup1;
            return this.Popup;
        }

        private Size EnsureAutoSize(Size size)
        {
            if (base.SizeToContent != SizeToContent.Manual)
            {
                Size layoutAutoSize = base.GetLayoutAutoSize();
                if (layoutAutoSize != Size.Empty)
                {
                    double width = layoutAutoSize.Width;
                    double height = layoutAutoSize.Height;
                    double realSize = (this.Popup.Width == 0.0) ? double.NaN : this.Popup.Width;
                    double num4 = (this.Popup.Height == 0.0) ? double.NaN : this.Popup.Height;
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
                        if (base.FloatSize != new Size(0.0, 0.0))
                        {
                            this.UpdateFloatSize(size);
                        }
                        if (realSize != width)
                        {
                            this.Popup.Width = width;
                        }
                        if (num4 != height)
                        {
                            this.Popup.Height = height;
                        }
                    }
                }
            }
            return size;
        }

        protected override FloatingMode GetFloatingMode() => 
            FloatingMode.Popup;

        protected void Hide()
        {
            this.Popup.IsOpen = false;
        }

        protected void OnOpened()
        {
            this.UpdateStartupLocation();
            base.EnsureMinSize();
        }

        private void SetPopupBounds(Rect bounds)
        {
            this.Popup.HorizontalOffset = SystemParameters.MenuDropAlignment ? (bounds.X + bounds.Width) : bounds.X;
            this.Popup.VerticalOffset = bounds.Y;
            this.Popup.Width = bounds.Width;
            this.Popup.Height = bounds.Height;
        }

        protected void Show()
        {
            this.Popup.IsOpen = true;
        }

        protected override void UpdateFloatingBoundsCore(Rect bounds)
        {
            if (this.lockFloatingBoundsChanging <= 0)
            {
                this.lockFloatingBoundsChanging++;
                try
                {
                    bounds = this.CorrectBounds(new Rect(bounds.Location, this.EnsureAutoSize(bounds.Size)));
                    base.ActualSize = bounds.Size;
                    this.SetPopupBounds(bounds);
                }
                finally
                {
                    this.lockFloatingBoundsChanging--;
                }
            }
        }

        private void UpdateFloatLocation(Point floatLocation)
        {
            this.lockFloatingBoundsChanging++;
            try
            {
                base.FloatLocation = floatLocation;
            }
            finally
            {
                this.lockFloatingBoundsChanging--;
            }
        }

        private void UpdateFloatSize(Size floatSize)
        {
            this.lockFloatingBoundsChanging++;
            try
            {
                base.FloatSize = floatSize;
            }
            finally
            {
                this.lockFloatingBoundsChanging--;
            }
        }

        protected override void UpdateIsOpenCore(bool isOpen)
        {
            if (!isOpen)
            {
                this.Hide();
            }
            else
            {
                base.isAutoSizeUpdating++;
                try
                {
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
                    this.UpdateFloatLocation(location);
                }
            }
        }

        public System.Windows.Controls.Primitives.Popup Popup { get; private set; }

        protected override bool IsAlive =>
            this.Popup != null;
    }
}

