namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows.Input;

    public class BarItemLinkSeparatorControlStrategy : BarItemLinkControlStrategy
    {
        public BarItemLinkSeparatorControlStrategy(IBarItemLinkControl instance);
        public override bool CanStartDragCore(object sender, MouseButtonEventArgs e);
        public override bool ReceiveEvent(object sender, EventArgs args);
        protected override bool ShouldHighlightItem();

        protected internal override bool CanSelectOnHoverInMenuMode { get; }
    }
}

