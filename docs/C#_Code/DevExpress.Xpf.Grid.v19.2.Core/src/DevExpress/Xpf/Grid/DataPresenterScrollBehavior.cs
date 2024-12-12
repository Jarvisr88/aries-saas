namespace DevExpress.Xpf.Grid
{
    using DevExpress.Xpf.Core;
    using System;
    using System.Windows;

    public class DataPresenterScrollBehavior : ScrollBehaviorBase
    {
        public override bool CanScrollDown(DependencyObject source) => 
            this.GetView(source).CanScrollDown();

        public override bool CanScrollLeft(DependencyObject source) => 
            this.GetView(source).CanScrollLeft();

        public override bool CanScrollRight(DependencyObject source) => 
            this.GetView(source).CanScrollRight();

        public override bool CanScrollUp(DependencyObject source) => 
            this.GetView(source).CanScrollUp();

        public override bool CheckHandlesMouseWheelScrolling(DependencyObject source) => 
            ((FrameworkElement) source).IsVisible;

        protected DataPresenterBase GetDataPresenter(DependencyObject source) => 
            this.GetView(source).DataPresenter;

        private DataViewBase GetView(DependencyObject source) => 
            DataControlBase.GetCurrentView(source);

        public override void MouseWheelDown(DependencyObject source, int lineCount)
        {
            this.GetView(source).MouseWheelDown(lineCount);
        }

        public override void MouseWheelLeft(DependencyObject source, int lineCount)
        {
            this.GetView(source).MouseWheelLeft(lineCount);
        }

        public override void MouseWheelRight(DependencyObject source, int lineCount)
        {
            this.GetView(source).MouseWheelRight(lineCount);
        }

        public override void MouseWheelUp(DependencyObject source, int lineCount)
        {
            this.GetView(source).MouseWheelUp(lineCount);
        }

        public override bool PreventMouseScrolling(DependencyObject source) => 
            false;

        public override void ScrollToHorizontalOffset(DependencyObject source, double offset)
        {
            this.ScrollToHorizontalOffsetCore(source, offset, false);
        }

        protected void ScrollToHorizontalOffsetCore(DependencyObject source, double offset, bool useAccumulator)
        {
            this.GetView(source).ScrollToHorizontalOffset(offset, useAccumulator);
        }

        public override void ScrollToVerticalOffset(DependencyObject source, double offset)
        {
            this.ScrollToVerticalOffsetCore(source, offset, true);
        }

        protected void ScrollToVerticalOffsetCore(DependencyObject source, double offset, bool useAccumulator)
        {
            this.GetView(source).ScrollToVerticalOffset(offset, useAccumulator);
        }
    }
}

