namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System.Collections.Generic;

    public interface INodesFactory
    {
        IGroupNode Create(GroupType type, ICollection<INode> subNodes);
        IClauseNode Create(ClauseType type, OperandProperty firstOperand, ICollection<CriteriaOperator> operands);
    }
}

