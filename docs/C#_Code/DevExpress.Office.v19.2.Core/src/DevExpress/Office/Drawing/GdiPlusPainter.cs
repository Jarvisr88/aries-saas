namespace DevExpress.Office.Drawing
{
    using DevExpress.Data.Utils;
    using DevExpress.Office.Layout;
    using DevExpress.Office.Utils;
    using DevExpress.Office.Utils.Internal;
    using DevExpress.Utils;
    using DevExpress.Utils.Drawing;
    using DevExpress.Utils.Text;
    using DevExpress.XtraPrinting;
    using DevExpress.XtraPrinting.BrickExporters;
    using DevExpress.XtraPrinting.Native;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class GdiPlusPainter : GdiPlusPainterBase
    {
        private const float grayscaleFactor = 0.5f;
        private static readonly ImageAttributes grayscaleImageAttributes = CreateGrayscaleImageAttributes();
        private readonly IGraphicsCache cache;
        private readonly DocumentLayoutUnitConverter unitConverter;
        private ConditionalWeakTable<OfficeImage, ImageCacheData> bitmapCache;
        private readonly Stack<Matrix> transformsStack;
        private readonly Stack<SmoothingMode> smoothingmodeStack;
        private readonly Stack<PixelOffsetMode> pixelmodeStack;
        private readonly Stack<Region> clipRegions;
        private readonly Stack<RectangleF> rectangularBounds;
        private RectangleF rectangularClipBounds;
        private int dpiY;
        private const int maxAccessibleLength = 0x7d0;
        private const int maxCachedBitmapSize = 0x249f00;

        public GdiPlusPainter(IGraphicsCache cache, DocumentLayoutUnitConverter unitConverter)
        {
            Guard.ArgumentNotNull(cache, "cache");
            this.cache = cache;
            Guard.ArgumentNotNull(unitConverter, "unitConverter");
            this.unitConverter = unitConverter;
            this.transformsStack = new Stack<Matrix>();
            this.smoothingmodeStack = new Stack<SmoothingMode>();
            this.pixelmodeStack = new Stack<PixelOffsetMode>();
            this.clipRegions = new Stack<Region>();
            this.rectangularBounds = new Stack<RectangleF>();
            this.dpiY = (int) cache.Graphics.DpiY;
            this.rectangularClipBounds = this.Graphics.ClipBounds;
        }

        private System.Drawing.Image AddBitmapToCache(OfficeImage img, Size sz, bool grayed)
        {
            System.Drawing.Image image = new Bitmap(sz.Width, sz.Height);
            using (System.Drawing.Graphics graphics = InternalOfficeImageHelper.CreateEnhancedGraphics(image))
            {
                System.Drawing.Image nativeImage = img.NativeImage;
                if (grayed)
                {
                    graphics.DrawImage(nativeImage, new Rectangle(0, 0, sz.Width, sz.Height), 0, 0, nativeImage.Width, nativeImage.Height, GraphicsUnit.Pixel, grayscaleImageAttributes);
                }
                else
                {
                    graphics.DrawImage(nativeImage, new Rectangle(0, 0, sz.Width, sz.Height));
                }
            }
            ConditionalWeakTable<OfficeImage, ImageCacheData>.CreateValueCallback createValueCallback = <>c.<>9__51_0;
            if (<>c.<>9__51_0 == null)
            {
                ConditionalWeakTable<OfficeImage, ImageCacheData>.CreateValueCallback local1 = <>c.<>9__51_0;
                createValueCallback = <>c.<>9__51_0 = key => new ImageCacheData();
            }
            this.bitmapCache.GetValue(img, createValueCallback).SetImage(image, grayed);
            return image;
        }

        private static ColorMatrix CreateColorMatrix(Color backbround, float grayscaleFactor)
        {
            float num = (1f - grayscaleFactor) / 255f;
            float[] singleArray1 = new float[5];
            singleArray1[0] = grayscaleFactor;
            float[][] newColorMatrix = new float[5][];
            newColorMatrix[0] = singleArray1;
            float[] singleArray2 = new float[5];
            singleArray2[1] = grayscaleFactor;
            newColorMatrix[1] = singleArray2;
            float[] singleArray3 = new float[5];
            singleArray3[2] = grayscaleFactor;
            newColorMatrix[2] = singleArray3;
            float[] singleArray4 = new float[5];
            singleArray4[3] = 1f;
            newColorMatrix[3] = singleArray4;
            float[] singleArray5 = new float[5];
            singleArray5[0] = num * backbround.R;
            singleArray5[1] = num * backbround.G;
            singleArray5[2] = num * backbround.B;
            newColorMatrix[4] = singleArray5;
            return new ColorMatrix(newColorMatrix);
        }

        private static ImageAttributes CreateGrayscaleImageAttributes()
        {
            ColorMatrix newColorMatrix = CreateColorMatrix(DXColor.White, 0.5f);
            ImageAttributes attributes = new ImageAttributes();
            attributes.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Bitmap);
            attributes.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Brush);
            attributes.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Pen);
            attributes.SetColorMatrix(newColorMatrix, ColorMatrixFlag.Default, ColorAdjustType.Text);
            return attributes;
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing && (this.clipRegions != null))
            {
                while (this.clipRegions.Count > 0)
                {
                    this.clipRegions.Pop().Dispose();
                }
            }
            base.Dispose(disposing);
        }

        public override void DrawBrick(PrintingSystemBase ps, VisualBrick brick, Rectangle bounds)
        {
            VisualBrickExporter exporter = (VisualBrickExporter) ExportersFactory.CreateExporter(brick);
            using (GdiGraphics graphics = new RichEditGdiGraphics(this.Graphics, ps))
            {
                exporter.Draw(graphics, bounds, bounds);
            }
        }

        public override void DrawGrayedImage(OfficeImage img, Rectangle bounds)
        {
            if (img.NativeImage is Metafile)
            {
                base.DrawGrayedImage(img, bounds);
            }
            else
            {
                this.DrawImage(img, bounds, ImageSizeMode.Normal, true);
            }
        }

        public override void DrawImage(OfficeImage img, Rectangle bounds)
        {
            this.DrawImage(img, bounds, ImageSizeMode.Normal, false);
        }

        private RectangleF DrawImage(OfficeImage img, Rectangle bounds, ImageSizeMode sizing, bool grayed = false)
        {
            RectangleF ef = SnapToDevicePixelsHelper.GetCorrectedBounds(this.Graphics, img.NativeImage.Size, bounds);
            int width = this.unitConverter.LayoutUnitsToPixels(Math.Max((int) ef.Size.Width, 1));
            System.Drawing.Image image = this.GetImageFromCache(img, new Size(width, this.unitConverter.LayoutUnitsToPixels(Math.Max((int) ef.Size.Height, 1))), sizing, grayed);
            this.DrawImageCore(image, ef, sizing);
            return ef;
        }

        public override void DrawImage(OfficeImage img, Rectangle bounds, Size imgActualSizeInLayoutUnits, ImageSizeMode sizing)
        {
            RectangleF clipBounds = this.Graphics.ClipBounds;
            try
            {
                Rectangle rectangle = Rectangle.Round(ImageTool.CalculateImageRectCore(bounds, (SizeF) imgActualSizeInLayoutUnits, sizing));
                Rectangle rectangle2 = Rectangle.Intersect(bounds, Rectangle.Round(clipBounds));
                rectangle2 = Rectangle.Round(SnapToDevicePixelsHelper.GetCorrectedBounds(this.Graphics, img.NativeImage.Size, rectangle2));
                this.Graphics.SetClip(rectangle2);
                this.DrawImage(img, rectangle, sizing, false);
            }
            finally
            {
                this.Graphics.SetClip(clipBounds);
            }
        }

        protected virtual void DrawImageCore(System.Drawing.Image img, RectangleF bounds, ImageSizeMode sizing)
        {
            this.Graphics.DrawImage(img, bounds);
        }

        public override void DrawImageWithAdorner(OfficeImage img, Rectangle bounds, Action<RectangleF> drawAdorner)
        {
            RectangleF ef = this.DrawImage(img, bounds, ImageSizeMode.Normal, false);
            if (drawAdorner != null)
            {
                drawAdorner(ef);
            }
        }

        public override void DrawLine(Pen pen, float x1, float y1, float x2, float y2)
        {
            this.Graphics.DrawLine(pen, x1, y1, x2, y2);
        }

        public override void DrawLines(Pen pen, PointF[] points)
        {
            this.Graphics.DrawLines(pen, points);
        }

        public override void DrawRectangle(Pen pen, Rectangle bounds)
        {
            this.cache.DrawRectangle(pen, bounds);
        }

        public override void DrawString(string text, FontInfo fontInfo, Rectangle bounds)
        {
            this.DrawString(text, fontInfo, bounds, base.StringFormat, null);
        }

        public override void DrawString(string text, FontInfo fontInfo, Rectangle bounds, StringFormat stringFormat, IWordBreakProvider workBreakProvider)
        {
            Brush solidBrush = this.cache.GetSolidBrush(base.TextForeColor);
            this.Graphics.DrawString(text, fontInfo.Font, solidBrush, this.CorrectTextDrawingBounds(fontInfo, bounds), base.StringFormat);
        }

        public override void DrawString(string text, Brush brush, Font font, float x, float y)
        {
            this.Graphics.DrawString(text, font, brush, new Rectangle((int) x, (int) y, 0x7fffffff, 0x7fffffff), base.StringFormat);
        }

        public override void ExcludeCellBounds(Rectangle rect, Rectangle rowBounds)
        {
            this.Graphics.ExcludeClip(rect);
        }

        public override void FillEllipse(Brush brush, Rectangle bounds)
        {
            this.Graphics.FillEllipse(brush, bounds);
        }

        public override void FillPolygon(Brush brush, PointF[] points)
        {
            SmoothingMode smoothingMode = this.Graphics.SmoothingMode;
            this.Graphics.SmoothingMode = SmoothingMode.AntiAlias;
            this.Graphics.FillPolygon(brush, points);
            this.Graphics.SmoothingMode = smoothingMode;
        }

        public override void FillRectangle(Brush brush, Rectangle actualBounds)
        {
            this.cache.FillRectangle(brush, actualBounds);
        }

        public override void FillRectangle(Brush brush, RectangleF bounds)
        {
            this.cache.Graphics.FillRectangle(brush, bounds);
        }

        public override void FillRectangle(Color color, Rectangle actualBounds)
        {
            this.cache.FillRectangle(color, actualBounds);
        }

        public override void FillRectangle(Color color, RectangleF bounds)
        {
            Brush solidBrush = this.cache.GetSolidBrush(color);
            this.FillRectangle(solidBrush, bounds);
        }

        public override Brush GetBrush(Color color) => 
            this.cache.GetSolidBrush(color);

        private System.Drawing.Image GetImageFromCache(OfficeImage img, Size actualSize, ImageSizeMode sizing, bool grayed = false)
        {
            System.Drawing.Image image;
            ImageCacheData data;
            if ((this.bitmapCache == null) || ((img.NativeImage is Metafile) || (sizing == ImageSizeMode.Tile)))
            {
                return img.NativeImage;
            }
            if (!grayed && !this.ShouldCacheImage(img.NativeImage.Size, actualSize))
            {
                return img.NativeImage;
            }
            if (!this.bitmapCache.TryGetValue(img, out data))
            {
                image = this.AddBitmapToCache(img, actualSize, grayed);
            }
            else
            {
                image = data.GetImage(grayed);
                if (image == null)
                {
                    image = this.AddBitmapToCache(img, actualSize, grayed);
                }
                else if ((image.Size.Width != actualSize.Width) || (image.Size.Height != actualSize.Height))
                {
                    image.Dispose();
                    image = this.AddBitmapToCache(img, actualSize, grayed);
                }
            }
            return image;
        }

        public override Pen GetPen(Color color) => 
            this.cache.GetPen(color);

        public override Pen GetPen(Color color, float thickness) => 
            this.cache.GetPen(color, (int) thickness);

        public override SizeF MeasureString(string text, Font font) => 
            this.Graphics.MeasureString(text, font, 0x7fffffff, base.StringFormat);

        public override void PopPixelOffsetMode()
        {
            this.Graphics.PixelOffsetMode = this.pixelmodeStack.Pop();
        }

        public override void PopSmoothingMode()
        {
            this.Graphics.SmoothingMode = this.smoothingmodeStack.Pop();
        }

        public override void PopTransform()
        {
            using (Region region = this.clipRegions.Pop())
            {
                this.Graphics.SetClip(region, CombineMode.Replace);
            }
            this.Graphics.Transform = this.transformsStack.Pop();
            this.SetClipBounds(this.rectangularBounds.Pop());
        }

        public override void PushPixelOffsetMode(bool highQualtity)
        {
            this.pixelmodeStack.Push(this.Graphics.PixelOffsetMode);
            this.Graphics.PixelOffsetMode = highQualtity ? PixelOffsetMode.HighQuality : PixelOffsetMode.Default;
        }

        public override void PushRotationTransform(Point center, float angleInDegrees)
        {
            this.transformsStack.Push(this.Graphics.Transform.Clone());
            this.rectangularBounds.Push(this.rectangularClipBounds);
            Matrix transform = this.Graphics.Transform;
            transform.RotateAt(angleInDegrees, (PointF) center);
            this.Graphics.Transform = transform;
            this.clipRegions.Push(this.Graphics.Clip);
            this.rectangularClipBounds = this.Graphics.ClipBounds;
        }

        public override void PushSmoothingMode(bool highQuality)
        {
            this.smoothingmodeStack.Push(this.Graphics.SmoothingMode);
            this.Graphics.SmoothingMode = highQuality ? SmoothingMode.HighQuality : SmoothingMode.Default;
        }

        public override bool PushTransform(Matrix transformMatrix)
        {
            this.transformsStack.Push(this.Graphics.Transform.Clone());
            this.rectangularBounds.Push(this.rectangularClipBounds);
            Matrix transform = this.Graphics.Transform;
            transform.Multiply(transformMatrix);
            this.Graphics.Transform = transform;
            this.clipRegions.Push(this.Graphics.Clip);
            this.rectangularClipBounds = this.Graphics.ClipBounds;
            return true;
        }

        public override void ReleaseBrush(Brush brush)
        {
        }

        public override void ReleasePen(Pen pen)
        {
        }

        public override void ResetCellBoundsClip()
        {
        }

        protected override void SetClipBounds(RectangleF bounds)
        {
            if (this.clipRegions.Count <= 0)
            {
                this.Graphics.SetClip(bounds);
                this.rectangularClipBounds = this.Graphics.ClipBounds;
            }
            else
            {
                using (Region region = this.clipRegions.Peek().Clone())
                {
                    region.Intersect(bounds);
                    this.Graphics.SetClip(region, CombineMode.Replace);
                }
                this.rectangularClipBounds = bounds;
            }
        }

        internal bool ShouldCacheImage(Size nativeImageSize, Size actualSize)
        {
            int num = actualSize.Width * actualSize.Height;
            return ((num >= 0) && ((num <= 0x249f00) && ((nativeImageSize.Width >= 0x7d0) || ((nativeImageSize.Height >= 0x7d0) || ((actualSize.Width < nativeImageSize.Width) && (actualSize.Height < nativeImageSize.Height))))));
        }

        public override void SnapHeights(float[] heights)
        {
            if (this.transformsStack.Count == 0)
            {
                base.SnapHeights(heights);
            }
        }

        public override void SnapWidths(float[] widths)
        {
            if (this.transformsStack.Count == 0)
            {
                base.SnapWidths(widths);
            }
        }

        protected override PointF[] TransformToLayoutUnits(PointF[] points)
        {
            this.Graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, points);
            return points;
        }

        protected override PointF[] TransformToPixels(PointF[] points)
        {
            this.Graphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.World, points);
            return points;
        }

        public IGraphicsCache Cache =>
            this.cache;

        public System.Drawing.Graphics Graphics =>
            this.cache.Graphics;

        public override int DpiY =>
            this.dpiY;

        protected override RectangleF RectangularClipBounds =>
            this.rectangularClipBounds;

        public bool HasTransform =>
            this.transformsStack.Count > 0;

        public ConditionalWeakTable<OfficeImage, ImageCacheData> BitmapCache
        {
            get => 
                this.bitmapCache;
            set => 
                this.bitmapCache = value;
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly GdiPlusPainter.<>c <>9 = new GdiPlusPainter.<>c();
            public static ConditionalWeakTable<OfficeImage, ImageCacheData>.CreateValueCallback <>9__51_0;

            internal ImageCacheData <AddBitmapToCache>b__51_0(OfficeImage key) => 
                new ImageCacheData();
        }

        private class RichEditGdiGraphics : GdiGraphics, IPixelAdjuster
        {
            public RichEditGdiGraphics(Graphics gr, PrintingSystemBase ps) : base(gr, ps)
            {
            }

            public RectangleF AdjustRect(RectangleF bounds)
            {
                PointF[] pts = new PointF[] { new PointF(0f, 0f), new PointF(bounds.Left, bounds.Top), new PointF(bounds.Width, bounds.Height) };
                base.Graphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.World, pts);
                pts[0] = new PointF((float) ((int) (pts[0].X + 0.5)), (float) ((int) (pts[0].Y + 0.5)));
                pts[1] = new PointF((float) ((int) (pts[1].X + 0.5)), (float) ((int) (pts[1].Y + 0.5)));
                pts[2] = new PointF((float) ((int) (pts[2].X + 0.5)), (float) ((int) (pts[2].Y + 0.5)));
                base.Graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, pts);
                return new RectangleF(pts[1].X, pts[1].Y, pts[2].X - pts[0].X, pts[2].Y - pts[0].Y);
            }

            public SizeF AdjustSize(SizeF size)
            {
                PointF[] pts = new PointF[] { new PointF(0f, 0f), new PointF(size.Width, size.Height) };
                base.Graphics.TransformPoints(CoordinateSpace.Device, CoordinateSpace.World, pts);
                pts[1] = new PointF((float) ((int) ((pts[1].X - pts[0].X) + 0.5)), (float) ((int) ((pts[1].Y - pts[0].Y) + 0.5)));
                pts[0] = new PointF(0f, 0f);
                if ((size.Width != 0f) && (pts[1].X == 0f))
                {
                    pts[1].X = Math.Sign(size.Width);
                }
                if ((size.Height != 0f) && (pts[1].Y == 0f))
                {
                    pts[1].Y = Math.Sign(size.Height);
                }
                base.Graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, pts);
                return new SizeF(pts[1].X - pts[0].X, pts[1].Y - pts[0].Y);
            }

            public SizeF GetDevicePointSize()
            {
                PointF[] pts = new PointF[] { new PointF(0f, 0f), new PointF(1f, 1f) };
                base.Graphics.TransformPoints(CoordinateSpace.World, CoordinateSpace.Device, pts);
                return new SizeF(pts[1].X - pts[0].X, pts[1].Y - pts[0].Y);
            }

            protected override void SetPageUnit()
            {
            }
        }
    }
}

