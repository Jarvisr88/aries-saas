namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using DevExpress.Utils.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class CustomUIFilterCustom : CustomUIFilter
    {
        private IEnumerable<ICustomUIFilterValue> children;

        internal CustomUIFilterCustom(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override bool AllowCore(ICustomUIFiltersOptions userOptions)
        {
            Func<ICustomUIFiltersOptions, bool> get = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<ICustomUIFiltersOptions, bool> local1 = <>c.<>9__2_0;
                get = <>c.<>9__2_0 = opt => opt.ShowCustomFilters;
            }
            return userOptions.Get<ICustomUIFiltersOptions, bool>(get, ExcelFilterOptions.Default.ShowCustomFilters.GetValueOrDefault(true));
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            new CustomCriteriaParser(this, base.id, metric);

        protected sealed override ICustomUIFilterValue CreateValue(object[] values)
        {
            ICustomUIFilterValuesFactory service = base.GetService<ICustomUIFilterValuesFactory>();
            this.EnsureChildren(service);
            object[] objArray1 = new object[] { values, this.children };
            return service.Create(base.id, objArray1);
        }

        private IEnumerable<ICustomUIFilterValue> EnsureChildren(ICustomUIFilterValuesFactory valuesFactory)
        {
            this.children ??= (from child in this.GetChildren() select valuesFactory.Create(child.GetID(), new object[0])).ToArray<ICustomUIFilterValue>();
            return this.children;
        }

        private IEnumerable<ICustomUIFilter> GetChildren()
        {
            Func<ICustomUIFilters, IEnumerable<ICustomUIFilter>> get = <>c.<>9__12_0;
            if (<>c.<>9__12_0 == null)
            {
                Func<ICustomUIFilters, IEnumerable<ICustomUIFilter>> local1 = <>c.<>9__12_0;
                get = <>c.<>9__12_0 = filters => from f in filters
                    where IsChild(f, filters.GetID())
                    select f;
            }
            return (base.ParentViewModel as ICustomUIFilters).Get<ICustomUIFilters, IEnumerable<ICustomUIFilter>>(get, null);
        }

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue)
        {
            if ((filterValue == null) || !(filterValue.Value is Array))
            {
                return null;
            }
            if (filterValue.Value is ICustomUIFilterValue[])
            {
                return null;
            }
            object[] objArray = (object[]) filterValue.Value;
            ICustomUIFilterValuesFactory service = base.GetService<ICustomUIFilterValuesFactory>();
            CustomUIFilterType filterType = (objArray[0] is CustomUIFilterType) ? ((CustomUIFilterType) objArray[0]) : CustomUIFilterType.None;
            CriteriaOperator @operator = this.GetCriteria(metric, filterType, objArray[1], service);
            CustomUIFilterType type2 = (objArray[3] is CustomUIFilterType) ? ((CustomUIFilterType) objArray[3]) : CustomUIFilterType.None;
            CriteriaOperator operator2 = this.GetCriteria(metric, type2, objArray[4], service);
            if (@operator == null)
            {
                return operator2;
            }
            if (operator2 == null)
            {
                return @operator;
            }
            CriteriaOperator[] operands = new CriteriaOperator[] { @operator, operator2 };
            return new GroupOperator((GroupOperatorType) objArray[2], operands);
        }

        private CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, CustomUIFilterType filterType, object value, ICustomUIFilterValuesFactory valuesFactory)
        {
            if (filterType == CustomUIFilterType.None)
            {
                return null;
            }
            object[] values = new object[] { value };
            ICustomUIFilterValue value2 = valuesFactory.Create(filterType, values);
            return this.GetCriteriaCore(metric, filterType, value2);
        }

        private CriteriaOperator GetCriteriaCore(IEndUserFilteringMetric metric, CustomUIFilterType filterType, ICustomUIFilterValue value) => 
            ((value == null) || value.IsDefault) ? null : (base.ParentViewModel as ICustomUIFilters).Get<ICustomUIFilters, CustomUIFilter>(filters => (filters[filterType] as CustomUIFilter), null).Get<ICustomUIFilterValueViewModel, CriteriaOperator>(vm => vm.CreateFilterCriteria(metric, value), null);

        protected sealed override ICustomUIFilterValue GetCustomUIFilterDialogViewModelParameter(ICustomUIFilter activeFilter) => 
            base.Value ?? this.CreateValue((activeFilter != null) ? this.GetValues(activeFilter.Value) : null);

        private object[] GetValues(ICustomUIFilterValue activeFilterValue)
        {
            if (activeFilterValue == null)
            {
                return null;
            }
            ICustomUIFilterValue value2 = this.EnsureChildren(base.GetService<ICustomUIFilterValuesFactory>()).FirstOrDefault<ICustomUIFilterValue>(x => x.FilterType == activeFilterValue.FilterType);
            if (value2 == null)
            {
                return null;
            }
            object obj2 = value2.IsKnownValue() ? null : activeFilterValue.Value;
            object[] objArray1 = new object[5];
            objArray1[0] = value2.FilterType;
            objArray1[1] = obj2;
            objArray1[2] = GroupOperatorType.And;
            return objArray1;
        }

        private static bool IsChild(ICustomUIFilter filter, CustomUIFiltersType filtersType) => 
            (((CustomUIFilterType) filter.GetID()) != CustomUIFilterType.Between) && ((filter.Group == filtersType.ToString()) || IsCompatible(filter, filtersType));

        private static bool IsCompatible(ICustomUIFilter filter, CustomUIFiltersType filtersType) => 
            (filter.Group == "Common") || ((filtersType == CustomUIFiltersType.Enum) && (filter.Group == "Numeric"));

        internal static bool Match(CustomUIFilterType filterType) => 
            filterType == CustomUIFilterType.Custom;

        protected sealed override void QueryViewModelResult(ICustomUIFilterDialogViewModel viewModel)
        {
            object[] objArray1 = new object[2];
            object[] objArray2 = new object[2];
            objArray2[0] = viewModel.Parameter?.Value;
            object[] values = objArray2;
            values[1] = this.children;
            viewModel.SetResult(values);
        }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomUIFilterCustom.<>c <>9 = new CustomUIFilterCustom.<>c();
            public static Func<ICustomUIFiltersOptions, bool> <>9__2_0;
            public static Func<ICustomUIFilters, IEnumerable<ICustomUIFilter>> <>9__12_0;

            internal bool <AllowCore>b__2_0(ICustomUIFiltersOptions opt) => 
                opt.ShowCustomFilters;

            internal IEnumerable<ICustomUIFilter> <GetChildren>b__12_0(ICustomUIFilters filters) => 
                from f in filters
                    where CustomUIFilterCustom.IsChild(f, filters.GetID())
                    select f;
        }

        private sealed class CustomCriteriaParser : CustomUIFilter.CustomUIFilterCriteriaParser
        {
            private readonly CustomUIFilterCustom custom;
            private readonly IEndUserFilteringMetric metric;
            private readonly object[] values;

            internal CustomCriteriaParser(CustomUIFilterCustom custom, CustomUIFilterType filterType, IEndUserFilteringMetric metric) : base(filterType, metric.Path)
            {
                this.values = new object[5];
                this.custom = custom;
                this.metric = metric;
            }

            protected sealed override void OnGroupOperator(GroupOperator theOperator)
            {
                GroupOperator @operator = theOperator;
                if (@operator != null)
                {
                    IEnumerable<ICustomUIFilter> children = this.custom.GetChildren();
                    if (children == null)
                    {
                        base.MarkInvalid();
                        return;
                    }
                    if ((@operator.Operands.Count == 0) || (@operator.Operands.Count > 2))
                    {
                        base.MarkInvalid();
                        return;
                    }
                    ICustomUIFilter result = null;
                    ICustomUIFilter filter2 = null;
                    if (this.TryParseChildren(children, @operator.Operands[0], out result))
                    {
                        this.values[0] = result.Value.FilterType;
                        this.values[1] = (result.Value.Value is KnownValues) ? null : result.Value.Value;
                        if (@operator.Operands.Count == 1)
                        {
                            this.values[2] = @operator.OperatorType;
                            this.values[3] = CustomUIFilterType.None;
                            Action<CustomUIFilter> @do = <>c.<>9__5_0;
                            if (<>c.<>9__5_0 == null)
                            {
                                Action<CustomUIFilter> local1 = <>c.<>9__5_0;
                                @do = <>c.<>9__5_0 = fCstom => fCstom.SetValueCore(null, false);
                            }
                            (result as CustomUIFilter).Do<CustomUIFilter>(@do);
                            return;
                        }
                        if (this.TryParseChildren(children, @operator.Operands[1], out filter2))
                        {
                            this.values[2] = @operator.OperatorType;
                            this.values[3] = filter2.Value.FilterType;
                            this.values[4] = (filter2.Value.Value is KnownValues) ? null : filter2.Value.Value;
                            CustomUIFilter filter1 = filter2 as CustomUIFilter;
                            if (<>c.<>9__5_1 == null)
                            {
                                CustomUIFilter local2 = filter2 as CustomUIFilter;
                                filter1 = (CustomUIFilter) (<>c.<>9__5_1 = sCustom => sCustom.SetValueCore(null, false));
                            }
                            ((CustomUIFilter) <>c.<>9__5_1).Do<CustomUIFilter>((Action<CustomUIFilter>) filter1);
                            return;
                        }
                    }
                }
                base.MarkInvalid();
            }

            protected sealed override object[] PrepareLocalValues() => 
                base.Prepare(this.values);

            private bool TryParseChildren(IEnumerable<ICustomUIFilter> children, CriteriaOperator criteria, out ICustomUIFilter result)
            {
                result = null;
                foreach (ICustomUIFilter filter in children)
                {
                    Func<IEndUserFilteringCriteriaAwareViewModel, bool> <>9__0;
                    Func<IEndUserFilteringCriteriaAwareViewModel, bool> get = <>9__0;
                    if (<>9__0 == null)
                    {
                        Func<IEndUserFilteringCriteriaAwareViewModel, bool> local1 = <>9__0;
                        get = <>9__0 = criteriaAware => criteriaAware.TryParse(this.metric, criteria);
                    }
                    if ((filter as IEndUserFilteringCriteriaAwareViewModel).Get<IEndUserFilteringCriteriaAwareViewModel, bool>(get, false))
                    {
                        result = filter;
                        break;
                    }
                }
                return (result != null);
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly CustomUIFilterCustom.CustomCriteriaParser.<>c <>9 = new CustomUIFilterCustom.CustomCriteriaParser.<>c();
                public static Action<CustomUIFilter> <>9__5_0;
                public static Action<CustomUIFilter> <>9__5_1;

                internal void <OnGroupOperator>b__5_0(CustomUIFilter fCstom)
                {
                    fCstom.SetValueCore(null, false);
                }

                internal void <OnGroupOperator>b__5_1(CustomUIFilter sCustom)
                {
                    sCustom.SetValueCore(null, false);
                }
            }
        }
    }
}

