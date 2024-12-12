namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public class BaseHeadersPanelAutomationPeer : BaseAutomationPeer<BaseHeadersPanel>, ISelectionProvider
    {
        public BaseHeadersPanelAutomationPeer(BaseHeadersPanel headersPanel) : base(headersPanel)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Group;

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Selection) ? base.GetPattern(patternInterface) : this;

        public IRawElementProviderSimple[] GetSelection()
        {
            List<IRawElementProviderSimple> list = new List<IRawElementProviderSimple>();
            Func<TabbedPaneItem, bool> predicate = <>c.<>9__7_0;
            if (<>c.<>9__7_0 == null)
            {
                Func<TabbedPaneItem, bool> local1 = <>c.<>9__7_0;
                predicate = <>c.<>9__7_0 = x => x.IsSelected;
            }
            TabbedPaneItem element = base.Owner.Children.OfType<TabbedPaneItem>().FirstOrDefault<TabbedPaneItem>(predicate);
            if (element != null)
            {
                AutomationPeer peer = CreatePeerForElement(element);
                if (peer != null)
                {
                    list.Add(base.ProviderFromPeer(peer));
                }
            }
            return list.ToArray();
        }

        public bool CanSelectMultiple =>
            false;

        public bool IsSelectionRequired =>
            true;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BaseHeadersPanelAutomationPeer.<>c <>9 = new BaseHeadersPanelAutomationPeer.<>c();
            public static Func<TabbedPaneItem, bool> <>9__7_0;

            internal bool <GetSelection>b__7_0(TabbedPaneItem x) => 
                x.IsSelected;
        }
    }
}

