namespace DevExpress.Xpf.Core
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Windows;
    using System.Windows.Documents;
    using System.Windows.Media;

    public class AdornerContainer : Adorner
    {
        private UIElement child;

        public AdornerContainer(UIElement adornedElement, UIElement child) : base(adornedElement)
        {
            base.AddVisualChild(child);
            this.child = child;
        }

        protected override Size ArrangeOverride(Size finalSize) => 
            LayoutHelper.ArrangeElementWithSingleChild(this, finalSize);

        protected override Visual GetVisualChild(int index) => 
            this.child;

        internal UIElement Child =>
            this.child;

        protected override int VisualChildrenCount =>
            1;
    }
}

