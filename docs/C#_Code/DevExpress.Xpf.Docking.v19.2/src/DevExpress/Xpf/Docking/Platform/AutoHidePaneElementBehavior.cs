namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class AutoHidePaneElementBehavior : DockLayoutElementBehavior
    {
        public AutoHidePaneElementBehavior(AutoHidePaneElement element) : base(element)
        {
        }

        protected override void AssertElement(IDockLayoutElement element)
        {
        }

        public override bool CanDrag(OperationType operation) => 
            (operation == OperationType.Resizing) && this.AllowResizing;

        public override bool AllowDragging =>
            base.AllowDragging || this.AllowResizing;
    }
}

