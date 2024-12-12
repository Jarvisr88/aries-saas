namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    public class DocumentPaneItemElementBehavior : DockLayoutElementBehavior
    {
        public DocumentPaneItemElementBehavior(DocumentPaneItemElement element) : base(element)
        {
        }

        public override bool CanDrag(DevExpress.Xpf.Layout.Core.OperationType operation)
        {
            if (base.Manager != null)
            {
                BaseLayoutItem item = base.Element.Item;
                if (item.Visibility != Visibility.Visible)
                {
                    return false;
                }
                switch (operation)
                {
                    case DevExpress.Xpf.Layout.Core.OperationType.Reordering:
                        return true;

                    case DevExpress.Xpf.Layout.Core.OperationType.Floating:
                        return (item.AllowFloat && base.Manager.ShowContentWhenDragging);

                    case DevExpress.Xpf.Layout.Core.OperationType.ClientDragging:
                        return (!item.AllowFloat ? item.AllowMove : !base.Manager.ShowContentWhenDragging);
                }
            }
            return false;
        }
    }
}

