namespace DevExpress.Xpf.LayoutControl.UIAutomation
{
    using DevExpress.Xpf.LayoutControl;
    using System;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Threading;

    public class TileAutomationPeer : BaseLayoutAutomationPeer<Tile>, IInvokeProvider
    {
        public TileAutomationPeer(Tile element) : base(element)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Button;

        protected override string GetAutomationIdImpl()
        {
            string automationIdImpl = base.GetAutomationIdImpl();
            return (!string.IsNullOrEmpty(automationIdImpl) ? automationIdImpl : ((base.Owner.Header != null) ? base.Owner.Header.ToString() : string.Empty));
        }

        protected override string GetClassNameCore() => 
            base.Owner.GetType().Name;

        protected override string GetNameImpl()
        {
            string nameImpl = base.GetNameImpl();
            return (!string.IsNullOrEmpty(nameImpl) ? nameImpl : ((base.Owner.Header != null) ? base.Owner.Header.ToString() : string.Empty));
        }

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

