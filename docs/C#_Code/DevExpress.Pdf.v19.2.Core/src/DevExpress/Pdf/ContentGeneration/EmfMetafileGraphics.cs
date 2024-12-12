namespace DevExpress.Pdf.ContentGeneration
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.Native;
    using DevExpress.Text.Fonts;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;

    public class EmfMetafileGraphics : PdfDisposableObject
    {
        private const float milimetersPerInch = 25.4f;
        private const float pointsPerInch = 72f;
        private const float documentsPerInch = 300f;
        private const float printerDefaultDPI = 100f;
        private readonly PdfGraphicsCommandConstructor graphics;
        private readonly Dictionary<int, object> objectTable = new Dictionary<int, object>();
        private readonly EmfPlusGraphicsStateStack state = new EmfPlusGraphicsStateStack();
        private readonly PdfExportFontManager fontCache;
        private float logicalDpiX = 96f;
        private float logicalDpiY = 96f;
        private bool isVideoDisplay = true;
        private DXGraphicsUnit pageUnit = DXGraphicsUnit.Pixel;
        private float scaleFactor = 1f;

        public EmfMetafileGraphics(PdfGraphicsCommandConstructor graphics, PdfExportFontManager fontCache)
        {
            this.graphics = graphics;
            this.fontCache = fontCache;
        }

        public void AddEmfPlusContinuedObject(int index, EmfPlusContinuedObject obj)
        {
            object obj2;
            if (!this.objectTable.TryGetValue(index, out obj2))
            {
                this.objectTable.Add(index, obj);
            }
            else
            {
                EmfPlusContinuedObject obj3 = obj2 as EmfPlusContinuedObject;
                this.objectTable[index] = (obj3 != null) ? obj3.Append(obj) : obj;
            }
        }

        public void AddEmfPlusObject(int index, object obj)
        {
            if (this.objectTable.ContainsKey(index))
            {
                this.objectTable[index] = obj;
            }
            else
            {
                this.objectTable.Add(index, obj);
            }
        }

        public void ApplyClip()
        {
            if (this.state.Clip != null)
            {
                this.state.Clip.ApplyClip(this.graphics);
            }
        }

        private void ApplyMatrix()
        {
            this.graphics.UpdateTransformationMatrix(this.GetFullTransformationMatrix());
        }

        public void Clear(ARGBColor color)
        {
            this.SetFillColor(color);
            this.graphics.Clear();
        }

        private static bool Compare(float a, float b) => 
            Math.Abs((float) (a - b)) < float.Epsilon;

        private static PdfStringAlignment ConvertAlignment(EmfPlusStringAlignment value) => 
            (value == EmfPlusStringAlignment.Center) ? PdfStringAlignment.Center : ((value == EmfPlusStringAlignment.Far) ? PdfStringAlignment.Far : PdfStringAlignment.Near);

        private static PdfTransformationMatrix ConvertMatrix(DXTransformationMatrix matrix) => 
            new PdfTransformationMatrix((double) matrix.A, (double) matrix.B, (double) matrix.C, (double) matrix.D, (double) matrix.E, (double) matrix.F);

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                foreach (object obj2 in this.objectTable.Values)
                {
                    IDisposable disposable = obj2 as IDisposable;
                    if (disposable != null)
                    {
                        disposable.Dispose();
                    }
                }
            }
        }

        public void DrawBeziers(DXPointF[] points)
        {
            this.graphics.DrawBeziers(PdfGDIPlusGraphicsConverter.ConvertPoints(points));
        }

        public void DrawEllipse(DXRectangleF bounds)
        {
            this.graphics.DrawEllipse(PdfGDIPlusGraphicsConverter.ConvertRectangle(bounds));
        }

        public void DrawImage(int id, DXPointF[] destinationPoints, DXRectangleF srcRect)
        {
            EmfPlusImage emfPlusObject = this.GetEmfPlusObject<EmfPlusImage>(id);
            if (emfPlusObject != null)
            {
                Image image = emfPlusObject.Image;
                RectangleF rect = PdfGDIPlusGraphicsConverter.ConvertRectangle(srcRect);
                float x = destinationPoints[0].X;
                float y = destinationPoints[0].Y;
                DXPointF tf = destinationPoints[1];
                DXPointF tf2 = destinationPoints[2];
                if (IsDefaultSourceRectangle(srcRect, image) && (Compare(tf.Y, y) && (Compare(tf2.X, x) && ((tf.X > x) && (tf2.Y > y)))))
                {
                    this.graphics.DrawXObject(this.graphics.ImageCache.AddXObject(image), new RectangleF(x, y, tf.X - x, tf2.Y - y));
                }
                else
                {
                    using (Matrix matrix = new Matrix(rect, PdfGDIPlusGraphicsConverter.ConvertPoints(destinationPoints)))
                    {
                        RectangleF bounds;
                        float[] elements = matrix.Elements;
                        this.graphics.SaveGraphicsState();
                        this.graphics.UpdateTransformationMatrix(new PdfTransformationMatrix((double) elements[0], (double) elements[1], (double) elements[2], (double) elements[3], (double) elements[4], (double) elements[5]));
                        this.graphics.IntersectClip(rect);
                        Metafile metafile = image as Metafile;
                        if (metafile != null)
                        {
                            bounds = metafile.GetMetafileHeader().Bounds;
                        }
                        else
                        {
                            bounds = new RectangleF(0f, 0f, (float) image.Width, (float) image.Height);
                        }
                        this.graphics.DrawXObject(this.graphics.ImageCache.AddXObject(image), bounds);
                        this.graphics.RestoreGraphicsState();
                    }
                }
            }
        }

        public void DrawImage(int id, DXRectangleF bounds, DXRectangleF srcRect)
        {
            EmfPlusImage emfPlusObject = this.GetEmfPlusObject<EmfPlusImage>(id);
            if (emfPlusObject != null)
            {
                Image image = emfPlusObject.Image;
                Metafile metafile = image as Metafile;
                if (metafile != null)
                {
                    if (IsDefaultSourceRectangle(srcRect, image))
                    {
                        this.graphics.DrawXObject(this.graphics.ImageCache.AddXObject(metafile), PdfGDIPlusGraphicsConverter.ConvertRectangle(bounds));
                    }
                    else
                    {
                        float num = bounds.Width / srcRect.Width;
                        float num2 = bounds.Height / srcRect.Height;
                        RectangleF ef = new RectangleF(bounds.X - (num * srcRect.X), bounds.Y - (num2 * srcRect.Y), num * image.Width, num2 * image.Height);
                        this.SaveGraphicsState();
                        this.graphics.IntersectClip(PdfGDIPlusGraphicsConverter.ConvertRectangle(bounds));
                        this.graphics.DrawXObject(this.graphics.ImageCache.AddXObject(metafile), ef);
                        this.RestoreGraphicsState();
                    }
                }
                else
                {
                    Bitmap bitmap = image as Bitmap;
                    if (bitmap != null)
                    {
                        using (Bitmap bitmap2 = bitmap.Clone(PdfGDIPlusGraphicsConverter.ConvertRectangle(srcRect), bitmap.PixelFormat))
                        {
                            this.graphics.DrawXObject(this.graphics.ImageCache.AddXObject(bitmap2), PdfGDIPlusGraphicsConverter.ConvertRectangle(bounds));
                        }
                    }
                }
            }
        }

        public void DrawLines(DXPointF[] points)
        {
            this.graphics.DrawLines(PdfGDIPlusGraphicsConverter.ConvertPoints(points));
        }

        public void DrawPath(int id)
        {
            EmfPlusPath emfPlusObject = this.GetEmfPlusObject<EmfPlusPath>(id);
            if (emfPlusObject != null)
            {
                DXGraphicsPathData pathData = emfPlusObject.PathData;
                this.graphics.DrawPath(PdfGDIPlusGraphicsConverter.ConvertPoints(pathData.Points), PdfGDIPlusGraphicsConverter.ConvertPointTypes(pathData.PathTypes));
            }
        }

        public void DrawPath(PointF[] pathPoints, byte[] pathTypes)
        {
            this.graphics.DrawPath(pathPoints, pathTypes);
        }

        public void DrawPolygon(DXPointF[] points)
        {
            this.graphics.DrawPolygon(PdfGDIPlusGraphicsConverter.ConvertPoints(points));
        }

        public void DrawRectangles(DXRectangleF[] rects)
        {
            foreach (DXRectangleF ef in rects)
            {
                this.graphics.DrawRectangle(PdfGDIPlusGraphicsConverter.ConvertRectangle(ef));
            }
        }

        public void DrawString(string text, DXRectangleF layoutRect, int fontId, ARGBColor? color, int brushId, int formatId)
        {
            PdfStringFormat genericDefault;
            PdfExportFontInfo font = this.GetFont(fontId);
            this.SetFontColor(font, color, brushId);
            EmfPlusStringFormat emfPlusObject = this.GetEmfPlusObject<EmfPlusStringFormat>(formatId);
            if (emfPlusObject == null)
            {
                genericDefault = PdfStringFormat.GenericDefault;
            }
            else
            {
                EmfPlusStringFormatFlags formatFlags = emfPlusObject.FormatFlags;
                PdfStringFormatFlags flags2 = 0;
                if (formatFlags.HasFlag(EmfPlusStringFormatFlags.StringFormatLineLimit))
                {
                    flags2 |= PdfStringFormatFlags.LineLimit;
                }
                if (formatFlags.HasFlag(EmfPlusStringFormatFlags.StringFormatNoClip))
                {
                    flags2 |= PdfStringFormatFlags.NoClip;
                }
                if (formatFlags.HasFlag(EmfPlusStringFormatFlags.StringFormatNoWrap))
                {
                    flags2 |= PdfStringFormatFlags.NoWrap;
                }
                if (formatFlags.HasFlag(EmfPlusStringFormatFlags.StringFormatMeasureTrailingSpaces))
                {
                    flags2 |= PdfStringFormatFlags.MeasureTrailingSpaces;
                }
                genericDefault = new PdfStringFormat(flags2) {
                    Alignment = ConvertAlignment(emfPlusObject.Alignment),
                    LineAlignment = ConvertAlignment(emfPlusObject.LineAlignment),
                    LeadingMarginFactor = emfPlusObject.LeadingMarginFactor,
                    TrailingMarginFactor = emfPlusObject.TrailingMarginFactor
                };
                if (emfPlusObject.HotkeyPrefix == EmfPlusHotkeyPrefix.Hide)
                {
                    genericDefault.HotkeyPrefix = PdfHotkeyPrefix.Hide;
                }
                switch (emfPlusObject.Trimming)
                {
                    case EmfPlusStringTrimming.Character:
                        genericDefault.Trimming = PdfStringTrimming.Character;
                        break;

                    case EmfPlusStringTrimming.Word:
                        genericDefault.Trimming = PdfStringTrimming.Word;
                        break;

                    case EmfPlusStringTrimming.EllipsisCharacter:
                        genericDefault.Trimming = PdfStringTrimming.EllipsisCharacter;
                        break;

                    case EmfPlusStringTrimming.EllipsisWord:
                        genericDefault.Trimming = PdfStringTrimming.EllipsisWord;
                        break;

                    default:
                        genericDefault.Trimming = PdfStringTrimming.None;
                        break;
                }
                float[] tabStops = emfPlusObject.TabStops;
                genericDefault.TabStopInterval = ((tabStops == null) || (tabStops.Length == 0)) ? ((double) 0f) : ((double) tabStops[0]);
            }
            this.graphics.DrawString(text, font, PdfGDIPlusGraphicsConverter.ConvertRectangle(layoutRect), genericDefault, PdfEnvironmentHelper.ShouldUseKerning ? DXKerningMode.MultilineOnly : DXKerningMode.None);
        }

        public void DrawUnicodeString(char[] glyphs, DXPointF[] positions, int fontId, int brushId, ARGBColor? color)
        {
            PdfExportFontInfo font = this.GetFont(fontId);
            this.SetFontColor(font, color, brushId);
            for (int i = 0; i < glyphs.Length; i++)
            {
                DXPointF tf = positions[i];
                this.graphics.DrawString(glyphs[i].ToString(), font, new PointF(tf.X, tf.Y), PdfStringFormat.GenericTypographic, PdfGraphicsTextOrigin.Baseline, DXKerningMode.None);
            }
        }

        public void FillEllipse(DXRectangleF bounds)
        {
            this.graphics.FillEllipse(PdfGDIPlusGraphicsConverter.ConvertRectangle(bounds));
        }

        public void FillPath(int id)
        {
            EmfPlusPath emfPlusObject = this.GetEmfPlusObject<EmfPlusPath>(id);
            if (emfPlusObject != null)
            {
                DXGraphicsPathData pathData = emfPlusObject.PathData;
                this.graphics.FillPath(PdfGDIPlusGraphicsConverter.ConvertPoints(pathData.Points), PdfGDIPlusGraphicsConverter.ConvertPointTypes(pathData.PathTypes), pathData.IsWindingFillMode);
            }
        }

        public void FillPath(PointF[] pathPoints, byte[] pathTypes)
        {
            this.graphics.FillPath(pathPoints, pathTypes, true);
        }

        public void FillPolygon(DXPointF[] points)
        {
            this.graphics.FillPolygon(PdfGDIPlusGraphicsConverter.ConvertPoints(points), false);
        }

        public void FillRects(DXRectangleF[] rectangles)
        {
            foreach (DXRectangleF ef in rectangles)
            {
                this.graphics.FillRectangle(PdfGDIPlusGraphicsConverter.ConvertRectangle(ef));
            }
        }

        private T GetEmfPlusObject<T>(int id) where T: class
        {
            object obj2;
            if (this.objectTable.TryGetValue(id, out obj2))
            {
                return (obj2 as T);
            }
            return default(T);
        }

        private PdfExportFontInfo GetFont(int fontId)
        {
            DXGraphicsUnit unitType;
            float emSize;
            Font font2;
            EmfPlusFont emfPlusObject = this.GetEmfPlusObject<EmfPlusFont>(fontId);
            if (emfPlusObject != null)
            {
                unitType = emfPlusObject.UnitType;
                emSize = emfPlusObject.EmSize;
                font2 = new Font(emfPlusObject.FontFamily, emfPlusObject.EmSize, (FontStyle) emfPlusObject.FontStyle, (GraphicsUnit) unitType);
            }
            else
            {
                unitType = DXGraphicsUnit.Pixel;
                emSize = 10f;
                font2 = new Font("Arial", 10f);
            }
            using (font2)
            {
                PdfExportFontInfo fontInfo = this.fontCache.GetFontInfo(font2);
                float num2 = ((this.pageUnit != DXGraphicsUnit.Pixel) || ((unitType == DXGraphicsUnit.Pixel) || (unitType == DXGraphicsUnit.World))) ? 1f : this.logicalDpiX;
                DXFontDecorations none = DXFontDecorations.None;
                if (font2.Strikeout)
                {
                    none |= DXFontDecorations.Strikeout;
                }
                if (font2.Underline)
                {
                    none |= DXFontDecorations.Underline;
                }
                return new PdfExportFontInfo(fontInfo.Font, (float) ((emSize * (this.GetUnitScale(1f, unitType) / this.GetUnitScale(this.scaleFactor, this.pageUnit))) * num2), none);
            }
        }

        private PdfTransformationMatrix GetFullTransformationMatrix() => 
            this.GetFullTransformationMatrix(this.state.Transform);

        private PdfTransformationMatrix GetFullTransformationMatrix(PdfTransformationMatrix matrix)
        {
            PdfTransformationMatrix matrix2 = PdfTransformationMatrix.Multiply(this.state.PageUnitTransformationMatrix, this.state.PageTransformationMatrix);
            return PdfTransformationMatrix.Multiply(matrix, matrix2);
        }

        private double GetUnitScale(float scale, DXGraphicsUnit unit)
        {
            switch (unit)
            {
                case DXGraphicsUnit.Display:
                    return (double) (scale / 100f);

                case DXGraphicsUnit.Point:
                    return (double) (scale / 72f);

                case DXGraphicsUnit.Inch:
                    return (double) scale;

                case DXGraphicsUnit.Document:
                    return (double) (scale / 300f);

                case DXGraphicsUnit.Millimeter:
                    return (double) (scale / 25.4f);
            }
            return 1.0;
        }

        private static bool IsDefaultSourceRectangle(DXRectangleF rectangle, Image image) => 
            Compare((float) image.Width, rectangle.Width) && (Compare((float) image.Height, rectangle.Height) && (Compare(rectangle.X, 0f) && Compare(rectangle.Y, 0f)));

        public void MultiplyWorldTransform(DXTransformationMatrix matrix, bool isPostMultiplied)
        {
            this.MultiplyWorldTransform(ConvertMatrix(matrix), isPostMultiplied);
        }

        private void MultiplyWorldTransform(PdfTransformationMatrix matrix, bool isPostMultiplied)
        {
            if (isPostMultiplied)
            {
                this.PostMultiplyWorldTransform(matrix);
            }
            else
            {
                this.graphics.UpdateTransformationMatrix(matrix);
                this.state.Transform = PdfTransformationMatrix.Multiply(matrix, this.state.Transform);
            }
        }

        private void PostMultiplyWorldTransform(PdfTransformationMatrix matrix)
        {
            this.SetMatrix(PdfTransformationMatrix.Multiply(this.state.Transform, matrix));
        }

        public void ResetClip()
        {
            this.SetClip(new EmfPlusRegion(new EmfPlusRegionInfiniteNode()), EmfPlusCombineMode.CombineModeReplace);
        }

        private void ResetCurrentTransform()
        {
            this.graphics.UpdateTransformationMatrix(PdfTransformationMatrix.Invert(this.GetFullTransformationMatrix()));
        }

        public void Restore(int id)
        {
            while (true)
            {
                this.graphics.RestoreGraphicsState();
                this.state.Pop();
                int? currentId = this.state.CurrentId;
                int? nullable2 = this.state.CurrentId;
                if (nullable2.Value == id)
                {
                    return;
                }
            }
        }

        public void RestoreGraphicsState()
        {
            this.graphics.RestoreGraphicsState();
        }

        public void RotateTransform(float angle, bool isPostMultiplied)
        {
            this.MultiplyWorldTransform(PdfTransformationMatrix.CreateRotate((double) angle), isPostMultiplied);
        }

        public void Save(int id)
        {
            this.state.Push(id);
            this.graphics.SaveGraphicsState();
        }

        public void SaveGraphicsState()
        {
            this.graphics.SaveGraphicsState();
        }

        public void SetBrush(int id)
        {
            EmfPlusBrush emfPlusObject = this.GetEmfPlusObject<EmfPlusBrush>(id);
            if (emfPlusObject != null)
            {
                this.graphics.SetBrush(emfPlusObject.Brush);
            }
        }

        private void SetClip(EmfPlusRegion clipRegion, EmfPlusCombineMode mode)
        {
            PdfTransformationMatrix fullTransformationMatrix = this.GetFullTransformationMatrix();
            using (Matrix matrix2 = new Matrix((float) fullTransformationMatrix.A, (float) fullTransformationMatrix.B, (float) fullTransformationMatrix.C, (float) fullTransformationMatrix.D, (float) fullTransformationMatrix.E, (float) fullTransformationMatrix.F))
            {
                clipRegion = clipRegion.GetTransformedRegion(matrix2);
            }
            EmfPlusClip clip = this.state.Clip;
            if (clip != null)
            {
                this.state.Clip = clip.Combine(mode, new EmfPlusClip(clipRegion));
            }
            else
            {
                this.state.Clip = new EmfPlusClip(clipRegion);
            }
        }

        public void SetClipPath(int id, EmfPlusCombineMode mode)
        {
            EmfPlusPath emfPlusObject = this.GetEmfPlusObject<EmfPlusPath>(id);
            if (emfPlusObject != null)
            {
                this.SetClip(new EmfPlusRegion(new EmfPlusRegionPathNode(emfPlusObject)), mode);
            }
        }

        public void SetClipRectangle(DXRectangleF rect, EmfPlusCombineMode mode)
        {
            this.SetClip(new EmfPlusRegion(new EmfPlusRegionRectangleNode(PdfGDIPlusGraphicsConverter.ConvertRectangle(rect))), mode);
        }

        public void SetClipRegion(int id, EmfPlusCombineMode mode)
        {
            EmfPlusRegion emfPlusObject = this.GetEmfPlusObject<EmfPlusRegion>(id);
            if (emfPlusObject != null)
            {
                this.SetClip(emfPlusObject, mode);
            }
        }

        public void SetFillColor(ARGBColor color)
        {
            this.graphics.SetFillColor(color);
        }

        private void SetFontColor(PdfExportFontInfo fontInfo, ARGBColor? color, int brushId)
        {
            if (color != null)
            {
                ARGBColor color2 = color.Value;
                this.graphics.SetFillColor(color2);
                if (fontInfo.ShouldSetStrokingColor)
                {
                    this.graphics.SetUnscaledPen(color2, fontInfo.FontLineSize);
                }
            }
            else
            {
                EmfPlusBrush emfPlusObject = this.GetEmfPlusObject<EmfPlusBrush>(brushId);
                if (emfPlusObject != null)
                {
                    DXBrush brush = emfPlusObject.Brush;
                    this.graphics.SetBrush(brush);
                    if (fontInfo.ShouldSetStrokingColor)
                    {
                        this.graphics.SetUnscaledPen(new DXPen(brush, fontInfo.FontLineSize));
                    }
                }
            }
        }

        public void SetInitialOffset(PdfTransformationMatrix matrix)
        {
            this.state.PageTransformationMatrix = matrix;
            this.ApplyMatrix();
        }

        public void SetLogicalDpi(float logicalDpiX, float logicalDpiY, bool isVideoDisplay)
        {
            this.logicalDpiX = logicalDpiX;
            this.logicalDpiY = logicalDpiY;
            this.isVideoDisplay = isVideoDisplay;
        }

        private void SetMatrix(PdfTransformationMatrix matrix)
        {
            this.graphics.UpdateTransformationMatrix(PdfTransformationMatrix.Multiply(this.GetFullTransformationMatrix(matrix), PdfTransformationMatrix.Invert(this.GetFullTransformationMatrix())));
            this.state.Transform = matrix;
        }

        public void SetPageUnit(DXGraphicsUnit unit, float scaleFactor)
        {
            if ((unit == DXGraphicsUnit.Display) && this.isVideoDisplay)
            {
                unit = DXGraphicsUnit.Pixel;
            }
            this.pageUnit = unit;
            this.scaleFactor = scaleFactor;
            double scaleX = 1.0;
            double scaleY = 1.0;
            switch (unit)
            {
                case DXGraphicsUnit.World:
                case DXGraphicsUnit.Pixel:
                    scaleX = 1.0;
                    scaleY = 1.0;
                    break;

                case DXGraphicsUnit.Display:
                    scaleX = this.logicalDpiX / 100f;
                    scaleY = this.logicalDpiY / 100f;
                    break;

                case DXGraphicsUnit.Point:
                    scaleX = this.logicalDpiX / 72f;
                    scaleY = this.logicalDpiY / 72f;
                    break;

                case DXGraphicsUnit.Inch:
                    scaleX = this.logicalDpiX;
                    scaleY = this.logicalDpiY;
                    break;

                case DXGraphicsUnit.Document:
                    scaleX = this.logicalDpiX / 300f;
                    scaleY = this.logicalDpiY / 300f;
                    break;

                case DXGraphicsUnit.Millimeter:
                    scaleX = this.logicalDpiX / 25.4f;
                    scaleY = this.logicalDpiY / 25.4f;
                    break;

                default:
                    break;
            }
            if (unit != DXGraphicsUnit.Display)
            {
                scaleX *= scaleFactor;
                scaleY *= scaleFactor;
            }
            this.ResetCurrentTransform();
            this.state.PageUnitTransformationMatrix = PdfTransformationMatrix.CreateScale(scaleX, scaleY);
            this.ApplyMatrix();
        }

        public void SetPen(DXPen pen)
        {
            this.graphics.SetPen(pen);
        }

        public void SetPen(int id)
        {
            EmfPlusPen emfPlusObject = this.GetEmfPlusObject<EmfPlusPen>(id);
            if (emfPlusObject != null)
            {
                this.graphics.SetPen(emfPlusObject.Pen);
            }
        }

        public void SetWorldTransform(DXTransformationMatrix matrix)
        {
            this.SetMatrix(ConvertMatrix(matrix));
        }
    }
}

