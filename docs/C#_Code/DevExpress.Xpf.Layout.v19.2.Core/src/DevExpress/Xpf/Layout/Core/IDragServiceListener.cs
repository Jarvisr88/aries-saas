namespace DevExpress.Xpf.Layout.Core
{
    using System;
    using System.Windows;

    public interface IDragServiceListener : IUIServiceListener
    {
        bool CanDrag(Point point, ILayoutElement element);
        bool CanDrop(Point point, ILayoutElement element);
        void OnBegin(Point point, ILayoutElement element);
        void OnCancel();
        void OnComplete();
        void OnDragging(Point point, ILayoutElement element);
        void OnDrop(Point point, ILayoutElement element);
        void OnEnter();
        void OnInitialize(Point point, ILayoutElement element);
        void OnLeave();

        DevExpress.Xpf.Layout.Core.OperationType OperationType { get; }
    }
}

