namespace DevExpress.Xpf.Printing.Native
{
    using DevExpress.Emf;
    using DevExpress.Pdf;
    using DevExpress.Pdf.ContentGeneration;
    using DevExpress.Text.Fonts;
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;
    using System.Windows;
    using System.Windows.Media;

    public static class PdfExportExtensions
    {
        public static System.Windows.Point ActualOffset(this PdfGraphicsCommandConstructor constructor) => 
            new System.Windows.Point(constructor.CommandConstructor.CurrentTransformationMatrix.E, -constructor.CommandConstructor.CurrentTransformationMatrix.F);

        public static void DoWithState(this PdfGraphicsCommandConstructor constructor, Action action, System.Windows.Media.Transform transform = null, Geometry clip = null)
        {
            bool flag = (transform != null) && !transform.Value.IsIdentity;
            bool flag2 = clip != null;
            if (flag | flag2)
            {
                constructor.SaveGraphicsState();
            }
            if (flag)
            {
                constructor.UpdateTransformationMatrix(transform.Value.ToPdfMatrix());
            }
            if (flag2)
            {
                if (clip is RectangleGeometry)
                {
                    constructor.IntersectClip(((RectangleGeometry) clip).Rect.ToRectangleF());
                }
                else
                {
                    PdfGeometry geometry = new PdfGeometry(clip, false, false, true);
                    constructor.IntersectClip(geometry.Points, geometry.PointTypes, geometry.NonZero);
                }
            }
            action();
            if (flag | flag2)
            {
                constructor.RestoreGraphicsState();
            }
        }

        private static System.Windows.Point GetPoint(Rect rect, System.Windows.Point offset) => 
            new System.Windows.Point(rect.Width * offset.X, rect.Height * offset.Y);

        public static System.Windows.Media.Matrix GetTransform(this Visual visual)
        {
            System.Windows.Media.Transform transform = VisualTreeHelper.GetTransform(visual);
            return ((transform != null) ? transform.Value : System.Windows.Media.Matrix.Identity);
        }

        public static double ScaleToDpi(this double value, double dpi) => 
            (value * 72.0) / dpi;

        public static ARGBColor ToColor(this System.Windows.Media.Color color) => 
            ARGBColor.FromArgb(color.A, color.R, color.G, color.B);

        public static DXDashCap ToDashCap(this PenLineCap sourceCap)
        {
            switch (sourceCap)
            {
                case PenLineCap.Square:
                    return DXDashCap.Square;

                case PenLineCap.Round:
                    return DXDashCap.Round;

                case PenLineCap.Triangle:
                    return DXDashCap.Triangle;
            }
            return DXDashCap.Flat;
        }

        public static DXTransformationMatrix ToDXMatrix(this System.Drawing.Drawing2D.Matrix matrix) => 
            new DXTransformationMatrix(matrix.Elements[0], matrix.Elements[1], matrix.Elements[2], matrix.Elements[3], matrix.Elements[4], matrix.Elements[5]);

        public static DXTransformationMatrix ToDXMatrix(this System.Windows.Media.Matrix matrix) => 
            new DXTransformationMatrix((float) matrix.M11, (float) matrix.M12, (float) matrix.M21, (float) matrix.M22, (float) matrix.OffsetX, (float) matrix.OffsetY);

        public static DXRectangleF ToDxRectangleF(this RectangleF rect) => 
            new DXRectangleF(rect.Left, rect.Top, rect.Width, rect.Height);

        public static DXRectangleF ToDxRectangleF(this Rect rect) => 
            new DXRectangleF((float) rect.Left, (float) rect.Top, (float) rect.Width, (float) rect.Height);

        public static DXFontStretch ToPdfFontStretch(this FontStretch fontStretch) => 
            (DXFontStretch) fontStretch.ToOpenTypeStretch();

        public static DevExpress.Text.Fonts.DXFontStyle ToPdfFontStyle(this System.Windows.FontStyle fontStyle) => 
            (DevExpress.Text.Fonts.DXFontStyle) fontStyle.GetHashCode();

        public static DXFontWeight ToPdfFontWeight(this FontWeight fontWeight) => 
            (DXFontWeight) fontWeight.ToOpenTypeWeight();

        public static PdfTransformationMatrix ToPdfMatrix(this System.Windows.Media.Matrix matrix) => 
            new PdfTransformationMatrix(matrix.M11, matrix.M12, matrix.M21, matrix.M22, matrix.OffsetX, matrix.OffsetY);

        public static DXPen ToPen(this System.Windows.Media.Pen sourcePen, DXBrush brush)
        {
            DXPen pen = new DXPen(brush, (double) ((float) sourcePen.Thickness)) {
                MiterLimit = (float) sourcePen.MiterLimit,
                LineJoin = (DXLineJoin) sourcePen.LineJoin,
                StartCap = (DXLineCap) sourcePen.StartLineCap,
                EndCap = (DXLineCap) sourcePen.EndLineCap,
                DashCap = sourcePen.DashCap.ToDashCap()
            };
            if ((sourcePen.DashStyle != null) && ((sourcePen.DashStyle.Dashes != null) && (sourcePen.DashStyle.Dashes.Count > 0)))
            {
                pen.DashStyle = DXDashStyle.Custom;
                pen.DashOffset = (float) sourcePen.DashStyle.Offset;
                Func<double, float> selector = <>c.<>9__12_0;
                if (<>c.<>9__12_0 == null)
                {
                    Func<double, float> local1 = <>c.<>9__12_0;
                    selector = <>c.<>9__12_0 = x => (float) x;
                }
                pen.DashPattern = sourcePen.DashStyle.Dashes.Select<double, float>(selector).ToArray<float>();
            }
            return pen;
        }

        public static PointF ToPointF(this System.Windows.Point point) => 
            new PointF((float) point.X, (float) point.Y);

        public static RectangleF ToRectangleF(this Rect rect) => 
            new RectangleF((float) rect.Left, (float) rect.Top, (float) rect.Width, (float) rect.Height);

        public static SizeF ToSizeF(this System.Windows.Size size) => 
            new SizeF((float) size.Width, (float) size.Height);

        public static System.Windows.Media.Matrix ToWpfMatrix(this DXTransformationMatrix matrix) => 
            new System.Windows.Media.Matrix((double) matrix.A, (double) matrix.B, (double) matrix.C, (double) matrix.D, (double) matrix.E, (double) matrix.F);

        public static DXWrapMode ToWrapMode(this GradientSpreadMethod spreadMethod) => 
            (spreadMethod == GradientSpreadMethod.Reflect) ? DXWrapMode.TileFlipXY : ((spreadMethod == GradientSpreadMethod.Repeat) ? DXWrapMode.Tile : DXWrapMode.Clamp);

        public static DXWrapMode ToWrapMode(this TileMode tileMode)
        {
            switch (tileMode)
            {
                case TileMode.None:
                    return DXWrapMode.Clamp;

                case TileMode.FlipX:
                    return DXWrapMode.TileFlipX;

                case TileMode.FlipY:
                    return DXWrapMode.TileFlipY;

                case TileMode.FlipXY:
                    return DXWrapMode.TileFlipXY;

                case TileMode.Tile:
                    return DXWrapMode.Tile;
            }
            throw new ArgumentException("Unknown tile mode");
        }

        public static System.Windows.Point Transform(this System.Windows.Point sourcePoint, System.Windows.Media.Matrix transform) => 
            transform.IsIdentity ? sourcePoint : transform.Transform(sourcePoint);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly PdfExportExtensions.<>c <>9 = new PdfExportExtensions.<>c();
            public static Func<double, float> <>9__12_0;

            internal float <ToPen>b__12_0(double x) => 
                (float) x;
        }
    }
}

