namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    public class CollectionValueBox<T> : ValueViewModel, ICollectionValueViewModel<T>, ICollectionValueViewModel, IBaseCollectionValueViewModel, IValueViewModel, ILookupValuesViewModel, IFilterValueViewModel, IUniqueValuesViewModel, ISupportInversion
    {
        private static readonly IReadOnlyCollection<T> UnsetValues;
        private static readonly IReadOnlyCollection<int> UnsetDisplayIndexes;
        private static readonly object valuesKey;
        private static readonly object displayIndexesKey;
        private static readonly object blanksKey;
        private static readonly bool IsDateTime;
        private bool? dataSourceLoaded;
        private readonly HashSet<object> valuesLookup;
        private readonly IDictionary<int, string> displayTextLookup;
        private HashSet<T> delayedInvertedValues;
        private HashSet<int> delayedInvertedIndexes;
        private Lazy<object> dataSourceCore;
        private LookupDataSource<T> lookupDataSourceCore;
        private bool? delayedInvertedBlanks;

        static CollectionValueBox();
        public CollectionValueBox();
        private bool AreAllDataSourceSelected(bool filterByText, int valuesCount, out bool useInversion);
        private bool AreAllUniqueValuesSelected(bool filterByText, int valuesCount, out bool useInversion);
        private bool AreAllValuesSelected(bool fromUnique, bool filterByText, out bool useInversion);
        private bool CanLoadData();
        public bool CanLoadFewer();
        public bool CanLoadMore();
        private bool CanParseIsNull();
        protected sealed override bool CanResetCore();
        CriteriaOperator IFilterValueViewModel.CreateFilterCriteria();
        private void EnsureDelayedInvertedDisplayIndexes();
        private void EnsureDelayedInvertedValues();
        private bool? GetActualBlanks(bool? value, Func<bool> useInversion);
        private static CriteriaOperator GetBlanks(OperandProperty prop);
        private static CriteriaOperator GetBlanks(OperandProperty prop, bool value);
        private static CriteriaOperator GetBlanks(OperandProperty prop, bool value, bool useInversion);
        private static CriteriaOperator GetBlanks(string path, bool value, bool useInversion);
        private FunctionOperator GetBlanksElement(CriteriaOperator operand, bool useInversion);
        private int GetCount();
        private int GetCount(bool forceFilterByText);
        private int GetDisplayIndex(OperandValue value);
        private int GetDisplayIndex(object value);
        private static int GetDisplayIndexCore(IDisplayMetricAttributes attributes, string value, Func<string, int> getDisplayIndexFallback = null);
        private Func<string, int> GetDisplayIndexFallback();
        private CriteriaOperator GetEquals(OperandProperty prop, OperandValue value, bool forceFilterByText, bool useInversion = false);
        private CriteriaOperator GetEqualsOrIsNull(OperandProperty prop, object value, bool forceFilterByText);
        private object GetFirstValue(bool forceFilterByText);
        private static bool GetForceFilterByText(IMetricAttributes metricAttributes);
        private CriteriaOperator GetIn(OperandProperty prop, OperandValue[] operandValues, bool forceFilterByText, bool useInversion);
        private InOperator GetInElement(CriteriaOperator operand, bool useInversion);
        private static List<TValue> GetInvertedList<TValue>(int totalCount, TValue[] values);
        private static UnaryOperator GetIsNull(OperandProperty prop, bool useInversion = false);
        private FunctionOperator GetIsSameDayElement(CriteriaOperator operand, bool useInversion);
        private static CriteriaOperator GetNothingOrBlanks(bool useBlanks, bool? blanks, string path, bool useInversion = false);
        private int GetNullDisplayIndex();
        private int[] GetNullDisplayIndexes();
        private static int[] GetNullDisplayIndexesCore(IDisplayMetricAttributes attributes, string nullName);
        private UnaryOperator GetNullElement(UnaryOperator unaryElement, bool useInversion);
        private OperandValue[] GetOperandValues(bool forceFilterByText);
        private OperandValue[] GetOperandValuesInverted(bool forceFilterByText, int maxDisplayIndex);
        private bool HasNull();
        private bool HasNullInverted();
        private bool HasValuesOrIndexes(bool filterByText);
        private void InitializeDataSource(bool fromUnique, int? count);
        protected sealed override bool InitializeWithNull(bool useInversion);
        protected sealed override bool InitializeWithValues(object[] uniqueAndSortedValues, bool useInversion);
        private bool IsBinaryElementOfGroup(CriteriaOperator operand, bool useInversion);
        private bool IsBlanksElementOfGroup(CriteriaOperator operand, bool useInversion);
        private bool IsBlanksGroup(GroupOperator group, bool useInversion = false);
        private bool IsEqualsGroup(GroupOperator group, bool useInversion = false);
        private bool IsInElementOfGroup(CriteriaOperator operand, bool useInversion);
        private bool IsIsSameDayElementOfGroup(CriteriaOperator operand, bool useInversion);
        private bool IsNoValuesLoaded();
        private bool IsNullElementOfGroup(GroupOperator group, bool useInversion);
        private bool IsNullGroup(GroupOperator group, bool useInversion = false);
        protected bool IsNullOrBlank(int index);
        public void LoadFewer();
        public void LoadMore();
        private void NotifyDataSourceChanged();
        protected void OnBlanksChanged();
        protected void OnDisplayIndexesChanged();
        protected sealed override void OnInitialized();
        protected sealed override void OnMetricAttributesMemberChanged(string propertyName);
        protected sealed override void OnMetricAttributesSpecialMemberChanged(string propertyName);
        protected void OnValuesChanged();
        protected sealed override void ResetCore();
        private void ResetDataSourceAndLoadCommand();
        private void SetBlanks(bool? value);
        private void SetBlanksFromParse(bool useInversion);
        private void SetDisplayIndexes(int[] indexes, bool useInversion);
        private void SetInvertedDisplayIndexes(int[] indexes, int maxDisplayIndex);
        private void SetInvertedValues(T[] valuesArray);
        private void SetValues(T[] valuesArray, bool useInversion);
        private bool TryParseBinary(string path, CriteriaOperator criteria, bool useInversion = false);
        private bool TryParseBlanks(string path, CriteriaOperator criteria, bool useInversion = false);
        private bool TryParseBlanksGroup(IEndUserFilteringMetric metric, GroupOperator group, bool forceText, bool useInversion = false);
        protected sealed override bool TryParseCore(IEndUserFilteringMetric metric, CriteriaOperator criteria);
        private bool TryParseEqualsGroup(GroupOperator group, bool forceText, bool useInversion = false);
        private bool TryParseIn(string path, CriteriaOperator criteria, bool useInversion = false);
        private bool TryParseIsSameDay(string path, CriteriaOperator criteria, bool useInversion = false);
        private bool TryParseNullGroup(IEndUserFilteringMetric metric, GroupOperator group, bool forceText, bool useInversion = false);
        private bool TryParseUnary(IEndUserFilteringMetric metric, CriteriaOperator criteria, bool useInversion = false);
        private bool TryResetBlanks();
        private void UpdateLoadCommands();
        private void UpdateLookupDataSource();
        private bool UseInversion();
        private bool UseInversion(int valuesCount, int dataCount);

        public virtual IReadOnlyCollection<T> Values { get; set; }

        public virtual IReadOnlyCollection<int> DisplayIndexes { get; set; }

        bool ISupportInversion.HasInversion { get; }

        object ISupportInversion.InvertedValues { get; }

        public bool IsLoadMoreAvailable { get; }

        public bool IsLoadFewerAvailable { get; }

        [Browsable(false)]
        public object DataSource { get; }

        [Browsable(false)]
        public IEnumerable<KeyValuePair<object, string>> LookupDataSource { get; }

        protected bool LookupDataSourceLoaded { get; }

        protected int NullAndBlanksCount { get; }

        protected int MaxDisplayIndex { get; }

        [Browsable(false)]
        public bool IsInverted { get; }

        protected ILookupMetricAttributes<T> MetricAttributes { get; }

        bool IUniqueValuesViewModel.HasValues { get; }

        object IUniqueValuesViewModel.Values { get; }

        [Browsable(false)]
        public string ValueMember { get; }

        [Browsable(false)]
        public string DisplayMember { get; }

        [Browsable(false)]
        public int? Top { get; }

        [Browsable(false)]
        public int? MaxCount { get; }

        [Browsable(false)]
        public bool UseSelectAll { get; }

        [Browsable(false)]
        public bool UseRadioSelection { get; }

        [Browsable(false)]
        public string SelectAllName { get; }

        [Browsable(false)]
        public string NullName { get; }

        [Browsable(false)]
        public string BlanksName { get; }

        [Browsable(false)]
        public bool UseBlanks { get; }

        public virtual bool? Blanks { get; set; }

        bool ICollectionValueViewModel.FilterByDisplayText { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CollectionValueBox<T>.<>c <>9;
            public static Func<BinaryOperator, OperandValue> <>9__17_2;
            public static Func<object> <>9__30_0;
            public static Func<HashSet<T>, object> <>9__64_0;
            public static Func<object, bool> <>9__67_0;
            public static Func<T, bool> <>9__136_0;
            public static Func<object, bool> <>9__137_0;
            public static Func<T, bool> <>9__138_1;
            public static Func<KeyValuePair<int, string>, int> <>9__152_1;
            public static Func<KeyValuePair<int, string>, string> <>9__152_2;

            static <>c();
            internal object <.ctor>b__30_0();
            internal object <DevExpress.Utils.Filtering.Internal.ISupportInversion.get_InvertedValues>b__64_0(HashSet<T> x);
            internal int <GetDisplayIndexFallback>b__152_1(KeyValuePair<int, string> x);
            internal string <GetDisplayIndexFallback>b__152_2(KeyValuePair<int, string> x);
            internal bool <GetOperandValues>b__138_1(T v);
            internal bool <HasNull>b__136_0(T v);
            internal bool <HasNullInverted>b__137_0(object v);
            internal bool <IsNoValuesLoaded>b__67_0(object x);
            internal OperandValue <TryParseEqualsGroup>b__17_2(BinaryOperator x);
        }
    }
}

