namespace DevExpress.Xpf.Layout.Core.Dragging
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class FloatingMovingListener : BaseDragListener
    {
        public override bool CanDrag(Point point, ILayoutElement element) => 
            ((element == null) || !IsRoot(element)) ? ((!IsRoot(element.Container) || (element.Container.Items != null)) ? (element.Container.Items.Count == 1) : true) : true;

        public sealed override DevExpress.Xpf.Layout.Core.OperationType OperationType =>
            DevExpress.Xpf.Layout.Core.OperationType.FloatingMoving;
    }
}

