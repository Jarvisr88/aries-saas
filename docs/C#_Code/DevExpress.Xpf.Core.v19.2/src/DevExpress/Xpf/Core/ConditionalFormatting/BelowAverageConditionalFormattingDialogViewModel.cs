namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class BelowAverageConditionalFormattingDialogViewModel : TopBottomConditionalFormattingDialogViewModel
    {
        protected BelowAverageConditionalFormattingDialogViewModel(IFormatsOwner owner) : base(owner, ConditionalFormattingStringId.ConditionalFormatting_BelowAverageDialog_Title, ConditionalFormattingStringId.ConditionalFormatting_BelowAverageDialog_Description, ConditionalFormattingStringId.ConditionalFormatting_BelowAverageDialog_Connector)
        {
        }

        public static Func<IFormatsOwner, ConditionalFormattingDialogViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IFormatsOwner), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return (Func<IFormatsOwner, ConditionalFormattingDialogViewModel>) ViewModelSource.Factory<IFormatsOwner, BelowAverageConditionalFormattingDialogViewModel>(Expression.Lambda<Func<IFormatsOwner, BelowAverageConditionalFormattingDialogViewModel>>(Expression.New((ConstructorInfo) methodof(BelowAverageConditionalFormattingDialogViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public override DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType ConditionValueType =>
            DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.None;

        protected override TopBottomRule RuleKind =>
            TopBottomRule.BelowAverage;
    }
}

