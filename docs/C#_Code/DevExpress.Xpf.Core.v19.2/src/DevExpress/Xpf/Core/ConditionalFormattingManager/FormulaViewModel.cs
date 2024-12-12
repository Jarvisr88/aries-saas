namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.POCO;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;
    using System.Collections.Generic;
    using System.Linq.Expressions;
    using System.Reflection;
    using System.Runtime.CompilerServices;

    public class FormulaViewModel : FormatEditorOwnerViewModel
    {
        public FormulaViewModel(IDialogContext context) : base(context)
        {
            this.ExpressionEditorViewModel = CustomConditionConditionalFormattingDialogViewModel.Factory(context.PredefinedFormatsOwner);
            this.ExpressionEditorViewModel.Initialize(context);
        }

        protected override void AddChanges(ExpressionEditUnit unit)
        {
            ConditionEditUnit unit2 = unit as ConditionEditUnit;
            if (unit2 != null)
            {
                base.AddChanges(unit2);
                string fieldName = base.Context.ColumnInfo.FieldName;
                unit2.FieldName = fieldName;
                unit2.Expression = this.ExpressionEditorViewModel.GetExpression(fieldName);
                unit2.ValueRule = ConditionRule.Expression;
                unit2.Value1 = null;
                unit2.Value2 = null;
            }
        }

        protected override bool CanInitCore(ExpressionEditUnit unit) => 
            unit is ConditionEditUnit;

        protected override ExpressionEditUnit CreateEditUnit() => 
            new ConditionEditUnit();

        protected override void InitCore(ExpressionEditUnit unit)
        {
            base.InitCore(unit);
            CriteriaOperator actualExpressionCriteriaOperator = unit.GetActualExpressionCriteriaOperator();
            if ((actualExpressionCriteriaOperator != null) && (base.Context != null))
            {
                if (this.ExpressionEditorViewModel.IsReadOnly)
                {
                    this.ExpressionEditorViewModel.Value = actualExpressionCriteriaOperator.ToString();
                }
                else
                {
                    this.ExpressionEditorViewModel.Value = ManagerHelper.ConvertExpression(actualExpressionCriteriaOperator.ToString(), base.Context.ColumnInfo, new Func<IDataColumnInfo, string, string>(UnboundExpressionConvertHelper.ConvertToCaption));
                }
            }
        }

        protected override bool ValidateExpression() => 
            this.ExpressionEditorViewModel.TryClose();

        public static Func<IDialogContext, FormulaViewModel> Factory
        {
            get
            {
                ParameterExpression expression = Expression.Parameter(typeof(IDialogContext), "x");
                Expression[] expressionArray1 = new Expression[] { expression };
                ParameterExpression[] parameters = new ParameterExpression[] { expression };
                return ViewModelSource.Factory<IDialogContext, FormulaViewModel>(Expression.Lambda<Func<IDialogContext, FormulaViewModel>>(Expression.New((ConstructorInfo) methodof(FormulaViewModel..ctor), (IEnumerable<Expression>) expressionArray1), parameters));
            }
        }

        public CustomConditionConditionalFormattingDialogViewModel ExpressionEditorViewModel { get; private set; }

        public override string Description =>
            base.GetLocalizedString(ConditionalFormattingStringId.ConditionalFormatting_Manager_FormulaDescription);
    }
}

