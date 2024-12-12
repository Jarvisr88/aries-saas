namespace DevExpress.XtraExport.Xls
{
    using System;

    public static class XNumChecker
    {
        private static readonly long negativeZeroBits = BitConverter.DoubleToInt64Bits(0.0);

        public static void CheckValue(double value)
        {
            if (double.IsNaN(value))
            {
                throw new ArgumentException("value is not-a-number");
            }
            if (double.IsInfinity(value))
            {
                throw new ArgumentException("value is infinity");
            }
            if (IsNegativeZero(value))
            {
                throw new ArgumentException("value is negative zero");
            }
        }

        public static bool IsNegativeZero(double value) => 
            BitConverter.DoubleToInt64Bits(value) == negativeZeroBits;
    }
}

