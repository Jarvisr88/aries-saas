namespace DevExpress.Xpf.Core.ConditionalFormatting.Printing
{
    using DevExpress.Export.Xl;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Grid;
    using DevExpress.XtraExport.Helpers;
    using System;

    public class FormatConditionRuleAboveBelowAverageExportWrapper : FormatConditionRuleBase, IFormatConditionRuleAboveBelowAverage, IFormatConditionRuleBase
    {
        private readonly XlCondFmtAverageCondition condition;
        private readonly XlDifferentialFormatting formatting;

        public FormatConditionRuleAboveBelowAverageExportWrapper(TopBottomRuleFormatCondition formatCondition)
        {
            TopBottomRule rule = formatCondition.Rule;
            if (rule == TopBottomRule.AboveAverage)
            {
                this.condition = XlCondFmtAverageCondition.Above;
            }
            else if (rule == TopBottomRule.BelowAverage)
            {
                this.condition = XlCondFmtAverageCondition.Below;
            }
            this.formatting = new FormatConverter(formatCondition.Format).GetXlDifferentialFormatting();
        }

        public XlCondFmtAverageCondition Condition =>
            this.condition;

        public XlDifferentialFormatting Formatting =>
            this.formatting;
    }
}

