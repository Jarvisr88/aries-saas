namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Automation.Peers;

    public class CheckEditBoxAutomationPeer : ToggleButtonAutomationPeer
    {
        public CheckEditBoxAutomationPeer(CheckEditBox owner) : base(owner)
        {
        }

        protected override bool IsControlElementCore() => 
            false;
    }
}

