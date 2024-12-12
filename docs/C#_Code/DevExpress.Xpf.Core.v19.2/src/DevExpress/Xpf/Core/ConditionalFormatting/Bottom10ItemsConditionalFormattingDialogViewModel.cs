namespace DevExpress.Xpf.Core.ConditionalFormatting
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class Bottom10ItemsConditionalFormattingDialogViewModel : TopBottomConditionalFormattingDialogViewModel
    {
        protected Bottom10ItemsConditionalFormattingDialogViewModel(IFormatsOwner owner) : base(owner, ConditionalFormattingStringId.ConditionalFormatting_Bottom10ItemsDialog_Title, ConditionalFormattingStringId.ConditionalFormatting_Bottom10ItemsDialog_Description, ConditionalFormattingStringId.ConditionalFormatting_Bottom10ItemsDialog_Connector)
        {
        }

        public static Func<IFormatsOwner, ConditionalFormattingDialogViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IFormatsOwner), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return (Func<IFormatsOwner, ConditionalFormattingDialogViewModel>) ViewModelSource.Factory<IFormatsOwner, Bottom10ItemsConditionalFormattingDialogViewModel>(Expression.Lambda<Func<IFormatsOwner, Bottom10ItemsConditionalFormattingDialogViewModel>>(Expression.New((ConstructorInfo) methodof(Bottom10ItemsConditionalFormattingDialogViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public override DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType ConditionValueType =>
            DevExpress.Xpf.Core.ConditionalFormatting.ConditionValueType.ItemCount;

        protected override TopBottomRule RuleKind =>
            TopBottomRule.BottomItems;
    }
}

