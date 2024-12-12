namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Automation.Peers;

    public class ThemedDialogControlAutomationPeer : ItemsControlAutomationPeer
    {
        public ThemedDialogControlAutomationPeer(ThemedWindowDialogButtonsControl buttonsControl) : base(buttonsControl)
        {
        }

        protected override ItemAutomationPeer CreateItemAutomationPeer(object item) => 
            new ThemedDialogButtonAutomationPeer(item, this);

        protected override string GetClassNameCore() => 
            "ThemedWindowDialogButtonsControl";
    }
}

