namespace DevExpress.Xpf.Core.ServerMode
{
    using DevExpress.Data;
    using DevExpress.Data.Filtering;
    using System;
    using System.Xml.Serialization;

    [Serializable]
    public class ServerModeOrderDescriptorSerializationWrapper
    {
        public CriteriaOperator SortExpression;
        [XmlAttribute]
        public bool IsDesc;

        public ServerModeOrderDescriptorSerializationWrapper()
        {
        }

        public ServerModeOrderDescriptorSerializationWrapper(ServerModeOrderDescriptor orderDescriptor)
        {
            this.SortExpression = orderDescriptor.SortExpression;
            this.IsDesc = orderDescriptor.IsDesc;
        }

        public ServerModeOrderDescriptor ToServerModeOrderDescriptor() => 
            new ServerModeOrderDescriptor(this.SortExpression, this.IsDesc);
    }
}

