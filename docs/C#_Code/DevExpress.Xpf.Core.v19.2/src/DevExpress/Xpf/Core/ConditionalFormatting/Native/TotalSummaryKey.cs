namespace DevExpress.Xpf.Core.ConditionalFormatting.Native
{
    using System;
    using System.Runtime.InteropServices;

    [StructLayout(LayoutKind.Sequential)]
    public struct TotalSummaryKey
    {
        private readonly ConditionalFormatSummaryType summaryType;
        private readonly string fieldName;
        public string FieldName =>
            this.fieldName;
        public ConditionalFormatSummaryType SummaryType =>
            this.summaryType;
        public TotalSummaryKey(ConditionalFormatSummaryType summaryType, string fieldName)
        {
            this.summaryType = summaryType;
            this.fieldName = fieldName;
        }

        public override int GetHashCode() => 
            (((0x11 * 0x17) + this.FieldName.GetHashCode()) * 0x17) + this.SummaryType.GetHashCode();
    }
}

