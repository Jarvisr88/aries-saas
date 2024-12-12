namespace DevExpress.Xpf.Core.DragDrop.Native
{
    using System;

    public interface IDragEventListener
    {
        void OnDragEnter(object sender, IDragEventArgs e);
        void OnDragLeave(object sender, IDragEventArgs e);
        void OnDragOver(object sender, IDragEventArgs e);
        void OnDrop(object sender, IDragEventArgs e);
        void OnMouseDown(object sender, IMouseButtonEventArgs e);
        void OnMouseLeave(object sender, IMouseEventArgs e);
        void OnMouseMove(object sender, IMouseEventArgs e);
        void OnMouseUp(object sender, IMouseButtonEventArgs e);
    }
}

