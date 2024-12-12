namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using System;
    using System.Collections.Generic;

    public class LayoutAdjusterWithAnchoring : LayoutAdjuster
    {
        public LayoutAdjusterWithAnchoring(float dpi);
        protected override void Adjust(List<ILayoutData> layoutData);
    }
}

