namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Drawing;

    public class BoundedCommandsStretchRotator : BoundedCommandsRotator
    {
        public BoundedCommandsStretchRotator(ShapeCommandCollection commands, RectangleF bounds, int angleInDeg) : base(commands, bounds, angleInDeg)
        {
        }

        protected override void CorrectScale(float scalingFactorX, float scalingFactorY)
        {
            base.ScaleAtCenter(scalingFactorX, scalingFactorY);
        }

        protected override float ZeroScaleFactor =>
            1f;
    }
}

