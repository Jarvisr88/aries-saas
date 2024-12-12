namespace DevExpress.Data.XtraReports.Wizard
{
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.ColumnNameSummaryOptions class from the DevExpress.XtraReports assembly instead.")]
    public class ColumnNameSummaryOptions : ICloneable
    {
        public ColumnNameSummaryOptions();
        public ColumnNameSummaryOptions(string columnName);
        public ColumnNameSummaryOptions(string columnName, SummaryOptionFlags flags);
        public object Clone();
        public override bool Equals(object obj);
        public override int GetHashCode();

        public string ColumnName { get; set; }

        public SummaryOptionFlags Flags { get; set; }
    }
}

