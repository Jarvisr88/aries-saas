namespace DevExpress.Xpf.Editors.Helpers
{
    using DevExpress.Data.Async;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public interface IAsyncApplyServerWrapper
    {
        CommandApply ApplyControlSettings(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags);
        CommandApply ApplyViewSettings(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> groupSummaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags);
    }
}

