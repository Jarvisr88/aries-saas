namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;

    public interface IFormatConditionRuleMinMaxBase
    {
        XlCondFmtValueObjectType MaxType { get; }

        XlCondFmtValueObjectType MinType { get; }

        object MaxValue { get; }

        object MinValue { get; }
    }
}

