namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Docking;
    using System;

    public class ClosedPanelItemAutomationPeer : BaseLayoutItemAutomationPeer
    {
        public ClosedPanelItemAutomationPeer(BaseLayoutItem item) : base(item)
        {
        }

        protected override void SetFocusCore()
        {
            base.Owner.Manager.DockController.Restore(base.Owner);
        }
    }
}

