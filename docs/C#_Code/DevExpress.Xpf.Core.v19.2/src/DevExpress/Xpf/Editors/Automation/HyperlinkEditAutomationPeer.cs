namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Windows.Automation;
    using System.Windows.Automation.Peers;
    using System.Windows.Automation.Provider;

    public class HyperlinkEditAutomationPeer : BaseEditAutomationPeer, IInvokeProvider
    {
        public HyperlinkEditAutomationPeer(HyperlinkEdit element) : base(element)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Hyperlink;

        public override object GetPattern(PatternInterface patternInterface) => 
            (patternInterface != PatternInterface.Invoke) ? base.GetPattern(patternInterface) : this;

        protected override bool HasKeyboardFocusCore() => 
            base.Editor.IsEditorKeyboardFocused;

        protected override bool IsKeyboardFocusableCore() => 
            base.Editor.Focusable;

        protected override void SetFocusCore()
        {
            base.Editor.Focus();
        }

        void IInvokeProvider.Invoke()
        {
            if (!base.IsEnabled())
            {
                throw new ElementNotEnabledException();
            }
            ((HyperlinkEdit) base.Owner).DoNavigate();
        }
    }
}

