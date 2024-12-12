namespace DevExpress.Office.Drawing
{
    using DevExpress.Office;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal class GraphicsWarpTransformer
    {
        private readonly ModelShapeCustomGeometry warpGeometry;
        private readonly double[] pathLength;
        private readonly List<double>[] pathInstructionsLength;
        private readonly List<PointD>[] firstPoints;
        private readonly IDocumentModel documentModel;
        private readonly PathInsidePointCalculator insidePointCalculator;

        public GraphicsWarpTransformer(IDocumentModel documentModel, ModelShapeCustomGeometry warpGeometry)
        {
            this.documentModel = documentModel;
            int count = warpGeometry.Paths.Count;
            this.PathPairsCount = count / 2;
            this.warpGeometry = warpGeometry;
            this.pathLength = new double[count];
            this.pathInstructionsLength = new List<double>[count];
            this.firstPoints = new List<PointD>[count];
            this.FillPathInfo();
            this.insidePointCalculator = new PathInsidePointCalculator();
        }

        private void FillPathInfo()
        {
            PathLengthCalculator calculator = new PathLengthCalculator();
            for (int i = 0; i < this.warpGeometry.Paths.Count; i++)
            {
                this.pathLength[i] = calculator.Walk(this.warpGeometry.Paths[i]);
                this.pathInstructionsLength[i] = calculator.InstructionsLength;
                this.firstPoints[i] = calculator.FirstPoints;
            }
        }

        private PointD FindBottom(int bucketIndex, double p)
        {
            int pathIndex = (bucketIndex * 2) + 1;
            return this.FindPointInsidePath(p, pathIndex);
        }

        private PointD FindPointInsidePath(double p, int pathIndex)
        {
            ModelShapePath path = this.warpGeometry.Paths[pathIndex];
            int count = path.Instructions.Count;
            for (int i = 0; i < count; i++)
            {
                double num3 = this.pathInstructionsLength[pathIndex][i] / this.pathLength[pathIndex];
                if (p < num3)
                {
                    double num4 = p / num3;
                    return this.insidePointCalculator.CalcPoint(path.Instructions[i], num4, this.firstPoints[pathIndex][i]);
                }
                p -= num3;
            }
            return this.insidePointCalculator.CalcPoint(path.Instructions[path.Instructions.Count - 1], 1.0, this.firstPoints[pathIndex][path.Instructions.Count - 1]);
        }

        private PointD FindTop(int bucketIndex, double p)
        {
            int pathIndex = bucketIndex * 2;
            return this.FindPointInsidePath(p, pathIndex);
        }

        public GraphicsPath TransformGraphics(GraphicsPath sourceGraphicsPath, RectangleF textBounds, int bucketIndex)
        {
            PathData pathData = sourceGraphicsPath.PathData;
            PointF[] points = pathData.Points;
            byte[] types = pathData.Types;
            double maxValue = double.MaxValue;
            for (int i = 0; i < points.Length; i++)
            {
                PointF tf = points[i];
                double p = (tf.X - textBounds.X) / textBounds.Width;
                double num4 = (tf.Y - textBounds.Y) / textBounds.Height;
                PointD td = this.FindTop(bucketIndex, p);
                PointD td2 = this.FindBottom(bucketIndex, p);
                double num5 = td.X + ((td2.X - td.X) * num4);
                double num6 = td.Y + ((td2.Y - td.Y) * num4);
                num5 = this.documentModel.UnitConverter.EmuToModelUnitsD(num5);
                num6 = this.documentModel.UnitConverter.EmuToModelUnitsD(num6);
                num6 = this.documentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits((float) num6);
                maxValue = Math.Min(maxValue, num6);
                points[i] = new PointF(this.documentModel.ToDocumentLayoutUnitConverter.ToLayoutUnits((float) num5), (float) num6);
            }
            GraphicsPath path = new GraphicsPath(points, types, sourceGraphicsPath.FillMode);
            sourceGraphicsPath.Dispose();
            return path;
        }

        public int PathPairsCount { get; private set; }

        private class PathInsidePointCalculator : IPathInstructionWalker
        {
            private GraphicsWarpTransformer.PointD firstPoint;
            private double p;
            private GraphicsWarpTransformer.PointD result;

            public GraphicsWarpTransformer.PointD CalcPoint(IPathInstruction instruction, double p, GraphicsWarpTransformer.PointD firstPoint)
            {
                this.result = GraphicsWarpTransformer.PointD.Empty;
                this.firstPoint = firstPoint;
                this.p = p;
                instruction.Visit(this);
                return this.result;
            }

            public void Visit(PathArc pathArc)
            {
            }

            public void Visit(PathClose value)
            {
            }

            public void Visit(PathCubicBezier value)
            {
                double p = this.p;
                double num10 = p * p;
                double num11 = 1.0 - p;
                double num12 = (num11 * num11) * num11;
                double num13 = ((3.0 * p) * num11) * num11;
                double num14 = (3.0 * num10) * num11;
                double num15 = num10 * p;
                double x = (((num12 * this.firstPoint.X) + (num13 * value.Points[0].X.ValueEMU)) + (num14 * value.Points[1].X.ValueEMU)) + (num15 * value.Points[2].X.ValueEMU);
                this.result = new GraphicsWarpTransformer.PointD(x, (((num12 * this.firstPoint.Y) + (num13 * value.Points[0].Y.ValueEMU)) + (num14 * value.Points[1].Y.ValueEMU)) + (num15 * value.Points[2].Y.ValueEMU));
            }

            public void Visit(PathLine pathLine)
            {
                double x = this.firstPoint.X;
                double y = this.firstPoint.Y;
                double num5 = x + ((pathLine.Point.X.ValueEMU - x) * this.p);
                this.result = new GraphicsWarpTransformer.PointD(num5, y + ((pathLine.Point.Y.ValueEMU - y) * this.p));
            }

            public void Visit(PathMove pathMove)
            {
                this.result = this.firstPoint;
            }

            public void Visit(PathQuadraticBezier value)
            {
                double p = this.p;
                double num8 = p * p;
                double num9 = 1.0 - p;
                double num10 = num9 * num9;
                double num11 = (2.0 * p) * num9;
                double x = ((num10 * this.firstPoint.X) + (num11 * value.Points[0].X.ValueEMU)) + (num8 * value.Points[1].X.ValueEMU);
                this.result = new GraphicsWarpTransformer.PointD(x, ((num10 * this.firstPoint.Y) + (num11 * value.Points[0].Y.ValueEMU)) + (num8 * value.Points[1].Y.ValueEMU));
            }
        }

        private class PathLengthCalculator : IPathInstructionWalker
        {
            private double currentInstructionLength;
            private GraphicsWarpTransformer.PointD firstPoint;

            public void Visit(PathArc pathArc)
            {
            }

            public void Visit(PathClose value)
            {
            }

            public void Visit(PathCubicBezier value)
            {
                double x = this.firstPoint.X;
                double y = this.firstPoint.Y;
                double valueEMU = value.Points[0].X.ValueEMU;
                double num4 = value.Points[0].Y.ValueEMU;
                double num5 = value.Points[1].X.ValueEMU;
                double num6 = value.Points[1].Y.ValueEMU;
                double num7 = value.Points[2].X.ValueEMU;
                double num8 = value.Points[2].Y.ValueEMU;
                double num9 = 0.0;
                double num10 = 0.001;
                double num11 = x;
                double num12 = y;
                for (int i = 0; i < 0x3e8; i++)
                {
                    double num14 = num10 * num10;
                    double num15 = 1.0 - num10;
                    double num16 = (num15 * num15) * num15;
                    double num17 = ((3.0 * num10) * num15) * num15;
                    double num18 = (3.0 * num14) * num15;
                    double num19 = num14 * num10;
                    double num20 = (((num16 * x) + (num17 * valueEMU)) + (num18 * num5)) + (num19 * num7);
                    double num21 = (((num16 * y) + (num17 * num4)) + (num18 * num6)) + (num19 * num8);
                    double num22 = num20 - num11;
                    double num23 = num21 - num12;
                    num9 += Math.Sqrt((num22 * num22) + (num23 * num23));
                    num11 = num20;
                    num12 = num21;
                    num10 += 0.001;
                }
                this.currentInstructionLength = num9;
                this.firstPoint = new GraphicsWarpTransformer.PointD(num7, num8);
            }

            public void Visit(PathLine pathLine)
            {
                double valueEMU = pathLine.Point.X.ValueEMU;
                double y = pathLine.Point.Y.ValueEMU;
                double num5 = valueEMU - this.firstPoint.X;
                double num6 = y - this.firstPoint.Y;
                double num7 = Math.Sqrt((num5 * num5) + (num6 * num6));
                this.currentInstructionLength = num7;
                this.firstPoint = new GraphicsWarpTransformer.PointD(valueEMU, y);
            }

            public void Visit(PathMove pathMove)
            {
                this.firstPoint = new GraphicsWarpTransformer.PointD(pathMove.Point.X.ValueEMU, pathMove.Point.Y.ValueEMU);
            }

            public void Visit(PathQuadraticBezier value)
            {
                double x = this.firstPoint.X;
                double y = this.firstPoint.Y;
                double valueEMU = value.Points[0].X.ValueEMU;
                double num4 = value.Points[0].Y.ValueEMU;
                double num5 = value.Points[1].X.ValueEMU;
                double num6 = value.Points[1].Y.ValueEMU;
                double num7 = 0.0;
                double num8 = 0.001;
                double num9 = x;
                double num10 = y;
                for (int i = 0; i < 0x3e8; i++)
                {
                    double num12 = num8 * num8;
                    double num13 = 1.0 - num8;
                    double num14 = num13 * num13;
                    double num15 = (2.0 * num8) * num13;
                    double num16 = ((num14 * x) + (num15 * valueEMU)) + (num12 * num5);
                    double num17 = ((num14 * y) + (num15 * num4)) + (num12 * num6);
                    double num18 = num16 - num9;
                    double num19 = num17 - num10;
                    num7 += Math.Sqrt((num18 * num18) + (num19 * num19));
                    num8 += 0.001;
                    num9 = num16;
                    num10 = num17;
                }
                this.currentInstructionLength = num7;
                this.firstPoint = new GraphicsWarpTransformer.PointD(num5, num6);
            }

            public double Walk(ModelShapePath path)
            {
                this.FirstPoints = new List<GraphicsWarpTransformer.PointD>();
                this.InstructionsLength = new List<double>();
                double num = 0.0;
                for (int i = 0; i < path.Instructions.Count; i++)
                {
                    IPathInstruction instruction = path.Instructions[i];
                    this.currentInstructionLength = 0.0;
                    this.FirstPoints.Add(this.firstPoint);
                    instruction.Visit(this);
                    num += this.currentInstructionLength;
                    this.InstructionsLength.Add(this.currentInstructionLength);
                }
                return num;
            }

            public List<GraphicsWarpTransformer.PointD> FirstPoints { get; private set; }

            public List<double> InstructionsLength { get; private set; }
        }

        [StructLayout(LayoutKind.Sequential)]
        private struct PointD
        {
            private static readonly GraphicsWarpTransformer.PointD emptyPoint;
            public double X { get; set; }
            public double Y { get; set; }
            public static GraphicsWarpTransformer.PointD Empty =>
                emptyPoint;
            public PointD(double x, double y)
            {
                this = new GraphicsWarpTransformer.PointD();
                this.X = x;
                this.Y = y;
            }

            static PointD()
            {
            }
        }
    }
}

