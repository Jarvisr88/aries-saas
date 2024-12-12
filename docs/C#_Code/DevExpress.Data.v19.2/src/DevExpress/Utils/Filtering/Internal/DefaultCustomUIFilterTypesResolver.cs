namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    internal sealed class DefaultCustomUIFilterTypesResolver : ICustomUIFilterTypesResolver
    {
        internal static readonly ICustomUIFilterTypesResolver Instance = new DefaultCustomUIFilterTypesResolver();

        private DefaultCustomUIFilterTypesResolver()
        {
        }

        [IteratorStateMachine(typeof(<DevExpress-Utils-Filtering-Internal-ICustomUIFilterTypesResolver-Resolve>d__2))]
        IEnumerable<CustomUIFilterType> ICustomUIFilterTypesResolver.Resolve(IEndUserFilteringMetric metric, CustomUIFiltersType filtersType, ICustomUIFiltersOptions options)
        {
            if (options.DefaultFilterType == CustomUIFilterType.None)
            {
                yield return CustomUIFilterType.None;
            }
            yield return CustomUIFilterType.Equals;
            yield return CustomUIFilterType.DoesNotEqual;
            if (filtersType == CustomUIFiltersType.Numeric)
            {
                yield return CustomUIFilterType.GreaterThan;
                yield return CustomUIFilterType.GreaterThanOrEqualTo;
                yield return CustomUIFilterType.LessThan;
                yield return CustomUIFilterType.LessThanOrEqualTo;
                yield return CustomUIFilterType.Between;
                if (!IsRange(metric))
                {
                    if (options.FilterByDisplayText && options.ShowBlanks)
                    {
                        yield return CustomUIFilterType.IsBlank;
                        yield return CustomUIFilterType.IsNotBlank;
                    }
                }
                else
                {
                    if (options.ShowSequences)
                    {
                        yield return CustomUIFilterType.TopN;
                        yield return CustomUIFilterType.BottomN;
                    }
                    if (options.ShowAggregates)
                    {
                        yield return CustomUIFilterType.AboveAverage;
                        yield return CustomUIFilterType.BelowAverage;
                        goto Label_PostSwitchInIterator;
                    }
                    goto TR_0027;
                }
                if (options.FilterByDisplayText && options.ShowLikeFilters)
                {
                    yield return CustomUIFilterType.Like;
                    yield return CustomUIFilterType.NotLike;
                    goto Label_PostSwitchInIterator;
                }
                goto TR_0027;
            }
            goto TR_001D;
        Label_PostSwitchInIterator:;
            goto TR_0027;
        TR_0015:
            if ((filtersType != CustomUIFiltersType.Boolean) && options.ShowCustomFilters)
            {
                yield return CustomUIFilterType.Custom;
            }
            while (true)
            {
                if (options.ShowUserDefinedFilters)
                {
                    yield return CustomUIFilterType.User;
                }
            }
        TR_0018:
            if (filtersType != CustomUIFiltersType.Boolean)
            {
                goto TR_0015;
            }
            else if (TypeHelper.AllowNull(metric.Type) && options.ShowNulls)
            {
                yield return CustomUIFilterType.IsNull;
                yield return CustomUIFilterType.IsNotNull;
            }
            if (options.ShowUserDefinedFilters)
            {
                yield return CustomUIFilterType.User;
            }
            goto TR_0015;
        TR_001D:
            if (filtersType == CustomUIFiltersType.DateTime)
            {
                if (IsTimeSpan(metric))
                {
                    yield return CustomUIFilterType.GreaterThan;
                    yield return CustomUIFilterType.GreaterThanOrEqualTo;
                    yield return CustomUIFilterType.LessThan;
                    yield return CustomUIFilterType.LessThanOrEqualTo;
                    yield return CustomUIFilterType.Between;
                    if (TypeHelper.AllowNull(metric.Type) && options.ShowNulls)
                    {
                        yield return CustomUIFilterType.IsNull;
                        yield return CustomUIFilterType.IsNotNull;
                    }
                    if (options.ShowCustomFilters)
                    {
                        yield return CustomUIFilterType.Custom;
                    }
                    while (true)
                    {
                        if (options.ShowUserDefinedFilters)
                        {
                            yield return CustomUIFilterType.User;
                        }
                    }
                }
                yield return CustomUIFilterType.DatePeriods;
                yield return CustomUIFilterType.IsSameDay;
                yield return CustomUIFilterType.Before;
                yield return CustomUIFilterType.After;
                yield return CustomUIFilterType.Between;
                yield return CustomUIFilterType.Yesterday;
                yield return CustomUIFilterType.Today;
                yield return CustomUIFilterType.Tomorrow;
                yield return CustomUIFilterType.LastWeek;
                yield return CustomUIFilterType.ThisWeek;
                yield return CustomUIFilterType.NextWeek;
                yield return CustomUIFilterType.LastMonth;
                yield return CustomUIFilterType.ThisMonth;
                yield return CustomUIFilterType.NextMonth;
                yield return CustomUIFilterType.LastYear;
                yield return CustomUIFilterType.ThisYear;
                yield return CustomUIFilterType.NextYear;
                yield return CustomUIFilterType.YearToDate;
                yield return CustomUIFilterType.AllDatesInThePeriod;
                yield return CustomUIFilterType.Quarter1;
                yield return CustomUIFilterType.Quarter2;
                yield return CustomUIFilterType.Quarter3;
                yield return CustomUIFilterType.Quarter4;
                yield return CustomUIFilterType.January;
                yield return CustomUIFilterType.February;
                yield return CustomUIFilterType.March;
                yield return CustomUIFilterType.April;
                yield return CustomUIFilterType.May;
                yield return CustomUIFilterType.June;
                yield return CustomUIFilterType.July;
                yield return CustomUIFilterType.August;
                yield return CustomUIFilterType.September;
                yield return CustomUIFilterType.October;
                yield return CustomUIFilterType.November;
                yield return CustomUIFilterType.December;
                if (TypeHelper.AllowNull(metric.Type) && options.ShowNulls)
                {
                    yield return CustomUIFilterType.IsNull;
                    yield return CustomUIFilterType.IsNotNull;
                }
            }
            if (filtersType == CustomUIFiltersType.Text)
            {
                yield return CustomUIFilterType.BeginsWith;
                yield return CustomUIFilterType.DoesNotBeginsWith;
                yield return CustomUIFilterType.EndsWith;
                yield return CustomUIFilterType.DoesNotEndsWith;
                yield return CustomUIFilterType.Contains;
                yield return CustomUIFilterType.DoesNotContain;
                if (options.ShowComparisons)
                {
                    yield return CustomUIFilterType.GreaterThan;
                    yield return CustomUIFilterType.GreaterThanOrEqualTo;
                    yield return CustomUIFilterType.LessThan;
                    yield return CustomUIFilterType.LessThanOrEqualTo;
                    yield return CustomUIFilterType.Between;
                }
                if (options.ShowBlanks)
                {
                    yield return CustomUIFilterType.IsBlank;
                    yield return CustomUIFilterType.IsNotBlank;
                }
                if (options.ShowLikeFilters)
                {
                    yield return CustomUIFilterType.Like;
                    yield return CustomUIFilterType.NotLike;
                }
            }
            if (filtersType != CustomUIFiltersType.Enum)
            {
                goto TR_0018;
            }
            else if (options.ShowComparisons)
            {
                yield return CustomUIFilterType.GreaterThan;
                yield return CustomUIFilterType.GreaterThanOrEqualTo;
                yield return CustomUIFilterType.LessThan;
                yield return CustomUIFilterType.LessThanOrEqualTo;
                yield return CustomUIFilterType.Between;
            }
            if (TypeHelper.AllowNull(metric.Type) && options.ShowNulls)
            {
                yield return CustomUIFilterType.IsNull;
                yield return CustomUIFilterType.IsNotNull;
            }
            goto TR_0018;
        TR_0027:
            if (TypeHelper.AllowNull(metric.Type) && options.ShowNulls)
            {
                yield return CustomUIFilterType.IsNull;
                yield return CustomUIFilterType.IsNotNull;
            }
            goto TR_001D;
        }

        private static bool IsRange(IEndUserFilteringMetric metric) => 
            metric.AttributesTypeDefinition == typeof(IRangeMetricAttributes<>);

        private static bool IsTimeSpan(IEndUserFilteringMetric metric) => 
            MetricAttributes.IsTimeSpan(metric.Type);

    }
}

