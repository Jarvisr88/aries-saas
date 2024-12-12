namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public abstract class TabPanelBase : DXPanelBase
    {
        public static readonly DependencyProperty ChildMinHeightProperty;
        public static readonly DependencyProperty SelectedIndexProperty;
        public static readonly DependencyProperty HeaderLocationProperty;

        static TabPanelBase();
        protected TabPanelBase();
        protected override Size AfterArrangeOverride(Size avSize);
        protected override Size AfterMeasureOverride(Size avSize);
        protected void ApplyOpacityMask(FrameworkElement child, double offset, double childWidth, double avWidth, double transparencySize);
        protected override Size BeforeArrangeOverride(Size avSize);
        protected override Size BeforeMeasureOverride(Size avSize);
        protected void ClearOpacityMask(FrameworkElement child);
        protected virtual Size CorrectSizeForControlBox(Size avSize, bool decreaseSize, bool increaseSize);
        public static double GetChildMinHeight(FrameworkElement obj);
        protected virtual void OnHeaderLocationChanged();
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved);
        public static void SetChildMinHeight(FrameworkElement obj, double value);
        protected void SetMinHeight(FrameworkElement child, double height);
        protected virtual void UpdateControlBoxPosition(Size actualSize);
        protected void UpdateOpacityMasks(IEnumerable<FrameworkElement> childs, Rect viewBox, double transparencySize);
        protected override void UpdateVisibleChildren();

        public int SelectedIndex { get; set; }

        public DevExpress.Xpf.Core.HeaderLocation HeaderLocation { get; set; }

        protected TabPanelContainer PanelContainer { get; }

        protected FrameworkElement ControlBox { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TabPanelBase.<>c <>9;
            public static Func<TabPanelContainer, DXBorder> <>9__14_0;

            static <>c();
            internal void <.cctor>b__29_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal DXBorder <get_ControlBox>b__14_0(TabPanelContainer x);
        }
    }
}

