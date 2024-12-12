namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;

    public class ButtonEditAutomationPeer : TextEditAutomationPeer
    {
        public ButtonEditAutomationPeer(ButtonEdit element) : base(element)
        {
        }

        protected override List<AutomationPeer> GetChildrenCore() => 
            base.GetChildrenCore();

        protected ButtonEdit Editor =>
            base.Editor as ButtonEdit;
    }
}

