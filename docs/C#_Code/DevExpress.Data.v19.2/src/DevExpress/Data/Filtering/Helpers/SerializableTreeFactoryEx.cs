namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System.Collections.Generic;

    public class SerializableTreeFactoryEx : SerializableTreeFactory, INodesFactoryEx, INodesFactory
    {
        IAggregateNode INodesFactoryEx.Create(OperandProperty firstOperand, Aggregate aggregate, OperandProperty aggregateOperand, ClauseType operation, ICollection<CriteriaOperator> operands, INode conditionNode);
    }
}

