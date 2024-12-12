namespace DevExpress.Office.Drawing
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class DrawingVisualHost : FrameworkElement
    {
        private readonly VisualCollection items;

        public DrawingVisualHost()
        {
            this.items = new VisualCollection(this);
        }

        protected override Visual GetVisualChild(int index)
        {
            if ((index < 0) || (index >= this.items.Count))
            {
                throw new IndexOutOfRangeException();
            }
            return this.items[index];
        }

        public VisualCollection Items =>
            this.items;

        protected override int VisualChildrenCount =>
            this.items.Count;
    }
}

