namespace DevExpress.Xpf.Core.ConditionalFormatting.Printing
{
    using DevExpress.Data.Filtering;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormattingManager;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Runtime.CompilerServices;

    public class DateOccurringConditionRuleDetector
    {
        private IList<DateOccurringOperator> Operators;

        public DateOccurringConditionRuleDetector()
        {
            this.InitOperators();
        }

        public DateOccurringConditionRule DetectRule(string expression)
        {
            CriteriaOperator criteriaOperator = CriteriaOperator.TryParse(expression, new object[0]);
            if (criteriaOperator == null)
            {
                return DateOccurringConditionRule.None;
            }
            DateOccurringOperator @operator = this.Operators.FirstOrDefault<DateOccurringOperator>(x => x.Match(criteriaOperator));
            return ((@operator != null) ? @operator.Rule : DateOccurringConditionRule.None);
        }

        private void InitOperators()
        {
            this.Operators = new List<DateOccurringOperator>();
            foreach (DateOccurringConditionalFormattingDialogViewModel.OperatorFactory factory in DateOccurringConditionalFormattingDialogViewModel.GetFactories())
            {
                this.Operators.Add(new DateOccurringOperator(factory.ToString(), factory.Rule, (op, _) => factory.Factory(op)));
            }
        }

        private class DateOccurringOperator : ContainOperatorBase<DateOccurringConditionRule>
        {
            public DateOccurringOperator(string name, DateOccurringConditionRule rule, Func<OperandProperty, OperandValue[], CriteriaOperator> factory) : this(name, rule, factory, func1)
            {
                Func<CriteriaOperator, object[]> func1 = <>c.<>9__0_0;
                if (<>c.<>9__0_0 == null)
                {
                    Func<CriteriaOperator, object[]> local1 = <>c.<>9__0_0;
                    func1 = <>c.<>9__0_0 = op => new object[0];
                }
            }

            [Serializable, CompilerGenerated]
            private sealed class <>c
            {
                public static readonly DateOccurringConditionRuleDetector.DateOccurringOperator.<>c <>9 = new DateOccurringConditionRuleDetector.DateOccurringOperator.<>c();
                public static Func<CriteriaOperator, object[]> <>9__0_0;

                internal object[] <.ctor>b__0_0(CriteriaOperator op) => 
                    new object[0];
            }
        }
    }
}

