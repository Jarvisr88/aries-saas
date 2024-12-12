namespace DevExpress.Xpf.LayoutControl.UIAutomation
{
    using DevExpress.Xpf.LayoutControl;
    using System;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Threading;

    public class GroupBoxButtonAutomationPeer : BaseLayoutAutomationPeer<GroupBoxButton>, IInvokeProvider
    {
        public GroupBoxButtonAutomationPeer(GroupBoxButton element) : base(element)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Button;

        protected override string GetClassNameCore() => 
            base.Owner.GetType().Name;

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Invoke) ? base.GetPattern(patternInterface) : this;

        void IInvokeProvider.Invoke()
        {
            if (!base.IsEnabled())
            {
                throw new ElementNotEnabledException();
            }
            base.Owner.Dispatcher.BeginInvoke(() => base.Owner.Controller.InvokeClick(), DispatcherPriority.Input, new object[0]);
        }
    }
}

