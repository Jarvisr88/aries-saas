namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Xml.Serialization;

    [Serializable, XmlInclude(typeof(SerializableTreeGroupNode)), XmlInclude(typeof(SerializableTreeClauseNode))]
    public abstract class SerializableTreeNode : INode
    {
        protected SerializableTreeNode();
        protected abstract object Accept(INodeVisitor visitor);
        object INode.Accept(INodeVisitor visitor);
        public void SetParentNode(IGroupNode parentNode);

        [XmlIgnore]
        public IGroupNode ParentNode { get; set; }
    }
}

