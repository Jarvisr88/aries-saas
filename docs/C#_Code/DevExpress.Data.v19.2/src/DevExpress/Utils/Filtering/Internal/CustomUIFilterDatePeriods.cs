namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;
    using System.Runtime.InteropServices;

    internal sealed class CustomUIFilterDatePeriods : CustomUIFilter
    {
        private IEnumerable<ICustomUIFilterValue> children;

        public CustomUIFilterDatePeriods(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            new DatePeriodsCriteriaParser(base.id, metric.Path);

        protected sealed override ICustomUIFilterValue CreateValue(object[] values)
        {
            ICustomUIFilterValuesFactory valuesFactory = base.GetService<ICustomUIFilterValuesFactory>();
            this.children ??= (from child in this.GetChildren() select valuesFactory.Create(child.GetID(), new object[0])).ToArray<ICustomUIFilterValue>();
            object[] objArray1 = new object[] { GetFilterTypes(values), this.children };
            return valuesFactory.Create(base.id, objArray1);
        }

        private IEnumerable<ICustomUIFilter> GetChildren()
        {
            Func<ICustomUIFilters, IEnumerable<ICustomUIFilter>> get = <>c.<>9__8_0;
            if (<>c.<>9__8_0 == null)
            {
                Func<ICustomUIFilters, IEnumerable<ICustomUIFilter>> local1 = <>c.<>9__8_0;
                get = <>c.<>9__8_0 = delegate (ICustomUIFilters filters) {
                    Func<ICustomUIFilter, bool> predicate = <>c.<>9__8_1;
                    if (<>c.<>9__8_1 == null)
                    {
                        Func<ICustomUIFilter, bool> local1 = <>c.<>9__8_1;
                        predicate = <>c.<>9__8_1 = filter => IsChild(filter.Group);
                    }
                    return filters.Where<ICustomUIFilter>(predicate);
                };
            }
            return (base.ParentViewModel as ICustomUIFilters).Get<ICustomUIFilters, IEnumerable<ICustomUIFilter>>(get, null);
        }

        private CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilter filter)
        {
            ICustomUIFilterValue value = this.children.FirstOrDefault<ICustomUIFilterValue>(c => c.FilterType == ((CustomUIFilterType) filter.GetID()));
            return (filter as ICustomUIFilterValueViewModel).Get<ICustomUIFilterValueViewModel, CriteriaOperator>(vm => vm.CreateFilterCriteria(metric, value), null);
        }

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue)
        {
            if ((filterValue == null) || (filterValue.Value == null))
            {
                return null;
            }
            CustomUIFilterType[] values = (CustomUIFilterType[]) filterValue.Value;
            return CriteriaOperator.Or((from filter in this.GetChildren()
                where Array.IndexOf<CustomUIFilterType>(values, filter.GetID()) != -1
                select this.GetCriteria(metric, filter)).ToArray<CriteriaOperator>());
        }

        protected sealed override ICustomUIFilterValue GetCustomUIFilterDialogViewModelParameter(ICustomUIFilter activeFilter) => 
            base.Value ?? this.CreateValue(null);

        private static CustomUIFilterType[] GetFilterTypes(object[] values) => 
            (values != null) ? values.OfType<CustomUIFilterType>().ToArray<CustomUIFilterType>() : null;

        private static object[] GetValues(IList<CustomUIFilterType> values)
        {
            object[] objArray = new object[values.Count];
            for (int i = 0; i < objArray.Length; i++)
            {
                objArray[i] = values[i];
            }
            return objArray;
        }

        private static bool IsChild(string group) => 
            (group == "DateDay") || ((group == "DateWeek") || ((group == "DateMonth") || (group == "DateYear")));

        internal static bool Match(CustomUIFilterType filterType) => 
            filterType == CustomUIFilterType.DatePeriods;

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
            public static readonly CustomUIFilterDatePeriods.<>c <>9 = new CustomUIFilterDatePeriods.<>c();
            public static Func<ICustomUIFilter, bool> <>9__8_1;
            public static Func<ICustomUIFilters, IEnumerable<ICustomUIFilter>> <>9__8_0;

            internal IEnumerable<ICustomUIFilter> <GetChildren>b__8_0(ICustomUIFilters filters)
            {
                Func<ICustomUIFilter, bool> predicate = <>9__8_1;
                if (<>9__8_1 == null)
                {
                    Func<ICustomUIFilter, bool> local1 = <>9__8_1;
                    predicate = <>9__8_1 = filter => CustomUIFilterDatePeriods.IsChild(filter.Group);
                }
                return filters.Where<ICustomUIFilter>(predicate);
            }

            internal bool <GetChildren>b__8_1(ICustomUIFilter filter) => 
                CustomUIFilterDatePeriods.IsChild(filter.Group);
        }

        private sealed class DatePeriodsCriteriaParser : CustomUIFilter.CustomUIFilterCriteriaParser
        {
            private static readonly IDictionary<FunctionOperatorType, CustomUIFilterType> functions;
            private List<CustomUIFilterType> values;

            static DatePeriodsCriteriaParser()
            {
                Dictionary<FunctionOperatorType, CustomUIFilterType> dictionary1 = new Dictionary<FunctionOperatorType, CustomUIFilterType>();
                dictionary1.Add(FunctionOperatorType.IsOutlookIntervalToday, CustomUIFilterType.Today);
                dictionary1.Add(FunctionOperatorType.IsOutlookIntervalYesterday, CustomUIFilterType.Yesterday);
                dictionary1.Add(FunctionOperatorType.IsOutlookIntervalTomorrow, CustomUIFilterType.Tomorrow);
                dictionary1.Add(FunctionOperatorType.IsThisWeek, CustomUIFilterType.ThisWeek);
                dictionary1.Add(FunctionOperatorType.IsOutlookIntervalLastWeek, CustomUIFilterType.LastWeek);
                dictionary1.Add(FunctionOperatorType.IsOutlookIntervalNextWeek, CustomUIFilterType.NextWeek);
                dictionary1.Add(FunctionOperatorType.IsThisMonth, CustomUIFilterType.ThisMonth);
                dictionary1.Add(FunctionOperatorType.IsLastMonth, CustomUIFilterType.LastMonth);
                dictionary1.Add(FunctionOperatorType.IsNextMonth, CustomUIFilterType.NextMonth);
                dictionary1.Add(FunctionOperatorType.IsThisYear, CustomUIFilterType.ThisYear);
                dictionary1.Add(FunctionOperatorType.IsLastYear, CustomUIFilterType.LastYear);
                dictionary1.Add(FunctionOperatorType.IsNextYear, CustomUIFilterType.NextYear);
                functions = dictionary1;
            }

            internal DatePeriodsCriteriaParser(CustomUIFilterType filterType, string path) : base(filterType, path)
            {
            }

            protected sealed override object[] LocalValuesReady(object[] localValues)
            {
                localValues = CustomUIFilterDatePeriods.GetValues(this.values.ToArray());
                this.values = null;
                return localValues;
            }

            protected sealed override void OnFunctionOperator(FunctionOperator theOperator)
            {
                CustomUIFilterType type;
                if (this.TryParseFunctionOperator(theOperator, out type))
                {
                    this.values.Add(type);
                }
                else
                {
                    base.MarkInvalid();
                }
            }

            protected sealed override void OnGroupOperator(GroupOperator theOperator)
            {
                GroupOperator @operator = theOperator;
                if (((@operator == null) || (@operator.OperatorType != GroupOperatorType.Or)) && (@operator.Operands.Count != 1))
                {
                    this.values = null;
                    base.MarkInvalid();
                }
                else
                {
                    using (List<CriteriaOperator>.Enumerator enumerator = @operator.Operands.GetEnumerator())
                    {
                        while (true)
                        {
                            if (enumerator.MoveNext())
                            {
                                CustomUIFilterType type;
                                CriteriaOperator current = enumerator.Current;
                                if (this.TryParseFunctionOperator(current, out type))
                                {
                                    this.values.Add(type);
                                    continue;
                                }
                                this.values = null;
                                base.MarkInvalid();
                            }
                            else
                            {
                                return;
                            }
                            break;
                        }
                    }
                }
            }

            protected sealed override object[] PrepareLocalValues()
            {
                this.values = new List<CustomUIFilterType>();
                return null;
            }

            private bool TryParseFunctionOperator(CriteriaOperator criteria, out CustomUIFilterType value)
            {
                FunctionOperator @operator = criteria as FunctionOperator;
                if ((@operator != null) && ((@operator.Operands.Count == 1) && functions.TryGetValue(@operator.OperatorType, out value)))
                {
                    OperandProperty property = @operator.Operands[0] as OperandProperty;
                    if (!base.IsInvalid(property))
                    {
                        return true;
                    }
                }
                value = CustomUIFilterType.None;
                return false;
            }
        }
    }
}

