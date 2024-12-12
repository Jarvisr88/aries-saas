namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class SerializableTreeClauseNode : SerializableTreeNode, IClauseNode, INode
    {
        private OperandProperty _FirstOperand;
        private ClauseType _Operation;
        private List<CriteriaOperator> _AdditionalOperands;

        public SerializableTreeClauseNode();
        protected override object Accept(INodeVisitor visitor);

        public OperandProperty FirstOperand { get; set; }

        [XmlAttribute]
        public ClauseType Operation { get; set; }

        [XmlArrayItem(typeof(CriteriaOperator))]
        public List<CriteriaOperator> AdditionalOperands { get; }

        IList<CriteriaOperator> IClauseNode.AdditionalOperands { get; }
    }
}

