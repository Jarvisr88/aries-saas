namespace DevExpress.Office.Drawing
{
    using System;
    using System.Collections.Generic;

    public static class GeometryCalculator
    {
        public static ModelShapeCustomGeometry Calculate(ModelShapeCustomGeometry geometry, double widthEMU, double heightEMU, ModelShapeGuideList presetAdjustList)
        {
            ShapeGuideCalculator guideCalculator = new ShapeGuideCalculator(geometry, widthEMU, heightEMU, presetAdjustList);
            ModelShapeCustomGeometry geometry2 = new ModelShapeCustomGeometry(geometry.DocumentModelPart);
            CalculatePaths(geometry.Paths, geometry2.Paths, guideCalculator);
            CalculateTextRect(geometry.ShapeTextRectangle, guideCalculator, geometry2.ShapeTextRectangle);
            CalculateConnectionSites(geometry.ConnectionSites, guideCalculator, geometry2.ConnectionSites);
            return geometry2;
        }

        private static void CalculateConnectionSites(ModelShapeConnectionList etalonConnections, ShapeGuideCalculator guideCalculator, ModelShapeConnectionList resultList)
        {
            foreach (ModelShapeConnection connection in etalonConnections)
            {
                ModelShapeConnection item = new ModelShapeConnection {
                    Angle = new AdjustableAngle(connection.Angle.Evaluate(guideCalculator)),
                    X = new AdjustableCoordinate(connection.X.Evaluate(guideCalculator)),
                    Y = new AdjustableCoordinate(connection.Y.Evaluate(guideCalculator))
                };
                resultList.AddCore(item);
            }
        }

        private static void CalculatePaths(ModelShapePathsList etalonPaths, ModelShapePathsList resultPathList, ShapeGuideCalculator guideCalculator)
        {
            PathWalker walker = new PathWalker(guideCalculator);
            foreach (ModelShapePath path in etalonPaths)
            {
                ModelShapePath item = path.Clone(false);
                walker.Walk(path, item.Instructions);
                resultPathList.AddCore(item);
            }
        }

        private static void CalculateTextRect(AdjustableRect etalonRect, ShapeGuideCalculator guideCalculator, AdjustableRect resultRect)
        {
            if ((etalonRect != null) && !etalonRect.IsEmpty())
            {
                double left = etalonRect.Left.Evaluate(guideCalculator);
                resultRect.FromDoubles(left, etalonRect.Top.Evaluate(guideCalculator), etalonRect.Right.Evaluate(guideCalculator), etalonRect.Bottom.Evaluate(guideCalculator));
            }
        }

        private class PathWalker : IPathInstructionWalker
        {
            private ModelPathInstructionList result;
            private ShapeGuideCalculator guideCalculator;
            private double lastX;
            private double lastY;

            public PathWalker(ShapeGuideCalculator guideCalculator)
            {
                this.guideCalculator = guideCalculator;
            }

            private static List<double> ArcToBezierCore(double centerX, double centerY, double wR, double hR, double startAngle, double endAngle, bool firstPart)
            {
                List<double> list = new List<double>();
                double item = Math.Cos(startAngle);
                double num2 = Math.Sin(startAngle);
                double num3 = Math.Cos(endAngle);
                double num4 = Math.Sin(endAngle);
                double d = (endAngle - startAngle) / 2.0;
                double num6 = (1.3333333333333333 * (1.0 - Math.Cos(d))) / Math.Sin(d);
                if (firstPart)
                {
                    list.Add(item);
                    list.Add(num2);
                }
                list.Add(item - (num6 * num2));
                list.Add(num2 + (num6 * item));
                list.Add(num3 + (num6 * num4));
                list.Add(num4 - (num6 * num3));
                list.Add(num3);
                list.Add(num4);
                for (int i = 0; i < (list.Count / 2); i++)
                {
                    list[i * 2] = (list[i * 2] * wR) + centerX;
                    list[(i * 2) + 1] = (list[(i * 2) + 1] * hR) + centerY;
                }
                return list;
            }

            private static double[] ArcToBeziers(double centerX, double centerY, double wR, double hR, double startAngle, double swingAngle)
            {
                List<double> list = new List<double>();
                double angle = startAngle + swingAngle;
                startAngle = UnstretchAngle(startAngle, wR, hR);
                angle = UnstretchAngle(angle, wR, hR);
                double num2 = startAngle;
                for (int i = 0; i < 4; i++)
                {
                    double num5;
                    double num4 = 1.5707963267948966;
                    if (swingAngle > 0.0)
                    {
                        if (num2 >= angle)
                        {
                            break;
                        }
                        num5 = Math.Min(num2 + num4, angle);
                    }
                    else
                    {
                        if (num2 <= angle)
                        {
                            break;
                        }
                        num5 = Math.Max(num2 - num4, angle);
                    }
                    list.AddRange(ArcToBezierCore(centerX, centerY, wR, hR, num2, num5, i == 0));
                    num2 += num4 * ((swingAngle < 0.0) ? -1.0 : 1.0);
                }
                return list.ToArray();
            }

            private static double UnstretchAngle(double angle, double wR, double hR)
            {
                double d = DrawingValueConverter.DegreeToRadian(angle);
                double num2 = Math.Cos(d);
                double num3 = Math.Sin(d);
                if ((Math.Abs(num2) < 1E-05) || (Math.Abs(num3) < 1E-05))
                {
                    return d;
                }
                double num4 = Math.Atan2(num3 / Math.Abs(hR), num2 / Math.Abs(wR));
                return (num4 + ((Math.Round((double) ((d / 6.2831853071795862) - Math.Round((double) (num4 / 6.2831853071795862)))) * 2.0) * 3.1415926535897931));
            }

            public void Visit(PathArc pathArc)
            {
                double hR = pathArc.HeightRadius.Evaluate(this.guideCalculator);
                double wR = pathArc.WidthRadius.Evaluate(this.guideCalculator);
                double degree = DrawingValueConverter.FromPositiveFixedAngle(pathArc.StartAngle.Evaluate(this.guideCalculator));
                double swingAngle = DrawingValueConverter.FromPositiveFixedAngle(pathArc.SwingAngle.Evaluate(this.guideCalculator));
                double num5 = Math.Cos(DrawingValueConverter.DegreeToRadian(degree));
                double num6 = Math.Sin(DrawingValueConverter.DegreeToRadian(degree));
                double d = (hR * wR) / Math.Sqrt((((hR * hR) * num5) * num5) + (((wR * wR) * num6) * num6));
                if (!double.IsNaN(d))
                {
                    double[] numArray = ArcToBeziers(this.lastX - (d * num5), this.lastY - (d * num6), wR, hR, degree, swingAngle);
                    int length = numArray.Length;
                    for (int i = 0; (i + 7) < length; i += 6)
                    {
                        this.result.AddCore(new PathCubicBezier(pathArc.DocumentModelPart, numArray[i + 2], numArray[i + 3], numArray[i + 4], numArray[i + 5], numArray[i + 6], numArray[i + 7]));
                    }
                    if (length > 1)
                    {
                        this.lastX = numArray[length - 2];
                        this.lastY = numArray[length - 1];
                    }
                }
            }

            public void Visit(PathClose pathClose)
            {
                this.result.AddCore(new PathClose());
            }

            public void Visit(PathCubicBezier pathCubicBezier)
            {
                double[] numArray = new double[pathCubicBezier.Points.Count];
                double[] numArray2 = new double[pathCubicBezier.Points.Count];
                for (int i = 0; i < pathCubicBezier.Points.Count; i++)
                {
                    numArray[i] = pathCubicBezier.Points[i].X.Evaluate(this.guideCalculator);
                    numArray2[i] = pathCubicBezier.Points[i].Y.Evaluate(this.guideCalculator);
                }
                this.result.AddCore(new PathCubicBezier(pathCubicBezier.DocumentModelPart, numArray[0], numArray2[0], numArray[1], numArray2[1], numArray[2], numArray2[2]));
                this.lastX = numArray[2];
                this.lastY = numArray2[2];
            }

            public void Visit(PathLine pathLine)
            {
                double x = pathLine.Point.X.Evaluate(this.guideCalculator);
                double y = pathLine.Point.Y.Evaluate(this.guideCalculator);
                this.result.AddCore(new PathLine(x, y));
                this.lastX = x;
                this.lastY = y;
            }

            public void Visit(PathMove pathMove)
            {
                double x = pathMove.Point.X.Evaluate(this.guideCalculator);
                double y = pathMove.Point.Y.Evaluate(this.guideCalculator);
                this.result.AddCore(new PathMove(x, y));
                this.lastX = x;
                this.lastY = y;
            }

            public void Visit(PathQuadraticBezier pathQuadraticBezier)
            {
                double[] numArray = new double[3];
                double[] numArray2 = new double[3];
                double[] numArray3 = new double[4];
                double[] numArray4 = new double[4];
                numArray[0] = this.lastX;
                numArray2[0] = this.lastY;
                for (int i = 1; i < 3; i++)
                {
                    numArray[i] = pathQuadraticBezier.Points[i - 1].X.Evaluate(this.guideCalculator);
                    numArray2[i] = pathQuadraticBezier.Points[i - 1].Y.Evaluate(this.guideCalculator);
                }
                numArray3[0] = numArray[0];
                numArray4[0] = numArray2[0];
                numArray3[1] = numArray[0] + ((2.0 * (numArray[1] - numArray[0])) / 3.0);
                numArray4[1] = numArray2[0] + ((2.0 * (numArray2[1] - numArray2[0])) / 3.0);
                numArray3[2] = numArray[2] + ((2.0 * (numArray[1] - numArray[2])) / 3.0);
                numArray4[2] = numArray2[2] + ((2.0 * (numArray2[1] - numArray2[2])) / 3.0);
                numArray3[3] = numArray[2];
                numArray4[3] = numArray2[2];
                this.lastX = numArray3[3];
                this.lastY = numArray4[3];
                this.result.AddCore(new PathCubicBezier(pathQuadraticBezier.DocumentModelPart, numArray3[1], numArray4[1], numArray3[2], numArray4[2], numArray3[3], numArray4[3]));
            }

            public void Walk(ModelShapePath path, ModelPathInstructionList result)
            {
                this.result = result;
                foreach (IPathInstruction instruction in path.Instructions)
                {
                    instruction.Visit(this);
                }
            }
        }
    }
}

