namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public abstract class SparklineMappingBase
    {
        private readonly DevExpress.Xpf.Editors.Internal.Bounds bounds;
        private readonly InternalRange argumentRange;
        private readonly InternalRange valueRange;
        private readonly double scaleX;
        private readonly double yZeroValue;
        private readonly double screenYZeroValue;
        private readonly double maxDrawingArgument;
        private readonly double minDrawingArgument;

        public SparklineMappingBase(IList<SparklinePoint> sortedPoints, DevExpress.Xpf.Editors.Internal.Bounds bounds, InternalRange argumentRange, InternalRange valueRange)
        {
            this.bounds = bounds;
            this.argumentRange = argumentRange;
            this.valueRange = valueRange;
            this.minDrawingArgument = Math.Min(this.StartArgument, this.EndArgument);
            this.maxDrawingArgument = Math.Max(this.StartArgument, this.EndArgument);
            double minPointDistance = this.GetMinPointDistance(sortedPoints, argumentRange);
            if (valueRange.Auto)
            {
                this.CorrectValueRange(valueRange);
            }
            if (argumentRange.Auto)
            {
                this.CorrectArgumentRange(argumentRange, minPointDistance);
            }
            this.scaleX = this.CalculateScaleX();
            this.yZeroValue = this.CalculateZeroValue();
            this.screenYZeroValue = this.MapYValueToScreen(this.yZeroValue);
            this.MinPointsDistancePx = this.scaleX * minPointDistance;
        }

        private double CalculateScaleX() => 
            (this.EndArgument != this.StartArgument) ? (((double) this.Bounds.Width) / (this.EndArgument - this.StartArgument)) : 0.0;

        protected virtual double CalculateZeroValue() => 
            (this.MinValue > 0.0) ? this.MinValue : ((this.MaxValue < 0.0) ? this.MaxValue : 0.0);

        protected virtual void CorrectArgumentRange(InternalRange argumentRange, double minPointDistance)
        {
        }

        protected virtual void CorrectValueRange(InternalRange valueRange)
        {
            if (valueRange.Min == valueRange.Max)
            {
                valueRange.CorrectionMin = -0.5;
                valueRange.CorrectionMax = 0.5;
            }
        }

        public static SparklineMappingBase CreateMapping(SparklineViewType viewType, IList<SparklinePoint> sortedPoints, DevExpress.Xpf.Editors.Internal.Bounds bounds, InternalRange argumentRange, InternalRange valueRange)
        {
            if ((bounds.Width > 0) && (bounds.Height > 0))
            {
                switch (viewType)
                {
                    case SparklineViewType.Line:
                    case SparklineViewType.Area:
                        return new LineSparklineMapping(sortedPoints, bounds, argumentRange, valueRange);

                    case SparklineViewType.Bar:
                        return new BarSparklineMapping(sortedPoints, bounds, argumentRange, valueRange);

                    case SparklineViewType.WinLoss:
                        return new WinLossSparklineMapping(sortedPoints, bounds, argumentRange, valueRange);
                }
            }
            return null;
        }

        private double GetMinPointDistance(IList<SparklinePoint> ActualPoints, InternalRange argumentRange)
        {
            if (ActualPoints.Count == 0)
            {
                return 0.0;
            }
            double naN = double.NaN;
            for (int i = 1; i < ActualPoints.Count; i++)
            {
                double argument = ActualPoints[i].Argument;
                double num4 = ActualPoints[i - 1].Argument;
                if (argumentRange.ContainsValue(argument) || argumentRange.ContainsValue(num4))
                {
                    double num5 = argument - num4;
                    if (double.IsNaN(naN))
                    {
                        naN = num5;
                    }
                    else if (naN > num5)
                    {
                        naN = num5;
                    }
                }
            }
            return (!double.IsNaN(naN) ? naN : 0.5);
        }

        public bool IsArgumentVisible(double argument) => 
            (argument >= this.minDrawingArgument) && (argument <= this.maxDrawingArgument);

        public bool IsValueVisible(double value) => 
            (value >= this.MinValue) && (value <= this.MaxValue);

        public double MapXValueToScreen(double value) => 
            this.Bounds.X + ((value - this.StartArgument) * this.ScaleX);

        public double MapYValueToScreen(double value) => 
            this.Bounds.Y + (((this.MaxValue - value) / (this.MaxValue - this.MinValue)) * this.Bounds.Height);

        protected double StartArgument =>
            this.argumentRange.ActualMin;

        protected double EndArgument =>
            this.argumentRange.ActualMax;

        public double MinValue =>
            this.valueRange.ActualMin;

        public double MaxValue =>
            this.valueRange.ActualMax;

        public double MinPointsDistancePx { get; private set; }

        public double ScreenYZeroValue =>
            this.screenYZeroValue;

        public double ScaleX =>
            this.scaleX;

        public DevExpress.Xpf.Editors.Internal.Bounds Bounds =>
            this.bounds;
    }
}

