namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ContainBlanksViewModel : ContainItemViewModel
    {
        private ContainOperator emptyOp;
        private ContainOperator emptyStringOp;
        private readonly bool isBlank;

        protected ContainBlanksViewModel(IDialogContext context, bool isBlank) : base(context)
        {
            this.isBlank = isBlank;
            base.Operators = this.GetOperators();
            this.Operator = base.Context.ColumnInfo.FieldType.Equals(typeof(string)) ? this.emptyStringOp : this.emptyOp;
        }

        private static object[] ExtractNotNull(CriteriaOperator op) => 
            ExtractUnary(((UnaryOperator) op).Operand);

        private static object[] ExtractNotNullOrEmpty(CriteriaOperator op) => 
            ExtractNullOrEmpty(((UnaryOperator) op).Operand);

        private static object[] ExtractNullOrEmpty(CriteriaOperator op) => 
            new object[] { ExtractProperty(((FunctionOperator) op).Operands[0]) };

        private static object[] ExtractUnary(CriteriaOperator op) => 
            new object[] { ExtractProperty(((UnaryOperator) op).Operand) };

        private IEnumerable<ContainOperator> GetOperators()
        {
            if (this.isBlank)
            {
                Func<OperandProperty, OperandValue[], CriteriaOperator> factory = <>c.<>9__4_0;
                if (<>c.<>9__4_0 == null)
                {
                    Func<OperandProperty, OperandValue[], CriteriaOperator> local1 = <>c.<>9__4_0;
                    factory = <>c.<>9__4_0 = (op, _) => op.IsNull();
                }
                this.emptyOp = new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_Blanks), ConditionRule.Expression, factory, new Func<CriteriaOperator, object[]>(ContainBlanksViewModel.ExtractUnary));
                Func<OperandProperty, OperandValue[], CriteriaOperator> func3 = <>c.<>9__4_1;
                if (<>c.<>9__4_1 == null)
                {
                    Func<OperandProperty, OperandValue[], CriteriaOperator> local2 = <>c.<>9__4_1;
                    func3 = <>c.<>9__4_1 = (op, _) => new FunctionOperator(FunctionOperatorType.IsNullOrEmpty, new CriteriaOperator[] { op });
                }
                this.emptyStringOp = new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_Blanks), ConditionRule.Expression, func3, new Func<CriteriaOperator, object[]>(ContainBlanksViewModel.ExtractNullOrEmpty));
            }
            else
            {
                Func<OperandProperty, OperandValue[], CriteriaOperator> factory = <>c.<>9__4_2;
                if (<>c.<>9__4_2 == null)
                {
                    Func<OperandProperty, OperandValue[], CriteriaOperator> local3 = <>c.<>9__4_2;
                    factory = <>c.<>9__4_2 = (op, _) => op.IsNotNull();
                }
                this.emptyOp = new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_NoBlanks), ConditionRule.Expression, factory, new Func<CriteriaOperator, object[]>(ContainBlanksViewModel.ExtractNotNull));
                Func<OperandProperty, OperandValue[], CriteriaOperator> func4 = <>c.<>9__4_3;
                if (<>c.<>9__4_3 == null)
                {
                    Func<OperandProperty, OperandValue[], CriteriaOperator> local4 = <>c.<>9__4_3;
                    func4 = <>c.<>9__4_3 = (op, _) => new FunctionOperator(FunctionOperatorType.IsNullOrEmpty, new CriteriaOperator[] { op }).Not();
                }
                this.emptyStringOp = new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_NoBlanks), ConditionRule.Expression, func4, new Func<CriteriaOperator, object[]>(ContainBlanksViewModel.ExtractNotNullOrEmpty));
            }
            return new ContainOperator[] { this.emptyOp, this.emptyStringOp };
        }

        public static Func<IDialogContext, bool, ContainBlanksViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                ParameterExpression expression2 = Expression.Parameter(typeof(bool), "y");
                Expression[] expressionArray1 = new Expression[] { expression, expression2 };
                ParameterExpression[] parameters = new ParameterExpression[] { expression, expression2 };
                return ViewModelSource.Factory<IDialogContext, bool, ContainBlanksViewModel>(Expression.Lambda<Func<IDialogContext, bool, ContainBlanksViewModel>>(Expression.New((ConstructorInfo) methodof(ContainBlanksViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public override string Description =>
            this.isBlank ? base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_Blanks) : base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_NoBlanks);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContainBlanksViewModel.<>c <>9 = new ContainBlanksViewModel.<>c();
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__4_0;
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__4_1;
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__4_2;
            public static Func<OperandProperty, OperandValue[], CriteriaOperator> <>9__4_3;

            internal CriteriaOperator <GetOperators>b__4_0(OperandProperty op, OperandValue[] _) => 
                op.IsNull();

            internal CriteriaOperator <GetOperators>b__4_1(OperandProperty op, OperandValue[] _)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { op };
                return new FunctionOperator(FunctionOperatorType.IsNullOrEmpty, operands);
            }

            internal CriteriaOperator <GetOperators>b__4_2(OperandProperty op, OperandValue[] _) => 
                op.IsNotNull();

            internal CriteriaOperator <GetOperators>b__4_3(OperandProperty op, OperandValue[] _)
            {
                CriteriaOperator[] operands = new CriteriaOperator[] { op };
                return new FunctionOperator(FunctionOperatorType.IsNullOrEmpty, operands).Not();
            }
        }
    }
}

