namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Mvvm.UI;
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Linq;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Threading;

    public class TabbedPaneItemAutomationPeer : BaseAutomationPeer<TabbedPaneItem>, IInvokeProvider, ISelectionItemProvider
    {
        public TabbedPaneItemAutomationPeer(TabbedPaneItem paneItem) : base(paneItem)
        {
        }

        public void AddToSelection()
        {
            this.Select();
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.TabItem;

        protected override string GetAutomationIdImpl()
        {
            string automationIdImpl = base.GetAutomationIdImpl();
            if (string.IsNullOrEmpty(automationIdImpl))
            {
                automationIdImpl = AutomationIdHelper.GetIdByLayoutItem(base.Owner) + "TabId";
            }
            return automationIdImpl;
        }

        protected override string GetNameImpl()
        {
            string nameImpl = base.GetNameImpl();
            if (string.IsNullOrEmpty(nameImpl))
            {
                nameImpl = AccessKeyHelper.RemoveAccessKeyMarker(AutomationIdHelper.GetLayoutItemName(DockLayoutManager.GetLayoutItem(base.Owner), base.Owner.Name));
                if (string.IsNullOrEmpty(nameImpl))
                {
                    nameImpl = base.Owner.GetType().FullName;
                }
            }
            return nameImpl;
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            ((patternInterface == PatternInterface.Invoke) || (patternInterface == PatternInterface.SelectionItem)) ? this : base.GetPattern(patternInterface);

        private void InvokeClick()
        {
            if (base.Owner.AutomationClick())
            {
                base.RaiseAutomationEvent(AutomationEvents.InvokePatternOnInvoked);
            }
        }

        protected override bool IsEnabledCore()
        {
            BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem(base.Owner);
            return ((layoutItem != null) && layoutItem.IsEnabled);
        }

        public void RemoveFromSelection()
        {
            this.Select(false);
        }

        public void Select()
        {
            this.Select(true);
        }

        private void Select(bool isSelected)
        {
            BaseLayoutItem layoutItem = DockLayoutManager.GetLayoutItem(base.Owner);
            if (layoutItem != null)
            {
                layoutItem.IsSelectedItem = isSelected;
            }
        }

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

        public bool IsSelected =>
            base.Owner.IsSelected;

        public IRawElementProviderSimple SelectionContainer
        {
            get
            {
                BaseHeadersPanel element = LayoutTreeHelper.GetVisualParents(base.Owner, null).OfType<BaseHeadersPanel>().FirstOrDefault<BaseHeadersPanel>();
                if (element != null)
                {
                    AutomationPeer peer = CreatePeerForElement(element);
                    if (peer != null)
                    {
                        return base.ProviderFromPeer(peer);
                    }
                }
                return null;
            }
        }
    }
}

