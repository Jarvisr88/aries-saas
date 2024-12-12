namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;

    public class ReplaceAction : InsertAction
    {
        public ReplaceAction();

        [EditorBrowsable(EditorBrowsableState.Never), Browsable(false), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden)]
        public CollectionActionKind Kind { get; set; }
    }
}

