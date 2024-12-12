namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class AddConditionViewModel : ConditionViewModelBase
    {
        protected AddConditionViewModel(IDialogContext context) : base(context)
        {
        }

        protected override IEnumerable<IConditionEditor> CreateChildViewModels()
        {
            List<IConditionEditor> list = new List<IConditionEditor>();
            if (base.Context.IsVirtualSource)
            {
                list.Add(ContainViewModel.Factory(base.Context));
                list.Add(FormulaViewModel.Factory(base.Context));
            }
            else
            {
                list.Add(ValueBasedViewModel.Factory(base.Context));
                list.Add(ContainViewModel.Factory(base.Context));
                list.Add(TopBottomViewModel.Factory(base.Context));
                list.Add(AboveBelowViewModel.Factory(base.Context));
                if (!base.Context.IsPivot)
                {
                    list.Add(UniqueDuplicateViewModel.Factory(base.Context));
                }
                if (!base.Context.IsPivot && base.Context.AllowAnimations)
                {
                    list.Add(AnimationRuleViewModel.Factory(base.Context));
                }
                list.Add(FormulaViewModel.Factory(base.Context));
            }
            return list;
        }

        public static Func<IDialogContext, AddConditionViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, AddConditionViewModel>(Expression.Lambda<Func<IDialogContext, AddConditionViewModel>>(Expression.New((ConstructorInfo) methodof(AddConditionViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public override string Description =>
            this.GetLocalizedString((base.source == null) ? ConditionalFormattingStringId.ConditionalFormatting_Manager_NewFormattingRule : ConditionalFormattingStringId.ConditionalFormatting_Manager_EditFormattingRule);
    }
}

