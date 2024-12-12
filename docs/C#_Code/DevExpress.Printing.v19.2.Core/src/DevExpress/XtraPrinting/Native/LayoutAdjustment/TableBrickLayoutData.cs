namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Collections.Generic;

    public class TableBrickLayoutData : VisualBrickLayoutData
    {
        public TableBrickLayoutData(VisualBrick brick, float dpi);
        protected override void AddToChildrenData(VisualBrick item, List<ILayoutData> childrenData, float dpi);
        protected override void AnchorChildren(float delta, float dpi);
    }
}

