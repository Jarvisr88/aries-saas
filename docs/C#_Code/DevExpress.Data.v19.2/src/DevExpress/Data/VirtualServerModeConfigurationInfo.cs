namespace DevExpress.Data
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.InteropServices;

    public class VirtualServerModeConfigurationInfo
    {
        private CriteriaOperator _Filter;
        private ServerModeOrderDescriptor[] _SortInfo;
        private ServerModeSummaryDescriptor[] _TotalSummary;

        public VirtualServerModeConfigurationInfo(CriteriaOperator filter, ServerModeOrderDescriptor[] sortInfo, ServerModeSummaryDescriptor[] totalSummary = null);

        public CriteriaOperator Filter { get; }

        public ServerModeOrderDescriptor[] SortInfo { get; }

        public ServerModeSummaryDescriptor[] TotalSummary { get; }
    }
}

