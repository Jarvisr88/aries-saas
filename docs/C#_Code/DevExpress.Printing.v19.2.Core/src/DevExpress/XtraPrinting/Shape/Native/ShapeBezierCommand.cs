namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Drawing;

    public class ShapeBezierCommand : ShapeLineCommand
    {
        private const int startControlPointIndex = 2;
        private const int endControlPointIndex = 3;

        public ShapeBezierCommand(PointF startPoint, PointF startControlPoint, PointF endControlPoint, PointF endPoint) : base(startPoint, endPoint, 4)
        {
            this.StartControlPoint = startControlPoint;
            this.EndControlPoint = endControlPoint;
        }

        public override void Accept(IShapeCommandVisitor visitor)
        {
            visitor.VisitShapeBezierCommand(this);
        }

        public PointF StartControlPoint
        {
            get => 
                base.Points[2];
            set => 
                base.Points[2] = value;
        }

        public PointF EndControlPoint
        {
            get => 
                base.Points[3];
            set => 
                base.Points[3] = value;
        }
    }
}

