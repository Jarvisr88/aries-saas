namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Windows.Automation.Peers;

    public class ControlBoxAutomationPeer : BaseAutomationPeer<BaseControlBoxControl>
    {
        public ControlBoxAutomationPeer(BaseControlBoxControl controlBoxControl) : base(controlBoxControl)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Group;
    }
}

