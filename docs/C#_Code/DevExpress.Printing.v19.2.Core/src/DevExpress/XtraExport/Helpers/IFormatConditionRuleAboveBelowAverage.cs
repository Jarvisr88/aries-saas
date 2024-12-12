namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;

    public interface IFormatConditionRuleAboveBelowAverage : IFormatConditionRuleBase
    {
        XlCondFmtAverageCondition Condition { get; }

        XlDifferentialFormatting Formatting { get; }
    }
}

