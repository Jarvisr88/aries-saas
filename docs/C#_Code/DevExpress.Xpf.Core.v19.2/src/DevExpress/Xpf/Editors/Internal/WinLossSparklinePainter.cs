namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Editors;
    using System;

    public class WinLossSparklinePainter : BarSparklinePainter
    {
        protected override double GetBarValue(double value) => 
            (double) Math.Sign(value);

        public override SparklineViewType SparklineType =>
            SparklineViewType.WinLoss;
    }
}

