namespace DevExpress.XtraEditors
{
    using DevExpress.Data.Filtering;
    using System;
    using System.ComponentModel;

    public class FilterDateElement
    {
        private string caption;
        private string tooltip;
        private CriteriaOperator criteria;
        private bool _checked;
        private FilterDateType filterType;

        public FilterDateElement(string caption, string tooltip, CriteriaOperator criteria);
        [EditorBrowsable(EditorBrowsableState.Never)]
        public FilterDateElement(string caption, string tooltip, CriteriaOperator criteria, FilterDateType filterType);

        public FilterDateType FilterType { get; }

        public string Caption { get; set; }

        public string Tooltip { get; set; }

        public CriteriaOperator Criteria { get; }

        public bool Checked { get; set; }
    }
}

