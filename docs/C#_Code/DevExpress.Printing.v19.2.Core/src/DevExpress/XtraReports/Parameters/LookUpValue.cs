namespace DevExpress.XtraReports.Parameters
{
    using DevExpress.Utils.Serializing;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;
    using System.Runtime.CompilerServices;
    using System.Runtime.Serialization;

    [DataContract]
    public class LookUpValue
    {
        public const string ValuePropertyName = "Value";
        public const string DescriptionPropertyName = "RealDescription";

        public LookUpValue()
        {
        }

        public LookUpValue(object value, string description)
        {
            this.Value = value;
            this.Description = description;
        }

        [EditorBrowsable(EditorBrowsableState.Never)]
        public LookUpValue Clone() => 
            new LookUpValue(this.Value, this.Description);

        [EditorBrowsable(EditorBrowsableState.Never)]
        public static IEqualityComparer<LookUpValue> CreateComparer() => 
            new LookUpValueValueComparer();

        [XtraSerializableProperty(XtraSerializationVisibility.Reference), DataMember]
        public object Value { get; set; }

        [XtraSerializableProperty, DataMember]
        public string Description { get; set; }

        [Browsable(false)]
        public string RealDescription =>
            !string.IsNullOrEmpty(this.Description) ? this.Description.TrimEnd(new char[0]) : ((this.Value != null) ? this.Value.ToString() : string.Empty);
    }
}

