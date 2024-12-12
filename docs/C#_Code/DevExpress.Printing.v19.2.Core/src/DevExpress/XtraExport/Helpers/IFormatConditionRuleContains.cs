namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System.Collections;

    public interface IFormatConditionRuleContains : IFormatConditionRuleBase
    {
        IList Values { get; }

        XlDifferentialFormatting Appearance { get; }
    }
}

