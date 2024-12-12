namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows.Automation.Peers;

    public class ThemedDialogButtonAutomationPeer : ItemAutomationPeer
    {
        public ThemedDialogButtonAutomationPeer(object item, ItemsControlAutomationPeer itemsControlAutomationPeer) : base(item, itemsControlAutomationPeer)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Button;

        protected override string GetClassNameCore() => 
            "ThemedWindowDialogButton";
    }
}

