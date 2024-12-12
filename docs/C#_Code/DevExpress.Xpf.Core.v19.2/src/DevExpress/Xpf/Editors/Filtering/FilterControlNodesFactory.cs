namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;

    public class FilterControlNodesFactory : INodesFactory
    {
        public IGroupNode Create(GroupType type, ICollection<INode> subNodes)
        {
            GroupNode parentNode = new GroupNode {
                NodeType = type
            };
            foreach (INode node2 in subNodes)
            {
                node2.SetParentNode(parentNode);
                parentNode.SubNodes.Add(node2);
            }
            return parentNode;
        }

        public IClauseNode Create(ClauseType type, OperandProperty firstOperand, ICollection<CriteriaOperator> operands)
        {
            ClauseNode node = new ClauseNode {
                Operation = type,
                FirstOperand = firstOperand
            };
            node.RepopulateAdditionalOperands(operands);
            return node;
        }
    }
}

