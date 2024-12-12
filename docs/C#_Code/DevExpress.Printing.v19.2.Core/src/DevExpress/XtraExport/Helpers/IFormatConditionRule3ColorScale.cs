namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;
    using System.Drawing;

    public interface IFormatConditionRule3ColorScale : IFormatConditionRuleColorScaleBase, IFormatConditionRuleBase, IFormatConditionRuleMinMaxBase
    {
        Color MidpointColor { get; }

        XlCondFmtValueObjectType MidpointType { get; }

        object MidpointValue { get; }
    }
}

