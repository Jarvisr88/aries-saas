namespace DevExpress.XtraEditors.Helpers
{
    using DevExpress.Data.Filtering;
    using DevExpress.XtraEditors;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class DateFilterResult
    {
        private const FilterDateType DefaultFilterType = FilterDateType.None;
        private FilterDateType filterType;
        private string filterDisplayText;
        private DateTime startDate;
        private DateTime endDate;
        private CriteriaOperator filterCriteria;
        private List<CriteriaOperator> userFilters;

        public DateFilterResult();
        public DateFilterResult(FilterDateType filterType);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public void SetDates(DateTime startDate, DateTime endDate);

        public List<CriteriaOperator> UserFilters { get; }

        public FilterDateType FilterType { get; internal set; }

        public string FilterDisplayText { get; internal set; }

        public DateTime StartDate { get; internal set; }

        public DateTime EndDate { get; internal set; }

        public CriteriaOperator FilterCriteria { get; set; }
    }
}

