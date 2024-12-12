namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;

    public class ValueBasedViewModel : ConditionViewModelBase
    {
        protected ValueBasedViewModel(IDialogContext context) : base(context)
        {
        }

        protected override IEnumerable<IConditionEditor> CreateChildViewModels() => 
            new IConditionEditor[] { ColorScaleViewModel.Factory(base.Context, false), ColorScaleViewModel.Factory(base.Context, true), DataBarViewModel.Factory(base.Context), IconViewModel.Factory(base.Context) };

        public static Func<IDialogContext, ValueBasedViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, ValueBasedViewModel>(Expression.Lambda<Func<IDialogContext, ValueBasedViewModel>>(Expression.New((ConstructorInfo) methodof(ValueBasedViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_ValueBasedDescription);
    }
}

