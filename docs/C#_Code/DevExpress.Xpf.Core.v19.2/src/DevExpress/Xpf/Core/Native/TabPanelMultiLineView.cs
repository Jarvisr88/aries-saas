namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class TabPanelMultiLineView : TabPanelMultiLineViewBase
    {
        public static readonly DependencyProperty IsHoldModeProperty;
        public static readonly DependencyProperty AutoEnableHoldModeWhenManyRowsProperty;

        static TabPanelMultiLineView();
        protected override Size MeasureOverrideCore(Size avSize);
        private void OnIsHoldModeChanged();

        public bool IsHoldMode { get; set; }

        public bool AutoEnableHoldModeWhenManyRows { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabPanelMultiLineView.<>c <>9;

            static <>c();
            internal void <.cctor>b__11_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
        }
    }
}

