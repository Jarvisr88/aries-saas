namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class AutoHidePaneHeaderItemElementBehavior : DockLayoutElementBehavior
    {
        public AutoHidePaneHeaderItemElementBehavior(AutoHidePaneHeaderItemElement element) : base(element)
        {
        }

        public override bool CanDrag(OperationType operation)
        {
            if (base.Manager != null)
            {
                BaseLayoutItem item = base.Element.Item;
                switch (operation)
                {
                    case OperationType.Reordering:
                        return true;

                    case OperationType.Floating:
                        return (item.AllowFloat && base.Manager.ShowContentWhenDragging);

                    case OperationType.ClientDragging:
                        return (!item.AllowFloat ? item.AllowMove : !base.Manager.ShowContentWhenDragging);
                }
            }
            return false;
        }
    }
}

