namespace DevExpress.Xpo.DB
{
    using DevExpress.Data.Filtering;
    using DevExpress.Data.Filtering.Helpers;
    using DevExpress.Utils;
    using System;
    using System.Collections.Generic;
    using System.Xml.Serialization;

    [Serializable]
    public class QuerySubQueryContainer : CriteriaOperator
    {
        private BaseStatement node;
        private Aggregate aggregateType;
        private CriteriaOperator aggregateProperty;
        private string customAggregateName;
        private readonly CriteriaOperatorCollection customAggregateOperands;
        private static int HashSeed = HashCodeHelper.StartGeneric<string>(typeof(QuerySubQueryContainer).Name);

        public QuerySubQueryContainer() : this(null, null, Aggregate.Exists)
        {
        }

        public QuerySubQueryContainer(BaseStatement node, CriteriaOperator aggregateProperty, Aggregate aggregateType)
        {
            this.customAggregateOperands = new CriteriaOperatorCollection();
            if (aggregateType == Aggregate.Custom)
            {
                throw new ArgumentException("The type Custom is not valid in the current context. Use the overloaded constructor that accepts the parameter 'customAggregateName'.", "aggregateType");
            }
            this.node = node;
            this.aggregateProperty = aggregateProperty;
            this.aggregateType = aggregateType;
        }

        public QuerySubQueryContainer(BaseStatement node, IEnumerable<CriteriaOperator> aggregateOperands, string customAggregateName)
        {
            this.customAggregateOperands = new CriteriaOperatorCollection();
            if (string.IsNullOrEmpty(customAggregateName))
            {
                throw new ArgumentNullException("customAggregateName");
            }
            this.node = node;
            this.aggregateType = Aggregate.Custom;
            this.customAggregateName = customAggregateName;
            this.customAggregateOperands.AddRange(aggregateOperands);
        }

        public override void Accept(ICriteriaVisitor visitor)
        {
            ((IQueryCriteriaVisitor) visitor).Visit(this);
        }

        public override T Accept<T>(ICriteriaVisitor<T> visitor) => 
            ((IQueryCriteriaVisitor<T>) visitor).Visit(this);

        protected override CriteriaOperator CloneCommon()
        {
            throw new NotSupportedException();
        }

        public override bool Equals(object obj)
        {
            QuerySubQueryContainer criterion = obj as QuerySubQueryContainer;
            return (!criterion.ReferenceEqualsNull() ? (Equals(this.Node, criterion.node) && (Equals(this.AggregateProperty, criterion.AggregateProperty) && ((this.AggregateType == criterion.AggregateType) && ((this.CustomAggregateName == criterion.CustomAggregateName) && this.CustomAggregateOperands.Equals(criterion.CustomAggregateOperands))))) : false);
        }

        public override int GetHashCode() => 
            HashCodeHelper.FinishGeneric<Aggregate, string, CriteriaOperator, CriteriaOperatorCollection, BaseStatement>(HashSeed, this.AggregateType, this.CustomAggregateName, this.AggregateProperty, this.CustomAggregateOperands, this.Node);

        public BaseStatement Node
        {
            get => 
                this.node;
            set => 
                this.node = value;
        }

        [XmlAttribute]
        public Aggregate AggregateType
        {
            get => 
                this.aggregateType;
            set => 
                this.aggregateType = value;
        }

        public string CustomAggregateName
        {
            get => 
                this.customAggregateName;
            set => 
                this.customAggregateName = value;
        }

        public CriteriaOperator AggregateProperty
        {
            get => 
                this.aggregateProperty;
            set => 
                this.aggregateProperty = value;
        }

        public CriteriaOperatorCollection CustomAggregateOperands =>
            this.customAggregateOperands;
    }
}

