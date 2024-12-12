namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;

    public class NativeScrollBehavior : ScrollBehaviorBase
    {
        public override bool CanScrollDown(DependencyObject source) => 
            true;

        public override bool CanScrollLeft(DependencyObject source) => 
            true;

        public override bool CanScrollRight(DependencyObject source) => 
            true;

        public override bool CanScrollUp(DependencyObject source) => 
            true;

        public override bool CheckHandlesMouseWheelScrolling(DependencyObject source) => 
            true;

        public override void MouseWheelDown(DependencyObject source, int lineCount)
        {
        }

        public override void MouseWheelLeft(DependencyObject source, int lineCount)
        {
        }

        public override void MouseWheelRight(DependencyObject source, int lineCount)
        {
        }

        public override void MouseWheelUp(DependencyObject source, int lineCount)
        {
        }

        public override bool PreventMouseScrolling(DependencyObject source) => 
            true;

        public override void ScrollToHorizontalOffset(DependencyObject source, double offset)
        {
        }

        public override void ScrollToVerticalOffset(DependencyObject source, double offset)
        {
        }
    }
}

