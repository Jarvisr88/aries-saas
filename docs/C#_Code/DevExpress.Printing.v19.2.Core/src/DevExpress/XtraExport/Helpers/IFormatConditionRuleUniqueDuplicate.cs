namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;

    public interface IFormatConditionRuleUniqueDuplicate : IFormatConditionRuleBase
    {
        bool Unique { get; }

        bool Duplicate { get; }

        XlDifferentialFormatting Formatting { get; }
    }
}

