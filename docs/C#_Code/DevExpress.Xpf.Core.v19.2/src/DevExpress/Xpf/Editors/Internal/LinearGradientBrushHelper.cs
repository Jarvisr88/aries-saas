namespace DevExpress.Xpf.Editors.Internal
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Windows;
    using System.Windows.Media;

    public static class LinearGradientBrushHelper
    {
        private static double Dist(Point px, Point po, Point pf)
        {
            double num = Math.Sqrt(((px.Y - po.Y) * (px.Y - po.Y)) + ((px.X - po.X) * (px.X - po.X)));
            if (((px.Y.LessThan(po.Y) && pf.Y.GreaterThan(po.Y)) || ((px.Y.GreaterThan(po.Y) && pf.Y.LessThan(po.Y)) || (px.Y.AreClose(po.Y) && (px.X.LessThan(po.X) && pf.X.GreaterThan(po.X))))) || (px.Y.AreClose(po.Y) && (px.X.GreaterThan(po.X) && pf.X.LessThan(po.X))))
            {
                num = -num;
            }
            return num;
        }

        public static Color GetColorAtPoint(this GradientBrush brush, double width, double height, Point thePoint)
        {
            Point point3;
            double y = 0.5 * height;
            Point po = new Point(0.0, y);
            double num2 = width;
            double num3 = 0.5 * height;
            Point pf = new Point(num2, num3);
            if (y.AreClose(num3))
            {
                point3 = new Point(thePoint.X, y);
            }
            else if (num2.AreClose(0.0))
            {
                point3 = new Point(0.0, thePoint.Y);
            }
            else
            {
                double num9 = (num3 - y) / num2;
                double num10 = -1.0 / num9;
                double num11 = y;
                double num13 = ((thePoint.Y - (num10 * thePoint.X)) - num11) / (num9 - num10);
                point3 = new Point(num13, (num9 * num13) + num11);
            }
            double x = Dist(point3, po, pf) / Dist(pf, po, pf);
            Func<GradientStop, double> selector = <>c.<>9__0_0;
            if (<>c.<>9__0_0 == null)
            {
                Func<GradientStop, double> local1 = <>c.<>9__0_0;
                selector = <>c.<>9__0_0 = n => n.Offset;
            }
            double num6 = brush.GradientStops.Max<GradientStop>(selector);
            if (x > num6)
            {
                x = num6;
            }
            Func<GradientStop, double> func2 = <>c.<>9__0_1;
            if (<>c.<>9__0_1 == null)
            {
                Func<GradientStop, double> local2 = <>c.<>9__0_1;
                func2 = <>c.<>9__0_1 = n => n.Offset;
            }
            double num7 = brush.GradientStops.Min<GradientStop>(func2);
            if (x < num7)
            {
                x = num7;
            }
            Func<GradientStop, double> keySelector = <>c.<>9__0_3;
            if (<>c.<>9__0_3 == null)
            {
                Func<GradientStop, double> local3 = <>c.<>9__0_3;
                keySelector = <>c.<>9__0_3 = n => n.Offset;
            }
            GradientStop stop = (from n in brush.GradientStops
                where n.Offset <= x
                select n).OrderBy<GradientStop, double>(keySelector).Last<GradientStop>();
            Func<GradientStop, double> func4 = <>c.<>9__0_5;
            if (<>c.<>9__0_5 == null)
            {
                Func<GradientStop, double> local4 = <>c.<>9__0_5;
                func4 = <>c.<>9__0_5 = n => n.Offset;
            }
            GradientStop stop2 = (from n in brush.GradientStops
                where n.Offset >= x
                select n).OrderBy<GradientStop, double>(func4).First<GradientStop>();
            float num8 = 0f;
            if (!stop.Offset.AreClose(stop2.Offset))
            {
                num8 = (float) ((x - stop.Offset) / (stop2.Offset - stop.Offset));
            }
            return ((brush.ColorInterpolationMode != ColorInterpolationMode.ScRgbLinearInterpolation) ? Color.FromArgb((byte) (((stop2.Color.A - stop.Color.A) * num8) + stop.Color.A), (byte) (((stop2.Color.R - stop.Color.R) * num8) + stop.Color.R), (byte) (((stop2.Color.G - stop.Color.G) * num8) + stop.Color.G), (byte) (((stop2.Color.B - stop.Color.B) * num8) + stop.Color.B)) : Color.FromScRgb(((stop2.Color.ScA - stop.Color.ScA) * num8) + stop.Color.ScA, ((stop2.Color.ScR - stop.Color.ScR) * num8) + stop.Color.ScR, ((stop2.Color.ScG - stop.Color.ScG) * num8) + stop.Color.ScG, ((stop2.Color.ScB - stop.Color.ScB) * num8) + stop.Color.ScB));
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly LinearGradientBrushHelper.<>c <>9 = new LinearGradientBrushHelper.<>c();
            public static Func<GradientStop, double> <>9__0_0;
            public static Func<GradientStop, double> <>9__0_1;
            public static Func<GradientStop, double> <>9__0_3;
            public static Func<GradientStop, double> <>9__0_5;

            internal double <GetColorAtPoint>b__0_0(GradientStop n) => 
                n.Offset;

            internal double <GetColorAtPoint>b__0_1(GradientStop n) => 
                n.Offset;

            internal double <GetColorAtPoint>b__0_3(GradientStop n) => 
                n.Offset;

            internal double <GetColorAtPoint>b__0_5(GradientStop n) => 
                n.Offset;
        }
    }
}

