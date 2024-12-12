namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class ServerModeSummaryDescriptorSerializationWrapper
    {
        public CriteriaOperator SummaryExpression;
        [XmlAttribute]
        public Aggregate SummaryType;

        public ServerModeSummaryDescriptorSerializationWrapper()
        {
        }

        public ServerModeSummaryDescriptorSerializationWrapper(ServerModeSummaryDescriptor summaryDescriptor)
        {
            this.SummaryExpression = summaryDescriptor.SummaryExpression;
            this.SummaryType = summaryDescriptor.SummaryType;
        }

        public ServerModeSummaryDescriptor ToServerModeSummaryDescriptor() => 
            new ServerModeSummaryDescriptor(this.SummaryExpression, this.SummaryType);
    }
}

