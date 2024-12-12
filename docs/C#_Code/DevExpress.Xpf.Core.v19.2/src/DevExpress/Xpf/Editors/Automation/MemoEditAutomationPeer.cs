namespace DevExpress.Xpf.Editors.Automation
{
    using DevExpress.Xpf.Editors;
    using System;

    public class MemoEditAutomationPeer : PopupBaseEditAutomationPeer
    {
        public MemoEditAutomationPeer(MemoEdit element) : base(element)
        {
        }

        protected MemoEdit Editor =>
            base.Editor as MemoEdit;
    }
}

