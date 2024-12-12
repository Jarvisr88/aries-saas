namespace DevExpress.Data.XtraReports.Wizard
{
    using System;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.SummaryOptionFlags class from the DevExpress.XtraReports assembly instead."), Flags]
    public enum SummaryOptionFlags
    {
        public const SummaryOptionFlags None = SummaryOptionFlags.None;,
        public const SummaryOptionFlags Sum = SummaryOptionFlags.Sum;,
        public const SummaryOptionFlags Avg = SummaryOptionFlags.Avg;,
        public const SummaryOptionFlags Min = SummaryOptionFlags.Min;,
        public const SummaryOptionFlags Max = SummaryOptionFlags.Max;,
        public const SummaryOptionFlags Count = SummaryOptionFlags.Count;
    }
}

