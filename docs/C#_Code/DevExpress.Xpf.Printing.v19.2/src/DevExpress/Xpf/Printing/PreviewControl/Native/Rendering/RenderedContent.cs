namespace DevExpress.Xpf.Printing.PreviewControl.Native.Rendering
{
    using DevExpress.Xpf.Printing.PreviewControl.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class RenderedContent
    {
        public IEnumerable<RenderItem> RenderedPages { get; set; }

        public DevExpress.Xpf.Printing.PreviewControl.Native.SelectionService SelectionService { get; set; }
    }
}

