namespace DevExpress.Xpf.Bars
{
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Browsable(false)]
    public class AddBarItemLinkAction : InsertBarItemLinkAction
    {
        public override bool IsEqual(BarManagerControllerActionBase action);

        [Description("Gets the index at which a bar item link is added to the target object (bar, menu, etc). This property is overridden, so the item link is always appended at the end of the item link collection.")]
        public override int ItemLinkIndex { get; set; }
    }
}

