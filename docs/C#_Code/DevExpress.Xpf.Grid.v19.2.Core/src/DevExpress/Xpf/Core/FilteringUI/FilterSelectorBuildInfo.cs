namespace DevExpress.Xpf.Core.FilteringUI
{
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    internal sealed class FilterSelectorBuildInfo
    {
        public FilterSelectorBuildInfo(OperatorMenuCategory category, FilterRestrictions restrictions, bool roundDateTime, IEnumerable<FormatConditionFilter> formatConditionFilters, IEnumerable<PredefinedFilter> predefinedFilters, string fieldName, bool isNullable)
        {
            this.<Category>k__BackingField = category;
            this.<Restrictions>k__BackingField = restrictions;
            this.<RoundDateTime>k__BackingField = roundDateTime;
            this.<FormatConditionFilters>k__BackingField = formatConditionFilters;
            this.<PredefinedFilters>k__BackingField = predefinedFilters;
            this.<FieldName>k__BackingField = fieldName;
            this.<IsNullable>k__BackingField = isNullable;
        }

        public OperatorMenuCategory Category { get; }

        public FilterRestrictions Restrictions { get; }

        public bool RoundDateTime { get; }

        public IEnumerable<FormatConditionFilter> FormatConditionFilters { get; }

        public IEnumerable<PredefinedFilter> PredefinedFilters { get; }

        public string FieldName { get; }

        public bool IsNullable { get; }
    }
}

