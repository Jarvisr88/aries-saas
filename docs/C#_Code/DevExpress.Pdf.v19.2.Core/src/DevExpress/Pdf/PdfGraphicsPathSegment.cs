namespace DevExpress.Pdf
{
    using System;
    using System.Collections.Generic;

    public abstract class PdfGraphicsPathSegment
    {
        private readonly PdfPoint endPoint;

        protected PdfGraphicsPathSegment(PdfPoint endPoint)
        {
            this.endPoint = endPoint;
        }

        protected internal virtual void AddSegmentPoints(IList<PdfPoint> points)
        {
            points.Add(this.endPoint);
        }

        protected internal abstract void GeneratePathSegmentCommands(IList<PdfCommand> commands);
        public static PdfGraphicsPathSegment Transform(PdfGraphicsPathSegment segment, PdfTransformationMatrix matrix)
        {
            PdfPoint endPoint = matrix.Transform(segment.EndPoint);
            PdfBezierGraphicsPathSegment segment2 = segment as PdfBezierGraphicsPathSegment;
            return ((segment2 == null) ? ((PdfGraphicsPathSegment) new PdfLineGraphicsPathSegment(endPoint)) : ((PdfGraphicsPathSegment) new PdfBezierGraphicsPathSegment(matrix.Transform(segment2.ControlPoint1), matrix.Transform(segment2.ControlPoint2), endPoint)));
        }

        public PdfPoint EndPoint =>
            this.endPoint;

        protected internal abstract bool Flat { get; }
    }
}

