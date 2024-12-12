namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;

    public class GroupBoxController : ControlControllerBase
    {
        public GroupBoxController(DevExpress.Xpf.LayoutControl.IGroupBox control) : base(control)
        {
        }

        protected override void OnMouseEnter(DXMouseEventArgs e)
        {
            base.OnMouseEnter(e);
            this.IGroupBox.UpdateShadowVisibility();
        }

        protected override void OnMouseLeave(DXMouseEventArgs e)
        {
            base.OnMouseLeave(e);
            this.IGroupBox.UpdateShadowVisibility();
        }

        public DevExpress.Xpf.LayoutControl.IGroupBox IGroupBox =>
            base.IControl as DevExpress.Xpf.LayoutControl.IGroupBox;
    }
}

