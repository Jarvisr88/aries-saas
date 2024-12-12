namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class TabbedLayoutGroupHeaderElementBehavior : DockLayoutElementBehavior
    {
        public TabbedLayoutGroupHeaderElementBehavior(TabbedLayoutGroupHeaderElement element) : base(element)
        {
        }

        public override bool CanDrag(OperationType operation) => 
            (base.Manager != null) ? ((operation == OperationType.Reordering) ? (base.Manager.IsCustomization && base.Element.Item.AllowMove) : ((operation == OperationType.ClientDragging) ? (base.Manager.IsCustomization && base.Element.Item.AllowMove) : false)) : false;

        public override bool CanSelect() => 
            (base.Manager != null) && (base.Manager.AllowSelection && base.Element.Item.AllowSelection);
    }
}

