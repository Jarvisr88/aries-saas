namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using System;

    public class SpinEditAutomationPeer : ButtonEditAutomationPeer
    {
        public SpinEditAutomationPeer(SpinEdit element) : base(element)
        {
        }

        protected SpinEdit Editor =>
            base.Editor as SpinEdit;
    }
}

