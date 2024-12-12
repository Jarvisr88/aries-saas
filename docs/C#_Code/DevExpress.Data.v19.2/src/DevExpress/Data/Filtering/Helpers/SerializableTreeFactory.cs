namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;

    public class SerializableTreeFactory : INodesFactory
    {
        public IGroupNode Create(GroupType type, ICollection<INode> subNodes);
        public IClauseNode Create(ClauseType type, OperandProperty firstOperand, ICollection<CriteriaOperator> operands);
    }
}

