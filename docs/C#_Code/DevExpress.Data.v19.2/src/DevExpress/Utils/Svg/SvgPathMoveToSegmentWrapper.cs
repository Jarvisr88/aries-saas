namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing.Drawing2D;

    public class SvgPathMoveToSegmentWrapper : SvgPathSegmentWrapper
    {
        public SvgPathMoveToSegmentWrapper(SvgPathSegment segment) : base(segment)
        {
        }

        public override void AddToPath(GraphicsPath path, double scale)
        {
            path.StartFigure();
        }
    }
}

