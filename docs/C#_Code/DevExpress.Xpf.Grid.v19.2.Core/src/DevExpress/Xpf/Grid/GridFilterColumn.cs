namespace DevExpress.Xpf.Grid
{
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Xpf.Editors.Filtering;
    using DevExpress.Xpf.Editors.Settings;
    using DevExpress.Xpf.Grid.Native;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class GridFilterColumn : FilterColumn
    {
        private bool useFilterClauseHelper = true;
        public static List<ClauseType> DomainDataSourceClauses;

        static GridFilterColumn()
        {
            List<ClauseType> list1 = new List<ClauseType>();
            list1.Add(ClauseType.Contains);
            list1.Add(ClauseType.EndsWith);
            list1.Add(ClauseType.BeginsWith);
            list1.Add(ClauseType.Equals);
            list1.Add(ClauseType.Greater);
            list1.Add(ClauseType.GreaterOrEqual);
            list1.Add(ClauseType.Less);
            list1.Add(ClauseType.LessOrEqual);
            list1.Add(ClauseType.DoesNotEqual);
            DomainDataSourceClauses = list1;
        }

        public GridFilterColumn(ColumnBase column, bool useDomainDataSourceRestrictions, bool useWcfSource)
        {
            this.GridColumn = column;
            this.UseDomainDataSourceRestrictions = useDomainDataSourceRestrictions;
            this.UseWcfSource = useWcfSource;
        }

        public override ClauseType GetDefaultOperation()
        {
            ClauseType? nullable = FilterClauseHelper.GetDefaultOperation(this.GridColumn, true, false);
            return ((nullable != null) ? nullable.GetValueOrDefault() : ClauseType.Equals);
        }

        public override bool IsValidClause(ClauseType clause)
        {
            if (this.UseFilterClauseHelper && !FilterClauseHelper.IsClauseEnabled(this.GridColumn, clause))
            {
                return false;
            }
            bool flag = base.IsValidClause(clause);
            return (!this.UseDomainDataSourceRestrictions ? ((!this.UseWcfSource || ((clause != ClauseType.Like) && ((clause != ClauseType.NotLike) && ((clause != ClauseType.IsToday) && ((clause != ClauseType.IsYesterday) && ((clause != ClauseType.IsLastWeek) && ((clause != ClauseType.IsPriorThisYear) && ((clause != ClauseType.IsTomorrow) && ((clause != ClauseType.IsNextWeek) && ((clause != ClauseType.IsBeyondThisYear) && ((clause != ClauseType.IsEarlierThisWeek) && ((clause != ClauseType.IsEarlierThisMonth) && ((clause != ClauseType.IsEarlierThisYear) && ((clause != ClauseType.IsLaterThisWeek) && ((clause != ClauseType.IsLaterThisMonth) && (clause != ClauseType.IsLaterThisYear)))))))))))))))) ? flag : false) : (DomainDataSourceClauses.Contains(clause) && flag));
        }

        internal ColumnBase GridColumn { get; set; }

        private bool UseDomainDataSourceRestrictions { get; set; }

        private bool UseWcfSource { get; set; }

        internal bool UseFilterClauseHelper
        {
            get => 
                this.useFilterClauseHelper;
            set => 
                this.useFilterClauseHelper = value;
        }

        public override FilterColumnClauseClass ClauseClass =>
            (this.GridColumn.ColumnFilterMode != ColumnFilterMode.DisplayText) ? (((this.GridColumn.EditSettings is ComboBoxEditSettings) || (this.GridColumn.EditSettings is ListBoxEditSettings)) ? FilterColumnClauseClass.Lookup : (((this.GridColumn.EditSettings is ImageEditSettings) || (this.ColumnType == typeof(byte[]))) ? FilterColumnClauseClass.Blob : (!(this.ColumnType == typeof(string)) ? (((this.ColumnType == typeof(DateTime)) || (this.ColumnType == typeof(DateTime?))) ? FilterColumnClauseClass.DateTime : FilterColumnClauseClass.Generic) : FilterColumnClauseClass.String))) : FilterColumnClauseClass.String;
    }
}

