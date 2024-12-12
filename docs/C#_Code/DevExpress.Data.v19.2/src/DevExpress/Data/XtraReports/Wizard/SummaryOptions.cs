namespace DevExpress.Data.XtraReports.Wizard
{
    using System;
    using System.Collections.Specialized;
    using System.ComponentModel;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("Use the DevExpress.XtraReports.Wizards.SummaryOptionFlags class from the DevExpress.XtraReports assembly instead.")]
    public class SummaryOptions
    {
        private BitVector32 vector;

        public SummaryOptions(SummaryOptionFlags flags);

        public SummaryOptionFlags Flags { get; }

        public bool Sum { get; set; }

        public bool Avg { get; set; }

        public bool Min { get; set; }

        public bool Max { get; set; }

        public bool Count { get; set; }
    }
}

