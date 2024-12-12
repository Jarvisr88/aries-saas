namespace DevExpress.Xpf.LayoutControl.UIAutomation
{
    using DevExpress.Xpf.LayoutControl;
    using System;
    using System.Windows.Automation.Peers;

    public class LayoutItemAutomationPeer : BaseLayoutAutomationPeer<LayoutItem>
    {
        public LayoutItemAutomationPeer(LayoutItem owner) : base(owner)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Pane;

        protected override string GetAutomationIdImpl()
        {
            string automationIdImpl = base.GetAutomationIdImpl();
            return (!string.IsNullOrEmpty(automationIdImpl) ? automationIdImpl : ((base.Owner.Label != null) ? base.Owner.Label.ToString() : string.Empty));
        }

        protected override string GetClassNameCore() => 
            base.Owner.GetType().Name;

        protected override string GetNameImpl()
        {
            string nameImpl = base.GetNameImpl();
            return (!string.IsNullOrEmpty(nameImpl) ? nameImpl : ((base.Owner.Label != null) ? base.Owner.Label.ToString() : string.Empty));
        }
    }
}

