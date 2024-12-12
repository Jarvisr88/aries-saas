namespace DevExpress.Xpf.Core.ConditionalFormatting.Printing
{
    using DevExpress.Export.Xl;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Grid;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Drawing;

    public class FormatConditionRuleColorScale2ExportWrapper : FormatConditionRuleBase, IFormatConditionRule2ColorScale, IFormatConditionRuleColorScaleBase, IFormatConditionRuleBase, IFormatConditionRuleMinMaxBase
    {
        protected readonly ColorScaleFormatCondition FormatCondition;
        private IndicatorFormatConditionExportWrapperDelegate indicatorDelegate;

        public FormatConditionRuleColorScale2ExportWrapper(ColorScaleFormatCondition formatCondition)
        {
            this.FormatCondition = formatCondition;
            this.indicatorDelegate = new IndicatorFormatConditionExportWrapperDelegate(formatCondition);
        }

        public Color MaxColor =>
            GetColor(this.FormatCondition.Format.ColorMax);

        public object MaxValue =>
            IndicatorFormatConditionInfo.GetParsedDecimalValue(this.FormatCondition.MaxValue);

        public XlCondFmtValueObjectType MaxType =>
            XlCondFmtValueObjectType.Number;

        public override bool IsValid =>
            this.indicatorDelegate.IsValid;

        public Color MinColor =>
            GetColor(this.FormatCondition.Format.ColorMin);

        public object MinValue =>
            IndicatorFormatConditionInfo.GetParsedDecimalValue(this.FormatCondition.MinValue);

        public XlCondFmtValueObjectType MinType =>
            XlCondFmtValueObjectType.Number;

        protected decimal? ActualMinValue =>
            this.MinValue as decimal?;

        protected decimal? ActualMaxValue =>
            this.MaxValue as decimal?;
    }
}

