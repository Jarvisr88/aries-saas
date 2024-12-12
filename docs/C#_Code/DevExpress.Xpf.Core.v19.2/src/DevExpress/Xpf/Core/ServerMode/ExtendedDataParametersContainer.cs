namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class ExtendedDataParametersContainer
    {
        [XmlAttribute]
        public ExtendedOperationType OperationType;
        public CriteriaOperator[] KeysCriteria;
        public CriteriaOperator Expression;
        public CriteriaOperator Where;
        public CriteriaOperator GroupWhere;
        public ServerModeOrderDescriptorSerializationWrapper GroupByDescriptor;
        public ServerModeOrderDescriptorSerializationWrapper[] Order;
        public ServerModeSummaryDescriptorSerializationWrapper[] Summaries;
        [XmlAttribute]
        public int Skip;
        [XmlAttribute]
        public int Take;
        [XmlAttribute]
        public int MaxCount;

        public ExtendedDataParametersContainer()
        {
        }

        public ExtendedDataParametersContainer(CriteriaOperator where)
        {
            this.OperationType = ExtendedOperationType.GetCount;
            this.Where = where;
        }

        public ExtendedDataParametersContainer(CriteriaOperator where, ServerModeSummaryDescriptor[] summaries)
        {
            this.OperationType = ExtendedOperationType.PrepareTopGroupInfo;
            this.Where = where;
            this.Summaries = ExtendedDataHelper.WrapSummaries(summaries);
        }

        public ExtendedDataParametersContainer(CriteriaOperator groupWhere, ServerModeOrderDescriptor groupByDescriptor, ServerModeSummaryDescriptor[] summaries)
        {
            this.OperationType = ExtendedOperationType.PrepareChildren;
            this.GroupWhere = groupWhere;
            this.GroupByDescriptor = new ServerModeOrderDescriptorSerializationWrapper(groupByDescriptor);
            this.Summaries = ExtendedDataHelper.WrapSummaries(summaries);
        }

        public ExtendedDataParametersContainer(CriteriaOperator expression, int maxCount, CriteriaOperator where)
        {
            this.OperationType = ExtendedOperationType.GetUniqueValues;
            this.Expression = expression;
            this.MaxCount = maxCount;
            this.Where = where;
        }

        public ExtendedDataParametersContainer(CriteriaOperator[] keysCriteria, CriteriaOperator where, ServerModeOrderDescriptor[] order, int skip, int take)
        {
            this.OperationType = ExtendedOperationType.FetchKeys;
            this.KeysCriteria = keysCriteria;
            this.Where = where;
            this.Order = ExtendedDataHelper.WrapOrder(order);
            this.Skip = skip;
            this.Take = take;
        }
    }
}

