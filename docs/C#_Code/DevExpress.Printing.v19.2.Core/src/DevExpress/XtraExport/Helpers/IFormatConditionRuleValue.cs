namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;

    public interface IFormatConditionRuleValue : IFormatConditionRuleBase
    {
        XlDifferentialFormatting Appearance { get; }

        FormatConditions Condition { get; }

        string Expression { get; }

        object Value1 { get; }

        object Value2 { get; }
    }
}

