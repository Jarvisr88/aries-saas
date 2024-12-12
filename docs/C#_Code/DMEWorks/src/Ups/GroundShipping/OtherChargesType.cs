namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/IF/v1.0")]
    public class OtherChargesType
    {
        private string monetaryValueField;
        private string descriptionField;

        public string MonetaryValue
        {
            get => 
                this.monetaryValueField;
            set => 
                this.monetaryValueField = value;
        }

        public string Description
        {
            get => 
                this.descriptionField;
            set => 
                this.descriptionField = value;
        }
    }
}

