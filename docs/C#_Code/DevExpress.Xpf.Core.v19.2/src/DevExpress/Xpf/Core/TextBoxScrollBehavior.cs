namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Internal;
    using System;
    using System.Reflection;
    using System.Windows;
    using System.Windows.Controls;
    using System.Windows.Controls.Primitives;

    public class TextBoxScrollBehavior : ScrollBehaviorBase
    {
        public override bool CanScrollDown(DependencyObject source)
        {
            TextBoxScrollInfo info = TextBoxScrollInfo.Create(source);
            return ((info != null) ? (info.TextBox.VerticalOffset < info.ScrollViwer.ScrollableHeight) : false);
        }

        public override bool CanScrollLeft(DependencyObject source) => 
            false;

        public override bool CanScrollRight(DependencyObject source) => 
            false;

        public override bool CanScrollUp(DependencyObject source)
        {
            TextBoxScrollInfo info = TextBoxScrollInfo.Create(source);
            return ((info != null) ? (info.TextBox.VerticalOffset > 0.0) : false);
        }

        public override bool CheckHandlesMouseWheelScrolling(DependencyObject source) => 
            this.CanScrollUp(source) || this.CanScrollDown(source);

        public override void MouseWheelDown(DependencyObject source, int lineCount)
        {
            TextBoxScrollInfo info = TextBoxScrollInfo.Create(source);
            IScrollInfo renderScope = info.RenderScope;
            base.RepeatAction(new Action(renderScope.MouseWheelDown), lineCount);
        }

        public override void MouseWheelLeft(DependencyObject source, int lineCount)
        {
            TextBoxScrollInfo info = TextBoxScrollInfo.Create(source);
            IScrollInfo renderScope = info.RenderScope;
            base.RepeatAction(new Action(renderScope.MouseWheelLeft), lineCount);
        }

        public override void MouseWheelRight(DependencyObject source, int lineCount)
        {
            TextBoxScrollInfo info = TextBoxScrollInfo.Create(source);
            IScrollInfo renderScope = info.RenderScope;
            base.RepeatAction(new Action(renderScope.MouseWheelRight), lineCount);
        }

        public override void MouseWheelUp(DependencyObject source, int lineCount)
        {
            TextBoxScrollInfo info = TextBoxScrollInfo.Create(source);
            IScrollInfo renderScope = info.RenderScope;
            base.RepeatAction(new Action(renderScope.MouseWheelUp), lineCount);
        }

        public override bool PreventMouseScrolling(DependencyObject source) => 
            false;

        public override void ScrollToHorizontalOffset(DependencyObject source, double offset)
        {
            TextBoxScrollInfo info = TextBoxScrollInfo.Create(source);
            info.RenderScope.SetHorizontalOffset(info.RenderScope.HorizontalOffset + offset);
        }

        public override void ScrollToVerticalOffset(DependencyObject source, double offset)
        {
            TextBoxScrollInfo info = TextBoxScrollInfo.Create(source);
            info.RenderScope.SetVerticalOffset(info.RenderScope.HorizontalOffset + offset);
        }

        private class TextBoxScrollInfo
        {
            private static readonly DevExpress.Xpf.Core.Internal.ReflectionHelper ReflectionHelper = new DevExpress.Xpf.Core.Internal.ReflectionHelper();
            private readonly System.Windows.Controls.TextBox textBox;
            private readonly ScrollViewer scrollViwer;
            private readonly IScrollInfo renderScope;

            private TextBoxScrollInfo(System.Windows.Controls.TextBox textBox, ScrollViewer scrollViwer, IScrollInfo renderScope)
            {
                this.textBox = textBox;
                this.scrollViwer = scrollViwer;
                this.renderScope = renderScope;
            }

            public static TextBoxScrollBehavior.TextBoxScrollInfo Create(DependencyObject source)
            {
                System.Windows.Controls.TextBox entity = source as System.Windows.Controls.TextBox;
                if (entity == null)
                {
                    return null;
                }
                ScrollViewer scrollViwer = ReflectionHelper.GetPropertyValue<ScrollViewer>(entity, "ScrollViewer", BindingFlags.NonPublic | BindingFlags.Instance);
                if (scrollViwer == null)
                {
                    return null;
                }
                IScrollInfo renderScope = ReflectionHelper.GetPropertyValue<FrameworkElement>(entity, "RenderScope", BindingFlags.NonPublic | BindingFlags.Instance) as IScrollInfo;
                return ((renderScope != null) ? new TextBoxScrollBehavior.TextBoxScrollInfo(entity, scrollViwer, renderScope) : null);
            }

            public System.Windows.Controls.TextBox TextBox =>
                this.textBox;

            public ScrollViewer ScrollViwer =>
                this.scrollViwer;

            public IScrollInfo RenderScope =>
                this.renderScope;
        }
    }
}

