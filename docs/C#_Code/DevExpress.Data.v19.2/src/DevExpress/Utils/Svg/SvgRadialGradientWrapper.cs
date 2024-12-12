namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;
    using System.Runtime.InteropServices;

    public class SvgRadialGradientWrapper : SvgGradientWrapper
    {
        public SvgRadialGradientWrapper(SvgElement element) : base(element)
        {
        }

        private double CalcScale(RectangleF bounds, GraphicsPath path)
        {
            PointF[] pts = new PointF[] { new PointF(bounds.Left, bounds.Top), new PointF(bounds.Right, bounds.Top), new PointF(bounds.Right, bounds.Bottom), new PointF(bounds.Left, bounds.Bottom) };
            RectangleF ef = path.GetBounds();
            PointF tf = new PointF(ef.X + (ef.Width / 2f), ef.Y + (ef.Height / 2f));
            using (Matrix matrix = new Matrix())
            {
                matrix.Translate(-1f * tf.X, -1f * tf.Y, MatrixOrder.Append);
                matrix.Scale(0.95f, 0.95f, MatrixOrder.Append);
                matrix.Translate(tf.X, tf.Y, MatrixOrder.Append);
                while (!path.IsVisible(pts[0]) || (!path.IsVisible(pts[1]) || (!path.IsVisible(pts[2]) || !path.IsVisible(pts[3]))))
                {
                    PointF[] first = new PointF[] { new PointF(pts[0].X, pts[0].Y), new PointF(pts[1].X, pts[1].Y), new PointF(pts[2].X, pts[2].Y), new PointF(pts[3].X, pts[3].Y) };
                    matrix.TransformPoints(pts);
                    if (first.SequenceEqual<PointF>(pts))
                    {
                        break;
                    }
                }
            }
            return (double) (bounds.Height / (pts[2].Y - pts[1].Y));
        }

        private ColorBlend CalculateColorBlend(RectangleF renderer, float opacity, double scale, out double outScale)
        {
            ColorBlend blend = base.GetColorBlend(renderer, (double) opacity, true);
            outScale = scale;
            if (scale > 1.0)
            {
                List<float> list;
                List<Color> list2;
                float newScale;
                SvgGradientSpreadMethod spreadMethod = this.Gradient.SpreadMethod;
                if (spreadMethod != SvgGradientSpreadMethod.Reflect)
                {
                    if (spreadMethod != SvgGradientSpreadMethod.Repeat)
                    {
                        outScale = 1.0;
                    }
                    else
                    {
                        newScale = (float) Math.Ceiling(scale);
                        list = (from p in blend.Positions select p / newScale).ToList<float>();
                        list2 = blend.Colors.ToList<Color>();
                        int i = 1;
                        while (true)
                        {
                            if (i >= newScale)
                            {
                                blend.Positions = list.ToArray();
                                blend.Colors = list2.ToArray();
                                outScale = newScale;
                                break;
                            }
                            list.AddRange(from p in blend.Positions select (i + ((p <= 0f) ? 0.001f : p)) / newScale);
                            list2.AddRange(blend.Colors);
                            int num4 = i;
                            i = num4 + 1;
                        }
                    }
                }
                else
                {
                    newScale = (float) Math.Ceiling(scale);
                    list = (from p in blend.Positions select 1f + ((p - 1f) / newScale)).ToList<float>();
                    list2 = blend.Colors.ToList<Color>();
                    int num = 1;
                    while (true)
                    {
                        if (num >= newScale)
                        {
                            blend.Positions = list.ToArray();
                            blend.Colors = list2.ToArray();
                            outScale = newScale;
                            break;
                        }
                        if ((num % 2) == 1)
                        {
                            for (int i = 1; i < blend.Positions.Length; i++)
                            {
                                list.Insert(0, ((((newScale - num) - 1f) / newScale) + 1f) - blend.Positions[i]);
                                list2.Insert(0, blend.Colors[i]);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < (blend.Positions.Length - 1); i++)
                            {
                                list.Insert(i, (((newScale - num) - 1f) / newScale) + blend.Positions[i]);
                                list2.Insert(i, blend.Colors[i]);
                            }
                        }
                        num++;
                    }
                }
            }
            return blend;
        }

        public override Brush GetBrush(ISvgGraphics g, SvgElement element, double scale, string colorValue, double? opacity, bool useStyleName, GraphicsPath path)
        {
            if (this.Gradient.Stops.Count < 1)
            {
                return new SolidBrush(Color.Black);
            }
            PointF[] pts = new PointF[1];
            SvgUnit x = (this.Gradient.FocalX == null) ? this.Gradient.CenterX : this.Gradient.FocalX;
            PointF tf = base.GetPoint(this.Gradient.GradientUnits, this.Gradient.CenterX, this.Gradient.CenterY, 1.0);
            pts[0] = base.GetPoint(this.Gradient.GradientUnits, x, (this.Gradient.FocalY == null) ? this.Gradient.CenterY : this.Gradient.FocalY, 1.0);
            float num = (float) base.GetValue(this.Gradient.GradientUnits, this.Gradient.Radius);
            GraphicsPath path2 = new GraphicsPath();
            path2.AddEllipse((float) (tf.X - num), (float) (tf.Y - num), (float) (num * 2f), (float) (num * 2f));
            RectangleF bounds = path.GetBounds();
            Matrix m = new Matrix();
            m.Scale((float) scale, (float) scale);
            using (Matrix matrix2 = this.GetTransform(m, 1.0, false))
            {
                if (this.Gradient.GradientUnits == SvgCoordinateUnits.ObjectBoundingBox)
                {
                    matrix2.Translate(bounds.X, bounds.Y, MatrixOrder.Prepend);
                    matrix2.Scale(bounds.Width, bounds.Height, MatrixOrder.Prepend);
                }
                path2.Transform(matrix2);
                matrix2.TransformPoints(pts);
            }
            float num2 = (element.StrokeWidth == null) ? ((float) 1.0) : ((float) element.StrokeWidth.Value);
            RectangleF ef2 = RectangleF.Inflate(bounds, num2, num2);
            double num3 = this.CalcScale(ef2, path2);
            if ((num3 > 1.0) && (this.Gradient.SpreadMethod == SvgGradientSpreadMethod.Pad))
            {
                SvgGradientStop stop = this.Gradient.Stops.Last<SvgGradientStop>();
                double? nullable = null;
                nullable = null;
                Color baseColor = base.GetColor(null, stop.StopColor, nullable, nullable, false);
                using (SolidBrush brush2 = new SolidBrush(Color.FromArgb((int) ((opacity.GetValueOrDefault(1.0) * stop.Opacity.GetValueOrDefault(1.0)) * 255.0), baseColor)))
                {
                    g.FillPath(brush2, path);
                }
            }
            ColorBlend blend = this.CalculateColorBlend(bounds, (float) opacity.GetValueOrDefault(1.0), num3, out num3);
            RectangleF ef3 = path2.GetBounds();
            PointF tf2 = new PointF(ef3.Left + (ef3.Width / 2f), ef3.Top + (ef3.Height / 2f));
            using (Matrix matrix3 = new Matrix())
            {
                matrix3.Translate(-1f * tf2.X, -1f * tf2.Y, MatrixOrder.Append);
                matrix3.Scale((float) num3, (float) num3, MatrixOrder.Append);
                matrix3.Translate(tf2.X, tf2.Y, MatrixOrder.Append);
                path2.Transform(matrix3);
            }
            return new PathGradientBrush(path2) { 
                CenterPoint = pts[0],
                InterpolationColors = blend
            };
        }

        private SvgRadialGradient Gradient =>
            base.Element as SvgRadialGradient;
    }
}

