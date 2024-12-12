namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class TextThatContainsConditionalFormattingDialogViewModel : ExpressionConditionalFormattingDialogViewModel
    {
        protected TextThatContainsConditionalFormattingDialogViewModel(IFormatsOwner owner) : base(owner, ConditionalFormattingStringId.ConditionalFormatting_TextThatContainsDialog_Title, ConditionalFormattingStringId.ConditionalFormatting_TextThatContainsDialog_Description, ConditionalFormattingStringId.ConditionalFormatting_TextThatContainsDialog_Connector)
        {
        }

        protected override CriteriaOperator GetCriteria(string fieldName)
        {
            CriteriaOperator[] operands = new CriteriaOperator[] { new OperandProperty(fieldName), new OperandValue(this.Value) };
            return new FunctionOperator(FunctionOperatorType.Contains, operands);
        }

        internal override object GetInitialValue() => 
            null;

        public static Func<IFormatsOwner, ConditionalFormattingDialogViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IFormatsOwner), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return (Func<IFormatsOwner, ConditionalFormattingDialogViewModel>) ViewModelSource.Factory<IFormatsOwner, TextThatContainsConditionalFormattingDialogViewModel>(Expression.Lambda<Func<IFormatsOwner, TextThatContainsConditionalFormattingDialogViewModel>>(Expression.New((ConstructorInfo) methodof(TextThatContainsConditionalFormattingDialogViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public override DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType ConditionValueType =>
            DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.SingleText;
    }
}

