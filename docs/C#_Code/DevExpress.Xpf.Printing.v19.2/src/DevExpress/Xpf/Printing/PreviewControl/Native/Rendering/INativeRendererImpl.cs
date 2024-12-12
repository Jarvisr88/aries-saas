namespace DevExpress.Xpf.Printing.PreviewControl.Native.Rendering
{
    using System;
    using System.Drawing;

    public interface INativeRendererImpl : IDisposable
    {
        bool RenderToGraphics(Graphics graphics, RenderedContent renderedContent, double zoomFactor, double scaleX, double angle);
    }
}

