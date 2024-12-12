namespace DevExpress.Office.Model
{
    using System;
    using System.Drawing;

    public static class ColorDifference
    {
        public static double HSB(Color x, Color y)
        {
            double num = Math.Abs((float) (x.GetHue() - y.GetHue()));
            if (num > 180.0)
            {
                num = 360.0 - num;
            }
            double num3 = Math.Abs((float) (x.GetSaturation() - y.GetSaturation())) * 1.5;
            return (((Math.Abs((float) (x.GetBrightness() - y.GetBrightness())) * 3.0) + (num / 57.3)) + num3);
        }

        public static double RGB(Color x, Color y)
        {
            double num = (x.R - y.R) / 255.0;
            double num2 = (x.G - y.G) / 255.0;
            double num3 = (x.B - y.B) / 255.0;
            return Math.Sqrt(((num * num) + (num2 * num2)) + (num3 * num3));
        }
    }
}

