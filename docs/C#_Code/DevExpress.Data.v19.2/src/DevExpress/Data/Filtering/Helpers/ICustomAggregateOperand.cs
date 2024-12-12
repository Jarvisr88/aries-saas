namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public interface ICustomAggregateOperand : IAggregateOperand
    {
        string CustomAggregateName { get; set; }

        CriteriaOperatorCollection CustomAggregateOperands { get; }
    }
}

