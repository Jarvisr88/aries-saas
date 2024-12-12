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

    public class ContainCellValueViewModel : ContainItemViewModel, IConditionalFormattingDialogViewModel
    {
        private ContainOperator betweenOperator;
        private ContainOperator notBetweenOperator;

        protected ContainCellValueViewModel(IDialogContext context) : base(context)
        {
            base.Operators = this.GetOperators();
            this.Operator = base.Operators.First<ContainOperator>();
        }

        private static object[] ExtractBetween(CriteriaOperator op)
        {
            BetweenOperator @operator = (BetweenOperator) op;
            return new object[] { ExtractProperty(@operator.TestExpression), ExtractOperandValue(@operator.BeginExpression), ExtractOperandValue(@operator.EndExpression) };
        }

        private static object[] ExtractBinary(CriteriaOperator op)
        {
            BinaryOperator @operator = (BinaryOperator) op;
            object[] objArray1 = new object[3];
            objArray1[0] = ExtractProperty(@operator.LeftOperand);
            objArray1[1] = ExtractOperandValue(@operator.RightOperand);
            return objArray1;
        }

        private static object[] ExtractNotBetween(CriteriaOperator op) => 
            ExtractBetween(((UnaryOperator) op).Operand);

        private IEnumerable<ContainOperator> GetOperators()
        {
            this.betweenOperator = new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_Between), ConditionRule.Between, FormatDisplayInfoHelper.GetExpressionFactory(ConditionRule.Between), new Func<CriteriaOperator, object[]>(ContainCellValueViewModel.ExtractBetween));
            this.notBetweenOperator = new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_NotBetween), ConditionRule.NotBetween, FormatDisplayInfoHelper.GetExpressionFactory(ConditionRule.NotBetween), new Func<CriteriaOperator, object[]>(ContainCellValueViewModel.ExtractNotBetween));
            return new ContainOperator[] { this.betweenOperator, this.notBetweenOperator, new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_EqualTo), ConditionRule.Equal, FormatDisplayInfoHelper.GetExpressionFactory(ConditionRule.Equal), new Func<CriteriaOperator, object[]>(ContainCellValueViewModel.ExtractBinary)), new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_NotEqualTo), ConditionRule.NotEqual, FormatDisplayInfoHelper.GetExpressionFactory(ConditionRule.NotEqual), new Func<CriteriaOperator, object[]>(ContainCellValueViewModel.ExtractBinary)), new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_GreaterThan), ConditionRule.Greater, FormatDisplayInfoHelper.GetExpressionFactory(ConditionRule.Greater), new Func<CriteriaOperator, object[]>(ContainCellValueViewModel.ExtractBinary)), new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_LessThan), ConditionRule.Less, FormatDisplayInfoHelper.GetExpressionFactory(ConditionRule.Less), new Func<CriteriaOperator, object[]>(ContainCellValueViewModel.ExtractBinary)), new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_GreaterOrEqual), ConditionRule.GreaterOrEqual, FormatDisplayInfoHelper.GetExpressionFactory(ConditionRule.GreaterOrEqual), new Func<CriteriaOperator, object[]>(ContainCellValueViewModel.ExtractBinary)), new ContainOperator(base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_LessOrEqual), ConditionRule.LessOrEqual, FormatDisplayInfoHelper.GetExpressionFactory(ConditionRule.LessOrEqual), new Func<CriteriaOperator, object[]>(ContainCellValueViewModel.ExtractBinary)) };
        }

        private DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType GetRangeType()
        {
            Func<DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType> dateTime = <>c.<>9__27_0;
            if (<>c.<>9__27_0 == null)
            {
                Func<DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType> local1 = <>c.<>9__27_0;
                dateTime = <>c.<>9__27_0 = () => DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.RangeDateTime;
            }
            return ExpressionConditionalFormattingDialogViewModel.SelectValue<DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType>(base.GetExpressionColumn().FieldType, dateTime, <>c.<>9__27_1 ??= () => DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.RangeNumeric, <>c.<>9__27_2 ??= () => DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.RangeText);
        }

        private DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType GetSingleType()
        {
            Func<DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType> dateTime = <>c.<>9__26_0;
            if (<>c.<>9__26_0 == null)
            {
                Func<DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType> local1 = <>c.<>9__26_0;
                dateTime = <>c.<>9__26_0 = () => DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.SingleDateTime;
            }
            return ExpressionConditionalFormattingDialogViewModel.SelectValue<DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType>(base.GetExpressionColumn().FieldType, dateTime, <>c.<>9__26_1 ??= () => DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.SingleNumeric, <>c.<>9__26_2 ??= () => DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.SingleText);
        }

        protected internal override object[] GetValues()
        {
            List<object> list = new List<object> {
                this.Value
            };
            if (this.IsBetween)
            {
                list.Add(this.Value2);
            }
            return list.ToArray();
        }

        protected void OnOperatorChanged()
        {
            this.IsBetween = ReferenceEquals(this.Operator, this.betweenOperator) || ReferenceEquals(this.Operator, this.notBetweenOperator);
        }

        protected override void ProcessExtractedValues(object[] values)
        {
            base.ProcessExtractedValues(values);
            this.Value = values[1];
            this.Value2 = values[2];
        }

        public static Func<IDialogContext, ContainCellValueViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, ContainCellValueViewModel>(Expression.Lambda<Func<IDialogContext, ContainCellValueViewModel>>(Expression.New((ConstructorInfo) methodof(ContainCellValueViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public virtual object Value { get; set; }

        public virtual object Value2 { get; set; }

        public virtual bool IsBetween { get; protected set; }

        public DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType ConditionValueType =>
            this.IsBetween ? this.GetRangeType() : this.GetSingleType();

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_CellValue);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContainCellValueViewModel.<>c <>9 = new ContainCellValueViewModel.<>c();
            public static Func<ConditionValueType> <>9__26_0;
            public static Func<ConditionValueType> <>9__26_1;
            public static Func<ConditionValueType> <>9__26_2;
            public static Func<ConditionValueType> <>9__27_0;
            public static Func<ConditionValueType> <>9__27_1;
            public static Func<ConditionValueType> <>9__27_2;

            internal ConditionValueType <GetRangeType>b__27_0() => 
                ConditionValueType.RangeDateTime;

            internal ConditionValueType <GetRangeType>b__27_1() => 
                ConditionValueType.RangeNumeric;

            internal ConditionValueType <GetRangeType>b__27_2() => 
                ConditionValueType.RangeText;

            internal ConditionValueType <GetSingleType>b__26_0() => 
                ConditionValueType.SingleDateTime;

            internal ConditionValueType <GetSingleType>b__26_1() => 
                ConditionValueType.SingleNumeric;

            internal ConditionValueType <GetSingleType>b__26_2() => 
                ConditionValueType.SingleText;
        }
    }
}

