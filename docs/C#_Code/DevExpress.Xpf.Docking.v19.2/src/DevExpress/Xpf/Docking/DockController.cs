namespace DevExpress.Xpf.Docking
{
    using DevExpress.Xpf.Layout.Core;
    using System;

    public class DockController : DockControllerBase, IDockController2010, IDockController, IActiveItemOwner, IDisposable
    {
        public DockController(DockLayoutManager container) : base(container)
        {
        }

        public bool DockAsDocument(BaseLayoutItem item, BaseLayoutItem target, DockType type) => 
            base.DockControllerImpl.DockAsDocument(item, target, type);
    }
}

