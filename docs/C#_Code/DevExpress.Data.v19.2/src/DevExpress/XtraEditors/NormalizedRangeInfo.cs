namespace DevExpress.XtraEditors
{
    using System;
    using System.Runtime.CompilerServices;

    public class NormalizedRangeInfo
    {
        private RangeControlNormalizedRange range;

        public NormalizedRangeInfo();
        public NormalizedRangeInfo(double minimum, double maximum);
        public NormalizedRangeInfo(double minimum, double maximum, RangeControlValidationType type);
        public NormalizedRangeInfo(double minimum, double maximum, RangeControlValidationType type, ChangedBoundType changedBound);

        public RangeControlValidationType Type { get; set; }

        public RangeControlNormalizedRange Range { get; }

        public ChangedBoundType ChangedBound { get; set; }
    }
}

