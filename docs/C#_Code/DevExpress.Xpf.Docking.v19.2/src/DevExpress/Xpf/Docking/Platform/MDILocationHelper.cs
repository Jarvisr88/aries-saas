namespace DevExpress.Xpf.Docking.Platform
{
    using DevExpress.Xpf.Docking;
    using DevExpress.Xpf.Layout.Core;
    using System;
    using System.Windows;

    internal class MDILocationHelper
    {
        private Point startLocation;
        private Point startPoint;

        public MDILocationHelper(IView view, IDockLayoutElement element)
        {
            this.startLocation = DocumentPanel.GetMDILocation(element.Item);
            this.startPoint = view.Adapter.DragService.DragOrigin;
        }

        public Point CalcLocation(Point screenPoint)
        {
            double num = screenPoint.X - this.startPoint.X;
            double num2 = screenPoint.Y - this.startPoint.Y;
            return new Point(this.startLocation.X + num, this.startLocation.Y + num2);
        }
    }
}

