namespace DevExpress.Xpf.PdfViewer.Internal
{
    using System;

    public class NotRenderedContent : TextureContent
    {
        public NotRenderedContent(double angle, double zoom) : base(zoom, angle, null)
        {
        }

        public override bool Match(double zoomFactor, double angle) => 
            false;
    }
}

