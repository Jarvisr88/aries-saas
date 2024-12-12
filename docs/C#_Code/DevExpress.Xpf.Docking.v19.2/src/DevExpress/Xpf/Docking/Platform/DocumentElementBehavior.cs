namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class DocumentElementBehavior : DockLayoutElementBehavior
    {
        public DocumentElementBehavior(DocumentElement element) : base(element)
        {
        }

        public override bool CanDrag(OperationType operation)
        {
            if (base.Manager == null)
            {
                return false;
            }
            BaseLayoutItem item = base.Element.Item;
            return ((operation == OperationType.Floating) ? (item.AllowFloat && (!item.IsFloatingRootItem && base.Manager.ShowContentWhenDragging)) : ((operation == OperationType.ClientDragging) ? (!item.AllowFloat ? item.AllowMove : !base.Manager.ShowContentWhenDragging) : false));
        }
    }
}

