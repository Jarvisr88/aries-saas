namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.PInvoke;
    using DevExpress.Office.Utils;
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Drawing.Imaging;
    using System.Drawing.Text;
    using System.Reflection;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Security;
    using System.Threading.Tasks;

    public static class GdipExtensions
    {
        private const int SRCCOPY = 0xcc0020;

        internal static bool AllowPathWidening(GraphicsPath graphicsPath)
        {
            int pointCount = graphicsPath.PointCount;
            if (pointCount >= 2)
            {
                PointF[] pathPoints = graphicsPath.PathPoints;
                PointF tf = pathPoints[0];
                for (int i = 1; i < pointCount; i++)
                {
                    PointF tf2 = pathPoints[i];
                    if (tf != tf2)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        internal static Matrix ApplyTransform(this Brush brush, Matrix transform, MatrixOrder order) => 
            ((brush == null) || (transform == null)) ? null : ApplyTransformCore(brush, transform, order);

        internal static Matrix ApplyTransform(this Pen pen, Matrix transform, MatrixOrder order)
        {
            Matrix matrix = null;
            Brush brush = pen.Brush;
            if ((brush != null) && (transform != null))
            {
                matrix = ApplyTransformCore(brush, transform, order);
                if (OSHelper.IsWindows)
                {
                    pen.Brush = brush;
                }
            }
            return matrix;
        }

        private static Matrix ApplyTransformCore(Brush brush, Matrix transform, MatrixOrder order)
        {
            Matrix matrix = null;
            if (brush is LinearGradientBrush)
            {
                LinearGradientBrush brush2 = (LinearGradientBrush) brush;
                matrix = brush2.Transform;
                brush2.MultiplyTransform(transform, order);
            }
            else if (brush is PathGradientBrush)
            {
                PathGradientBrush brush3 = (PathGradientBrush) brush;
                matrix = brush3.Transform;
                brush3.MultiplyTransform(transform, order);
            }
            else if (brush is TextureBrush)
            {
                TextureBrush brush4 = (TextureBrush) brush;
                matrix = brush4.Transform;
                brush4.MultiplyTransform(transform, order);
            }
            return matrix;
        }

        [DllImport("gdi32.dll")]
        private static extern bool BitBlt(IntPtr hDC, int x, int y, int nWidth, int nHeight, IntPtr hSrcDC, int xSrc, int ySrc, int dwRop);
        internal static GraphicsPath BuildFigure(List<GraphicsPath> graphicsPaths, bool addNotClosedPaths)
        {
            GraphicsPath path = new GraphicsPath(FillMode.Alternate);
            if (graphicsPaths != null)
            {
                if (graphicsPaths.Count > 0)
                {
                    GraphicsPath addingPath = graphicsPaths[0];
                    if (addingPath.PointCount > 0)
                    {
                        path.AddPath(addingPath, false);
                    }
                }
                for (int i = 1; i < graphicsPaths.Count; i++)
                {
                    GraphicsPath graphicsPath = graphicsPaths[i];
                    if (addNotClosedPaths || graphicsPath.IsClosed())
                    {
                        path.AddPath(graphicsPath, false);
                    }
                }
            }
            return path;
        }

        [SecuritySafeCritical]
        public static Metafile ConvertToEmfPlus(Graphics graphics, Metafile metafile)
        {
            Metafile metafile2;
            if (!OSHelper.IsWindows)
            {
                return null;
            }
            IntPtr zero = IntPtr.Zero;
            try
            {
                if (!CreateNativeGraphics(graphics, out zero))
                {
                    metafile2 = null;
                }
                else
                {
                    IntPtr ptr2;
                    SetupNativeGraphics(zero);
                    metafile2 = GetNativeMetafile(metafile, out ptr2) ? ConvertToEmfPlusCore(zero, ptr2) : null;
                }
            }
            finally
            {
                DeleteNativeGraphics(zero);
            }
            return metafile2;
        }

        [SecuritySafeCritical]
        private static Metafile ConvertToEmfPlusCore(IntPtr nativeGraphics, IntPtr nativeMetafile)
        {
            IntPtr ptr;
            Metafile metafile;
            if (!ConvertToEmfPlusWrapper(nativeGraphics, nativeMetafile, out ptr))
            {
                return null;
            }
            try
            {
                IntPtr ptr2;
                if (PInvokeSafeNativeMethods.GdipGetHemfFromMetafile(ptr, out ptr2) == 0)
                {
                    metafile = new Metafile(ptr2, true);
                }
                else
                {
                    MetafileHelper.DeleteMetafileHandle(ptr2);
                    metafile = null;
                }
            }
            finally
            {
                DisposeImage(ptr);
            }
            return metafile;
        }

        [SecuritySafeCritical]
        private static bool ConvertToEmfPlusWrapper(IntPtr nativeGraphics, IntPtr nativeMetafile, out IntPtr emfPlus)
        {
            bool flag2;
            emfPlus = IntPtr.Zero;
            try
            {
                bool flag;
                if (!((PInvokeSafeNativeMethods.GdipConvertToEmfPlus(nativeGraphics, nativeMetafile, out flag, EmfType.EmfPlusOnly, string.Empty, out emfPlus) != 0) | flag))
                {
                    return true;
                }
                else
                {
                    flag2 = false;
                }
            }
            catch
            {
                flag2 = false;
            }
            return flag2;
        }

        [SecuritySafeCritical]
        internal static bool CreateNativeGraphics(Graphics graphics, out IntPtr nativeGraphics)
        {
            bool flag;
            IntPtr hdc = graphics.GetHdc();
            nativeGraphics = IntPtr.Zero;
            try
            {
                flag = PInvokeSafeNativeMethods.GdipCreateFromHDC(hdc, out nativeGraphics) == 0;
            }
            finally
            {
                graphics.ReleaseHdc(hdc);
            }
            return flag;
        }

        [SecuritySafeCritical]
        internal static void DeleteNativeGraphics(IntPtr nativeGraphics)
        {
            DisposeNativeCore(nativeGraphics, new Func<IntPtr, int>(PInvokeSafeNativeMethods.GdipDeleteGraphics));
        }

        [SecuritySafeCritical]
        internal static void DisposeImage(IntPtr nativeImage)
        {
            DisposeNativeCore(nativeImage, new Func<IntPtr, int>(PInvokeSafeNativeMethods.GdipDisposeImage));
        }

        private static void DisposeNativeCore(IntPtr pointer, Func<IntPtr, int> action)
        {
            if (pointer != IntPtr.Zero)
            {
                try
                {
                    action(pointer);
                }
                catch
                {
                }
            }
        }

        public static Rectangle GetBoundsExt(this GraphicsPath graphicsPath) => 
            graphicsPath.GetBoundsExt(null);

        public static Rectangle GetBoundsExt(this GraphicsPath graphicsPath, Matrix transform) => 
            Rectangle.Round(graphicsPath.GetBoundsExtF(transform));

        private static RectangleF GetBoundsExtCore(GraphicsPath graphicsPath) => 
            graphicsPath.GetBounds();

        public static RectangleF GetBoundsExtF(this GraphicsPath graphicsPath, Matrix transform) => 
            graphicsPath.GetBoundsExtF(transform, null);

        public static RectangleF GetBoundsExtF(this GraphicsPath graphicsPath, Matrix transform, PenInfo penInfo)
        {
            using (GraphicsPath path = (GraphicsPath) graphicsPath.Clone())
            {
                if (OSHelper.IsWindows)
                {
                    path.Flatten(null, 0.25f);
                }
                Pen pen = penInfo?.Pen;
                if ((pen == null) || !AllowPathWidening(path))
                {
                    if (transform != null)
                    {
                        path.Transform(transform);
                    }
                }
                else if (pen.Alignment != PenAlignment.Outset)
                {
                    WidenHelper.Apply(path, penInfo, false, transform);
                }
                else
                {
                    return GetBoundsExtForOutsetPen(path, penInfo, transform);
                }
                return GetBoundsExtCore(path);
            }
        }

        public static RectangleF GetBoundsExtF(this GraphicsPath graphicsPath, Matrix transform, PenInfo penInfo, bool correctLineJoin)
        {
            using (GraphicsPath path = (GraphicsPath) graphicsPath.Clone())
            {
                Pen pen = penInfo?.Pen;
                if ((pen == null) || !AllowPathWidening(path))
                {
                    if (transform != null)
                    {
                        path.Transform(transform);
                    }
                }
                else
                {
                    LineJoin miter = LineJoin.Miter;
                    if (correctLineJoin)
                    {
                        miter = pen.LineJoin;
                        pen.LineJoin = LineJoin.Round;
                    }
                    try
                    {
                        if (pen.Alignment != PenAlignment.Outset)
                        {
                            WidenHelper.Apply(path, penInfo, false, transform);
                        }
                        else
                        {
                            return GetBoundsExtForOutsetPen(path, penInfo, transform);
                        }
                    }
                    finally
                    {
                        if (correctLineJoin)
                        {
                            pen.LineJoin = miter;
                        }
                    }
                }
                return GetBoundsExtCore(path);
            }
        }

        private static RectangleF GetBoundsExtForOutsetPen(GraphicsPath path, PenInfo penInfo, Matrix transform)
        {
            using (GraphicsPath path2 = TransformGraphicsPathForOutsetPen(path, penInfo.Pen))
            {
                WidenHelper.Apply(path2, penInfo, false, transform);
                return GetBoundsExtCore(path2);
            }
        }

        [SecuritySafeCritical]
        public static Bitmap GetGraphicsSnapshot(Graphics graphics, Rectangle bounds)
        {
            Bitmap bitmap2;
            IntPtr hdc = graphics.GetHdc();
            try
            {
                Bitmap image = new Bitmap(bounds.Width, bounds.Height);
                using (Graphics graphics2 = Graphics.FromImage(image))
                {
                    IntPtr hDC = graphics2.GetHdc();
                    try
                    {
                        bool flag = BitBlt(hDC, 0, 0, bounds.Width, bounds.Height, hdc, bounds.X, bounds.Y, 0xcc0020);
                    }
                    finally
                    {
                        graphics2.ReleaseHdc(hDC);
                    }
                }
                bitmap2 = image;
            }
            finally
            {
                graphics.ReleaseHdc(hdc);
            }
            return bitmap2;
        }

        public static RectangleF GetImageBounds(this Image image)
        {
            GraphicsUnit pixel = GraphicsUnit.Pixel;
            return image.GetBounds(ref pixel);
        }

        private static bool GetNativeMetafile(Metafile sourceMetafile, out IntPtr nativeMetafile)
        {
            FieldInfo field = typeof(Metafile).GetField("nativeImage", BindingFlags.NonPublic | BindingFlags.Instance);
            object local1 = field?.GetValue(sourceMetafile);
            object zero = local1;
            if (local1 == null)
            {
                object local2 = local1;
                zero = IntPtr.Zero;
            }
            nativeMetafile = (IntPtr) zero;
            return ((field != null) && (nativeMetafile != IntPtr.Zero));
        }

        public static bool IsClosed(this GraphicsPath graphicsPath)
        {
            if (graphicsPath.PointCount != 0)
            {
                foreach (byte num2 in graphicsPath.PathData.Types)
                {
                    if ((num2 & 0x80) != 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        public static void IterateBitmapBytes(int size, Action<int> action)
        {
            IterateBitmapBytes(size, 4, action);
        }

        public static void IterateBitmapBytes(int size, int channelsCount, Action<int> action)
        {
            Parallel.For(0, size / channelsCount, (Action<int>) (i => action(i * channelsCount)));
        }

        public static Graphics PrepareGraphicsFromImage(Image image) => 
            PrepareGraphicsFromImage(image, GraphicsUnit.Pixel, 1f);

        public static Graphics PrepareGraphicsFromImage(Image image, GraphicsUnit pageUnit, float pageScale)
        {
            Graphics graphics = Graphics.FromImage(image);
            graphics.PageUnit = pageUnit;
            graphics.PageScale = pageScale;
            graphics.SmoothingMode = SmoothingMode.AntiAlias;
            graphics.CompositingMode = CompositingMode.SourceOver;
            graphics.CompositingQuality = CompositingQuality.Default;
            graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
            return graphics;
        }

        internal static void ReplaceTransform(this Brush brush, Matrix transform)
        {
            if ((brush != null) && (transform != null))
            {
                ReplaceTransformCore(brush, transform);
            }
        }

        internal static void ReplaceTransform(this Pen pen, Matrix transform)
        {
            Brush brush = pen.Brush;
            if ((brush != null) && (transform != null))
            {
                ReplaceTransformCore(brush, transform);
                if (OSHelper.IsWindows)
                {
                    pen.Brush = brush;
                }
            }
        }

        private static void ReplaceTransformCore(Brush brush, Matrix transform)
        {
            if (brush is LinearGradientBrush)
            {
                ((LinearGradientBrush) brush).Transform = transform;
            }
            else if (brush is PathGradientBrush)
            {
                ((PathGradientBrush) brush).Transform = transform;
            }
            else if (brush is TextureBrush)
            {
                ((TextureBrush) brush).Transform = transform;
            }
        }

        [SecuritySafeCritical]
        private static void SetupNativeGraphics(IntPtr nativeGraphics)
        {
            PInvokeSafeNativeMethods.GdipSetSmoothingMode(nativeGraphics, SmoothingMode.AntiAlias);
            PInvokeSafeNativeMethods.GdipSetTextRenderingHint(nativeGraphics, TextRenderingHint.AntiAlias);
        }

        public static GraphicsPath TransformGraphicsPathForOutsetPen(GraphicsPath graphicsPath, Pen pen)
        {
            GraphicsPath path = (GraphicsPath) graphicsPath.Clone();
            RectangleF boundsExt = path.GetBoundsExt();
            if ((boundsExt.Width != 0f) && (boundsExt.Height != 0f))
            {
                float width = pen.Width;
                float num2 = boundsExt.Width + width;
                float num3 = boundsExt.Height + width;
                float num4 = num2 / boundsExt.Width;
                float num5 = num3 / boundsExt.Height;
                using (Matrix matrix = new Matrix(num4, 0f, 0f, num5, (boundsExt.X - (boundsExt.X * num4)) - ((num2 - boundsExt.Width) / 2f), (boundsExt.Y - (boundsExt.Y * num5)) - ((num3 - boundsExt.Height) / 2f)))
                {
                    path.Transform(matrix);
                }
            }
            return path;
        }

        public static void Translate(this GraphicsPath graphicsPath, float offsetX, float offsetY)
        {
            using (Matrix matrix = new Matrix(1f, 0f, 0f, 1f, offsetX, offsetY))
            {
                graphicsPath.Transform(matrix);
            }
        }
    }
}

