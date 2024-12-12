namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Drawing;

    public class ShapeLineCommand : ShapePointsCommand
    {
        private const int startPointIndex = 0;
        private const int endPointIndex = 1;

        public ShapeLineCommand(PointF startPoint, PointF endPoint) : this(startPoint, endPoint, 2)
        {
        }

        protected ShapeLineCommand(PointF startPoint, PointF endPoint, int pointCount) : base(pointCount)
        {
            this.StartPoint = startPoint;
            this.EndPoint = endPoint;
        }

        public override void Accept(IShapeCommandVisitor visitor)
        {
            visitor.VisitShapeLineCommand(this);
        }

        public PointF StartPoint
        {
            get => 
                base.Points[0];
            set => 
                base.Points[0] = value;
        }

        public PointF EndPoint
        {
            get => 
                base.Points[1];
            set => 
                base.Points[1] = value;
        }

        public float Length =>
            (float) Math.Sqrt((double) (((this.StartPoint.X - this.EndPoint.X) * (this.StartPoint.X - this.EndPoint.X)) + ((this.StartPoint.Y - this.EndPoint.Y) * (this.StartPoint.Y - this.EndPoint.Y))));
    }
}

