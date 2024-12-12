namespace DevExpress.Xpf.PdfViewer.Internal
{
    using System;

    public class NotRenderedStub : RenderedPage
    {
        public NotRenderedStub() : base(-1)
        {
        }

        public override bool Match(double zoomFactor, double angle) => 
            false;
    }
}

