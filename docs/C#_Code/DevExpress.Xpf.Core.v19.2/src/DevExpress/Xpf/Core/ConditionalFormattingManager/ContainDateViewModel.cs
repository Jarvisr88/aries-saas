namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ContainDateViewModel : ContainItemViewModel
    {
        protected ContainDateViewModel(IDialogContext context) : base(context)
        {
            base.Operators = this.GetOperators();
            this.Operator = base.Operators.First<ContainOperator>();
        }

        private static object[] ExtractDate(CriteriaOperator op)
        {
            FunctionOperator testExpression = null;
            if (op is FunctionOperator)
            {
                testExpression = op as FunctionOperator;
            }
            else if (op is BetweenOperator)
            {
                testExpression = ((BetweenOperator) op).TestExpression as FunctionOperator;
            }
            else if (op is BinaryOperator)
            {
                testExpression = ((BinaryOperator) op).LeftOperand as FunctionOperator;
            }
            if (testExpression != null)
            {
                Func<CriteriaOperator, bool> predicate = <>c.<>9__3_0;
                if (<>c.<>9__3_0 == null)
                {
                    Func<CriteriaOperator, bool> local1 = <>c.<>9__3_0;
                    predicate = <>c.<>9__3_0 = x => x is OperandProperty;
                }
                CriteriaOperator operator2 = testExpression.Operands.FirstOrDefault<CriteriaOperator>(predicate);
                if (operator2 != null)
                {
                    return new object[] { ExtractProperty(operator2) };
                }
            }
            return new object[1];
        }

        private IEnumerable<ContainOperator> GetOperators()
        {
            Func<DateOccurringConditionalFormattingDialogViewModel.OperatorFactory, ContainOperator> selector = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<DateOccurringConditionalFormattingDialogViewModel.OperatorFactory, ContainOperator> local1 = <>c.<>9__2_0;
                selector = <>c.<>9__2_0 = x => new ContainOperator(x.ToString(), ConditionRule.Expression, (op, _) => x.Factory(op), new Func<CriteriaOperator, object[]>(ContainDateViewModel.ExtractDate));
            }
            return DateOccurringConditionalFormattingDialogViewModel.GetFactories().Select<DateOccurringConditionalFormattingDialogViewModel.OperatorFactory, ContainOperator>(selector).ToArray<ContainOperator>();
        }

        public static Func<IDialogContext, ContainDateViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, ContainDateViewModel>(Expression.Lambda<Func<IDialogContext, ContainDateViewModel>>(Expression.New((ConstructorInfo) methodof(ContainDateViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_DatesOccurring);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContainDateViewModel.<>c <>9 = new ContainDateViewModel.<>c();
            public static Func<DateOccurringConditionalFormattingDialogViewModel.OperatorFactory, ContainOperator> <>9__2_0;
            public static Func<CriteriaOperator, bool> <>9__3_0;

            internal bool <ExtractDate>b__3_0(CriteriaOperator x) => 
                x is OperandProperty;

            internal ContainOperator <GetOperators>b__2_0(DateOccurringConditionalFormattingDialogViewModel.OperatorFactory x) => 
                new ContainOperator(x.ToString(), ConditionRule.Expression, (op, _) => x.Factory(op), new Func<CriteriaOperator, object[]>(ContainDateViewModel.ExtractDate));
        }
    }
}

