namespace DevExpress.Xpf.Core.ConditionalFormatting.Printing
{
    using DevExpress.Export.Xl;
    using DevExpress.Xpf.Grid;
    using DevExpress.XtraExport.Helpers;
    using System;

    public class FormatConditionRuleValueExportWrapper : FormatConditionRuleBase, IFormatConditionRuleValue, IFormatConditionRuleBase
    {
        private readonly DevExpress.Xpf.Grid.FormatCondition FormatCondition;
        private readonly XlDifferentialFormatting appearance;

        public FormatConditionRuleValueExportWrapper(DevExpress.Xpf.Grid.FormatCondition formatCondition)
        {
            this.FormatCondition = formatCondition;
            this.appearance = new FormatConverter(this.FormatCondition.Format).GetXlDifferentialFormatting();
        }

        private object GetConvertedValue(object value)
        {
            if ((base.ColumnType != null) && ((value != null) && !base.ColumnType.Equals(value.GetType())))
            {
                try
                {
                    value = Convert.ChangeType(value, base.ColumnType);
                }
                catch
                {
                    value = null;
                }
            }
            return value;
        }

        public XlDifferentialFormatting Appearance =>
            this.appearance;

        public FormatConditions Condition =>
            (FormatConditions) this.FormatCondition.ValueRule;

        public string Expression =>
            this.FormatCondition.Expression;

        public object Value1 =>
            this.GetConvertedValue(this.FormatCondition.Value1);

        public object Value2 =>
            this.GetConvertedValue(this.FormatCondition.Value2);
    }
}

