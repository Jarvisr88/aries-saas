namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class LayoutItemDragAndDropControllerBase : DragAndDropController
    {
        public static double DragImageOpacity = 0.7;
        private object _MaxWidthStoredValue;
        private object _MaxHeightStoredValue;
        private object _OpacityStoredValue;

        public LayoutItemDragAndDropControllerBase(DevExpress.Xpf.Core.Controller controller, Point startDragPoint, FrameworkElement dragControl) : base(controller, startDragPoint)
        {
            this.DragControl = dragControl;
            this.DragControlParent = (Panel) this.DragControl.GetParent();
            if (!this.IsDragControlVisible)
            {
                this.DragControlOrigin = PointHelper.Empty;
            }
            else
            {
                this.DragControlOrigin = this.DragControl.GetPosition(this.Controller.Control);
                this.DragControlPlaceHolder = this.CreateDragControlPlaceHolder();
                this.InitDragControlPlaceHolder();
            }
            this.DragControlIndex = (this.DragControlParent != null) ? this.DragControlParent.Children.IndexOf(this.DragControl) : -1;
            if (!PointHelper.IsEmpty(this.DragControlOrigin))
            {
                this.StartDragRelativePoint = new Point((base.StartDragPoint.X - this.DragControlOrigin.X) / this.DragControl.ActualWidth, (base.StartDragPoint.Y - this.DragControlOrigin.Y) / this.DragControl.ActualHeight);
            }
        }

        protected virtual FrameworkElement CreateDragControlPlaceHolder() => 
            new Canvas();

        protected override FrameworkElement CreateDragImage() => 
            this.DragControl;

        protected override void FinalizeDragImage()
        {
            if (this.IsDragControlVisible)
            {
                this.DragControlParent.Children.Remove(this.DragControlPlaceHolder);
            }
            base.DragImage.RestorePropertyValue(FrameworkElement.MaxWidthProperty, this._MaxWidthStoredValue);
            base.DragImage.RestorePropertyValue(FrameworkElement.MaxHeightProperty, this._MaxHeightStoredValue);
            base.DragImage.RestorePropertyValue(UIElement.OpacityProperty, this._OpacityStoredValue);
            base.FinalizeDragImage();
        }

        protected override Point GetDragImageOffset()
        {
            Size dragImageSize = this.DragImageSize;
            return new Point(-Math.Floor((double) (this.StartDragRelativePoint.X * dragImageSize.Width)) - base.DragImage.Margin.Left, -Math.Floor((double) (this.StartDragRelativePoint.Y * dragImageSize.Height)) - base.DragImage.Margin.Top);
        }

        protected virtual void InitDragControlPlaceHolder()
        {
            if (this.DragControlPlaceHolder is Panel)
            {
                ((Panel) this.DragControlPlaceHolder).Background = this.Controller.ILayoutControl.MovingItemPlaceHolderBrush;
            }
            this.Controller.LayoutProvider.CopyLayoutInfo(this.DragControl, this.DragControlPlaceHolder);
        }

        protected override void InitializeDragImage()
        {
            this._MaxWidthStoredValue = base.DragImage.StorePropertyValue(FrameworkElement.MaxWidthProperty);
            this._MaxHeightStoredValue = base.DragImage.StorePropertyValue(FrameworkElement.MaxHeightProperty);
            this._OpacityStoredValue = base.DragImage.StorePropertyValue(UIElement.OpacityProperty);
            base.InitializeDragImage();
            if (double.IsNaN(base.DragImage.Width))
            {
                base.DragImage.MaxWidth = Math.Min(base.DragImage.MaxWidth, this.Controller.IPanel.ContentBounds.Width);
            }
            if (double.IsNaN(base.DragImage.Height))
            {
                base.DragImage.MaxHeight = Math.Min(base.DragImage.MaxHeight, this.Controller.IPanel.ContentBounds.Height);
            }
            base.DragImage.Opacity = DragImageOpacity;
            if (this.IsDragControlVisible)
            {
                this.DragControlParent.Children.Insert(this.DragControlIndex, this.DragControlPlaceHolder);
            }
        }

        public Point StartDragRelativePoint { get; set; }

        protected override bool AllowAutoScrolling =>
            true;

        protected LayoutControllerBase Controller =>
            (LayoutControllerBase) base.Controller;

        protected FrameworkElement DragControl { get; private set; }

        protected int DragControlIndex { get; private set; }

        protected Point DragControlOrigin { get; private set; }

        protected Panel DragControlParent { get; private set; }

        protected FrameworkElement DragControlPlaceHolder { get; private set; }

        protected bool IsDragControlVisible =>
            (this.DragControlParent != null) && this.DragControlParent.GetVisible();

        protected Size DragImageSize
        {
            get
            {
                Size desiredSize = base.DragImage.GetSize();
                if (desiredSize == SizeHelper.Zero)
                {
                    desiredSize = base.DragImage.DesiredSize;
                    SizeHelper.Deflate(ref desiredSize, base.DragImage.Margin);
                }
                return desiredSize;
            }
        }
    }
}

