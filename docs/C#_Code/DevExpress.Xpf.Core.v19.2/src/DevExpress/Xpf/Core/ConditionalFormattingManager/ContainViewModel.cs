namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class ContainViewModel : FormatEditorOwnerViewModel
    {
        protected ContainViewModel(IDialogContext context) : base(context)
        {
            this.ViewModels = this.GetViewModels();
            this.SelectedViewModel = this.ViewModels.First<ContainItemViewModel>();
        }

        protected override void AddChanges(ExpressionEditUnit unit)
        {
            ConditionEditUnit unit2 = unit as ConditionEditUnit;
            if (unit2 != null)
            {
                base.AddChanges(unit2);
                unit2.FieldName = base.Context.ColumnInfo.FieldName;
                if (this.ForceExpressionMode || (((ConditionRule) this.SelectedViewModel.Operator.Rule) == ConditionRule.Expression))
                {
                    unit2.Expression = this.SelectedViewModel.Expression;
                    unit2.ValueRule = ConditionRule.Expression;
                }
                else
                {
                    unit2.ValueRule = this.SelectedViewModel.Operator.Rule;
                    object[] values = this.SelectedViewModel.GetValues();
                    unit2.Value1 = this.GetValue(0, values);
                    unit2.Value2 = this.GetValue(1, values);
                }
            }
        }

        protected override bool CanInitCore(ExpressionEditUnit unit) => 
            this.IsContainCondition(unit);

        protected override ExpressionEditUnit CreateEditUnit() => 
            new ConditionEditUnit();

        private static string ExtractExpressionName(ContainOperator template, CriteriaOperator op)
        {
            object[] objArray = template.Extractor(op);
            return (((objArray == null) || (objArray.Length == 0)) ? null : (objArray[0] as string));
        }

        private object GetValue(int index, object[] values)
        {
            object source = ManagerHelper.SafeGetValue(index, values);
            if (source != null)
            {
                source = ManagerHelperBase.ConvertRuleValue(source, base.Context.ColumnInfo.FieldType, this.IsInDesignMode());
            }
            return source;
        }

        private IEnumerable<ContainItemViewModel> GetViewModels() => 
            new ContainItemViewModel[] { ContainCellValueViewModel.Factory(base.Context), ContainTextViewModel.Factory(base.Context), ContainDateViewModel.Factory(base.Context), ContainBlanksViewModel.Factory(base.Context, true), ContainBlanksViewModel.Factory(base.Context, false) };

        protected override void InitCore(ExpressionEditUnit unit)
        {
            ConditionEditUnit conditionUnit = unit as ConditionEditUnit;
            if (conditionUnit != null)
            {
                Action<ContainItemViewModel> action;
                Func<ContainOperator, bool> predicate;
                base.InitCore(conditionUnit);
                this.ForceExpressionMode = conditionUnit.ValueRule == ConditionRule.Expression;
                if (!this.ForceExpressionMode)
                {
                    predicate = x => x.Match(conditionUnit.ValueRule);
                    action = y => y.InitFromRule(conditionUnit);
                }
                else
                {
                    CriteriaOperator op = CriteriaOperator.TryParse(conditionUnit.Expression, new object[0]);
                    if (op == null)
                    {
                        return;
                    }
                    predicate = x => x.Match(op);
                    action = y => y.InitFromCriteria(op);
                }
                ContainItemViewModel model = this.ViewModels.FirstOrDefault<ContainItemViewModel>(x => x.Operators.Any<ContainOperator>(predicate));
                if (model != null)
                {
                    this.SelectedViewModel = model;
                    action(this.SelectedViewModel);
                }
            }
        }

        internal bool IsContainCondition(ExpressionEditUnit unit)
        {
            Func<ContainOperator, bool> func;
            ConditionEditUnit unit2 = unit as ConditionEditUnit;
            if (unit2 == null)
            {
                return false;
            }
            ConditionRule rule = unit2.ValueRule;
            if (rule != ConditionRule.Expression)
            {
                func = x => x.Match(rule);
            }
            else
            {
                CriteriaOperator op = CriteriaOperator.TryParse(unit.Expression, new object[0]);
                if (op == null)
                {
                    return false;
                }
                string expressionName = unit2.FieldName;
                func = x => x.Match(op) && (ExtractExpressionName(x, op) == expressionName);
            }
            Func<ContainItemViewModel, IEnumerable<ContainOperator>> selector = <>c.<>9__2_2;
            if (<>c.<>9__2_2 == null)
            {
                Func<ContainItemViewModel, IEnumerable<ContainOperator>> local1 = <>c.<>9__2_2;
                selector = <>c.<>9__2_2 = x => x.Operators;
            }
            return this.ViewModels.SelectMany<ContainItemViewModel, ContainOperator>(selector).Any<ContainOperator>(func);
        }

        public static Func<IDialogContext, ContainViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, ContainViewModel>(Expression.Lambda<Func<IDialogContext, ContainViewModel>>(Expression.New((ConstructorInfo) methodof(ContainViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public IEnumerable<ContainItemViewModel> ViewModels { get; private set; }

        public virtual ContainItemViewModel SelectedViewModel { get; set; }

        public bool ForceExpressionMode { get; set; }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_CellsContainDescription);

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly ContainViewModel.<>c <>9 = new ContainViewModel.<>c();
            public static Func<ContainItemViewModel, IEnumerable<ContainOperator>> <>9__2_2;

            internal IEnumerable<ContainOperator> <IsContainCondition>b__2_2(ContainItemViewModel x) => 
                x.Operators;
        }
    }
}

