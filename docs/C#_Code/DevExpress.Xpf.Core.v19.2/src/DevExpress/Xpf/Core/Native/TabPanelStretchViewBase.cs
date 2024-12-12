namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;

    public class TabPanelStretchViewBase : TabPanelBase
    {
        public static readonly DependencyProperty NormalChildSizeProperty;
        public static readonly DependencyProperty NormalChildMinSizeProperty;
        public static readonly DependencyProperty ActiveChildMinSizeProperty;
        public static readonly DependencyProperty PinChildLeftSizeProperty;
        public static readonly DependencyProperty PinChildRightSizeProperty;
        public static readonly DependencyProperty PinPanelLeftIndentProperty;
        public static readonly DependencyProperty PinPanelRightIndentProperty;
        public static readonly DependencyProperty TransparencySizeProperty;
        public static readonly DependencyProperty AlwaysShowActiveChildProperty;

        static TabPanelStretchViewBase();
        public TabPanelStretchViewBase();
        protected override Size AfterArrangeOverride(Size avSize);
        protected override Size ArrangeOverrideCore(Rect avRect);
        protected override Size CorrectSizeForControlBox(Size avSize, bool decreaseSize, bool increaseSize);
        public void ForceUpdateLayout();
        private double GetChildWidth(FrameworkElement child, double avWidth);
        protected virtual TabPinMode GetPinMode(FrameworkElement child);
        private double GetPinPanelLeftSize(bool useActualSizeInstedOfDesired = false);
        private double GetPinPanelRightSize(bool useActualSizeInstedOfDesired = false);
        protected override Size MeasureOverrideCore(Size avSize);
        protected override void UpdateControlBoxPosition(Size actualSize);
        protected override void UpdateVisibleChildren();

        public int NormalChildSize { get; set; }

        public int NormalChildMinSize { get; set; }

        public int ActiveChildMinSize { get; set; }

        public int PinChildLeftSize { get; set; }

        public int PinChildRightSize { get; set; }

        public int PinPanelLeftIndent { get; set; }

        public int PinPanelRightIndent { get; set; }

        public int TransparencySize { get; set; }

        public bool AlwaysShowActiveChild { get; set; }

        protected IEnumerable<FrameworkElement> PinnedLeftChildren { get; private set; }

        protected IEnumerable<FrameworkElement> PinnedRightChildren { get; private set; }

        protected IEnumerable<FrameworkElement> PinnedNoneChildren { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabPanelStretchViewBase.<>c <>9;
            public static Action<FrameworkElement> <>9__57_0;
            public static Action<FrameworkElement> <>9__57_1;

            static <>c();
            internal void <.cctor>b__36_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__36_1(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__36_2(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__36_3(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__36_4(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__36_5(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__36_6(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <.cctor>b__36_7(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal void <ArrangeOverrideCore>b__57_0(FrameworkElement x);
            internal void <ArrangeOverrideCore>b__57_1(FrameworkElement x);
        }
    }
}

