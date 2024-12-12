namespace DevExpress.Xpf.Editors
{
    using DevExpress.Xpf.Editors.Internal;
    using System;

    public class WinLossSparklineControl : BarSparklineControlBase
    {
        protected internal override BaseSparklinePainter CreatePainter() => 
            new WinLossSparklinePainter();

        protected override string GetViewName() => 
            EditorLocalizer.GetString(EditorStringId.SparklineViewWinLoss);

        protected internal override bool ActualShowNegativePoint =>
            true;

        public override SparklineViewType Type =>
            SparklineViewType.WinLoss;
    }
}

