namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Drawing;
    using System.Windows;

    public class VirtualSurfaceImageSourceLikeRenderer : INativeImageRenderer
    {
        private INativeImageRendererCallback callback;

        public void RegisterCallback(INativeImageRendererCallback callback)
        {
            this.callback = callback;
        }

        public void ReleaseCallback()
        {
            this.callback = null;
        }

        public void Render(Graphics graphics, Rect invalidateRect, System.Windows.Size renderSize)
        {
        }
    }
}

