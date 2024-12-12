namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public interface INativeImageRendererCallback
    {
        void Invalidate();
        void Invalidate(Rect region);
        void SetRenderMask(DrawingBrush drawing);
    }
}

