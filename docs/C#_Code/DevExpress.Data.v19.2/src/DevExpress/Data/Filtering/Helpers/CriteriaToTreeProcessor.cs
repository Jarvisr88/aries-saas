namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Runtime.InteropServices;

    public class CriteriaToTreeProcessor : IClientCriteriaVisitor<INode>, ICriteriaVisitor<INode>
    {
        protected readonly INodesFactory Factory;
        protected readonly INodesFactoryEx FactoryEx;
        private readonly bool supportFunctions;
        public readonly IList<CriteriaOperator> Skipped;

        protected CriteriaToTreeProcessor(INodesFactory nodesFactory, IList<CriteriaOperator> skippedHolder);
        protected CriteriaToTreeProcessor(INodesFactory nodesFactory, IList<CriteriaOperator> skippedHolder, bool supportFunctions);
        protected IClauseNode CreateClauseNode(CriteriaOperator original, ClauseType type, CriteriaOperator firstOperand, ICollection<CriteriaOperator> operands);
        private INode CreateNodeForUnaryClause(FunctionOperator theOperator, ClauseType clauseType);
        INode IClientCriteriaVisitor<INode>.Visit(AggregateOperand theOperand);
        INode IClientCriteriaVisitor<INode>.Visit(JoinOperand theOperand);
        INode IClientCriteriaVisitor<INode>.Visit(OperandProperty theOperand);
        INode ICriteriaVisitor<INode>.Visit(BetweenOperator theOperator);
        INode ICriteriaVisitor<INode>.Visit(BinaryOperator theOperator);
        INode ICriteriaVisitor<INode>.Visit(FunctionOperator theOperator);
        INode ICriteriaVisitor<INode>.Visit(GroupOperator theOperator);
        INode ICriteriaVisitor<INode>.Visit(InOperator theOperator);
        INode ICriteriaVisitor<INode>.Visit(OperandValue theOperand);
        INode ICriteriaVisitor<INode>.Visit(UnaryOperator theOperator);
        private INode DoStartsEndsContains(FunctionOperator opa);
        private ClauseType GetInvertedOperation(IClauseNode subNode);
        public static INode GetTree(INodesFactory nodesFactory, CriteriaOperator op, IList<CriteriaOperator> skippedCriteria, bool supportFunctions = false);
        private bool IsConstantPercent(CriteriaOperator op);
        public static bool IsConvertibleOperator(CriteriaOperator opa, bool supportFunctions = false);
        private bool IsGoodForAdditionalOperands(CriteriaOperator opa);
        private INode Process(CriteriaOperator op);
        protected IClauseNode Skip(CriteriaOperator skip);
        private INode TryCreateNodeForFunctionClause(FunctionOperator theOperator, CriteriaOperator operandProperty, CriteriaOperator additionalOperand = null);

        private class IsConvertibleClauseNode : IClauseNode, INode
        {
            private readonly OperandProperty firstOperand;
            private readonly ClauseType operation;
            private readonly IList<CriteriaOperator> additinalOperands;
            private IGroupNode ParentNodeCore;

            public IsConvertibleClauseNode(ClauseType type, OperandProperty firstOperand, IList<CriteriaOperator> operands);
            object INode.Accept(INodeVisitor visitor);
            public void SetParentNode(IGroupNode node);

            public OperandProperty FirstOperand { get; }

            public ClauseType Operation { get; }

            public IList<CriteriaOperator> AdditionalOperands { get; }

            public IGroupNode ParentNode { get; }
        }

        private class IsConvertibleFactory : INodesFactory
        {
            public IGroupNode Create(GroupType type, ICollection<INode> subNodes);
            public IClauseNode Create(ClauseType type, OperandProperty firstOperand, ICollection<CriteriaOperator> operands);
        }

        private class IsConvertibleGroupNode : IGroupNode, INode
        {
            private readonly GroupType nodeType;
            private readonly IList<INode> subNodes;
            private IGroupNode ParentNodeCore;

            public IsConvertibleGroupNode(GroupType nodeType, IList<INode> subNodes);
            object INode.Accept(INodeVisitor visitor);
            public void SetParentNode(IGroupNode node);

            public GroupType NodeType { get; }

            public IList<INode> SubNodes { get; }

            public IGroupNode ParentNode { get; }
        }
    }
}

