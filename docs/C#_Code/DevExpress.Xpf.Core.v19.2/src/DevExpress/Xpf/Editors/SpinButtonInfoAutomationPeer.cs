namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Collections.Generic;
    using System.Windows.Automation.Peers;

    internal class SpinButtonInfoAutomationPeer : ButtonInfoBaseAutomationPeer
    {
        public SpinButtonInfoAutomationPeer(SpinButtonInfo owner) : base(owner)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Custom;

        protected override List<AutomationPeer> GetChildrenCore()
        {
            List<AutomationPeer> list1 = new List<AutomationPeer>();
            list1.Add(new SpinUpDownButtonInfoAutomationPeer(this.Owner, true));
            list1.Add(new SpinUpDownButtonInfoAutomationPeer(this.Owner, false));
            return list1;
        }

        protected override string GetClassNameCore() => 
            "SpinButtonInfo";

        private SpinButtonInfo Owner =>
            base.Owner as SpinButtonInfo;
    }
}

