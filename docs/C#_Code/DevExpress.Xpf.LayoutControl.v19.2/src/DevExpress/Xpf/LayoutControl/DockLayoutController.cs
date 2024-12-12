namespace DevExpress.Xpf.LayoutControl
{
    using System;

    public class DockLayoutController : LayoutControllerBase
    {
        public DockLayoutController(IDockLayoutControl control) : base(control)
        {
        }

        public override bool IsScrollable() => 
            false;
    }
}

