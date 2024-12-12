namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using System;

    public class DateEditAutomationPeer : PopupBaseEditAutomationPeer
    {
        public DateEditAutomationPeer(DateEdit element) : base(element)
        {
        }

        protected DateEdit Editor =>
            base.Editor as DateEdit;
    }
}

