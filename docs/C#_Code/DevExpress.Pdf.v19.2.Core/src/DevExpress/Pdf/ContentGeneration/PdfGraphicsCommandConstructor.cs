namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Drawing;

    public class PdfGraphicsCommandConstructor : PdfDisposableObject
    {
        private const float pdfDpi = 72f;
        private readonly PdfRectangle bBox;
        private readonly PdfResources resources;
        private readonly PdfGraphicsStateStack graphicsStateStack;
        private readonly PdfCommandConstructor commandConstructor;
        private readonly PdfTextPainter textPainter;
        private readonly PdfCompatibility compatibility;
        private readonly PdfGraphicsDocument graphicsDocument;
        private readonly IList<DXBrush> disposableBrushes;
        private PdfTransformationMatrix coordinateTransformationMatrix;
        private PdfTransformationMatrix invertedCoordinateTransformationMatrix;
        private float dpiX;
        private float dpiY;

        public PdfGraphicsCommandConstructor(PdfForm form, PdfGraphicsDocument graphicsDocument, float dpiX, float dpiY) : this(form.BBox, form.Resources, graphicsDocument, dpiX, dpiY)
        {
        }

        public PdfGraphicsCommandConstructor(PdfPage page, PdfGraphicsDocument graphicsDocument, float dpiX, float dpiY) : this(page.CropBox, page.Resources, graphicsDocument, dpiX, dpiY)
        {
        }

        private PdfGraphicsCommandConstructor(PdfRectangle bBox, PdfResources resources, PdfGraphicsDocument graphicsDocument, float dpiX, float dpiY)
        {
            this.graphicsStateStack = new PdfGraphicsStateStack();
            this.disposableBrushes = new List<DXBrush>();
            this.resources = resources;
            this.bBox = bBox;
            this.SetDpi(dpiX, dpiY);
            this.graphicsDocument = graphicsDocument;
            this.commandConstructor = new PdfCommandConstructor(resources);
            this.textPainter = new PdfTextPainter(this.commandConstructor);
            this.compatibility = graphicsDocument.DocumentCatalog.CreationOptions.Compatibility;
        }

        public void Clear()
        {
            this.EnsureBrush();
            this.commandConstructor.FillRectangle(this.bBox);
        }

        private bool CompareComponents(double[] first, double[] second)
        {
            if (first.Length != second.Length)
            {
                return false;
            }
            for (int i = 0; i < first.Length; i++)
            {
                if (first[i] != second[i])
                {
                    return false;
                }
            }
            return true;
        }

        public PdfFormSignatureAppearance CreateSignatureAppearance(RectangleF rectangle, int pageIndex)
        {
            PdfRectangle signatureBounds = this.TransformRectangle(rectangle);
            return new PdfFormSignatureAppearance(new PdfForm(this.DocumentCatalog, new PdfRectangle(0.0, 0.0, signatureBounds.Width, signatureBounds.Height)), signatureBounds, pageIndex);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                this.commandConstructor.Dispose();
                foreach (DXBrush brush in this.disposableBrushes)
                {
                    brush.Dispose();
                }
                this.disposableBrushes.Clear();
            }
        }

        public void DrawBezier(PointF start, PointF controlPoint1, PointF controlPoint2, PointF finish)
        {
            this.EnsureLineWidth(false);
            this.commandConstructor.DrawBezier(this.TransformPoint(start), this.TransformPoint(controlPoint1), this.TransformPoint(controlPoint2), this.TransformPoint(finish));
        }

        public void DrawBeziers(PointF[] points)
        {
            this.EnsureLineWidth(false);
            this.commandConstructor.DrawBeziers(this.TransformPoints(points));
        }

        public PdfDeferredForm DrawDeferredForm(RectangleF rectangle)
        {
            PdfRectangle rectangle2 = this.TransformRectangle(rectangle);
            PdfDeferredForm form = new PdfDeferredForm(this.DocumentCatalog, new PdfRectangle(0.0, 0.0, rectangle2.Width, rectangle2.Height));
            this.commandConstructor.DrawForm(this.DocumentCatalog.Objects.AddResolvedObject(form), new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, rectangle2.Left, rectangle2.Bottom));
            return form;
        }

        public void DrawEllipse(RectangleF rect)
        {
            this.EnsureLineWidth(true);
            PdfRectangle ellipseBBox = this.TransformRectangle(rect);
            if (!this.graphicsStateStack.Current.IsInsetPen)
            {
                this.commandConstructor.DrawEllipse(this.TransformRectangle(rect));
            }
            else
            {
                this.commandConstructor.SaveGraphicsState();
                this.commandConstructor.IntersectClipByEllipse(ellipseBBox);
                this.commandConstructor.DrawEllipse(ellipseBBox);
                this.commandConstructor.RestoreGraphicsState();
            }
        }

        public void DrawFormattedLines(string[] lines, PdfExportFontInfo fontInfo, RectangleF layout, PdfStringFormat format, RectangleF clipRectangle)
        {
            if ((lines != null) && (lines.Length != 0))
            {
                DXSizeF? nullable1;
                this.EnsureBrush();
                float trasformedTabStopInterval = this.GetTrasformedTabStopInterval(format, (double) fontInfo.FontSize);
                PdfExportFont font = fontInfo.Font;
                PdfRectangle layoutRect = this.TransformRectangle(layout);
                if (!layout.IsEmpty)
                {
                    nullable1 = new DXSizeF((float) layoutRect.Width, (float) layoutRect.Height);
                }
                else
                {
                    nullable1 = null;
                }
                DXSizeF? nullable = nullable1;
                DXKerningMode mode = ((lines.Length <= 1) || !PdfEnvironmentHelper.ShouldUseKerning) ? DXKerningMode.None : DXKerningMode.Always;
                IList<IList<DXCluster>> list = new List<IList<DXCluster>>(lines.Length);
                PdfTabbedStringFormatter formatter = new PdfTabbedStringFormatter(fontInfo, trasformedTabStopInterval, format.DirectionRightToLeft, lines.Length > 1);
                foreach (string str in lines)
                {
                    list.Add(formatter.FormatString(str));
                }
                PdfStringClipper stringClipper = clipRectangle.IsEmpty ? new PdfStringClipper() : new PdfRectangleStringClipper(this.TransformRectangle(clipRectangle));
                this.textPainter.DrawLines(list, fontInfo, layoutRect, format, stringClipper);
            }
        }

        public void DrawLine(float x1, float y1, float x2, float y2)
        {
            PointF[] points = new PointF[] { new PointF(x1, y1), new PointF(x2, y2) };
            this.DrawLines(points);
        }

        public void DrawLines(PointF[] points)
        {
            if (ShouldDrawPath(points))
            {
                this.EnsureLineWidth(false);
                PdfPoint[] pointArray = this.TransformPoints(points);
                PdfGraphicsExportState current = this.graphicsStateStack.Current;
                IPdfLineCapPainter startCapPainter = current.StartCapPainter;
                if (startCapPainter != null)
                {
                    PdfPoint point = pointArray[0];
                    PdfPoint vector = pointArray[1];
                    pointArray[0] = startCapPainter.TranslatePoint(point, vector);
                    startCapPainter.DrawLineCap(this.commandConstructor, point, vector);
                }
                IPdfLineCapPainter endCapPainter = current.EndCapPainter;
                if (endCapPainter != null)
                {
                    int index = pointArray.Length - 1;
                    PdfPoint point = pointArray[index];
                    PdfPoint vector = pointArray[index - 1];
                    pointArray[index] = endCapPainter.TranslatePoint(point, vector);
                    endCapPainter.DrawLineCap(this.commandConstructor, point, vector);
                }
                this.commandConstructor.DrawLines(pointArray);
            }
        }

        public void DrawPath(PointF[] pathPoints, byte[] pathTypes)
        {
            if (ShouldDrawPath(pathPoints))
            {
                this.EnsureLineWidth(false);
                this.commandConstructor.DrawPaths(this.TransformPath(pathPoints, pathTypes));
            }
        }

        public void DrawPolygon(PointF[] points)
        {
            int length = points.Length;
            if (length > 0)
            {
                this.EnsureLineWidth(true);
                PdfPoint[] pointArray = this.TransformPoints(points);
                if (this.graphicsStateStack.Current.IsInsetPen)
                {
                    this.commandConstructor.SaveGraphicsState();
                    PdfGraphicsPath path = new PdfGraphicsPath(pointArray[0]);
                    for (int i = 1; i < length; i++)
                    {
                        path.AppendLineSegment(pointArray[i]);
                    }
                    this.commandConstructor.IntersectClip(path);
                    this.commandConstructor.DrawPolygon(pointArray);
                    this.commandConstructor.RestoreGraphicsState();
                }
                else
                {
                    this.commandConstructor.DrawPolygon(pointArray);
                }
            }
        }

        public void DrawPrecalculatedString(IPdfGlyphPlacementInfo placementInfo, PdfExportFontInfo fontInfo, PointF location, PdfGraphicsTextOrigin textOrigin, DXKerningMode kerningMode)
        {
            if (fontInfo.Font.UseTwoByteCodePoints)
            {
                this.EnsureBrush();
                IList<IList<DXCluster>> clusters = placementInfo.GetClusters();
                if (clusters != null)
                {
                    PdfPoint point = this.TransformPoint(location);
                    if (textOrigin == PdfGraphicsTextOrigin.Baseline)
                    {
                        point = new PdfPoint(point.X, point.Y + fontInfo.Font.Metrics.GetAscent((double) fontInfo.FontSize));
                    }
                    this.textPainter.DrawLines(clusters, fontInfo, point, PdfStringFormat.GenericTypographic, false);
                    return;
                }
            }
            this.DrawString(placementInfo.Text, fontInfo, location, textOrigin, kerningMode);
        }

        public void DrawRectangle(RectangleF rect)
        {
            this.EnsureLineWidth(true);
            PdfRectangle rectangle = this.TransformRectangle(rect);
            if (!this.graphicsStateStack.Current.IsInsetPen)
            {
                this.commandConstructor.DrawRectangle(rectangle);
            }
            else
            {
                this.commandConstructor.SaveGraphicsState();
                this.commandConstructor.IntersectClip(rectangle);
                this.commandConstructor.DrawRectangle(rectangle);
                this.commandConstructor.RestoreGraphicsState();
            }
        }

        public void DrawString(string text, PdfExportFontInfo fontInfo, PointF location, PdfGraphicsTextOrigin textOrigin, DXKerningMode kerningMode)
        {
            this.DrawString(text, fontInfo, location, PdfStringFormat.GenericDefault, textOrigin, kerningMode);
        }

        public void DrawString(string text, PdfExportFontInfo fontInfo, RectangleF layout, PdfStringFormat format, DXKerningMode kerningMode)
        {
            this.EnsureBrush();
            PdfExportFont shaper = fontInfo.Font;
            float trasformedTabStopInterval = this.GetTrasformedTabStopInterval(format, (double) fontInfo.FontSize);
            if (layout.IsEmpty)
            {
                DXSizeF? layoutSize = null;
                IList<IList<DXCluster>> lines = DXLineFormatter.FormatText(text, layoutSize, shaper.Metrics, fontInfo.FontSize, format, trasformedTabStopInterval, shaper, kerningMode);
                this.textPainter.DrawLines(lines, fontInfo, this.TransformPoint(layout.Location), format, true);
            }
            else
            {
                PdfRectangle layoutRect = this.TransformRectangle(layout);
                DXSizeF ef = new DXSizeF((float) layoutRect.Width, (float) layoutRect.Height);
                IList<IList<DXCluster>> lines = DXLineFormatter.FormatText(text, new DXSizeF?(ef), shaper.Metrics, fontInfo.FontSize, format, trasformedTabStopInterval, shaper, kerningMode);
                this.textPainter.DrawLines(lines, fontInfo, layoutRect, format);
            }
        }

        public void DrawString(string text, PdfExportFontInfo fontInfo, PointF location, PdfStringFormat format, PdfGraphicsTextOrigin textOrigin, DXKerningMode kerningMode)
        {
            this.EnsureBrush();
            PdfPoint point = this.TransformPoint(location);
            PdfExportFont shaper = fontInfo.Font;
            if (textOrigin == PdfGraphicsTextOrigin.Baseline)
            {
                format = new PdfStringFormat(format);
                format.LeadingMarginFactor = 0.0;
                format.TrailingMarginFactor = 0.0;
                point = new PdfPoint(point.X, point.Y + shaper.Metrics.GetAscent((double) fontInfo.FontSize));
            }
            float trasformedTabStopInterval = this.GetTrasformedTabStopInterval(format, (double) fontInfo.FontSize);
            DXSizeF? layoutSize = null;
            IList<IList<DXCluster>> lines = DXLineFormatter.FormatText(text, layoutSize, shaper.Metrics, fontInfo.FontSize, format, trasformedTabStopInterval, shaper, kerningMode);
            this.textPainter.DrawLines(lines, fontInfo, point, format, true);
        }

        public void DrawXObject(PdfXObjectCachedResource cachedResource, PointF location)
        {
            cachedResource.Draw(this.commandConstructor, this.TransformRectangle(new RectangleF(location.X, location.Y, cachedResource.Width, cachedResource.Height)));
        }

        public void DrawXObject(PdfXObjectCachedResource cachedResource, RectangleF bounds)
        {
            cachedResource.Draw(this.commandConstructor, this.TransformRectangle(bounds));
        }

        private void EnsureBrush()
        {
            PdfBrushContainer currentBrush = this.graphicsStateStack.Current.CurrentBrush;
            if (currentBrush != null)
            {
                this.SetColorForNonStrokingOperations(currentBrush.GetColor(this));
            }
        }

        private void EnsureLineWidth(bool isClosedShape)
        {
            PdfGraphicsExportState current = this.graphicsStateStack.Current;
            double actualLineWidth = current.ActualLineWidth;
            this.SetLineWidth((current.IsInsetPen & isClosedShape) ? (actualLineWidth * 2.0) : actualLineWidth);
        }

        public void FillEllipse(RectangleF rect)
        {
            PdfRectangle transformedBBox = this.TransformRectangle(rect);
            if (!this.FillShape(new PdfEllipseFillingStrategy(rect, transformedBBox)))
            {
                this.commandConstructor.FillEllipse(transformedBBox);
            }
        }

        public void FillPath(PointF[] pathPoints, byte[] pathTypes, bool nonZero)
        {
            IList<PdfGraphicsPath> transformedPaths = this.TransformPath(pathPoints, pathTypes);
            if (!this.FillShape(new PdfPathFillingStrategy(pathPoints, transformedPaths, nonZero)))
            {
                this.commandConstructor.FillPath(transformedPaths, nonZero);
            }
        }

        public void FillPolygon(PointF[] points, bool nonZero)
        {
            PdfPoint[] transformedPoints = this.TransformPoints(points);
            if (!this.FillShape(new PdfPolygonFillingStrategy(points, transformedPoints, nonZero)))
            {
                this.commandConstructor.FillPolygon(transformedPoints, nonZero);
            }
        }

        public void FillRectangle(RectangleF rect)
        {
            PdfRectangle transformedRectangle = this.TransformRectangle(rect);
            if (!this.FillShape(new PdfRectangleFillingStrategy(rect, transformedRectangle)))
            {
                this.commandConstructor.FillRectangle(transformedRectangle);
            }
        }

        private bool FillShape(PdfShapeFillingStrategy strategy)
        {
            PdfBrushContainer currentBrush = this.graphicsStateStack.Current.CurrentBrush;
            if (currentBrush != null)
            {
                if (currentBrush.FillShape(this, strategy))
                {
                    return true;
                }
                this.SetColorForNonStrokingOperations(currentBrush.GetColor(this));
            }
            return false;
        }

        private float GetTrasformedTabStopInterval(PdfStringFormat format, double fontSize) => 
            (float) this.TransformX(format.TabStopInterval);

        public void IntersectClip(RectangleF rect)
        {
            this.commandConstructor.IntersectClip(this.TransformRectangle(rect));
        }

        public void IntersectClip(PointF[] points, byte[] types, bool isWindingFillMode)
        {
            this.commandConstructor.IntersectClip(this.TransformPath(points, types), isWindingFillMode);
        }

        public void IntersectClipWithoutWorldTransform(PointF[] points)
        {
            int length = points.Length;
            if (length > 2)
            {
                PdfTransformationMatrix invertedTransformationMatrix = this.InvertedTransformationMatrix;
                PointF tf = points[0];
                PdfGraphicsPath path = new PdfGraphicsPath(invertedTransformationMatrix.Transform((double) tf.X, (double) tf.Y));
                int index = 1;
                while (true)
                {
                    if (index >= length)
                    {
                        path.Closed = true;
                        this.commandConstructor.IntersectClip(path);
                        break;
                    }
                    tf = points[index];
                    path.AppendLineSegment(invertedTransformationMatrix.Transform((double) tf.X, (double) tf.Y));
                    index++;
                }
            }
        }

        public void IntersectClipWithoutWorldTransform(RectangleF rect)
        {
            PdfTransformationMatrix invertedTransformationMatrix = this.InvertedTransformationMatrix;
            if (((invertedTransformationMatrix.A == 0.0) && (invertedTransformationMatrix.D == 0.0)) || ((invertedTransformationMatrix.B == 0.0) && (invertedTransformationMatrix.C == 0.0)))
            {
                this.commandConstructor.IntersectClip(new PdfRectangle(invertedTransformationMatrix.Transform((double) rect.Left, (double) rect.Top), invertedTransformationMatrix.Transform((double) rect.Right, (double) rect.Bottom)));
            }
            else
            {
                double left = rect.Left;
                double bottom = rect.Bottom;
                double top = rect.Top;
                double right = rect.Right;
                PdfPoint[] points = new PdfPoint[] { new PdfPoint(left, top), new PdfPoint(left, bottom), new PdfPoint(right, bottom), new PdfPoint(right, top) };
                PdfPoint[] pointArray = invertedTransformationMatrix.Transform(points);
                PdfGraphicsPath path = new PdfGraphicsPath(pointArray[0]);
                for (int i = 1; i < 4; i++)
                {
                    path.AppendLineSegment(pointArray[i]);
                }
                path.Closed = true;
                this.commandConstructor.IntersectClip(path);
            }
        }

        public void IntersectClipWithoutWorldTransform(PointF[] points, byte[] types, bool isWindingFillMode)
        {
            PdfTransformationMatrix transformationMatrix = this.InvertedTransformationMatrix;
            this.commandConstructor.IntersectClip(this.TransformPath(points, types, point => transformationMatrix.Transform((double) point.X, (double) point.Y)), isWindingFillMode);
        }

        private void RecalculateMatrix()
        {
            this.coordinateTransformationMatrix = new PdfTransformationMatrix((double) (72f / this.dpiX), 0.0, 0.0, (double) (-72f / this.dpiY), this.bBox.Left, this.bBox.Top);
            this.invertedCoordinateTransformationMatrix = PdfTransformationMatrix.Invert(this.coordinateTransformationMatrix);
        }

        public void RestoreGraphicsState()
        {
            this.graphicsStateStack.Pop();
            this.commandConstructor.RestoreGraphicsState();
        }

        public void RotateTransform(float degree)
        {
            this.UpdateTransformationMatrix(PdfTransformationMatrix.CreateRotate((double) degree));
        }

        public void SaveGraphicsState()
        {
            this.graphicsStateStack.Push();
            this.commandConstructor.SaveGraphicsState();
        }

        public void ScaleTransform(double sx, double sy)
        {
            this.UpdateTransformationMatrix(PdfTransformationMatrix.CreateScale(sx, sy));
        }

        public void SetBrush(DXBrush brush)
        {
            brush.Accept(new PdfBrushExportVisitor(this));
        }

        public void SetBrush(PdfBrushContainer container)
        {
            this.graphicsStateStack.Current.CurrentBrush = container;
        }

        public void SetBrush(Brush brush)
        {
            DXBrush item = PdfGDIPlusGraphicsConverter.ConvertBrush(brush);
            this.disposableBrushes.Add(item);
            this.SetBrush(item);
        }

        public void SetColorForNonStrokingOperations(PdfTransparentColor color)
        {
            PdfGraphicsExportState current = this.graphicsStateStack.Current;
            current.CurrentBrush = null;
            if (color.Pattern != null)
            {
                current.NonStrokingColorComponents = null;
                this.commandConstructor.SetColorForNonStrokingOperations(color);
            }
            else
            {
                double[] components = color.Components;
                if ((current.NonStrokingColorComponents == null) || !this.CompareComponents(current.NonStrokingColorComponents, components))
                {
                    current.NonStrokingColorComponents = components;
                    this.commandConstructor.SetColorForNonStrokingOperations(color);
                }
            }
            this.SetNonStrokingAlpha(color.Alpha);
        }

        public void SetColorForStrokingOperations(PdfTransparentColor color)
        {
            PdfGraphicsExportState current = this.graphicsStateStack.Current;
            if ((color.Alpha != current.StrokingAlpha) && (this.compatibility != PdfCompatibility.PdfA1b))
            {
                this.SetGraphicsStateParameters(this.graphicsDocument.StrokingAlphaCache.GetParameters(color.Alpha));
                current.StrokingAlpha = color.Alpha;
            }
            if (color.Pattern != null)
            {
                this.commandConstructor.SetColorForStrokingOperations(color);
            }
            else
            {
                double[] components = color.Components;
                if ((current.StrokingColorComponents == null) || !this.CompareComponents(current.StrokingColorComponents, components))
                {
                    current.StrokingColorComponents = components;
                    this.commandConstructor.SetColorForStrokingOperations(color);
                }
            }
        }

        public void SetDpi(float dpiX, float dpiY)
        {
            this.dpiX = dpiX;
            this.dpiY = dpiY;
            this.RecalculateMatrix();
        }

        public void SetFillColor(ARGBColor color)
        {
            this.SetColorForNonStrokingOperations(new PdfTransparentColor(color.A, PdfGraphicsConverter.ConvertColorToColorComponents(color)));
        }

        private void SetGraphicsStateParameters(PdfGraphicsStateParameters parameters)
        {
            this.commandConstructor.SetGraphicsStateParameters(parameters);
        }

        private void SetLineWidth(double lineWidth)
        {
            PdfGraphicsExportState current = this.graphicsStateStack.Current;
            if (lineWidth != current.LineWidth)
            {
                this.commandConstructor.SetLineWidth(lineWidth);
                current.LineWidth = lineWidth;
            }
        }

        public void SetMiterLimit(double miterLimit)
        {
            if (this.graphicsStateStack.Current.MiterLimit != miterLimit)
            {
                this.commandConstructor.SetMiterLimit(miterLimit);
                this.graphicsStateStack.Current.MiterLimit = miterLimit;
            }
        }

        public void SetNonStrokingAlpha(double alpha)
        {
            PdfGraphicsExportState current = this.graphicsStateStack.Current;
            if ((alpha != current.NonStrokingAlpha) && (this.compatibility != PdfCompatibility.PdfA1b))
            {
                this.SetGraphicsStateParameters(this.graphicsDocument.NonStrokingAlphaCache.GetParameters(alpha));
                current.NonStrokingAlpha = alpha;
            }
        }

        public void SetPen(DXPen pen)
        {
            this.SetPen(pen, this.TransformX(pen.Width));
        }

        public void SetPen(Pen pen)
        {
            using (DXPen pen2 = PdfGDIPlusGraphicsConverter.ConvertPen(pen))
            {
                this.SetPen(pen2);
            }
        }

        private void SetPen(DXPen pen, double lineWidth)
        {
            DXLineJoin? nullable3;
            PdfGraphicsExportState current = this.graphicsStateStack.Current;
            current.ActualLineWidth = lineWidth;
            bool flag = pen.Alignment == DXPenAlignment.Inset;
            if (flag)
            {
                lineWidth *= 2.0;
            }
            current.IsInsetPen = flag;
            this.SetLineWidth(lineWidth);
            IPdfLineCapPainter painter = PdfLineCapPainterFactory.Create(pen.StartCap, pen.CustomStartCap, lineWidth);
            IPdfLineCapPainter painter2 = PdfLineCapPainterFactory.Create(pen.EndCap, pen.CustomEndCap, lineWidth);
            current.StartCapPainter = painter;
            current.EndCapPainter = painter2;
            pen.Brush.Accept(new PdfPenExportVisitor(this, (painter != null) || (painter2 != null)));
            if (pen.DashStyle == DXDashStyle.Solid)
            {
                DXDashStyle? dashStyle = current.DashStyle;
                DXDashStyle solid = DXDashStyle.Solid;
                if ((((DXDashStyle) dashStyle.GetValueOrDefault()) == solid) ? (dashStyle == null) : true)
                {
                    this.commandConstructor.SetLineStyle(PdfLineStyle.CreateSolid());
                }
            }
            else
            {
                float[] dashPattern = pen.DashPattern;
                if (dashPattern != null)
                {
                    int length = dashPattern.Length;
                    double[] numArray2 = new double[length];
                    if (lineWidth == 0.0)
                    {
                        for (int i = 0; i < length; i++)
                        {
                            numArray2[i] = this.TransformX((double) dashPattern[i]);
                        }
                    }
                    else
                    {
                        for (int i = 0; i < length; i++)
                        {
                            float num4 = dashPattern[i];
                            if (pen.DashCap == DXDashCap.Round)
                            {
                                num4 = ((i % 2) == 0) ? Math.Max((float) (num4 - 1f), (float) 0f) : (num4 + 1f);
                            }
                            numArray2[i] = num4 * lineWidth;
                        }
                    }
                    this.commandConstructor.SetLineStyle(PdfLineStyle.CreateDashed(numArray2, pen.DashOffset * lineWidth));
                }
            }
            current.DashStyle = new DXDashStyle?(pen.DashStyle);
            DXDashCap dashCap = pen.DashCap;
            DXDashCap? nullable2 = current.DashCap;
            if ((dashCap == ((DXDashCap) nullable2.GetValueOrDefault())) ? (nullable2 == null) : true)
            {
                DXLineCap square;
                PdfLineCapStyle projectingSquare;
                if (dashCap == DXDashCap.Square)
                {
                    square = DXLineCap.Square;
                    projectingSquare = PdfLineCapStyle.ProjectingSquare;
                }
                else if (dashCap != DXDashCap.Round)
                {
                    square = DXLineCap.Flat;
                    projectingSquare = PdfLineCapStyle.Butt;
                }
                else
                {
                    square = DXLineCap.Round;
                    projectingSquare = PdfLineCapStyle.Round;
                }
                this.commandConstructor.SetLineCapStyle(projectingSquare);
                current.LineCap = new DXLineCap?(square);
            }
            current.DashCap = new DXDashCap?(pen.DashCap);
            DXLineJoin lineJoin = pen.LineJoin;
            if (lineJoin == DXLineJoin.Bevel)
            {
                nullable3 = current.LineJoin;
                lineJoin = DXLineJoin.Bevel;
                if ((((DXLineJoin) nullable3.GetValueOrDefault()) == lineJoin) ? (nullable3 == null) : true)
                {
                    this.commandConstructor.SetLineJoinStyle(PdfLineJoinStyle.Bevel);
                }
            }
            else if (lineJoin == DXLineJoin.Round)
            {
                nullable3 = current.LineJoin;
                lineJoin = DXLineJoin.Round;
                if ((((DXLineJoin) nullable3.GetValueOrDefault()) == lineJoin) ? (nullable3 == null) : true)
                {
                    this.commandConstructor.SetLineJoinStyle(PdfLineJoinStyle.Round);
                }
            }
            else
            {
                nullable3 = current.LineJoin;
                lineJoin = DXLineJoin.Miter;
                if ((((DXLineJoin) nullable3.GetValueOrDefault()) == lineJoin) ? (nullable3 == null) : true)
                {
                    nullable3 = current.LineJoin;
                    lineJoin = DXLineJoin.MiterClipped;
                    if ((((DXLineJoin) nullable3.GetValueOrDefault()) == lineJoin) ? (nullable3 == null) : true)
                    {
                        this.commandConstructor.SetLineJoinStyle(PdfLineJoinStyle.Miter);
                    }
                }
                this.SetMiterLimit(pen.MiterLimit);
            }
            current.LineJoin = new DXLineJoin?(pen.LineJoin);
        }

        public void SetUnscaledPen(DXPen pen)
        {
            this.SetPen(pen, pen.Width);
        }

        public void SetUnscaledPen(ARGBColor color, double width)
        {
            using (DXPen pen = new DXPen(color))
            {
                this.SetPen(pen, width);
            }
        }

        public void SetUnscaledPen(Color color, double width)
        {
            this.SetUnscaledPen(PdfGDIPlusGraphicsConverter.ConvertColor(color), width);
        }

        private static bool ShouldDrawPath(PointF[] points)
        {
            int length = points.Length;
            if (length > 1)
            {
                PointF tf = points[0];
                for (int i = 1; i < length; i++)
                {
                    if (points[i] != tf)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal PdfTransformationMatrix TransformMatrix(PdfTransformationMatrix matrix) => 
            PdfTransformationMatrix.Multiply(this.invertedCoordinateTransformationMatrix, PdfTransformationMatrix.Multiply(matrix, this.coordinateTransformationMatrix));

        private IList<PdfGraphicsPath> TransformPath(PointF[] pathPoints, byte[] pathTypes) => 
            this.TransformPath(pathPoints, pathTypes, new Func<PointF, PdfPoint>(this.TransformPoint));

        private IList<PdfGraphicsPath> TransformPath(PointF[] pathPoints, byte[] pathTypes, Func<PointF, PdfPoint> transformPoint)
        {
            List<PdfGraphicsPath> list = new List<PdfGraphicsPath>();
            PdfGraphicsPath item = null;
            for (int i = 0; i < pathPoints.Length; i++)
            {
                PdfPoint startPoint = transformPoint(pathPoints[i]);
                int num2 = pathTypes[i] & 7;
                switch (num2)
                {
                    case 0:
                        item = new PdfGraphicsPath(startPoint);
                        list.Add(item);
                        break;

                    case 1:
                        if (item == null)
                        {
                            return list;
                        }
                        item.AppendLineSegment(startPoint);
                        break;

                    case 3:
                        if (item == null)
                        {
                            return list;
                        }
                        item.AppendBezierSegment(startPoint, transformPoint(pathPoints[++i]), transformPoint(pathPoints[++i]));
                        break;

                    default:
                        break;
                }
                if (((pathTypes[i] & 0x80) != 0) && (item != null))
                {
                    item.Closed = true;
                }
            }
            return list;
        }

        private PdfPoint TransformPoint(PointF point) => 
            this.coordinateTransformationMatrix.Transform((double) point.X, (double) point.Y);

        private PdfPoint[] TransformPoints(PointF[] points)
        {
            int length = points.Length;
            PdfPoint[] pointArray = new PdfPoint[length];
            for (int i = 0; i < length; i++)
            {
                pointArray[i] = this.TransformPoint(points[i]);
            }
            return pointArray;
        }

        public PdfRectangle TransformRectangle(RectangleF rect)
        {
            PdfPoint point = this.TransformPoint(new PointF(Math.Min(rect.Left, rect.Right), Math.Max(rect.Top, rect.Bottom)));
            PdfPoint point2 = this.TransformPoint(new PointF(Math.Max(rect.Left, rect.Right), Math.Min(rect.Top, rect.Bottom)));
            return new PdfRectangle(point.X, point.Y, point2.X, point2.Y);
        }

        internal double TransformX(double value) => 
            this.coordinateTransformationMatrix.Transform(value, 0.0).X;

        internal double TransformY(double value) => 
            this.coordinateTransformationMatrix.Transform(0.0, value).Y;

        public void TranslateTransform(double x, double y)
        {
            this.UpdateTransformationMatrix(new PdfTransformationMatrix(1.0, 0.0, 0.0, 1.0, x, y));
        }

        public void UpdateTransformationMatrix(PdfTransformationMatrix matrix)
        {
            this.commandConstructor.ModifyTransformationMatrix(PdfTransformationMatrix.Multiply(PdfTransformationMatrix.Multiply(this.invertedCoordinateTransformationMatrix, matrix), this.coordinateTransformationMatrix));
        }

        public PdfTransformationMatrix CoordinateTransformationMatrix =>
            this.coordinateTransformationMatrix;

        public PdfTransformationMatrix TransformationMatrix =>
            PdfTransformationMatrix.Multiply(this.coordinateTransformationMatrix, this.commandConstructor.CurrentTransformationMatrix);

        public PdfXObjectResourceCache ImageCache =>
            this.graphicsDocument.ImageCache;

        public PdfResources Resources =>
            this.resources;

        public PdfRectangle BBox =>
            this.bBox;

        public float DpiX =>
            this.dpiX;

        public float DpiY =>
            this.dpiY;

        public byte[] Commands =>
            this.commandConstructor.Commands;

        public PdfShadingCache ShadingCache =>
            this.graphicsDocument.ShadingCache;

        public PdfCommandConstructor CommandConstructor =>
            this.commandConstructor;

        public PdfDocumentCatalog DocumentCatalog =>
            this.graphicsDocument.DocumentCatalog;

        public PdfGraphicsDocument GraphicsDocument =>
            this.graphicsDocument;

        private PdfTransformationMatrix InvertedTransformationMatrix
        {
            get
            {
                PdfTransformationMatrix matrix = PdfTransformationMatrix.Invert(this.commandConstructor.CurrentTransformationMatrix);
                return PdfTransformationMatrix.Multiply(this.coordinateTransformationMatrix, matrix);
            }
        }
    }
}

