namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;

    [Browsable(false), EditorBrowsable(EditorBrowsableState.Never)]
    public class AddBarItemAction : InsertBarItemAction
    {
        public override bool IsEqual(BarManagerControllerActionBase action);

        [Description("Gets the index at which a bar item is added to the BarManager.Items collection. This property is overridden, so the item is always appended at the end of the bar item collection.")]
        public override int ItemIndex { get; set; }
    }
}

