namespace DevExpress.Xpf.Editors
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.ObjectModel;

    public interface ISearchPanelColumnProviderBase
    {
        string GetSearchText();
        void UpdateColumns(FilterByColumnsMode mode);
        bool UpdateFilter(string searchText, FilterCondition filterCondition, CriteriaOperator filterCriteria);

        ObservableCollection<string> CustomColumns { get; set; }
    }
}

