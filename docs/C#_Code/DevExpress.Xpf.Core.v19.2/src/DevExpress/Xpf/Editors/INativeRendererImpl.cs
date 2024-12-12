namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Drawing;
    using System.Windows;

    public interface INativeRendererImpl : IDisposable
    {
        bool RenderToGraphics(Graphics graphics, INativeImageRenderer renderer, Rect invalidateRect, System.Windows.Size totalSize);
        void Reset();
    }
}

