namespace DevExpress.Xpf.Layout.Core.Dragging
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class ClientDraggingListener : BaseDragListener
    {
        public override bool CanDrag(Point point, ILayoutElement element) => 
            (element != null) && (element.Parent != null);

        public sealed override DevExpress.Xpf.Layout.Core.OperationType OperationType =>
            DevExpress.Xpf.Layout.Core.OperationType.ClientDragging;
    }
}

