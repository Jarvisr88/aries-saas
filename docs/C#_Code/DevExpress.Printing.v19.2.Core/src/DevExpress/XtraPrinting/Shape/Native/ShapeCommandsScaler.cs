namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Drawing;

    public class ShapeCommandsScaler : ShapeCommandsTransformer
    {
        private float scaleFactorX;
        private float scaleFactorY;

        public ShapeCommandsScaler(PointF scaleCenter, float scaleFactorX, float scaleFactorY) : base(scaleCenter)
        {
            this.scaleFactorX = scaleFactorX;
            this.scaleFactorY = scaleFactorY;
        }

        private static float ScaleValue(float value, float scaleFactor) => 
            value * scaleFactor;

        protected override void TransformPoint(ShapePointsCommand command, SizeF vector, int i)
        {
            command.Points[i] = new PointF(base.center.X + ScaleValue(vector.Width, this.scaleFactorX), base.center.Y + ScaleValue(vector.Height, this.scaleFactorY));
        }
    }
}

