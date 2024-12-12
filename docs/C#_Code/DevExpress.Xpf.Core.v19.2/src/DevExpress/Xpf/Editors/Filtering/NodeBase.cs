namespace DevExpress.Xpf.Editors.Filtering
{
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Windows;

    public abstract class NodeBase : DependencyObject, INode
    {
        private FilterControl owner;
        private GroupNode parentNode;
        private FilterControlNodeBase visualNode;

        protected NodeBase()
        {
        }

        protected abstract object Accept(INodeVisitor visitor);
        object INode.Accept(INodeVisitor visitor) => 
            this.Accept(visitor);

        void INode.SetParentNode(IGroupNode node)
        {
            this.SetParentNode(node);
        }

        public virtual void SetOwner(FilterControl owner, GroupNode parentNode)
        {
            this.SetParentNode(parentNode);
            this.owner = owner;
        }

        private void SetParentNode(INode node)
        {
            this.parentNode = (GroupNode) node;
        }

        public virtual void SetVisualNode(FilterControlNodeBase visualNode)
        {
            this.visualNode = visualNode;
        }

        public FilterControl Owner =>
            this.owner;

        public FilterControlNodeBase VisualNode =>
            this.visualNode;

        public IGroupNode ParentNode =>
            this.parentNode;
    }
}

