namespace DevExpress.Xpf.Core.FilteringUI
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Mvvm.Native;
    using DevExpress.Xpf.Core.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Threading.Tasks;

    public sealed class ExcelStyleFilterModel : FilterModelBase
    {
        private readonly MultiFilterModel RulesFilterModel;
        private readonly CheckedTreeListFilterModel ValuesFilterModel;
        private AllowedExcelStyleFilterTypes allowedFilterTypes;
        private AllowedExcelStyleFilterTypes actualAllowedFilterTypes;

        internal ExcelStyleFilterModel(FilterModelClient client, Func<UniqueValues, UniqueValues> getUniqueValuesForValuesTab, Func<OperatorMenuItemsSubstitutionInfo<ExcelStyleFilterElementOperatorItem>, OperatorMenuItemsSubstitutionInfo<ExcelStyleFilterElementOperatorItem>> getOperatorsForRulesTab, Func<FilterModelBase, DataTemplate> selectTemplate) : base(client)
        {
            <>c__DisplayClass19_0 class_;
            Func<CountsIncludeMode, Task<UniqueValues>> func;
            this.allowedFilterTypes = AllowedExcelStyleFilterTypes.All;
            this.actualAllowedFilterTypes = AllowedExcelStyleFilterTypes.All;
            this.RulesFilterModel = MultiFilterModelFactory.CreateMultiElementModel(client, getOperatorsForRulesTab, selectTemplate);
            func = (getUniqueValuesForValuesTab != null) ? (func = delegate (CountsIncludeMode countsIncludeMode) {
                <>c__DisplayClass19_0.<<-ctor>b__0>d local;
                local.<>4__this = class_;
                local.countsIncludeMode = countsIncludeMode;
                local.<>t__builder = AsyncTaskMethodBuilder<UniqueValues>.Create();
                local.<>1__state = -1;
                local.<>t__builder.Start<<>c__DisplayClass19_0.<<-ctor>b__0>d>(ref local);
                return local.<>t__builder.Task;
            }) : null;
            this.ValuesFilterModel = new CheckedTreeListFilterModel(client.Update(null, func, null));
            this.DefaultFilterType = ExcelStyleFilterType.Values;
            List<FilterModelBase> source = new List<FilterModelBase>();
            source.Add(this.RulesFilterModel);
            source.Add(this.ValuesFilterModel);
            this.<FilterModels>k__BackingField = source.ToReadOnlyObservableCollection<FilterModelBase>();
            this.ValuesFilterModel.PropertyChanged += t => delegate (object _, PropertyChangedEventArgs e) {
                if (e.PropertyName == "Nodes")
                {
                    this.ValuesFilterModel.PropertyChanged -= t.Value;
                    this.UpdateSelectedFilterModel();
                }
            }.WithReturnValue<PropertyChangedEventHandler>();
        }

        internal override CriteriaOperator BuildFilter()
        {
            throw new NotImplementedException();
        }

        internal override bool CanBuildFilterCore() => 
            this.ActualAllowedFilterTypes != AllowedExcelStyleFilterTypes.None;

        private List<FilterModelBase> GetAllowedFilterModels()
        {
            List<FilterModelBase> list = new List<FilterModelBase>();
            if (this.ActualAllowedFilterTypes.HasFlag(AllowedExcelStyleFilterTypes.Rules))
            {
                list.Add(this.RulesFilterModel);
            }
            if (this.ActualAllowedFilterTypes.HasFlag(AllowedExcelStyleFilterTypes.Values))
            {
                list.Add(this.ValuesFilterModel);
            }
            return list;
        }

        private FilterModelBase GetAppropriateFilterModel()
        {
            if (base.Filter == null)
            {
                return ((this.DefaultFilterType == ExcelStyleFilterType.Rules) ? ((FilterModelBase) this.RulesFilterModel) : ((FilterModelBase) this.ValuesFilterModel));
            }
            if (this.IsRulesModelAppropriate())
            {
                return this.RulesFilterModel;
            }
            bool? nullable = this.ValuesFilterModel.CalculateHasFilter();
            return (nullable?.Value ? ((FilterModelBase) this.ValuesFilterModel) : ((FilterModelBase) this.RulesFilterModel));
        }

        private bool IsRulesModelAppropriate()
        {
            CriteriaOperator filter = BetweenDatesHelper.SubstituteDateInRange(base.Filter);
            if (!this.RulesFilterModel.CanUpdate(filter))
            {
                return false;
            }
            FunctionOperatorMapper<bool> function = <>c.<>9__28_0;
            if (<>c.<>9__28_0 == null)
            {
                FunctionOperatorMapper<bool> local1 = <>c.<>9__28_0;
                function = <>c.<>9__28_0 = delegate (string propertyName, object[] values, FunctionOperatorType type) {
                    ValueData[] dataArray = values.Select<object, ValueData>(new Func<object, ValueData>(ValueData.FromValue)).ToArray<ValueData>();
                    return (BetweenDatesHelper.TryGetRangeFromSubstituted<object>(propertyName, dataArray, type) != null) || (BetweenDatesHelper.TryGetPropertyValuesFromSubstituted<object>(propertyName, dataArray, type) != null);
                };
            }
            FallbackMapper<bool> fallback = <>c.<>9__28_1;
            if (<>c.<>9__28_1 == null)
            {
                FallbackMapper<bool> local2 = <>c.<>9__28_1;
                fallback = <>c.<>9__28_1 = _ => false;
            }
            if (filter.Map<bool>(null, null, null, null, function, null, null, null, fallback, null))
            {
                return false;
            }
            Predicate<UnaryOperator> condition = <>c.<>9__28_2;
            if (<>c.<>9__28_2 == null)
            {
                Predicate<UnaryOperator> local3 = <>c.<>9__28_2;
                condition = <>c.<>9__28_2 = unary => unary.OperatorType == UnaryOperatorType.Not;
            }
            return base.Filter.Transform<UnaryOperator, bool>(condition, (<>c.<>9__28_3 ??= delegate (UnaryOperator unaryNot) {
                UnaryOperatorMapper<bool> unary = <>c.<>9__28_4;
                if (<>c.<>9__28_4 == null)
                {
                    UnaryOperatorMapper<bool> local1 = <>c.<>9__28_4;
                    unary = <>c.<>9__28_4 = (_, type) => type != UnaryOperatorType.IsNull;
                }
                FallbackMapper<bool> mapper2 = <>c.<>9__28_5;
                if (<>c.<>9__28_5 == null)
                {
                    FallbackMapper<bool> local2 = <>c.<>9__28_5;
                    mapper2 = <>c.<>9__28_5 = _ => true;
                }
                return unaryNot.Operand.Map<bool>(null, unary, null, null, null, null, null, null, mapper2, null);
            }), delegate (CriteriaOperator op) {
                UnaryOperatorMapper<bool> unary = <>c.<>9__28_8;
                if (<>c.<>9__28_8 == null)
                {
                    UnaryOperatorMapper<bool> local1 = <>c.<>9__28_8;
                    unary = <>c.<>9__28_8 = (_, type) => type != UnaryOperatorType.IsNull;
                }
                FallbackMapper<bool> mapper2 = <>c.<>9__28_9;
                if (<>c.<>9__28_9 == null)
                {
                    FallbackMapper<bool> local2 = <>c.<>9__28_9;
                    mapper2 = <>c.<>9__28_9 = _ => true;
                }
                return op.Map<bool>((_, __, type) => (((type == BinaryOperatorType.Equal) || (type == BinaryOperatorType.NotEqual)) ? FilterTreeHelper.IsDateTimeProperty(base.Column.Type) : true), unary, null, null, null, null, null, null, mapper2, null);
            });
        }

        private void OnActualAllowedFilterTypesChanged()
        {
            base.UpdateCanBuildFilter();
            this.UpdateSelectedFilterModel();
        }

        private void UpdateActualAllowedFilterTypes()
        {
            AllowedExcelStyleFilterTypes allowedFilterTypes = this.AllowedFilterTypes;
            if (!this.ValuesFilterModel.CanBuildFilter)
            {
                allowedFilterTypes &= ~AllowedExcelStyleFilterTypes.Values;
            }
            if (!this.RulesFilterModel.CanBuildFilter)
            {
                allowedFilterTypes &= ~AllowedExcelStyleFilterTypes.Rules;
            }
            this.ActualAllowedFilterTypes = allowedFilterTypes;
        }

        internal override Task UpdateCoreAsync()
        {
            this.RulesFilterModel.Update(this.RulesFilterModel.CanUpdate(base.Filter) ? base.Filter : null);
            this.ValuesFilterModel.Update(base.Filter);
            this.UpdateActualAllowedFilterTypes();
            this.UpdateSelectedFilterModel();
            return FilteringUIExtensions.CompletedTask;
        }

        private void UpdateSelectedFilterModel()
        {
            List<FilterModelBase> allowedFilterModels = this.GetAllowedFilterModels();
            if ((this.SelectedFilterModel == null) || !allowedFilterModels.Contains(this.SelectedFilterModel))
            {
                this.SelectedFilterModel = (allowedFilterModels.Count == 1) ? allowedFilterModels.Single<FilterModelBase>() : this.GetAppropriateFilterModel();
            }
        }

        protected override void UpdateShowCounts()
        {
        }

        public IReadOnlyList<FilterModelBase> FilterModels { get; }

        public AllowedExcelStyleFilterTypes AllowedFilterTypes
        {
            get => 
                this.allowedFilterTypes;
            set => 
                base.SetProperty<AllowedExcelStyleFilterTypes>(ref this.allowedFilterTypes, value, "AllowedFilterTypes", new Action(this.UpdateActualAllowedFilterTypes));
        }

        public AllowedExcelStyleFilterTypes ActualAllowedFilterTypes
        {
            get => 
                this.actualAllowedFilterTypes;
            private set => 
                base.SetProperty<AllowedExcelStyleFilterTypes>(ref this.actualAllowedFilterTypes, value, "ActualAllowedFilterTypes", new Action(this.OnActualAllowedFilterTypesChanged));
        }

        public ExcelStyleFilterType DefaultFilterType
        {
            get => 
                base.GetValue<ExcelStyleFilterType>("DefaultFilterType");
            set => 
                base.SetValue<ExcelStyleFilterType>(value, "DefaultFilterType");
        }

        public FilterModelBase SelectedFilterModel
        {
            get => 
                base.GetValue<FilterModelBase>("SelectedFilterModel");
            set => 
                base.SetValue<FilterModelBase>(value, "SelectedFilterModel");
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ExcelStyleFilterModel.<>c <>9 = new ExcelStyleFilterModel.<>c();
            public static FunctionOperatorMapper<bool> <>9__28_0;
            public static FallbackMapper<bool> <>9__28_1;
            public static Predicate<UnaryOperator> <>9__28_2;
            public static UnaryOperatorMapper<bool> <>9__28_4;
            public static FallbackMapper<bool> <>9__28_5;
            public static Func<UnaryOperator, bool> <>9__28_3;
            public static UnaryOperatorMapper<bool> <>9__28_8;
            public static FallbackMapper<bool> <>9__28_9;

            internal bool <IsRulesModelAppropriate>b__28_0(string propertyName, object[] values, FunctionOperatorType type)
            {
                ValueData[] dataArray = values.Select<object, ValueData>(new Func<object, ValueData>(ValueData.FromValue)).ToArray<ValueData>();
                return ((BetweenDatesHelper.TryGetRangeFromSubstituted<object>(propertyName, dataArray, type) != null) || (BetweenDatesHelper.TryGetPropertyValuesFromSubstituted<object>(propertyName, dataArray, type) != null));
            }

            internal bool <IsRulesModelAppropriate>b__28_1(CriteriaOperator _) => 
                false;

            internal bool <IsRulesModelAppropriate>b__28_2(UnaryOperator unary) => 
                unary.OperatorType == UnaryOperatorType.Not;

            internal bool <IsRulesModelAppropriate>b__28_3(UnaryOperator unaryNot)
            {
                UnaryOperatorMapper<bool> unary = <>9__28_4;
                if (<>9__28_4 == null)
                {
                    UnaryOperatorMapper<bool> local1 = <>9__28_4;
                    unary = <>9__28_4 = (_, type) => type != UnaryOperatorType.IsNull;
                }
                FallbackMapper<bool> fallback = <>9__28_5;
                if (<>9__28_5 == null)
                {
                    FallbackMapper<bool> local2 = <>9__28_5;
                    fallback = <>9__28_5 = _ => true;
                }
                return unaryNot.Operand.Map<bool>(null, unary, null, null, null, null, null, null, fallback, null);
            }

            internal bool <IsRulesModelAppropriate>b__28_4(string _, UnaryOperatorType type) => 
                type != UnaryOperatorType.IsNull;

            internal bool <IsRulesModelAppropriate>b__28_5(CriteriaOperator _) => 
                true;

            internal bool <IsRulesModelAppropriate>b__28_8(string _, UnaryOperatorType type) => 
                type != UnaryOperatorType.IsNull;

            internal bool <IsRulesModelAppropriate>b__28_9(CriteriaOperator _) => 
                true;
        }
    }
}

