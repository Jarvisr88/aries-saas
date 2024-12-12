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

    public class ContainTextViewModel : ContainItemViewModel
    {
        protected ContainTextViewModel(IDialogContext column) : base(column)
        {
            base.Operators = this.GetOperators();
            this.Operator = base.Operators.First<ContainOperator>();
        }

        private static object[] ExtractFunction(CriteriaOperator op)
        {
            FunctionOperator @operator = (FunctionOperator) op;
            return new object[] { ExtractProperty(@operator.Operands[0]), ExtractOperandValue(@operator.Operands[1]) };
        }

        private static object[] ExtractUnary(CriteriaOperator op) => 
            ExtractFunction(((UnaryOperator) op).Operand);

        private IEnumerable<ContainOperator> GetOperators()
        {
            ContainOperator[] operatorArray1 = new ContainOperator[4];
            Func<OperandProperty, OperandValue[], CriteriaOperator> factory = <>c.<>9__2_0;
            if (<>c.<>9__2_0 == null)
            {
                Func<OperandProperty, OperandValue[], CriteriaOperator> local1 = <>c.<>9__2_0;
                factory = <>c.<>9__2_0 = (op, ov) => new FunctionOperator(FunctionOperatorType.Contains, new CriteriaOperator[] { op, ov[0] });
            }
            operatorArray1[0] = new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_Containing), ConditionRule.Expression, factory, new Func<CriteriaOperator, object[]>(ContainTextViewModel.ExtractFunction));
            Func<OperandProperty, OperandValue[], CriteriaOperator> func2 = <>c.<>9__2_1;
            if (<>c.<>9__2_1 == null)
            {
                Func<OperandProperty, OperandValue[], CriteriaOperator> local2 = <>c.<>9__2_1;
                func2 = <>c.<>9__2_1 = (op, ov) => new UnaryOperator(UnaryOperatorType.Not, new FunctionOperator(FunctionOperatorType.Contains, new CriteriaOperator[] { op, ov[0] }));
            }
            operatorArray1[1] = new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_NotContaining), ConditionRule.Expression, func2, new Func<CriteriaOperator, object[]>(ContainTextViewModel.ExtractUnary));
            Func<OperandProperty, OperandValue[], CriteriaOperator> func3 = <>c.<>9__2_2;
            if (<>c.<>9__2_2 == null)
            {
                Func<OperandProperty, OperandValue[], CriteriaOperator> local3 = <>c.<>9__2_2;
                func3 = <>c.<>9__2_2 = (op, ov) => new FunctionOperator(FunctionOperatorType.StartsWith, new CriteriaOperator[] { op, ov[0] });
            }
            operatorArray1[2] = new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_BeginningWith), ConditionRule.Expression, func3, new Func<CriteriaOperator, object[]>(ContainTextViewModel.ExtractFunction));
            Func<OperandProperty, OperandValue[], CriteriaOperator> func4 = <>c.<>9__2_3;
            if (<>c.<>9__2_3 == null)
            {
                Func<OperandProperty, OperandValue[], CriteriaOperator> local4 = <>c.<>9__2_3;
                func4 = <>c.<>9__2_3 = (op, ov) => new FunctionOperator(FunctionOperatorType.EndsWith, new CriteriaOperator[] { op, ov[0] });
            }
            operatorArray1[3] = new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_EndingWith), ConditionRule.Expression, func4, new Func<CriteriaOperator, object[]>(ContainTextViewModel.ExtractFunction));
            return operatorArray1;
        }

        protected internal override object[] GetValues() => 
            new object[] { this.Value };

        protected override void ProcessExtractedValues(object[] values)
        {
            base.ProcessExtractedValues(values);
            this.Value = values[1];
        }

        public static Func<IDialogContext, ContainTextViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, ContainTextViewModel>(Expression.Lambda<Func<IDialogContext, ContainTextViewModel>>(Expression.New((ConstructorInfo) methodof(ContainTextViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public virtual object Value { get; set; }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_SpecificText);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContainTextViewModel.<>c <>9 = new ContainTextViewModel.<>c();
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__2_0;
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__2_1;
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__2_2;
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__2_3;

            internal CriteriaOperator <GetOperators>b__2_0(OperandProperty op, OperandValue[] ov)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { op, ov[0] };
                return new FunctionOperator(FunctionOperatorType.Contains, operands);
            }

            internal CriteriaOperator <GetOperators>b__2_1(OperandProperty op, OperandValue[] ov)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { op, ov[0] };
                return new UnaryOperator(UnaryOperatorType.Not, new FunctionOperator(FunctionOperatorType.Contains, operands));
            }

            internal CriteriaOperator <GetOperators>b__2_2(OperandProperty op, OperandValue[] ov)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { op, ov[0] };
                return new FunctionOperator(FunctionOperatorType.StartsWith, operands);
            }

            internal CriteriaOperator <GetOperators>b__2_3(OperandProperty op, OperandValue[] ov)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { op, ov[0] };
                return new FunctionOperator(FunctionOperatorType.EndsWith, operands);
            }
        }
    }
}

