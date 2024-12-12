namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Drawing;

    public abstract class ShapePointsCommand : ShapeCommandBase
    {
        private PointF[] points;

        protected ShapePointsCommand(int pointCount)
        {
            this.points = new PointF[pointCount];
        }

        public PointF[] Points =>
            this.points;
    }
}

