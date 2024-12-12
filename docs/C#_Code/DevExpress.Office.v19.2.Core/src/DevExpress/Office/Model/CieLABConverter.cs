namespace DevExpress.Office.Model
{
    using System;
    using System.Drawing;

    public static class CieLABConverter
    {
        private static readonly double threshold = Math.Pow(0.20689655172413793, 3.0);

        private static double Func(double t) => 
            (t <= threshold) ? (((Math.Pow(4.833333333333333, 2.0) * t) / 3.0) + 0.13793103448275862) : Math.Pow(t, 0.33333333333333331);

        public static double GetA(Color color)
        {
            double y = CieXYZConverter.GetY(color);
            return (500.0 * (Func(CieXYZConverter.GetX(color) / 3.0) - Func(y / 3.0)));
        }

        public static double GetB(Color color)
        {
            double z = CieXYZConverter.GetZ(color);
            return (200.0 * (Func(CieXYZConverter.GetY(color) / 3.0) - Func(z / 3.0)));
        }

        public static double GetL(Color color) => 
            (116.0 * Func(CieXYZConverter.GetY(color) / 3.0)) - 16.0;
    }
}

