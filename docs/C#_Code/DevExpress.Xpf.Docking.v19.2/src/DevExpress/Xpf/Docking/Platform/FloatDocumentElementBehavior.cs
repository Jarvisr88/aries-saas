namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class FloatDocumentElementBehavior : DockLayoutElementBehavior
    {
        public FloatDocumentElementBehavior(FloatDocumentElement element) : base(element)
        {
        }

        public override bool CanDrag(OperationType operation)
        {
            if (base.Manager != null)
            {
                switch (operation)
                {
                    case OperationType.ClientDragging:
                        return (this.AllowDragging && !base.Manager.ShowContentWhenDragging);

                    case OperationType.FloatingMoving:
                        return (this.AllowDragging && base.Manager.ShowContentWhenDragging);

                    case OperationType.FloatingResizing:
                    {
                        FloatGroup parent = base.Element.Item.Parent as FloatGroup;
                        return (this.AllowResizing && ((parent != null) && !parent.IsMaximized));
                    }
                }
            }
            return false;
        }

        public override bool CanSelect() => 
            base.Element.Item.AllowSelection;
    }
}

