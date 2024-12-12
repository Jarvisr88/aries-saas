namespace DevExpress.Xpf.PdfViewer
{
    using DevExpress.Mvvm;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Windows;

    public class SelectionRectangle : BindableBase
    {
        public SelectionRectangle(Point startPoint, Size viewportSize)
        {
            this.ViewportSize = viewportSize;
            this.SetStartPoint(startPoint);
        }

        public void AddXOffset(double dx)
        {
            this.SetAnchorPoint(new Point(this.AnchorPoint.X + dx, this.AnchorPoint.Y));
        }

        public void AddYOffset(double dy)
        {
            this.SetAnchorPoint(new Point(this.AnchorPoint.X, this.AnchorPoint.Y + dy));
        }

        private Point CoerceAnchorPoint(Point point) => 
            new Point(Math.Max(-this.HorizontalOffset, Math.Min(point.X, this.ViewportSize.Width - this.HorizontalOffset)), Math.Max(-this.VerticalOffset, Math.Min(point.Y, this.ViewportSize.Height - this.VerticalOffset)));

        public void Reset()
        {
            this.SetAnchorPoint(this.StartPoint);
        }

        private void SetAnchorPoint(Point p)
        {
            this.AnchorPoint = this.CoerceAnchorPoint(p);
            base.RaisePropertiesChanged<double, double, double, double>(System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(SelectionRectangle)), (MethodInfo) methodof(SelectionRectangle.get_X)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(SelectionRectangle)), (MethodInfo) methodof(SelectionRectangle.get_Y)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(SelectionRectangle)), (MethodInfo) methodof(SelectionRectangle.get_Width)), new ParameterExpression[0]), System.Linq.Expressions.Expression.Lambda<Func<double>>(System.Linq.Expressions.Expression.Property(System.Linq.Expressions.Expression.Constant(this, typeof(SelectionRectangle)), (MethodInfo) methodof(SelectionRectangle.get_Height)), new ParameterExpression[0]));
        }

        public void SetHorizontalOffset(double offset, bool coerceStart)
        {
            double num = offset - this.HorizontalOffset;
            this.HorizontalOffset = offset;
            if (coerceStart)
            {
                this.StartPoint = new Point(this.StartPoint.X - num, this.StartPoint.Y);
            }
        }

        public void SetPointPosition(Point point)
        {
            this.SetAnchorPoint(point);
        }

        public void SetStartPoint(Point point)
        {
            this.StartPoint = point;
            this.Reset();
        }

        public void SetVerticalOffset(double offset, bool coerceStart)
        {
            double num = offset - this.VerticalOffset;
            this.VerticalOffset = offset;
            if (coerceStart)
            {
                this.StartPoint = new Point(this.StartPoint.X, this.StartPoint.Y - num);
            }
        }

        public void SetViewport(Size size)
        {
            this.ViewportSize = size;
            this.SetAnchorPoint(this.AnchorPoint);
        }

        public double Width =>
            Math.Abs((double) (this.StartPoint.X - this.AnchorPoint.X));

        public double Height =>
            Math.Abs((double) (this.StartPoint.Y - this.AnchorPoint.Y));

        public double X =>
            Math.Min(this.StartPoint.X, this.AnchorPoint.X);

        public double Y =>
            Math.Min(this.StartPoint.Y, this.AnchorPoint.Y);

        public bool IsEmpty =>
            this.Width.AreClose(0.0) && this.Height.AreClose(0.0);

        public Rect Rectangle =>
            new Rect(this.X + this.HorizontalOffset, this.Y + this.VerticalOffset, this.Width, this.Height);

        private Size ViewportSize { get; set; }

        public Point StartPoint { get; private set; }

        public Point AnchorPoint { get; private set; }

        private double HorizontalOffset { get; set; }

        private double VerticalOffset { get; set; }
    }
}

