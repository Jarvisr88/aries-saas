namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Automation.Peers;

    public class ProgressBarEditAutomationPeer : RangeBaseEditAutomationPeer
    {
        public ProgressBarEditAutomationPeer(ProgressBarEdit element) : base(element)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.ProgressBar;

        protected ProgressBarEdit Editor =>
            base.Editor as ProgressBarEdit;
    }
}

