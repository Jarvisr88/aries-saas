namespace DevExpress.Pdf
{
    using DevExpress.Pdf.Native;
    using System;
    using System.Collections.Generic;

    public class PdfGraphicsPath
    {
        private readonly List<PdfGraphicsPathSegment> segments = new List<PdfGraphicsPathSegment>();
        private readonly PdfPoint startPoint;
        private bool closed;

        public PdfGraphicsPath(PdfPoint startPoint)
        {
            this.startPoint = startPoint;
        }

        public void AppendBezierSegment(PdfPoint controlPoint1, PdfPoint controlPoint2, PdfPoint endPoint)
        {
            this.segments.Add(new PdfBezierGraphicsPathSegment(controlPoint1, controlPoint2, endPoint));
        }

        public void AppendLineSegment(PdfPoint endPoint)
        {
            this.segments.Add(new PdfLineGraphicsPathSegment(endPoint));
        }

        protected internal virtual void GeneratePathCommands(IList<PdfCommand> commands)
        {
            commands.Add(new PdfBeginPathCommand(this.startPoint));
            foreach (PdfGraphicsPathSegment segment in this.segments)
            {
                segment.GeneratePathSegmentCommands(commands);
            }
            if (this.closed)
            {
                commands.Add(PdfClosePathCommand.Instance);
            }
        }

        internal virtual PdfRectangle GetAxisAlignedRectangle()
        {
            int count = this.segments.Count;
            if (count == 4)
            {
                if (!this.segments[3].Flat || !this.startPoint.Equals(this.segments[3].EndPoint))
                {
                    return null;
                }
                count--;
            }
            if (count != 3)
            {
                return null;
            }
            if (!this.segments[0].Flat || (!this.segments[1].Flat || !this.segments[2].Flat))
            {
                return null;
            }
            PdfPoint endPoint = this.segments[0].EndPoint;
            PdfPoint point3 = this.segments[1].EndPoint;
            PdfPoint point4 = this.segments[2].EndPoint;
            if (this.startPoint.X == endPoint.X)
            {
                if ((endPoint.Y != point3.Y) || (point3.X != point4.X))
                {
                    return null;
                }
            }
            else
            {
                if (this.startPoint.Y != endPoint.Y)
                {
                    return null;
                }
                if ((endPoint.X != point3.X) || (point3.Y != point4.Y))
                {
                    return null;
                }
            }
            PdfPoint[] points = new PdfPoint[] { this.startPoint, endPoint, point3, point4 };
            return PdfRectangle.CreateBoundingBox(points);
        }

        internal static PdfRectangle GetBoundingBox(IList<PdfGraphicsPath> paths)
        {
            List<PdfPoint> points = new List<PdfPoint>();
            foreach (PdfGraphicsPath path in paths)
            {
                points.Add(path.StartPoint);
                foreach (PdfGraphicsPathSegment segment in path.segments)
                {
                    segment.AddSegmentPoints(points);
                }
            }
            return PdfRectangle.CreateBoundingBox(points);
        }

        protected internal virtual bool IsFlat(bool forFilling)
        {
            bool flag;
            double x = this.startPoint.X;
            double y = this.startPoint.Y;
            double num3 = x;
            double num4 = y;
            using (List<PdfGraphicsPathSegment>.Enumerator enumerator = this.segments.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfGraphicsPathSegment current = enumerator.Current;
                        if (!current.Flat)
                        {
                            flag = false;
                        }
                        else
                        {
                            PdfPoint endPoint = current.EndPoint;
                            double num5 = endPoint.X;
                            double num6 = endPoint.Y;
                            if ((num5 == num3) || (num6 == num4))
                            {
                                num3 = num5;
                                num4 = num6;
                                continue;
                            }
                            flag = false;
                        }
                    }
                    else
                    {
                        return (!forFilling || (this.closed || ((x == num3) && (y == num4))));
                    }
                    break;
                }
            }
            return flag;
        }

        internal PdfLineSegment? ToLineSegment()
        {
            PdfPoint endPoint;
            if (!this.IsFlat(false))
            {
                return null;
            }
            if (this.Closed)
            {
                if (this.segments.Count != 2)
                {
                    return null;
                }
                endPoint = this.segments[0].EndPoint;
            }
            else
            {
                if (this.segments.Count != 1)
                {
                    return null;
                }
                endPoint = this.EndPoint;
            }
            return new PdfLineSegment(this.startPoint, endPoint);
        }

        public static IList<PdfGraphicsPath> Transform(IList<PdfGraphicsPath> paths, PdfTransformationMatrix matrix)
        {
            int count = paths.Count;
            if ((count == 1) && matrix.IsNotRotated)
            {
                PdfRectangularGraphicsPath path = paths[0] as PdfRectangularGraphicsPath;
                if (path != null)
                {
                    PdfRectangle rectangle = path.Rectangle;
                    PdfRectangle rectangle2 = new PdfRectangle(matrix.Transform(rectangle.BottomLeft), matrix.Transform(rectangle.TopRight));
                    return new PdfGraphicsPath[] { new PdfRectangularGraphicsPath(rectangle2.Left, rectangle2.Bottom, rectangle2.Width, rectangle2.Height) };
                }
            }
            List<PdfGraphicsPath> list = new List<PdfGraphicsPath>(count);
            foreach (PdfGraphicsPath path2 in paths)
            {
                PdfGraphicsPath item = new PdfGraphicsPath(matrix.Transform(path2.StartPoint));
                IList<PdfGraphicsPathSegment> segments = item.Segments;
                foreach (PdfGraphicsPathSegment segment in path2.Segments)
                {
                    segments.Add(PdfGraphicsPathSegment.Transform(segment, matrix));
                }
                item.Closed = path2.Closed;
                list.Add(item);
            }
            return list;
        }

        public IList<PdfGraphicsPathSegment> Segments =>
            this.segments;

        public PdfPoint StartPoint =>
            this.startPoint;

        internal PdfPoint EndPoint
        {
            get
            {
                int count = this.segments.Count;
                return ((count == 0) ? this.startPoint : this.segments[count - 1].EndPoint);
            }
        }

        internal bool Closed
        {
            get => 
                this.closed;
            set => 
                this.closed = value;
        }
    }
}

