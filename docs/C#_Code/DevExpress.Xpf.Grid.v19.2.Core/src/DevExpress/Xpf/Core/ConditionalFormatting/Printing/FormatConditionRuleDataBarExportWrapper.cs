namespace DevExpress.Xpf.Core.ConditionalFormatting.Printing
{
    using DevExpress.Export.Xl;
    using DevExpress.Xpf.Core.ConditionalFormatting;
    using DevExpress.Xpf.Core.ConditionalFormatting.Native;
    using DevExpress.Xpf.Grid;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Drawing;
    using System.Windows.Media;

    public class FormatConditionRuleDataBarExportWrapper : FormatConditionRuleBase, IFormatConditionRuleDataBar, IFormatConditionRuleBase, IFormatConditionRuleMinMaxBase
    {
        private readonly DataBarFormatCondition FormatCondition;
        private IndicatorFormatConditionExportWrapperDelegate indicatorDelegate;

        public FormatConditionRuleDataBarExportWrapper(DataBarFormatCondition formatCondition)
        {
            this.FormatCondition = formatCondition;
            this.indicatorDelegate = new IndicatorFormatConditionExportWrapperDelegate(formatCondition);
        }

        private bool IsGradientFill() => 
            (this.Format.Fill is LinearGradientBrush) || (this.Format.FillNegative is LinearGradientBrush);

        private DataBarFormat Format =>
            this.FormatCondition.Format;

        public bool GradientFill =>
            this.IsGradientFill();

        public bool AllowNegativeAxis =>
            true;

        public System.Drawing.Color AxisColor =>
            GetColor(this.Format.ZeroLineBrush, true);

        public System.Drawing.Color BorderColor =>
            GetColor(this.Format.BorderBrush, true);

        public int Direction =>
            1;

        public bool DrawAxis =>
            true;

        public bool DrawAxisAtMiddle =>
            false;

        public System.Drawing.Color FillColor =>
            GetColor(this.Format.Fill, true);

        public object MaxValue =>
            IndicatorFormatConditionInfo.GetParsedDecimalValue(this.FormatCondition.MaxValue);

        public object MinValue =>
            IndicatorFormatConditionInfo.GetParsedDecimalValue(this.FormatCondition.MinValue);

        public System.Drawing.Color NegativeBorderColor =>
            GetColor(this.Format.BorderBrushNegative, true);

        public System.Drawing.Color NegativeFillColor =>
            GetColor(this.Format.FillNegative, false);

        public string PredefinedName =>
            this.FormatCondition.PredefinedFormatName;

        public bool ShowBarOnly =>
            false;

        public XlCondFmtValueObjectType MaxType =>
            XlCondFmtValueObjectType.Number;

        public XlCondFmtValueObjectType MinType =>
            XlCondFmtValueObjectType.Number;

        public override bool IsValid =>
            this.indicatorDelegate.IsValid;
    }
}

