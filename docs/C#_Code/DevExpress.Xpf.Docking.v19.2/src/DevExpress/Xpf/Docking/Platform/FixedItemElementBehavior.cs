namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class FixedItemElementBehavior : DockLayoutElementBehavior
    {
        public FixedItemElementBehavior(FixedItemElement element) : base(element)
        {
        }

        public override bool CanDrag(OperationType operation) => 
            (operation == OperationType.ClientDragging) && ((base.Manager != null) && (base.Manager.IsCustomization && base.Element.Item.AllowMove));

        public override bool CanSelect() => 
            (base.Manager != null) && (base.Manager.AllowSelection && base.Element.Item.AllowSelection);
    }
}

