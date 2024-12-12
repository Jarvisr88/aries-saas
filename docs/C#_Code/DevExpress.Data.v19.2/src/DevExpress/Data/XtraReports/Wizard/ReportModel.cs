namespace DevExpress.Data.XtraReports.Wizard
{
    using DevExpress.Data.WizardFramework;
    using DevExpress.Data.XtraReports.DataProviders;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.ReportModel class from the DevExpress.XtraReports assembly instead.", false)]
    public class ReportModel : IWizardModel, ICloneable
    {
        public ReportModel();
        protected ReportModel(ReportModel source);
        public virtual object Clone();
        public override bool Equals(object obj);
        public override int GetHashCode();
        private static bool HashSetsEqual<T>(HashSet<T> x, HashSet<T> y);
        public bool IsGrouped();

        public string DataSourceName { get; set; }

        public TableInfo DataMemberName { get; set; }

        public string[] Columns { get; set; }

        public HashSet<string>[] GroupingLevels { get; set; }

        public DevExpress.Data.XtraReports.Wizard.ReportType ReportType { get; set; }

        public bool Portrait { get; set; }

        public bool AdjustFieldWidth { get; set; }

        public HashSet<ColumnNameSummaryOptions> SummaryOptions { get; set; }

        public bool IgnoreNullValuesForSummary { get; set; }

        public ReportLayout Layout { get; set; }

        public DevExpress.Data.XtraReports.Wizard.ReportStyleId ReportStyleId { get; set; }

        public string ReportTitle { get; set; }

        public int LabelProductId { get; set; }

        public int LabelProductDetailId { get; set; }

        public DevExpress.Data.XtraReports.Wizard.CustomLabelInformation CustomLabelInformation { get; set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ReportModel.<>c <>9;
            public static Func<ColumnNameSummaryOptions, ColumnNameSummaryOptions> <>9__61_0;

            static <>c();
            internal ColumnNameSummaryOptions <.ctor>b__61_0(ColumnNameSummaryOptions x);
        }
    }
}

