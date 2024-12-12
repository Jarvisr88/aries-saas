namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Markup;

    public abstract class ScrollBehaviorBase : MarkupExtension
    {
        protected ScrollBehaviorBase()
        {
        }

        public virtual bool CanScope(DependencyObject source, double delta) => 
            false;

        public abstract bool CanScrollDown(DependencyObject source);
        public abstract bool CanScrollLeft(DependencyObject source);
        public abstract bool CanScrollRight(DependencyObject source);
        public abstract bool CanScrollUp(DependencyObject source);
        public abstract bool CheckHandlesMouseWheelScrolling(DependencyObject source);
        public abstract void MouseWheelDown(DependencyObject source, int lineCount);
        public abstract void MouseWheelLeft(DependencyObject source, int lineCount);
        public abstract void MouseWheelRight(DependencyObject source, int lineCount);
        public abstract void MouseWheelUp(DependencyObject source, int lineCount);
        public abstract bool PreventMouseScrolling(DependencyObject source);
        public override object ProvideValue(IServiceProvider serviceProvider) => 
            this;

        protected void RepeatAction(Action action, int count)
        {
            for (int i = 0; i < count; i++)
            {
                action();
            }
        }

        internal virtual void Scope(DependencyObject source, ScopeEventArgs e)
        {
        }

        public abstract void ScrollToHorizontalOffset(DependencyObject source, double offset);
        public abstract void ScrollToVerticalOffset(DependencyObject source, double offset);
    }
}

