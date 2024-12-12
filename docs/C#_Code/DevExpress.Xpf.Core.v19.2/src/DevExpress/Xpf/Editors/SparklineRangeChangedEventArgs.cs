namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using System;
    using System.Runtime.CompilerServices;

    public class SparklineRangeChangedEventArgs : EventArgs
    {
        public SparklineRangeChangedEventArgs(InternalRange range)
        {
            this.MinValue = SparklineMathUtils.ConvertToNative(range.Min, range.ScaleTypeMin);
            this.MaxValue = SparklineMathUtils.ConvertToNative(range.Max, range.ScaleTypeMax);
        }

        public SparklineRangeChangedEventArgs(object minValue, object maxValue)
        {
            this.MinValue = minValue;
            this.MaxValue = maxValue;
        }

        public object MinValue { get; private set; }

        public object MaxValue { get; private set; }
    }
}

