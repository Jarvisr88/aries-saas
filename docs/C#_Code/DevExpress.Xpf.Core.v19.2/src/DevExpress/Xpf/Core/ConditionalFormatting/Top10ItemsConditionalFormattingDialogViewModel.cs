namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class Top10ItemsConditionalFormattingDialogViewModel : TopBottomConditionalFormattingDialogViewModel
    {
        protected Top10ItemsConditionalFormattingDialogViewModel(IFormatsOwner owner) : base(owner, ConditionalFormattingStringId.ConditionalFormatting_Top10ItemsDialog_Title, ConditionalFormattingStringId.ConditionalFormatting_Top10ItemsDialog_Description, ConditionalFormattingStringId.ConditionalFormatting_Top10ItemsDialog_Connector)
        {
        }

        public static Func<IFormatsOwner, ConditionalFormattingDialogViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IFormatsOwner), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return (Func<IFormatsOwner, ConditionalFormattingDialogViewModel>) ViewModelSource.Factory<IFormatsOwner, Top10ItemsConditionalFormattingDialogViewModel>(Expression.Lambda<Func<IFormatsOwner, Top10ItemsConditionalFormattingDialogViewModel>>(Expression.New((ConstructorInfo) methodof(Top10ItemsConditionalFormattingDialogViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public override DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType ConditionValueType =>
            DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.ItemCount;

        protected override TopBottomRule RuleKind =>
            TopBottomRule.TopItems;
    }
}

