namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using DevExpress.XtraPrinting;
    using System;

    public class TableCellLayoutData : VisualBrickLayoutData
    {
        public TableCellLayoutData(VisualBrick brick, float dpi);

        public override bool NeedAdjust { get; }
    }
}

