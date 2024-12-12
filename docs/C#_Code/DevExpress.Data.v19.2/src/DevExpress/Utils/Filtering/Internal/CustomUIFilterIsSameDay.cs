namespace DevExpress.Utils.Filtering.Internal
{
    using DevExpress.Data.Filtering;
    using System;

    internal sealed class CustomUIFilterIsSameDay : CustomUIFilter
    {
        public CustomUIFilterIsSameDay(CustomUIFilterType filterType, Func<IServiceProvider> getServiceProvider) : base(filterType, getServiceProvider)
        {
        }

        protected sealed override ICustomUIFilterCriteriaParser CreateCriteriaParser(IEndUserFilteringMetric metric) => 
            new IsSameDayCriteriaParser(base.id, metric.Path, metric.Type);

        protected sealed override CriteriaOperator GetCriteria(IEndUserFilteringMetric metric, ICustomUIFilterValue filterValue)
        {
            OperandProperty property = new OperandProperty(metric.Path);
            OperandValue value2 = new OperandValue(filterValue.Value);
            CriteriaOperator[] operands = new CriteriaOperator[] { property, value2 };
            return new FunctionOperator(FunctionOperatorType.IsSameDay, operands);
        }

        internal static bool Match(CustomUIFilterType filterType) => 
            filterType == CustomUIFilterType.IsSameDay;

        private sealed class IsSameDayCriteriaParser : CustomUIFilter.CustomUIFilterCriteriaParser
        {
            private readonly Type type;
            private readonly object[] values;

            internal IsSameDayCriteriaParser(CustomUIFilterType filterType, string path, Type type) : base(filterType, path)
            {
                this.values = new object[1];
                this.type = type;
            }

            protected sealed override void OnFunctionOperator(FunctionOperator theOperator)
            {
                if ((theOperator.OperatorType != FunctionOperatorType.IsSameDay) || (theOperator.Operands.Count != 2))
                {
                    base.MarkInvalid();
                }
                else
                {
                    OperandProperty property = theOperator.Operands[0] as OperandProperty;
                    OperandValue value2 = theOperator.Operands[1] as OperandValue;
                    if (base.IsInvalid(property) || !CustomUIFilter.TryGetValue(value2, this.type, out this.values[0]))
                    {
                        base.MarkInvalid();
                    }
                }
            }

            protected sealed override object[] PrepareLocalValues() => 
                base.Prepare(this.values);
        }
    }
}

