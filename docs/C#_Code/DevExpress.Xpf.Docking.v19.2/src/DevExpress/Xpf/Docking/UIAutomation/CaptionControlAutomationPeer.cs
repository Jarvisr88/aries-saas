namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Windows.Automation.Peers;

    public class CaptionControlAutomationPeer : BaseAutomationPeer<CaptionControl>
    {
        public CaptionControlAutomationPeer(CaptionControl captionControl) : base(captionControl)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Group;
    }
}

