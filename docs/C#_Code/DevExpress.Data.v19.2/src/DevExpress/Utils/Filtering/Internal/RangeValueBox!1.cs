namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class RangeValueBox<T> : ValueViewModel, IRangeValueViewModel<T>, IRangeValueViewModel, IValueViewModel, IFilterValueViewModel, IUniqueValuesViewModel where T: struct
    {
        private static readonly IReadOnlyCollection<Interval<T>> UnspecifiedIntervals;
        private static readonly object intervalsKey;
        private static readonly object fromValueKey;
        private static readonly object toValueKey;
        private RangeValueBox<T>.ParsedRangeType? parsedType;
        private bool? parsedExactStateChanged;
        private static readonly bool IsDateTime;
        private static readonly bool IsTimeSpan;

        static RangeValueBox();
        protected sealed override bool CanResetCore();
        internal static bool CompareAndCheck(ref T? fromValue, ref T? toValue);
        private CriteriaOperator CreateDateIntervalsCriteria();
        private CriteriaOperator CreateIntervalsCriteria();
        CriteriaOperator IFilterValueViewModel.CreateFilterCriteria();
        protected IReadOnlyCollection<Interval<T>> GetRangeIntervals();
        protected bool HasParsedRange(out RangeValueBox<T>.ParsedRangeType type);
        protected override void Initialize(Action setValues);
        protected sealed override bool InitializeWithNull(bool useInversion);
        protected sealed override bool InitializeWithValues(object[] uniqueAndSortedValues, bool useInversion);
        private bool IsBinaryGroup(GroupOperator group);
        private bool IsGreater(BinaryOperator binary);
        private bool IsGreaterThanMaximum(T? val);
        private bool IsIntervals(GroupOperator group);
        private bool IsLess(BinaryOperator binary);
        private bool IsLessThanMinimum(T? val);
        private bool IsOrdered(OperandValue first, OperandValue second);
        private bool IsSameDay(FunctionOperator functionOperator);
        private static IReadOnlyCollection<Interval<T>> Merge(IReadOnlyCollection<Interval<T>> intervals);
        protected virtual void OnFromValueChanged();
        protected override void OnInitialized();
        protected void OnIntervalsChanged();
        protected sealed override bool OnMetricAttributesChanged(string propertyName);
        protected sealed override void OnMetricAttributesSpecialMemberChanged(string propertyName);
        protected virtual void OnToValueChanged();
        protected virtual void OnUniqueValuesChanged();
        private void RaiseParsedExactStateChanged();
        protected override void ResetCore();
        private void SetEqual(OperandValue value);
        private void SetEqual(T? value);
        private void SetGreaterThan(bool isGroup, OperandValue value, bool inclusive);
        private void SetIntervals(IReadOnlyCollection<Interval<T>> intervals);
        protected virtual void SetIsNull();
        private void SetLessThan(bool isGroup, OperandValue value, bool inclusive);
        private void SetParsedRangeSpecifics(RangeValueBox<T>.ParsedRangeType? type, bool? exact, bool? exactFrom = new bool?(), bool? exactTo = new bool?(), bool notify = true);
        private void SetRange(T from, T to);
        private void SetRange(OperandValue from, OperandValue to, bool exclusiveFrom, bool exclusiveTo);
        private void SetRangeCore(Interval<T> range);
        private void SetRangeInterval(Interval<T> range);
        private static void TryAddDay(List<Interval<T>> intervals, OperandValue day);
        private bool TryGetRange(OperandValue from, OperandValue to, bool exclusiveFrom, bool exclusiveTo, out Interval<T> range);
        private bool TryGetRange(T? fromValue, T? toValue, bool exclusiveFrom, bool exclusiveTo, out Interval<T> range);
        private static bool TryGetValue(OperandValue value, out T? result);
        private void TryNotifyIntervals();
        private bool TryParseBetween(string path, CriteriaOperator left, CriteriaOperator right);
        private bool TryParseBinary(string path, CriteriaOperator criteria, bool isGroup = true);
        private bool TryParseBinaryGroup(string path, BinaryOperator first, BinaryOperator second);
        protected sealed override bool TryParseCore(IEndUserFilteringMetric metric, CriteriaOperator criteria);
        private bool TryParseIntervals(string path, GroupOperator group);
        private bool TryParseIsNull(string path, CriteriaOperator criteria);
        private bool TryParseIsSameDay(string path, FunctionOperator functionOperator);
        private bool TryParseValue(string path, CriteriaOperator criteria, BinaryOperatorType binaryOperatorType);
        private bool TryParseValueCore(string path, OperandValue value, BinaryOperatorType binaryOperatorType, bool isGroup);
        protected bool TryResetRangeIntervals();

        [Bindable(false), Browsable(false)]
        public bool HasIntervals { get; }

        [Bindable(false), Browsable(false)]
        protected bool IsRangeIntervalUnspecified { get; }

        public virtual IReadOnlyCollection<Interval<T>> Intervals { get; set; }

        public virtual T? FromValue { get; set; }

        public virtual T? ToValue { get; set; }

        protected IRangeMetricAttributes<T> MetricAttributes { get; }

        bool IUniqueValuesViewModel.HasValues { get; }

        object IUniqueValuesViewModel.Values { get; }

        bool IRangeValueViewModel.AllowNull { get; }

        [Browsable(false)]
        public T? Minimum { get; }

        [Browsable(false)]
        public T? Maximum { get; }

        [Browsable(false)]
        public virtual T? Average { get; }

        [Browsable(false)]
        public string FromName { get; }

        [Browsable(false)]
        public string ToName { get; }

        [Browsable(false)]
        public string NullName { get; }

        [Browsable(false)]
        public bool? ParsedExact { get; private set; }

        [Browsable(false)]
        public bool? ParsedExactFrom { get; private set; }

        [Browsable(false)]
        public bool? ParsedExactTo { get; private set; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly RangeValueBox<T>.<>c <>9;
            public static Func<CriteriaOperator, bool> <>9__20_0;

            static <>c();
            internal bool <IsSameDay>b__20_0(CriteriaOperator op);
        }

        protected enum ParsedRangeType
        {
            public const RangeValueBox<T>.ParsedRangeType None = RangeValueBox<T>.ParsedRangeType.None;,
            public const RangeValueBox<T>.ParsedRangeType Equal = RangeValueBox<T>.ParsedRangeType.Equal;,
            public const RangeValueBox<T>.ParsedRangeType Range = RangeValueBox<T>.ParsedRangeType.Range;,
            public const RangeValueBox<T>.ParsedRangeType GreaterThan = RangeValueBox<T>.ParsedRangeType.GreaterThan;,
            public const RangeValueBox<T>.ParsedRangeType LessThan = RangeValueBox<T>.ParsedRangeType.LessThan;
        }
    }
}

