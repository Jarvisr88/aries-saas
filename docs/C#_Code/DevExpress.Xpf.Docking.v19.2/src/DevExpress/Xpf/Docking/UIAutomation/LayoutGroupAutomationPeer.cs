namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Threading;

    public class LayoutGroupAutomationPeer : BaseLayoutItemAutomationPeer<LayoutGroup>, ISelectionProvider
    {
        private readonly EventHandler weakItemsChangedHandler;
        private readonly EventHandler weakSelectedItemChangedHandler;

        public LayoutGroupAutomationPeer(LayoutGroup group) : base(group)
        {
            if (group != null)
            {
                this.weakItemsChangedHandler = new EventHandler(this.OnGroupItemsChanged);
                group.WeakItemsChanged += this.weakItemsChangedHandler;
                this.weakSelectedItemChangedHandler = new EventHandler(this.OnGroupSelectedItemChanged);
                group.WeakSelectedItemChanged += this.weakSelectedItemChangedHandler;
            }
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            base.Owner.IsTabHost ? AutomationControlType.Tab : AutomationControlType.Group;

        protected override List<AutomationPeer> GetChildrenCore()
        {
            List<AutomationPeer> list = new List<AutomationPeer>();
            foreach (BaseLayoutItem item in base.Owner.Items)
            {
                list.Add(CreatePeerForElement(item));
            }
            BaseHeadersPanel templateChild = LayoutItemsHelper.GetTemplateChild<BaseHeadersPanel>(base.Owner);
            if (templateChild != null)
            {
                list.Add(CreatePeerForElement(templateChild));
            }
            GroupBoxControlBoxControl control = LayoutItemsHelper.GetTemplateChild<GroupBoxControlBoxControl>(base.Owner);
            if ((control != null) && (control.PartExpandButton != null))
            {
                list.Add(CreatePeerForElement(control.PartExpandButton));
            }
            TabHeaderControlBoxControl control2 = LayoutItemsHelper.GetTemplateChild<TabHeaderControlBoxControl>(base.Owner);
            if (control2 != null)
            {
                if (control2.PartCloseButton != null)
                {
                    list.Add(CreatePeerForElement(control2.PartCloseButton));
                }
                if (control2.PartScrollPrevButton != null)
                {
                    list.Add(CreatePeerForElement(control2.PartScrollPrevButton));
                }
                if (control2.PartScrollNextButton != null)
                {
                    list.Add(CreatePeerForElement(control2.PartScrollNextButton));
                }
                if (control2.PartDropDownButton != null)
                {
                    list.Add(CreatePeerForElement(control2.PartDropDownButton));
                }
                if (control2.PartRestoreButton != null)
                {
                    list.Add(CreatePeerForElement(control2.PartRestoreButton));
                }
            }
            return list;
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (!base.Owner.IsTabHost || (patternInterface != PatternInterface.Selection)) ? base.GetPattern(patternInterface) : this;

        public IRawElementProviderSimple[] GetSelection()
        {
            List<IRawElementProviderSimple> list = new List<IRawElementProviderSimple>();
            if (base.Owner.SelectedItem != null)
            {
                AutomationPeer peer = CreatePeerForElement(base.Owner.SelectedItem);
                list.Add(base.ProviderFromPeer(peer));
            }
            return list.ToArray();
        }

        private void OnGroupItemsChanged(object sender, EventArgs e)
        {
            base.Dispatcher.BeginInvoke(delegate {
                this.ResetHeadersPanel();
                base.ResetChildrenCache();
            }, new object[0]);
        }

        private void OnGroupSelectedItemChanged(object sender, EventArgs e)
        {
            base.Dispatcher.BeginInvoke(new Action(this.ResetSelectedItemCache), DispatcherPriority.Loaded, new object[0]);
        }

        private void ResetHeadersPanel()
        {
            if (base.Owner != null)
            {
                BaseHeadersPanel templateChild = LayoutItemsHelper.GetTemplateChild<BaseHeadersPanel>(base.Owner);
                if (templateChild != null)
                {
                    AutomationPeer peer = CreatePeerForElement(templateChild);
                    if (peer != null)
                    {
                        peer.ResetChildrenCache();
                    }
                }
            }
        }

        private void ResetSelectedItemCache()
        {
            LayoutGroup owner = base.Owner;
            if (owner != null)
            {
                BaseLayoutItem selectedItem = owner.SelectedItem;
                if (selectedItem != null)
                {
                    AutomationPeer peer = CreatePeerForElement(selectedItem);
                    if (peer != null)
                    {
                        peer.ResetChildrenCache();
                    }
                }
            }
        }

        public bool CanSelectMultiple =>
            false;

        public bool IsSelectionRequired =>
            true;
    }
}

