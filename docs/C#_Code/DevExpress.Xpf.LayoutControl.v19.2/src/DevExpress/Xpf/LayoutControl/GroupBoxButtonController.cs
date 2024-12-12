namespace DevExpress.Xpf.LayoutControl
{
    using DevExpress.Xpf.Core;
    using System;

    public class GroupBoxButtonController : DXButtonController
    {
        public GroupBoxButtonController(IControl control) : base(control)
        {
        }

        internal void InvokeClick()
        {
            this.OnClick();
        }
    }
}

