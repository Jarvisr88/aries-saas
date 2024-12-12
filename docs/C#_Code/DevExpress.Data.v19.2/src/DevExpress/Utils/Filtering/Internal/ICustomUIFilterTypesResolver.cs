namespace DevExpress.Utils.Filtering.Internal
{
    using System.Collections.Generic;

    public interface ICustomUIFilterTypesResolver
    {
        IEnumerable<CustomUIFilterType> Resolve(IEndUserFilteringMetric metric, CustomUIFiltersType filtersType, ICustomUIFiltersOptions option);
    }
}

