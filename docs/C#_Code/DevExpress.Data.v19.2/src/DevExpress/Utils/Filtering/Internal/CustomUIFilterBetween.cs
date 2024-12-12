namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Runtime.CompilerServices;

    internal sealed class CustomUIFilterBetween : CustomUIFilter
    {
        public CustomUIFilterBetween(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override bool AllowCore(ICustomUIFiltersOptions userOptions)
        {
            Func<ICustomUIFiltersOptions, bool> get = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<ICustomUIFiltersOptions, bool> local1 = <>c.<>9__2_0;
                get = <>c.<>9__2_0 = opt => opt.ShowComparisons;
            }
            return userOptions.Get<ICustomUIFiltersOptions, bool>(get, true);
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            new BetweenCriteriaParser(base.id, metric.Path, metric.Type);

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue) => 
            (filterValue.Value as object[]).Get<object[], CriteriaOperator>(delegate (object[] values) {
                if (values.Length != 2)
                {
                    return null;
                }
                object obj2 = GetValue(values[0], metric.Type);
                object obj3 = GetValue(values[1], metric.Type);
                if ((obj2 == null) && (obj3 == null))
                {
                    return null;
                }
                if ((obj2 != null) && (obj3 == null))
                {
                    return new BinaryOperator(metric.Path, obj2, BinaryOperatorType.GreaterOrEqual);
                }
                if ((obj3 != null) && (obj2 == null))
                {
                    return new BinaryOperator(metric.Path, obj3, BinaryOperatorType.LessOrEqual);
                }
                IComparable comparable = obj2 as IComparable;
                IComparable comparable2 = obj3 as IComparable;
                if ((comparable != null) && (comparable2 != null))
                {
                    int num = comparable.CompareTo(comparable2);
                    if (num == 0)
                    {
                        return new BinaryOperator(metric.Path, obj2, BinaryOperatorType.Equal);
                    }
                    if (num > 0)
                    {
                        obj2 = values[1];
                        obj3 = values[0];
                    }
                }
                return new BetweenOperator(metric.Path, obj2, obj3);
            }, null);

        internal static bool Match(CustomUIFilterType filterType) => 
            filterType == CustomUIFilterType.Between;

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly CustomUIFilterBetween.<>c <>9 = new CustomUIFilterBetween.<>c();
            public static Func<ICustomUIFiltersOptions, bool> <>9__2_0;

            internal bool <AllowCore>b__2_0(ICustomUIFiltersOptions opt) => 
                opt.ShowComparisons;
        }

        private sealed class BetweenCriteriaParser : CustomUIFilter.CustomUIFilterCriteriaParser
        {
            private readonly Type type;
            private readonly object[] values;

            internal BetweenCriteriaParser(CustomUIFilterType filterType, string path, Type type) : base(filterType, path)
            {
                this.values = new object[2];
                this.type = type;
            }

            protected sealed override void OnBetweenOperator(BetweenOperator theOperator)
            {
                OperandProperty testExpression = theOperator.TestExpression as OperandProperty;
                if (base.IsInvalid(testExpression))
                {
                    base.MarkInvalid();
                }
                else
                {
                    OperandValue beginExpression = theOperator.BeginExpression as OperandValue;
                    OperandValue endExpression = theOperator.EndExpression as OperandValue;
                    if ((beginExpression == null) || (endExpression == null))
                    {
                        base.MarkInvalid();
                    }
                    else if (!CustomUIFilter.TryGetValue(beginExpression, this.type, out this.values[0]) || !CustomUIFilter.TryGetValue(endExpression, this.type, out this.values[1]))
                    {
                        base.MarkInvalid();
                    }
                }
            }

            protected sealed override void OnGroupOperator(GroupOperator theOperator)
            {
                GroupOperator @operator = theOperator;
                if ((@operator == null) || (@operator.OperatorType != GroupOperatorType.And))
                {
                    base.MarkInvalid();
                }
                else if (((@operator.Operands.Count != 2) || (!this.TryParseBinary(@operator.Operands[0]) && !this.TryParseIsNull(@operator.Operands[0]))) || (!this.TryParseBinary(@operator.Operands[1]) && !this.TryParseIsNull(@operator.Operands[1])))
                {
                    base.MarkInvalid();
                }
            }

            protected sealed override object[] PrepareLocalValues() => 
                base.Prepare(this.values);

            private bool TryParseBinary(CriteriaOperator criteria)
            {
                BinaryOperator @operator = criteria as BinaryOperator;
                if ((@operator == null) || base.IsInvalid(@operator.LeftOperand as OperandProperty))
                {
                    return false;
                }
                OperandValue rightOperand = @operator.RightOperand as OperandValue;
                return ((rightOperand != null) && (((@operator.OperatorType != BinaryOperatorType.GreaterOrEqual) || ((this.values[0] != null) || !CustomUIFilter.TryGetValue(rightOperand, this.type, out this.values[0]))) ? ((@operator.OperatorType == BinaryOperatorType.LessOrEqual) && ((this.values[1] == null) && CustomUIFilter.TryGetValue(rightOperand, this.type, out this.values[1]))) : true));
            }

            private bool TryParseIsNull(CriteriaOperator criteria)
            {
                UnaryOperator @operator = criteria as UnaryOperator;
                if ((@operator != null) && (@operator.OperatorType == UnaryOperatorType.IsNull))
                {
                    return !base.IsInvalid(@operator.Operand as OperandProperty);
                }
                base.MarkInvalid();
                return false;
            }
        }
    }
}

