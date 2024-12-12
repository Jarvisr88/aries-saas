namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;
    using System.Collections.Generic;

    public class BarSparklineMapping : SparklineMappingBase
    {
        public BarSparklineMapping(IList<SparklinePoint> sortedPoints, Bounds bounds, InternalRange argumentRange, InternalRange valueRange) : base(sortedPoints, bounds, argumentRange, valueRange)
        {
        }

        protected override void CorrectArgumentRange(InternalRange argumentRange, double minPointDistance)
        {
            if (argumentRange.Auto)
            {
                argumentRange.CorrectionMin = -minPointDistance * 0.5;
                argumentRange.CorrectionMax = minPointDistance * 0.5;
            }
        }

        protected override void CorrectValueRange(InternalRange valueRange)
        {
            if ((valueRange.Max >= 0.0) && (valueRange.Min >= 0.0))
            {
                valueRange.CorrectionMin = (valueRange.Max - valueRange.Min) * 0.1;
            }
            else if ((valueRange.Max <= 0.0) && (valueRange.Min <= 0.0))
            {
                valueRange.CorrectionMax = (valueRange.Max - valueRange.Min) * 0.1;
            }
            base.CorrectValueRange(valueRange);
        }
    }
}

