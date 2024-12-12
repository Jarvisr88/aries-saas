namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;

    public class TableRowLayoutData : VisualBrickLayoutData
    {
        public TableRowLayoutData(VisualBrick brick, float dpi);
        protected override void AddToChildrenData(VisualBrick item, List<ILayoutData> childrenData, float dpi);
    }
}

