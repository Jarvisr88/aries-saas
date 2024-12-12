namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class DockPaneElementBehavior : DockLayoutElementBehavior
    {
        public DockPaneElementBehavior(DockPaneElement element) : base(element)
        {
        }

        public override bool CanDrag(OperationType operation)
        {
            if (base.Manager != null)
            {
                BaseLayoutItem item = base.Element.Item;
                switch (operation)
                {
                    case OperationType.Floating:
                        return (item.AllowFloat && (!item.IsFloatingRootItem && base.Manager.ShowContentWhenDragging));

                    case OperationType.ClientDragging:
                        return (!item.AllowFloat ? item.AllowMove : !base.Manager.ShowContentWhenDragging);

                    case OperationType.FloatingMoving:
                        return (item.IsFloatingRootItem && base.Manager.ShowContentWhenDragging);
                }
            }
            return false;
        }

        public override bool CanSelect() => 
            base.Element.Item.AllowSelection;
    }
}

