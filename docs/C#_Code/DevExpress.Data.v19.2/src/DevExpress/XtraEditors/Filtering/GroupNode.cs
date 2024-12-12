namespace DevExpress.XtraEditors.Filtering
{
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;

    public class GroupNode : Node, IGroupNode, INode
    {
        private GroupType _NodeType;
        private Node.ObservableList<INode> _SubNodes;

        public GroupNode(FilterTreeNodeModel model);
        protected override object Accept(INodeVisitor visitor);
        public override void AddElement();
        public void AddNode(INode node);
        protected override void ChangeElement(NodeEditableElement element, object value);
        public override void DeleteElement();
        public override void GetAbsoluteList(List<Node> list);
        public override IList<INode> GetChildren();
        public int GetHighestLevelOfChildren();
        public Node GetNodeByIndex(int index);
        public override void RebuildElements();
        protected internal void ReplaceClauseNodes(ClauseNode oldNode, ClauseNode newNode);

        public GroupType NodeType { get; set; }

        public IList<INode> SubNodes { get; }

        public override string Text { get; }
    }
}

