namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Markup;

    [ContentProperty("Child")]
    public class TabPanelContainer : TabOrientablePanel
    {
        public static readonly DependencyProperty NormalMarginProperty;
        public static readonly DependencyProperty NormalPaddingProperty;
        public static readonly DependencyProperty HoldMarginProperty;
        public static readonly DependencyProperty HoldPaddingProperty;

        static TabPanelContainer();
        public TabPanelContainer();
        protected override void OnViewInfoChanged();
        private void UpdateMargings();

        public Thickness NormalMargin { get; set; }

        public Thickness NormalPadding { get; set; }

        public Thickness HoldMargin { get; set; }

        public Thickness HoldPadding { get; set; }

        public Thickness ActualMargin { get; }

        public Thickness ActualPadding { get; }

        public double ActualLength { get; }

        public DXBorder ActualChild { get; private set; }

        public DXBorder ActualControlBox { get; private set; }

        public ItemsPresenter Child { get; set; }

        public FrameworkElement ControlBox { get; set; }

        public TabPanelBase Panel { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabPanelContainer.<>c <>9;
            public static Func<TabViewInfo, bool> <>9__17_0;
            public static Func<TabViewInfo, bool> <>9__19_0;
            public static Func<TabViewInfo, Orientation> <>9__21_0;
            public static Func<Orientation> <>9__21_1;

            static <>c();
            internal void <.cctor>b__41_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__41_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__41_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__41_3(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal Orientation <get_ActualLength>b__21_0(TabViewInfo x);
            internal Orientation <get_ActualLength>b__21_1();
            internal bool <get_ActualMargin>b__17_0(TabViewInfo x);
            internal bool <get_ActualPadding>b__19_0(TabViewInfo x);
        }
    }
}

