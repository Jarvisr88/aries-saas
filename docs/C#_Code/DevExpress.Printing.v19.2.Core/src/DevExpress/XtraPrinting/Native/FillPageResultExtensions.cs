namespace DevExpress.XtraPrinting.Native
{
    using System;
    using System.Runtime.CompilerServices;

    public static class FillPageResultExtensions
    {
        public static bool IsComplete(this FillPageResult fillPageResult);
        public static bool IsFulfill(this FillPageResult fillPageResult);
        public static bool IsOverFulfill(this FillPageResult fillPageResult);
    }
}

