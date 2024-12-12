namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;
    using System.Windows.Input;

    public class BarButtonItemLinkControlStrategy : BarItemLinkControlStrategy
    {
        private bool mouseMoved;
        private Point oldPosition;

        public BarButtonItemLinkControlStrategy(IBarItemLinkControl instance);
        public override bool GetCloseSubMenuOnClick();
        public override bool OnMouseLeave(MouseEventArgs args);
        public override bool OnMouseLeftButtonDown(MouseButtonEventArgs args);
        public override bool OnMouseLeftButtonUp(MouseButtonEventArgs args);
        public override bool OnMouseMove(MouseEventArgs args);
        public override bool OnMouseRightButtonDown(MouseButtonEventArgs args);
        public override bool OnMouseRightButtonUp(MouseButtonEventArgs args);
        public override bool ProcessKeyDown(KeyEventArgs args);
        public override bool SelectOnKeyTip();
        protected virtual bool ShouldClickOnMouseLeftButtonUp(bool wasPressed);
        protected override bool ShouldHighlightItem();
        protected virtual bool ShouldUnpressOnMouseLeave(MouseEventArgs e);

        public BarButtonItemLink ButtonLink { get; }

        public BarButtonItem ButtonItem { get; }
    }
}

