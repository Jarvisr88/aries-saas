namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Runtime.CompilerServices;

    public class DockLayoutElementBehavior : ILayoutElementBehavior
    {
        public DockLayoutElementBehavior(IDockLayoutElement element)
        {
            this.AssertElement(element);
            this.Element = element;
        }

        protected virtual void AssertElement(IDockLayoutElement element)
        {
        }

        public virtual bool CanDrag(OperationType operation) => 
            false;

        public virtual bool CanSelect() => 
            false;

        public IDockLayoutElement Element { get; private set; }

        public virtual bool AllowDragging =>
            this.Element.Item.AllowDrag;

        public virtual bool AllowResizing =>
            this.Element.Item.AllowSizing;

        protected DockLayoutManager Manager =>
            DockLayoutManager.GetDockLayoutManager(this.Element.View);
    }
}

