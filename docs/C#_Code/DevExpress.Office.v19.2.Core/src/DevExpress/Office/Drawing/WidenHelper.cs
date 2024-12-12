namespace DevExpress.Office.Drawing
{
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class WidenHelper
    {
        private readonly List<Polygon> polygons;
        private readonly Pen pen;
        private readonly LineCapInfo startCapInfo;
        private readonly LineCapInfo endCapInfo;
        private readonly float miterLimit;
        private readonly float delta;
        private readonly bool applyBoundingPath;
        private const float flatness = 0.1f;
        private GraphicsPath currentPath;
        private ProcessLineJoin processLineJoin;
        private GraphicsPath startCapPath;
        private GraphicsPath endCapPath;
        private Matrix scaleTransform;

        private WidenHelper(Pen pen)
        {
            Guard.ArgumentNotNull(pen, "pen");
            this.polygons = new List<Polygon>();
            this.pen = pen;
            this.delta = pen.Width / 2f;
            this.miterLimit = 2f / (pen.MiterLimit * pen.MiterLimit);
        }

        private WidenHelper(PenInfo penInfo, bool applyBoundingPath, Matrix scaleTransform) : this(penInfo.Pen)
        {
            this.startCapInfo = penInfo.StartCap;
            this.endCapInfo = penInfo.EndCap;
            this.applyBoundingPath = applyBoundingPath;
            this.scaleTransform = scaleTransform;
        }

        private void AddFlattenedCurve(PointF[] curve, List<PointF> points, List<PointF> normals, List<PointF> reversedNormals)
        {
            PointF tf = points.First<PointF>();
            while (!this.BezierIsFlat(curve, 0.1f))
            {
                SplittedBezierCurve curve2 = this.FindFlatSegment(curve, 0.5f);
                this.AddPointAndNormals(points, normals, reversedNormals, curve2.FirstHalf[3]);
                curve = curve2.SecondHalf;
            }
            PointF point = curve[3];
            if (!point.Equals(tf))
            {
                this.AddPointAndNormals(points, normals, reversedNormals, point);
            }
        }

        private void AddLine(PointF start, PointF end)
        {
            this.currentPath.AddLine(start, end);
        }

        private void AddPointAndNormals(List<PointF> points, List<PointF> normals, List<PointF> reversedNormals, PointF point)
        {
            PointF tf = points[points.Count - 1];
            if (!tf.Equals(point))
            {
                points.Add(point);
                normals.Add(this.GetNormal(tf, point));
                reversedNormals.Add(this.GetNormal(point, tf));
            }
        }

        private void AddPolygon(List<PointF> points, List<PointF> normals, List<PointF> reversedNormals, bool isClosed)
        {
            if (points.Count > 1)
            {
                normals.Add(this.GetLastNormal(points, normals, isClosed));
                reversedNormals.Insert(0, this.GetLastNormalReversed(points, reversedNormals, isClosed));
                if (!isClosed)
                {
                    this.ProcessLineCaps(points, normals, reversedNormals);
                }
                if (points.Count > 1)
                {
                    this.polygons.Add(new Polygon(points.ToArray(), normals.ToArray(), reversedNormals.ToArray(), isClosed));
                }
                normals.Clear();
                reversedNormals.Clear();
            }
            points.Clear();
        }

        private void AddScaledPath(GraphicsPath parentPath, GraphicsPath subPath)
        {
            if (subPath.PointCount > 0)
            {
                subPath.CloseFigure();
                if (this.scaleTransform != null)
                {
                    subPath.Transform(this.scaleTransform);
                }
                parentPath.AddPath(subPath, false);
                subPath.Dispose();
            }
        }

        public static void Apply(GraphicsPath path, Pen pen)
        {
            path.Widen(pen);
        }

        public static void Apply(GraphicsPath path, PenInfo penInfo, bool applyBoundingPath, Matrix transform)
        {
            Guard.ArgumentNotNull(penInfo, "penInfo");
            path.Widen(penInfo.Pen, transform);
        }

        private void ApplyCore(GraphicsPath path)
        {
            Guard.ArgumentNotNull(path, "path");
            this.PreparePolygons(path);
            path.Reset();
            path.FillMode = FillMode.Winding;
            this.processLineJoin = this.GetLineJoinStrategy();
            for (int i = 0; i < this.polygons.Count; i++)
            {
                using (GraphicsPath path2 = this.ProcessFigure(this.polygons[i]))
                {
                    path.AddPath(path2, false);
                }
            }
            using (GraphicsPath path3 = this.CreateLineCapPath())
            {
                if (path3.PointCount > 0)
                {
                    path.AddPath(path3, false);
                }
            }
        }

        private bool BezierIsFlat(PointF[] p, float flatness)
        {
            double segmentLength = this.GetSegmentLength(p[2], p[3]);
            double num4 = this.GetSegmentLength(p[0], p[3]);
            return ((((this.GetSegmentLength(p[0], p[1]) + this.GetSegmentLength(p[1], p[2])) + segmentLength) - num4) < flatness);
        }

        private double CalculateLengthToCurveControlPoints(PointF a, PointF b, PointF c)
        {
            double segmentLength = this.GetSegmentLength(a, b);
            double d = segmentLength / (2.0 * this.GetSegmentLength(a, c));
            if (d >= 1.0)
            {
                return 0.0;
            }
            double num4 = Math.Sin(Math.Acos(d));
            return (((0.66666666666666663 * (1.0 - d)) * segmentLength) / (num4 * num4));
        }

        private PointF CalculatePointInsideSegment(PointF start, PointF end, double s)
        {
            PointF tf;
            PointF tf2;
            float x = start.X;
            float y = start.Y;
            if (this.CoordsAreEqual(end.X, x))
            {
                tf = new PointF(x, y + ((float) s));
                tf2 = new PointF(x, y - ((float) s));
            }
            else if (this.CoordsAreEqual(end.Y, y))
            {
                tf = new PointF(x + ((float) s), y);
                tf2 = new PointF(x - ((float) s), y);
            }
            else
            {
                double num3 = (end.Y - y) / (end.X - x);
                double num4 = end.Y - (num3 * end.X);
                double num5 = 1.0 + (num3 * num3);
                double num6 = (-x + (num3 * num4)) - (y * num3);
                double num8 = Math.Sqrt(Math.Abs((double) ((num6 * num6) - (num5 * (((((x * x) + (num4 * num4)) - ((2f * y) * num4)) + (y * y)) - (s * s))))));
                double num9 = (-num6 + num8) / num5;
                double num10 = (-num6 - num8) / num5;
                tf = new PointF((float) num9, (float) ((num9 * num3) + num4));
                tf2 = new PointF((float) num10, (float) ((num10 * num3) + num4));
            }
            return ((!this.IsBetween(x, end.X, tf.X) || !this.IsBetween(y, end.Y, tf.Y)) ? tf2 : tf);
        }

        private PointF CalculatePointOnLine(PointF start, PointF prev, PointF next, double arrowLength, double lengthFromStart)
        {
            bool flag = prev.Equals(start);
            double num = flag ? lengthFromStart : this.GetSegmentLength(prev, next);
            double num2 = ((double) (next.X - prev.X)) / num;
            double num3 = ((double) (next.Y - prev.Y)) / num;
            if (!flag)
            {
                double num7 = (num3 * (start.X - prev.X)) - (num2 * (start.Y - prev.Y));
                arrowLength = ((num3 * (start.Y - prev.Y)) + (num2 * (start.X - prev.X))) + Math.Sqrt((arrowLength * arrowLength) - (num7 * num7));
            }
            double num5 = prev.Y + (num3 * arrowLength);
            return new PointF(prev.X + ((float) (num2 * arrowLength)), (float) num5);
        }

        private bool CoordsAreEqual(float c1, float c2) => 
            Math.Abs((float) (c2 - c1)) < 1f;

        private GraphicsPath CreatCapPath(LineCapInfo lineCapInfo, Matrix capTransform)
        {
            GraphicsPath path = this.applyBoundingPath ? ((GraphicsPath) lineCapInfo.BoundingPath.Clone()) : ((GraphicsPath) lineCapInfo.GraphicsPath.Clone());
            path.Transform(capTransform);
            return path;
        }

        private Matrix CreateCapTransform(float penWidth)
        {
            Matrix matrix = new Matrix();
            if (this.scaleTransform != null)
            {
                matrix.Multiply(this.scaleTransform);
            }
            matrix.Scale(penWidth, penWidth);
            return matrix;
        }

        private GraphicsPath CreateLineCapPath()
        {
            GraphicsPath path = new GraphicsPath(FillMode.Winding);
            if (this.startCapPath != null)
            {
                path.AddPath(this.startCapPath, false);
                this.startCapPath.Dispose();
            }
            if (this.endCapPath != null)
            {
                path.AddPath(this.endCapPath, false);
                this.endCapPath.Dispose();
            }
            return path;
        }

        public static void DrawPath(GraphicsPath path, PenInfo penInfo, Graphics graphics)
        {
            Guard.ArgumentNotNull(penInfo, "penInfo");
            graphics.DrawPath(penInfo.Pen, path);
        }

        private GraphicsPath FinalizeCapPath(GraphicsPath path, PointF start, PointF end)
        {
            this.RotatePath(path, start, end);
            path.Translate(start.X, start.Y);
            if (!path.IsClosed())
            {
                this.WidenCap(path);
            }
            return path;
        }

        private SplittedBezierCurve FindFlatSegment(PointF[] curve, float t)
        {
            SplittedBezierCurve curve2 = new SplittedBezierCurve(curve, t);
            return (!this.BezierIsFlat(curve2.FirstHalf, 0.1f) ? this.FindFlatSegment(curve, t / 2f) : curve2);
        }

        private float GetArrowLength(GraphicsPath path, float inset, OutlineHeadTailType type)
        {
            switch (type)
            {
                case OutlineHeadTailType.Arrow:
                case OutlineHeadTailType.StealthArrow:
                    return inset;

                case OutlineHeadTailType.TriangleArrow:
                    return (float) path.GetBoundsExt().Height;
            }
            return (float) (path.GetBoundsExt().Height / 2);
        }

        private ArrowSegment GetEndCapArrowSegment(List<PointF> points, float arrowLength, float inset)
        {
            PointF tf = points.Last<PointF>();
            PointF prev = new PointF();
            PointF tf3 = tf;
            int endPointIndex = points.Count - 1;
            double lengthFromStart = 0.0;
            while ((endPointIndex > 0) && (lengthFromStart < arrowLength))
            {
                endPointIndex--;
                prev = tf3;
                tf3 = points[endPointIndex];
                lengthFromStart = this.GetSegmentLength(tf, tf3);
            }
            PointF end = this.CalculatePointOnLine(tf, prev, tf3, (double) arrowLength, lengthFromStart);
            PointF empty = (PointF) Point.Empty;
            if (lengthFromStart >= arrowLength)
            {
                empty = (inset == 0f) ? tf : this.CalculatePointInsideSegment(tf, end, (double) inset);
                endPointIndex++;
            }
            return new ArrowSegment(tf, end, empty, endPointIndex);
        }

        private PointF GetEndPoint(PointF[] points, PointF[] normals, int prev, int next, int endSign = 1)
        {
            double num2 = points[next].Y + ((this.delta * endSign) * normals[next].Y);
            return new PointF(points[next].X + ((this.delta * endSign) * normals[next].X), (float) num2);
        }

        private PointF GetLastNormal(List<PointF> points, List<PointF> normals, bool isClosed) => 
            !isClosed ? normals.Last<PointF>() : this.GetNormal(points.Last<PointF>(), points.First<PointF>());

        private PointF GetLastNormalReversed(List<PointF> points, List<PointF> reversedNormals, bool isClosed) => 
            !isClosed ? reversedNormals.First<PointF>() : this.GetNormal(points.First<PointF>(), points.Last<PointF>());

        private ProcessLineJoin GetLineJoinStrategy()
        {
            switch (this.pen.LineJoin)
            {
                case LineJoin.Miter:
                case LineJoin.MiterClipped:
                    return new ProcessLineJoin(this.ProcessMiterLineJoin);

                case LineJoin.Round:
                    return new ProcessLineJoin(this.ProcessRoundLineJoin);
            }
            return null;
        }

        private PointF GetMiterIntersection(PointF[] points, PointF[] normals, int prev, int next, double delta)
        {
            double num2 = points[next].Y + (delta * (normals[prev].Y + normals[next].Y));
            return new PointF(points[next].X + ((float) (delta * (normals[prev].X + normals[next].X))), (float) num2);
        }

        private PointF GetNormal(PointF pt1, PointF pt2)
        {
            float num = pt2.X - pt1.X;
            float num2 = pt2.Y - pt1.Y;
            if ((num == 0f) && (num2 == 0f))
            {
                return new PointF();
            }
            float num3 = 1f / ((float) Math.Sqrt((double) ((num * num) + (num2 * num2))));
            return new PointF(num2 * num3, -(num * num3));
        }

        private double GetNormalCos(PointF[] normals, int prev, int next) => 
            (double) ((normals[prev].X * normals[next].X) + (normals[prev].Y * normals[next].Y));

        private double GetNormalSin(PointF[] normals, int prev, int next) => 
            (double) ((normals[prev].X * normals[next].Y) - (normals[next].X * normals[prev].Y));

        private PathPointType GetPointType(byte type)
        {
            byte num = 0x80;
            if (type > num)
            {
                type = (byte) (type - num);
            }
            return (PathPointType) type;
        }

        private float GetRotationAngle(PointF start, PointF end)
        {
            float num = end.X - start.X;
            float num2 = end.Y - start.Y;
            double d = Math.Min(1.0, Math.Max((double) -1.0, (double) (((double) -num2) / Math.Sqrt((double) ((num * num) + (num2 * num2))))));
            return (float) (((((num > 0f) ? ((double) 1) : ((double) (-1))) * Math.Acos(d)) * 180.0) / 3.1415926535897931);
        }

        private double GetSegmentLength(PointF p1, PointF p2) => 
            Math.Sqrt(Math.Pow((double) (p2.X - p1.X), 2.0) + Math.Pow((double) (p2.Y - p1.Y), 2.0));

        private ArrowSegment GetStartCapArrowSegment(List<PointF> points, float arrowLength, float inset)
        {
            PointF tf = points.First<PointF>();
            PointF prev = new PointF();
            PointF tf3 = tf;
            int endPointIndex = 0;
            double lengthFromStart = 0.0;
            while ((endPointIndex < (points.Count - 1)) && (lengthFromStart < arrowLength))
            {
                endPointIndex++;
                prev = tf3;
                tf3 = points[endPointIndex];
                lengthFromStart = this.GetSegmentLength(tf, tf3);
            }
            PointF end = this.CalculatePointOnLine(tf, prev, tf3, (double) arrowLength, lengthFromStart);
            PointF empty = PointF.Empty;
            if (lengthFromStart >= arrowLength)
            {
                empty = (inset == 0f) ? tf : this.CalculatePointInsideSegment(tf, end, (double) inset);
            }
            else
            {
                endPointIndex++;
            }
            return new ArrowSegment(tf, end, empty, endPointIndex);
        }

        private PointF GetStartPoint(PointF[] points, PointF[] normals, int prev, int next, int startSign = 1)
        {
            double num2 = points[next].Y + ((this.delta * startSign) * normals[prev].Y);
            return new PointF(points[next].X + ((this.delta * startSign) * normals[prev].X), (float) num2);
        }

        private bool IsBetween(float start, float end, float p) => 
            (start < end) ? ((p >= start) && (p <= end)) : ((p >= end) && (p <= start));

        private void OffsetPoints(PointF[] points, PointF[] normals, int prev, int next, bool isFirst = false, bool isLast = false)
        {
            double num = this.GetNormalSin(normals, prev, next);
            if (!((ReferenceEquals(this.processLineJoin, null) | isFirst) | isLast) && (num >= 0.0))
            {
                this.processLineJoin(points, normals, prev, next);
            }
            else
            {
                PointF start = this.GetStartPoint(points, normals, prev, next, isFirst ? -1 : 1);
                this.AddLine(start, this.GetEndPoint(points, normals, prev, next, isLast ? -1 : 1));
            }
        }

        private bool PointIsNan(PointF p) => 
            float.IsNaN(p.X) || float.IsNaN(p.Y);

        private bool PointsAreEqual(PointF p1, PointF p2) => 
            this.CoordsAreEqual(p1.X, p2.X) && this.CoordsAreEqual(p1.Y, p2.Y);

        private void PreparePolygons(GraphicsPath path)
        {
            bool isClosed = false;
            byte[] pathTypes = path.PathTypes;
            PointF[] pathPoints = path.PathPoints;
            List<PointF> points = new List<PointF>();
            List<PointF> normals = new List<PointF>();
            List<PointF> reversedNormals = new List<PointF>();
            for (int i = 0; i < path.PointCount; i++)
            {
                PathPointType pointType = this.GetPointType(pathTypes[i]);
                if (pointType == PathPointType.Start)
                {
                    this.AddPolygon(points, normals, reversedNormals, isClosed);
                    points.Add(pathPoints[i]);
                    isClosed = false;
                }
                else if (pointType == PathPointType.Line)
                {
                    this.AddPointAndNormals(points, normals, reversedNormals, pathPoints[i]);
                    isClosed = (pathTypes[i] & 0x80) != 0;
                }
                else if (pointType == PathPointType.Bezier)
                {
                    PointF[] curve = new PointF[] { points.Last<PointF>(), pathPoints[i], pathPoints[i + 1], pathPoints[i + 2] };
                    this.AddFlattenedCurve(curve, points, normals, reversedNormals);
                    isClosed = (pathTypes[i + 2] & 0x80) != 0;
                    i += 2;
                }
            }
            this.AddPolygon(points, normals, reversedNormals, isClosed);
        }

        private GraphicsPath ProcessClosedFigure(PointF[] points, PointF[] normals, PointF[] reversedNormals)
        {
            GraphicsPath parentPath = new GraphicsPath(FillMode.Winding);
            this.currentPath = new GraphicsPath(FillMode.Winding);
            int prev = 0;
            int length = points.Length;
            this.OffsetPoints(points, normals, length - 1, 0, false, false);
            for (int i = 1; i < length; i++)
            {
                this.OffsetPoints(points, normals, prev, i, false, false);
                prev = i;
            }
            this.AddScaledPath(parentPath, this.currentPath);
            this.currentPath = new GraphicsPath(FillMode.Winding);
            prev = length - 1;
            this.OffsetPoints(points, reversedNormals, 0, length - 1, false, false);
            for (int j = length - 2; j >= 0; j--)
            {
                this.OffsetPoints(points, reversedNormals, prev, j, false, false);
                prev = j;
            }
            this.AddScaledPath(parentPath, this.currentPath);
            return parentPath;
        }

        private GraphicsPath ProcessFigure(Polygon polygon) => 
            !polygon.IsClosed ? this.ProcessOpenFigure(polygon.Points, polygon.Normals, polygon.ReversedNormals) : this.ProcessClosedFigure(polygon.Points, polygon.Normals, polygon.ReversedNormals);

        private void ProcessLineCaps(List<PointF> points, List<PointF> normals, List<PointF> reversedNormals)
        {
            if ((this.startCapInfo != null) || (this.endCapInfo != null))
            {
                ArrowSegment empty = ArrowSegment.Empty;
                ArrowSegment segment2 = ArrowSegment.Empty;
                using (Matrix matrix = this.CreateCapTransform(this.pen.Width))
                {
                    if (this.startCapInfo != null)
                    {
                        GraphicsPath path = this.CreatCapPath(this.startCapInfo, matrix);
                        float inset = this.startCapInfo.Inset * this.pen.Width;
                        float arrowLength = this.GetArrowLength(path, inset, this.startCapInfo.Type);
                        empty = this.GetStartCapArrowSegment(points, arrowLength, inset);
                        this.startCapPath = this.FinalizeCapPath(path, empty.StartPoint, empty.EndPoint);
                    }
                    if (this.endCapInfo != null)
                    {
                        GraphicsPath path = this.CreatCapPath(this.endCapInfo, matrix);
                        float inset = this.endCapInfo.Inset * this.pen.Width;
                        float arrowLength = this.GetArrowLength(path, inset, this.endCapInfo.Type);
                        segment2 = this.GetEndCapArrowSegment(points, arrowLength, inset);
                        this.endCapPath = this.FinalizeCapPath(path, segment2.StartPoint, segment2.EndPoint);
                    }
                }
                if (segment2.IsEmpty)
                {
                    this.UpdatePointsAndNormals_HasStartCap(points, normals, reversedNormals, empty.EndPointIndex, empty.InsidePoint);
                }
                else if (empty.IsEmpty)
                {
                    this.UpdatePointsAndNormals_HasEndCap(points, normals, reversedNormals, segment2.EndPointIndex, segment2.InsidePoint);
                }
                else
                {
                    this.UpdatePointsAndNormals(points, normals, reversedNormals, empty.EndPointIndex, segment2.EndPointIndex, empty.InsidePoint, segment2.InsidePoint);
                }
            }
        }

        private void ProcessMiterLineJoin(PointF[] points, PointF[] normals, int prev, int next)
        {
            double num2 = 1.0 + this.GetNormalCos(normals, prev, next);
            if (num2 != 0.0)
            {
                PointF end = this.GetMiterIntersection(points, normals, prev, next, ((double) this.delta) / num2);
                if (num2 >= this.miterLimit)
                {
                    PointF start = (this.currentPath.PointCount > 0) ? this.currentPath.GetLastPoint() : end;
                    this.AddLine(start, end);
                }
                else
                {
                    PointF tf3 = this.GetStartPoint(points, normals, prev, next, 1);
                    PointF tf4 = this.GetEndPoint(points, normals, prev, next, 1);
                    PointF tf5 = points[next];
                    if (!this.PointsAreEqual(tf3, tf4) && (!this.PointsAreEqual(tf3, end) && (!this.PointsAreEqual(tf4, end) && !this.PointsAreEqual(tf5, end))))
                    {
                        double segmentLength = this.GetSegmentLength(end, tf5);
                        double s = Math.Sqrt((segmentLength * segmentLength) - (this.delta * this.delta)) - ((segmentLength - (this.delta * this.pen.MiterLimit)) / Math.Cos(Math.Asin(((double) this.delta) / segmentLength)));
                        PointF start = this.CalculatePointInsideSegment(tf3, end, s);
                        this.AddLine(start, this.CalculatePointInsideSegment(tf4, end, s));
                    }
                }
            }
        }

        private GraphicsPath ProcessOpenFigure(PointF[] points, PointF[] normals, PointF[] reversedNormals)
        {
            GraphicsPath parentPath = new GraphicsPath(FillMode.Winding);
            this.currentPath = new GraphicsPath(FillMode.Winding);
            this.OffsetPoints(points, normals, 0, 0, true, false);
            int prev = 0;
            int num2 = points.Length - 1;
            for (int i = 1; i < num2; i++)
            {
                this.OffsetPoints(points, normals, prev, i, false, false);
                prev = i;
            }
            this.OffsetPoints(points, normals, num2, num2, false, true);
            prev = num2;
            for (int j = num2 - 1; j > 0; j--)
            {
                this.OffsetPoints(points, reversedNormals, prev, j, false, false);
                prev = j;
            }
            this.AddScaledPath(parentPath, this.currentPath);
            return parentPath;
        }

        private void ProcessRoundLineJoin(PointF[] points, PointF[] normals, int prev, int next)
        {
            PointF end = this.GetStartPoint(points, normals, prev, next, 1);
            PointF tf2 = this.GetEndPoint(points, normals, prev, next, 1);
            if (this.currentPath.PointCount > 0)
            {
                this.AddLine(this.currentPath.GetLastPoint(), end);
            }
            if (!this.PointsAreEqual(end, tf2))
            {
                PointF tf3 = new PointF();
                PointF tf4 = new PointF();
                if (this.TryCalculateControlPoints(points, normals, prev, next, end, ref tf3, ref tf4, tf2))
                {
                    this.currentPath.AddBezier(end, tf3, tf4, tf2);
                }
                else
                {
                    this.AddLine(end, tf2);
                }
            }
        }

        private void RotatePath(GraphicsPath path, PointF capStart, PointF capEnd)
        {
            float rotationAngle = this.GetRotationAngle(capStart, capEnd);
            if (rotationAngle != 0f)
            {
                using (Matrix matrix = new Matrix())
                {
                    matrix.Rotate(rotationAngle);
                    path.Transform(matrix);
                }
            }
        }

        private bool TryCalculateControlPoints(PointF[] points, PointF[] normals, int prev, int next, PointF p0, ref PointF p1, ref PointF p2, PointF p3)
        {
            double num = this.GetNormalCos(normals, prev, next);
            if (num <= -1.0)
            {
                return false;
            }
            PointF p = this.GetMiterIntersection(points, normals, prev, next, ((double) this.delta) / (1.0 + num));
            if (this.PointIsNan(p) || (this.PointsAreEqual(p0, p) || this.PointsAreEqual(p3, p)))
            {
                return false;
            }
            double s = this.CalculateLengthToCurveControlPoints(p0, p3, p);
            if (s < 1.0)
            {
                return false;
            }
            p1 = this.CalculatePointInsideSegment(p0, p, s);
            if (this.PointIsNan(p1))
            {
                return false;
            }
            p2 = this.CalculatePointInsideSegment(p3, p, s);
            return !this.PointIsNan(p2);
        }

        private void UpdateNormalsForArrowWithOneSegmentGeometry(List<PointF> normals, List<PointF> reversedNormals, PointF start, PointF end)
        {
            PointF normal = this.GetNormal(start, end);
            PointF item = this.GetNormal(end, start);
            normals.Clear();
            normals.Add(normal);
            normals.Add(normal);
            reversedNormals.Clear();
            reversedNormals.Add(item);
            reversedNormals.Add(item);
        }

        private void UpdatePointsAndNormals(List<PointF> points, List<PointF> normals, List<PointF> reversedNormals, int startArrowSegmentEndIndex, int endArrowSegmentEndIndex, PointF startInsidePoint, PointF endInsidePoint)
        {
            if (startArrowSegmentEndIndex < endArrowSegmentEndIndex)
            {
                this.UpdatePointsAndNormals_HasEndCap(points, normals, reversedNormals, endArrowSegmentEndIndex, endInsidePoint);
                this.UpdatePointsAndNormals_HasStartCap(points, normals, reversedNormals, startArrowSegmentEndIndex, startInsidePoint);
            }
            else if (startArrowSegmentEndIndex != endArrowSegmentEndIndex)
            {
                points.Clear();
            }
            else
            {
                points.Clear();
                points.Add(startInsidePoint);
                points.Add(endInsidePoint);
                this.UpdateNormalsForArrowWithOneSegmentGeometry(normals, reversedNormals, startInsidePoint, endInsidePoint);
            }
        }

        private void UpdatePointsAndNormals_HasEndCap(List<PointF> points, List<PointF> normals, List<PointF> reversedNormals, int arrowSegmentEndIndex, PointF arrowInsidePoint)
        {
            if (arrowSegmentEndIndex == 0)
            {
                points.Clear();
            }
            else
            {
                int count = points.Count;
                points.RemoveRange(arrowSegmentEndIndex, count - arrowSegmentEndIndex);
                points.Add(arrowInsidePoint);
                if (arrowSegmentEndIndex == 1)
                {
                    this.UpdateNormalsForArrowWithOneSegmentGeometry(normals, reversedNormals, points.First<PointF>(), points.Last<PointF>());
                }
                else
                {
                    normals.RemoveRange(arrowSegmentEndIndex - 1, (count - arrowSegmentEndIndex) + 1);
                    reversedNormals.RemoveRange(arrowSegmentEndIndex, count - arrowSegmentEndIndex);
                    normals.Add(this.GetNormal(points[points.Count - 2], points.Last<PointF>()));
                    normals.Add(normals.Last<PointF>());
                    reversedNormals.Add(this.GetNormal(points.Last<PointF>(), points[points.Count - 2]));
                }
            }
        }

        private void UpdatePointsAndNormals_HasStartCap(List<PointF> points, List<PointF> normals, List<PointF> reversedNormals, int arrowSegmentEndIndex, PointF arrowInsidePoint)
        {
            int num = points.Count - 1;
            if (arrowSegmentEndIndex > num)
            {
                points.Clear();
            }
            else
            {
                points.RemoveRange(0, arrowSegmentEndIndex);
                points.Insert(0, arrowInsidePoint);
                if (arrowSegmentEndIndex == num)
                {
                    this.UpdateNormalsForArrowWithOneSegmentGeometry(normals, reversedNormals, points.First<PointF>(), points.Last<PointF>());
                }
                else
                {
                    normals.RemoveRange(0, arrowSegmentEndIndex - 1);
                    reversedNormals.RemoveRange(0, arrowSegmentEndIndex - 1);
                }
            }
        }

        private void WidenCap(GraphicsPath path)
        {
            using (Pen pen = (Pen) this.pen.Clone())
            {
                pen.LineJoin = LineJoin.Miter;
                if (this.scaleTransform != null)
                {
                    pen.Width *= this.scaleTransform.Elements[0];
                }
                new WidenHelper(pen).ApplyCore(path);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct ArrowSegment
        {
            public static WidenHelper.ArrowSegment Empty;
            public ArrowSegment(PointF startPoint, PointF endPoint, PointF insidePoint, int endPointIndex)
            {
                this.<StartPoint>k__BackingField = startPoint;
                this.<EndPointIndex>k__BackingField = endPointIndex;
                this.<EndPoint>k__BackingField = endPoint;
                this.<InsidePoint>k__BackingField = insidePoint;
            }

            public PointF StartPoint { get; }
            public PointF EndPoint { get; }
            public PointF InsidePoint { get; }
            public int EndPointIndex { get; }
            public bool IsEmpty =>
                this.StartPoint.Equals(PointF.Empty) && (this.EndPoint.Equals(PointF.Empty) && (this.InsidePoint.Equals(PointF.Empty) && (this.EndPointIndex == -2147483648)));
            static ArrowSegment()
            {
                Empty = new WidenHelper.ArrowSegment(PointF.Empty, PointF.Empty, PointF.Empty, -2147483648);
            }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct Polygon
        {
            public Polygon(PointF[] points, PointF[] normals, PointF[] reversedNormals, bool isClosed)
            {
                this.<Points>k__BackingField = points;
                this.<Normals>k__BackingField = normals;
                this.<ReversedNormals>k__BackingField = reversedNormals;
                this.<IsClosed>k__BackingField = isClosed;
            }

            public PointF[] Points { get; }
            public PointF[] Normals { get; }
            public PointF[] ReversedNormals { get; }
            public bool IsClosed { get; }
        }

        private delegate void ProcessLineJoin(PointF[] points, PointF[] normals, int prev, int next);

        [StructLayout(LayoutKind.Sequential)]
        private struct SplittedBezierCurve
        {
            public SplittedBezierCurve(PointF[] p, float t)
            {
                this = new WidenHelper.SplittedBezierCurve();
                PointF tf = p[0];
                PointF start = this.GetBezierPoint(p[0], p[1], t);
                PointF end = this.GetBezierPoint(p[1], p[2], t);
                PointF tf4 = this.GetBezierPoint(p[2], p[3], t);
                PointF tf5 = this.GetBezierPoint(start, end, t);
                PointF tf6 = this.GetBezierPoint(end, tf4, t);
                PointF tf7 = this.GetBezierPoint(tf5, tf6, t);
                PointF tf8 = p[3];
                this.<FirstHalf>k__BackingField = new PointF[] { tf, start, tf5, tf7 };
                this.<SecondHalf>k__BackingField = new PointF[] { tf7, tf6, tf4, tf8 };
            }

            public PointF[] FirstHalf { get; }
            public PointF[] SecondHalf { get; }
            private PointF GetBezierPoint(PointF start, PointF end, float t) => 
                new PointF(((end.X - start.X) * t) + start.X, ((end.Y - start.Y) * t) + start.Y);
        }
    }
}

