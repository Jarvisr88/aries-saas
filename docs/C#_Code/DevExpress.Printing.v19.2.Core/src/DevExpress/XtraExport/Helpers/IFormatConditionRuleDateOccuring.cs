namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System.Collections.Generic;

    public interface IFormatConditionRuleDateOccuring : IFormatConditionRuleBase
    {
        IEnumerable<XlCondFmtTimePeriod> DateTypes { get; }

        XlDifferentialFormatting Formatting { get; }
    }
}

