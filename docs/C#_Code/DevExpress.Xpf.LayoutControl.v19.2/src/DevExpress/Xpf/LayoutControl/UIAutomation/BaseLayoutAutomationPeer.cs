namespace DevExpress.Xpf.LayoutControl.UIAutomation
{
    using DevExpress.Xpf.Bars.Automation;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Media;

    public abstract class BaseLayoutAutomationPeer : BaseNavigationAutomationPeer
    {
        protected BaseLayoutAutomationPeer(FrameworkElement owner) : base(owner)
        {
        }

        protected sealed override string GetAutomationIdCore()
        {
            string automationIdImpl = this.GetAutomationIdImpl();
            return (string.IsNullOrEmpty(automationIdImpl) ? base.Owner.GetType().Name : automationIdImpl);
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

        protected sealed override string GetNameCore()
        {
            string nameImpl = this.GetNameImpl();
            return (string.IsNullOrEmpty(nameImpl) ? base.Owner.GetType().Name : nameImpl);
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

        protected static bool Iterate(DependencyObject parent, IteratorCallback callback, FilterCallback filterCallback)
        {
            bool flag = false;
            if (parent != null)
            {
                AutomationPeer peer = null;
                int childrenCount = VisualTreeHelper.GetChildrenCount(parent);
                for (int i = 0; (i < childrenCount) && !flag; i++)
                {
                    DependencyObject child = VisualTreeHelper.GetChild(parent, i);
                    flag = (!filterCallback(child) || ((peer = CreatePeerForElement((UIElement) child)) == null)) ? (((child == null) || (!(child is UIElement3D) || ((peer = UIElement3DAutomationPeer.CreatePeerForElement((UIElement3D) child)) == null))) ? Iterate(child, callback, filterCallback) : callback(peer)) : callback(peer);
                }
            }
            return flag;
        }

        protected delegate bool FilterCallback(object obj);

        protected delegate bool IteratorCallback(AutomationPeer peer);
    }
}

