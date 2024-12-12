namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;
    using System.Windows.Controls;

    public class ListBoxEditAutomationPeer : BaseEditAutomationPeer, ISelectionProvider
    {
        public ListBoxEditAutomationPeer(ListBoxEdit element) : base(element)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.List;

        protected override List<AutomationPeer> GetChildrenCore()
        {
            if (this.Editor.ListBoxCore == null)
            {
                return null;
            }
            List<AutomationPeer> list1 = new List<AutomationPeer>();
            list1.Add(this.ListBoxPeer);
            return list1;
        }

        protected override string GetClassNameCore() => 
            "ListBoxEdit";

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Selection) ? base.GetPattern(patternInterface) : this;

        public IRawElementProviderSimple[] GetSelection()
        {
            ISelectionProvider listBoxPeer = this.ListBoxPeer;
            return listBoxPeer?.GetSelection();
        }

        private ListBoxAutomationPeer ListBoxPeer =>
            (this.Editor.ListBoxCore != null) ? ((ListBoxAutomationPeer) CreatePeerForElement(this.Editor.ListBoxCore)) : null;

        protected ListBoxEdit Editor =>
            base.Editor as ListBoxEdit;

        public bool CanSelectMultiple =>
            this.Editor.SelectionMode != SelectionMode.Single;

        public bool IsSelectionRequired
        {
            get
            {
                ISelectionProvider listBoxPeer = this.ListBoxPeer;
                return ((listBoxPeer != null) && listBoxPeer.IsSelectionRequired);
            }
        }
    }
}

