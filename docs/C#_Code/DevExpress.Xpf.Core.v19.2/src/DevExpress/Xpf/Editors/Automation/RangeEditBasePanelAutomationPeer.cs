namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Automation.Peers;

    public class RangeEditBasePanelAutomationPeer : FrameworkElementAutomationPeer
    {
        public RangeEditBasePanelAutomationPeer(RangeEditBasePanel owner) : base(owner)
        {
        }

        protected override bool IsControlElementCore() => 
            false;
    }
}

