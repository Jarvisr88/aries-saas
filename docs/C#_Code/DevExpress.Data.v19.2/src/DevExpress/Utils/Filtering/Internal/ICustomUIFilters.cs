namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Reflection;
    using System.Runtime.InteropServices;

    public interface ICustomUIFilters : ILocalizableUIElement<CustomUIFiltersType>, IEnumerable<ICustomUIFilter>, IEnumerable
    {
        bool AllowFilter(ICustomUIFilter filter);
        bool ApplyFilterCriteria(CriteriaOperator criteria, out ICustomUIFilter filter);
        bool CanReset();
        void Reset();

        ICustomUIFilter this[CustomUIFilterType filterType] { get; }

        IEnumerable<IGrouping<string, ICustomUIFilter>> Groups { get; }

        ICustomUIFiltersOptions Options { get; }

        ICustomUIFiltersOptions UserOptions { get; set; }

        IEndUserFilteringMetric Metric { get; }

        ICustomUIFilter ActiveFilter { get; }

        CriteriaOperator FilterCriteria { get; }
    }
}

