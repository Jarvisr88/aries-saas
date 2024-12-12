namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing.Drawing2D;

    public class SvgPathCurveToCubicSegmentWrapper : SvgPathSegmentWrapper
    {
        public SvgPathCurveToCubicSegmentWrapper(SvgPathSegment segment) : base(segment)
        {
        }

        public override void AddToPath(GraphicsPath path, double scale)
        {
            path.AddBezier(base.ScaleValue(this.CurveToCubicSegment.Start, scale), base.ScaleValue(this.CurveToCubicSegment.FirstAdditionalPoint, scale), base.ScaleValue(this.CurveToCubicSegment.SecondAdditionalPoint, scale), base.ScaleValue(this.CurveToCubicSegment.End, scale));
        }

        public SvgPathCurveToCubicSegment CurveToCubicSegment =>
            base.Segment as SvgPathCurveToCubicSegment;
    }
}

