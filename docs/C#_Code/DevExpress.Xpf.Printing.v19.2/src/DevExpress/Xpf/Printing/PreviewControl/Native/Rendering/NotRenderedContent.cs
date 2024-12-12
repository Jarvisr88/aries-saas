namespace DevExpress.Xpf.Printing.PreviewControl.Native.Rendering
{
    using System;

    public class NotRenderedContent : TextureContent
    {
        public NotRenderedContent(double angle, double zoom) : base(zoom, angle, false, null)
        {
        }

        public override bool Match(double zoomFactor, double angle, bool restored) => 
            false;
    }
}

