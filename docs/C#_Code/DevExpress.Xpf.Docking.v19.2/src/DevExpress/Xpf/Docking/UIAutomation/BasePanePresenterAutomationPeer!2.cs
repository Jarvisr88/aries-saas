namespace DevExpress.Xpf.Docking.UIAutomation
{
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Windows.Automation.Peers;

    public class BasePanePresenterAutomationPeer<TVisual, TLogical> : FrameworkElementAutomationPeer where TVisual: DependencyObject, IDisposable where TLogical: BaseLayoutItem
    {
        public BasePanePresenterAutomationPeer(BasePanePresenter<TVisual, TLogical> presenter) : base(presenter)
        {
        }

        protected override AutomationControlType GetAutomationControlTypeCore() => 
            AutomationControlType.Group;

        protected override string GetAutomationIdCore() => 
            AutomationIdHelper.GetIdByLayoutItem(base.Owner);

        protected override string GetClassNameCore() => 
            typeof(TVisual).Name;

        protected override string GetNameCore() => 
            ((BasePanePresenter<TVisual, TLogical>) base.Owner).Name;
    }
}

