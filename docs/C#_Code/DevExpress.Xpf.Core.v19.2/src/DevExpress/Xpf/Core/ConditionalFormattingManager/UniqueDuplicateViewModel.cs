namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class UniqueDuplicateViewModel : FormatEditorOwnerViewModel
    {
        protected UniqueDuplicateViewModel(IDialogContext context) : base(context)
        {
            this.Rule = this.ConvertToManagerRule(UniqueDuplicateRule.Duplicate);
        }

        protected override void AddChanges(ExpressionEditUnit unit)
        {
            UniqueDuplicateEditUnit unit2 = unit as UniqueDuplicateEditUnit;
            if (unit2 != null)
            {
                base.AddChanges(unit2);
                unit2.FieldName = base.Context.ColumnInfo.FieldName;
                unit2.Rule = this.ConvertToConditionRule(this.Rule);
            }
        }

        protected override bool CanInitCore(ExpressionEditUnit unit) => 
            unit is UniqueDuplicateEditUnit;

        private UniqueDuplicateRule ConvertToConditionRule(UniqueDuplicateRuleType rule)
        {
            if (rule == UniqueDuplicateRuleType.Unique)
            {
                return UniqueDuplicateRule.Unique;
            }
            if (rule != UniqueDuplicateRuleType.Duplicate)
            {
                throw new InvalidOperationException();
            }
            return UniqueDuplicateRule.Duplicate;
        }

        private UniqueDuplicateRuleType ConvertToManagerRule(UniqueDuplicateRule rule)
        {
            if (rule == UniqueDuplicateRule.Unique)
            {
                return UniqueDuplicateRuleType.Unique;
            }
            if (rule != UniqueDuplicateRule.Duplicate)
            {
                throw new InvalidOperationException();
            }
            return UniqueDuplicateRuleType.Duplicate;
        }

        protected override ExpressionEditUnit CreateEditUnit() => 
            new UniqueDuplicateEditUnit();

        protected override void InitCore(ExpressionEditUnit unit)
        {
            UniqueDuplicateEditUnit unit2 = unit as UniqueDuplicateEditUnit;
            if (unit2 != null)
            {
                base.InitCore(unit2);
                this.Rule = this.ConvertToManagerRule(unit2.Rule);
            }
        }

        public static Func<IDialogContext, UniqueDuplicateViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, UniqueDuplicateViewModel>(Expression.Lambda<Func<IDialogContext, UniqueDuplicateViewModel>>(Expression.New((ConstructorInfo) methodof(UniqueDuplicateViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public virtual UniqueDuplicateRuleType Rule { get; set; }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_UniqueDuplicateDescription);
    }
}

