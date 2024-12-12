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

    public class TopBottomViewModel : TopBottomViewModelBase
    {
        protected TopBottomViewModel(IDialogContext context) : base(context)
        {
            this.Threshold = 10.0;
        }

        protected override void AddChanges(ExpressionEditUnit unit)
        {
            base.AddChanges(unit);
            TopBottomEditUnit unit2 = unit as TopBottomEditUnit;
            if (unit2 != null)
            {
                unit2.Threshold = this.Threshold;
            }
        }

        protected override bool CanInitCore(ExpressionEditUnit unit)
        {
            Func<TopBottomEditUnit, bool> evaluator = <>c.<>9__19_0;
            if (<>c.<>9__19_0 == null)
            {
                Func<TopBottomEditUnit, bool> local1 = <>c.<>9__19_0;
                evaluator = <>c.<>9__19_0 = x => x.IsTopBottomCondition();
            }
            return ((unit as TopBottomEditUnit).If<TopBottomEditUnit>(evaluator) != null);
        }

        protected override TopBottomRule GetRule() => 
            (this.Rule == TopBottomOperatorType.Top) ? this.GetRule(TopBottomRule.TopItems, TopBottomRule.TopPercent) : this.GetRule(TopBottomRule.BottomItems, TopBottomRule.BottomPercent);

        private TopBottomRule GetRule(TopBottomRule itemsRule, TopBottomRule percentRule) => 
            this.UsePercent ? percentRule : itemsRule;

        protected override void InitRule(TopBottomRule rule, double threshold)
        {
            this.UsePercent = (rule == TopBottomRule.BottomPercent) || (rule == TopBottomRule.TopPercent);
            this.Rule = ((rule == TopBottomRule.TopItems) || (rule == TopBottomRule.TopPercent)) ? TopBottomOperatorType.Top : TopBottomOperatorType.Bottom;
            this.Threshold = threshold;
        }

        public static Func<IDialogContext, TopBottomViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, TopBottomViewModel>(Expression.Lambda<Func<IDialogContext, TopBottomViewModel>>(Expression.New((ConstructorInfo) methodof(TopBottomViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public virtual TopBottomOperatorType Rule { get; set; }

        public virtual double Threshold { get; set; }

        public virtual bool UsePercent { get; set; }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_TopBottomDescription);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly TopBottomViewModel.<>c <>9 = new TopBottomViewModel.<>c();
            public static Func<TopBottomEditUnit, bool> <>9__19_0;

            internal bool <CanInitCore>b__19_0(TopBottomEditUnit x) => 
                x.IsTopBottomCondition();
        }
    }
}

