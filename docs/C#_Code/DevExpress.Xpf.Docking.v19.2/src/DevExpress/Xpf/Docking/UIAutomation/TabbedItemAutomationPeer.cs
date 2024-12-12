namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public class TabbedItemAutomationPeer : BaseLayoutItemAutomationPeer, ISelectionItemProvider
    {
        public TabbedItemAutomationPeer(BaseLayoutItem item) : base(item)
        {
        }

        public void AddToSelection()
        {
            this.SetFocusCore();
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.SelectionItem) ? base.GetPattern(patternInterface) : this;

        public void RemoveFromSelection()
        {
            base.Owner.IsSelectedItem = false;
        }

        public void Select()
        {
            this.SetFocusCore();
        }

        public bool IsSelected =>
            base.Owner.IsSelectedItem;

        public IRawElementProviderSimple SelectionContainer
        {
            get
            {
                AutomationPeer parent = base.GetParent();
                return base.ProviderFromPeer(parent);
            }
        }
    }
}

