namespace DevExpress.XtraPrinting.Shape.Native
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Drawing;

    public abstract class BoundedCommandsRotator
    {
        private ShapeCommandCollection commands;
        private int angleInDeg;
        private RectangleF bounds;
        private PointF center;

        protected BoundedCommandsRotator(ShapeCommandCollection commands, RectangleF bounds, int angleInDeg)
        {
            this.commands = commands;
            this.angleInDeg = angleInDeg;
            this.bounds = bounds;
            this.center = RectHelper.CenterOf(bounds);
        }

        private static float CalcExcess(float begin, float end, float max, float min)
        {
            float num = begin - min;
            float num2 = max - end;
            float num3 = Math.Max(Math.Abs(num), Math.Abs(num2));
            if ((num > 0f) || (num2 > 0f))
            {
                num3 = -num3;
            }
            return num3;
        }

        private float CalcScaleFactor(float begin, float end, float max, float min)
        {
            if (max == min)
            {
                return this.ZeroScaleFactor;
            }
            float num = CalcExcess(begin, end, max, min);
            float num2 = (end - begin) / 2f;
            return ((Math.Abs((float) (num2 - num)) >= 0.0001) ? (num2 / (num2 - num)) : this.ZeroScaleFactor);
        }

        private void CorrectCenter()
        {
            CriticalPointsCalculator criticalPointsCalculator = this.commands.GetCriticalPointsCalculator();
            float x = this.center.X - ((criticalPointsCalculator.MaxX + criticalPointsCalculator.MinX) / 2f);
            this.commands.Offset(new PointF(x, this.center.Y - ((criticalPointsCalculator.MaxY + criticalPointsCalculator.MinY) / 2f)));
        }

        protected abstract void CorrectScale(float scalingFactorX, float scalingFactorY);
        private void Rotate()
        {
            this.commands.RotateAt(this.center, this.angleInDeg);
            this.CorrectCenter();
            CriticalPointsCalculator criticalPointsCalculator = this.commands.GetCriticalPointsCalculator();
            float scalingFactorX = this.CalcScaleFactor(this.bounds.Left, this.bounds.Right, criticalPointsCalculator.MaxX, criticalPointsCalculator.MinX);
            this.CorrectScale(scalingFactorX, this.CalcScaleFactor(this.bounds.Top, this.bounds.Bottom, criticalPointsCalculator.MaxY, criticalPointsCalculator.MinY));
        }

        public static void Rotate(ShapeCommandCollection commands, RectangleF bounds, int angleInDeg, bool stretch)
        {
            (!stretch ? ((BoundedCommandsRotator) new BoundedCommandsUnstretchRotator(commands, bounds, angleInDeg)) : ((BoundedCommandsRotator) new BoundedCommandsStretchRotator(commands, bounds, angleInDeg))).Rotate();
        }

        protected void ScaleAtCenter(float correctCoeffX, float correctCoeffY)
        {
            this.commands.ScaleAt(this.center, correctCoeffX, correctCoeffY);
        }

        protected abstract float ZeroScaleFactor { get; }
    }
}

