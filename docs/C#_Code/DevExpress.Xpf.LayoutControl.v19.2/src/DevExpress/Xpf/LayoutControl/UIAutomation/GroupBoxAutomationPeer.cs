namespace DevExpress.Xpf.LayoutControl.UIAutomation
{
    using DevExpress.Xpf.LayoutControl;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public class GroupBoxAutomationPeer : BaseLayoutAutomationPeer<GroupBox>, IExpandCollapseProvider
    {
        public GroupBoxAutomationPeer(GroupBox owner) : base(owner)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Group;

        protected override string GetAutomationIdImpl()
        {
            string automationIdImpl = base.GetAutomationIdImpl();
            return (!string.IsNullOrEmpty(automationIdImpl) ? automationIdImpl : ((base.Owner.Header != null) ? base.Owner.Header.ToString() : string.Empty));
        }

        protected override List<AutomationPeer> GetChildrenCore()
        {
            List<AutomationPeer> children = null;
            BaseLayoutAutomationPeer.IteratorCallback callback = delegate (AutomationPeer peer) {
                children = new List<AutomationPeer> {
                    peer
                };
                return false;
            };
            Iterate(base.Owner, callback, <>c.<>9__6_1 ??= obj => ((obj != null) && ((obj is UIElement) && (!(obj is LayoutGroup) && ((UIElement) obj).IsVisible))));
            return children;
        }

        protected override string GetNameImpl()
        {
            string nameImpl = base.GetNameImpl();
            return (!string.IsNullOrEmpty(nameImpl) ? nameImpl : ((base.Owner.Header != null) ? base.Owner.Header.ToString() : string.Empty));
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            ((patternInterface != PatternInterface.ExpandCollapse) || !this.IsCollapsible) ? base.GetPattern(patternInterface) : this;

        void IExpandCollapseProvider.Collapse()
        {
            base.Owner.State = GroupBoxState.Minimized;
        }

        void IExpandCollapseProvider.Expand()
        {
            base.Owner.State = GroupBoxState.Normal;
        }

        private bool IsCollapsible =>
            true;

        ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState =>
            (base.Owner.State == GroupBoxState.Minimized) ? ExpandCollapseState.Collapsed : ExpandCollapseState.Expanded;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DevExpress.Xpf.LayoutControl.UIAutomation.GroupBoxAutomationPeer.<>c <>9 = new DevExpress.Xpf.LayoutControl.UIAutomation.GroupBoxAutomationPeer.<>c();
            public static BaseLayoutAutomationPeer.FilterCallback <>9__6_1;

            internal bool <GetChildrenCore>b__6_1(object obj) => 
                (obj != null) && ((obj is UIElement) && (!(obj is LayoutGroup) && ((UIElement) obj).IsVisible));
        }
    }
}

