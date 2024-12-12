namespace DevExpress.XtraPrinting.Export.Pdf
{
    using DevExpress.Utils;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Runtime.InteropServices;

    public class PdfGraphicsImpl
    {
        private PdfDocument document;
        private Matrix transform;
        private Stack<Matrix> transformStack = new Stack<Matrix>();
        private PointF[] clipArray;
        private PdfGraphicsState pdfGraphicsState;
        private PdfDrawContext context;
        private SizeF size;
        private IXObjectsOwner xObjectsOwner;
        private IPdfDocumentOwner documentInfo;
        private GraphicsUnit pageUnit;

        public PdfGraphicsImpl(IPdfDocumentOwner documentInfo, PdfDrawContext context, SizeF size, PdfDocument document, IXObjectsOwner xObjectsOwner)
        {
            this.documentInfo = documentInfo;
            this.document = document;
            this.context = context;
            this.size = size;
            this.xObjectsOwner = xObjectsOwner;
            this.transform = new Matrix();
            this.pdfGraphicsState = PdfGraphicsState.No;
        }

        private void AddPendingTransform()
        {
            this.CloseTransform();
            if (!this.transform.IsIdentity)
            {
                this.pdfGraphicsState = PdfGraphicsState.Pending;
            }
        }

        private void AddTransform(Matrix matrix, MatrixOrder order)
        {
            this.ResetClip();
            this.transform.Multiply(matrix, order);
            this.AddPendingTransform();
            this.ApplyTransformCacheToClipArray();
        }

        private PointF[] ApplyClip(RectangleF bounds, StringFormat format)
        {
            PointF[] clipArray = this.clipArray;
            if ((format.FormatFlags & StringFormatFlags.NoClip) == 0)
            {
                RectangleF a = PointArray2Rectangle(this.clipArray);
                if (!a.IsEmpty)
                {
                    this.ClipBounds = RectangleF.Intersect(a, bounds);
                }
            }
            return clipArray;
        }

        private void ApplyPageUnitToClipArray(GraphicsUnit newPageUnit)
        {
            if (this.clipArray != null)
            {
                float num = GraphicsDpi.UnitToDpiI(newPageUnit) / GraphicsDpi.UnitToDpiI(this.pageUnit);
                using (Matrix matrix = this.transform.Clone())
                {
                    using (Matrix matrix2 = new Matrix(num, 0f, 0f, num, 0f, 0f))
                    {
                        matrix2.Multiply(matrix, MatrixOrder.Prepend);
                        matrix.Invert();
                        matrix2.Multiply(matrix, MatrixOrder.Append);
                        matrix2.TransformPoints(this.clipArray);
                    }
                }
            }
        }

        private void ApplyPattern(Color foreColor, Color backColor)
        {
            PdfPattern pattern = this.document.Patterns.CreateAddUnique(foreColor, backColor);
            this.xObjectsOwner.AddPattern(pattern);
            this.Context.Pattern(pattern);
        }

        private void ApplyPendingTransform()
        {
            if (this.pdfGraphicsState == PdfGraphicsState.Pending)
            {
                this.Context.GSave();
                this.ApplyTransform();
                this.pdfGraphicsState = PdfGraphicsState.Opened;
            }
        }

        private void ApplyShading(Color startColor, Color endColor)
        {
            PdfShading shading = this.document.Shading.CreateAddUnique(startColor, endColor);
            this.xObjectsOwner.AddShading(shading);
            this.Context.Shading(shading);
        }

        private void ApplyTransform()
        {
            if (!this.transform.IsIdentity)
            {
                using (Matrix matrix = this.transform.Clone())
                {
                    new TransformActivator(matrix, this.PageUnit, this.size).ApplyToDrawContext(this.context);
                }
            }
        }

        private void ApplyTransformCacheToClipArray()
        {
            if (this.clipArray != null)
            {
                using (Matrix matrix = this.transform.Clone())
                {
                    matrix.Invert();
                    matrix.TransformPoints(this.clipArray);
                }
            }
        }

        public void ApplyTransformState(MatrixOrder order, bool removeState)
        {
            Matrix matrix = removeState ? this.transformStack.Pop() : this.transformStack.Peek();
            this.AddTransform(matrix, order);
            if (removeState)
            {
                matrix.Dispose();
            }
        }

        private Color ApplyTransparency(Color color)
        {
            if (color.A == 0xff)
            {
                return color;
            }
            this.SetTransparency(color.A);
            return Color.FromArgb(0xff, color);
        }

        private PointF BackTransformPoint(PointF pt)
        {
            PointF[] pts = new PointF[] { pt };
            using (Matrix matrix = this.transform.Clone())
            {
                matrix.Invert();
                matrix.TransformPoints(pts);
                return pts[0];
            }
        }

        private SizeF BackTransformValue(SizeF value) => 
            PdfCoordinate.BackTransformValue(this.PageUnit, value);

        private static int CalculateEndLine(float yBase, float lineSpacing, PdfGraphicsRectangle clipBounds) => 
            (int) Math.Floor((double) (((yBase - clipBounds.Bottom) / lineSpacing) - 0.1));

        private static int CalculateStartLine(float yBase, float lineSpacing, PdfGraphicsRectangle clipBounds) => 
            (int) Math.Floor(Math.Max((double) 0.0, (double) (((yBase - clipBounds.Top) / lineSpacing) + 0.1)));

        private bool CanApplyImageBackgroundColor(System.Drawing.Image image) => 
            (!image.RawFormat.Equals(ImageFormat.Wmf) || this.document.ConvertImagesToJpeg) ? ((!image.RawFormat.Equals(ImageFormat.Emf) || this.document.ConvertImagesToJpeg) && (!image.RawFormat.Equals(ImageFormat.Jpeg) && ((!image.RawFormat.Equals(ImageFormat.Gif) || this.document.ConvertImagesToJpeg) && ((image.PixelFormat != PixelFormat.Format24bppRgb) && ((image.PixelFormat != PixelFormat.Format1bppIndexed) && (image.PixelFormat != PixelFormat.Format32bppArgb)))))) : false;

        private void CloseTransform()
        {
            if (this.pdfGraphicsState == PdfGraphicsState.Opened)
            {
                this.Context.GRestore();
                this.pdfGraphicsState = PdfGraphicsState.No;
            }
        }

        private void ConstructEllipse(float x, float y, float w, float h)
        {
            this.Context.MoveTo(x, y + (h / 2f));
            this.Context.CurveTo(x, (y + (h / 2f)) - (((h / 2f) * 11f) / 20f), (x + (w / 2f)) - (((w / 2f) * 11f) / 20f), y, x + (w / 2f), y);
            this.Context.CurveTo((x + (w / 2f)) + (((w / 2f) * 11f) / 20f), y, x + w, (y + (h / 2f)) - (((h / 2f) * 11f) / 20f), x + w, y + (h / 2f));
            this.Context.CurveTo(x + w, (y + (h / 2f)) + (((h / 2f) * 11f) / 20f), (x + (w / 2f)) + (((w / 2f) * 11f) / 20f), y + h, x + (w / 2f), y + h);
            this.Context.CurveTo((x + (w / 2f)) - (((w / 2f) * 11f) / 20f), y + h, x, (y + (h / 2f)) + (((h / 2f) * 11f) / 20f), x, y + (h / 2f));
        }

        private void ConstructLineParams(Pen pen)
        {
            Color color = this.ApplyTransparency(pen.Color);
            this.Context.SetRGBStrokeColor(color);
            float lineWidth = (pen.Width < 0f) ? 0f : this.TransformValue(pen.Width);
            this.Context.SetLineWidth(lineWidth);
            if (pen.DashStyle != DashStyle.Solid)
            {
                this.Context.SetDash(this.GetDashArray(pen, lineWidth), 0);
            }
            PdfLineCapStyle lineCap = (pen.DashStyle != DashStyle.Solid) ? GetLineCapStyle(pen.DashCap) : GetLineCapStyle(pen.StartCap);
            if (lineCap != PdfLineCapStyle.Butt)
            {
                this.Context.SetLineCap(lineCap);
            }
            PdfLineJoinStyle lineJoinStyle = GetLineJoinStyle(pen.LineJoin);
            if (lineJoinStyle != PdfLineJoinStyle.Miter)
            {
                this.Context.SetLineJoin(lineJoinStyle);
            }
        }

        private PointF CorrectPoint(PointF pt) => 
            PdfCoordinate.CorrectPoint(this.PageUnit, pt, this.PageSizeInPoints);

        private RectangleF CorrectRectangle(RectangleF rect) => 
            PdfCoordinate.CorrectRectangle(this.PageUnit, rect, this.PageSizeInPoints);

        private static SizeF[] CreateDeltas(PointF[] pointArray, out float width, out float height)
        {
            width = 0f;
            height = 0f;
            SizeF[] efArray = new SizeF[3];
            for (int i = 1; i < 4; i++)
            {
                float num2 = pointArray[i].X - pointArray[0].X;
                float num3 = pointArray[i].Y - pointArray[0].Y;
                if ((num2 != 0f) && (width == 0f))
                {
                    width = num2;
                }
                if ((num3 != 0f) && (height == 0f))
                {
                    height = num3;
                }
                efArray[i - 1] = new SizeF(num2, num3);
            }
            return efArray;
        }

        private PdfGraphicsRectangle CreatePdfGraphicsRectangle(RectangleF rect) => 
            new PdfGraphicsRectangle(new RectangleF(this.TransformValue(rect.Location), this.TransformValue(rect.Size)), this.PageSizeInPoints);

        private static unsafe RectangleF CreateRectangle(PointF basePoint, float width, float height)
        {
            PointF location = basePoint;
            if (width < 0f)
            {
                PointF* tfPtr1 = &location;
                tfPtr1.X += width;
            }
            if (height < 0f)
            {
                PointF* tfPtr2 = &location;
                tfPtr2.Y += height;
            }
            return new RectangleF(location, new SizeF(Math.Abs(width), Math.Abs(height)));
        }

        private RectangleF CutBoundsByPage(RectangleF bounds)
        {
            PointF tf = this.BackTransformPoint(this.BackTransformValue(this.PageSizeInPoints).ToPointF());
            if (bounds.Right > tf.X)
            {
                bounds.Width = Math.Abs((float) (tf.X - bounds.X));
            }
            if (bounds.Bottom > tf.Y)
            {
                bounds.Height = Math.Abs((float) (tf.Y - bounds.Y));
            }
            return new RectangleF(bounds.Location, bounds.Size);
        }

        private void DoClip()
        {
            if (this.clipArray != null)
            {
                RectangleF clipRect = PointArray2Rectangle(this.clipArray);
                if (clipRect.IsEmpty)
                {
                    this.DoPolygonClip();
                }
                else
                {
                    this.DoRectangleClip(clipRect);
                }
                this.Context.Clip();
                this.Context.NewPath();
            }
        }

        private void DoPolygonClip()
        {
            PointF[] array = new PointF[this.clipArray.Length];
            this.clipArray.CopyTo(array, 0);
            for (int i = 0; i < array.Length; i++)
            {
                array[i] = this.CorrectPoint(array[i]);
            }
            if (array.Length != 0)
            {
                this.Context.MoveTo(array[0].X, array[0].Y);
                int index = 1;
                while (true)
                {
                    if (index >= array.Length)
                    {
                        this.Context.LineTo(array[0].X, array[0].Y);
                        break;
                    }
                    this.Context.LineTo(array[index].X, array[index].Y);
                    index++;
                }
            }
        }

        private void DoRectangleClip(RectangleF clipRect)
        {
            PdfGraphicsRectangle rectangle = this.CreatePdfGraphicsRectangle(clipRect);
            this.Context.Rectangle(rectangle.Left, rectangle.Bottom, rectangle.Width, rectangle.Height);
        }

        public void DrawBeziers(Pen pen, PointF[] points)
        {
            if (pen.Color.A != 0)
            {
                this.ApplyPendingTransform();
                this.Context.GSave();
                this.DoClip();
                this.ConstructLineParams(this.TestPen(pen));
                PointF tf = this.CorrectPoint(points[0]);
                this.Context.MoveTo(tf.X, tf.Y);
                int length = points.Length;
                for (int i = 1; i < length; i += 3)
                {
                    tf = this.CorrectPoint(points[i]);
                    PointF tf2 = this.CorrectPoint(points[i + 1]);
                    PointF tf3 = this.CorrectPoint(points[i + 2]);
                    this.Context.CurveTo(tf.X, tf.Y, tf2.X, tf2.Y, tf3.X, tf3.Y);
                }
                this.Context.Stroke();
                this.Context.GRestore();
            }
        }

        private void DrawCap(ref PointF start, PointF end, float width, LineCap cap, CustomLineCap customCap, Color color)
        {
            LineCapHelper.LineCapDrawInfo info = LineCapHelper.MakeCapInfo(start, end, width, cap, customCap);
            if ((info != null) && (info.path != null))
            {
                start = info.start;
                color = this.ApplyTransparency(color);
                if (info.fill)
                {
                    this.Context.SetRGBFillColor(color);
                    this.DrawPathInternal(info.path);
                    this.Context.Fill();
                }
                else
                {
                    this.Context.SetRGBStrokeColor(color);
                    this.Context.SetLineWidth(this.TransformValue(width));
                    this.DrawPathInternal(info.path);
                    this.Context.Stroke();
                }
                info.path.Dispose();
            }
        }

        public void DrawEllipse(Pen pen, RectangleF rect)
        {
            if (pen.Color.A != 0)
            {
                rect = this.CorrectRectangle(rect);
                this.ApplyPendingTransform();
                this.Context.GSave();
                this.DoClip();
                this.ConstructLineParams(this.TestPen(pen));
                this.ConstructEllipse(rect.X, rect.Y, rect.Width, rect.Height);
                this.Context.ClosePathAndStroke();
                this.Context.GRestore();
            }
        }

        private void DrawEndCap(ref PointF start, PointF end, Pen pen)
        {
            if (pen != null)
            {
                this.DrawCap(ref start, end, pen.Width, pen.EndCap, (pen.EndCap == LineCap.Custom) ? pen.CustomEndCap : null, pen.Color);
            }
        }

        public void DrawImage(System.Drawing.Image image, RectangleF bounds, Color underlyingColor)
        {
            if (((bounds.Width > 0f) && (bounds.Height > 0f)) && (image != null))
            {
                PdfImageBase pdfImage = this.GetPdfImage(image, underlyingColor);
                this.DrawPdfImage(pdfImage, bounds);
            }
        }

        public void DrawImage(System.Drawing.Image image, RectangleF bounds, RectangleF sourceRect, Color underlyingColor)
        {
            if (((bounds.Width > 0f) && (bounds.Height > 0f)) && (image != null))
            {
                this.DrawImage(image, bounds, underlyingColor);
            }
        }

        public void DrawLine(Pen pen, PointF pt1, PointF pt2)
        {
            PointF[] points = new PointF[] { pt1, pt2 };
            this.DrawLines(pen, points);
        }

        public void DrawLines(Pen pen, PointF[] points)
        {
            if ((pen.Color.A != 0) && (points.Length >= 2))
            {
                this.ApplyPendingTransform();
                this.Context.GSave();
                this.DoClip();
                this.DrawStartCap(ref points[0], points[1], pen);
                this.DrawEndCap(ref points[points.Length - 1], points[points.Length - 2], pen);
                this.ConstructLineParams(this.TestPen(pen));
                PointF tf = this.CorrectPoint(points[0]);
                this.Context.MoveTo(tf.X, tf.Y);
                int length = points.Length;
                for (int i = 1; i < length; i++)
                {
                    tf = this.CorrectPoint(points[i]);
                    this.Context.LineTo(tf.X, tf.Y);
                }
                this.Context.Stroke();
                this.Context.GRestore();
            }
        }

        public void DrawPath(Pen pen, GraphicsPath path)
        {
            if (pen.Color.A != 0)
            {
                this.ApplyPendingTransform();
                this.Context.GSave();
                this.DoClip();
                this.ConstructLineParams(this.TestPen(pen));
                this.DrawPathInternal(path);
                if (IsPathClosed(path))
                {
                    this.Context.ClosePathAndStroke();
                }
                else
                {
                    this.Context.Stroke();
                }
                this.Context.GRestore();
            }
        }

        private void DrawPathInternal(GraphicsPath path)
        {
            PathData pathData = path.PathData;
            for (int i = 0; i < pathData.Points.Length; i++)
            {
                PointF tf = this.CorrectPoint(pathData.Points[i]);
                int num2 = pathData.Types[i] & 7;
                switch (num2)
                {
                    case 0:
                        this.Context.MoveTo(tf.X, tf.Y);
                        break;

                    case 1:
                        this.Context.LineTo(tf.X, tf.Y);
                        break;

                    case 3:
                    {
                        PointF tf2 = this.CorrectPoint(pathData.Points[++i]);
                        PointF tf3 = this.CorrectPoint(pathData.Points[++i]);
                        this.Context.CurveTo(tf.X, tf.Y, tf2.X, tf2.Y, tf3.X, tf3.Y);
                        break;
                    }
                    default:
                        break;
                }
                if ((pathData.Types[i] & 0x80) != 0)
                {
                    this.Context.ClosePath();
                }
            }
        }

        private void DrawPdfImage(PdfImageBase pdfImage, RectangleF bounds)
        {
            RectangleF rect = RectangleF.Intersect(this.ClipBounds, bounds);
            RectangleF correctedBounds = this.CorrectRectangle(bounds);
            RectangleF ef3 = this.CorrectRectangle(rect);
            this.ApplyPendingTransform();
            this.Context.GSave();
            this.Context.Rectangle(ef3.X, ef3.Y, ef3.Width, ef3.Height);
            this.Context.Clip();
            this.Context.NewPath();
            this.Context.Concat(pdfImage.Transform(correctedBounds));
            this.Context.ExecuteXObject(pdfImage);
            this.Context.GRestore();
        }

        public void DrawRectangle(Pen pen, RectangleF bounds)
        {
            if (pen.Color.A != 0)
            {
                bounds = this.CorrectRectangle(bounds);
                this.ApplyPendingTransform();
                this.Context.GSave();
                this.DoClip();
                this.ConstructLineParams(this.TestPen(pen));
                this.Context.Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
                this.Context.ClosePathAndStroke();
                this.Context.GRestore();
            }
        }

        private void DrawStartCap(ref PointF start, PointF end, Pen pen)
        {
            if (pen != null)
            {
                this.DrawCap(ref start, end, pen.Width, pen.StartCap, (pen.StartCap == LineCap.Custom) ? pen.CustomStartCap : null, pen.Color);
            }
        }

        public void DrawString(string s, Font font, Brush brush, PointF point, StringFormat format)
        {
            RectangleF ef;
            if ((format == null) || ((format.Alignment == StringAlignment.Near) && (format.LineAlignment == StringAlignment.Near)))
            {
                ef = this.PointToBounds(point);
            }
            else
            {
                SizeF ef2 = this.documentInfo.Measurer.MeasureString(s, font, float.MaxValue, format, this.PageUnit);
                ef = new RectangleF(point.X - GetOffset(format.Alignment, ef2.Width), point.Y - GetOffset(format.LineAlignment, ef2.Height), ef2.Width, ef2.Height);
            }
            this.DrawString(s, font, brush, ef, format);
        }

        public void DrawString(string s, Font font, Brush brush, RectangleF bounds, StringFormat format)
        {
            if (s == null)
            {
                throw new ArgumentNullException("s");
            }
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }
            if (brush == null)
            {
                throw new ArgumentNullException("brush");
            }
            SolidBrush solidBrush = brush as SolidBrush;
            if (solidBrush == null)
            {
                throw new PdfGraphicsException("The brush must be solid");
            }
            if ((solidBrush.Color.A != 0) && !bounds.IsEmpty)
            {
                StringFormat format2 = PrepareStringFormat(format);
                s = HotkeyPrefixHelper.PreprocessHotkeyPrefixesInString(s, format2.HotkeyPrefix);
                PointF[] tfArray = this.ApplyClip(bounds, format2);
                try
                {
                    if (!this.ClipBounds.IsEmpty)
                    {
                        string actualString = this.RemoveChar(s, '\r');
                        Font newFont = null;
                        PdfFont pdfFont = this.document.RegisterFontSmart(font, ref actualString, ref newFont);
                        Font actualFont = (newFont == null) ? font : newFont;
                        pdfFont.NeedToEmbedFont = !this.NeverEmbeddedFonts.FindFont(actualFont);
                        PdfTextRotation rotation = PdfTextRotation.CreateInstance(this, format2);
                        try
                        {
                            PdfTextRotation.BeginRotation(rotation, bounds);
                            try
                            {
                                string[] lines = null;
                                GraphicsBase.EnsureStringFormat(font, bounds, this.PageUnit, format2, validFormat => lines = TextFormatter.CreateInstance(this.PageUnit, this.documentInfo.Measurer).FormatMultilineText(actualString, actualFont, bounds.Width, bounds.Height, validFormat));
                                if (lines.Length != 0)
                                {
                                    this.DrawStringCore(lines, actualFont, pdfFont, solidBrush, this.CreatePdfGraphicsRectangle(bounds), format2);
                                }
                            }
                            finally
                            {
                                format2.Dispose();
                                if (newFont != null)
                                {
                                    newFont.Dispose();
                                }
                            }
                        }
                        finally
                        {
                            PdfTextRotation.EndRotation(rotation);
                        }
                    }
                }
                finally
                {
                    this.clipArray = tfArray;
                }
            }
        }

        private void DrawStringCore(string[] lines, Font actualFont, PdfFont pdfFont, SolidBrush solidBrush, PdfGraphicsRectangle pdfRect, StringFormat actualFormat)
        {
            int num5;
            int num6;
            PdfTextLines lines2 = new PdfTextLines(lines, actualFormat);
            float lineSpacing = FontHelper.GetLineSpacing(actualFont);
            float cellAscent = FontHelper.GetCellAscent(actualFont);
            float cellDescent = FontHelper.GetCellDescent(actualFont);
            float textHeight = (((lines.Length - 1) * lineSpacing) + cellAscent) + cellDescent;
            bool flag = (actualFont.Italic && ((pdfFont.TTFFile != null) && (pdfFont.TTFFile.Post != null))) && (pdfFont.TTFFile.Post.ItalicAngle == 0f);
            bool flag2 = pdfFont.EmulateBold(this.document.Fonts);
            FontLines lines3 = new FontLines(actualFont, cellAscent, cellDescent);
            TextLocation location = TextLocation.CreateInstance(actualFormat);
            float ty = this.GetBaseYAndLimits(location.CalculateTextY(pdfRect, textHeight), lineSpacing, lines2.Count, out num5, out num6) - cellAscent;
            if ((num6 - num5) >= 0)
            {
                this.ApplyPendingTransform();
                this.Context.GSave();
                this.DoClip();
                Color color = this.ApplyTransparency(solidBrush.Color);
                this.Context.BeginText();
                this.Context.SetRGBFillColor(color);
                this.Context.SetRGBStrokeColor(color);
                this.Context.SetFontAndSize(pdfFont, actualFont);
                if (!flag)
                {
                    this.Context.MoveTextPoint(0f, ty);
                }
                if (flag2)
                {
                    this.SetBoldEmulation(FontSizeHelper.GetSizeInPoints(actualFont));
                }
                float tabStopInterval = this.GetTabStopInterval(actualFormat);
                using (TextProcessor processor = new TextProcessor(this.Context, tabStopInterval, actualFont, IsRtl(actualFormat)))
                {
                    for (int i = num5; i <= num6; i++)
                    {
                        string source = lines2[i];
                        float textWidth = processor.GetTextWidth(source);
                        float textX = location.CalculateTextX(pdfRect, textWidth);
                        lines3.AddLines(textX, ty - (lineSpacing * (i - num5)), textWidth);
                        float a = (!this.ScaleStrings || ((textWidth - pdfRect.Width) <= 0.01f)) ? 1f : (pdfRect.Width / textWidth);
                        if (flag)
                        {
                            this.Context.SetTextMatrix(a, 0f, 0.3333f, 1f, textX, ty);
                            ty -= lineSpacing;
                        }
                        else if (a < 1f)
                        {
                            this.Context.SetTextMatrix(a, 0f, 0f, 1f, textX, ty);
                        }
                        else
                        {
                            this.Context.MoveTextPoint(textX, 0f);
                        }
                        processor.ShowText(source);
                        if (!flag && (a >= 1f))
                        {
                            this.Context.MoveTextPoint(-textX, -lineSpacing);
                        }
                    }
                }
                this.Context.EndText();
                lines3.DrawContents(this.Context, solidBrush.Color);
                this.Context.GRestore();
            }
        }

        public void FillEllipse(Brush brush, RectangleF rect)
        {
            Action makeFigure = delegate {
                rect = this.CorrectRectangle(rect);
                this.ConstructEllipse(rect.X, rect.Y, rect.Width, rect.Height);
            };
            this.FillFigure(brush, makeFigure);
        }

        private void FillFigure(Brush brush, Action makeFigure)
        {
            Color black = Color.Black;
            FillAction fillAction = this.GetFillAction(brush, ref black);
            if (fillAction != FillAction.None)
            {
                this.ApplyPendingTransform();
                this.Context.GSave();
                this.DoClip();
                switch (fillAction)
                {
                    case FillAction.Solid:
                        this.FillSolid(black, makeFigure);
                        break;

                    case FillAction.Hatch:
                        this.FillHatch(brush, makeFigure);
                        break;

                    case FillAction.Gradient:
                        this.FillGradient(brush, makeFigure);
                        break;

                    default:
                        break;
                }
                this.Context.GRestore();
            }
        }

        private void FillGradient(Brush brush, Action makeFigure)
        {
            LinearGradientBrush brush2 = brush as LinearGradientBrush;
            makeFigure();
            this.Context.Clip();
            this.Context.NewPath();
            RectangleF rectangle = brush2.Rectangle;
            PointF[] pts = new PointF[] { rectangle.Location, new PointF(rectangle.Right, rectangle.Top), new PointF(rectangle.Left, rectangle.Bottom) };
            brush2.Transform.TransformPoints(pts);
            for (int i = 0; i < pts.Length; i++)
            {
                pts[i] = this.CorrectPoint(pts[i]);
            }
            using (Matrix matrix = new Matrix(new RectangleF(0f, 0f, 1f, rectangle.Height / rectangle.Width), pts))
            {
                this.Context.Concat(matrix);
            }
            this.ApplyShading(brush2.LinearColors[0], brush2.LinearColors[brush2.LinearColors.Length - 1]);
        }

        private void FillHatch(Brush brush, Action makeFigure)
        {
            HatchBrush brush2 = brush as HatchBrush;
            this.ApplyPattern(brush2.ForegroundColor, brush2.BackgroundColor);
            makeFigure();
            this.Context.Fill();
        }

        public void FillPath(Brush brush, GraphicsPath path)
        {
            this.FillFigure(brush, () => this.DrawPathInternal(path));
        }

        public void FillRectangle(Brush brush, RectangleF bounds)
        {
            Action makeFigure = delegate {
                bounds = this.CorrectRectangle(bounds);
                this.Context.Rectangle(bounds.X, bounds.Y, bounds.Width, bounds.Height);
            };
            this.FillFigure(brush, makeFigure);
        }

        private void FillSolid(Color fillColor, Action makeFigure)
        {
            fillColor = this.ApplyTransparency(fillColor);
            this.Context.SetRGBFillColor(fillColor);
            makeFigure();
            this.Context.Fill();
        }

        private static bool FindDelta(SizeF d, SizeF[] deltas)
        {
            foreach (SizeF ef in deltas)
            {
                if (ef == d)
                {
                    return true;
                }
            }
            return false;
        }

        public void Finish()
        {
            this.CloseTransform();
        }

        private Color GetActualBackColor(System.Drawing.Image image, Color underlyingColor) => 
            (!this.CanApplyImageBackgroundColor(image) || DXColor.IsEmpty(underlyingColor)) ? DXColor.Empty : Color.FromArgb(0xff, underlyingColor.R, underlyingColor.G, underlyingColor.B);

        private float GetBaseYAndLimits(float yBase, float lineSpacing, int lineCount, out int startLine, out int endLine) => 
            GetBaseYAndLimits(yBase, lineSpacing, lineCount, out startLine, out endLine, this.CreatePdfGraphicsRectangle(this.ClipBounds));

        private static float GetBaseYAndLimits(float yBase, float lineSpacing, int lineCount, out int startLine, out int endLine, PdfGraphicsRectangle clipBounds)
        {
            startLine = CalculateStartLine(yBase, lineSpacing, clipBounds);
            endLine = Math.Min(CalculateEndLine(yBase, lineSpacing, clipBounds), lineCount - 1);
            return (yBase - (((float) startLine) * lineSpacing));
        }

        private float[] GetDashArray(Pen pen, float width)
        {
            float[] numArray = new float[pen.DashPattern.Length];
            width = (width <= 0f) ? this.TransformValue((float) 1f) : width;
            for (int i = 0; i < numArray.Length; i++)
            {
                numArray[i] = pen.DashPattern[i] * width;
            }
            return numArray;
        }

        private FillAction GetFillAction(Brush brush, ref Color color)
        {
            if (brush == null)
            {
                color = Color.Black;
                return FillAction.Solid;
            }
            if (brush is SolidBrush)
            {
                color = ((SolidBrush) brush).Color;
                return ((color.A != 0) ? FillAction.Solid : FillAction.None);
            }
            if (!(brush is HatchBrush))
            {
                return (!(brush is LinearGradientBrush) ? FillAction.None : FillAction.Gradient);
            }
            HatchBrush brush2 = (HatchBrush) brush;
            if (brush2.HatchStyle == HatchStyle.WideUpwardDiagonal)
            {
                return FillAction.Hatch;
            }
            color = brush2.BackgroundColor;
            return ((color.A != 0) ? FillAction.Solid : FillAction.None);
        }

        private static PdfLineCapStyle GetLineCapStyle(DashCap dashCap) => 
            (dashCap != DashCap.Round) ? PdfLineCapStyle.Butt : PdfLineCapStyle.Round;

        private static PdfLineCapStyle GetLineCapStyle(LineCap lineCap) => 
            (lineCap == LineCap.Square) ? PdfLineCapStyle.ProtectingSquare : ((lineCap == LineCap.Round) ? PdfLineCapStyle.Round : PdfLineCapStyle.Butt);

        private static PdfLineJoinStyle GetLineJoinStyle(LineJoin lineJoin) => 
            (lineJoin == LineJoin.Bevel) ? PdfLineJoinStyle.Bevel : ((lineJoin == LineJoin.Round) ? PdfLineJoinStyle.Round : PdfLineJoinStyle.Miter);

        private static float GetOffset(StringAlignment alignment, float size) => 
            (alignment == StringAlignment.Near) ? 0f : ((alignment == StringAlignment.Center) ? (size / 2f) : size);

        private static GraphicsPath GetPathForPoints(PointF[] points)
        {
            GraphicsPath path = new GraphicsPath();
            path.AddLines(points);
            return path;
        }

        private PdfImageBase GetPdfImage(System.Drawing.Image image, Color underlyingColor)
        {
            Color actualBackColor = this.GetActualBackColor(image, underlyingColor);
            PdfImageCache.Params imageParams = new PdfImageCache.Params(image, actualBackColor);
            PdfImageBase xObject = this.ImageCache[imageParams];
            if (xObject != null)
            {
                this.xObjectsOwner.AddExistingXObject(xObject);
            }
            else
            {
                xObject = this.document.CreatePdfImage(this.documentInfo, this.xObjectsOwner, image, actualBackColor);
                this.ImageCache.AddPdfImage(xObject, imageParams);
            }
            return xObject;
        }

        private float GetTabStopInterval(StringFormat stringFormat)
        {
            float num;
            float[] tabStops = stringFormat.GetTabStops(out num);
            return ((tabStops.Length == 0) ? 0f : this.TransformValue(tabStops[0]));
        }

        private static bool IsPathClosed(GraphicsPath path)
        {
            PointF[] pathPoints = path.PathPoints;
            return ((pathPoints.Length != 0) ? (pathPoints[0] == pathPoints[pathPoints.Length - 1]) : false);
        }

        private static bool IsRectangle(SizeF[] deltas, float width, float height) => 
            (width != 0f) && ((height != 0f) && (FindDelta(new SizeF(width, 0f), deltas) ? (FindDelta(new SizeF(0f, height), deltas) ? FindDelta(new SizeF(width, height), deltas) : false) : false));

        private static bool IsRtl(StringFormat actualFormat) => 
            (actualFormat.FormatFlags & StringFormatFlags.DirectionRightToLeft) != 0;

        public void MultiplyTransform(Matrix matrix, MatrixOrder order)
        {
            this.AddTransform(matrix, order);
        }

        internal static RectangleF PointArray2Rectangle(PointF[] pointArray)
        {
            float num;
            float num2;
            return (((pointArray == null) || (pointArray.Length != 4)) ? RectangleF.Empty : (IsRectangle(CreateDeltas(pointArray, out num, out num2), num, num2) ? CreateRectangle(pointArray[0], num, num2) : RectangleF.Empty));
        }

        private static RectangleF PointArrayToFramingRectangle(PointF[] pointArray)
        {
            if (pointArray == null)
            {
                return new RectangleF(-1.701412E+38f, -1.701412E+38f, float.MaxValue, float.MaxValue);
            }
            float maxValue = float.MaxValue;
            float num2 = float.MaxValue;
            float minValue = float.MinValue;
            float num4 = float.MinValue;
            foreach (PointF tf in pointArray)
            {
                maxValue = Math.Min(maxValue, tf.X);
                num2 = Math.Min(num2, tf.Y);
                minValue = Math.Max(minValue, tf.X);
                num4 = Math.Max(num4, tf.Y);
            }
            return new RectangleF(maxValue, num2, minValue - maxValue, num4 - num2);
        }

        private RectangleF PointToBounds(PointF point) => 
            this.CutBoundsByPage(new RectangleF(point, new SizeF(float.MaxValue, float.MaxValue)));

        private static StringFormat PrepareStringFormat(StringFormat format)
        {
            if (format == null)
            {
                return (StringFormat) StringFormat.GenericTypographic.Clone();
            }
            StringFormat format2 = (StringFormat) format.Clone();
            StringAlignment alignment = format2.Alignment;
            StringAlignment lineAlignment = format2.LineAlignment;
            bool flag2 = (format2.FormatFlags & StringFormatFlags.DirectionRightToLeft) != 0;
            if ((format2.FormatFlags & StringFormatFlags.DirectionVertical) != 0)
            {
                format2.Alignment = lineAlignment;
                format2.LineAlignment = alignment;
            }
            if (flag2)
            {
                format2.Alignment = ReverseAlignment(format2.Alignment);
            }
            return format2;
        }

        private static PointF[] RectangleToPointArray(RectangleF rect) => 
            new PointF[] { new PointF(rect.Left, rect.Top), new PointF(rect.Right, rect.Top), new PointF(rect.Right, rect.Bottom), new PointF(rect.Left, rect.Bottom) };

        private string RemoveChar(string str, char ch)
        {
            for (int i = str.Length - 1; i >= 0; i--)
            {
                if (str[i] == ch)
                {
                    str = str.Remove(i, 1);
                }
            }
            return str;
        }

        private void ResetClip()
        {
            if (this.clipArray != null)
            {
                using (Matrix matrix = this.transform.Clone())
                {
                    matrix.TransformPoints(this.clipArray);
                }
            }
        }

        public void ResetTransform()
        {
            this.ResetClip();
            this.transform.Reset();
            this.AddPendingTransform();
        }

        private static StringAlignment ReverseAlignment(StringAlignment alignment) => 
            (alignment == StringAlignment.Near) ? StringAlignment.Far : ((alignment == StringAlignment.Far) ? StringAlignment.Near : alignment);

        public void RotateTransform(float angle, MatrixOrder order)
        {
            Matrix matrix = new Matrix();
            matrix.Rotate(angle);
            this.AddTransform(matrix, order);
        }

        public void SaveTransformState()
        {
            this.transformStack.Push(this.transform.Clone());
        }

        public void ScaleTransform(float sx, float sy, MatrixOrder order)
        {
            Matrix matrix = new Matrix();
            matrix.Scale(sx, sy);
            this.AddTransform(matrix, order);
        }

        private void SetBoldEmulation(float fontSize)
        {
            this.Context.SetRenderingMode(PdfTextRenderingMode.FillThenStroke);
            this.Context.SetLineWidth(fontSize / 30f);
        }

        public void SetClip(GraphicsPath path, CombineMode combineMode)
        {
            this.SetClip(new Region(path), combineMode);
        }

        public void SetClip(RectangleF rect, CombineMode combineMode)
        {
            this.SetClip(new Region(rect), combineMode);
        }

        public void SetClip(Region region, CombineMode combineMode)
        {
            Region region2 = (this.clipArray != null) ? new Region(GetPathForPoints(this.clipArray)) : new Region();
            switch (combineMode)
            {
                case CombineMode.Replace:
                    region2 = region;
                    break;

                case CombineMode.Intersect:
                    region2.Intersect(region);
                    break;

                case CombineMode.Union:
                    region2.Union(region);
                    break;

                case CombineMode.Xor:
                    region2.Xor(region);
                    break;

                case CombineMode.Exclude:
                    region2.Exclude(region);
                    break;

                case CombineMode.Complement:
                    region2.Complement(region);
                    break;

                default:
                    break;
            }
            RectangleF bounds = region2.GetBounds(Graphics.FromHwnd(IntPtr.Zero));
            this.clipArray = RectangleToPointArray(bounds);
            if (bounds.IsEmpty)
            {
                this.clipArray = null;
            }
        }

        public void SetTransform(Matrix matrix)
        {
            this.ResetClip();
            this.transform.Reset();
            this.transform.Multiply(matrix);
            this.AddPendingTransform();
            this.ApplyTransformCacheToClipArray();
        }

        private void SetTransparency(int transparency)
        {
            PdfTransparencyGS gs = this.document.TransparencyGS.CreateAddUnique(transparency);
            this.xObjectsOwner.AddTransparencyGS(gs);
            this.Context.ExecuteGraphicsState(gs);
        }

        private Pen TestPen(Pen pen) => 
            (pen != null) ? pen : new Pen(DXColor.Black);

        private PointF TransformValue(PointF value) => 
            PdfCoordinate.TransformValue(this.PageUnit, value);

        private SizeF TransformValue(SizeF value) => 
            PdfCoordinate.TransformValue(this.PageUnit, value);

        private float TransformValue(float value) => 
            PdfCoordinate.TransformValue(this.PageUnit, value);

        public void TranslateTransform(float dx, float dy, MatrixOrder order)
        {
            Matrix matrix = new Matrix();
            matrix.Translate(dx, dy);
            this.AddTransform(matrix, order);
        }

        public GraphicsUnit PageUnit
        {
            get => 
                this.pageUnit;
            set
            {
                this.ApplyPageUnitToClipArray(value);
                this.pageUnit = value;
            }
        }

        private PdfImageCache ImageCache =>
            this.documentInfo.ImageCache;

        private PdfNeverEmbeddedFonts NeverEmbeddedFonts =>
            this.documentInfo.NeverEmbeddedFonts;

        public bool ScaleStrings =>
            this.documentInfo.ScaleStrings;

        private PdfDrawContext Context =>
            this.context;

        private SizeF PageSizeInPoints =>
            this.size;

        public Matrix Transform =>
            this.transform;

        public float Dpi =>
            72f;

        public RectangleF ClipBounds
        {
            get => 
                PointArrayToFramingRectangle(this.clipArray);
            set => 
                this.clipArray = RectangleToPointArray(value);
        }

        private enum FillAction
        {
            None,
            Solid,
            Hatch,
            Gradient
        }

        private enum PdfGraphicsState
        {
            No,
            Pending,
            Opened
        }
    }
}

