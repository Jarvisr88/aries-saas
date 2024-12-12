namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/Error/v1.1")]
    public class AdditionalInfoType
    {
        private string typeField;
        private AdditionalCodeDescType[] valueField;

        public string Type
        {
            get => 
                this.typeField;
            set => 
                this.typeField = value;
        }

        [XmlElement("Value")]
        public AdditionalCodeDescType[] Value
        {
            get => 
                this.valueField;
            set => 
                this.valueField = value;
        }
    }
}

