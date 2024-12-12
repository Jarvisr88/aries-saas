namespace DevExpress.Xpf.Layout.Core.Dragging
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public abstract class BaseDragListener : SupportFloatingListener, IDragServiceListener, IUIServiceListener
    {
        protected BaseDragListener()
        {
        }

        public virtual bool CanDrag(Point point, ILayoutElement element) => 
            element != null;

        public virtual bool CanDrop(Point point, ILayoutElement element) => 
            false;

        protected static bool IsRoot(ILayoutElement element) => 
            (element != null) && (element.Parent == null);

        public virtual void OnBegin(Point point, ILayoutElement element)
        {
        }

        public virtual void OnCancel()
        {
        }

        public virtual void OnComplete()
        {
        }

        public virtual void OnDragging(Point point, ILayoutElement element)
        {
        }

        public virtual void OnDrop(Point point, ILayoutElement element)
        {
        }

        public virtual void OnEnter()
        {
        }

        public virtual void OnInitialize(Point point, ILayoutElement element)
        {
        }

        public virtual void OnLeave()
        {
        }

        public object Key =>
            this.OperationType;

        public abstract DevExpress.Xpf.Layout.Core.OperationType OperationType { get; }
    }
}

