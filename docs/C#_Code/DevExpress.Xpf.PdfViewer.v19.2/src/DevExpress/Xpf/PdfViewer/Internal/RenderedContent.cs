namespace DevExpress.Xpf.PdfViewer.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows.Media;

    public class RenderedContent
    {
        public IEnumerable<RenderItem> RenderedPages { get; set; }

        public Color SelectionColor { get; set; }

        public bool NeedsRefresh { get; set; }
    }
}

