namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class FlowLayoutController : LayoutControllerBase
    {
        private bool _AllowLayerSizing;

        public FlowLayoutController(IFlowLayoutControl control) : base(control)
        {
        }

        protected override bool CanDragAndDropItem(FrameworkElement item) => 
            base.CanDragAndDropItem(item) && !ReferenceEquals(item, this.ILayoutControl.MaximizedElement);

        protected virtual bool CanResizeLayers() => 
            !double.IsInfinity(this.ILayoutControl.LayerWidth);

        protected override DragAndDropController CreateItemDragAndDropControler(Point startDragPoint, FrameworkElement dragControl) => 
            new FlowLayoutItemDragAndDropController(this, startDragPoint, dragControl);

        protected override bool WantsDragAndDrop(Point p, out DragAndDropController controller)
        {
            if ((this.AllowMaximizedElementMoving && (this.ILayoutControl.MaximizedElement != null)) && ReferenceEquals(this.GetMoveableItem(p), this.ILayoutControl.MaximizedElement))
            {
                controller = new FlowLayoutMaximizedElementDragAndDropController(this, p);
                return true;
            }
            if (this.AllowLayerSizing && this.CanResizeLayers())
            {
                UIElement element = base.IPanel.ChildAt(p, true, false, false);
                if (this.ILayoutControl.IsLayerSeparator(element))
                {
                    controller = new FlowLayoutLayerSizingController(this, p, (LayerSeparator) element);
                    return true;
                }
            }
            return base.WantsDragAndDrop(p, out controller);
        }

        public IFlowLayoutControl ILayoutControl =>
            base.ILayoutControl as IFlowLayoutControl;

        public FlowLayoutProvider LayoutProvider =>
            (FlowLayoutProvider) base.LayoutProvider;

        public bool AllowLayerSizing
        {
            get => 
                this._AllowLayerSizing;
            set
            {
                if (this._AllowLayerSizing != value)
                {
                    this._AllowLayerSizing = value;
                    this.ILayoutControl.OnAllowLayerSizingChanged();
                }
            }
        }

        public bool AllowMaximizedElementMoving { get; set; }
    }
}

