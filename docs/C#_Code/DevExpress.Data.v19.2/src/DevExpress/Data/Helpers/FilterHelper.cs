namespace DevExpress.Data.Helpers
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    public class FilterHelper : FilterHelperBase
    {
        public FilterHelper(DataController controller, VisibleListSourceRowCollection visibleListSourceRows);
        protected override IEnumerable<int> GetRowIndices(CriteriaOperator filter, bool ignoreAppliedFilter);

        public DataController Controller { get; }
    }
}

