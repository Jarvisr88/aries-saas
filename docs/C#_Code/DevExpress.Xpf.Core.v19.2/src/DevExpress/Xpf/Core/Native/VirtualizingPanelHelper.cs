namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Windows;

    public static class VirtualizingPanelHelper
    {
        public static readonly DependencyProperty IsVirtualizingWhenGroupingProperty;
        public static readonly DependencyProperty CanVirtualizeWhenGroupingProperty;
        [IgnoreDependencyPropertiesConsistencyChecker]
        private static readonly DependencyProperty Net45IsVirtualizingWhenGroupingProperty;

        static VirtualizingPanelHelper();
        public static bool GetCanVirtualizeWhenGrouping(DependencyObject target);
        public static bool GetIsVirtualizingWhenGrouping(DependencyObject target);
        private static void OnCanVirtualizeWhenGroupingChanged(DependencyObject d, DependencyPropertyChangedEventArgs e);
        public static void SetCanVirtualizeWhenGrouping(DependencyObject target, bool value);
        public static void SetIsVirtualizingWhenGrouping(DependencyObject target, bool value);
    }
}

