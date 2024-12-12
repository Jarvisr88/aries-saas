namespace DevExpress.Xpf.Printing
{
    using DevExpress.Utils;
    using System;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class ImageInplaceEditorInfo : InplaceEditorInfoBase
    {
        public ImageInplaceEditorInfo(string editorName, ImageEditorOptions options, string editorDisplayName = null) : base(editorName, editorDisplayName)
        {
            Guard.ArgumentNotNull(options, "options");
            this.<Options>k__BackingField = options;
        }

        public ImageEditorOptions Options { get; }

        internal override DevExpress.Xpf.Printing.EditingFieldType EditingFieldType =>
            DevExpress.Xpf.Printing.EditingFieldType.Image;
    }
}

