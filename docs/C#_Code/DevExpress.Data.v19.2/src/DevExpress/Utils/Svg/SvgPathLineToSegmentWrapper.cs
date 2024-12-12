namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing.Drawing2D;

    public class SvgPathLineToSegmentWrapper : SvgPathSegmentWrapper
    {
        public SvgPathLineToSegmentWrapper(SvgPathSegment segment) : base(segment)
        {
        }

        public override void AddToPath(GraphicsPath path, double scale)
        {
            path.AddLine(base.ScaleValue(this.Line.Start, scale), base.ScaleValue(this.Line.End, scale));
        }

        public SvgPathLineToSegment Line =>
            base.Segment as SvgPathLineToSegment;
    }
}

