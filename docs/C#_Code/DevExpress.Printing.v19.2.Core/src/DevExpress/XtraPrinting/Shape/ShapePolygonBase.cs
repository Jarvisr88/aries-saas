namespace DevExpress.XtraPrinting.Shape
{
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraPrinting.Shape.Native;
    using System;
    using System.Drawing;

    public abstract class ShapePolygonBase : FilletShapeBase
    {
        private int numberOfSides;

        protected ShapePolygonBase()
        {
            this.numberOfSides = 3;
        }

        protected ShapePolygonBase(ShapePolygonBase source) : base(source)
        {
            this.numberOfSides = 3;
            this.numberOfSides = source.NumberOfSides;
        }

        protected PointF[] CreatePointsCore(RectangleF bounds, double startAngle, float radius)
        {
            double a = startAngle;
            PointF[] tfArray = new PointF[this.numberOfSides];
            PointF tf = RectHelper.CenterOf(bounds);
            for (int i = 0; i < this.numberOfSides; i++)
            {
                tfArray[i] = new PointF(tf.X + (radius * ((float) Math.Sin(a))), tf.Y - (radius * ((float) Math.Cos(a))));
                a += this.AngleStep;
            }
            return tfArray;
        }

        protected double AngleStep =>
            6.2831853071795862 / ((double) this.numberOfSides);

        protected internal int NumberOfSides
        {
            get => 
                this.numberOfSides;
            set => 
                this.numberOfSides = ShapeHelper.ValidateRestrictedValue(value, 3, 0x7fffffff, "NumberOfSide");
        }
    }
}

