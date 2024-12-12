namespace DevExpress.Xpf.Docking.VisualElements
{
    using DevExpress.Xpf.Docking;
    using System;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class psvDecorator : psvPanel
    {
        public static readonly DependencyProperty ChildProperty;

        static psvDecorator()
        {
            new DependencyPropertyRegistrator<psvDecorator>().Register<UIElement>("Child", ref ChildProperty, null, (dObj, e) => ((psvDecorator) dObj).OnChildChanged((UIElement) e.NewValue, (UIElement) e.OldValue), null);
        }

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            Point location = new Point();
            Rect finalRect = new Rect(location, arrangeSize);
            UIElement child = this.Child;
            if (child != null)
            {
                child.Arrange(finalRect);
            }
            return arrangeSize;
        }

        protected override Size MeasureOverride(Size constraint)
        {
            UIElement child = this.Child;
            if (child != null)
            {
                child.Measure(constraint);
                return child.DesiredSize;
            }
            return new Size();
        }

        protected virtual void OnChildChanged(UIElement child, UIElement oldChild)
        {
            if (oldChild != null)
            {
                base.Children.Remove(oldChild);
            }
            if (child != null)
            {
                base.Children.Add(child);
            }
        }

        public UIElement Child
        {
            get => 
                (UIElement) base.GetValue(ChildProperty);
            set => 
                base.SetValue(ChildProperty, value);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly psvDecorator.<>c <>9 = new psvDecorator.<>c();

            internal void <.cctor>b__1_0(DependencyObject dObj, DependencyPropertyChangedEventArgs e)
            {
                ((psvDecorator) dObj).OnChildChanged((UIElement) e.NewValue, (UIElement) e.OldValue);
            }
        }
    }
}

