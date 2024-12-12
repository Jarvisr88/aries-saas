namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public interface IAggregateOperand
    {
        CriteriaOperator Condition { get; set; }

        object AggregationObject { get; set; }

        CriteriaOperator AggregatedExpression { get; set; }

        Aggregate AggregateType { get; set; }
    }
}

