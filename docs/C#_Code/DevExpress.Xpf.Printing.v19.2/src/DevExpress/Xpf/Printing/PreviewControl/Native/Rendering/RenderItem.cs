namespace DevExpress.Xpf.Printing.PreviewControl.Native.Rendering
{
    using DevExpress.Xpf.Printing.PreviewControl;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class RenderItem
    {
        public Rect Rectangle { get; set; }

        public IDocumentPage Page { get; internal set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public DevExpress.Xpf.Printing.PreviewControl.Native.Rendering.TextureType TextureType { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool NeedsInvalidate { get; set; }

        [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
        public bool ForceInvalidate { get; set; }
    }
}

