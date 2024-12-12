namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class Top10PercentConditionalFormattingDialogViewModel : TopBottomConditionalFormattingDialogViewModel
    {
        protected Top10PercentConditionalFormattingDialogViewModel(IFormatsOwner owner) : base(owner, ConditionalFormattingStringId.ConditionalFormatting_Top10PercentDialog_Title, ConditionalFormattingStringId.ConditionalFormatting_Top10PercentDialog_Description, ConditionalFormattingStringId.ConditionalFormatting_Top10PercentDialog_Connector)
        {
        }

        public static Func<IFormatsOwner, ConditionalFormattingDialogViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IFormatsOwner), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return (Func<IFormatsOwner, ConditionalFormattingDialogViewModel>) ViewModelSource.Factory<IFormatsOwner, Top10PercentConditionalFormattingDialogViewModel>(Expression.Lambda<Func<IFormatsOwner, Top10PercentConditionalFormattingDialogViewModel>>(Expression.New((ConstructorInfo) methodof(Top10PercentConditionalFormattingDialogViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public override DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType ConditionValueType =>
            DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.Percent;

        protected override TopBottomRule RuleKind =>
            TopBottomRule.TopPercent;
    }
}

