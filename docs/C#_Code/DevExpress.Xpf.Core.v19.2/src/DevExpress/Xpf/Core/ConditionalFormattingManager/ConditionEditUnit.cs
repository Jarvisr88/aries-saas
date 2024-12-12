namespace DevExpress.Xpf.Core.ConditionalFormattingManager
{
    using DevExpress.Data.Filtering;
    using DevExpress.Mvvm.UI.Native.ViewGenerator.Model;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using System;

    public class ConditionEditUnit : ExpressionEditUnit
    {
        private ConditionRule valueRule;
        private object value1;
        private object value2;

        public override IModelItem BuildCondition(IConditionModelItemsBuilder builder, IModelItem source) => 
            builder.BuildCondition(this, source);

        internal override CriteriaOperator GetActualExpressionCriteriaOperator() => 
            FormatDisplayInfoHelper.GetCriteria(this.ValueRule, base.Expression, base.FieldName, this.Value1, this.Value2);

        public override string GetDescription(IDialogContext context)
        {
            CriteriaOperator actualExpressionCriteriaOperator = this.GetActualExpressionCriteriaOperator();
            if (actualExpressionCriteriaOperator == null)
            {
                return string.Empty;
            }
            if (context == null)
            {
                return actualExpressionCriteriaOperator.ToString();
            }
            string filterOperatorCustomText = context.GetFilterOperatorCustomText(actualExpressionCriteriaOperator);
            return ((filterOperatorCustomText != null) ? filterOperatorCustomText : ManagerHelper.ConvertExpression(actualExpressionCriteriaOperator.ToString(), context.ColumnInfo, new Func<IDataColumnInfo, string, string>(UnboundExpressionConvertHelper.ConvertToCaption)));
        }

        public ConditionRule ValueRule
        {
            get => 
                this.valueRule;
            set
            {
                if (this.valueRule != value)
                {
                    this.valueRule = value;
                }
                base.RegisterPropertyModification("ValueRule");
            }
        }

        public object Value1
        {
            get => 
                this.value1;
            set
            {
                if (this.value1 != value)
                {
                    this.value1 = value;
                }
                base.RegisterPropertyModification("Value1");
            }
        }

        public object Value2
        {
            get => 
                this.value2;
            set
            {
                if (this.value2 != value)
                {
                    this.value2 = value;
                }
                base.RegisterPropertyModification("Value2");
            }
        }
    }
}

