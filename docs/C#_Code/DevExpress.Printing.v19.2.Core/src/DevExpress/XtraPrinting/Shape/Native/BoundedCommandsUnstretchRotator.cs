namespace DevExpress.XtraPrinting.Shape.Native
{
    using System;
    using System.Drawing;

    public class BoundedCommandsUnstretchRotator : BoundedCommandsRotator
    {
        public BoundedCommandsUnstretchRotator(ShapeCommandCollection commands, RectangleF bounds, int angleInDeg) : base(commands, bounds, angleInDeg)
        {
        }

        protected override void CorrectScale(float scalingFactorX, float scalingFactorY)
        {
            float correctCoeffX = Math.Min(scalingFactorX, scalingFactorY);
            base.ScaleAtCenter(correctCoeffX, correctCoeffX);
        }

        protected override float ZeroScaleFactor =>
            float.MaxValue;
    }
}

