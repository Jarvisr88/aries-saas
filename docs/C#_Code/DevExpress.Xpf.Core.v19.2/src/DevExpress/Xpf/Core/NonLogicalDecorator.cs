namespace DevExpress.Xpf.Core
{
    using System;
    using System.Windows;
    using System.Windows.Markup;
    using System.Windows.Media;

    [ContentProperty("Child")]
    public class NonLogicalDecorator : FrameworkElement, IAddChild
    {
        private UIElement child;

        protected override Size ArrangeOverride(Size arrangeSize)
        {
            UIElement child = this.Child;
            if (child != null)
            {
                child.Arrange(new Rect(arrangeSize));
            }
            return arrangeSize;
        }

        protected override Visual GetVisualChild(int index)
        {
            if ((this.child == null) || (index != 0))
            {
                throw new ArgumentOutOfRangeException("index");
            }
            return this.child;
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

        void IAddChild.AddChild(object value)
        {
            if (!(value is UIElement))
            {
                throw new ArgumentException("value");
            }
            if (this.Child != null)
            {
                throw new ArgumentException();
            }
            this.Child = (UIElement) value;
        }

        void IAddChild.AddText(string text)
        {
        }

        public virtual UIElement Child
        {
            get => 
                this.child;
            set
            {
                if (!ReferenceEquals(this.child, value))
                {
                    base.RemoveVisualChild(this.child);
                    this.child = value;
                    base.AddVisualChild(value);
                    base.InvalidateMeasure();
                }
            }
        }

        protected override int VisualChildrenCount =>
            (this.child != null) ? 1 : 0;
    }
}

