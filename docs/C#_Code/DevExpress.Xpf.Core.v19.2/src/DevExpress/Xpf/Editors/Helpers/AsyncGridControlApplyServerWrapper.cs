namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Async;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class AsyncGridControlApplyServerWrapper : IAsyncApplyServerWrapper
    {
        private readonly AsyncVisibleListWrapper wrapper;
        private readonly IDataControllerOwner owner;
        private CriteriaOperator clientFilterCriteria;
        private ICollection<ServerModeOrderDescriptor[]> clientSortInfo;
        private int clientGroupCount;
        private ICollection<ServerModeSummaryDescriptor> clientGroupSummaryInfo;
        private ICollection<ServerModeSummaryDescriptor> clientTotalSummaryInfo;
        private CriteriaOperator viewFilterCriteria;

        public AsyncGridControlApplyServerWrapper(AsyncVisibleListWrapper wrapper, IDataControllerOwner owner)
        {
            this.wrapper = wrapper;
            this.owner = owner;
        }

        public CommandApply ApplyControlSettings(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags)
        {
            this.clientFilterCriteria = filterCriteria;
            this.clientSortInfo = sortInfo;
            this.clientGroupCount = groupCount;
            this.clientGroupSummaryInfo = groupSummaryInfo;
            this.clientTotalSummaryInfo = totalSummaryInfo;
            return this.UpdateWrapper();
        }

        public CommandApply ApplyViewSettings(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags)
        {
            this.viewFilterCriteria = filterCriteria;
            CommandApply apply = this.UpdateWrapper();
            this.owner.Update();
            return apply;
        }

        private CommandApply UpdateWrapper() => 
            this.wrapper.Wrapper.ApplySortGroupFilter(CriteriaOperator.And(this.clientFilterCriteria, this.viewFilterCriteria), this.clientSortInfo, this.clientGroupCount, this.clientGroupSummaryInfo, this.clientTotalSummaryInfo, new DictionaryEntry[0]);
    }
}

