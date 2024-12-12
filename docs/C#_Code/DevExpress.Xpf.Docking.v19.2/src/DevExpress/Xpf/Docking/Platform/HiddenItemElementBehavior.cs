namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class HiddenItemElementBehavior : DockLayoutElementBehavior
    {
        public HiddenItemElementBehavior(HiddenItemElement element) : base(element)
        {
        }

        public override bool CanDrag(OperationType operation) => 
            (operation == OperationType.ClientDragging) && base.Element.Item.AllowRestore;
    }
}

