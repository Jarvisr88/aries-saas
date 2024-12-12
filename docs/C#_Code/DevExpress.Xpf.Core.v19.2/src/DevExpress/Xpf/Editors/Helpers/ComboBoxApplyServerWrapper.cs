namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    public class ComboBoxApplyServerWrapper : IApplyServerWrapper
    {
        private readonly SyncVisibleListWrapper wrapper;

        public ComboBoxApplyServerWrapper(SyncVisibleListWrapper wrapper)
        {
            this.wrapper = wrapper;
        }

        public void ApplyControlSettings(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo)
        {
        }

        public void ApplyViewSettings(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo)
        {
            this.wrapper.Wrapper.ApplySortGroupFilter(filterCriteria, sortInfo, groupCount, groupSummaryInfo, totalSummaryInfo);
        }
    }
}

