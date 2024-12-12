namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing.Drawing2D;

    public class SvgPathCurveToQuadraticSegmentWrapper : SvgPathSegmentWrapper
    {
        public SvgPathCurveToQuadraticSegmentWrapper(SvgPathSegment segment) : base(segment)
        {
        }

        public override void AddToPath(GraphicsPath path, double scale)
        {
            path.AddBezier(base.ScaleValue(this.CurveToQuadraticSegment.Start, scale), base.ScaleValue(this.CurveToQuadraticSegment.FirstAdditionalPoint, scale), base.ScaleValue(this.CurveToQuadraticSegment.SecondAdditionalPoint, scale), base.ScaleValue(this.CurveToQuadraticSegment.End, scale));
        }

        public SvgPathCurveToQuadraticSegment CurveToQuadraticSegment =>
            base.Segment as SvgPathCurveToQuadraticSegment;
    }
}

