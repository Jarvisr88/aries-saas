namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;
    using System.Windows.Input;

    public class ScrollViewerScrollBehavior : ScrollBehaviorBase
    {
        private static readonly DevExpress.Xpf.Core.Internal.ReflectionHelper ReflectionHelper = new DevExpress.Xpf.Core.Internal.ReflectionHelper();

        public override bool CanScope(DependencyObject source, double delta)
        {
            ScrollViewer scrollViewer = this.GetScrollViewer(source);
            return (this.GetScrollInfo(source) is ISupportScope);
        }

        public override bool CanScrollDown(DependencyObject source)
        {
            ScrollViewer scrollViewer = this.GetScrollViewer(source);
            IScrollInfo scrollInfo = this.GetScrollInfo(source);
            return (((scrollInfo == null) || scrollInfo.CanVerticallyScroll) ? ((scrollViewer.VerticalOffset + scrollViewer.ViewportHeight) < scrollViewer.ExtentHeight) : false);
        }

        public override bool CanScrollLeft(DependencyObject source)
        {
            ScrollViewer scrollViewer = this.GetScrollViewer(source);
            IScrollInfo scrollInfo = this.GetScrollInfo(source);
            return (((scrollInfo == null) || scrollInfo.CanHorizontallyScroll) ? (scrollViewer.HorizontalOffset > 0.0) : false);
        }

        public override bool CanScrollRight(DependencyObject source)
        {
            ScrollViewer scrollViewer = this.GetScrollViewer(source);
            IScrollInfo scrollInfo = this.GetScrollInfo(source);
            return (((scrollInfo == null) || scrollInfo.CanHorizontallyScroll) ? ((scrollViewer.HorizontalOffset + scrollViewer.ViewportWidth) < scrollViewer.ExtentWidth) : false);
        }

        public override bool CanScrollUp(DependencyObject source)
        {
            ScrollViewer scrollViewer = this.GetScrollViewer(source);
            IScrollInfo scrollInfo = this.GetScrollInfo(source);
            return (((scrollInfo == null) || scrollInfo.CanVerticallyScroll) ? (scrollViewer.VerticalOffset > 0.0) : false);
        }

        public override bool CheckHandlesMouseWheelScrolling(DependencyObject source) => 
            ReflectionHelper.GetPropertyValue<bool>(source, "HandlesMouseWheelScrolling", BindingFlags.NonPublic | BindingFlags.Instance);

        private IScrollInfo GetScrollInfo(DependencyObject source) => 
            ReflectionHelper.GetPropertyValue<IScrollInfo>(source, "ScrollInfo", BindingFlags.NonPublic | BindingFlags.Instance);

        private ScrollViewer GetScrollViewer(DependencyObject source) => 
            (ScrollViewer) source;

        public override void MouseWheelDown(DependencyObject source, int lineCount)
        {
            ScrollViewer scrollViewer = this.GetScrollViewer(source);
            IScrollInfo scrollInfo = this.GetScrollInfo(source);
            if (scrollInfo != null)
            {
                base.RepeatAction(new Action(scrollInfo.MouseWheelDown), lineCount);
            }
            else
            {
                base.RepeatAction(new Action(scrollViewer.LineDown), SystemParameters.WheelScrollLines * lineCount);
            }
        }

        public override void MouseWheelLeft(DependencyObject source, int lineCount)
        {
            ScrollViewer scrollViewer = this.GetScrollViewer(source);
            IScrollInfo scrollInfo = this.GetScrollInfo(source);
            if (scrollInfo != null)
            {
                base.RepeatAction(new Action(scrollInfo.MouseWheelLeft), lineCount);
            }
            else
            {
                base.RepeatAction(new Action(scrollViewer.LineLeft), SystemParameters.WheelScrollLines * lineCount);
            }
        }

        public override void MouseWheelRight(DependencyObject source, int lineCount)
        {
            ScrollViewer scrollViewer = this.GetScrollViewer(source);
            IScrollInfo scrollInfo = this.GetScrollInfo(source);
            if (scrollInfo != null)
            {
                base.RepeatAction(new Action(scrollInfo.MouseWheelRight), lineCount);
            }
            else
            {
                base.RepeatAction(new Action(scrollViewer.LineRight), SystemParameters.WheelScrollLines * lineCount);
            }
        }

        public override void MouseWheelUp(DependencyObject source, int lineCount)
        {
            ScrollViewer scrollViewer = this.GetScrollViewer(source);
            IScrollInfo scrollInfo = this.GetScrollInfo(source);
            if (scrollInfo != null)
            {
                base.RepeatAction(new Action(scrollInfo.MouseWheelUp), lineCount);
            }
            else
            {
                base.RepeatAction(new Action(scrollViewer.LineUp), SystemParameters.WheelScrollLines * lineCount);
            }
        }

        public override bool PreventMouseScrolling(DependencyObject source) => 
            false;

        internal override void Scope(DependencyObject source, ScopeEventArgs e)
        {
            ScrollViewer scrollViewer = this.GetScrollViewer(source);
            ISupportScope scrollInfo = this.GetScrollInfo(source) as ISupportScope;
            if ((scrollInfo != null) && (Keyboard.IsKeyDown(Key.LeftCtrl) || Keyboard.IsKeyDown(Key.RightCtrl)))
            {
                scrollInfo.Scope(e);
            }
        }

        public override void ScrollToHorizontalOffset(DependencyObject source, double offset)
        {
            ScrollViewer scrollViewer = this.GetScrollViewer(source);
            IScrollInfo scrollInfo = this.GetScrollInfo(source);
            if (scrollInfo != null)
            {
                scrollInfo.SetHorizontalOffset(scrollInfo.HorizontalOffset + offset);
            }
            else
            {
                scrollViewer.ScrollToHorizontalOffset(scrollViewer.HorizontalOffset + offset);
            }
        }

        public override void ScrollToVerticalOffset(DependencyObject source, double offset)
        {
            ScrollViewer scrollViewer = this.GetScrollViewer(source);
            IScrollInfo scrollInfo = this.GetScrollInfo(source);
            if (scrollInfo != null)
            {
                scrollInfo.SetVerticalOffset(scrollInfo.VerticalOffset - offset);
            }
            else
            {
                scrollViewer.ScrollToVerticalOffset(scrollViewer.VerticalOffset - offset);
            }
        }
    }
}

