namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Documents;

    public class PositionedAdornerContainer : AdornerContainer
    {
        private Point position;

        public PositionedAdornerContainer(UIElement adornedElement, UIElement child) : base(adornedElement, child)
        {
        }

        protected override Size ArrangeOverride(Size finalSize)
        {
            base.Child.Arrange(new Rect(this.position, base.Child.DesiredSize));
            return finalSize;
        }

        protected override Size MeasureOverride(Size constraint) => 
            LayoutHelper.MeasureElementWithSingleChild(this, constraint);

        public void UpdateLocation(Point newPos)
        {
            if (!this.position.Equals(newPos))
            {
                this.position = newPos;
                AdornerLayer parent = base.Parent as AdornerLayer;
                if (parent != null)
                {
                    parent.Update(base.AdornedElement);
                }
            }
        }

        public Point Position =>
            this.position;
    }
}

