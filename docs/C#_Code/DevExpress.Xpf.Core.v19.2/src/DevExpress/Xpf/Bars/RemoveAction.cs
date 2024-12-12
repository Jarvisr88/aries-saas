namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;

    public class RemoveAction : CollectionAction
    {
        public RemoveAction();

        [EditorBrowsable(EditorBrowsableState.Never), DesignerSerializationVisibility(DesignerSerializationVisibility.Hidden), Browsable(false)]
        public CollectionActionKind Kind { get; set; }
    }
}

