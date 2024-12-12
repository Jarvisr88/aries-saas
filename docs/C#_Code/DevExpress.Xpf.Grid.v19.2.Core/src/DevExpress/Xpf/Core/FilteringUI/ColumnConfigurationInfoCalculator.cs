namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Helpers;
    using DevExpress.Xpf.Editors.Settings;
    using System;
    using System.Linq;

    internal static class ColumnConfigurationInfoCalculator
    {
        public static FilterSelectorBuildInfo Calculate(FilterModelClient client)
        {
            FilteringColumn column = client.GetColumn();
            return ((column != null) ? new FilterSelectorBuildInfo(CalculateCategory(column), column.GetFilterRestrictions(), column.GetRoundDateTimeFilter(), client.GetFormatConditionFilters(), column.GetSustitutedPredefinedFilters(), column.Name, NullableHelpers.CanAcceptNull(column.Type)) : new FilterSelectorBuildInfo(OperatorMenuCategory.None, FilterRestrictions.None(), false, Enumerable.Empty<FormatConditionFilter>(), Enumerable.Empty<PredefinedFilter>(), null, false));
        }

        private static OperatorMenuCategory CalculateCategory(FilteringColumn column)
        {
            BaseEditSettings editSettings = column.GetEditSettings();
            return (((column.Type == typeof(bool?)) || (column.Type == typeof(bool))) ? OperatorMenuCategory.Boolean : (((editSettings is ComboBoxEditSettings) || (editSettings is ListBoxEditSettings)) ? OperatorMenuCategory.Selector : (((editSettings is ImageEditSettings) || (column.Type == typeof(byte[]))) ? OperatorMenuCategory.Object : (!(column.Type == typeof(string)) ? (((column.Type == typeof(DateTime?)) || (column.Type == typeof(DateTime))) ? OperatorMenuCategory.DateTime : OperatorMenuCategory.Numeric) : OperatorMenuCategory.String))));
        }
    }
}

