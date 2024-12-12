namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class TileLayoutController : FlowLayoutController
    {
        public TileLayoutController(IFlowLayoutControl control) : base(control)
        {
        }

        protected override DragAndDropController CreateItemDragAndDropControler(Point startDragPoint, FrameworkElement dragControl) => 
            new TileLayoutItemDragAndDropController(this, startDragPoint, dragControl);

        protected override void OnMouseLeftButtonUp(DXMouseButtonEventArgs e)
        {
            bool flag = base.IsMouseLeftButtonDown && !base.IsDragAndDrop;
            base.OnMouseLeftButtonUp(e);
            if (flag)
            {
                this.ILayoutControl.StopGroupHeaderEditing();
            }
        }

        public ITileLayoutControl ILayoutControl =>
            base.ILayoutControl as ITileLayoutControl;
    }
}

