namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Drawing;
    using System.Windows;

    public interface INativeImageRenderer
    {
        void RegisterCallback(INativeImageRendererCallback callback);
        void ReleaseCallback();
        void Render(Graphics graphics, Rect invalidateRect, System.Windows.Size renderSize);
    }
}

