namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    public class GridControlApplyServerWrapper : IApplyServerWrapper
    {
        private readonly SyncVisibleListWrapper wrapper;
        private readonly IDataControllerOwner owner;
        private CriteriaOperator clientFilterCriteria;
        private ICollection<ServerModeOrderDescriptor[]> clientSortInfo;
        private int clientGroupCount;
        private ICollection<ServerModeSummaryDescriptor> clientGroupSummaryInfo;
        private ICollection<ServerModeSummaryDescriptor> clientTotalSummaryInfo;
        private CriteriaOperator viewFilterCriteria;

        public GridControlApplyServerWrapper(SyncVisibleListWrapper wrapper, IDataControllerOwner owner)
        {
            this.wrapper = wrapper;
            this.owner = owner;
        }

        public void ApplyControlSettings(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo)
        {
            this.clientFilterCriteria = filterCriteria;
            this.clientSortInfo = sortInfo;
            this.clientGroupCount = groupCount;
            this.clientGroupSummaryInfo = groupSummaryInfo;
            this.clientTotalSummaryInfo = totalSummaryInfo;
            this.UpdateWrapper();
        }

        public void ApplyViewSettings(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo)
        {
            this.viewFilterCriteria = filterCriteria;
            this.UpdateWrapper();
            this.owner.Update();
        }

        private void UpdateWrapper()
        {
            this.wrapper.Wrapper.ApplySortGroupFilter(CriteriaOperator.And(this.clientFilterCriteria, this.viewFilterCriteria), this.clientSortInfo, this.clientGroupCount, this.clientGroupSummaryInfo, this.clientTotalSummaryInfo);
        }
    }
}

