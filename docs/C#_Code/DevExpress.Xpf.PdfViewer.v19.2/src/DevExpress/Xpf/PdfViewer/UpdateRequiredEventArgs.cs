namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Pdf.Native;
    using DevExpress.Xpf.DocumentViewer;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class UpdateRequiredEventArgs : EventArgs
    {
        public UpdateRequiredEventArgs(PdfTarget target)
        {
            this.ShouldScrollIntoView = true;
            this.ScrollIntoViewTarget = target;
        }

        public UpdateRequiredEventArgs(bool shouldScrollIntoView) : this(shouldScrollIntoView, ScrollIntoViewMode.TopLeft)
        {
        }

        public UpdateRequiredEventArgs(int invalidatePage = -1)
        {
            this.InvalidatePage = invalidatePage;
            this.InvalidateRender = true;
        }

        public UpdateRequiredEventArgs(bool shouldScrollIntoView, ScrollIntoViewMode scrollMode)
        {
            this.ShouldScrollIntoView = shouldScrollIntoView;
            this.ScrollMode = scrollMode;
        }

        public bool ShouldScrollIntoView { get; private set; }

        public ScrollIntoViewMode ScrollMode { get; private set; }

        public bool InvalidateRender { get; private set; }

        public int InvalidatePage { get; private set; }

        public PdfTarget ScrollIntoViewTarget { get; private set; }
    }
}

