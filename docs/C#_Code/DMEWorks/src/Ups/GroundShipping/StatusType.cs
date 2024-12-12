namespace Ups.GroundShipping
{
    using System;
    using System.CodeDom.Compiler;
    using System.ComponentModel;
    using System.Diagnostics;
    using System.Xml.Serialization;

    [Serializable, GeneratedCode("System.Xml", "4.6.1586.0"), DebuggerStepThrough, DesignerCategory("code"), XmlType(Namespace="http://www.ups.com/XMLSchema/XOLTWS/FreightShip/v1.0")]
    public class StatusType
    {
        private string codeField;
        private string descriptionField;

        public string Code
        {
            get => 
                this.codeField;
            set => 
                this.codeField = value;
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

