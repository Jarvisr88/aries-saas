namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class EqualToConditionalFormattingDialogViewModel : ExpressionConditionalFormattingDialogViewModel
    {
        protected EqualToConditionalFormattingDialogViewModel(IFormatsOwner owner) : base(owner, ConditionalFormattingStringId.ConditionalFormatting_EqualToDialog_Title, ConditionalFormattingStringId.ConditionalFormatting_EqualToDialog_Description, ConditionalFormattingStringId.ConditionalFormatting_EqualToDialog_Connector)
        {
        }

        protected override CriteriaOperator GetCriteria(string fieldName) => 
            GetCriteria(fieldName, this.Value);

        public static CriteriaOperator GetCriteria(string fieldName, object value) => 
            new BinaryOperator(fieldName, value, BinaryOperatorType.Equal);

        internal override ConditionRule GetValueRule() => 
            ConditionRule.Equal;

        public static Func<IFormatsOwner, ConditionalFormattingDialogViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IFormatsOwner), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return (Func<IFormatsOwner, ConditionalFormattingDialogViewModel>) ViewModelSource.Factory<IFormatsOwner, EqualToConditionalFormattingDialogViewModel>(Expression.Lambda<Func<IFormatsOwner, EqualToConditionalFormattingDialogViewModel>>(Expression.New((ConstructorInfo) methodof(EqualToConditionalFormattingDialogViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }
    }
}

