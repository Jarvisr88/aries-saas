namespace DevExpress.XtraPrinting.Native
{
    using System;

    [Flags]
    public enum FillPageResult
    {
        public const FillPageResult None = FillPageResult.None;,
        public const FillPageResult Fulfill = FillPageResult.Fulfill;,
        public const FillPageResult OverFulfill = FillPageResult.OverFulfill;,
        public const FillPageResult Complete = FillPageResult.Complete;
    }
}

