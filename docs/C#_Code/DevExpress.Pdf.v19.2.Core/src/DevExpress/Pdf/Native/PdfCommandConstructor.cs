namespace DevExpress.Pdf.Native
{
    using DevExpress.Pdf;
    using System;
    using System.Collections.Generic;

    public class PdfCommandConstructor : IDisposable
    {
        private static readonly double ellipticFactor = (0.5 - (((1.0 / Math.Sqrt(2.0)) - 0.5) / 0.75));
        private const byte useEvenOddRule = 0x2a;
        private const byte space = 0x20;
        private static readonly byte[] saveGraphicsStateData = new byte[] { 0x20, 0x71 };
        private static readonly byte[] restoreGraphicsStateData = new byte[] { 0x20, 0x51 };
        private static readonly byte[] nameData = new byte[] { 0x20, 0x2f };
        private static readonly byte[] setGraphicsStateData = new byte[] { 0x20, 0x67, 0x73 };
        private static readonly byte[] setLineStyleData = new byte[] { 0x20, 100 };
        private static readonly byte[] modifyTransformationMatrixData = new byte[] { 0x20, 0x63, 0x6d };
        private static readonly byte[] intersectClipData = new byte[] { 0x20, 0x57 };
        private static readonly byte[] closePathWithoutFillingAndStroking = new byte[] { 0x20, 110 };
        private static readonly byte[] beginPathData = new byte[] { 0x20, 0x6d };
        private static readonly byte[] beginBezierPathData = new byte[] { 0x20, 0x6d };
        private static readonly byte[] setPatternColorSpaceForNonStrokingOperationsData = new byte[] { 0x20, 0x2f, 80, 0x61, 0x74, 0x74, 0x65, 0x72, 110, 0x20, 0x63, 0x73 };
        private static readonly byte[] setPattermColorSpaceForStrokingOperationsData = new byte[] { 0x20, 0x2f, 80, 0x61, 0x74, 0x74, 0x65, 0x72, 110, 0x20, 0x43, 0x53 };
        private static readonly byte[] lineSegmentData = new byte[] { 0x20, 0x6c };
        private static readonly byte[] bezierSegmentData = new byte[] { 0x20, 0x63 };
        private static readonly byte[] closePathData = new byte[] { 0x20, 0x68 };
        private static readonly byte[] setColorSpaceForNonStrokingOperationsData = new byte[] { 0x20, 0x63, 0x73 };
        private static readonly byte[] setColorSpaceForStrokingOperationsData = new byte[] { 0x20, 0x43, 0x53 };
        private static readonly byte[] setGrayColorSpaceForNonStrokingOperationsData = new byte[] { 0x20, 0x67 };
        private static readonly byte[] setCMYKColorSpaceForNonStrokingOperationsData = new byte[] { 0x20, 0x6b };
        private static readonly byte[] setRGBColorSpaceForNonStrokingOperationsData = new byte[] { 0x20, 0x72, 0x67 };
        private static readonly byte[] setGrayColorSpaceForStrokingOperationsData = new byte[] { 0x20, 0x47 };
        private static readonly byte[] setCMYKColorSpaceForStrokingOperationsData = new byte[] { 0x20, 0x4b };
        private static readonly byte[] setRGBColorSpaceForStrokingOperationsData = new byte[] { 0x20, 0x52, 0x47 };
        private static readonly byte[] setColorAdvancedForNonStrokingOperationsData = new byte[] { 0x20, 0x73, 0x63, 110 };
        private static readonly byte[] setColorAdvancedForStrokingOperationsData = new byte[] { 0x20, 0x53, 0x43, 0x4e };
        private static readonly byte[] setLineJoinData = new byte[] { 0x20, 0x6a };
        private static readonly byte[] setMiterLimitData = new byte[] { 0x20, 0x4d };
        private static readonly byte[] setTextRenderingModeData = new byte[] { 0x20, 0x54, 0x72 };
        private static readonly byte[] setLineWidthData = new byte[] { 0x20, 0x77 };
        private static readonly byte[] drawTextData = new byte[] { 0x20, 0x54, 100 };
        private static readonly byte[] setLineCapData = new byte[] { 0x20, 0x4a };
        private static readonly byte[] strokePathData = new byte[] { 0x20, 0x53 };
        private static readonly byte[] obliqueStringData = new byte[] { 0x20, 0x31, 0x20, 0x30, 0x20, 0x30, 0x2e, 0x33, 0x33, 0x33, 0x20, 0x31 };
        private static readonly byte[] fillNonZeroPathData = new byte[] { 0x20, 0x66 };
        private static readonly byte[] appendRectangleData = new byte[] { 0x20, 0x72, 0x65 };
        private static readonly byte[] drawXObjectData = new byte[] { 0x20, 0x44, 0x6f };
        private static readonly byte[] setTextFontData = new byte[] { 0x20, 0x54, 0x66, 0x20 };
        private static readonly byte[] setTextMatrixData = new byte[] { 0x20, 0x54, 0x6d };
        private static readonly byte[] closeAndStrokeData = new byte[] { 0x20, 0x73 };
        private static readonly byte[] beginMarkedContentData = new byte[] { 0x20, 0x2f, 0x54, 120, 0x20, 0x42, 0x4d, 0x43 };
        private static readonly byte[] endMarkedContentData = new byte[] { 0x20, 0x45, 0x4d, 0x43 };
        private static readonly byte[] setCharacterSpacingData = new byte[] { 0x20, 0x54, 0x63 };
        private static readonly byte[] setWordSpacingData = new byte[] { 0x20, 0x54, 0x77 };
        private static readonly byte[] setTextHorizontalScalingData = new byte[] { 0x20, 0x54, 0x7a };
        private static readonly byte[] endTextData = new byte[] { 0x20, 0x45, 0x54 };
        private static readonly byte[] drawStringBeginTextData = new byte[] { 0x20, 0x42, 0x54, 0x20, 0x2f };
        private static readonly byte[] showTextCommand = new byte[] { 0x20, 0x54, 0x6a };
        private static readonly byte[] showTextWithGlyphPositioningCommand = new byte[] { 0x20, 0x54, 0x4a };
        private static readonly byte[] fillAndStrokePath = new byte[] { 0x20, 0x42 };
        private static readonly byte[] drawShadingCommandData = new byte[] { 0x20, 0x73, 0x68 };
        private readonly PdfStreamWriter writer;
        private readonly PdfResources resources;
        private readonly Stack<PdfTransformationMatrix> matrixStack = new Stack<PdfTransformationMatrix>();
        private PdfTransformationMatrix currentTransformationMatrix = new PdfTransformationMatrix();

        public PdfCommandConstructor(PdfResources resources)
        {
            this.resources = resources;
            this.writer = new PdfStreamWriter();
        }

        public void AddCommands(byte[] data)
        {
            if (data != null)
            {
                this.writer.WriteByte(0x20);
                this.writer.Write(data, 0, data.Length);
            }
        }

        public void AppendEllipse(PdfRectangle rect)
        {
            double left = rect.Left;
            double right = rect.Right;
            double num3 = (left + right) / 2.0;
            double num4 = (right - left) * ellipticFactor;
            double num5 = left + num4;
            double num6 = right - num4;
            double bottom = rect.Bottom;
            double top = rect.Top;
            double num9 = (bottom + top) / 2.0;
            double num10 = (top - bottom) * ellipticFactor;
            double num11 = bottom + num10;
            double num12 = top - num10;
            this.writer.WriteDouble(right, true);
            this.writer.WriteDouble(num9, true);
            this.writer.Write(beginPathData, 0, 2);
            this.writer.WriteDouble(right, true);
            this.writer.WriteDouble(num12, true);
            this.writer.WriteDouble(num6, true);
            this.writer.WriteDouble(top, true);
            this.writer.WriteDouble(num3, true);
            this.writer.WriteDouble(top, true);
            this.writer.Write(bezierSegmentData, 0, 2);
            this.writer.WriteDouble(num5, true);
            this.writer.WriteDouble(top, true);
            this.writer.WriteDouble(left, true);
            this.writer.WriteDouble(num12, true);
            this.writer.WriteDouble(left, true);
            this.writer.WriteDouble(num9, true);
            this.writer.Write(bezierSegmentData, 0, 2);
            this.writer.WriteDouble(left, true);
            this.writer.WriteDouble(num11, true);
            this.writer.WriteDouble(num5, true);
            this.writer.WriteDouble(bottom, true);
            this.writer.WriteDouble(num3, true);
            this.writer.WriteDouble(bottom, true);
            this.writer.Write(bezierSegmentData, 0, 2);
            this.writer.WriteDouble(num6, true);
            this.writer.WriteDouble(bottom, true);
            this.writer.WriteDouble(right, true);
            this.writer.WriteDouble(num11, true);
            this.writer.WriteDouble(right, true);
            this.writer.WriteDouble(num9, true);
            this.writer.Write(bezierSegmentData, 0, 2);
        }

        private void AppendPolygon(PdfPoint[] points)
        {
            int num = (points == null) ? 0 : points.Length;
            if (num >= 2)
            {
                PdfPoint point = points[0];
                this.writer.WriteDouble(point.X, true);
                this.writer.WriteDouble(point.Y, true);
                this.writer.Write(beginPathData, 0, 2);
                for (int i = 1; i < num; i++)
                {
                    point = points[i];
                    this.writer.WriteDouble(point.X, true);
                    this.writer.WriteDouble(point.Y, true);
                    this.writer.Write(lineSegmentData, 0, 2);
                }
            }
        }

        public void AppendRectangle(PdfRectangle rect)
        {
            this.writer.WriteDouble(rect.Left, true);
            this.writer.WriteDouble(rect.Bottom, true);
            this.writer.WriteDouble(rect.Width, true);
            this.writer.WriteDouble(rect.Height, true);
            this.writer.Write(appendRectangleData, 0, 3);
        }

        public void BeginMarkedContent()
        {
            this.writer.Write(beginMarkedContentData, 0, 8);
        }

        public void BeginText()
        {
            this.writer.Write(drawStringBeginTextData, 0, 3);
        }

        public void CloseAndStrokePath()
        {
            this.writer.Write(closeAndStrokeData, 0, 2);
        }

        public void ClosePath()
        {
            this.writer.Write(closePathData, 0, 2);
        }

        public PdfTextWriter CreateTextWriter(bool useTwoByteCodePoints) => 
            new PdfTextWriter(this.writer, useTwoByteCodePoints);

        public void Dispose()
        {
            this.writer.Dispose();
        }

        public void DrawBezier(PdfPoint start, PdfPoint controlPoint1, PdfPoint controlPoint2, PdfPoint finish)
        {
            this.writer.WriteDouble(start.X, true);
            this.writer.WriteDouble(start.Y, true);
            this.writer.Write(beginBezierPathData, 0, 2);
            this.writer.WriteDouble(controlPoint1.X, true);
            this.writer.WriteDouble(controlPoint1.Y, true);
            this.writer.WriteDouble(controlPoint2.X, true);
            this.writer.WriteDouble(controlPoint2.Y, true);
            this.writer.WriteDouble(finish.X, true);
            this.writer.WriteDouble(finish.Y, true);
            this.writer.Write(bezierSegmentData, 0, 2);
            this.StrokePath();
        }

        public void DrawBeziers(PdfPoint[] points)
        {
            int length = points.Length;
            if (length >= 4)
            {
                PdfPoint point = points[0];
                this.writer.WriteDouble(point.X, true);
                this.writer.WriteDouble(point.Y, true);
                this.writer.Write(beginBezierPathData, 0, 2);
                int num2 = 1;
                int num3 = length - 1;
                while (true)
                {
                    if (num3 < 3)
                    {
                        this.StrokePath();
                        break;
                    }
                    PdfPoint point2 = points[num2++];
                    PdfPoint point3 = points[num2++];
                    PdfPoint point4 = points[num2++];
                    this.writer.WriteDouble(point2.X, true);
                    this.writer.WriteDouble(point2.Y, true);
                    this.writer.WriteDouble(point3.X, true);
                    this.writer.WriteDouble(point3.Y, true);
                    this.writer.WriteDouble(point4.X, true);
                    this.writer.WriteDouble(point4.Y, true);
                    this.writer.Write(bezierSegmentData, 0, 2);
                    num3 -= 3;
                }
            }
        }

        public void DrawEllipse(PdfRectangle rect)
        {
            this.AppendEllipse(rect);
            this.CloseAndStrokePath();
        }

        public void DrawForm(int xObjectNumber, PdfTransformationMatrix matrix)
        {
            this.DrawXObject(xObjectNumber, matrix);
        }

        public void DrawImage(int xObjectNumber, PdfRectangle bounds)
        {
            this.DrawXObject(xObjectNumber, new PdfTransformationMatrix(bounds.Width, 0.0, 0.0, bounds.Height, bounds.Left, bounds.Bottom));
        }

        public void DrawImage(int xObjectNumber, PdfRectangle bounds, PdfTransformationMatrix transform)
        {
            this.DrawXObject(xObjectNumber, PdfTransformationMatrix.Multiply(new PdfTransformationMatrix(bounds.Width, 0.0, 0.0, bounds.Height, bounds.Left, bounds.Bottom), transform));
        }

        public void DrawLine(PdfPoint pt1, PdfPoint pt2)
        {
            this.writer.WriteDouble(pt1.X, true);
            this.writer.WriteDouble(pt1.Y, true);
            this.writer.Write(beginPathData, 0, 2);
            this.writer.WriteDouble(pt2.X, true);
            this.writer.WriteDouble(pt2.Y, true);
            this.writer.Write(lineSegmentData, 0, 2);
            this.StrokePath();
        }

        public void DrawLines(PdfPoint[] points)
        {
            this.AppendPolygon(points);
            this.StrokePath();
        }

        public void DrawPaths(IList<PdfGraphicsPath> paths)
        {
            foreach (PdfGraphicsPath path in paths)
            {
                this.GeneratePathCommands(path);
            }
            this.StrokePath();
        }

        public void DrawPolygon(PdfPoint[] points)
        {
            this.AppendPolygon(points);
            this.CloseAndStrokePath();
        }

        public void DrawRectangle(PdfRectangle rect)
        {
            this.AppendRectangle(rect);
            this.StrokePath();
        }

        public void DrawShading(PdfShading shading)
        {
            this.writer.Write(nameData, 0, nameData.Length);
            this.writer.WriteString(this.resources.AddShading(shading));
            this.writer.Write(drawShadingCommandData, 0, drawShadingCommandData.Length);
        }

        private void DrawXObject(int xObjectNumber, PdfTransformationMatrix matrix)
        {
            this.SaveGraphicsState();
            this.ModifyTransformationMatrix(matrix);
            this.writer.Write(nameData, 0, 2);
            this.writer.WriteString(this.resources.AddXObject(xObjectNumber));
            this.writer.Write(drawXObjectData, 0, 3);
            this.RestoreGraphicsState();
        }

        public void EndMarkedContent()
        {
            this.writer.Write(endMarkedContentData, 0, 4);
        }

        public void EndText()
        {
            this.writer.Write(endTextData, 0, 3);
        }

        public void FillAndStrokePath(bool nonZero)
        {
            this.writer.Write(fillAndStrokePath, 0, 2);
            if (!nonZero)
            {
                this.writer.WriteByte(0x2a);
            }
        }

        public void FillEllipse(PdfRectangle rect)
        {
            this.AppendEllipse(rect);
            this.writer.Write(closePathData, 0, 2);
            this.FillPath(true);
        }

        public void FillPath(bool nonZero)
        {
            this.writer.Write(fillNonZeroPathData, 0, 2);
            if (!nonZero)
            {
                this.writer.WriteByte(0x2a);
            }
        }

        public void FillPath(IList<PdfGraphicsPath> paths, bool nonZero)
        {
            foreach (PdfGraphicsPath path in paths)
            {
                this.GeneratePathCommands(path);
            }
            this.FillPath(nonZero);
        }

        public void FillPolygon(PdfPoint[] points, bool nonZero)
        {
            this.AppendPolygon(points);
            this.FillPath(nonZero);
        }

        public void FillRectangle(PdfRectangle rect)
        {
            this.AppendRectangle(rect);
            this.FillPath(true);
        }

        private void GeneratePathCommands(PdfGraphicsPath path)
        {
            PdfPoint startPoint = path.StartPoint;
            this.writer.WriteDouble(startPoint.X, true);
            this.writer.WriteDouble(startPoint.Y, true);
            this.writer.Write(beginPathData, 0, 2);
            foreach (PdfGraphicsPathSegment segment in path.Segments)
            {
                PdfBezierGraphicsPathSegment segment2 = segment as PdfBezierGraphicsPathSegment;
                if (segment2 == null)
                {
                    PdfLineGraphicsPathSegment segment3 = segment as PdfLineGraphicsPathSegment;
                    if (segment3 == null)
                    {
                        continue;
                    }
                    PdfPoint point3 = segment3.EndPoint;
                    this.writer.WriteDouble(point3.X, true);
                    this.writer.WriteDouble(point3.Y, true);
                    this.writer.Write(lineSegmentData, 0, 2);
                    continue;
                }
                PdfPoint endPoint = segment2.ControlPoint1;
                this.writer.WriteDouble(endPoint.X, true);
                this.writer.WriteDouble(endPoint.Y, true);
                endPoint = segment2.ControlPoint2;
                this.writer.WriteDouble(endPoint.X, true);
                this.writer.WriteDouble(endPoint.Y, true);
                endPoint = segment2.EndPoint;
                this.writer.WriteDouble(endPoint.X, true);
                this.writer.WriteDouble(endPoint.Y, true);
                this.writer.Write(bezierSegmentData, 0, 2);
            }
            if (path.Closed)
            {
                this.writer.Write(closePathData, 0, 2);
            }
        }

        public void IntersectClip(PdfGraphicsPath path)
        {
            this.IntersectClip(path, true);
        }

        public void IntersectClip(PdfRectangle rect)
        {
            this.AppendRectangle(rect);
            this.writer.Write(intersectClipData, 0, intersectClipData.Length);
            this.writer.Write(closePathWithoutFillingAndStroking, 0, closePathWithoutFillingAndStroking.Length);
        }

        public void IntersectClip(PdfGraphicsPath path, bool nonZero)
        {
            this.GeneratePathCommands(path);
            this.WriteIntersectClipCommand(nonZero);
        }

        public void IntersectClip(IList<PdfGraphicsPath> paths, bool nonZero)
        {
            foreach (PdfGraphicsPath path in paths)
            {
                this.GeneratePathCommands(path);
            }
            this.WriteIntersectClipCommand(nonZero);
        }

        public void IntersectClipByEllipse(PdfRectangle ellipseBBox)
        {
            this.AppendEllipse(ellipseBBox);
            this.writer.Write(closePathData, 0, closePathData.Length);
            this.WriteIntersectClipCommand(true);
        }

        public void ModifyTransformationMatrix(PdfTransformationMatrix matrix)
        {
            this.currentTransformationMatrix = PdfTransformationMatrix.Multiply(matrix, this.currentTransformationMatrix);
            this.writer.WriteDouble(matrix.A, true);
            this.writer.WriteDouble(matrix.B, true);
            this.writer.WriteDouble(matrix.C, true);
            this.writer.WriteDouble(matrix.D, true);
            this.writer.WriteDouble(matrix.E, true);
            this.writer.WriteDouble(matrix.F, true);
            this.writer.Write(modifyTransformationMatrixData, 0, 3);
        }

        public void RestoreGraphicsState()
        {
            if (this.matrixStack.Count > 0)
            {
                this.currentTransformationMatrix = this.matrixStack.Pop();
            }
            this.writer.Write(restoreGraphicsStateData, 0, 2);
        }

        public void SaveGraphicsState()
        {
            this.matrixStack.Push(this.currentTransformationMatrix);
            this.writer.Write(saveGraphicsStateData, 0, 2);
        }

        public void SetCharacterSpacing(double characterSpacing)
        {
            this.writer.WriteDouble(characterSpacing, true);
            this.writer.Write(setCharacterSpacingData, 0, 3);
        }

        public void SetColorForNonStrokingOperations(PdfColor color)
        {
            if (color != null)
            {
                if (color.Pattern == null)
                {
                    double[] components = color.Components;
                    int length = components.Length;
                    if (length == 1)
                    {
                        this.writer.WriteDouble(components[0], true);
                        this.writer.Write(setGrayColorSpaceForNonStrokingOperationsData, 0, 2);
                    }
                    else if (length != 4)
                    {
                        this.writer.WriteDoubleArray(components, 3);
                        this.writer.Write(setRGBColorSpaceForNonStrokingOperationsData, 0, 3);
                    }
                    else
                    {
                        this.writer.WriteDoubleArray(components);
                        this.writer.Write(setCMYKColorSpaceForNonStrokingOperationsData, 0, 2);
                    }
                }
                else
                {
                    this.SetColorWithPattern(color, setColorSpaceForNonStrokingOperationsData, setPatternColorSpaceForNonStrokingOperationsData, setColorAdvancedForNonStrokingOperationsData);
                }
            }
        }

        public void SetColorForStrokingOperations(PdfColor color)
        {
            if (color != null)
            {
                if (color.Pattern == null)
                {
                    double[] components = color.Components;
                    int length = components.Length;
                    if (length == 1)
                    {
                        this.writer.WriteDouble(components[0], true);
                        this.writer.Write(setGrayColorSpaceForStrokingOperationsData, 0, 2);
                    }
                    else if (length != 4)
                    {
                        this.writer.WriteDoubleArray(components, 3);
                        this.writer.Write(setRGBColorSpaceForStrokingOperationsData, 0, 3);
                    }
                    else
                    {
                        this.writer.WriteDoubleArray(components);
                        this.writer.Write(setCMYKColorSpaceForStrokingOperationsData, 0, 2);
                    }
                }
                else
                {
                    this.SetColorWithPattern(color, setColorSpaceForStrokingOperationsData, setPattermColorSpaceForStrokingOperationsData, setColorAdvancedForStrokingOperationsData);
                }
            }
        }

        public void SetColorForStrokingOperations(PdfRGBColor color)
        {
            this.writer.WriteDouble(color.R, true);
            this.writer.WriteDouble(color.G, true);
            this.writer.WriteDouble(color.B, true);
            this.writer.Write(setRGBColorSpaceForStrokingOperationsData, 0, 3);
        }

        private void SetColorWithPattern(PdfColor color, byte[] setColorSpaceData, byte[] setPatternColorSpaceData, byte[] setColorAdvancedData)
        {
            PdfTilingPattern pattern = color.Pattern as PdfTilingPattern;
            if ((pattern == null) || pattern.Colored)
            {
                this.writer.Write(setPatternColorSpaceData, 0, setPatternColorSpaceData.Length);
            }
            else
            {
                PdfColorSpace colorSpace = new PdfPatternColorSpace(new PdfDeviceColorSpace(PdfDeviceColorSpaceKind.RGB));
                this.writer.WriteByte(0x2f);
                this.writer.WriteString(this.resources.AddColorSpace(colorSpace));
                this.writer.Write(setColorSpaceData, 0, setColorSpaceData.Length);
            }
            this.writer.WriteDoubleArray(color.Components);
            this.writer.WriteByte(0x20);
            string s = this.resources.AddPattern(color.Pattern);
            this.writer.WriteByte(0x2f);
            this.writer.WriteString(s);
            this.writer.Write(setColorAdvancedData, 0, setColorAdvancedData.Length);
        }

        public void SetGraphicsStateParameters(PdfGraphicsStateParameters parameters)
        {
            this.resources.AddGraphicsStateParameters(parameters);
            this.writer.Write(nameData, 0, 2);
            this.writer.WriteString(this.resources.FindGraphicsStateParametersName(parameters).Name);
            this.writer.Write(setGraphicsStateData, 0, 3);
        }

        public void SetLineCapStyle(PdfLineCapStyle capStyle)
        {
            this.writer.WriteByte(0x20);
            this.writer.WriteInt((int) capStyle);
            this.writer.Write(setLineCapData, 0, 2);
        }

        public void SetLineJoinStyle(PdfLineJoinStyle lineJoin)
        {
            this.writer.WriteByte(0x20);
            switch (lineJoin)
            {
                case PdfLineJoinStyle.Miter:
                    this.writer.WriteByte(0x30);
                    break;

                case PdfLineJoinStyle.Round:
                    this.writer.WriteByte(0x31);
                    break;

                case PdfLineJoinStyle.Bevel:
                    this.writer.WriteByte(50);
                    break;

                default:
                    break;
            }
            this.writer.Write(setLineJoinData, 0, 2);
        }

        public void SetLineStyle(PdfLineStyle lineStyle)
        {
            this.writer.WriteByte(0x20);
            this.writer.WriteByte(0x5b);
            if (lineStyle.DashPattern != null)
            {
                foreach (double num2 in lineStyle.DashPattern)
                {
                    this.writer.WriteDouble(num2, true);
                }
            }
            this.writer.WriteByte(0x5d);
            this.writer.WriteDouble(lineStyle.DashPhase, true);
            this.writer.Write(setLineStyleData, 0, 2);
        }

        public void SetLineWidth(double lineWidth)
        {
            this.writer.WriteDouble(lineWidth, true);
            this.writer.Write(setLineWidthData, 0, 2);
        }

        public void SetMiterLimit(double miterLimit)
        {
            this.writer.WriteDouble(miterLimit, true);
            this.writer.Write(setMiterLimitData, 0, 2);
        }

        public void SetObliqueTextMatrix(double x, double y)
        {
            this.writer.Write(obliqueStringData, 0, 12);
            this.writer.WriteDouble(x, true);
            this.writer.WriteDouble(y, true);
            this.writer.Write(setTextMatrixData, 0, 3);
        }

        public void SetTextFont(PdfFont font, double fontSize)
        {
            this.SetTextFont(this.resources.AddFont(font), fontSize);
        }

        public void SetTextFont(string fontName, double fontSize)
        {
            this.writer.Write(nameData, 0, 2);
            this.writer.WriteString(fontName);
            this.writer.WriteDouble(fontSize, true);
            this.writer.Write(setTextFontData, 0, 3);
        }

        public void SetTextHorizontalScaling(double textHorizontalScaling)
        {
            this.writer.WriteDouble(textHorizontalScaling, true);
            this.writer.Write(setTextHorizontalScalingData, 0, 3);
        }

        public void SetTextRenderingMode(PdfTextRenderingMode mode)
        {
            this.writer.WriteByte(0x20);
            this.writer.WriteInt((int) mode);
            this.writer.Write(setTextRenderingModeData, 0, 3);
        }

        public void SetWordSpacing(double wordSpacing)
        {
            this.writer.WriteDouble(wordSpacing, true);
            this.writer.Write(setWordSpacingData, 0, 3);
        }

        public void ShowText(byte[] text, double[] glyphOffsets)
        {
            if ((text != null) && (text.Length != 0))
            {
                this.writer.WriteByte(0x20);
                if (glyphOffsets == null)
                {
                    this.writer.WriteHexadecimalString(text);
                    this.writer.Write(showTextCommand, 0, 3);
                }
                else
                {
                    this.writer.WriteOpenBracket();
                    int length = text.Length;
                    int index = 0;
                    while (true)
                    {
                        if (index >= length)
                        {
                            double num2 = glyphOffsets[glyphOffsets.Length - 1];
                            if (num2 != 0.0)
                            {
                                this.writer.WriteByte(0x20);
                                this.writer.WriteDouble(num2, true);
                            }
                            this.writer.WriteClosedBracket();
                            this.writer.Write(showTextWithGlyphPositioningCommand, 0, 3);
                            break;
                        }
                        double num4 = glyphOffsets[index];
                        if (num4 != 0.0)
                        {
                            this.writer.WriteByte(0x20);
                            this.writer.WriteDouble(num4, true);
                        }
                        this.writer.WriteByte(0x20);
                        List<byte> list1 = new List<byte>();
                        list1.Add(text[index]);
                        List<byte> data = list1;
                        index++;
                        while (true)
                        {
                            if ((index >= length) || (glyphOffsets[index] != 0.0))
                            {
                                this.writer.WriteHexadecimalString(data);
                                break;
                            }
                            data.Add(text[index]);
                            index++;
                        }
                    }
                }
            }
        }

        public void StartTextLineWithOffsets(double x, double y)
        {
            this.writer.WriteDouble(x, true);
            this.writer.WriteDouble(y, true);
            this.writer.Write(drawTextData, 0, 3);
        }

        public void StrokePath()
        {
            this.writer.Write(strokePathData, 0, 2);
        }

        private void WriteIntersectClipCommand(bool nonZero)
        {
            this.writer.Write(intersectClipData, 0, intersectClipData.Length);
            if (!nonZero)
            {
                this.writer.WriteByte(0x2a);
            }
            this.writer.Write(closePathWithoutFillingAndStroking, 0, closePathWithoutFillingAndStroking.Length);
        }

        public PdfDocumentCatalog DocumentCatalog =>
            this.resources.DocumentCatalog;

        public byte[] Commands =>
            this.writer.Commands;

        public PdfTransformationMatrix CurrentTransformationMatrix =>
            this.currentTransformationMatrix;
    }
}

