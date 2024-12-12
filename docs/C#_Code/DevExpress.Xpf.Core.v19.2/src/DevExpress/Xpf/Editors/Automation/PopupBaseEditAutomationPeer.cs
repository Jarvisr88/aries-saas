namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using DevExpress.Xpf.Editors.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Windows;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public class PopupBaseEditAutomationPeer : ButtonEditAutomationPeer, IExpandCollapseProvider
    {
        public PopupBaseEditAutomationPeer(PopupBaseEdit element) : base(element)
        {
        }

        protected virtual AutomationPeer CreatePopupRootPeer()
        {
            FrameworkElement popupRoot = this.Editor.PopupRoot;
            return ((popupRoot != null) ? CreatePeerForElement(popupRoot) : null);
        }

        protected override List<AutomationPeer> GetChildrenCore()
        {
            List<AutomationPeer> childrenCore = base.GetChildrenCore();
            if (!this.Editor.IsPopupOpen)
            {
                return childrenCore;
            }
            AutomationPeer peer = this.CreatePopupRootPeer();
            if (peer == null)
            {
                return childrenCore;
            }
            AutomationPeer[] second = new AutomationPeer[] { peer };
            return childrenCore.Append<AutomationPeer>(second).ToList<AutomationPeer>();
        }

        public override object GetPattern(PatternInterface pattern) => 
            (pattern != PatternInterface.ExpandCollapse) ? base.GetPattern(pattern) : this;

        void IExpandCollapseProvider.Collapse()
        {
            this.Editor.IsPopupOpen = false;
        }

        void IExpandCollapseProvider.Expand()
        {
            this.Editor.IsPopupOpen = true;
        }

        protected PopupBaseEdit Editor =>
            base.Editor as PopupBaseEdit;

        ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState =>
            !this.Editor.IsPopupOpen ? ExpandCollapseState.Collapsed : ExpandCollapseState.Expanded;
    }
}

