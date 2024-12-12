namespace DevExpress.Xpf.Core.ConditionalFormatting.Printing
{
    using DevExpress.Export.Xl;
    using DevExpress.Xpf.Grid;
    using DevExpress.XtraExport.Helpers;
    using System;
    using System.Drawing;

    public class FormatConditionRuleColorScale3ExportWrapper : FormatConditionRuleColorScale2ExportWrapper, IFormatConditionRule3ColorScale, IFormatConditionRuleColorScaleBase, IFormatConditionRuleBase, IFormatConditionRuleMinMaxBase
    {
        public FormatConditionRuleColorScale3ExportWrapper(ColorScaleFormatCondition formatCondition) : base(formatCondition)
        {
        }

        public Color MidpointColor =>
            GetColor(base.FormatCondition.Format.ColorMiddle.Value);

        public object MidpointValue
        {
            get
            {
                decimal? nullable1;
                decimal? nullable5;
                if ((base.ActualMinValue == null) || (base.ActualMaxValue == null))
                {
                    return null;
                }
                decimal? actualMinValue = base.ActualMinValue;
                decimal? actualMaxValue = base.ActualMaxValue;
                if ((actualMinValue != null) & (actualMaxValue != null))
                {
                    nullable1 = new decimal?(actualMinValue.GetValueOrDefault() + actualMaxValue.GetValueOrDefault());
                }
                else
                {
                    nullable1 = null;
                }
                decimal? nullable = nullable1;
                decimal num = 2;
                if (nullable != null)
                {
                    nullable5 = new decimal?(nullable.GetValueOrDefault() / num);
                }
                else
                {
                    actualMaxValue = null;
                    nullable5 = actualMaxValue;
                }
                return nullable5;
            }
        }

        public XlCondFmtValueObjectType MidpointType =>
            XlCondFmtValueObjectType.Number;
    }
}

