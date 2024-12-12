namespace DevExpress.Xpf.Core.Native
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Collections.Generic;
    using System.Collections.Specialized;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Controls;

    public abstract class DXPanelBase : Panel
    {
        public static readonly DependencyProperty ChildrenSourceProperty;
        public static readonly DependencyProperty OrientationProperty;

        static DXPanelBase();
        protected DXPanelBase();
        protected virtual Size AfterArrangeOverride(Size avSize);
        protected virtual Size AfterMeasureOverride(Size avSize);
        protected void ArrangeChild(FrameworkElement child, Rect rect);
        protected sealed override Size ArrangeOverride(Size avSize);
        protected abstract Size ArrangeOverrideCore(Rect avRect);
        protected virtual Size BeforeArrangeOverride(Size avSize);
        protected virtual Size BeforeMeasureOverride(Size avSize);
        private Rect CorrectRect(Rect rect);
        private Size CorrectSize(Size size);
        private IEnumerable<FrameworkElement> FilterChildren(Func<FrameworkElement, bool> filter);
        protected internal FrameworkElement GetActiveChild();
        protected Size GetActualSize(FrameworkElement child);
        protected Size GetDesiredSize(FrameworkElement child);
        protected Size GetMaxSize(FrameworkElement child);
        protected Size GetMinSize(FrameworkElement child, bool includeMargin = false);
        protected double GetRenderHeight(FrameworkElement child);
        protected double GetRenderWidth(FrameworkElement child);
        protected Size GetSize(FrameworkElement child);
        protected double GetStretchCoef(double avWidth, IEnumerable<FrameworkElement> childs, FrameworkElement child);
        protected double GetSumChildActualWidth(IEnumerable<FrameworkElement> childs);
        protected double GetSumChildDesireWidth(IEnumerable<FrameworkElement> childs);
        protected virtual int GetVisibleIndex(FrameworkElement child);
        protected void MeasureChild(FrameworkElement child, Size size);
        protected sealed override Size MeasureOverride(Size avSize);
        protected abstract Size MeasureOverrideCore(Size avSize);
        protected virtual void OnChildrenSourceChanged(IEnumerable<FrameworkElement> oldValue, IEnumerable<FrameworkElement> newValue);
        protected virtual void OnChildrenSourceCollectionChanged(object sender, NotifyCollectionChangedEventArgs e);
        protected override void OnVisualChildrenChanged(DependencyObject visualAdded, DependencyObject visualRemoved);
        protected virtual void UpdateVisibleChildren();

        public IEnumerable<FrameworkElement> ChildrenSource { get; set; }

        public System.Windows.Controls.Orientation Orientation { get; set; }

        protected SizeHelperBase OrientationHelper { get; }

        protected IEnumerable<FrameworkElement> VisibleChildren { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly DXPanelBase.<>c <>9;
            public static Func<FrameworkElement, bool> <>9__17_0;
            public static Func<bool> <>9__19_1;

            static <>c();
            internal void <.cctor>b__44_0(DependencyObject d, DependencyPropertyChangedEventArgs e);
            internal bool <FilterChildren>b__19_1();
            internal bool <UpdateVisibleChildren>b__17_0(FrameworkElement x);
        }
    }
}

