namespace DevExpress.Data.XtraReports.ServiceModel.DataContracts
{
    using DevExpress.Data.Browsing.Design;
    using System;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [EditorBrowsable(EditorBrowsableState.Never), Obsolete("This class is no longer used in the current implementation."), DataContract]
    public class PropertyDescriptorProxy : IPropertyDescriptor
    {
        public PropertyDescriptorProxy();
        public PropertyDescriptorProxy(TypeSpecifics specifics);
        public static PropertyDescriptorProxy CreateFrom(IPropertyDescriptor from);
        public override string ToString();

        [DataMember]
        public string DisplayName { get; set; }

        [DataMember]
        public bool IsComplex { get; set; }

        [DataMember]
        public bool IsListType { get; set; }

        [DataMember]
        public string Name { get; set; }

        [DataMember]
        public string ToStringValue { get; set; }

        [DataMember]
        public TypeSpecifics Specifics { get; set; }
    }
}

