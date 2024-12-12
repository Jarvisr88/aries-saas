namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Runtime.CompilerServices;

    internal static class PredefinedFormatConditionFilterExtensions
    {
        internal static bool IsPredefinedFormatCondition(this ConditionFilterType type) => 
            (type == ConditionFilterType.TopItems) || ((type == ConditionFilterType.TopPercent) || ((type == ConditionFilterType.BottomItems) || ((type == ConditionFilterType.BottomPercent) || ((type == ConditionFilterType.AboveAverage) || ((type == ConditionFilterType.BelowAverage) || ((type == ConditionFilterType.Unique) || (type == ConditionFilterType.Duplicate)))))));

        internal static AllowedDataAnalysisFilters ToAllowedDataAnalysisFilters(this PredefinedFormatConditionType type)
        {
            switch (type)
            {
                case PredefinedFormatConditionType.Top:
                    return AllowedDataAnalysisFilters.Top;

                case PredefinedFormatConditionType.Bottom:
                    return AllowedDataAnalysisFilters.Bottom;

                case PredefinedFormatConditionType.AboveAverage:
                    return AllowedDataAnalysisFilters.AboveAverage;

                case PredefinedFormatConditionType.BelowAverage:
                    return AllowedDataAnalysisFilters.BelowAverage;

                case PredefinedFormatConditionType.Unique:
                    return AllowedDataAnalysisFilters.Unique;

                case PredefinedFormatConditionType.Duplicate:
                    return AllowedDataAnalysisFilters.Duplicate;
            }
            throw new ArgumentOutOfRangeException("type", type, null);
        }

        public static ConditionFilterType ToConditionFilterType(this PredefinedFormatConditionType type, TopBottomValueType unit)
        {
            switch (type)
            {
                case PredefinedFormatConditionType.Top:
                    return ((unit == TopBottomValueType.Items) ? ConditionFilterType.TopItems : ConditionFilterType.TopPercent);

                case PredefinedFormatConditionType.Bottom:
                    return ((unit == TopBottomValueType.Items) ? ConditionFilterType.BottomItems : ConditionFilterType.BottomPercent);

                case PredefinedFormatConditionType.AboveAverage:
                    return ConditionFilterType.AboveAverage;

                case PredefinedFormatConditionType.BelowAverage:
                    return ConditionFilterType.BelowAverage;

                case PredefinedFormatConditionType.Unique:
                    return ConditionFilterType.Unique;

                case PredefinedFormatConditionType.Duplicate:
                    return ConditionFilterType.Duplicate;
            }
            throw new InvalidOperationException();
        }

        public static PredefinedFormatConditionType ToPredefinedFormatConditionType(this ConditionFilterType type)
        {
            switch (type)
            {
                case ConditionFilterType.TopItems:
                case ConditionFilterType.TopPercent:
                    return PredefinedFormatConditionType.Top;

                case ConditionFilterType.BottomItems:
                case ConditionFilterType.BottomPercent:
                    return PredefinedFormatConditionType.Bottom;

                case ConditionFilterType.AboveAverage:
                    return PredefinedFormatConditionType.AboveAverage;

                case ConditionFilterType.BelowAverage:
                    return PredefinedFormatConditionType.BelowAverage;

                case ConditionFilterType.Unique:
                    return PredefinedFormatConditionType.Unique;

                case ConditionFilterType.Duplicate:
                    return PredefinedFormatConditionType.Duplicate;
            }
            throw new InvalidOperationException();
        }
    }
}

