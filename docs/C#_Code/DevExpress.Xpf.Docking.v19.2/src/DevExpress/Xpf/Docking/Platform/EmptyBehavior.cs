namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    internal class EmptyBehavior : ILayoutElementBehavior
    {
        public virtual bool CanDrag(OperationType operation) => 
            false;

        public virtual bool CanSelect() => 
            false;

        public virtual bool AllowDragging =>
            false;
    }
}

