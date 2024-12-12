namespace DevExpress.Data.Filtering.Helpers
{
    using DevExpress.Data.Filtering;
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class SerializableTreeAggregateNode : SerializableTreeClauseNode, IAggregateNode, IClauseNode, INode
    {
        private OperandProperty _AggregateOperand;
        private INode _AggregateCondition;
        private DevExpress.Data.Filtering.Aggregate _Aggregate;

        protected override object Accept(INodeVisitor visitor);

        public OperandProperty AggregateOperand { get; set; }

        [XmlAttribute]
        public DevExpress.Data.Filtering.Aggregate Aggregate { get; set; }

        [XmlAttribute]
        public INode AggregateCondition { get; set; }
    }
}

