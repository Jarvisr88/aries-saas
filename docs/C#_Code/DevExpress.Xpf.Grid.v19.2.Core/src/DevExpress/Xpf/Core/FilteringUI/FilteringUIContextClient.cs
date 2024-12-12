namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using System;

    internal sealed class FilteringUIContextClient
    {
        private readonly Func<CriteriaOperator> getFilter;
        private readonly Action<CriteriaOperator> setFilter;
        internal readonly Func<CriteriaOperator, CriteriaOperator> SubstituteFilter;
        internal readonly Func<string, FilteringColumn> GetColumn;
        internal readonly Func<ColumnForestFilterMode, IEnumerable<Tree<VisualFilteringColumn, HeaderAppearanceAccessor>>> GetColumnForest;
        internal readonly Func<AllowedGroupFilters> GetAllowedGroupFilters;
        internal readonly Func<bool> GetAllowCustomExpressionInFilterEditor;
        internal readonly Func<IEnumerable<FormatConditionFilter>> GetFormatConditionFilters;
        internal readonly Func<FormatConditionFilterInfo, CriteriaOperator> SubstituteTopBottomFilter;

        internal FilteringUIContextClient(Func<CriteriaOperator> getFilter, Action<CriteriaOperator> setFilter, Func<CriteriaOperator, CriteriaOperator> substituteFilter, Func<string, FilteringColumn> getColumn, Func<ColumnForestFilterMode, IEnumerable<Tree<VisualFilteringColumn, HeaderAppearanceAccessor>>> getColumnForest, Func<AllowedGroupFilters> getAllowedGroupFilters, Func<bool> getAllowCustomExpressionInFilterEditor, Func<IEnumerable<FormatConditionFilter>> getFormatConditionFilters, Func<FormatConditionFilterInfo, CriteriaOperator> substituteTopBottomFilter)
        {
            this.getFilter = getFilter;
            this.setFilter = setFilter;
            this.SubstituteFilter = substituteFilter;
            this.GetColumn = getColumn;
            this.GetColumnForest = getColumnForest;
            this.GetAllowedGroupFilters = getAllowedGroupFilters;
            this.GetAllowCustomExpressionInFilterEditor = getAllowCustomExpressionInFilterEditor;
            this.GetFormatConditionFilters = getFormatConditionFilters;
            this.SubstituteTopBottomFilter = substituteTopBottomFilter;
        }

        internal CriteriaOperator Filter
        {
            get => 
                this.getFilter();
            set => 
                this.setFilter(value);
        }
    }
}

