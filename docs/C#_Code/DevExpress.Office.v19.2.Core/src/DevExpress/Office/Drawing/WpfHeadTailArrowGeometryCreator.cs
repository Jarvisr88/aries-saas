namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public class WpfHeadTailArrowGeometryCreator
    {
        private static Dictionary<OutlineHeadTailType, WpfArrowGeometryCreatorBase> creators = PrepareCreators();

        private PathGeometry ApplyHeadAndTailArrow(PathGeometry geometry, WpfArrowGeometryCreatorBase headCreator, WpfArrowGeometryCreatorBase tailCreator, Pen pen, double penWidthLimit, Outline outline)
        {
            ArrowSegmentInfo arrowSegmentInfo = headCreator.ArrowSegmentInfo;
            Geometry boundingGeometry = headCreator.BoundingGeometry;
            PathGeometry geometry5 = Geometry.Combine(Geometry.Combine(this.PrepareGeometryForHeadTailArrows(arrowSegmentInfo, tailCreator.ArrowSegmentInfo, geometry.Figures[0].Segments).GetWidenedPathGeometry(pen), headCreator.CreateHeadArrow(geometry, pen, penWidthLimit, outline.HeadLength, outline.HeadWidth), GeometryCombineMode.Union, Transform.Identity), tailCreator.CreateTailArrow(geometry, pen, penWidthLimit, outline.TailLength, outline.TailWidth), GeometryCombineMode.Union, Transform.Identity);
            this.BoundingGeometry = Geometry.Combine(boundingGeometry, tailCreator.BoundingGeometry, GeometryCombineMode.Union, Transform.Identity);
            return geometry5;
        }

        private PathGeometry ApplyHeadArrow(PathGeometry geometry, WpfArrowGeometryCreatorBase headCreator, Pen pen, double penWidthLimit, Outline outline)
        {
            PathGeometry geometry3 = Geometry.Combine(this.PrepareGeometryForHeadArrow(headCreator.ArrowSegmentInfo, geometry.Figures[0].Segments).GetWidenedPathGeometry(pen), headCreator.CreateHeadArrow(geometry, pen, penWidthLimit, outline.HeadLength, outline.HeadWidth), GeometryCombineMode.Union, Transform.Identity);
            this.BoundingGeometry = headCreator.BoundingGeometry;
            return geometry3;
        }

        private PathGeometry ApplyTailArrow(PathGeometry geometry, WpfArrowGeometryCreatorBase tailCreator, Pen pen, double penWidthLimit, Outline outline)
        {
            PathGeometry geometry3 = Geometry.Combine(this.PrepareGeometryForTailArrow(tailCreator.ArrowSegmentInfo, geometry.Figures[0]).GetWidenedPathGeometry(pen), tailCreator.CreateTailArrow(geometry, pen, penWidthLimit, outline.TailLength, outline.TailWidth), GeometryCombineMode.Union, Transform.Identity);
            this.BoundingGeometry = tailCreator.BoundingGeometry;
            return geometry3;
        }

        public PathGeometry CreateArrowGeometry(PathGeometry geometry, ShapeProperties shapeProperties, ShapeStyle shapeStyle, Rect bounds)
        {
            this.BoundingGeometry = new PathGeometry();
            Outline outline = shapeProperties.Outline;
            OutlineHeadTailType headType = outline.HeadType;
            OutlineHeadTailType tailType = outline.TailType;
            bool hasHeadArrow = headType != OutlineHeadTailType.None;
            bool hasTailArrow = tailType != OutlineHeadTailType.None;
            if ((!hasHeadArrow && !hasTailArrow) || (geometry.Figures.Count == 0))
            {
                return geometry;
            }
            Pen pen = WpfDrawingShapeHelper.CalculateLinePen(outline, shapeStyle, bounds, shapeProperties.ShapeType, hasHeadArrow, hasTailArrow);
            if (pen == null)
            {
                return geometry;
            }
            double penWidthLimit = shapeStyle.DocumentModel.LayoutUnitConverter.Pixels96DPIToLayoutUnitsF(3f);
            return (!(hasHeadArrow & hasTailArrow) ? (!hasHeadArrow ? this.ApplyTailArrow(geometry, creators[tailType], pen, penWidthLimit, outline) : this.ApplyHeadArrow(geometry, creators[headType], pen, penWidthLimit, outline)) : this.ApplyHeadAndTailArrow(geometry, creators[headType], creators[tailType], pen, penWidthLimit, outline));
        }

        private PathSegment CreateArrowSegment(ArrowSegmentInfo arrowSegmentInfo, Point endPoint)
        {
            if (!arrowSegmentInfo.IsBezierCurve)
            {
                return new LineSegment(endPoint, true);
            }
            Point[] bezierPoints = arrowSegmentInfo.BezierPoints;
            return new BezierSegment(bezierPoints[0], bezierPoints[1], bezierPoints[2], true);
        }

        private PathGeometry CreateGeometry(Point startPoint)
        {
            PathGeometry geometry = new PathGeometry();
            PathFigure figure = new PathFigure {
                StartPoint = startPoint
            };
            geometry.Figures.Add(figure);
            return geometry;
        }

        private PathSegment CreateHeadTailArrowsSegment(ArrowSegmentInfo headSegmentInfo, ArrowSegmentInfo tailSegmentInfo)
        {
            if (!headSegmentInfo.IsBezierCurve)
            {
                return new LineSegment(tailSegmentInfo.InsidePoint, true);
            }
            Point[] bezierPoints = headSegmentInfo.BezierPoints;
            Point[] pointArray2 = tailSegmentInfo.BezierPoints;
            Point point = new Point((bezierPoints[0].X + pointArray2[0].X) / 2.0, (bezierPoints[0].Y + pointArray2[0].Y) / 2.0);
            return new BezierSegment(point, new Point((bezierPoints[1].X + pointArray2[1].X) / 2.0, (bezierPoints[1].Y + pointArray2[1].Y) / 2.0), tailSegmentInfo.InsidePoint, true);
        }

        private static Dictionary<OutlineHeadTailType, WpfArrowGeometryCreatorBase> PrepareCreators() => 
            new Dictionary<OutlineHeadTailType, WpfArrowGeometryCreatorBase> { 
                { 
                    OutlineHeadTailType.Arrow,
                    new WpfArrowGeometryCreator()
                },
                { 
                    OutlineHeadTailType.TriangleArrow,
                    new WpfTriangleArrowGeometryCreator()
                },
                { 
                    OutlineHeadTailType.StealthArrow,
                    new WpfStealthArrowGeometryCreator()
                },
                { 
                    OutlineHeadTailType.Oval,
                    new WpfOvalArrowGeometryCreator()
                },
                { 
                    OutlineHeadTailType.Diamond,
                    new WpfDiamondArrowGeometryCreator()
                }
            };

        private PathGeometry PrepareGeometryForHeadArrow(ArrowSegmentInfo headSegmentInfo, PathSegmentCollection originalSegments)
        {
            if (!headSegmentInfo.IsValid)
            {
                return new PathGeometry();
            }
            PathGeometry geometry = this.CreateGeometry(headSegmentInfo.InsidePoint);
            PathSegmentCollection segments = geometry.Figures[0].Segments;
            segments.Add(this.CreateArrowSegment(headSegmentInfo, headSegmentInfo.OutsidePoint));
            for (int i = headSegmentInfo.SegmentIndex + 1; i < originalSegments.Count; i++)
            {
                segments.Add(originalSegments[i].Clone());
            }
            return geometry;
        }

        private PathGeometry PrepareGeometryForHeadTailArrows(ArrowSegmentInfo headSegmentInfo, ArrowSegmentInfo tailSegmentInfo, PathSegmentCollection originalSegments)
        {
            int segmentIndex = headSegmentInfo.SegmentIndex;
            int num2 = tailSegmentInfo.SegmentIndex;
            if ((segmentIndex > num2) || (!headSegmentInfo.IsValid || !tailSegmentInfo.IsValid))
            {
                return new PathGeometry();
            }
            PathGeometry geometry = this.CreateGeometry(headSegmentInfo.InsidePoint);
            PathSegmentCollection segments = geometry.Figures[0].Segments;
            if (segmentIndex == num2)
            {
                segments.Add(this.CreateHeadTailArrowsSegment(headSegmentInfo, tailSegmentInfo));
            }
            else
            {
                segments.Add(this.CreateArrowSegment(headSegmentInfo, headSegmentInfo.OutsidePoint));
                int num3 = segmentIndex + 1;
                while (true)
                {
                    if (num3 >= num2)
                    {
                        segments.Add(this.CreateArrowSegment(tailSegmentInfo, tailSegmentInfo.InsidePoint));
                        break;
                    }
                    segments.Add(originalSegments[num3].Clone());
                    num3++;
                }
            }
            return geometry;
        }

        private PathGeometry PrepareGeometryForTailArrow(ArrowSegmentInfo tailSegmentInfo, PathFigure originalFigure)
        {
            if (!tailSegmentInfo.IsValid)
            {
                return new PathGeometry();
            }
            PathGeometry geometry = this.CreateGeometry(originalFigure.StartPoint);
            PathSegmentCollection segments = geometry.Figures[0].Segments;
            for (int i = 0; i < tailSegmentInfo.SegmentIndex; i++)
            {
                segments.Add(originalFigure.Segments[i].Clone());
            }
            segments.Add(this.CreateArrowSegment(tailSegmentInfo, tailSegmentInfo.InsidePoint));
            return geometry;
        }

        public Geometry BoundingGeometry { get; private set; }

        [StructLayout(LayoutKind.Sequential)]
        private struct ArrowSegmentInfo
        {
            public static WpfHeadTailArrowGeometryCreator.ArrowSegmentInfo Invalid;
            public ArrowSegmentInfo(int segmentIndex, Point insidePoint, Point outsidePoint)
            {
                this = new WpfHeadTailArrowGeometryCreator.ArrowSegmentInfo();
                this.SegmentIndex = segmentIndex;
                this.InsidePoint = insidePoint;
                this.OutsidePoint = outsidePoint;
            }

            public ArrowSegmentInfo(int segmentIndex, Point insidePoint, Point outsidePoint, Point[] bezierPoints) : this(segmentIndex, insidePoint, outsidePoint)
            {
                this.BezierPoints = bezierPoints;
                this.IsBezierCurve = true;
            }

            public int SegmentIndex { get; private set; }
            public Point InsidePoint { get; private set; }
            public Point OutsidePoint { get; private set; }
            public bool IsBezierCurve { get; private set; }
            public Point[] BezierPoints { get; private set; }
            public bool IsValid =>
                this.SegmentIndex >= 0;
            static ArrowSegmentInfo()
            {
                Point insidePoint = new Point();
                insidePoint = new Point();
                Invalid = new WpfHeadTailArrowGeometryCreator.ArrowSegmentInfo(-1, insidePoint, insidePoint);
            }
        }

        private class WpfArrowGeometryCreator : WpfHeadTailArrowGeometryCreator.WpfArrowGeometryCreatorBase
        {
            private static readonly float[] boundingCoeffs = new float[] { 1.8f, 1.4f, 1.3f, 2.2f, 1.5f, 1.1f, 3.1f, 1.7f, 0.9f };
            private double inset;

            protected override double CorrectArrowLenght(double arrowLength, double arrowWidth, double penWidth)
            {
                double a = Math.Atan2(arrowWidth / 2.0, arrowLength);
                this.inset = (penWidth / Math.Sin(a)) / 2.0;
                return this.inset;
            }

            protected override Geometry CreateArrowGeometry(Pen pen, double length, double width, TransformGroup transform)
            {
                PathGeometry widenedPathGeometry = new PathGeometry();
                PathFigure figure = new PathFigure {
                    StartPoint = new Point(length + this.inset, width / 2.0),
                    Segments = { 
                        new LineSegment(new Point(this.inset, 0.0), true),
                        new LineSegment(new Point(length + this.inset, -width / 2.0), true)
                    },
                    IsClosed = false
                };
                widenedPathGeometry.Figures.Add(figure);
                Pen pen1 = new Pen(Brushes.Black, pen.Thickness);
                pen1.StartLineCap = PenLineCap.Round;
                pen1.EndLineCap = PenLineCap.Round;
                widenedPathGeometry = widenedPathGeometry.GetWidenedPathGeometry(pen1);
                widenedPathGeometry.Transform = transform;
                return widenedPathGeometry;
            }

            protected override Geometry CreateBoundingGeometry(double length, double width, TransformGroup transform) => 
                base.CreateCircleGeometry(length, boundingCoeffs, transform);

            protected override double GetArrowLengthCoefficient(OutlineHeadTailSize length) => 
                (length != OutlineHeadTailSize.Large) ? ((length != OutlineHeadTailSize.Medium) ? 2.0 : 3.0) : 4.5;

            protected override double GetArrowWidthCoefficient(OutlineHeadTailSize width) => 
                (width != OutlineHeadTailSize.Large) ? ((width != OutlineHeadTailSize.Medium) ? 2.5 : 3.5) : 5.0;
        }

        private abstract class WpfArrowGeometryCreatorBase
        {
            private DevExpress.Office.Drawing.WpfHeadTailArrowGeometryCreator.ArrowSegmentInfo arrowSegmentInfo;
            private Geometry boundingGeometry;
            private OutlineHeadTailSize lengthSize;
            private OutlineHeadTailSize widthSize;
            private double penWidth;

            protected WpfArrowGeometryCreatorBase()
            {
            }

            private Tuple<double, Point> CalculatePointOnBezier(Vector[] bezierPoints, Point startPoint, double arrowLength, double minT, double maxT)
            {
                int steps = (int) Math.Max((double) 10.0, (double) (WpfDrawingShapeHelper.CalcBezierLength(bezierPoints) / 20.0));
                double num3 = (maxT - minT) / ((double) steps);
                double num4 = minT;
                for (int i = 0; i < steps; i++)
                {
                    double t = minT + ((i + 1.0) * num3);
                    Point end = WpfDrawingShapeHelper.CalcBezier(bezierPoints, t);
                    double segmentLength = this.GetSegmentLength(startPoint, end);
                    if (segmentLength == arrowLength)
                    {
                        return new Tuple<double, Point>(t, end);
                    }
                    if (segmentLength > arrowLength)
                    {
                        return this.CalculatePointOnBezierMorePrecisely(bezierPoints, startPoint, arrowLength, num4, t, end, steps);
                    }
                    num4 = t;
                }
                return null;
            }

            private Tuple<double, Point> CalculatePointOnBezierMorePrecisely(Vector[] bezierPoints, Point startPoint, double arrowLength, double minT, double maxT, Point prevPoint, int steps)
            {
                if (steps <= 0)
                {
                    return new Tuple<double, Point>(maxT, prevPoint);
                }
                double t = (minT + maxT) / 2.0;
                Point end = WpfDrawingShapeHelper.CalcBezier(bezierPoints, t);
                double segmentLength = this.GetSegmentLength(startPoint, end);
                steps--;
                return ((segmentLength <= arrowLength) ? this.CalculatePointOnBezierMorePrecisely(bezierPoints, startPoint, arrowLength, t, maxT, end, steps) : this.CalculatePointOnBezierMorePrecisely(bezierPoints, startPoint, arrowLength, minT, t, end, steps));
            }

            private Point CalculatePointOnLine(Point start, Point prev, Point next, double arrowLength, double lengthFromStart)
            {
                bool flag = prev.Equals(start);
                double num = flag ? lengthFromStart : this.GetSegmentLength(prev, next);
                double num2 = (next.X - prev.X) / num;
                double num3 = (next.Y - prev.Y) / num;
                if (!flag)
                {
                    double num7 = (num3 * (start.X - prev.X)) - (num2 * (start.Y - prev.Y));
                    arrowLength = ((num3 * (start.Y - prev.Y)) + (num2 * (start.X - prev.X))) + Math.Sqrt((arrowLength * arrowLength) - (num7 * num7));
                }
                return new Point(prev.X + (num2 * arrowLength), prev.Y + (num3 * arrowLength));
            }

            protected virtual double CorrectArrowLenght(double arrowLength, double arrowWidth, double penWidth) => 
                arrowLength;

            private Geometry CreateArrowCore(PathGeometry geometry, Pen pen, double penWidthLimit, OutlineHeadTailSize lengthSize, OutlineHeadTailSize widthSize, Func<PathGeometry, double, TransformGroup> createTransform)
            {
                this.penWidth = pen.Thickness;
                this.lengthSize = lengthSize;
                this.widthSize = widthSize;
                double num = Math.Max(penWidthLimit, this.penWidth);
                double arrowLength = num * this.GetArrowLengthCoefficient(lengthSize);
                double arrowWidth = num * this.GetArrowWidthCoefficient(widthSize);
                TransformGroup transform = createTransform(geometry, this.CorrectArrowLenght(arrowLength, arrowWidth, this.penWidth));
                this.boundingGeometry = this.CreateBoundingGeometry(arrowLength, arrowWidth, transform);
                return this.CreateArrowGeometry(pen, arrowLength, arrowWidth, transform);
            }

            protected abstract Geometry CreateArrowGeometry(Pen pen, double arrowLength, double arrowWidth, TransformGroup transform);
            protected abstract Geometry CreateBoundingGeometry(double arrowLength, double arrowWidth, TransformGroup transform);
            protected Geometry CreateCircleGeometry(double radius, Transform transform) => 
                this.CreateEllipseGeometry(radius, radius, transform);

            protected Geometry CreateCircleGeometry(double radius, float[] boundingCoeffs, Transform transform)
            {
                radius += boundingCoeffs[(int) (((OutlineHeadTailSize.Large | OutlineHeadTailSize.Medium) * this.lengthSize) + this.widthSize)] * this.penWidth;
                return this.CreateCircleGeometry(radius, transform);
            }

            protected Geometry CreateEllipseGeometry(double length, double width, Transform transform) => 
                new EllipseGeometry(new Rect(-length, -width, 2.0 * length, 2.0 * width)) { Transform = transform };

            private TransformGroup CreateEndArrowTransform(PathGeometry geometry, double arrowLength)
            {
                if (geometry.IsEmpty())
                {
                    return null;
                }
                PathFigure figure = geometry.Figures[0];
                Point lastPoint = this.GetLastPoint(figure);
                return this.CreateTransformCore(lastPoint, this.GetPrevPoint(figure, arrowLength, lastPoint));
            }

            public Geometry CreateHeadArrow(PathGeometry geometry, Pen pen, double penWidthLimit, OutlineHeadTailSize lengthSize, OutlineHeadTailSize widthSize) => 
                this.CreateArrowCore(geometry, pen, penWidthLimit, lengthSize, widthSize, new Func<PathGeometry, double, TransformGroup>(this.CreateStartArrowTransform));

            private TransformGroup CreateStartArrowTransform(PathGeometry geometry, double arrowLength)
            {
                if (geometry.IsEmpty())
                {
                    return null;
                }
                PathFigure figure = geometry.Figures[0];
                Point startPoint = figure.StartPoint;
                return this.CreateTransformCore(startPoint, this.GetNextPoint(figure, arrowLength, startPoint));
            }

            public Geometry CreateTailArrow(PathGeometry geometry, Pen pen, double penWidthLimit, OutlineHeadTailSize lengthSize, OutlineHeadTailSize widthSize) => 
                this.CreateArrowCore(geometry, pen, penWidthLimit, lengthSize, widthSize, new Func<PathGeometry, double, TransformGroup>(this.CreateEndArrowTransform));

            private TransformGroup CreateTransformCore(Point firstPoint, Point secondPoint)
            {
                double angle = (Math.Atan2(secondPoint.Y - firstPoint.Y, secondPoint.X - firstPoint.X) * 180.0) / 3.1415926535897931;
                return new TransformGroup { Children = { 
                    new RotateTransform(angle),
                    new TranslateTransform(firstPoint.X, firstPoint.Y)
                } };
            }

            protected virtual double GetArrowLengthCoefficient(OutlineHeadTailSize length) => 
                this.GetStandartArrowWidthOrLength(length);

            protected virtual double GetArrowWidthCoefficient(OutlineHeadTailSize width) => 
                this.GetStandartArrowWidthOrLength(width);

            private Point GetBezierPoint(Point start, Point end, double t) => 
                new Point(((end.X - start.X) * t) + start.X, ((end.Y - start.Y) * t) + start.Y);

            private Point GetLastPoint(PathFigure figure)
            {
                PathSegment segment = figure.Segments[figure.Segments.Count - 1];
                LineSegment segment2 = segment as LineSegment;
                if (segment2 != null)
                {
                    return segment2.Point;
                }
                BezierSegment segment3 = segment as BezierSegment;
                if (segment3 != null)
                {
                    return segment3.Point3;
                }
                return new Point();
            }

            private Point GetNextPoint(PathFigure figure, double arrowLength, Point firstPoint)
            {
                PathSegmentCollection segments = figure.Segments;
                Point prev = firstPoint;
                Point end = new Point();
                for (int i = 0; i < segments.Count; i++)
                {
                    PathSegment segment = segments[i];
                    LineSegment segment2 = segment as LineSegment;
                    if (segment2 != null)
                    {
                        end = segment2.Point;
                        double segmentLength = this.GetSegmentLength(firstPoint, end);
                        if (segmentLength >= arrowLength)
                        {
                            Point point3 = (segmentLength == arrowLength) ? end : this.CalculatePointOnLine(firstPoint, prev, end, arrowLength, segmentLength);
                            this.arrowSegmentInfo = new DevExpress.Office.Drawing.WpfHeadTailArrowGeometryCreator.ArrowSegmentInfo(i, this.GetPointInsideArrow(firstPoint, point3, arrowLength), end);
                            return point3;
                        }
                    }
                    else
                    {
                        BezierSegment segment3 = segment as BezierSegment;
                        if (segment3 != null)
                        {
                            end = segment3.Point3;
                            Vector[] bezierPoints = WpfDrawingShapeHelper.GetBezierPoints(prev, segment3.Point1, segment3.Point2, end);
                            Tuple<double, Point> tuple = this.CalculatePointOnBezier(bezierPoints, firstPoint, arrowLength, 0.0, 1.0);
                            if (tuple != null)
                            {
                                Point[] splitBezierSegmentPoints = this.GetSplitBezierSegmentPoints(bezierPoints, 1.0 - tuple.Item1);
                                Point[] pointArray1 = new Point[] { splitBezierSegmentPoints[4], splitBezierSegmentPoints[5], end };
                                this.arrowSegmentInfo = new DevExpress.Office.Drawing.WpfHeadTailArrowGeometryCreator.ArrowSegmentInfo(i, splitBezierSegmentPoints[3], end, pointArray1);
                                return tuple.Item2;
                            }
                        }
                    }
                    prev = end;
                }
                this.arrowSegmentInfo = DevExpress.Office.Drawing.WpfHeadTailArrowGeometryCreator.ArrowSegmentInfo.Invalid;
                return end;
            }

            protected virtual Point GetPointInsideArrow(Point start, Point end, double arrowLength) => 
                end;

            private Point GetPrevPoint(PathFigure figure, double arrowLength, Point firstPoint)
            {
                PathSegmentCollection segments = figure.Segments;
                Point prev = firstPoint;
                Point end = new Point();
                for (int i = segments.Count - 1; i >= 0; i--)
                {
                    PathSegment segment = segments[i];
                    LineSegment segment2 = segment as LineSegment;
                    if (segment2 != null)
                    {
                        end = this.GetPrevSegmentEndPoint(figure, i);
                        double segmentLength = this.GetSegmentLength(firstPoint, end);
                        if (segmentLength >= arrowLength)
                        {
                            Point point3 = (segmentLength == arrowLength) ? end : this.CalculatePointOnLine(firstPoint, prev, end, arrowLength, segmentLength);
                            this.arrowSegmentInfo = new DevExpress.Office.Drawing.WpfHeadTailArrowGeometryCreator.ArrowSegmentInfo(i, this.GetPointInsideArrow(firstPoint, point3, arrowLength), end);
                            return point3;
                        }
                    }
                    else
                    {
                        BezierSegment segment3 = segment as BezierSegment;
                        if (segment3 != null)
                        {
                            end = this.GetPrevSegmentEndPoint(figure, i);
                            Vector[] bezierPoints = WpfDrawingShapeHelper.GetBezierPoints(end, segment3.Point1, segment3.Point2, segment3.Point3);
                            Tuple<double, Point> tuple = this.CalculatePointOnBezier(bezierPoints, firstPoint, arrowLength, 1.0, 0.0);
                            if (tuple != null)
                            {
                                Point[] splitBezierSegmentPoints = this.GetSplitBezierSegmentPoints(bezierPoints, 1.0 - tuple.Item1);
                                Point[] pointArray1 = new Point[] { splitBezierSegmentPoints[1], splitBezierSegmentPoints[2], splitBezierSegmentPoints[3] };
                                this.arrowSegmentInfo = new DevExpress.Office.Drawing.WpfHeadTailArrowGeometryCreator.ArrowSegmentInfo(i, splitBezierSegmentPoints[3], end, pointArray1);
                                return tuple.Item2;
                            }
                        }
                    }
                    prev = end;
                }
                this.arrowSegmentInfo = DevExpress.Office.Drawing.WpfHeadTailArrowGeometryCreator.ArrowSegmentInfo.Invalid;
                return end;
            }

            private Point GetPrevSegmentEndPoint(PathFigure figure, int index)
            {
                if (index <= 0)
                {
                    return figure.StartPoint;
                }
                PathSegment segment = figure.Segments[index - 1];
                LineSegment segment2 = segment as LineSegment;
                if (segment2 != null)
                {
                    return segment2.Point;
                }
                BezierSegment segment3 = segment as BezierSegment;
                if (segment3 != null)
                {
                    return segment3.Point3;
                }
                return new Point();
            }

            private double GetSegmentLength(Point start, Point end) => 
                Math.Sqrt(Math.Pow(end.X - start.X, 2.0) + Math.Pow(end.Y - start.Y, 2.0));

            private Point[] GetSplitBezierSegmentPoints(Vector[] bezierPoints, double t)
            {
                Point point = (Point) bezierPoints[0];
                Point end = this.GetBezierPoint((Point) bezierPoints[1], (Point) bezierPoints[0], t);
                Point start = this.GetBezierPoint((Point) bezierPoints[2], (Point) bezierPoints[1], t);
                Point point4 = this.GetBezierPoint((Point) bezierPoints[3], (Point) bezierPoints[2], t);
                Point point5 = this.GetBezierPoint(start, end, t);
                Point point6 = this.GetBezierPoint(point4, start, t);
                Point point7 = this.GetBezierPoint(point6, point5, t);
                Point point8 = (Point) bezierPoints[3];
                return new Point[] { point, end, point5, point7, point6, point4, point8 };
            }

            private double GetStandartArrowWidthOrLength(OutlineHeadTailSize size) => 
                (size != OutlineHeadTailSize.Large) ? ((size != OutlineHeadTailSize.Medium) ? 2.0 : 3.0) : 5.0;

            public Geometry BoundingGeometry =>
                this.boundingGeometry;

            public DevExpress.Office.Drawing.WpfHeadTailArrowGeometryCreator.ArrowSegmentInfo ArrowSegmentInfo
            {
                get => 
                    this.arrowSegmentInfo;
                protected set => 
                    this.arrowSegmentInfo = value;
            }
        }

        private class WpfDiamondArrowGeometryCreator : WpfHeadTailArrowGeometryCreator.WpfOvalArrowGeometryCreator
        {
            protected override Geometry CreateArrowGeometry(Pen pen, double length, double width, TransformGroup transform)
            {
                PathGeometry arrow = new PathGeometry();
                PathFigure figure = new PathFigure {
                    StartPoint = new Point(-length, 0.0),
                    Segments = { 
                        new LineSegment(new Point(0.0, width), false),
                        new LineSegment(new Point(length, 0.0), false),
                        new LineSegment(new Point(0.0, -width), false),
                        new LineSegment(new Point(-length, 0.0), false)
                    },
                    IsClosed = true
                };
                arrow.Figures.Add(figure);
                arrow.Transform = transform;
                return base.FinishArrowGeometry(arrow, transform, pen);
            }
        }

        private class WpfOvalArrowGeometryCreator : WpfHeadTailArrowGeometryCreator.WpfArrowGeometryCreatorBase
        {
            protected override Geometry CreateArrowGeometry(Pen pen, double length, double width, TransformGroup transform)
            {
                Geometry arrow = base.CreateEllipseGeometry(length, width, transform);
                return this.FinishArrowGeometry(arrow, transform, pen);
            }

            protected override Geometry CreateBoundingGeometry(double length, double width, TransformGroup transform) => 
                base.CreateCircleGeometry(Math.Max(length, width), transform);

            protected Geometry FinishArrowGeometry(Geometry arrow, TransformGroup transformGroup, Pen pen)
            {
                if (!base.ArrowSegmentInfo.IsBezierCurve)
                {
                    return arrow;
                }
                PathGeometry widenedPathGeometry = new PathGeometry();
                PathFigure figure = new PathFigure();
                TranslateTransform transform = (TranslateTransform) transformGroup.Children[1];
                figure.StartPoint = new Point(transform.X, transform.Y);
                figure.Segments.Add(new LineSegment(base.ArrowSegmentInfo.InsidePoint, true));
                widenedPathGeometry.Figures.Add(figure);
                widenedPathGeometry = widenedPathGeometry.GetWidenedPathGeometry(pen);
                return Geometry.Combine(arrow, widenedPathGeometry, GeometryCombineMode.Union, Transform.Identity);
            }

            protected override double GetArrowLengthCoefficient(OutlineHeadTailSize length) => 
                base.GetArrowLengthCoefficient(length) / 2.0;

            protected override double GetArrowWidthCoefficient(OutlineHeadTailSize width) => 
                base.GetArrowWidthCoefficient(width) / 2.0;

            protected override Point GetPointInsideArrow(Point start, Point end, double arrowLength) => 
                start;
        }

        private class WpfStealthArrowGeometryCreator : WpfHeadTailArrowGeometryCreator.WpfArrowGeometryCreatorBase
        {
            private static readonly float[] boundingCoeffs = new float[] { 0f, 0.25f, 0.85f, -0.15f, 0f, 0.5f, -0.4f, -0.3f, 0.03f };
            private double inset;

            protected override Geometry CreateArrowGeometry(Pen pen, double length, double width, TransformGroup transform)
            {
                double x = length / this.inset;
                PathGeometry geometry = new PathGeometry();
                PathFigure figure = new PathFigure {
                    StartPoint = new Point(0.0, 0.0),
                    Segments = { 
                        new LineSegment(new Point(x, width / 2.0), false),
                        new LineSegment(new Point(length, 0.0), false),
                        new LineSegment(new Point(x, -width / 2.0), false),
                        new LineSegment(new Point(0.0, 0.0), false)
                    },
                    IsClosed = true
                };
                geometry.Figures.Add(figure);
                geometry.Transform = transform;
                return geometry;
            }

            protected override Geometry CreateBoundingGeometry(double length, double width, TransformGroup transform) => 
                base.CreateCircleGeometry(length / this.inset, boundingCoeffs, transform);

            protected override double GetArrowLengthCoefficient(OutlineHeadTailSize length)
            {
                double arrowLengthCoefficient = base.GetArrowLengthCoefficient(length);
                this.inset = (length == OutlineHeadTailSize.Medium) ? 0.66666666666666663 : 0.6;
                return (arrowLengthCoefficient * this.inset);
            }
        }

        private class WpfTriangleArrowGeometryCreator : WpfHeadTailArrowGeometryCreator.WpfArrowGeometryCreatorBase
        {
            private static readonly float[] boundingCoeffs = new float[] { 0f, 0.25f, 0.85f, -0.15f, 0f, 0.5f, -0.4f, -0.3f, 0.03f };

            protected override Geometry CreateArrowGeometry(Pen pen, double length, double width, TransformGroup transform)
            {
                PathGeometry geometry = new PathGeometry();
                PathFigure figure = new PathFigure {
                    StartPoint = new Point(0.0, 0.0),
                    Segments = { 
                        new LineSegment(new Point(length, width / 2.0), false),
                        new LineSegment(new Point(length, -width / 2.0), false),
                        new LineSegment(new Point(0.0, 0.0), false)
                    },
                    IsClosed = true
                };
                geometry.Figures.Add(figure);
                geometry.Transform = transform;
                return geometry;
            }

            protected override Geometry CreateBoundingGeometry(double length, double width, TransformGroup transform) => 
                base.CreateCircleGeometry(length, boundingCoeffs, transform);

            protected override Point GetPointInsideArrow(Point start, Point end, double arrowLength)
            {
                double num = (end.X - start.X) / arrowLength;
                double num2 = (end.Y - start.Y) / arrowLength;
                return new Point(end.X - ((num * arrowLength) / 10.0), end.Y - ((num2 * arrowLength) / 10.0));
            }
        }
    }
}

