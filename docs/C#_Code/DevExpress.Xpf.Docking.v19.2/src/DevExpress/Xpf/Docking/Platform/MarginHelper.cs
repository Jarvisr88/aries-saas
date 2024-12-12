namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Core.Native;
    using DevExpress.Xpf.Docking.VisualElements;
    using System;
    using System.Windows;

    internal class MarginHelper
    {
        private Thickness margin;

        public MarginHelper(LayoutView view)
        {
            this.margin = this.GetContentMargin(view.RootUIElement);
        }

        public MarginHelper(Thickness margin)
        {
            this.margin = margin;
        }

        public Point Correct(Point location) => 
            this.CorrectLocation(this.margin, location);

        public Rect Correct(Rect bounds) => 
            new Rect(this.CorrectLocation(this.margin, bounds.Location()), this.CorrectSize(this.margin, bounds.Size()));

        private Point CorrectLocation(Thickness margins, Point floatLocation) => 
            new Point(floatLocation.X - margins.Left, floatLocation.Y - margins.Top);

        private Size CorrectSize(Thickness margins, Size floatSize) => 
            new Size((floatSize.Width + margins.Left) + margins.Right, (floatSize.Height + margins.Top) + margins.Bottom);

        private Thickness GetContentMargin(object element)
        {
            FloatPanePresenter.FloatingContentPresenter presenter = element as FloatPanePresenter.FloatingContentPresenter;
            if (presenter != null)
            {
                return ((FloatPanePresenter) presenter.Container).GetFloatingMargin();
            }
            return new Thickness();
        }
    }
}

