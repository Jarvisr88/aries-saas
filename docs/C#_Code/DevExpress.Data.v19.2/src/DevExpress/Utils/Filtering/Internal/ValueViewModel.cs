namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Linq.Expressions;
    using System.Runtime.CompilerServices;

    public abstract class ValueViewModel : IValueViewModel, IEndUserFilteringCriteriaAwareViewModel
    {
        private bool isModifiedCore;
        private WeakReference viewModelsRef;
        private IMetricAttributes metricAttributes;
        private bool? initializedWithValues;
        private int lockSetIsModified;
        private EventHandler changedCore;
        private static readonly IValueViewModel[] EmptyValues;
        private readonly Hashtable valueHash;
        private Lazy<object[]> nullsCore;

        public event EventHandler Changed;

        static ValueViewModel();
        protected ValueViewModel();
        protected virtual bool AfterParseCore(IEndUserFilteringMetric metric, bool result);
        private void Attributes_PropertyChanged(object sender, PropertyChangedEventArgs e);
        protected virtual void BeforeParseCore(IEndUserFilteringMetric metric, CriteriaOperator criteria);
        public bool CanReset();
        public bool CanResetAll();
        public static bool CanResetAll(IEnumerable<IEndUserFilteringMetricViewModel> viewModels);
        protected abstract bool CanResetCore();
        bool IEndUserFilteringCriteriaAwareViewModel.TryParse(IEndUserFilteringMetric metric, CriteriaOperator criteria);
        void IValueViewModel.Initialize(IEndUserFilteringMetricViewModel metricViewModel);
        void IValueViewModel.Initialize(IEnumerable<IEndUserFilteringMetricViewModel> viewModels);
        void IValueViewModel.Release();
        protected void EnsureDataItemsLookup(string propertyName);
        private static IDisposable EnterFilterCriteriaChange(IEnumerable<IEndUserFilteringMetricViewModel> viewModels);
        private static IEnumerable<IValueViewModel> GetAllValues(IEnumerable<IEndUserFilteringMetricViewModel> viewModels);
        protected static int GetCount<TValue>(IEnumerable<TValue> values);
        public static object GetDataItemsLookup(IValueViewModel value);
        public static Type GetDataType(IValueViewModel viewModel);
        protected T? GetValue<T>(object valueKey) where T: struct;
        protected T GetValue<T>(object valueKey, T defaultValue) where T: class;
        protected IReadOnlyCollection<T> GetValues<T>(object valueKey, IReadOnlyCollection<T> defaultValue);
        protected bool HasNulls(IUniqueValuesMetricAttributes uniqueValues);
        protected bool HasValue(object valueKey);
        protected virtual void Initialize(Action setValues);
        protected void InitializeNulls<T>(IUniqueValuesMetricAttributes uniqueValues);
        protected virtual bool InitializeWithNull(bool useInversion);
        protected virtual bool InitializeWithValues(object[] uniqueAndSortedValues, bool useInversion);
        protected bool IsEmpty<T>(IEnumerable<T> value, IEnumerable<T> defaultValue);
        protected virtual void OnInitialized();
        protected void OnIsModifiedChanged();
        protected virtual bool OnMetricAttributesChanged(string propertyName);
        protected virtual void OnMetricAttributesMemberChanged(string propertyName);
        protected virtual void OnMetricAttributesSpecialMemberChanged(string propertyName);
        protected virtual bool OnParseReset();
        protected virtual void OnReleasing();
        protected void OnResetComplete();
        protected virtual bool ParseFilterCriteria(IEndUserFilteringMetric metric, CriteriaOperator criteria);
        protected void RaiseChanged();
        [Browsable(false)]
        public void Reset();
        protected void Reset<T>(object key, Expression<Func<T>> selector);
        protected void Reset<T>(object key, Expression<Func<T>> selector, object partialKey);
        [Browsable(false)]
        public void ResetAll();
        public static void ResetAll(IEnumerable<IEndUserFilteringMetricViewModel> viewModels);
        protected abstract void ResetCore();
        protected void ResetIsModified();
        protected bool ResetValue<T>(object valueKey, Expression<Func<T>> selector);
        protected bool ResetValueCore(object valueKey);
        protected void SetIsModified();
        private void SetValueCore<T>(object valueKey, object value, Expression<Func<T>> selector);
        private void SubscribeAttributes(IMetricAttributes attributes);
        protected internal bool TryInitializeFromParse(object[] values, bool useInversion);
        protected abstract bool TryParseCore(IEndUserFilteringMetric metric, CriteriaOperator criteria);
        protected bool TryParseProperty(string path, CriteriaOperator criteria);
        protected bool TryParseUnaryIsNull(string path, CriteriaOperator criteria, Func<bool> allowNull, Action setNull);
        protected bool TryResetValues<T>(IEnumerable<T> value, IEnumerable<T> defaultValue, object key, Expression<Func<IEnumerable<T>>> selector);
        protected bool TryResetValues<T>(IEnumerable<T> value, IEnumerable<T> defaultValue, object key, Expression<Func<IEnumerable<T>>> selector, object partialKey);
        protected bool TrySetValue<T>(object valueKey, object value, Expression<Func<T>> selector);
        protected bool TrySetValues<T>(object valuesKey, IEnumerable<T> values, Expression<Func<IEnumerable<T>>> selector);
        private void UnsubscribeAttributes(IMetricAttributes attributes);
        protected void UpdateIsModified(Func<bool> getValue);
        protected virtual void UpdateResetCommands();

        [Browsable(false)]
        public bool IsModified { get; protected set; }

        protected internal IEndUserFilteringMetricViewModel MetricViewModel { get; private set; }

        protected bool IsInitialized { get; }

        protected bool IsViewModelsInitialized { get; }

        protected IMetricAttributes MetricAttributes { get; }

        public bool IsInitializedWithValues { get; }

        protected virtual bool AllowNull { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ValueViewModel.<>c <>9;
            public static Func<bool> <>9__4_0;
            public static Func<bool> <>9__5_0;
            public static Func<EndUserFilteringMetricViewModel, IEndUserFilteringViewModel> <>9__72_0;
            public static Comparison<object> <>9__76_0;
            public static Func<ValueViewModel, IEndUserFilteringMetricViewModel> <>9__82_0;
            public static Func<IEndUserFilteringMetricViewModel, Type> <>9__82_1;
            public static Func<ValueViewModel, IMetricAttributes> <>9__83_0;
            public static Func<IDisplayMetricAttributes, object> <>9__83_1;
            public static Func<IEndUserFilteringMetricViewModel, bool> <>9__84_1;
            public static Func<IEnumerable<IEndUserFilteringMetricViewModel>, bool> <>9__84_0;
            public static Func<IEndUserFilteringCriteriaChangeAware, IDisposable> <>9__86_0;
            public static Func<IEndUserFilteringMetricViewModel, IValueViewModel> <>9__87_1;
            public static Func<IEnumerable<IEndUserFilteringMetricViewModel>, IEnumerable<IValueViewModel>> <>9__87_0;

            static <>c();
            internal bool <CanResetAll>b__84_0(IEnumerable<IEndUserFilteringMetricViewModel> all);
            internal bool <CanResetAll>b__84_1(IEndUserFilteringMetricViewModel x);
            internal IDisposable <EnterFilterCriteriaChange>b__86_0(IEndUserFilteringCriteriaChangeAware changeAware);
            internal IEnumerable<IValueViewModel> <GetAllValues>b__87_0(IEnumerable<IEndUserFilteringMetricViewModel> all);
            internal IValueViewModel <GetAllValues>b__87_1(IEndUserFilteringMetricViewModel x);
            internal IMetricAttributes <GetDataItemsLookup>b__83_0(ValueViewModel x);
            internal object <GetDataItemsLookup>b__83_1(IDisplayMetricAttributes a);
            internal IEndUserFilteringMetricViewModel <GetDataType>b__82_0(ValueViewModel x);
            internal Type <GetDataType>b__82_1(IEndUserFilteringMetricViewModel x);
            internal IEndUserFilteringViewModel <ParseFilterCriteria>b__72_0(EndUserFilteringMetricViewModel x);
            internal bool <ResetIsModified>b__5_0();
            internal bool <SetIsModified>b__4_0();
            internal int <TryInitializeFromParse>b__76_0(object x, object y);
        }
    }
}

