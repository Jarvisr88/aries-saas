namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class TreeItemElementBehavior : DockLayoutElementBehavior
    {
        public TreeItemElementBehavior(TreeItemElement element) : base(element)
        {
        }

        public override bool CanDrag(OperationType operation) => 
            (operation == OperationType.ClientDragging) && base.Element.Item.AllowMove;

        public override bool CanSelect() => 
            base.Element.Item.AllowSelection;
    }
}

