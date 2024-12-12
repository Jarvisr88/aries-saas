namespace DevExpress.Xpf.Grid
{
    using System;

    internal class ScrollAccumulator
    {
        public const double ScrollLineSize = 120.0;
        private double accumulator;
        private int direction;

        private void ChangeDirection(double delta)
        {
            if (Math.Sign(delta) != this.direction)
            {
                this.direction = Math.Sign(delta);
                this.accumulator = 0.0;
            }
        }

        public double GetCorrectedDelta(bool useAccumulator, double elementSize, double delta)
        {
            this.ChangeDirection(delta);
            double num = elementSize;
            double a = -delta / num;
            if (useAccumulator)
            {
                double num3 = a - ((int) a);
                a = Math.Round(a);
                this.accumulator += Math.Abs(num3);
                if (Math.Abs(this.accumulator) >= 1.0)
                {
                    this.accumulator--;
                    a -= this.direction;
                }
            }
            return a;
        }
    }
}

