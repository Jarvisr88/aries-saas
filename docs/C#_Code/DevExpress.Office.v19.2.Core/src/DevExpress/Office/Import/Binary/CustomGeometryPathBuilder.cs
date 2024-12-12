namespace DevExpress.Office.Import.Binary
{
    using DevExpress.Office.Drawing;
    using DevExpress.Office.Utils;
    using System;
    using System.Drawing;

    internal class CustomGeometryPathBuilder
    {
        private readonly ModelShapeCustomGeometry customGeometry;
        private readonly Point[] points;
        private readonly MsoPathInfo[] msoPathInfos;
        private readonly XlsAdjustableCoordinateCache adjustableCoordinateCache;
        private readonly XlsAdjustableAngleCache adjustableAngleCache;
        private int lastX;
        private int lastY;
        private int pointsIndex;

        public CustomGeometryPathBuilder(ModelShapeCustomGeometry customGeometry, Point[] points, MsoPathInfo[] msoPathInfos, XlsAdjustableCoordinateCache adjustableCoordinateCache, XlsAdjustableAngleCache adjustableAngleCache)
        {
            this.customGeometry = customGeometry;
            this.points = points;
            this.msoPathInfos = msoPathInfos;
            this.adjustableCoordinateCache = adjustableCoordinateCache;
            this.adjustableAngleCache = adjustableAngleCache;
            this.lastX = -1;
            this.lastY = -1;
        }

        private void AddAngleEllipse(MsoPathInfo pathInfo, bool moveToStart)
        {
            bool flag = moveToStart;
            for (int i = 0; (i < pathInfo.Segments) && ((this.pointsIndex + 2) < this.points.Length); i++)
            {
                Point point = this.points[this.pointsIndex];
                Point point2 = this.points[this.pointsIndex + 1];
                Point point3 = this.points[this.pointsIndex + 2];
                double angle = (((float) -point3.X) / 65536f) % 360f;
                double a = (((float) -point3.Y) / 65536f) % 360f;
                try
                {
                    Point point4 = GetIntersectionPoint(point.X, point.Y, (double) point2.X, (double) point2.Y, angle);
                    Point point5 = GetIntersectionPoint(point.X, point.Y, (double) point2.X, (double) point2.Y, angle + a);
                    if ((point4.X != this.lastX) || (point4.Y != this.lastY))
                    {
                        this.lastX = point4.X;
                        this.lastY = point4.Y;
                        if (!flag)
                        {
                            this.Path.Instructions.Add(new PathLine(this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastX), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastY)));
                        }
                        else
                        {
                            this.Path.Instructions.Add(new PathMove(this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastX), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastY)));
                            flag = false;
                        }
                    }
                    angle *= 60000.0;
                    a *= 60000.0;
                    this.Path.Instructions.Add(new PathArc(this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(point2.X), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(point2.Y), this.adjustableAngleCache.GetCachedAdjustableAngle((int) Math.Round(angle)), this.adjustableAngleCache.GetCachedAdjustableAngle((int) Math.Round(a))));
                    this.lastX = point5.X;
                    this.lastY = point5.Y;
                }
                catch
                {
                }
                this.pointsIndex += 3;
            }
        }

        private void AddArc(MsoPathInfo pathInfo, bool clockWise)
        {
            this.AddArcCore(pathInfo, clockWise, true);
        }

        private void AddArcCore(MsoPathInfo pathInfo, bool clockWise, bool moveToStart)
        {
            bool flag = moveToStart;
            for (int i = 0; (i < (pathInfo.Segments / 4)) && ((this.pointsIndex + 3) < this.points.Length); i++)
            {
                int pointsIndex = this.pointsIndex;
                this.pointsIndex = pointsIndex + 1;
                Point point = this.points[pointsIndex];
                pointsIndex = this.pointsIndex;
                this.pointsIndex = pointsIndex + 1;
                Point point2 = this.points[pointsIndex];
                int num2 = point2.X - point.X;
                int num3 = point2.Y - point.Y;
                pointsIndex = this.pointsIndex;
                this.pointsIndex = pointsIndex + 1;
                Point point3 = this.points[pointsIndex];
                pointsIndex = this.pointsIndex;
                this.pointsIndex = pointsIndex + 1;
                Point point4 = this.points[pointsIndex];
                if ((num2 != 0) && (num3 != 0))
                {
                    int num4 = point.X + (num2 / 2);
                    int num5 = point.Y + (num3 / 2);
                    int num6 = AngleByVector(point3.X - num4, point3.Y - num5);
                    int num7 = AngleBetweenVectors(point3.X - num4, point3.Y - num5, point4.X - num4, point4.Y - num5);
                    num7 = !clockWise ? (num7 - 0x1499700) : (num7 + 0x1499700);
                    num7 = num7 % 0x1499700;
                    num7 ??= 0x1499700;
                    int num8 = Math.Abs((int) (num2 / 2));
                    int num9 = Math.Abs((int) (num3 / 2));
                    if ((point3.X != this.lastX) || (point3.Y != this.lastY))
                    {
                        this.lastX = point3.X;
                        this.lastY = point3.Y;
                        if (!flag)
                        {
                            this.Path.Instructions.Add(new PathLine(this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastX), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastY)));
                        }
                        else
                        {
                            this.Path.Instructions.Add(new PathMove(this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastX), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastY)));
                            flag = false;
                        }
                    }
                    this.Path.Instructions.Add(new PathArc(this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(num8), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(num9), this.adjustableAngleCache.GetCachedAdjustableAngle(num6), this.adjustableAngleCache.GetCachedAdjustableAngle(num7)));
                    this.lastX = point4.X;
                    this.lastY = point4.Y;
                }
            }
        }

        private void AddArcTo(MsoPathInfo pathInfo, bool clockWise)
        {
            this.AddArcCore(pathInfo, clockWise, false);
        }

        private void AddCurveToSegments(MsoPathInfo pathInfo)
        {
            for (int i = 0; (i < pathInfo.Segments) && ((this.pointsIndex + 2) < this.points.Length); i++)
            {
                this.lastX = this.points[this.pointsIndex + 2].X;
                this.lastY = this.points[this.pointsIndex + 2].Y;
                this.Path.Instructions.Add(new PathCubicBezier(this.Path.DocumentModelPart, this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.points[this.pointsIndex].X), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.points[this.pointsIndex].Y), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.points[this.pointsIndex + 1].X), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.points[this.pointsIndex + 1].Y), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastX), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastY)));
                this.pointsIndex += 3;
            }
        }

        private void AddEllipticalQuadrantCore(Point startPoint, Point endPoint, bool tangentialX)
        {
            int num = Math.Abs((int) (endPoint.X - startPoint.X));
            int num2 = Math.Abs((int) (endPoint.Y - startPoint.Y));
            int num3 = tangentialX ? startPoint.X : ((startPoint.X > endPoint.X) ? (startPoint.X - num) : (startPoint.X + num));
            int num4 = tangentialX ? ((startPoint.Y > endPoint.Y) ? (startPoint.Y - num2) : (startPoint.Y + num2)) : startPoint.Y;
            int num5 = AngleByVector(startPoint.X - num3, startPoint.Y - num4);
            int num6 = AngleBetweenVectors(startPoint.X - num3, startPoint.Y - num4, endPoint.X - num3, endPoint.Y - num4);
            this.Path.Instructions.Add(new PathArc(this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(num), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(num2), this.adjustableAngleCache.GetCachedAdjustableAngle(num5), this.adjustableAngleCache.GetCachedAdjustableAngle(num6)));
        }

        private void AddEllipticalQuadrantX(MsoPathInfo pathInfo)
        {
            for (int i = 0; (i < pathInfo.Segments) && (this.pointsIndex < this.points.Length); i++)
            {
                Point startPoint = new Point(this.lastX, this.lastY);
                int pointsIndex = this.pointsIndex;
                this.pointsIndex = pointsIndex + 1;
                Point endPoint = this.points[pointsIndex];
                this.lastX = endPoint.X;
                this.lastY = endPoint.Y;
                if ((i % 2) == 0)
                {
                    this.AddEllipticalQuadrantXCore(startPoint, endPoint);
                }
                else
                {
                    this.AddEllipticalQuadrantYCore(startPoint, endPoint);
                }
            }
        }

        private void AddEllipticalQuadrantXCore(Point startPoint, Point endPoint)
        {
            this.AddEllipticalQuadrantCore(startPoint, endPoint, true);
        }

        private void AddEllipticalQuadrantY(MsoPathInfo pathInfo)
        {
            for (int i = 0; (i < pathInfo.Segments) && (this.pointsIndex < this.points.Length); i++)
            {
                Point startPoint = new Point(this.lastX, this.lastY);
                int pointsIndex = this.pointsIndex;
                this.pointsIndex = pointsIndex + 1;
                Point endPoint = this.points[pointsIndex];
                this.lastX = endPoint.X;
                this.lastY = endPoint.Y;
                if ((i % 2) == 0)
                {
                    this.AddEllipticalQuadrantYCore(startPoint, endPoint);
                }
                else
                {
                    this.AddEllipticalQuadrantXCore(startPoint, endPoint);
                }
            }
        }

        private void AddEllipticalQuadrantYCore(Point startPoint, Point endPoint)
        {
            this.AddEllipticalQuadrantCore(startPoint, endPoint, false);
        }

        private void AddLineToSegments(MsoPathInfo pathInfo)
        {
            for (int i = 0; (i < pathInfo.Segments) && (this.pointsIndex < this.points.Length); i++)
            {
                this.lastX = this.points[this.pointsIndex].X;
                this.lastY = this.points[this.pointsIndex].Y;
                this.Path.Instructions.Add(new PathLine(this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastX), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastY)));
                this.pointsIndex++;
            }
        }

        private void AddNewPath()
        {
            ModelShapePath item = new ModelShapePath(this.Path.DocumentModelPart) {
                Width = this.Path.Width,
                Height = this.Path.Height
            };
            this.customGeometry.Paths.Add(item);
        }

        private void AddQuadraticBezier(MsoPathInfo pathInfo)
        {
            for (int i = 0; (i < pathInfo.Segments) && ((this.pointsIndex + 1) < this.points.Length); i++)
            {
                int pointsIndex = this.pointsIndex;
                this.pointsIndex = pointsIndex + 1;
                Point point = this.points[pointsIndex];
                Point point2 = this.points[this.pointsIndex];
                this.lastX = point2.X;
                this.lastY = point2.Y;
                this.Path.Instructions.Add(new PathQuadraticBezier(this.Path.DocumentModelPart, this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(point.X), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(point.Y), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastX), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastY)));
            }
        }

        private static int AngleBetweenVectors(int x1, int y1, int x2, int y2)
        {
            double num = (x1 * x2) + (y1 * y2);
            double num2 = (x1 * y2) - (x2 * y1);
            double num4 = (Math.Atan(num2 / num) * 180.0) / 3.1415926535897931;
            if ((num == 0.0) && (num2 > 0.0))
            {
                num4 = 90.0;
            }
            if ((num == 0.0) && (num2 < 0.0))
            {
                num4 = -90.0;
            }
            if ((num < 0.0) && (num2 >= 0.0))
            {
                num4 += 180.0;
            }
            if ((num < 0.0) && (num2 < 0.0))
            {
                num4 += 180.0;
            }
            return (int) (num4 * 60000.0);
        }

        private static int AngleByVector(int x, int y)
        {
            double num2 = (Math.Atan2((double) y, (double) x) * 180.0) / 3.1415926535897931;
            if (num2 < 0.0)
            {
                num2 += 360.0;
            }
            return (int) (num2 * 60000.0);
        }

        internal void CreateComplexPath()
        {
            this.pointsIndex = 0;
            for (int i = 0; i < this.msoPathInfos.Length; i++)
            {
                MsoPathInfo pathInfo = this.msoPathInfos[i];
                MsoPathType pathType = pathInfo.PathType;
                switch (pathType)
                {
                    case MsoPathType.LineTo:
                        this.AddLineToSegments(pathInfo);
                        break;

                    case MsoPathType.CurveTo:
                        this.AddCurveToSegments(pathInfo);
                        break;

                    case MsoPathType.MoveTo:
                        if (this.pointsIndex < this.points.Length)
                        {
                            this.lastX = this.points[this.pointsIndex].X;
                            this.lastY = this.points[this.pointsIndex].Y;
                            this.Path.Instructions.Add(new PathMove(this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastX), this.adjustableCoordinateCache.GetCachedAdjustableCoordinate(this.lastY)));
                            this.pointsIndex++;
                        }
                        break;

                    case MsoPathType.Close:
                        this.Path.Instructions.Add(new PathClose());
                        break;

                    case MsoPathType.End:
                        if (i != (this.msoPathInfos.Length - 1))
                        {
                            this.AddNewPath();
                        }
                        break;

                    case MsoPathType.Escape:
                    case MsoPathType.ClientEscape:
                        this.ProcessEscapeSegments(pathInfo);
                        break;

                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        private static Point GetIntersectionPoint(int centerX, int centerY, double wR, double hR, double angle)
        {
            double a = (angle / 180.0) * 3.1415926535897931;
            double num2 = Math.Atan2(wR * Math.Sin(a), hR * Math.Cos(a));
            double num4 = (hR * Math.Sin(num2)) + centerY;
            return new Point((int) Math.Round((double) ((wR * Math.Cos(num2)) + centerX)), (int) Math.Round(num4));
        }

        private void ProcessEscapeSegments(MsoPathInfo pathInfo)
        {
            switch (pathInfo.PathEscape)
            {
                case MsoPathEscape.Extension:
                case MsoPathEscape.AutoLine:
                case MsoPathEscape.AutoCurve:
                case MsoPathEscape.CornerLine:
                case MsoPathEscape.CornerCurve:
                case MsoPathEscape.SmoothLine:
                case MsoPathEscape.SmoothCurve:
                case MsoPathEscape.SymmetricLine:
                case MsoPathEscape.SymmetricCurve:
                case MsoPathEscape.Freeform:
                case MsoPathEscape.FillColor:
                case MsoPathEscape.LineColor:
                    return;

                case MsoPathEscape.AngleEllipseTo:
                    this.AddAngleEllipse(pathInfo, false);
                    return;

                case MsoPathEscape.AngleEllipse:
                    this.AddAngleEllipse(pathInfo, true);
                    return;

                case MsoPathEscape.ArcTo:
                    this.AddArcTo(pathInfo, false);
                    return;

                case MsoPathEscape.Arc:
                    this.AddArc(pathInfo, false);
                    return;

                case MsoPathEscape.ClockwiseArcTo:
                    this.AddArcTo(pathInfo, true);
                    return;

                case MsoPathEscape.ClockwiseArc:
                    this.AddArc(pathInfo, true);
                    return;

                case MsoPathEscape.EllipticalQuadrantX:
                    this.AddEllipticalQuadrantX(pathInfo);
                    return;

                case MsoPathEscape.EllipticalQuadrantY:
                    this.AddEllipticalQuadrantY(pathInfo);
                    return;

                case MsoPathEscape.QuadraticBezier:
                    this.AddQuadraticBezier(pathInfo);
                    return;

                case MsoPathEscape.NoFill:
                    this.Path.FillMode = PathFillMode.None;
                    return;

                case MsoPathEscape.NoLine:
                    this.Path.Stroke = false;
                    return;
            }
            throw new ArgumentOutOfRangeException();
        }

        private ModelShapePath Path =>
            this.customGeometry.Paths.Last;
    }
}

