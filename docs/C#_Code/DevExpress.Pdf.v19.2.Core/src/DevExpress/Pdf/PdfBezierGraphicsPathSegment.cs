namespace DevExpress.Pdf
{
    using System;
    using System.Collections.Generic;

    public class PdfBezierGraphicsPathSegment : PdfGraphicsPathSegment
    {
        private readonly PdfPoint controlPoint1;
        private readonly PdfPoint controlPoint2;

        public PdfBezierGraphicsPathSegment(PdfPoint controlPoint1, PdfPoint controlPoint2, PdfPoint endPoint) : base(endPoint)
        {
            this.controlPoint1 = controlPoint1;
            this.controlPoint2 = controlPoint2;
        }

        protected internal override void AddSegmentPoints(IList<PdfPoint> points)
        {
            base.AddSegmentPoints(points);
            points.Add(this.controlPoint1);
            points.Add(this.controlPoint2);
        }

        protected internal override void GeneratePathSegmentCommands(IList<PdfCommand> commands)
        {
            PdfPoint endPoint = base.EndPoint;
            commands.Add(new PdfAppendBezierCurveCommand(this.controlPoint1.X, this.controlPoint1.Y, this.controlPoint2.X, this.controlPoint2.Y, endPoint.X, endPoint.Y));
        }

        public PdfPoint ControlPoint1 =>
            this.controlPoint1;

        public PdfPoint ControlPoint2 =>
            this.controlPoint2;

        protected internal override bool Flat =>
            false;
    }
}

