namespace DevExpress.Xpf.Core.ConditionalFormatting.Printing
{
    using DevExpress.Export.Xl;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Grid;
    using DevExpress.XtraExport.Helpers;
    using System;

    public class FormatConditionRuleTopBottomExportWrapper : FormatConditionRuleBase, IFormatConditionRuleTopBottom, IFormatConditionRuleBase
    {
        private readonly XlDifferentialFormatting appearance;
        private readonly TopBottomRuleFormatCondition FormatCondition;
        private const int MinRank = 1;
        private const int MaxPercent = 100;
        private const int MaxItemCount = 0x3e8;

        public FormatConditionRuleTopBottomExportWrapper(TopBottomRuleFormatCondition formatCondition)
        {
            this.FormatCondition = formatCondition;
            this.appearance = new FormatConverter(formatCondition.Format).GetXlDifferentialFormatting();
        }

        private int GetRank() => 
            Convert.ToInt32(Math.Min(this.Percent ? ((double) 100) : ((double) 0x3e8), Math.Max(1.0, this.FormatCondition.Threshold)));

        public XlDifferentialFormatting Appearance =>
            this.appearance;

        public int Rank =>
            this.GetRank();

        public bool Bottom =>
            (this.FormatCondition.Rule == TopBottomRule.BottomItems) || (this.FormatCondition.Rule == TopBottomRule.BottomPercent);

        public bool Percent =>
            (this.FormatCondition.Rule == TopBottomRule.BottomPercent) || (this.FormatCondition.Rule == TopBottomRule.TopPercent);
    }
}

