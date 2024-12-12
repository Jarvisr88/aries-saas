namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class PackingListReferenceType
    {
        private string labelField;
        private string valueField;

        public string Label
        {
            get => 
                this.labelField;
            set => 
                this.labelField = value;
        }

        public string Value
        {
            get => 
                this.valueField;
            set => 
                this.valueField = value;
        }
    }
}

