namespace DevExpress.Office.Drawing
{
    using DevExpress.Office.Model;
    using System;
    using System.Drawing;

    public static class DrawingValueConverter
    {
        public static double DegreeToRadian(double degree) => 
            (degree / 180.0) * 3.1415926535897931;

        public static double FromPercentage(int value) => 
            ((double) value) / 100000.0;

        public static double FromPositiveFixedAngle(double value) => 
            value / 60000.0;

        public static double FromPositiveFixedAngle(int value) => 
            ((double) value) / 60000.0;

        public static Color GetDarkenTransformColor(Color color, double vmod)
        {
            double num14;
            double num15;
            double num16;
            double num = ((double) color.R) / 255.0;
            double num2 = ((double) color.G) / 255.0;
            double num3 = ((double) color.B) / 255.0;
            double num4 = Math.Max(num, Math.Max(num2, num3));
            double num5 = Math.Min(num, Math.Min(num2, num3));
            double num6 = (num4 != num5) ? (((num4 != num) || (num2 < num3)) ? (((num4 != num) || (num2 >= num3)) ? ((num4 != num2) ? (((60.0 * (num - num2)) / (num4 - num5)) + 240.0) : (((60.0 * (num3 - num)) / (num4 - num5)) + 120.0)) : (((60.0 * (num2 - num3)) / (num4 - num5)) + 360.0)) : ((60.0 * (num2 - num3)) / (num4 - num5))) : 0.0;
            double num8 = Math.Min((double) 1.0, (double) (num4 * vmod));
            int num9 = ((int) (num6 / 60.0)) % 6;
            double num10 = (1.0 - ((num4 == 0.0) ? 0.0 : (1.0 - (num5 / num4)))) * num8;
            double num11 = ((num8 - num10) * (num6 % 60.0)) / 60.0;
            double num12 = num10 + num11;
            double num13 = num8 - num11;
            if (num9 == 0)
            {
                num14 = num8;
                num15 = num12;
                num16 = num10;
            }
            else if (num9 == 1)
            {
                num14 = num13;
                num15 = num8;
                num16 = num10;
            }
            else if (num9 == 2)
            {
                num14 = num10;
                num15 = num8;
                num16 = num12;
            }
            else if (num9 == 3)
            {
                num14 = num10;
                num15 = num13;
                num16 = num8;
            }
            else if (num9 == 4)
            {
                num14 = num12;
                num15 = num10;
                num16 = num8;
            }
            else
            {
                num14 = num8;
                num15 = num10;
                num16 = num13;
            }
            return Color.FromArgb((int) (num14 * 255.0), (int) (num15 * 255.0), (int) (num16 * 255.0));
        }

        public static Color GetLightenTransformColor(Color color, double tint) => 
            ColorHSL.CalculateColorRGB(color, tint);

        public static double RadianToDegree(double radian) => 
            (radian / 3.1415926535897931) * 180.0;

        public static int ToPercentage(double value) => 
            (int) (value * 100000.0);

        public static int ToPositiveFixedAngle(double value) => 
            (int) (value * 60000.0);
    }
}

