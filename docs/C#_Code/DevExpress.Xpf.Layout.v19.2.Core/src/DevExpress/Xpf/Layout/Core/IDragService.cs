namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public interface IDragService : IUIService, IDisposable
    {
        void Reset();
        void SetState(DevExpress.Xpf.Layout.Core.OperationType type);

        ILayoutElement DragItem { get; set; }

        IView DragSource { get; set; }

        Point DragOrigin { get; set; }

        bool SuspendBehindDragging { get; set; }

        DevExpress.Xpf.Layout.Core.OperationType OperationType { get; }
    }
}

