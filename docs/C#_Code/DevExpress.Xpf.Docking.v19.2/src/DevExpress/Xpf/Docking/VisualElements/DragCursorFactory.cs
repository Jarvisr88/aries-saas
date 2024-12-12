namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Core;
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows;

    public class DragCursorFactory
    {
        public static FloatingContainer CreateDragCursorContainer(DockLayoutManager manager, object content)
        {
            Size size = new Size();
            return CreateDragCursorContainer(manager, content, size);
        }

        public static FloatingContainer CreateDragCursorContainer(DockLayoutManager manager, object content, Size size)
        {
            FloatingContainer container = new DragCursorWindowContainer();
            container.BeginUpdate();
            container.Owner = manager;
            Size size2 = new Size();
            container.FloatSize = (size == size2) ? new Size(150.0, 150.0) : size;
            container.ShowActivated = false;
            container.AllowMoving = false;
            container.AllowSizing = false;
            container.ShowContentOnly = true;
            container.Content = content;
            manager.AddToLogicalTree(container, content);
            container.EndUpdate();
            return container;
        }
    }
}

