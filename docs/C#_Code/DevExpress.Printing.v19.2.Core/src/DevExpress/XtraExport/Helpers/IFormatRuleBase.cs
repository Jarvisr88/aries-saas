namespace DevExpress.XtraExport.Helpers
{
    using System;

    public interface IFormatRuleBase
    {
        bool StopIfTrue { get; }

        bool Enabled { get; }

        bool ApplyToRow { get; }

        IColumn Column { get; }

        IColumn ColumnApplyTo { get; }

        string ColumnName { get; }

        string Name { get; }

        IFormatConditionRuleBase Rule { get; }

        object Tag { get; set; }
    }
}

