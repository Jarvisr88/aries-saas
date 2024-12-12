namespace DevExpress.Xpf.Layout.Core.Dragging
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class DragServiceStateFactory : IDragServiceStateFactory
    {
        public virtual IDragServiceState Create(IDragService service, OperationType operationType)
        {
            switch (operationType)
            {
                case OperationType.Regular:
                    return new RegularDragServiceState(service);

                case OperationType.Resizing:
                    return new ResizingDragServiceState(service);

                case OperationType.Reordering:
                    return new ReorderingDragServiceState(service);

                case OperationType.Floating:
                    return new FloatingDragServiceState(service);

                case OperationType.ClientDragging:
                    return new ClientDragServiceState(service);

                case OperationType.FloatingMoving:
                    return new FloatingMovingDragServiceState(service);

                case OperationType.FloatingResizing:
                    return new FloatingResizingDragServiceState(service);

                case OperationType.NonClientDragging:
                    return new NonClientDragServiceState(service);
            }
            return null;
        }
    }
}

