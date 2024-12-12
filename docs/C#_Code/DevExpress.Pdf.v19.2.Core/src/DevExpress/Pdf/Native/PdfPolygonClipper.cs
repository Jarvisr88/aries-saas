namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfPolygonClipper
    {
        private static readonly PdfPolygonClipperEdge[] edges = new PdfPolygonClipperEdge[] { PdfPolygonClipperEdge.Left, PdfPolygonClipperEdge.Right, PdfPolygonClipperEdge.Bottom, PdfPolygonClipperEdge.Top };
        private readonly PdfRectangle bounds;
        private readonly Dictionary<PdfPoint, PdfPolygonClipperEdge> pointEdges = new Dictionary<PdfPoint, PdfPolygonClipperEdge>();
        private PdfPolygon polygon;

        public PdfPolygonClipper(PdfRectangle bounds)
        {
            this.bounds = bounds;
        }

        private void AddIntersection(PdfPolygonClipperEdge edge, PdfPoint point1, PdfPoint point2)
        {
            double x = point1.X;
            double y = point1.Y;
            double num3 = (point2.Y - y) / (point2.X - x);
            switch (edge)
            {
                case PdfPolygonClipperEdge.Left:
                {
                    double left = this.bounds.Left;
                    this.polygon.AddPoint(left, y + (num3 * (left - x)));
                    return;
                }
                case PdfPolygonClipperEdge.Right:
                {
                    double right = this.bounds.Right;
                    this.polygon.AddPoint(right, y + (num3 * (right - x)));
                    return;
                }
                case PdfPolygonClipperEdge.Bottom:
                {
                    double bottom = this.bounds.Bottom;
                    this.polygon.AddPoint(x + ((bottom - y) / num3), bottom);
                    return;
                }
            }
            double top = this.bounds.Top;
            this.polygon.AddPoint(x + ((top - y) / num3), top);
        }

        public PdfGraphicsPath Clip(PdfGraphicsPath path)
        {
            int count;
            PdfGraphicsPath path4;
            PdfRectangularGraphicsPath path2 = path as PdfRectangularGraphicsPath;
            if (path2 != null)
            {
                PdfRectangle rectangle = PdfRectangle.Intersect(this.bounds, path2.Rectangle);
                return ((rectangle == null) ? path : new PdfRectangularGraphicsPath(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height));
            }
            IList<PdfPoint> list = new List<PdfPoint> {
                path.StartPoint
            };
            using (IEnumerator<PdfGraphicsPathSegment> enumerator = path.Segments.GetEnumerator())
            {
                while (true)
                {
                    if (enumerator.MoveNext())
                    {
                        PdfGraphicsPathSegment current = enumerator.Current;
                        if (current is PdfLineGraphicsPathSegment)
                        {
                            list.Add(current.EndPoint);
                            continue;
                        }
                        path4 = path;
                    }
                    else
                    {
                        goto TR_0014;
                    }
                    break;
                }
            }
            return path4;
        TR_0014:
            count = list.Count;
            PdfPolygonClipperEdge[] edges = PdfPolygonClipper.edges;
            int index = 0;
            while (true)
            {
                if (index < edges.Length)
                {
                    PdfPolygonClipperEdge edge = edges[index];
                    if (count >= 2)
                    {
                        this.polygon = new PdfPolygon();
                        PdfPoint currentPoint = list[0];
                        PdfPoint point = currentPoint;
                        bool isPreviousPointInside = this.IsInside(point, edge);
                        int num3 = 1;
                        while (true)
                        {
                            if (num3 >= count)
                            {
                                this.ClipEdge(edge, isPreviousPointInside, point, currentPoint);
                                count = this.polygon.Points.Count;
                                index++;
                                break;
                            }
                            PdfPoint point3 = list[num3];
                            isPreviousPointInside = this.ClipEdge(edge, isPreviousPointInside, point, point3);
                            point = point3;
                            num3++;
                        }
                        continue;
                    }
                }
                if (count < 2)
                {
                    return path;
                }
                PdfGraphicsPath path3 = new PdfGraphicsPath(list[0]);
                IList<PdfGraphicsPathSegment> segments = path3.Segments;
                for (int i = 1; i < count; i++)
                {
                    segments.Add(new PdfLineGraphicsPathSegment(list[i]));
                }
                path3.Closed = path.Closed;
                return path3;
            }
        }

        private bool ClipEdge(PdfPolygonClipperEdge edge, bool isPreviousPointInside, PdfPoint previousPoint, PdfPoint currentPoint)
        {
            bool flag = this.IsInside(currentPoint, edge);
            if (!flag)
            {
                if (isPreviousPointInside)
                {
                    this.AddIntersection(edge, previousPoint, currentPoint);
                }
            }
            else
            {
                if (!isPreviousPointInside)
                {
                    this.AddIntersection(edge, previousPoint, currentPoint);
                }
                this.polygon.AddPoint(currentPoint.X, currentPoint.Y);
            }
            return flag;
        }

        internal bool IsInside(PdfPoint point, PdfPolygonClipperEdge edge)
        {
            PdfPolygonClipperEdge edge2;
            if (!this.pointEdges.TryGetValue(point, out edge2))
            {
                if (point.X < this.bounds.Left)
                {
                    edge2 |= PdfPolygonClipperEdge.Left;
                }
                else if (point.X > this.bounds.Right)
                {
                    edge2 |= PdfPolygonClipperEdge.Right;
                }
                if (point.Y < this.bounds.Bottom)
                {
                    edge2 |= PdfPolygonClipperEdge.Bottom;
                }
                else if (point.Y > this.bounds.Top)
                {
                    edge2 |= PdfPolygonClipperEdge.Top;
                }
                this.pointEdges[point] = edge2;
            }
            return ((edge2 & edge) == PdfPolygonClipperEdge.None);
        }
    }
}

