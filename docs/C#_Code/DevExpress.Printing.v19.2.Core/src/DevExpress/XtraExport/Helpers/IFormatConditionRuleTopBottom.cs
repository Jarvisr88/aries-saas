namespace DevExpress.XtraExport.Helpers
{
    using DevExpress.Export.Xl;
    using System;

    public interface IFormatConditionRuleTopBottom : IFormatConditionRuleBase
    {
        int Rank { get; }

        bool Bottom { get; }

        bool Percent { get; }

        XlDifferentialFormatting Appearance { get; }
    }
}

