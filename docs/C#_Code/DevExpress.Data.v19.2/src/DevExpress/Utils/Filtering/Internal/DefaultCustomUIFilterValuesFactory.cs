namespace DevExpress.Utils.Filtering.Internal
{
    using System;
    using System.Collections.Generic;
    using System.Diagnostics;
    using System.Runtime.InteropServices;

    internal class DefaultCustomUIFilterValuesFactory : ICustomUIFilterValuesFactory
    {
        internal static readonly ICustomUIFilterValuesFactory Instance = new DefaultCustomUIFilterValuesFactory();

        private DefaultCustomUIFilterValuesFactory()
        {
        }

        ICustomUIFilterValue ICustomUIFilterValuesFactory.Create(CustomUIFilterType filterType, params object[] values)
        {
            switch (filterType)
            {
                case CustomUIFilterType.Custom:
                case CustomUIFilterType.AllDatesInThePeriod:
                case CustomUIFilterType.DatePeriods:
                    return new CustomUIFilterValue(filterType, GetValue(values, null), GetChildren(values));

                case CustomUIFilterType.Equals:
                case CustomUIFilterType.DoesNotEqual:
                case CustomUIFilterType.GreaterThan:
                case CustomUIFilterType.GreaterThanOrEqualTo:
                case CustomUIFilterType.LessThan:
                case CustomUIFilterType.LessThanOrEqualTo:
                    return new CustomUIFilterValue(filterType, GetValue(values, null), null);

                case CustomUIFilterType.Between:
                    return new CustomUIFilterValue(filterType, GetValues(values, null), null);

                case CustomUIFilterType.IsNull:
                case CustomUIFilterType.IsNotNull:
                case CustomUIFilterType.IsBlank:
                case CustomUIFilterType.IsNotBlank:
                    return new CustomUIFilterValue(filterType, KnownValues.NullOrBlank, null);

                case CustomUIFilterType.TopN:
                case CustomUIFilterType.BottomN:
                    return new CustomUIFilterValue(filterType, ValuesOrKnown(values, KnownValues.Sequence), null);

                case CustomUIFilterType.AboveAverage:
                case CustomUIFilterType.BelowAverage:
                    return new CustomUIFilterValue(filterType, ValueOrKnown(values, KnownValues.Aggregate), null);

                case CustomUIFilterType.BeginsWith:
                case CustomUIFilterType.EndsWith:
                case CustomUIFilterType.DoesNotBeginsWith:
                case CustomUIFilterType.DoesNotEndsWith:
                case CustomUIFilterType.Contains:
                case CustomUIFilterType.DoesNotContain:
                    return new CustomUIFilterValue(filterType, GetValue(values, null), null);

                case CustomUIFilterType.Like:
                case CustomUIFilterType.NotLike:
                    return new CustomUIFilterValue(filterType, GetValue(values, null), null);

                case CustomUIFilterType.Before:
                case CustomUIFilterType.After:
                    return new CustomUIFilterValue(filterType, GetValue(values, null), null);

                case CustomUIFilterType.Yesterday:
                case CustomUIFilterType.Today:
                case CustomUIFilterType.Tomorrow:
                case CustomUIFilterType.LastWeek:
                case CustomUIFilterType.ThisWeek:
                case CustomUIFilterType.NextWeek:
                case CustomUIFilterType.LastMonth:
                case CustomUIFilterType.ThisMonth:
                case CustomUIFilterType.NextMonth:
                case CustomUIFilterType.LastYear:
                case CustomUIFilterType.ThisYear:
                case CustomUIFilterType.NextYear:
                case CustomUIFilterType.YearToDate:
                case CustomUIFilterType.January:
                case CustomUIFilterType.February:
                case CustomUIFilterType.March:
                case CustomUIFilterType.April:
                case CustomUIFilterType.May:
                case CustomUIFilterType.June:
                case CustomUIFilterType.July:
                case CustomUIFilterType.August:
                case CustomUIFilterType.September:
                case CustomUIFilterType.October:
                case CustomUIFilterType.November:
                case CustomUIFilterType.December:
                    return new CustomUIFilterValue(filterType, KnownValues.Date, null);

                case CustomUIFilterType.LastQuarter:
                case CustomUIFilterType.ThisQuarter:
                case CustomUIFilterType.NextQuarter:
                    return new CustomUIFilterValue(filterType, KnownValues.BaseDate, null);

                case CustomUIFilterType.Quarter1:
                case CustomUIFilterType.Quarter2:
                case CustomUIFilterType.Quarter3:
                case CustomUIFilterType.Quarter4:
                    return new CustomUIFilterValue(filterType, KnownValues.DatePart, null);

                case CustomUIFilterType.IsSameDay:
                    return new CustomUIFilterValue(filterType, GetValue(values, null), null);

                case CustomUIFilterType.User:
                    return new CustomUIFilterValue(filterType, ValueOrKnown(values, KnownValues.User), GetChildren(values));
            }
            return new CustomUIFilterValue(filterType, null, null);
        }

        private static IEnumerable<ICustomUIFilterValue> GetChildren(object[] values) => 
            ((values == null) || (values.Length <= 1)) ? null : (values[1] as IEnumerable<ICustomUIFilterValue>);

        private static object GetValue(object[] values, object defaultValue = null) => 
            ((values == null) || (values.Length == 0)) ? defaultValue : values[0];

        private static object GetValues(object[] values, object defaultValue = null) => 
            ((values == null) || (values.Length == 0)) ? defaultValue : values;

        private static object ValueOrKnown(object[] values, KnownValues defaultValue) => 
            GetValue(values, defaultValue);

        private static object ValuesOrKnown(object[] values, KnownValues defaultValue) => 
            GetValues(values, defaultValue);

        [DebuggerDisplay("{FilterType}, {Value}")]
        private sealed class CustomUIFilterValue : ICustomUIFilterValue, IEquatable<ICustomUIFilterValue>, IEqualityComparer<ICustomUIFilterValue>
        {
            private readonly CustomUIFilterType filterTypeCore;
            private readonly object valueCore;
            private readonly IEnumerable<ICustomUIFilterValue> childrenCore;

            public CustomUIFilterValue(CustomUIFilterType filterType, object value = null, IEnumerable<ICustomUIFilterValue> children = null)
            {
                this.filterTypeCore = filterType;
                this.valueCore = value;
                this.childrenCore = children;
            }

            bool IEqualityComparer<ICustomUIFilterValue>.Equals(ICustomUIFilterValue x, ICustomUIFilterValue y) => 
                !ReferenceEquals(x, y) ? ((x != null) && ((y != null) && x.Equals(y))) : true;

            int IEqualityComparer<ICustomUIFilterValue>.GetHashCode(ICustomUIFilterValue value) => 
                (int) this.filterTypeCore;

            bool IEquatable<ICustomUIFilterValue>.Equals(ICustomUIFilterValue value) => 
                (value != null) && ((this.FilterType == value.FilterType) && ((this.HasChildren == value.HasChildren) && ((this.Value != value.Value) ? ((this.Value != null) && ((value.Value != null) && Equals(this.Value, value.Value))) : true)));

            public CustomUIFilterType FilterType =>
                this.filterTypeCore;

            public string FilterName =>
                LocalizableUIElement<CustomUIFilterType>.GetName(this.FilterType);

            public string FilterDescription =>
                LocalizableUIElement<CustomUIFilterType>.GetDescription(this.FilterType);

            public object Value =>
                this.valueCore;

            public bool IsDefault =>
                (this.valueCore == null) || UniqueValues.IsNotLoaded(this.valueCore);

            public bool HasChildren =>
                this.childrenCore != null;

            public IEnumerable<ICustomUIFilterValue> Children =>
                this.childrenCore;
        }
    }
}

