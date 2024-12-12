namespace DevExpress.Utils.Svg
{
    using System;
    using System.Drawing.Drawing2D;

    public class SvgPathArcSegmentWrapper : SvgPathSegmentWrapper
    {
        public SvgPathArcSegmentWrapper(SvgPathSegment segment) : base(segment)
        {
        }

        public override void AddToPath(GraphicsPath path, double scale)
        {
            if (this.ArcSegment.Start != this.ArcSegment.End)
            {
                if ((this.ArcSegment.RadiusX == 0.0) && (this.ArcSegment.RadiusY == 0.0))
                {
                    path.AddLine(base.ScaleValue(this.ArcSegment.Start, scale), base.ScaleValue(this.ArcSegment.End, scale));
                }
                else
                {
                    double num = 0.0;
                    double radiusX = this.ArcSegment.RadiusX;
                    double radiusY = this.ArcSegment.RadiusY;
                    double num4 = Math.Sin((this.ArcSegment.Angle * 3.1415926535897931) / 180.0);
                    double num5 = Math.Cos((this.ArcSegment.Angle * 3.1415926535897931) / 180.0);
                    double num6 = ((num5 * (this.ArcSegment.Start.X - this.ArcSegment.End.X)) / 2.0) + ((num4 * (this.ArcSegment.Start.Y - this.ArcSegment.End.Y)) / 2.0);
                    double num7 = ((-num4 * (this.ArcSegment.Start.X - this.ArcSegment.End.X)) / 2.0) + ((num5 * (this.ArcSegment.Start.Y - this.ArcSegment.End.Y)) / 2.0);
                    double num8 = ((((radiusX * radiusX) * radiusY) * radiusY) - (((radiusX * radiusX) * num7) * num7)) - (((radiusY * radiusY) * num6) * num6);
                    if (num8 >= 0.0)
                    {
                        num = (((this.ArcSegment.LargeArc && this.ArcSegment.Sweep) || (!this.ArcSegment.LargeArc && !this.ArcSegment.Sweep)) ? -1.0 : 1.0) * Math.Sqrt(num8 / ((((radiusX * radiusX) * num7) * num7) + (((radiusY * radiusY) * num6) * num6)));
                    }
                    else
                    {
                        double num20 = (float) Math.Sqrt(1.0 - (num8 / (((radiusX * radiusX) * radiusY) * radiusY)));
                        radiusX *= num20;
                        radiusY *= num20;
                    }
                    double num9 = ((num * radiusX) * num7) / radiusY;
                    double num10 = ((-num * radiusY) * num6) / radiusX;
                    double num11 = ((num5 * num9) - (num4 * num10)) + ((this.ArcSegment.Start.X + this.ArcSegment.End.X) / 2.0);
                    double num12 = ((num4 * num9) + (num5 * num10)) + ((this.ArcSegment.Start.Y + this.ArcSegment.End.Y) / 2.0);
                    double a = this.GetAngle(1.0, 0.0, (num6 - num9) / radiusX, (num7 - num10) / radiusY);
                    double num14 = this.GetAngle((num6 - num9) / radiusX, (num7 - num10) / radiusY, (-num6 - num9) / radiusX, (-num7 - num10) / radiusY);
                    if (!this.ArcSegment.Sweep && (num14 > 0.0))
                    {
                        num14 -= 6.2831853071795862;
                    }
                    else if (this.ArcSegment.Sweep && (num14 < 0.0))
                    {
                        num14 += 6.2831853071795862;
                    }
                    int num15 = (int) Math.Ceiling(Math.Abs((double) (num14 / 1.5707963267948966)));
                    double num16 = num14 / ((double) num15);
                    double num17 = ((2.6666666666666665 * Math.Sin(num16 / 4.0)) * Math.Sin(num16 / 4.0)) / Math.Sin(num16 / 2.0);
                    double x = this.ArcSegment.Start.X;
                    double y = this.ArcSegment.Start.Y;
                    for (int i = 0; i < num15; i++)
                    {
                        double d = a + num16;
                        double num24 = (((num5 * radiusX) * Math.Cos(d)) - ((num4 * radiusY) * Math.Sin(d))) + num11;
                        double num25 = (((num4 * radiusX) * Math.Cos(d)) + ((num5 * radiusY) * Math.Sin(d))) + num12;
                        double num26 = num17 * (((-num5 * radiusX) * Math.Sin(a)) - ((num4 * radiusY) * Math.Cos(a)));
                        double num27 = num17 * (((-num4 * radiusX) * Math.Sin(a)) + ((num5 * radiusY) * Math.Cos(a)));
                        double num28 = num17 * (((num5 * radiusX) * Math.Sin(d)) + ((num4 * radiusY) * Math.Cos(d)));
                        double num29 = num17 * (((num4 * radiusX) * Math.Sin(d)) - ((num5 * radiusY) * Math.Cos(d)));
                        path.AddBezier((float) (x * scale), (float) (y * scale), (float) ((x + num26) * scale), (float) ((y + num27) * scale), (float) ((num24 + num28) * scale), (float) ((num25 + num29) * scale), (float) (num24 * scale), (float) (num25 * scale));
                        a = d;
                        x = (float) num24;
                        y = (float) num25;
                    }
                }
            }
        }

        private double GetAngle(double ax, double ay, double bx, double by)
        {
            double num = Math.Atan2(ay, ax);
            double num2 = Math.Atan2(by, bx);
            return ((num2 < num) ? (6.2831853071795862 - (num - num2)) : (num2 - num));
        }

        public SvgPathArcSegment ArcSegment =>
            base.Segment as SvgPathArcSegment;
    }
}

