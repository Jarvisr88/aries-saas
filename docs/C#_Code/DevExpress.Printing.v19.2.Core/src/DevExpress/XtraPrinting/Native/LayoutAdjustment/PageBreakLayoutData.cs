namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Runtime.CompilerServices;

    public class PageBreakLayoutData : UnprintLayoutData
    {
        public PageBreakLayoutData(ValueInfo pageBreak, float dpi);
        public override void SetBoundsY(float y);

        public ValueInfo PageBreak { get; private set; }

        protected override float Y { get; }
    }
}

