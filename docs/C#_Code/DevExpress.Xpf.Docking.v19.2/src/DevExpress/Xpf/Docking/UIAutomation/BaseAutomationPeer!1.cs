namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Bars.Automation;
    using System;
    using System.Windows.Automation;

    public abstract class BaseAutomationPeer<T> : BaseNavigationAutomationPeer where T: FrameworkElement
    {
        protected BaseAutomationPeer(T owner) : base(owner)
        {
        }

        protected sealed override string GetAutomationIdCore()
        {
            string automationIdImpl = this.GetAutomationIdImpl();
            return (string.IsNullOrEmpty(automationIdImpl) ? this.Owner.GetType().Name : automationIdImpl);
        }

        protected virtual string GetAutomationIdImpl()
        {
            string automationIdCore = base.GetAutomationIdCore();
            if (string.IsNullOrEmpty(automationIdCore))
            {
                bool flag;
                object obj2 = base.TryGetAutomationPropertyValue(AutomationProperties.AutomationIdProperty, out flag);
                if (flag)
                {
                    return (string) obj2;
                }
            }
            return automationIdCore;
        }

        protected override string GetClassNameCore() => 
            this.Owner.GetType().Name;

        protected sealed override string GetNameCore()
        {
            string nameImpl = this.GetNameImpl();
            if (string.IsNullOrEmpty(nameImpl))
            {
                nameImpl = string.IsNullOrEmpty(this.Owner.Name) ? this.Owner.GetType().Name : this.Owner.Name;
            }
            return nameImpl;
        }

        protected virtual string GetNameImpl()
        {
            string nameCore = base.GetNameCore();
            if (string.IsNullOrEmpty(nameCore))
            {
                bool flag;
                object obj2 = base.TryGetAutomationPropertyValue(AutomationProperties.NameProperty, out flag);
                if (flag)
                {
                    return (string) obj2;
                }
            }
            return nameCore;
        }

        public T Owner =>
            (T) base.Owner;
    }
}

