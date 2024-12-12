namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Drawing;

    public class ShapeCommandsRotator : ShapeCommandsTransformer
    {
        private float sin;
        private float cos;

        public ShapeCommandsRotator(PointF rotateCenter, int angle) : base(rotateCenter)
        {
            double a = ShapeHelper.DegToRad((float) angle);
            this.sin = (float) Math.Sin(a);
            this.cos = (float) Math.Cos(a);
        }

        protected override void TransformPoint(ShapePointsCommand command, SizeF vector, int i)
        {
            SizeF ef = new SizeF((this.cos * vector.Width) + (this.sin * vector.Height), (-this.sin * vector.Width) + (this.cos * vector.Height));
            command.Points[i] = new PointF(base.center.X + ef.Width, base.center.Y + ef.Height);
        }
    }
}

