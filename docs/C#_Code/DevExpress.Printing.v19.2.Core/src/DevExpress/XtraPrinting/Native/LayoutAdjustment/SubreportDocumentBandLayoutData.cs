namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using DevExpress.XtraPrinting.Native;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class SubreportDocumentBandLayoutData : ILayoutData
    {
        private ISubreportDocumentBand docBand;
        private float bottom;
        private float top;
        protected LayoutDataContext context;
        private RectangleF initialRect;

        public SubreportDocumentBandLayoutData(ISubreportDocumentBand docBand, LayoutDataContext context, RectangleF initialRect);
        public virtual void Anchor(float delta, float dpi);
        void ILayoutData.SetBoundsY(float y);
        public virtual void FillPage();
        public virtual void UpdateViewBounds();

        VerticalAnchorStyles ILayoutData.AnchorVertical { get; }

        bool ILayoutData.NeedAdjust { get; }

        public float Top { get; }

        public float Bottom { get; set; }

        public RectangleF InitialRect { get; }

        List<ILayoutData> ILayoutData.ChildrenData { get; }

        public DevExpress.XtraPrinting.Native.DocumentBand DocumentBand { get; }
    }
}

