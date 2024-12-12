namespace DevExpress.Data.Async
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;

    public class CommandApply : Command
    {
        public CriteriaOperator FilterCriteria;
        public ICollection<ServerModeOrderDescriptor[]> SortInfo;
        public int GroupCount;
        public ICollection<ServerModeSummaryDescriptor> GroupSummaryInfo;
        public ICollection<ServerModeSummaryDescriptor> TotalSummaryInfo;

        public CommandApply(CriteriaOperator filterCriteria, ICollection<ServerModeOrderDescriptor[]> sortInfo, int groupCount, ICollection<ServerModeSummaryDescriptor> summaryInfo, ICollection<ServerModeSummaryDescriptor> totalSummaryInfo, params DictionaryEntry[] tags);
        public override void Accept(IAsyncCommandVisitor visitor);
    }
}

