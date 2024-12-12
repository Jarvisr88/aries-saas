namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public class SplitButtonAutomationPeer : ButtonAutomationPeer, IExpandCollapseProvider
    {
        private readonly SplitButton splitButton;

        public SplitButtonAutomationPeer(SplitButton owner) : base(owner)
        {
            this.splitButton = owner;
        }

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface == PatternInterface.ExpandCollapse) ? this : base.GetPattern(patternInterface);

        void IExpandCollapseProvider.Collapse()
        {
            this.splitButton.IsPopupOpen = false;
        }

        void IExpandCollapseProvider.Expand()
        {
            this.splitButton.IsPopupOpen = true;
        }

        ExpandCollapseState IExpandCollapseProvider.ExpandCollapseState =>
            this.splitButton.IsPopupOpen ? ExpandCollapseState.Expanded : ExpandCollapseState.Collapsed;
    }
}

