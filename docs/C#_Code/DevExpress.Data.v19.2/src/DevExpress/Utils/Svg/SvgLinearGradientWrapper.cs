namespace DevExpress.Utils.Svg
{
    using System;
    using System.Collections.Generic;
    using System.Drawing;
    using System.Drawing.Drawing2D;
    using System.Linq;

    public class SvgLinearGradientWrapper : SvgGradientWrapper
    {
        public SvgLinearGradientWrapper(SvgElement element) : base(element)
        {
        }

        private IList<PointF> CalcIntersections(RectangleF bounds, PointF start, PointF end)
        {
            List<PointF> list = new List<PointF>();
            if (Math.Round((double) Math.Abs((float) (start.Y - end.Y)), 3) == 0.0)
            {
                list.Add(new PointF(bounds.Left, start.Y));
                list.Add(new PointF(bounds.Right, start.Y));
            }
            else if (Math.Round((double) Math.Abs((float) (start.X - end.X)), 3) == 0.0)
            {
                list.Add(new PointF(start.X, bounds.Top));
                list.Add(new PointF(start.X, bounds.Bottom));
            }
            else
            {
                if (((start.X == bounds.Left) || (start.X == bounds.Right)) && ((start.Y == bounds.Top) || (start.Y == bounds.Bottom)))
                {
                    list.Add(start);
                }
                if (((end.X == bounds.Left) || (end.X == bounds.Right)) && ((end.Y == bounds.Top) || (end.Y == bounds.Bottom)))
                {
                    list.Add(end);
                }
                if (list.Count < 2)
                {
                    PointF item = new PointF(bounds.Left, (((end.Y - start.Y) / (end.X - start.X)) * (bounds.Left - start.X)) + start.Y);
                    if ((bounds.Top <= item.Y) && ((item.Y <= bounds.Bottom) && !list.Contains(item)))
                    {
                        list.Add(item);
                    }
                    item = new PointF(bounds.Right, (((end.Y - start.Y) / (end.X - start.X)) * (bounds.Right - start.X)) + start.Y);
                    if ((bounds.Top <= item.Y) && ((item.Y <= bounds.Bottom) && !list.Contains(item)))
                    {
                        list.Add(item);
                    }
                    item = new PointF((((bounds.Top - start.Y) / (end.Y - start.Y)) * (end.X - start.X)) + start.X, bounds.Top);
                    if ((bounds.Left <= item.X) && ((item.X <= bounds.Right) && !list.Contains(item)))
                    {
                        list.Add(item);
                    }
                    item = new PointF((((bounds.Bottom - start.Y) / (end.Y - start.Y)) * (end.X - start.X)) + start.X, bounds.Bottom);
                    if ((bounds.Left <= item.X) && ((item.X <= bounds.Right) && !list.Contains(item)))
                    {
                        list.Add(item);
                    }
                }
            }
            return list;
        }

        private LinePart CalcPartToMove(RectangleF bounds, PointF start, PointF end) => 
            (start.X != end.X) ? ((start.Y != end.Y) ? ((bounds.Contains(start) ? LinePart.Start : LinePart.None) | (bounds.Contains(end) ? LinePart.End : LinePart.None)) : ((((bounds.Left >= start.X) || (start.X >= bounds.Right)) ? LinePart.None : LinePart.Start) | (((bounds.Left >= end.X) || (end.X >= bounds.Right)) ? LinePart.None : LinePart.End))) : ((((bounds.Top >= start.Y) || (start.Y >= bounds.Bottom)) ? LinePart.None : LinePart.Start) | (((bounds.Top >= end.Y) || (end.Y >= bounds.Bottom)) ? LinePart.None : LinePart.End));

        private ColorBlend CalculateColorBlend(RectangleF bounds, double opacity, PointF specifiedStart, PointF realStart, PointF specifiedEnd, PointF realEnd)
        {
            float stretchedStart = 0f;
            float stretchedEnd = 0f;
            List<Color> list = new List<Color>();
            List<float> list2 = new List<float>();
            ColorBlend blend = base.GetColorBlend(bounds, opacity, false);
            double length = GetLength(specifiedStart, realStart);
            double num2 = GetLength(specifiedEnd, realEnd);
            if ((length > 0.0) || (num2 > 0.0))
            {
                double num3 = GetLength(specifiedStart, specifiedEnd);
                PointF unitVector = new PointF((specifiedEnd.X - specifiedStart.X) / ((float) num3), (specifiedEnd.Y - specifiedStart.Y) / ((float) num3));
                double num4 = GetLength(realStart, realEnd);
                SvgGradientSpreadMethod spreadMethod = this.Gradient.SpreadMethod;
                if (spreadMethod != SvgGradientSpreadMethod.Reflect)
                {
                    if (spreadMethod == SvgGradientSpreadMethod.Repeat)
                    {
                        stretchedStart = (float) Math.Ceiling((double) (GetLength(realStart, specifiedStart) / num3));
                        stretchedEnd = (float) Math.Ceiling((double) (GetLength(realEnd, specifiedEnd) / num3));
                        int num12 = 0;
                        while (true)
                        {
                            if (num12 >= ((stretchedStart + stretchedEnd) + 1f))
                            {
                                list2[list2.Count - 1] = 1f;
                                blend.Colors = list.ToArray();
                                blend.Positions = list2.ToArray();
                                break;
                            }
                            int index = 0;
                            while (true)
                            {
                                if (index >= blend.Positions.Length)
                                {
                                    num12++;
                                    break;
                                }
                                list2.Add((num12 + (blend.Positions[index] * 0.9999f)) / ((stretchedStart + stretchedEnd) + 1f));
                                list.Add(blend.Colors[index]);
                                index++;
                            }
                        }
                    }
                    else
                    {
                        int index = 0;
                        while (true)
                        {
                            if (index >= blend.Positions.Length)
                            {
                                if (length > 0.0)
                                {
                                    blend.Positions = new float[1].Concat<float>(blend.Positions).ToArray<float>();
                                    Color[] first = new Color[] { blend.Colors.First<Color>() };
                                    blend.Colors = first.Concat<Color>(blend.Colors).ToArray<Color>();
                                }
                                if (num2 > 0.0)
                                {
                                    float[] singleArray1 = new float[] { 1f };
                                    blend.Positions = blend.Positions.Concat<float>(singleArray1).ToArray<float>();
                                    Color[] colorArray2 = new Color[] { blend.Colors.Last<Color>() };
                                    blend.Colors = blend.Colors.Concat<Color>(colorArray2).ToArray<Color>();
                                }
                                break;
                            }
                            PointF second = MovePointAlongVector(specifiedStart, unitVector, ((float) num3) * blend.Positions[index]);
                            double num15 = GetLength(realStart, second);
                            blend.Positions[index] = (float) Math.Round(Math.Max(0.0, Math.Min((double) (num15 / num4), (double) 1.0)), 5);
                            index++;
                        }
                    }
                }
                else
                {
                    stretchedStart = (float) Math.Ceiling((double) (GetLength(realStart, specifiedStart) / num3));
                    stretchedEnd = (float) Math.Ceiling((double) (GetLength(realEnd, specifiedEnd) / num3));
                    list = blend.Colors.ToList<Color>();
                    list2 = (from p in blend.Positions select p + stretchedStart).ToList<float>();
                    int num6 = 0;
                    while (true)
                    {
                        if (num6 >= stretchedStart)
                        {
                            int num9 = 0;
                            while (true)
                            {
                                if (num9 >= stretchedEnd)
                                {
                                    blend.Colors = list.ToArray();
                                    blend.Positions = (from p in list2 select p / ((stretchedStart + 1f) + stretchedEnd)).ToArray<float>();
                                    break;
                                }
                                if ((num9 % 2) != 0)
                                {
                                    for (int i = 1; i < blend.Positions.Length; i++)
                                    {
                                        list2.Add(((stretchedStart + 1f) + num9) + blend.Positions[i]);
                                        list.Add(blend.Colors[i]);
                                    }
                                }
                                else
                                {
                                    int count = list2.Count;
                                    for (int i = 0; i < (blend.Positions.Length - 1); i++)
                                    {
                                        list2.Insert(count, (((stretchedStart + 1f) + num9) + 1f) - blend.Positions[i]);
                                        list.Insert(count, blend.Colors[i]);
                                    }
                                }
                                num9++;
                            }
                            break;
                        }
                        if ((num6 % 2) == 0)
                        {
                            for (int i = 1; i < blend.Positions.Length; i++)
                            {
                                list2.Insert(0, (((stretchedStart - 1f) - num6) + 1f) - blend.Positions[i]);
                                list.Insert(0, blend.Colors[i]);
                            }
                        }
                        else
                        {
                            for (int i = 0; i < (blend.Positions.Length - 1); i++)
                            {
                                list2.Insert(i, ((stretchedStart - 1f) - num6) + blend.Positions[i]);
                                list.Insert(i, blend.Colors[i]);
                            }
                        }
                        num6++;
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
            if (this.Gradient.Stops.Count == 1)
            {
                double? brightness = null;
                return new SolidBrush(base.GetColor(null, this.Gradient.Stops[0].StopColor, this.Gradient.Stops[0].StopOpacity, brightness, true));
            }
            RectangleF bounds = path.GetBounds();
            PointF[] pts = new PointF[] { base.GetPoint(this.Gradient.GradientUnits, this.Gradient.StartX, this.Gradient.StartY, scale), base.GetPoint(this.Gradient.GradientUnits, this.Gradient.EndX, this.Gradient.EndY, scale) };
            if ((bounds.Width <= 0f) || ((bounds.Height <= 0f) || ((pts[0].X == pts[1].X) && (pts[0].Y == pts[1].Y))))
            {
                return new SolidBrush(Color.Black);
            }
            Matrix m = new Matrix();
            using (Matrix matrix2 = this.GetTransform(m, scale, false))
            {
                if (this.Gradient.GradientUnits == SvgCoordinateUnits.ObjectBoundingBox)
                {
                    PointF tf3 = new PointF((pts[0].X + pts[1].X) / 2f, (pts[0].Y + pts[1].Y) / 2f);
                    matrix2.Translate(bounds.X, bounds.Y, MatrixOrder.Prepend);
                    if (this.Gradient.GradientUnits == SvgCoordinateUnits.ObjectBoundingBox)
                    {
                        matrix2.Scale(bounds.Width, bounds.Height, MatrixOrder.Prepend);
                    }
                }
                matrix2.TransformPoints(pts);
            }
            PointF tf = pts[0];
            PointF tf2 = pts[1];
            if (this.CalcPartToMove(bounds, pts[0], pts[1]) != LinePart.None)
            {
                PointF[] tfArray2 = this.UpdateGradient(bounds, pts[0], pts[1]);
                tf = tfArray2[0];
                tf2 = tfArray2[1];
            }
            LinearGradientBrush brush1 = new LinearGradientBrush(tf, tf2, Color.Transparent, Color.Transparent);
            brush1.InterpolationColors = this.CalculateColorBlend(bounds, opacity.GetValueOrDefault(1.0), pts[0], tf, pts[1], tf2);
            brush1.WrapMode = WrapMode.TileFlipX;
            return brush1;
        }

        private static double GetLength(PointF first, PointF second) => 
            Math.Sqrt(Math.Pow((double) (first.X - second.X), 2.0) + Math.Pow((double) (first.Y - second.Y), 2.0));

        private static PointF MovePointAlongVector(PointF start, PointF unitVector, float distance) => 
            start + new SizeF(unitVector.X * distance, unitVector.Y * distance);

        private PointF[] UpdateGradient(RectangleF bounds, PointF start, PointF end)
        {
            PointF[] tfArray = new PointF[2];
            LinePart part = this.CalcPartToMove(bounds, start, end);
            if (part == LinePart.None)
            {
                tfArray[0] = start;
                tfArray[1] = end;
                return tfArray;
            }
            PointF first = start;
            PointF tf2 = end;
            IList<PointF> source = this.CalcIntersections(bounds, start, end);
            if ((source.Count > 1) && ((Math.Sign((float) (source[1].X - source[0].X)) != Math.Sign((float) (end.X - start.X))) || (Math.Sign((float) (source[1].Y - source[0].Y)) != Math.Sign((float) (end.Y - start.Y)))))
            {
                source = source.Reverse<PointF>().ToList<PointF>();
            }
            if ((part & LinePart.Start) > LinePart.None)
            {
                first = source[0];
            }
            if ((part & LinePart.End) > LinePart.None)
            {
                tf2 = (source.Count > 1) ? source[1] : source[0];
            }
            SvgGradientSpreadMethod spreadMethod = this.Gradient.SpreadMethod;
            if ((spreadMethod == SvgGradientSpreadMethod.Reflect) || (spreadMethod == SvgGradientSpreadMethod.Repeat))
            {
                double length = GetLength(start, end);
                PointF unitVector = new PointF((end.X - start.X) / ((float) length), (end.Y - start.Y) / ((float) length));
                PointF tf5 = new PointF(-unitVector.X, -unitVector.Y);
                first = MovePointAlongVector(start, tf5, (float) (Math.Ceiling((double) (GetLength(first, start) / length)) * length));
                tf2 = MovePointAlongVector(end, unitVector, (float) (Math.Ceiling((double) (GetLength(tf2, end) / length)) * length));
            }
            tfArray[0] = first;
            tfArray[1] = tf2;
            return tfArray;
        }

        private SvgLinearGradient Gradient =>
            base.Element as SvgLinearGradient;

        [Flags]
        private enum LinePart
        {
            None,
            Start,
            End
        }
    }
}

