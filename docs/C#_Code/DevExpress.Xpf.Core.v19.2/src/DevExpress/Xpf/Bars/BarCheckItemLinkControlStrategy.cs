namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Diagnostics;
    using System.Windows.Input;

    public class BarCheckItemLinkControlStrategy : BarButtonItemLinkControlStrategy
    {
        private readonly BarCheckItemLinkControlHelperWorker worker;

        public BarCheckItemLinkControlStrategy(IBarItemLinkControl instance);
        public override bool GetCloseSubMenuOnClick();
        public override void OnActualIsCheckedChanged(bool? oldValue, bool? newValue);
        public override bool OnMouseUp(MouseButtonEventArgs e);
        protected override void UpdateActualPropertiesOverride();

        public BarCheckItemLinkControlHelperWorker Worker { [DebuggerStepThrough] get; }

        public IBarCheckItemLinkControl CheckInstance { get; }
    }
}

