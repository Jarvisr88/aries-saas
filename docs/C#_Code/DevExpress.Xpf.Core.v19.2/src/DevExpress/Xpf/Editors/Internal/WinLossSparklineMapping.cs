namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;

    public class WinLossSparklineMapping : BarSparklineMapping
    {
        public WinLossSparklineMapping(IList<SparklinePoint> sortedPoints, Bounds bounds, InternalRange argumentRange, InternalRange valueRange) : base(sortedPoints, bounds, argumentRange, valueRange)
        {
        }

        protected override double CalculateZeroValue() => 
            0.0;

        protected override void CorrectValueRange(InternalRange valueRange)
        {
            valueRange.Min = -1.0;
            valueRange.Max = 1.0;
        }
    }
}

