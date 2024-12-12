namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;

    public interface IFormatConditionRuleExpression : IFormatConditionRuleBase
    {
        XlDifferentialFormatting Appearance { get; }

        string Expression { get; }
    }
}

