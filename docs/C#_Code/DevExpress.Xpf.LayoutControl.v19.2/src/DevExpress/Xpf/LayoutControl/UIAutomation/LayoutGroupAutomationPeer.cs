namespace DevExpress.Xpf.LayoutControl.UIAutomation
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.LayoutControl;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public class LayoutGroupAutomationPeer : LayoutControlBaseAutomationPeer<LayoutGroup>, IExpandCollapseProvider, ISelectionProvider
    {
        public LayoutGroupAutomationPeer(LayoutGroup owner) : base(owner)
        {
        }

        protected override string GetAutomationIdImpl()
        {
            string automationIdImpl = base.GetAutomationIdImpl();
            return (!string.IsNullOrEmpty(automationIdImpl) ? automationIdImpl : ((base.Owner.Header != null) ? base.Owner.Header.ToString() : string.Empty));
        }

        protected override List<AutomationPeer> GetChildrenCore()
        {
            List<AutomationPeer> childrenCore = base.GetChildrenCore();
            if (base.Owner.View == LayoutGroupView.GroupBox)
            {
                Func<FrameworkElement, bool> predicate = <>c.<>9__8_0;
                if (<>c.<>9__8_0 == null)
                {
                    Func<FrameworkElement, bool> local1 = <>c.<>9__8_0;
                    predicate = <>c.<>9__8_0 = x => x is GroupBox;
                }
                FrameworkElement treeRoot = base.Owner.GetChildren(true, false, true).Where<FrameworkElement>(predicate).FirstOrDefault<FrameworkElement>();
                if (treeRoot != null)
                {
                    GroupBoxButton element = LayoutHelper.FindElementByName(treeRoot, "MinimizeElement") as GroupBoxButton;
                    if (element != null)
                    {
                        AutomationPeer item = CreatePeerForElement(element);
                        if (item != null)
                        {
                            childrenCore.Add(item);
                        }
                    }
                }
            }
            return childrenCore;
        }

        protected override string GetNameImpl()
        {
            string nameImpl = base.GetNameImpl();
            return (!string.IsNullOrEmpty(nameImpl) ? nameImpl : ((base.Owner.Header != null) ? base.Owner.Header.ToString() : string.Empty));
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            ((patternInterface != PatternInterface.ExpandCollapse) || !this.IsCollapsible) ? (((patternInterface != PatternInterface.Selection) || (base.Owner.View != LayoutGroupView.Tabs)) ? base.GetPattern(patternInterface) : this) : this;

        void IExpandCollapseProvider.Collapse()
        {
            if (base.Owner.IsCollapsible)
            {
                base.Owner.IsCollapsed = true;
            }
        }

        void IExpandCollapseProvider.Expand()
        {
            if (base.Owner.IsCollapsible)
            {
                base.Owner.IsCollapsed = false;
            }
        }

        IRawElementProviderSimple[] ISelectionProvider.GetSelection()
        {
            AutomationPeer peer = CreatePeerForElement(base.Owner.SelectedTabChild);
            return new IRawElementProviderSimple[] { base.ProviderFromPeer(peer) };
        }

        private bool IsCollapsible =>
            (base.Owner.View == LayoutGroupView.GroupBox) && base.Owner.IsCollapsible;

        protected override bool IncludeInternalElements =>
            base.IncludeInternalElements || (base.Owner.View == LayoutGroupView.Tabs);

        ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState =>
            (!base.Owner.IsCollapsible || !base.Owner.IsActuallyCollapsed) ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed;

        bool ISelectionProvider.CanSelectMultiple =>
            false;

        bool ISelectionProvider.IsSelectionRequired =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LayoutGroupAutomationPeer.<>c <>9 = new LayoutGroupAutomationPeer.<>c();
            public static Func<FrameworkElement, bool> <>9__8_0;

            internal bool <GetChildrenCore>b__8_0(FrameworkElement x) => 
                x is GroupBox;
        }
    }
}

