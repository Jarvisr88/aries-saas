namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.Native;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class AboveBelowViewModel : TopBottomViewModelBase
    {
        protected AboveBelowViewModel(IDialogContext context) : base(context)
        {
        }

        protected override bool CanInitCore(ExpressionEditUnit unit)
        {
            Func<TopBottomEditUnit, bool> evaluator = <>c.<>9__9_0;
            if (<>c.<>9__9_0 == null)
            {
                Func<TopBottomEditUnit, bool> local1 = <>c.<>9__9_0;
                evaluator = <>c.<>9__9_0 = x => x.IsAboveBelowCondition();
            }
            return ((unit as TopBottomEditUnit).If<TopBottomEditUnit>(evaluator) != null);
        }

        protected override TopBottomRule GetRule() => 
            (this.Rule == AboveBelowOperatorType.Above) ? TopBottomRule.AboveAverage : TopBottomRule.BelowAverage;

        protected override void InitRule(TopBottomRule rule, double threshold)
        {
            this.Rule = (rule == TopBottomRule.AboveAverage) ? AboveBelowOperatorType.Above : AboveBelowOperatorType.Below;
        }

        public static Func<IDialogContext, AboveBelowViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, AboveBelowViewModel>(Expression.Lambda<Func<IDialogContext, AboveBelowViewModel>>(Expression.New((ConstructorInfo) methodof(AboveBelowViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public virtual AboveBelowOperatorType Rule { get; set; }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_AboveBelowDescription);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AboveBelowViewModel.<>c <>9 = new AboveBelowViewModel.<>c();
            public static Func<TopBottomEditUnit, bool> <>9__9_0;

            internal bool <CanInitCore>b__9_0(TopBottomEditUnit x) => 
                x.IsAboveBelowCondition();
        }
    }
}

