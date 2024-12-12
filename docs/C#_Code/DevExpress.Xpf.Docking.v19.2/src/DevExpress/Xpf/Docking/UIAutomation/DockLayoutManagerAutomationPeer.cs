namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation.Peers;

    public class DockLayoutManagerAutomationPeer : BaseAutomationPeer<DockLayoutManager>
    {
        public DockLayoutManagerAutomationPeer(DockLayoutManager control) : base(control)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Group;

        protected override List<AutomationPeer> GetChildrenCore()
        {
            List<AutomationPeer> list = new List<AutomationPeer>();
            if ((base.Owner != null) && !base.Owner.IsDisposing)
            {
                foreach (BaseLayoutItem item in base.Owner.AutoHideGroups)
                {
                    list.Add(CreatePeerForElement(item));
                }
                foreach (BaseLayoutItem item2 in base.Owner.ClosedPanels)
                {
                    list.Add(CreatePeerForElement(item2));
                }
                if (base.Owner.LayoutRoot != null)
                {
                    list.Add(CreatePeerForElement(base.Owner.LayoutRoot));
                }
                foreach (AutoHidePane pane in base.Owner.AutoHidePanes)
                {
                    if (pane != null)
                    {
                        list.Add(CreatePeerForElement(pane));
                    }
                }
            }
            return list;
        }
    }
}

