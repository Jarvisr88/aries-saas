namespace DevExpress.Xpf.Core.Native
{
    using System;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;
    using System.Windows.Media.Animation;

    public class TabPanelScrollView : TabPanelScrollViewBase
    {
        public static readonly DependencyProperty AllowAnimationProperty;
        public static readonly DependencyProperty TransparencySizeProperty;
        private DoubleAnimation scrollAnimation;
        private TranslateTransform Transform;
        private System.Windows.Media.Animation.Storyboard Storyboard;
        private double offset;
        internal bool disableAnimation;

        static TabPanelScrollView();
        public TabPanelScrollView();
        protected override Size AfterArrangeOverride(Size avSize);
        private void ApplyAnimation(double value);
        internal bool CanScrollTo(int index);
        public bool CanScrollTo(FrameworkElement child);
        internal FrameworkElement GetFirstFullVisibleItem();
        internal int GetFirstFullVisibleItemIndex();
        internal double GetHeaderOffset(int index);
        internal FrameworkElement GetLastFullVisibleItem();
        internal double GetOffset(FrameworkElement child);
        internal double GetRightOffset(FrameworkElement child);
        private bool HandleScrollBoxChanged(bool allowAnimation);
        protected override void OnHeaderLocationChanged();
        private void OnLoaded(object sender, RoutedEventArgs e);
        private void OnOffsetChanged(double oldValue);
        private void OnTransformUpdated(object sender, EventArgs e);
        public void ScrollNext(bool allowAnimation = true);
        public void ScrollPrev(bool allowAnimation = true);
        internal void ScrollTo(int index);
        public void ScrollTo(FrameworkElement child, bool allowAnimation = true);
        public void ScrollToBegin(bool allowAnimation = true);
        public void ScrollToEnd(bool allowAnimation = true);
        private void UpdateAnimation();

        public bool AllowAnimation { get; set; }

        public int TransparencySize { get; set; }

        public DoubleAnimation ScrollAnimation { get; set; }

        public bool CanScroll { get; }

        public bool CanScrollPrev { get; }

        public bool CanScrollNext { get; }

        internal double VisibleLength { get; }

        internal double PanelLength { get; }

        internal double MinOffset { get; }

        internal double MaxOffset { get; }

        internal double Offset { get; set; }

        internal double RightOffset { get; set; }
    }
}

