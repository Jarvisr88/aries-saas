namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public interface IDropTarget
    {
        void Drop(UIElement source, Point pt);
        void OnDragLeave();
        void OnDragOver(UIElement source, Point pt);
    }
}

