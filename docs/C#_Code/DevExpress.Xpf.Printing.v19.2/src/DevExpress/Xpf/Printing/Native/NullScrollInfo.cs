namespace DevExpress.Xpf.Printing.Native
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    internal class NullScrollInfo : ScrollInfoBase
    {
        public NullScrollInfo(FrameworkElement scrollablePageView) : base(scrollablePageView, null, thickness)
        {
            Thickness thickness = new Thickness();
        }

        public override double GetTransformX()
        {
            throw new InvalidOperationException();
        }

        public override double GetTransformY()
        {
            throw new InvalidOperationException();
        }

        protected override double GetVerticalScrollOffset(ScrollInfoBase.ScrollMode scrollMode, ScrollInfoBase.ScrollDirection scrollDirection) => 
            0.0;

        public override Rect MakeVisible(Visual visual, Rect rectangle) => 
            rectangle;

        public override void SetCurrentPageIndex()
        {
            throw new InvalidOperationException();
        }

        public override double ExtentHeight =>
            0.0;

        protected override double ScrollablePageViewHeight =>
            0.0;
    }
}

