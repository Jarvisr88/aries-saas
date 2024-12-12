namespace DevExpress.Data.Helpers
{
    using System;

    public class FilterDataOutlookDateHelper : OutlookDateHelper
    {
        private BaseFilterData data;

        public FilterDataOutlookDateHelper(BaseFilterData data);

        protected override DateTime SortZeroTime { get; }

        protected override DateTime SortStartWeek { get; }
    }
}

