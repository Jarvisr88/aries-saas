namespace DevExpress.XtraPrinting.Native.LayoutAdjustment
{
    using DevExpress.XtraReports.UI;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public interface ILayoutData
    {
        void Anchor(float delta, float dpi);
        void SetBoundsY(float y);
        void UpdateViewBounds();

        VerticalAnchorStyles AnchorVertical { get; }

        bool NeedAdjust { get; }

        float Top { get; }

        float Bottom { get; }

        RectangleF InitialRect { get; }

        List<ILayoutData> ChildrenData { get; }
    }
}

