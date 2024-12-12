namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Drawing;
    using System.Windows;

    public class DirectNativeRendererImpl : NativeRendererImpl
    {
        public override bool RenderToGraphics(Graphics graphics, INativeImageRenderer renderer, Rect invalidateRect, System.Windows.Size totalSize)
        {
            renderer.Render(graphics, invalidateRect, totalSize);
            return true;
        }
    }
}

