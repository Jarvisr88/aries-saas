namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;

    public interface IAggregateNode : IClauseNode, INode
    {
        DevExpress.Data.Filtering.Aggregate Aggregate { get; }

        OperandProperty AggregateOperand { get; }

        INode AggregateCondition { get; set; }
    }
}

