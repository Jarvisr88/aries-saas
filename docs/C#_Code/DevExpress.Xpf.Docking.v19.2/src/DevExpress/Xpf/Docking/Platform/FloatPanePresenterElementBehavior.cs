namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class FloatPanePresenterElementBehavior : DockLayoutElementBehavior
    {
        public FloatPanePresenterElementBehavior(FloatPanePresenterElement element) : base(element)
        {
        }

        public override bool CanDrag(OperationType operation)
        {
            if (base.Manager != null)
            {
                switch (operation)
                {
                    case OperationType.ClientDragging:
                        return (this.AllowDragging && (!((FloatGroup) base.Element.Item).IsMinimized && !base.Manager.ShowContentWhenDragging));

                    case OperationType.FloatingMoving:
                        return (this.AllowDragging && (!((FloatGroup) base.Element.Item).IsMinimized && base.Manager.ShowContentWhenDragging));

                    case OperationType.FloatingResizing:
                        return (this.AllowResizing && !((FloatGroup) base.Element.Item).IsMaximized);
                }
            }
            return false;
        }
    }
}

