namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class LayoutControllerBase : ScrollControlController
    {
        public LayoutControllerBase(ILayoutControlBase control) : base(control)
        {
        }

        protected virtual bool CanDragAndDropItem(FrameworkElement item) => 
            true;

        protected virtual bool CanItemDragAndDrop() => 
            this.ILayoutControl.AllowItemMoving;

        protected virtual DragAndDropController CreateItemDragAndDropControler(Point startDragPoint, FrameworkElement dragControl) => 
            null;

        public virtual FrameworkElement GetMoveableItem(Point p) => 
            (FrameworkElement) base.IPanel.ChildAt(p, true, true, false);

        private ScrollParams GetScrollParams(bool horz) => 
            !horz ? (base.VertScrollParams.Enabled ? base.VertScrollParams : base.HorzScrollParams) : (base.HorzScrollParams.Enabled ? base.HorzScrollParams : base.VertScrollParams);

        internal bool ProcessMouseWheelScrolling(bool horz, int delta)
        {
            ScrollParams scrollParams = this.GetScrollParams(horz);
            return base.ProcessMouseWheelScrolling(scrollParams, delta);
        }

        protected override bool WantsDragAndDrop(Point p, out DragAndDropController controller) => 
            !this.WantsItemDragAndDrop(p, point => this.GetMoveableItem(point), out controller) ? base.WantsDragAndDrop(p, out controller) : true;

        protected bool WantsItemDragAndDrop(Point p, Func<Point, FrameworkElement> getItem, out DragAndDropController controller)
        {
            if (this.CanItemDragAndDrop())
            {
                FrameworkElement item = getItem(p);
                if ((item != null) && this.CanDragAndDropItem(item))
                {
                    controller = this.CreateItemDragAndDropControler(p, item);
                    return true;
                }
            }
            controller = null;
            return false;
        }

        public ILayoutControlBase ILayoutControl =>
            base.IControl as ILayoutControlBase;

        public LayoutProviderBase LayoutProvider =>
            this.ILayoutControl.LayoutProvider;

        protected override Rect ScrollableAreaBounds
        {
            get
            {
                Rect scrollableAreaBounds = base.ScrollableAreaBounds;
                this.LayoutProvider.UpdateScrollableAreaBounds(ref scrollableAreaBounds);
                return scrollableAreaBounds;
            }
        }

        protected override bool CanProcessMouseWheel =>
            false;

        internal bool CanScrollDown
        {
            get
            {
                if (!base.Control.IsVisible)
                {
                    return false;
                }
                ScrollParams scrollParams = this.GetScrollParams(false);
                return (scrollParams.Enabled && !(scrollParams.Position == scrollParams.MaxPosition));
            }
        }

        internal bool CanScrollLeft
        {
            get
            {
                if (!base.Control.IsVisible)
                {
                    return false;
                }
                ScrollParams scrollParams = this.GetScrollParams(true);
                return (scrollParams.Enabled && !(scrollParams.Position == scrollParams.Min));
            }
        }

        internal bool CanScrollRight
        {
            get
            {
                if (!base.Control.IsVisible)
                {
                    return false;
                }
                ScrollParams scrollParams = this.GetScrollParams(true);
                return (scrollParams.Enabled && !(scrollParams.Position == scrollParams.MaxPosition));
            }
        }

        internal bool CanScrollUp
        {
            get
            {
                if (!base.Control.IsVisible)
                {
                    return false;
                }
                ScrollParams scrollParams = this.GetScrollParams(false);
                return (scrollParams.Enabled && !(scrollParams.Position == scrollParams.Min));
            }
        }
    }
}

