namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class InternalRange
    {
        public InternalRange(double min, double max)
        {
            this.Min = min;
            this.Max = max;
            this.IsSet = false;
            this.CorrectionMin = 0.0;
            this.CorrectionMax = 0.0;
        }

        public InternalRange(double min, double max, bool auto) : this(min, max)
        {
            this.Auto = auto;
        }

        public bool ContainsValue(double value) => 
            (value >= this.Min) && (value <= this.Max);

        public bool IsEqual(InternalRange range) => 
            (range != null) && ((this.Min == range.Min) && ((this.Max == range.Max) && (this.Auto == range.Auto)));

        public void SetScaleTypes(SparklineScaleType scaleType)
        {
            this.ScaleTypeMin = this.ScaleTypeMax = scaleType;
        }

        public bool Auto { get; set; }

        public double Min { get; set; }

        public double Max { get; set; }

        public bool IsSet { get; set; }

        public double CorrectionMin { get; set; }

        public double CorrectionMax { get; set; }

        public double ActualMin =>
            this.Min + this.CorrectionMin;

        public double ActualMax =>
            this.Max + this.CorrectionMax;

        public SparklineScaleType ScaleTypeMin { get; set; }

        public SparklineScaleType ScaleTypeMax { get; set; }
    }
}

