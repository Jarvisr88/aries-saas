namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Runtime.CompilerServices;

    public class EnumValueBox<T> : SimpleValueBox<T>, IEnumValueViewModel<T>, IEnumValueViewModel, IBaseCollectionValueViewModel, IValueViewModel, ISimpleValueViewModel<T>, IFilterValueViewModel, IUniqueValuesViewModel where T: struct
    {
        private static readonly IReadOnlyCollection<T> UnsetValues;
        private static readonly object valuesKey;
        private const string EnumDataSource = "#EnumDataSource";

        static EnumValueBox();
        protected sealed override bool CanResetCore();
        CriteriaOperator IFilterValueViewModel.CreateFilterCriteria();
        private CriteriaOperator GetBitwiseCriteria(OperandProperty prop, UnaryOperator isNull, T xorRes);
        private static T? GetEnum(OperandValue value, Type enumType);
        private static T[] GetEnumValues(T value, bool useFlags);
        private bool IsEqualsGroup(GroupOperator group);
        private bool IsUnaryGroup(GroupOperator group);
        protected sealed override void OnInitialized();
        protected sealed override void OnMetricAttributesSpecialMemberChanged(string propertyName);
        protected void OnValuesChanged();
        protected sealed override void ResetCore();
        private void SetValues(IEnumerable<T> values);
        private void SetValues(T? value);
        private bool TryParseBinary(string path, CriteriaOperator criteria);
        protected sealed override bool TryParseCore(IEndUserFilteringMetric metric, CriteriaOperator criteria);
        private bool TryParseIn(string path, CriteriaOperator criteria);
        private static T Xor(Type enumType, IEnumerable<T> values);
        [IteratorStateMachine(typeof(EnumValueBox<>.<XorValues>d__37))]
        private static IEnumerable<T> XorValues(Type enumType, object xorValue);

        public override T? Value { get; set; }

        public virtual IReadOnlyCollection<T> Values { get; set; }

        protected IEnumChoiceMetricAttributes<T> MetricAttributes { get; }

        bool IUniqueValuesViewModel.HasValues { get; }

        object IUniqueValuesViewModel.Values { get; }

        [Browsable(false)]
        public Type EnumType { get; }

        [Browsable(false)]
        public bool UseFlags { get; }

        [Browsable(false)]
        public bool UseContainsForFlags { get; }

        [Browsable(false)]
        public bool UseRadioSelection { get; }

        [Browsable(false)]
        public bool UseSelectAll { get; }

        [Browsable(false)]
        public string SelectAllName { get; }

        [Browsable(false)]
        public string NullName { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly EnumValueBox<T>.<>c <>9;
            public static Func<T, OperandValue> <>9__34_0;
            public static Func<CriteriaOperator, bool> <>9__40_0;

            static <>c();
            internal OperandValue <DevExpress.Utils.Filtering.Internal.IFilterValueViewModel.CreateFilterCriteria>b__34_0(T v);
            internal bool <IsEqualsGroup>b__40_0(CriteriaOperator operand);
        }

        [CompilerGenerated]
        private sealed class <XorValues>d__37 : IEnumerable<T>, IEnumerable, IEnumerator<T>, IDisposable, IEnumerator
        {
            private int <>1__state;
            private T <>2__current;
            private int <>l__initialThreadId;
            private Type enumType;
            public Type <>3__enumType;
            private object xorValue;
            public object <>3__xorValue;
            private Type <underlyingType>5__1;
            private long <xorValueLong>5__2;
            private HashSet<T>.Enumerator <>7__wrap1;

            [DebuggerHidden]
            public <XorValues>d__37(int <>1__state);
            private void <>m__Finally1();
            private bool MoveNext();
            [DebuggerHidden]
            IEnumerator<T> IEnumerable<T>.GetEnumerator();
            [DebuggerHidden]
            IEnumerator IEnumerable.GetEnumerator();
            [DebuggerHidden]
            void IEnumerator.Reset();
            [DebuggerHidden]
            void IDisposable.Dispose();

            T IEnumerator<T>.Current { [DebuggerHidden] get; }

            object IEnumerator.Current { [DebuggerHidden] get; }
        }
    }
}

