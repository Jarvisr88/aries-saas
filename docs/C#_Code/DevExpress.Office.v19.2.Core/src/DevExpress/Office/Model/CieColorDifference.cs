namespace DevExpress.Office.Model
{
    using System;
    using System.Drawing;

    public static class CieColorDifference
    {
        public static double Cie76(Color x, Color y)
        {
            double num = CieLABConverter.GetL(x) - CieLABConverter.GetL(y);
            double num2 = CieLABConverter.GetA(x) - CieLABConverter.GetA(y);
            double num3 = CieLABConverter.GetB(x) - CieLABConverter.GetB(y);
            return Math.Sqrt(((num * num) + (num2 * num2)) + (num3 * num3));
        }

        public static double Cie94(Color x, Color y)
        {
            double a = CieLABConverter.GetA(x);
            double b = CieLABConverter.GetB(x);
            double num3 = CieLABConverter.GetA(y);
            double num4 = CieLABConverter.GetB(y);
            double num5 = CieLABConverter.GetL(x) - CieLABConverter.GetL(y);
            double num6 = a - num3;
            double num7 = b - num4;
            double num8 = Math.Sqrt((a * a) + (b * b));
            double num10 = num8 - Math.Sqrt((num3 * num3) + (num4 * num4));
            double num11 = Math.Sqrt(((num6 * num6) + (num7 * num7)) - (num10 * num10));
            return Math.Sqrt(((num5 * num5) + Math.Pow(num10 / (1.0 + (0.045 * num8)), 2.0)) + Math.Pow(num11 / (1.0 + (0.015 * num8)), 2.0));
        }
    }
}

