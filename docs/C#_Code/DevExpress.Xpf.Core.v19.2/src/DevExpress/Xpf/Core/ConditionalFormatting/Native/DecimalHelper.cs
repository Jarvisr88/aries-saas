namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Runtime.CompilerServices;

    internal static class DecimalHelper
    {
        public static decimal AsDecimal(this double value) => 
            !double.IsNegativeInfinity(value) ? (!double.IsPositiveInfinity(value) ? ((decimal) value) : decimal.MaxValue) : decimal.MinValue;
    }
}

