namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;

    public class BarSplitButtonItemArrowControl : ArrowControl
    {
        protected override void OnLinkControlChanged(DependencyPropertyChangedEventArgs e);
        protected virtual void SetBindings();

        public BarSplitButtonItemLinkControl SubItemLinkControl { get; }
    }
}

