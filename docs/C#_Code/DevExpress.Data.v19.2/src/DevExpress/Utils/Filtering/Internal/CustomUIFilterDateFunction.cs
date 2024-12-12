namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    internal sealed class CustomUIFilterDateFunction : CustomUIFilter
    {
        private static readonly IDictionary<CustomUIFilterType, FunctionOperatorType> map;

        static CustomUIFilterDateFunction()
        {
            Dictionary<CustomUIFilterType, FunctionOperatorType> dictionary1 = new Dictionary<CustomUIFilterType, FunctionOperatorType>();
            dictionary1.Add(CustomUIFilterType.Today, FunctionOperatorType.IsOutlookIntervalToday);
            dictionary1.Add(CustomUIFilterType.Yesterday, FunctionOperatorType.IsOutlookIntervalYesterday);
            dictionary1.Add(CustomUIFilterType.Tomorrow, FunctionOperatorType.IsOutlookIntervalTomorrow);
            dictionary1.Add(CustomUIFilterType.ThisWeek, FunctionOperatorType.IsThisWeek);
            dictionary1.Add(CustomUIFilterType.LastWeek, FunctionOperatorType.IsOutlookIntervalLastWeek);
            dictionary1.Add(CustomUIFilterType.NextWeek, FunctionOperatorType.IsOutlookIntervalNextWeek);
            dictionary1.Add(CustomUIFilterType.ThisMonth, FunctionOperatorType.IsThisMonth);
            dictionary1.Add(CustomUIFilterType.LastMonth, FunctionOperatorType.IsLastMonth);
            dictionary1.Add(CustomUIFilterType.NextMonth, FunctionOperatorType.IsNextMonth);
            dictionary1.Add(CustomUIFilterType.ThisYear, FunctionOperatorType.IsThisYear);
            dictionary1.Add(CustomUIFilterType.LastYear, FunctionOperatorType.IsLastYear);
            dictionary1.Add(CustomUIFilterType.NextYear, FunctionOperatorType.IsNextYear);
            dictionary1.Add(CustomUIFilterType.YearToDate, FunctionOperatorType.IsYearToDate);
            dictionary1.Add(CustomUIFilterType.January, FunctionOperatorType.IsJanuary);
            dictionary1.Add(CustomUIFilterType.February, FunctionOperatorType.IsFebruary);
            dictionary1.Add(CustomUIFilterType.March, FunctionOperatorType.IsMarch);
            dictionary1.Add(CustomUIFilterType.April, FunctionOperatorType.IsApril);
            dictionary1.Add(CustomUIFilterType.May, FunctionOperatorType.IsMay);
            dictionary1.Add(CustomUIFilterType.June, FunctionOperatorType.IsJune);
            dictionary1.Add(CustomUIFilterType.July, FunctionOperatorType.IsJuly);
            dictionary1.Add(CustomUIFilterType.August, FunctionOperatorType.IsAugust);
            dictionary1.Add(CustomUIFilterType.September, FunctionOperatorType.IsSeptember);
            dictionary1.Add(CustomUIFilterType.October, FunctionOperatorType.IsOctober);
            dictionary1.Add(CustomUIFilterType.November, FunctionOperatorType.IsNovember);
            dictionary1.Add(CustomUIFilterType.December, FunctionOperatorType.IsDecember);
            dictionary1.Add(CustomUIFilterType.NextQuarter, FunctionOperatorType.GetMonth);
            dictionary1.Add(CustomUIFilterType.LastQuarter, FunctionOperatorType.GetMonth);
            dictionary1.Add(CustomUIFilterType.ThisQuarter, FunctionOperatorType.GetMonth);
            dictionary1.Add(CustomUIFilterType.Quarter1, FunctionOperatorType.GetMonth);
            dictionary1.Add(CustomUIFilterType.Quarter2, FunctionOperatorType.GetMonth);
            dictionary1.Add(CustomUIFilterType.Quarter3, FunctionOperatorType.GetMonth);
            dictionary1.Add(CustomUIFilterType.Quarter4, FunctionOperatorType.GetMonth);
            map = dictionary1;
        }

        public CustomUIFilterDateFunction(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            new DateFunctionCriteriaParser(base.id, metric.Path);

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue)
        {
            OperandProperty property = new OperandProperty(metric.Path);
            if (this.IsKnownDate(filterValue))
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { property };
                return new FunctionOperator(map[base.id], operands);
            }
            if (this.IsKnownBaseDate(filterValue))
            {
                FunctionOperator @operator = new FunctionOperator(FunctionOperatorType.Today, new CriteriaOperator[0]);
                CriteriaOperator[] operands = new CriteriaOperator[] { @operator };
                CriteriaOperator[] operatorArray3 = new CriteriaOperator[] { property };
                return new BinaryOperator(new FunctionOperator(map[base.id], operands), new FunctionOperator(map[base.id], operatorArray3), BinaryOperatorType.Equal);
            }
            if (this.IsKnownDatePart(filterValue))
            {
                int num;
                CriteriaOperator[] operands = new CriteriaOperator[] { property };
                FunctionOperator opLeft = new FunctionOperator(map[base.id], operands);
                if (this.IsMonth(out num))
                {
                    return new BinaryOperator(opLeft, num, BinaryOperatorType.Equal);
                }
                if (this.IsQuarter(out num))
                {
                    return new BetweenOperator(opLeft, num, num + 2);
                }
            }
            return null;
        }

        private bool IsKnownBaseDate(ICustomUIFilterValue filterValue) => 
            Equals(filterValue.Value, KnownValues.BaseDate);

        private bool IsKnownDate(ICustomUIFilterValue filterValue) => 
            Equals(filterValue.Value, KnownValues.Date);

        private bool IsKnownDatePart(ICustomUIFilterValue filterValue) => 
            Equals(filterValue.Value, KnownValues.DatePart);

        private bool IsMonth(out int month)
        {
            month = (((CustomUIFilterType) base.id) - CustomUIFilterType.January) + 1;
            return ((((CustomUIFilterType) base.id) >= CustomUIFilterType.January) && (((CustomUIFilterType) base.id) <= CustomUIFilterType.December));
        }

        private bool IsQuarter(out int startMonth)
        {
            startMonth = ((((CustomUIFilterType) base.id) - CustomUIFilterType.Quarter1) * CustomUIFilterType.DoesNotEqual) + 1;
            return ((((CustomUIFilterType) base.id) >= CustomUIFilterType.Quarter1) && (((CustomUIFilterType) base.id) <= CustomUIFilterType.Quarter4));
        }

        internal static bool Match(CustomUIFilterType filterType) => 
            map.ContainsKey(filterType);

        protected override void QueryViewModelResult(ICustomUIFilterDialogViewModel viewModel)
        {
            viewModel.SetResult(new object[0]);
        }

        private sealed class DateFunctionCriteriaParser : CustomUIFilter.CustomUIFilterCriteriaParser
        {
            internal DateFunctionCriteriaParser(CustomUIFilterType filterType, string path) : base(filterType, path)
            {
            }

            protected sealed override void OnBetweenOperator(BetweenOperator between)
            {
                if ((base.filterType < CustomUIFilterType.Quarter1) || (base.filterType > CustomUIFilterType.Quarter4))
                {
                    base.OnBetweenOperator(between);
                }
                else
                {
                    FunctionOperator @operator;
                    if (!between.TestExpression.Is<FunctionOperator>(out @operator) || ((@operator.OperatorType != FunctionOperatorType.GetMonth) || (@operator.Operands.Count != 1)))
                    {
                        base.MarkInvalid();
                    }
                    else
                    {
                        OperandProperty property = @operator.Operands[0] as OperandProperty;
                        if (base.IsInvalid(property))
                        {
                            base.MarkInvalid();
                        }
                        else
                        {
                            int num;
                            int num2;
                            if (!CustomUIFilter.TryGetValue<int>(between.BeginExpression, out num) || !CustomUIFilter.TryGetValue<int>(between.EndExpression, out num2))
                            {
                                base.MarkInvalid();
                            }
                            else
                            {
                                int num3 = ((int) ((base.filterType - CustomUIFilterType.Quarter1) * CustomUIFilterType.DoesNotEqual)) + 2;
                                if ((num3 < num) || (num3 > num2))
                                {
                                    base.MarkInvalid();
                                }
                            }
                        }
                    }
                }
            }

            protected sealed override void OnFunctionOperator(FunctionOperator function)
            {
                if ((function.OperatorType != ((FunctionOperatorType) CustomUIFilterDateFunction.map[base.filterType])) || (function.Operands.Count != 1))
                {
                    base.MarkInvalid();
                }
                else
                {
                    OperandProperty property = function.Operands[0] as OperandProperty;
                    if (base.IsInvalid(property))
                    {
                        base.MarkInvalid();
                    }
                }
            }
        }
    }
}

