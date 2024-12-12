namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Async;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class AsyncComboBoxApplyServerWrapper : IAsyncApplyServerWrapper
    {
        private readonly AsyncVisibleListWrapper wrapper;

        public AsyncComboBoxApplyServerWrapper(AsyncVisibleListWrapper wrapper)
        {
            this.wrapper = wrapper;
        }

        public CommandApply ApplyControlSettings(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags)
        {
            throw new InvalidOperationException();
        }

        public CommandApply ApplyViewSettings(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags) => 
            this.wrapper.Wrapper.ApplySortGroupFilter(filterCriteria, sortInfo, groupCount, groupSummaryInfo, totalSummaryInfo, tags);
    }
}

