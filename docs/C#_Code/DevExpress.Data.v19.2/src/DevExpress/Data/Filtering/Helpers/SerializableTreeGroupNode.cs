namespace DevExpress.Data.Filtering.Helpers
{
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class SerializableTreeGroupNode : SerializableTreeNode, IGroupNode, INode
    {
        private GroupType _NodeType;
        private List<SerializableTreeNode> _SubNodes;

        public SerializableTreeGroupNode();
        protected override object Accept(INodeVisitor visitor);

        [XmlAttribute]
        public GroupType NodeType { get; set; }

        [XmlArrayItem(typeof(SerializableTreeNode))]
        public List<SerializableTreeNode> SubNodes { get; }

        IList<INode> IGroupNode.SubNodes { get; }
    }
}

