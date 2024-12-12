namespace DevExpress.XtraEditors.Filtering
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using System;
    using System.Collections.Generic;
    using System.Runtime.CompilerServices;

    public class AggregateNode : ClauseNode, IAggregateNode, IClauseNode, INode
    {
        private DevExpress.Data.Filtering.Aggregate _Aggregate;
        private OperandProperty _AggregateOperand;
        private INode _AggregateCondtion;
        private IList<INode> children;
        private IBoundPropertyCollection childrenFilterProperties;

        public AggregateNode(FilterTreeNodeModel model);
        protected override object Accept(INodeVisitor visitor);
        private void AddAggregationToChildren();
        protected override void ChangeElement(NodeEditableElement element, object value);
        protected override void ClauseNodeFirstOperandChanged(OperandProperty newProp, int elementIndex);
        private void CreateAggregateCondtion();
        protected string GetAggregatedDisplayText(OperandProperty property);
        private OperandProperty GetAggregateProperty(string propertyName);
        public List<DevExpress.Data.Filtering.Aggregate> GetAvailableAggregateOperations();
        public List<IBoundProperty> GetAvailableAggregateProperties();
        protected List<IBoundProperty> GetAvailableAggregateProperties(DevExpress.Data.Filtering.Aggregate forAggregate);
        public override List<ClauseType> GetAvailableOperations();
        protected IBoundProperty GetChildByCaption(string caption);
        protected IBoundProperty GetChildByName(string name);
        public override IList<INode> GetChildren();
        protected override IBoundPropertyCollection GetChildrenFilterProperties();
        protected IBoundProperty GetChildrenFilterProperty(OperandProperty property);
        public override IBoundProperty GetPropertyForEditing();
        protected override Type GetValueType();
        protected override bool IsRequireChangeNodeType(IBoundProperty newProperty);
        public override void RebuildElements();
        protected void ValidateAggregate();
        protected void ValidateAggregateProperty();
        protected void ValidateOperation();

        public override bool IsList { get; }

        public DevExpress.Data.Filtering.Aggregate Aggregate { get; set; }

        public OperandProperty AggregateOperand { get; set; }

        public INode AggregateCondition { get; set; }

        public IBoundProperty AggregateProperty { get; }

        [Serializable, CompilerGenerated]
        private sealed class <>c
        {
            public static readonly AggregateNode.<>c <>9;
            public static Comparison<IBoundProperty> <>9__32_0;

            static <>c();
            internal int <GetAvailableAggregateProperties>b__32_0(IBoundProperty l, IBoundProperty r);
        }
    }
}

