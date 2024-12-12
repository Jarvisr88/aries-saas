namespace DevExpress.XtraPrinting.Native
{
    using DevExpress.XtraPrinting;
    using System;
    using System.Runtime.CompilerServices;

    internal static class BorderDashStyleExtensions
    {
        public static bool IsDashedOrDottedLineStyle(this BorderDashStyle borderDashStyle);
        public static bool IsSolidLineStyle(this BorderDashStyle borderDashStyle);
    }
}

