namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using DevExpress.XtraPrinting;
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class BrickLayoutDataBase : ILayoutData
    {
        private VisualBrick fBrick;
        private RectangleF initialRect;
        protected float dpi;

        public BrickLayoutDataBase(VisualBrick brick, float dpi);
        protected virtual void AnchorChildren(float delta, float dpi);
        void ILayoutData.Anchor(float delta, float dpi);
        protected RectangleF GetBrickBounds();
        public virtual void SetBoundsY(float y);
        public virtual void UpdateViewBounds();

        public VisualBrick Brick { get; }

        VerticalAnchorStyles ILayoutData.AnchorVertical { get; }

        public virtual bool NeedAdjust { get; }

        public virtual List<ILayoutData> ChildrenData { get; }

        protected int ChildrenDataCount { get; }

        public float Top { get; }

        public float Bottom { get; }

        RectangleF ILayoutData.InitialRect { get; }
    }
}

