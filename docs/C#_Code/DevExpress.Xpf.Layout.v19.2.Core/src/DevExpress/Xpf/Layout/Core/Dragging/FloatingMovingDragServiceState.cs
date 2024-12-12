namespace DevExpress.Xpf.Layout.Core.Dragging
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class FloatingMovingDragServiceState : ClientDragServiceStateBase
    {
        public FloatingMovingDragServiceState(IDragService service) : base(service)
        {
        }

        public sealed override DevExpress.Xpf.Layout.Core.OperationType OperationType =>
            DevExpress.Xpf.Layout.Core.OperationType.FloatingMoving;
    }
}

