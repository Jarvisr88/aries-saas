namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;
    using System.Windows;

    public interface IDropMarkerDisplayer
    {
        void Hide();
        void Show(UIElement adornedElement);
    }
}

