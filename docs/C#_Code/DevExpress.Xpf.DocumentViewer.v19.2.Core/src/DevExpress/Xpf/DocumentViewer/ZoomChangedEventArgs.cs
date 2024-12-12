namespace DevExpress.Xpf.DocumentViewer
{
    using System;
    using System.Runtime.CompilerServices;

    public class ZoomChangedEventArgs : EventArgs
    {
        public ZoomChangedEventArgs(double oldZoomFactor, double zoomFactor, DevExpress.Xpf.DocumentViewer.ZoomMode oldZoomMode, DevExpress.Xpf.DocumentViewer.ZoomMode zoomMode)
        {
            this.OldZoomFactor = oldZoomFactor;
            this.ZoomFactor = zoomFactor;
            this.OldZoomMode = oldZoomMode;
            this.ZoomMode = zoomMode;
        }

        public double OldZoomFactor { get; private set; }

        public double ZoomFactor { get; private set; }

        public DevExpress.Xpf.DocumentViewer.ZoomMode OldZoomMode { get; private set; }

        public DevExpress.Xpf.DocumentViewer.ZoomMode ZoomMode { get; private set; }
    }
}

