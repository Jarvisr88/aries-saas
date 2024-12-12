namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class GreaterThanConditionalFormattingDialogViewModel : ExpressionConditionalFormattingDialogViewModel
    {
        protected GreaterThanConditionalFormattingDialogViewModel(IFormatsOwner owner) : base(owner, ConditionalFormattingStringId.ConditionalFormatting_GreaterThanDialog_Title, ConditionalFormattingStringId.ConditionalFormatting_GreaterThanDialog_Description, ConditionalFormattingStringId.ConditionalFormatting_GreaterThanDialog_Connector)
        {
        }

        protected override CriteriaOperator GetCriteria(string fieldName) => 
            GetCriteria(fieldName, this.Value);

        public static CriteriaOperator GetCriteria(string fieldName, object value) => 
            new BinaryOperator(fieldName, value, BinaryOperatorType.Greater);

        internal override ConditionRule GetValueRule() => 
            ConditionRule.Greater;

        public static Func<IFormatsOwner, ConditionalFormattingDialogViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IFormatsOwner), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return (Func<IFormatsOwner, ConditionalFormattingDialogViewModel>) ViewModelSource.Factory<IFormatsOwner, GreaterThanConditionalFormattingDialogViewModel>(Expression.Lambda<Func<IFormatsOwner, GreaterThanConditionalFormattingDialogViewModel>>(Expression.New((ConstructorInfo) methodof(GreaterThanConditionalFormattingDialogViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }
    }
}

