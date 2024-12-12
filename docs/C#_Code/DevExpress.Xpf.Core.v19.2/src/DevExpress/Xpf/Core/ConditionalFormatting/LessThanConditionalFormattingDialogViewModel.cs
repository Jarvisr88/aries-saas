namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class LessThanConditionalFormattingDialogViewModel : ExpressionConditionalFormattingDialogViewModel
    {
        protected LessThanConditionalFormattingDialogViewModel(IFormatsOwner owner) : base(owner, ConditionalFormattingStringId.ConditionalFormatting_LessThanDialog_Title, ConditionalFormattingStringId.ConditionalFormatting_LessThanDialog_Description, ConditionalFormattingStringId.ConditionalFormatting_LessThanDialog_Connector)
        {
        }

        protected override CriteriaOperator GetCriteria(string fieldName) => 
            new BinaryOperator(fieldName, this.Value, BinaryOperatorType.Less);

        internal override ConditionRule GetValueRule() => 
            ConditionRule.Less;

        public static Func<IFormatsOwner, ConditionalFormattingDialogViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IFormatsOwner), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return (Func<IFormatsOwner, ConditionalFormattingDialogViewModel>) ViewModelSource.Factory<IFormatsOwner, LessThanConditionalFormattingDialogViewModel>(Expression.Lambda<Func<IFormatsOwner, LessThanConditionalFormattingDialogViewModel>>(Expression.New((ConstructorInfo) methodof(LessThanConditionalFormattingDialogViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }
    }
}

