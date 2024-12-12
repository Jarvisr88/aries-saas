namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Windows;
    using System.Windows.Controls;

    public class BarControlItemsPresenter : ItemsPresenter
    {
        public static readonly DependencyProperty QuickCustomizationButtonStyleProperty;
        public static readonly DependencyProperty ShowBackgroundProperty;

        static BarControlItemsPresenter();
        public override void OnApplyTemplate();

        public Style QuickCustomizationButtonStyle { get; set; }

        public bool ShowBackground { get; set; }
    }
}

