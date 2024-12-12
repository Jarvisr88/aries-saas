namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class LayoutDataContainer : ILayoutData
    {
        private List<ILayoutData> childrenData;
        private float initialBottom;
        private float bottomSpan;
        private float bottom;

        public LayoutDataContainer(List<ILayoutData> childrenData);
        public LayoutDataContainer(List<ILayoutData> childrenData, float bottomSpan);
        public void Anchor(float delta, float dpi);
        private float CalcBottom();
        private float CalculateContainerHeight();
        public void SetBoundsY(float y);
        public void UpdateViewBounds();

        RectangleF ILayoutData.InitialRect { get; }

        public VerticalAnchorStyles AnchorVertical { get; }

        public bool NeedAdjust { get; }

        public float Top { get; }

        public float Bottom { get; }

        public List<ILayoutData> ChildrenData { get; }
    }
}

