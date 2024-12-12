namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    public class CustomPrepareChildrenEventArgs : EventArgs
    {
        private CriteriaOperator groupWhere;
        private ServerModeOrderDescriptor groupByDescriptor;
        private ServerModeSummaryDescriptor[] summaries;

        public CustomPrepareChildrenEventArgs(CriteriaOperator groupWhere, ServerModeOrderDescriptor groupByDescriptor, ServerModeSummaryDescriptor[] summaries);

        public bool Handled { get; set; }

        public ServerModeGroupInfoData[] Result { get; set; }

        public CriteriaOperator GroupWhere { get; }

        public ServerModeOrderDescriptor GroupByDescriptor { get; }

        public ServerModeSummaryDescriptor[] Summaries { get; }
    }
}

