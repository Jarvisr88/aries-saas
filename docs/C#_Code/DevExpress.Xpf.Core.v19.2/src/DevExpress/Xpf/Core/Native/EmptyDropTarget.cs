namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class EmptyDropTarget : IDropTarget
    {
        public static IDropTarget Instance;

        static EmptyDropTarget();
        private EmptyDropTarget();
        void IDropTarget.Drop(UIElement source, Point pt);
        void IDropTarget.OnDragLeave();
        void IDropTarget.OnDragOver(UIElement source, Point pt);
    }
}

