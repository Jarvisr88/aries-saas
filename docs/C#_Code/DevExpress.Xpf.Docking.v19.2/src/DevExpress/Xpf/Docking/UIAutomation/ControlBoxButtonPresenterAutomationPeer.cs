namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Threading;

    public class ControlBoxButtonPresenterAutomationPeer : BaseAutomationPeer<ControlBoxButtonPresenter>, IInvokeProvider
    {
        public ControlBoxButtonPresenterAutomationPeer(ControlBoxButtonPresenter controlBoxButtonPresenter) : base(controlBoxButtonPresenter)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Button;

        protected override string GetAutomationIdImpl()
        {
            string automationIdImpl = base.GetAutomationIdImpl();
            if (string.IsNullOrEmpty(automationIdImpl))
            {
                automationIdImpl = AutomationIdHelper.GetIdByLayoutItem(base.Owner);
            }
            return automationIdImpl;
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Invoke) ? base.GetPattern(patternInterface) : this;

        private void InvokeClick()
        {
            if (base.Owner.AutomationClick())
            {
                base.RaiseAutomationEvent(AutomationEvents.InvokePatternOnInvoked);
            }
        }

        protected override bool IsEnabledCore() => 
            base.Owner.PartButton != null;

        protected override void SetFocusCore()
        {
            DockLayoutManager dockLayoutManager = DockLayoutManager.GetDockLayoutManager(base.Owner);
            BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem(base.Owner);
            if (layoutItem != null)
            {
                dockLayoutManager.DockController.Activate(layoutItem);
            }
        }

        void IInvokeProvider.Invoke()
        {
            if (!base.IsEnabled())
            {
                throw new ElementNotEnabledException();
            }
            InvokeHelper.BeginInvoke(base.Owner, new Action(this.InvokeClick), DispatcherPriority.Input, new object[0]);
        }
    }
}

