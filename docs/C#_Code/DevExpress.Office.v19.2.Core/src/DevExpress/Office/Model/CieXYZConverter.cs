namespace DevExpress.Office.Model
{
    using System;
    using System.Drawing;

    public static class CieXYZConverter
    {
        private static double CLinear(double c) => 
            (c > 0.04045) ? Math.Pow((c + 0.055) / 1.055, 2.4) : (c / 12.92);

        public static double GetX(Color color)
        {
            double num2 = CLinear(((double) color.G) / 255.0);
            double num3 = CLinear(((double) color.B) / 255.0);
            return (((0.4124 * CLinear(((double) color.R) / 255.0)) + (0.3876 * num2)) + (0.1805 * num3));
        }

        public static double GetY(Color color)
        {
            double num2 = CLinear(((double) color.G) / 255.0);
            double num3 = CLinear(((double) color.B) / 255.0);
            return (((0.2126 * CLinear(((double) color.R) / 255.0)) + (0.7152 * num2)) + (0.0722 * num3));
        }

        public static double GetZ(Color color)
        {
            double num2 = CLinear(((double) color.G) / 255.0);
            double num3 = CLinear(((double) color.B) / 255.0);
            return (((0.0193 * CLinear(((double) color.R) / 255.0)) + (0.1192 * num2)) + (0.9505 * num3));
        }
    }
}

