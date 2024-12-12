namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    internal class LocationHelper
    {
        private Point startLocation;
        private Point startPoint;

        public unsafe LocationHelper(IView view, ILayoutElement element, Point dragOffset)
        {
            this.startLocation = view.ClientToScreen(element.Location);
            this.startPoint = view.Adapter.DragService.DragOrigin;
            Point* pointPtr1 = &this.startPoint;
            pointPtr1.X -= dragOffset.X;
            Point* pointPtr2 = &this.startPoint;
            pointPtr2.Y -= dragOffset.Y;
        }

        public Point CalcLocation(Point screenPoint)
        {
            double num = screenPoint.X - this.startPoint.X;
            double num2 = screenPoint.Y - this.startPoint.Y;
            return new Point(this.startLocation.X + num, this.startLocation.Y + num2);
        }
    }
}

