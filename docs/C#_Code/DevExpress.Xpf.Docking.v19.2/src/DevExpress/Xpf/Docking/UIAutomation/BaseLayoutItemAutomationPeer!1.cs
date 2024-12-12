namespace DevExpress.Xpf.Docking.UIAutomation
{
    using System;
    using System.Windows.Automation.Peers;

    public class BaseLayoutItemAutomationPeer<T> : BaseAutomationPeer<T> where T: BaseLayoutItem
    {
        public BaseLayoutItemAutomationPeer(T item) : base(item)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Group;

        protected override string GetAutomationIdImpl()
        {
            string automationIdImpl = base.GetAutomationIdImpl();
            return (string.IsNullOrEmpty(automationIdImpl) ? AutomationIdHelper.GetIdByLayoutItem(base.Owner) : automationIdImpl);
        }

        protected override string GetNameImpl()
        {
            string nameImpl = base.GetNameImpl();
            return (string.IsNullOrEmpty(nameImpl) ? AutomationIdHelper.GetLayoutItemName(base.Owner, base.Owner.GetType().Name) : nameImpl);
        }

        protected override void SetFocusCore()
        {
            base.Owner.Manager.Activate(base.Owner);
        }
    }
}

