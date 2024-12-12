namespace DevExpress.Office.Drawing
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class PathInfo : PathInfoBase
    {
        public PathInfo(GraphicsPath graphicsPath, Brush fill, bool stroke) : base(graphicsPath, fill, stroke)
        {
        }

        public PathInfo(GraphicsPath graphicsPath, Brush fill, bool stroke, bool hasPermanentFill) : base(graphicsPath, fill, stroke, hasPermanentFill)
        {
        }

        public override bool AllowHitTest =>
            true;
    }
}

