namespace DevExpress.Xpf.Editors
{
    using System;
    using System.Runtime.CompilerServices;

    public class SparklinePoint
    {
        public SparklinePoint()
        {
        }

        public SparklinePoint(double argument, double value)
        {
            this.Argument = argument;
            this.Value = value;
            this.ArgumentScaleType = SparklineScaleType.Unknown;
            this.ValueScaleType = SparklineScaleType.Unknown;
        }

        public SparklinePoint(double argument, double value, SparklineScaleType argumentScaleType, SparklineScaleType valueScaleType)
        {
            this.Argument = argument;
            this.Value = value;
            this.ArgumentScaleType = argumentScaleType;
            this.ValueScaleType = valueScaleType;
        }

        public double Argument { get; set; }

        public double Value { get; set; }

        public SparklineScaleType ArgumentScaleType { get; set; }

        public SparklineScaleType ValueScaleType { get; set; }
    }
}

