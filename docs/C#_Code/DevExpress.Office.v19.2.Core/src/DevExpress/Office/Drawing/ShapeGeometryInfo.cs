namespace DevExpress.Office.Drawing
{
    using System;
    using System.Windows;
    using System.Windows.Media;

    public class ShapeGeometryInfo : IShapeGeometryInfo
    {
        private readonly System.Windows.Media.Geometry geometry;

        public ShapeGeometryInfo(System.Windows.Media.Geometry geometry)
        {
            this.geometry = geometry;
        }

        private Brush ChooseGlowBrush(Brush parentBrush, Color glowColor)
        {
            if (parentBrush is GradientBrush)
            {
                Brush brush2 = this.ConvertGradientBrushWithTransparentColors((GradientBrush) parentBrush, glowColor);
                if (brush2 != null)
                {
                    return brush2;
                }
            }
            SolidColorBrush brush = new SolidColorBrush(glowColor);
            brush.Freeze();
            return brush;
        }

        private GradientBrush ConvertGradientBrushWithTransparentColors(GradientBrush brush, Color glowColor)
        {
            System.Windows.Media.GradientStopCollection gradientStops = brush.GradientStops;
            System.Windows.Media.GradientStopCollection stops2 = new System.Windows.Media.GradientStopCollection();
            bool flag = false;
            bool flag2 = false;
            bool flag3 = false;
            for (int i = 0; i < gradientStops.Count; i++)
            {
                GradientStop stop = gradientStops[i];
                Color color = stop.Color;
                double offset = stop.Offset;
                flag2 = color.A == 0;
                if (flag2)
                {
                    flag3 = true;
                }
                if (!flag2 & flag)
                {
                    stops2.Add(new GradientStop(glowColor, gradientStops[i - 1].Offset));
                }
                else if ((!flag & flag2) && (i != 0))
                {
                    stops2.Add(new GradientStop(glowColor, offset));
                }
                stops2.Add(new GradientStop(flag2 ? color : glowColor, offset));
                flag = flag2;
            }
            GradientBrush brush2 = null;
            if (flag3)
            {
                brush2 = brush.Clone();
                brush2.GradientStops = stops2;
                brush2.Freeze();
            }
            return brush2;
        }

        protected Pen CreateGlowPen(Color glowColor, Brush parentBrush, double blurRadius)
        {
            Pen pen = this.CreateGlowPenCore(glowColor, parentBrush, blurRadius);
            this.PrepareLineCapsAndFreeze(pen);
            return pen;
        }

        private Pen CreateGlowPen(Color glowColor, Pen originalPen, double blurRadius)
        {
            double thickness = originalPen.Thickness;
            Pen pen = this.CreateGlowPenCore(glowColor, originalPen.Brush, thickness);
            pen.DashCap = originalPen.DashCap;
            pen.DashStyle = this.GetDashStyle(originalPen.DashStyle.Clone(), thickness, blurRadius);
            pen.StartLineCap = this.GetCorrectedPenLineCap(originalPen.StartLineCap);
            pen.EndLineCap = this.GetCorrectedPenLineCap(originalPen.EndLineCap);
            pen.Thickness += blurRadius;
            if (pen.CanFreeze)
            {
                pen.Freeze();
            }
            return pen;
        }

        private Pen CreateGlowPenCore(Color glowColor, Brush parentBrush, double thickness) => 
            new Pen(this.ChooseGlowBrush(parentBrush, glowColor), thickness) { LineJoin = PenLineJoin.Round };

        public virtual void Draw(DrawingContext dc, Pen pen, Brush brush)
        {
            dc.DrawGeometry(brush, pen, this.geometry);
        }

        public virtual void DrawGlow(DrawingContext dc, Pen parentOutlinePen, Brush parentFillBrush, Color glowColor, double blurRadius)
        {
            if (parentOutlinePen != null)
            {
                Pen pen = this.CreateGlowPen(glowColor, parentOutlinePen, blurRadius);
                dc.DrawGeometry(null, pen, this.geometry);
            }
            if (parentFillBrush != null)
            {
                Pen pen = this.CreateGlowPen(glowColor, parentFillBrush, blurRadius);
                dc.DrawGeometry(pen.Brush, pen, this.geometry.GetOutlinedPathGeometry());
            }
        }

        private PenLineCap GetCorrectedPenLineCap(PenLineCap originalLineCap) => 
            (originalLineCap != PenLineCap.Square) ? PenLineCap.Round : originalLineCap;

        private DashStyle GetDashStyle(DashStyle dashStyle, double oldThickness, double blurRadius)
        {
            DoubleCollection dashes = dashStyle.Dashes;
            int count = dashes.Count;
            if (count > 0)
            {
                double num2 = oldThickness / (oldThickness + blurRadius);
                int num3 = 0;
                while (true)
                {
                    if (num3 >= count)
                    {
                        dashStyle.Offset *= num2;
                        break;
                    }
                    DoubleCollection doubles2 = dashes;
                    int num4 = num3;
                    doubles2[num4] *= num2;
                    num3++;
                }
            }
            return dashStyle;
        }

        public virtual Rect GetTransformedBounds(Matrix transform) => 
            this.GetTransformedGeometryBounds(this.geometry.Clone(), transform);

        protected Rect GetTransformedGeometryBounds(System.Windows.Media.Geometry geometry, Matrix transform)
        {
            geometry.Transform = new MatrixTransform(transform);
            return geometry.Bounds;
        }

        public virtual Rect GetTransformedWidenedBounds(Pen pen, Matrix transform, double additionalSize)
        {
            System.Windows.Media.Geometry geometry = this.GetWidenedByPenGeometry(this.geometry, pen, additionalSize).Clone();
            return this.GetTransformedGeometryBounds(geometry, transform);
        }

        public System.Windows.Media.Geometry GetWidenedByPenGeometry(Pen parentOutlinePen) => 
            this.GetWidenedByPenGeometry(this.geometry, parentOutlinePen, 0.0);

        protected System.Windows.Media.Geometry GetWidenedByPenGeometry(System.Windows.Media.Geometry geometry, Pen parentOutlinePen, double additionalSize)
        {
            Pen widenPen = this.GetWidenPen(parentOutlinePen, additionalSize);
            if (widenPen == null)
            {
                return geometry;
            }
            System.Windows.Media.Geometry widenedPathGeometry = geometry.GetWidenedPathGeometry(widenPen);
            return System.Windows.Media.Geometry.Combine(geometry, widenedPathGeometry, GeometryCombineMode.Union, Transform.Identity);
        }

        private Pen GetWidenPen(Pen parentOutlinePen, double additionalSize)
        {
            Pen pen = null;
            if (parentOutlinePen != null)
            {
                pen = parentOutlinePen.Clone();
                pen.Thickness += additionalSize;
                this.PrepareLineCapsAndFreeze(pen);
            }
            else if (additionalSize > 0.0)
            {
                pen = new Pen(Brushes.Black, additionalSize);
                this.PrepareLineCapsAndFreeze(pen);
            }
            return pen;
        }

        private void PrepareLineCapsAndFreeze(Pen pen)
        {
            pen.StartLineCap = PenLineCap.Round;
            pen.EndLineCap = PenLineCap.Round;
            if (pen.CanFreeze)
            {
                pen.Freeze();
            }
        }

        public System.Windows.Media.Geometry Geometry =>
            this.geometry;

        public virtual Rect Bounds =>
            this.geometry.Bounds;
    }
}

