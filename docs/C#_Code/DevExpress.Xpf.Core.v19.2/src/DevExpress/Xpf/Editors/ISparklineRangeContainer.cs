namespace DevExpress.Xpf.Editors
{
    using System;

    public interface ISparklineRangeContainer
    {
        void RaiseArgumentRangeChanged(SparklineRangeChangedEventArgs e);
        void RaiseValueRangeChanged(SparklineRangeChangedEventArgs e);
    }
}

