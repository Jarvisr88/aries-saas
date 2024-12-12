namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public class EmptyDropTarget : IDropTarget, IDropTargetFactory
    {
        public static readonly IDropTarget Instance = new EmptyDropTarget();

        private EmptyDropTarget()
        {
        }

        void IDropTarget.Drop(UIElement source, Point pt)
        {
        }

        void IDropTarget.OnDragLeave()
        {
        }

        void IDropTarget.OnDragOver(UIElement source, Point pt)
        {
        }

        IDropTarget IDropTargetFactory.CreateDropTarget(UIElement dropTargetElement) => 
            this;
    }
}

