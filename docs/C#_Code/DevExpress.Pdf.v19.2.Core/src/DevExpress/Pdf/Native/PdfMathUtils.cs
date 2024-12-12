namespace DevExpress.Pdf.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public static class PdfMathUtils
    {
        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Max(double value1, double value2) => 
            (value1 > value2) ? value1 : value2;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static double Min(double value1, double value2) => 
            (value1 < value2) ? value1 : value2;

        public static double NormalizeAngle(double angle)
        {
            while (angle < 0.0)
            {
                angle += 6.2831853071795862;
            }
            while (angle >= 6.2831853071795862)
            {
                angle -= 6.2831853071795862;
            }
            return angle;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static byte ToByte(double value) => 
            (value <= 0.0) ? 0 : ((value >= 255.5) ? 0xff : ((byte) (value + 0.5)));

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        public static int ToInt32(double value) => 
            (value < 0.0) ? ((value < -2147483648.5) ? -2147483648 : ((int) (value - 0.5))) : ((value >= 2147483647.5) ? 0x7fffffff : ((int) (value + 0.5)));
    }
}

