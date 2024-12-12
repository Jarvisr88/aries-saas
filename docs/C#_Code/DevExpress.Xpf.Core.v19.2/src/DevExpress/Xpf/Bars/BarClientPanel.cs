namespace DevExpress.Xpf.Bars
{
    using System;
    using System.Collections;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Media;

    public class BarClientPanel : Panel
    {
        public static readonly DependencyProperty QuickCustomizationButtonProperty;
        public static readonly DependencyProperty QuickCustomizationButtonShowModeProperty;
        public static readonly DependencyProperty HorizontalIndentProperty;
        private BarItemsLayoutCalculator itemsCalculator;

        static BarClientPanel();
        public BarClientPanel();
        protected override Size ArrangeOverride(Size arrangeBounds);
        protected virtual BarItemsLayoutCalculator CreateItemsCalculator();
        protected override Visual GetVisualChild(int index);
        protected override Size MeasureOverride(Size constraint);
        protected virtual void OnLoaded(object sender, RoutedEventArgs e);
        protected virtual void OnQuickCustomizationButtonChanged(BarQuickCustomizationButton oldValue);
        protected virtual void OnQuickCustomizationButtonShowModeChanged(bool? oldValue);
        protected internal virtual void SetQuickCustomizationButtonVisibility(bool isVisible);

        public BarQuickCustomizationButton QuickCustomizationButton { get; set; }

        public bool? QuickCustomizationButtonShowMode { get; set; }

        public double HorizontalIndent { get; set; }

        protected internal BarItemsLayoutCalculator ItemsCalculator { get; }

        public BarControl Owner { get; }

        public BarContainerControl Container { get; }

        protected override int VisualChildrenCount { get; }

        protected override IEnumerator LogicalChildren { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly BarClientPanel.<>c <>9;

            static <>c();
            internal void <.cctor>b__3_0(BarClientPanel x, BarQuickCustomizationButton oldValue, BarQuickCustomizationButton newValue);
            internal void <.cctor>b__3_1(BarClientPanel x, bool? oldValue, bool? newValue);
        }
    }
}

