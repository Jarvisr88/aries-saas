namespace DevExpress.Xpf.Core.ConditionalFormatting.Printing
{
    using DevExpress.Export.Xl;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Grid;
    using DevExpress.XtraExport.Helpers;
    using System;

    public class FormatConditionRuleUniqueDuplicateExportWrapper : FormatConditionRuleBase, IFormatConditionRuleUniqueDuplicate, IFormatConditionRuleBase
    {
        private readonly UniqueDuplicateRule rule;
        private readonly XlDifferentialFormatting formatting;

        public FormatConditionRuleUniqueDuplicateExportWrapper(UniqueDuplicateRuleFormatCondition formatCondition)
        {
            this.rule = formatCondition.Rule;
            this.formatting = new FormatConverter(formatCondition.Format).GetXlDifferentialFormatting();
        }

        public bool Duplicate =>
            this.rule == UniqueDuplicateRule.Duplicate;

        public bool Unique =>
            this.rule == UniqueDuplicateRule.Unique;

        public XlDifferentialFormatting Formatting =>
            this.formatting;
    }
}

