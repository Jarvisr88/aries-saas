namespace DevExpress.XtraEditors.Filtering
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;

    public class FilterControlNodesFactory : INodesFactory, INodesFactoryEx
    {
        private FilterTreeNodeModel model;

        public FilterControlNodesFactory(FilterTreeNodeModel model);
        public IGroupNode Create(GroupType type, ICollection<INode> subNodes);
        public IClauseNode Create(ClauseType type, OperandProperty firstOperand, ICollection<CriteriaOperator> operands);
        public IAggregateNode Create(OperandProperty firstOperand, Aggregate aggregate, OperandProperty aggregateOperand, ClauseType operation, ICollection<CriteriaOperator> operands, INode conditionNode);
        protected void SetClauseNodeValues(ClauseNode node, ClauseType type, OperandProperty firstOperand, ICollection<CriteriaOperator> operands);

        protected internal FilterTreeNodeModel Model { get; }
    }
}

