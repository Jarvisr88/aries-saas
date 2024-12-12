namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public abstract class UnprintLayoutData : ILayoutData
    {
        protected float initialY;
        protected float fDpi;

        protected UnprintLayoutData(float dpi);
        void ILayoutData.Anchor(float delta, float dpi);
        void ILayoutData.UpdateViewBounds();
        public abstract void SetBoundsY(float y);

        protected abstract float Y { get; }

        VerticalAnchorStyles ILayoutData.AnchorVertical { get; }

        bool ILayoutData.NeedAdjust { get; }

        float ILayoutData.Top { get; }

        float ILayoutData.Bottom { get; }

        public float InitialTop { get; }

        public float InitialBottom { get; }

        RectangleF ILayoutData.InitialRect { get; }

        List<ILayoutData> ILayoutData.ChildrenData { get; }
    }
}

