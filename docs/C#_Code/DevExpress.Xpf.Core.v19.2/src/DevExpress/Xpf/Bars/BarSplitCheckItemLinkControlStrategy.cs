namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Diagnostics;

    public class BarSplitCheckItemLinkControlStrategy : BarSplitButtonItemLinkControlStrategy
    {
        private readonly BarCheckItemLinkControlHelperWorker worker;

        public BarSplitCheckItemLinkControlStrategy(IBarItemLinkControl instance);
        public override bool GetCloseSubMenuOnClick();
        public override void OnActualIsCheckedChanged(bool? oldValue, bool? newValue);
        protected override bool ShouldClickOnMouseLeftButtonUp(bool wasPressed);
        protected override void UpdateActualPropertiesOverride();

        public BarCheckItemLinkControlHelperWorker Worker { [DebuggerStepThrough] get; }
    }
}

