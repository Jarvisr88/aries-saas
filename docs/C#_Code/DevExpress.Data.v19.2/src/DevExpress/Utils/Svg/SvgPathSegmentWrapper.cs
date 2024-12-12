namespace DevExpress.Utils.Svg
{
    using DevExpress.Data.Svg;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class SvgPathSegmentWrapper
    {
        private SvgPathSegment segmentCore;

        public SvgPathSegmentWrapper(SvgPathSegment segment)
        {
            this.segmentCore = segment;
        }

        public virtual void AddToPath(GraphicsPath path, double scale)
        {
        }

        public PointF ScaleValue(SvgPoint point, double scale) => 
            (scale != 1.0) ? new PointF((float) (point.X * scale), (float) (point.Y * scale)) : new PointF((float) point.X, (float) point.Y);

        public SvgPathSegment Segment =>
            this.segmentCore;
    }
}

